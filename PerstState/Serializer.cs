using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hexaplicate.PerstState
{
    internal class Serializer
    {
        public DataContractSerializerSettings settings = new DataContractSerializerSettings();
        static Type[] knownTypes = new Type[] { typeof(EssenceHexagon), typeof(FractalHexagon), typeof(RecipeHexagon), typeof(EmptyHexagon) };
        public DataContractSerializer invSerializer;
        public DataContractSerializer gridSerializer;
        public XmlWriter xWrite;
        public string savePath;
        public Serializer(string file)
        {
            settings.PreserveObjectReferences = true;
            settings.KnownTypes = knownTypes; ;
            invSerializer = new DataContractSerializer(typeof(Inventory), settings);
            gridSerializer = new DataContractSerializer(typeof(Grid), settings);
            savePath = file;
        }

        public void SaveGrid(Grid grid)
        {
            string filePath = savePath + "hex.xml";
            using (xWrite = XmlWriter.Create(@filePath))
            {
                gridSerializer.WriteObject(xWrite, grid);
            }
        }

        public void SaveInventory(Inventory inv)
        {
            string filePath = savePath + "inv.xml";
            using (xWrite = XmlWriter.Create(@filePath))
            {
                invSerializer.WriteObject(xWrite, inv);
            }
        }

        public Grid LoadGrid()
        {
            string filePath = savePath + "hex.xml";
            using(XmlReader reader = XmlReader.Create(filePath))
            {
                return (Grid)gridSerializer.ReadObject(reader);
            }
        }

        public Inventory LoadInventory()
        {
            string filePath = savePath + "inv.xml";
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                return (Inventory)invSerializer.ReadObject(reader);
            }
        }

    }
}
