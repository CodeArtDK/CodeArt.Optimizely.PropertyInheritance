namespace PropertyInheritance.Inheritance
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InheritAttribute : Attribute
    {
        /// <summary>
        /// Name of Boolean property that indicates if this property should be inherited
        /// </summary>
        public string SwitchPropertyName { get; set; }

        /// <summary>
        /// Inherit this value if it's null
        /// </summary>
        public bool InheritIfNull { get; set; }

        /// <summary>
        /// Inherit this value if it's null or empty
        /// </summary>
        public bool InheritIfNullOrEmpty { get; set; }

        /// <summary>
        /// Name of property on parent content to inherit from. Default is same name.
        /// </summary>
        public string ParentPropertyToInheritFrom { get; set; }

        /// <summary>
        /// Keep searching ancestors until Root
        /// </summary>
        public bool SearchAllAncestors { get; set; }

    }
}
