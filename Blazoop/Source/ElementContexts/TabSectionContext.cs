using Blazoop.ExternalDeps.Classes;
using Blazoop.ExternalDeps.Classes.Management;
using Blazoop.ExternalDeps.Interfaces;
using Blazoop.Source.NodeContexts;
using Blazoop.Source.Operations;

namespace Blazoop.Source.ElementContexts
{
    public class TabSectionContext : ElementContext, INodeElement
    {
        public StyleOperator StyleOperator { get; }
        public LinkMember ElementNode { get; }
        
        public TabSectionContext(NodeBase nodeBase) : base($"Tabs_{nodeBase.Id}_")
        {
            StyleOperator = nodeBase.ServiceData.OperationManager.GetOperation<StyleOperator>();
            Add("node", ElementNode = new LinkMember(this));
            
            WithAttribute("style", (StyleContext tabsStyle) =>
            {
                tabsStyle.WithStyle(StyleOperator, this,
                    ("position", "relative"),
                    ("background-color", "orange"),
                    ("place-self", "stretch"),
                    ("display", "grid"),
                    ("grid-auto-columns", "min-content"),
                    ("grid-template-areas", "\"\""),
                    ("overflow", "hidden"),
                    ("grid-auto-flow", "column"),
                    ("user-select", "none"));
            });
            
            WithAttribute("draggable", (AttributeString drag) =>
            {
                drag.Value = "false";
            });
        }
    }
}