define([
    "dojo",
    "dojo/Stateful",
    "dojo/when",
    "dojo/Deferred",
    "dojo/_base/declare",
    "dojo/dom-construct",
    "dojo/on",

    // dijit
    "dijit/Destroyable",

    // epi
    "epi/routes",
    "epi/shell/MetadataTransformer",
    "epi/dependency",
    "epi/shell/form/Field",
    "epi/shell/form/formFieldRegistry",
    "epi/shell/widget/dialog/Dialog",

    // epi-cms
    "epi-cms/_ContentContextMixin",
    "epi-cms/widget/_HasChildDialogMixin",

    "xstyle/css!./styles.css"
], function (
    dojo,
    Stateful,
    when,
    Deferred,
    declare,
    domConstruct,
    on,

    // dijit
    Destroyable,

    // epi
    routes,
    MetadataTransformer,
    dependency,
    Field,
    formFieldRegistry,
    Dialog,

    // epi-cms
    _ContentContextMixin,
    _HasChildDialogMixin
) {
    var factory = formFieldRegistry.get(formFieldRegistry.type.field, "");

    return declare([Stateful, _ContentContextMixin, _HasChildDialogMixin, Destroyable], {
        onRevertClick: function (widget) {
            alert("property");
        },

        revertablePropertiesFactory: function (widget, parent) {
            var wrapper = factory(widget, parent);

            if (!widget.params.isInheritedProperty || widget.params.readOnly) {
                return wrapper;
            }

            var undoNode = domConstruct
                .toDom("<span class='dijitInline dijitReset dijitIcon epi-cursor--pointer icon-inheritance' title='Revert to default'></span>");
            domConstruct.place(undoNode, wrapper.labelNode);
            undoNode.title = "Property value is inherited: " +  widget.params.inheritedPropertyValue;
            return wrapper;
        },

        createFactory: function () {
            return {
                type: formFieldRegistry.type.field,
                hint: "",
                factory: this.revertablePropertiesFactory.bind(this)
            }
        }
    });
});