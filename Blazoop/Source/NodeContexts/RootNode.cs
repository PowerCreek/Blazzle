using System.ComponentModel;
using Blazoop.ExternalDeps.Classes;
using Blazoop.ExternalDeps.Classes.Management;
using Blazoop.Source.ElementContexts;
using Blazoop.Source.Operations;

namespace Blazoop.Source.NodeContexts
{
    public interface IRootElement
    {
        public ElementContext RootElement { get; init; }
        public IServiceData ServiceData { get; }
    }
    
    public class RootNode : NodeBase
    {
        public override ElementContext RootElement { get; init; }
        public LinkMember Node { get; set; }
        
        public override IServiceData ServiceData { get; }
 
        public RootNode(IServiceData serviceData)
        {
            ServiceData = serviceData;

            Node = (RootElement = new ContainerContext(this))
                .Get<LinkMember>("node");
    

            var WindowContextOp = serviceData.OperationManager.GetOperation<WindowContextOperator>();
            WindowContextOp.Register(serviceData);
            
            WindowContextOp.DoThing();

            //setup first window
        }
    }
}