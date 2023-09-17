using System;
using System.Xml.Linq;

namespace CunCorrector.Models
{
    internal class Concentrator : Hardware
    {
        private bool isSelected;

        public Concentrator() : base() { }
        public Concentrator(string id, string name, string classId) : base(id, name, classId) { }

        public bool IsSelected { get => isSelected; set { isSelected = value; OnPropertyChanged(); } }

        public static Concentrator GenerateFromXml(XElement element)
        {
            if (element == null) throw new ArgumentNullException();

            string id = element.Attribute("ID").Value;
            string name = element.Attribute("Name").Value;
            string @class = element.Element("Class").Value;

            if (!AppVariable.ConcentratorClasses.ContainsKey(@class))
            {
                return null;
            }

            return new Concentrator(id, name, @class);
        }
    }
}
