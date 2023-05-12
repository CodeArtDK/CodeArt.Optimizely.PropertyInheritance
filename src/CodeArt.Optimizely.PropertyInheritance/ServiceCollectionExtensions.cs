using EPiServer.Shell.Modules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CodeArt.Optimizely.PropertyInheritance
{
    public static class ServiceCollectionExtensions
    {

            public static IServiceCollection AddPropertyInheritance(
                this IServiceCollection services)
            {
                AddModule(services,"CodeArt.Optimizely.PropertyInheritance");

            services.Configure<MvcOptions>(options => options.Filters.Add<ApplyInheritanceAttribute>());


            return services;
            }

            private static void AddModule(IServiceCollection services, string ModuleName)
            {
                services.Configure<ProtectedModuleOptions>(
                    pm =>
                    {
                        if (!pm.Items.Any(i => i.Name.Equals(ModuleName, StringComparison.OrdinalIgnoreCase)))
                        {
                            pm.Items.Add(new ModuleDetails { Name = ModuleName });
                        }
                    });
            }
        }
}
