﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorFabric
{
    public partial class Tooltip : FabricComponentBase
    {
        [Parameter] public int BeakWidth { get; set; } = 16;
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public TooltipDelay Delay { get; set; } = TooltipDelay.Medium;
        [Parameter] public DirectionalHint DirectionalHint { get; set; } = DirectionalHint.TopCenter;
        [Parameter] public FabricComponentBase FabricComponentTarget { get; set; }
        [Parameter] public double MaxWidth { get; set; } = 364;
        [Parameter] public EventCallback<EventArgs> OnMouseEnter { get; set; }
        [Parameter] public EventCallback<EventArgs> OnMouseLeave { get; set; }

        private ICollection<Rule> TooltipGlobalRules { get; set; } = new List<Rule>();
        private ICollection<Rule> TooltipLocalRules { get; set; } = new List<Rule>();

        private Rule TooltipRule = new Rule();
        private Rule TooltipAfterRule = new Rule();
        private double TooltipGabSpace;

        protected override void OnInitialized()
        {
            CreateLocalCss();
            base.OnInitialized();
        }

        protected override void OnParametersSet()
        {
            CreateGlobalCss();
            SetStyle();
            base.OnParametersSet();
        }

        private void CreateLocalCss()
        {
            TooltipRule.Selector = new ClassSelector() { SelectorName = "ms-Tooltip" };
            TooltipAfterRule.Selector = new ClassSelector() { SelectorName = "ms-Tooltip", PseudoElement = PseudoElements.After };
            TooltipLocalRules.Add(TooltipRule);
            TooltipLocalRules.Add(TooltipAfterRule);
        }

        private void CreateGlobalCss()
        {
            TooltipGlobalRules.Clear();
            TooltipGlobalRules.Add(new Rule()
            {
                Selector = new CssStringSelector() { SelectorName = ".ms-Tooltip-content" },
                Properties = new CssString()
                {
                    Css = $"position:relative;" +
                            $"z-index:1;" +
                            $"color:{Theme.SemanticTextColors.MenuItemText};" +
                            $"word-wrap:break-word;" +
                            $"overflow-wrap:break-word;" +
                            $"overflow:hidden;"
                }
            });
            TooltipGlobalRules.Add(new Rule()
            {
                Selector = new CssStringSelector() { SelectorName = ".ms-Tooltip-subtext" },
                Properties = new CssString()
                {
                    Css = $"font-size:inherit;" +
                            $"font-weight:inherit;" +
                            $"color:inherit;" +
                            $"margin:0;"
                }
            });
        }

        private void SetStyle()
        {
            TooltipGabSpace = -(Math.Sqrt((BeakWidth * BeakWidth) / 2) + 0);
            TooltipRule.Properties = new CssString()
            {
                Css = $"background:{Theme.SemanticColors.MenuBackground};" +
                            $"box-shadow:var(--effects-Elevation8);" +
                            $"padding:8px;" +
                            $"max-width:{MaxWidth}px;"
            };
            TooltipAfterRule.Properties = new CssString()
            {
                Css = $"content:'';" +
                        $"position:absolute;" +
                        $"bottom:{TooltipGabSpace}px;" +
                        $"left:{TooltipGabSpace}px;" +
                        $"right:{TooltipGabSpace}px;" +
                        $"top:{TooltipGabSpace}px;" +
                        $"z-index:0;"
            };
        }
    }
}
