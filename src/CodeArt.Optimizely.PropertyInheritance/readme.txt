CodeArt.Optimizely.PropertyInheritance

Installation
============


In order to start using property inheritacnce module you need to add it explicitly to your site.
Please add the following statement to your Startup.cs

public class Startup
{
    ...
    public void ConfigureServices(IServiceCollection services)
    {
        ...
        services.AddPropertyInheritance();
        ...
    }
    ...
}