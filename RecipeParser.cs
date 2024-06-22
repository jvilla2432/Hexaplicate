using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Hexaplicate
{
    internal static class RecipeParser
    {

        private static void recursiveRecipe()
        {

        }
        public static void parseRecipe(string file)
        {
            using (XmlReader reader = XmlReader.Create(@file))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            System.Diagnostics.Debug.WriteLine("Start Element {0}", reader.Name);
                            break;
                        case XmlNodeType.Text:
                            System.Diagnostics.Debug.WriteLine("Text Node: {0}",
                                     reader.Value);
                            break;
                        case XmlNodeType.EndElement:
                            System.Diagnostics.Debug.WriteLine("End Element {0}", reader.Name);
                            break;
                        default:
                            System.Diagnostics.Debug.WriteLine("Other node {0} with value {1}",
                                            reader.NodeType, reader.Value);
                            break;
                    }
                }
            }
        }
    }
}
