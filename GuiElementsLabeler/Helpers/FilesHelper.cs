using System;
using System.IO;
using GuiElementsLabeler.ProjectModel;
using Newtonsoft.Json;

namespace GuiElementsLabeler.Helpers
{
    public static class FilesHelper
    {
        public static void SaveJsonFile(Elements elements)
        {
            string output = JsonConvert.SerializeObject(elements, Formatting.Indented);
            try
            {
                StreamWriter sw = new StreamWriter(@"C:\Temp\Photos\data\json.json");
                sw.WriteLine(output);
                sw.Close();
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public static void SaveProjectFile(Rectangles rectangles)
        {
            string output = JsonConvert.SerializeObject(rectangles, Formatting.Indented);
            try
            {
                StreamWriter sw = new StreamWriter(@"C:\Temp\Photos\data\rectangles.data");
                sw.WriteLine(output);
                sw.Close();
                Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public static void ReadProjectFile()
        {
            Rectangles userRectangles = JsonConvert.DeserializeObject<Rectangles>(File.ReadAllText(@"C:\Temp\Photos\data\rectangles.data"));
        }
    }
}
