using System.Collections.Generic;
using System.Drawing;

namespace GuiElementsLabeler.ProjectModel
{
    public class UserRectangle
    {
        public string FileName { get; set; }

        public Rectangle Rectangle { get; set; }
    }

    public class Rectangles
    {
        public List<UserRectangle> ListRectangles { get; set; }
    }
}