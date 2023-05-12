using EPiServer.Data.Entity;
using EPiServer.ServiceLocation;
using System.Reflection;

namespace PropertyInheritance.Inheritance
{
    public static class PropertyInheritor
    {
        public static IEnumerable<PropertyInfo> GetPropertiesWithAttribute(this IContent c, Type a)
        {
            return c.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, a, true));
        }

        public static IEnumerable<PropertyInfo> GetEmptyPropertiesWithAttribute(this IContent c, Type a)
        {
            return c.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, a, true)).Where(prop => prop.GetValue(c) == null);
        }


        public static T PopulateInheritedProperties<T>(this T Content) where T : IContentData
        {
            if (!(Content is PageData))   return Content;

            var rt = (Content as IReadOnly).CreateWritableClone() as PageData;
            var props = (Content as PageData).GetPropertiesWithAttribute(typeof(InheritAttribute));
            bool modified = false;
            foreach (var prop in props)
            {
                var attr = prop.GetCustomAttribute<InheritAttribute>(true);

                if (
                    (!String.IsNullOrEmpty(attr.SwitchPropertyName) && ((bool)Content.GetType().GetProperty(attr.SwitchPropertyName).GetValue(Content))) ||
                    ((attr.InheritIfNull || attr.InheritIfNullOrEmpty) && (prop.GetValue(Content) == null)) ||
                    (attr.InheritIfNullOrEmpty && ((prop.PropertyType == typeof(ContentArea)) && (prop.GetValue(Content) as ContentArea).Count == 0))
                    )
                {
                    //Resolve Inherited Properties
                    var repo = ServiceLocator.Current.GetInstance<IContentRepository>();
                    foreach (var a in repo.GetAncestors((Content as PageData).ContentLink).Take((attr.SearchAllAncestors) ? 1000 : 1))
                    {
                        var parentprop = (a as IContentData).Property[attr.ParentPropertyToInheritFrom ?? prop.Name];
                        if (parentprop != null && !parentprop.IsNull)
                        {
                            prop.SetValue(rt, parentprop.Value);
                            modified = true;
                            break;
                        }
                    }
                }
            }
            if (modified)
            {
                rt.MakeReadOnly();
                return (T)((IContentData)rt);
            }
            return Content;

        }


    }
}
