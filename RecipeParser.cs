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
        public static void parseRecipe(string file)
        {
            using (XmlReader reader = XmlReader.Create(@file))
            {
                while (reader.Read())
                {

                }
            }
        }
    }
}
