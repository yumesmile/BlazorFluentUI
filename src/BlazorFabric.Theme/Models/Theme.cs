﻿using BlazorFabric.Interfaces;

namespace BlazorFabric
{
    public class Theme : ITheme
    {
        public IPalette Palette { get; set; }
        public ISemanticTextColors SemanticTextColors { get; set; }
        public ISemanticColors SemanticColors { get; set; }
        public IFontStyle FontStyle { get; set; }
        public ICommonStyle CommonStyle { get; set; }
        public IZIndex ZIndex { get; set; }
    }
}
