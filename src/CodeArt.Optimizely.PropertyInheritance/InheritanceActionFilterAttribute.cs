using EPiServer.Core;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CodeArt.Optimizely.PropertyInheritance
{
    public class ApplyInheritanceAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            //if(context.Controller is PageController)
            if (context.ActionArguments.ContainsKey("currentPage"))
            {
                var obj = context.ActionArguments["currentPage"];
                if (obj != null && obj is PageData)
                {
                    var pageData = (PageData)obj;
                    context.ActionArguments["currentPage"] = pageData.PopulateInheritedProperties();
                }
            }
        }
        public ApplyInheritanceAttribute()
        {
        }
    }
}
