define([
    "dojo",
    "dojo/_base/declare",
    "dojo/dom-construct",
    "dojo/on",

    "epi/dependency",
    "epi/routes",
    "epi/_Module",
    "epi/shell/form/Field",
    "epi/shell/form/formFieldRegistry",
    "./fieldFactory"
], function (
    dojo,
    declare,
    domConstruct,
    on,

    dependency,
    routes,
    _Module,
    Field,
    formFieldRegistry,
    FieldFactory
) {
    return declare([_Module], {
        initialize: function () {
            this.inherited(arguments);

            var fieldFactory = new FieldFactory();
            formFieldRegistry.add(fieldFactory.createFactory());
        }
    });
});