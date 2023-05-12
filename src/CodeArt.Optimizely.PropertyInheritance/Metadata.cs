using EPiServer.Framework.Initialization;
using EPiServer.Framework;
using EPiServer.Shell.ObjectEditing;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Cms.Shell.Extensions;

namespace PropertyInheritance.Inheritance
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Cms.Shell.InitializableModule))]
    public class PropertyDefaultValueInitialization : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            context.InitComplete += (sender, args) =>
            {
                var editorRegistry = ServiceLocator.Current.GetInstance<MetadataHandlerRegistry>();
                editorRegistry.RegisterMetadataHandler(typeof(ContentData), new InheritedPropertyMetadata(), null, EditorDescriptorBehavior.PlaceLast);
            };
        }

        public void Uninitialize(InitializationEngine context)
        {
        }
    }

    public class InheritedPropertyMetadata : IMetadataExtender
    {
        public void ModifyMetadata(ExtendedMetadata metadata, IEnumerable<Attribute> attributes)
        {
            if (metadata.Properties.Where(x => x.Attributes.OfType<InheritAttribute>().Any()).Any())
            {
                var content = metadata.Model as PageData;
                if (content == null) return;
                var populated = content.PopulateInheritedProperties();

                foreach (var property in metadata.Properties.Cast<ExtendedMetadata>())
                {
                    if (!property.Attributes.OfType<InheritAttribute>().Any())
                    {
                        continue;
                    }
                    //Check if this has it's own value - or if it is inherited
                    //property.Model.Value
                    //property.FindOwnerContent()
                    var pd = property.Model as PropertyData;
                    if (pd != null && pd.Value != null)
                    {
                        continue;
                    }
                    property.EditorConfiguration["isInheritedProperty"] = true;


                    //TODO: Changes: Always show an indication that a property is an inherited property. Also - if inherited show a value + link to edit mode for page where it's inherited from


                    property.EditorConfiguration["inheritedPropertyValue"] = populated[property.PropertyName]?.ToString();
                }
            }
        }
    }
}
