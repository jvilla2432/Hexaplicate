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
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            List<Recipe> recipes = new();
            using (XmlReader reader = XmlReader.Create(@file,settings))
            {
                reader.Read(); //xml versioning
                while (reader.Read())
                {
                    //Recipe
                    string name = getValue(reader);
                    string description = getValue(reader);
                    int ticks = int.Parse(getValue(reader));
                    (int resource, int cost, bool type)[] inputs = readValue(reader);
                    (int resource, int cost, bool type)[] outputs = readValue(reader);
                    recipes.Add(new Recipe(ticks, name, description, inputs,outputs));
                    reader.Read(); // recipe
                }
            }
            Constants.recipes = recipes.ToArray();
        }
        private static string getValue(XmlReader reader)
        {
            reader.Read(); 
            reader.Read(); 
            string value = reader.Value;
            reader.Read();
            return value;
        }
        private static (int resource, int cost, bool type)[] readValue(XmlReader reader)
        {
            reader.Read(); //input/output
            string start = reader.Name;
            reader.Read();
            List<(int resource, int cost, bool type)> pairs = new();
            while (reader.Name != start) { 
                 //pair
                int resource = int.Parse(getValue(reader));
                int amount = int.Parse(getValue(reader));
                bool type = true;
                if (getValue(reader) == "Resource")
                {
                    type = false;
                }
                pairs.Add((resource, amount, type));
                reader.Read(); //pair
                reader.Read();
            }
            return pairs.ToArray(); 
        }
    }
}
