using System;
using System.Threading.Tasks;
using Blazoop.ExternalDeps.Classes;
using Blazoop.ExternalDeps.Classes.Management;
using Blazoop.Source.NodeContexts;
using Blazoop.Source.Operations;
using Blazoop.Source.Properties;
using Blazoop.Source.Properties.Vector;

namespace Blazoop.Source.ElementContexts
{
    public class ContainerContext : ElementContext, INodeElement
    {
        public LinkMember ElementNode { get; }
        
        public WindowingService WindowingService { get; }
        
        public StyleOperator StyleOperator { get; }

        public ElementContext Slider { get; }

        public NodeBase NodeBase { get; }
        public LinkMember SlideMember { get; }
        public Transform SliderTransform { get; }
        public Transform ToolbarTransform { get; }
        
        public ContainerContext(NodeBase nodeBase) : base($"Container_{nodeBase.Id}_")
        {
            cssClass = "window-container";
            
            NodeBase = nodeBase;
            Add("node", ElementNode = new LinkMember(this));


            WithAttribute("draggable", (AttributeString drag) =>
            {
                drag.Value = "false";
            });
            
            WindowingService = nodeBase.ServiceData.OperationManager.GetOperation<WindowingService>();
            WindowingService.ContainerContext = this;
            StyleOperator = nodeBase.ServiceData.OperationManager.GetOperation<StyleOperator>();
            
            int ToolbarHeight = 40;
            
            ToolbarTransform = new Transform();
            SliderTransform = new Transform();
            
            WithAttribute("style", (StyleContext styleContext) =>
            {
                styleContext.WithStyle(StyleOperator, this,
                    ("grid-template-rows", "40px auto")); 
                
                ToolbarTransform.OnResize = (transform, size) =>
                {
                    styleContext.WithStyle(StyleOperator, this,
                        ("grid-template-rows", $"{ToolbarHeight}px auto"));
                };
            });

            var Toolbar = new ViewDropdownContext(nodeBase);
            ElementNode.Add(Toolbar.ElementNode);
            
            Slider = new ElementContext("Slider");
            
            Slider.WithAttribute("style", (StyleContext sliderStyle) =>
            {
                SliderTransform.OnMove = (transform1, position) =>
                {
                    sliderStyle.WithStyle(StyleOperator, Slider, 
                        ("margin-top",$"{position.Y+ToolbarHeight}px"),
                        ("margin-left",$"{position.X}px"));
                };
                
                ToolbarTransform.OnResize = (transform, size) =>
                {
                    sliderStyle.WithStyle(StyleOperator, Slider, 
                        ("margin-top",$"{SliderTransform.Position.Y+ToolbarHeight}px"));
                };
            });
            
            Slider.cssClass = "window-container-slider";

            SlideMember = new LinkMember(Slider);
            ElementNode.Add(SlideMember);
            
            WindowingService.CreateUnjoinedWindow();

            ToolbarTransform.Size.Height = ToolbarHeight;
            
            string[] events = { };
            
            StopPropagations.AddRange(events);
            StopPropagations.Add("onwheel");

            PreventDefaults.Add("ondragover");
            PreventDefaults.Add("ondrop");

            AddEvent("onmousedown", WindowingService.ContainerMouseDown);
            AddEvent("onmousemove", WindowingService.ContainerMouseMove);
            AddEvent("onmouseup", WindowingService.ContainerMouseUp);
            AddEvent("onmouseleave", WindowingService.ContainerMouseLeave);
            AddEvent("ondragend", WindowingService.ContainerMouseUp);
            AddEvent("onwheel", WindowingService.OnContainerWheel);
            AddEvent("ondrop", WindowingService.OnContainerTabDropped);
            AddEvent("ondragover", a=>{  });
        }

        public void InitDockSections()
        {
            var (leftLink, leftContext) = WindowingService.DockService.CreateDock($"{Id}_Left");
            leftContext.cssClass = "dockzone";
            leftContext.WithAttribute("style", out StyleContext leftStyle);
            ElementNode.Add(leftLink);
        }
        
        public void SetCursor(string cursor)
        {
            WithAttribute("Style", (StyleContext styleContext) =>
            {
                styleContext.Valid = true;
                styleContext.WithStyle(StyleOperator,
                    this,
                    ("cursor", cursor)
                ); 
            });
        }
    }
}