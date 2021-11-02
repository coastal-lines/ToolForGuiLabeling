using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiElementsLabeler
{
    public class ElementColor
    {
        public string active { get; set; }
        public string non_active { get; set; }
    }

    public class ElementScroll
    {
        public string horizontal { get; set; }
        public string vertical { get; set; }
    }

    public class ElementAdditionalData
    {
        public string arrow { get; set; }
        public string width { get; set; }
        public string heigth { get; set; }
        public string text { get; set; }
    }

    public class Element
    {
        public string name { get; set; }
        public string type { get; set; }
        public string width { get; set; }
        public string heigth { get; set; }
        public string parent { get; set; }
        public ElementColor color { get; set; }
        public string text { get; set; }
        public List<string> columns { get; set; }
        public ElementScroll scroll { get; set; }
        public ElementAdditionalData additional_data { get; set; }
        public string ImagePath { get; set; }
    }

    public class Elements
    {
        public List<Element> elements { get; set; }
    }
}
