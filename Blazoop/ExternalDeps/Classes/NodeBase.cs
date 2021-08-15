using System.ComponentModel.Design;
using Blazoop.ExternalDeps.Classes.Management;
using Blazoop.Source.NodeContexts;

namespace Blazoop.ExternalDeps.Classes
{
    public abstract class NodeBase : IRootElement
    {
        private static int _NodeIndex = 0;
        public static int NextNodalId => _NodeIndex++;
        
        public readonly int Id = NextNodalId;
        
        public abstract ElementContext RootElement { get; init; }
        public abstract IServiceData ServiceData { get; }
    }
}