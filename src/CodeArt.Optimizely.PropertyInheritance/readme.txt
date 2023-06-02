CodeArt.Optimizely.PropertyInheritance

Add-on for Optimizely CMS 12 that enables property inheritance across content types. This solves a similar problem to the old (pre CMS 7) Dynamic Properties.
If a property is not set on a page, it will fallback to inheriting from the ancestors.


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

Usage
=====
In your models, add the following attribute to the properties you want to inherit:

    [Inherit(InheritIfNullOrEmpty = true,SearchAllAncestors =true)]
    public virtual string MyProperty { get; set; }

The attribute has multiple properties:
- SwitchPropertyName. A string with the name of a boolean property that indicates if the property should be inherited or not. If the property is not set, it will be inherited.
- InheritIfNull. If true, the property will be inherited if it's null.
- InheritIfNullOrEmpty. If true, the property will be inherited if it's null or empty. This is true by default
- ParentPropertyToInheritFrom. The name of the property on the parent content to inherit from. If not set, the same property name will be used.
- SearchAllAncestors. If true, the module will search all ancestors until it finds a value. If false, it will only search the direct parent. This is false by default.


Caution
=======
Note, there can be a slight performance penalty when using this module.
