using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace CunCorrector.Models
{
    internal class Concentrator : Hardware
    {
        private bool isSelected;

        public Concentrator() : base() { }
        public Concentrator(string id, string name, string classId) : base(id, name, classId) { }
        public Concentrator(string id, string name, string classId, bool setVoiceFilter) : this(id, name, classId)
        {
            SetVoiceFilter = setVoiceFilter;
        }

        public bool SetVoiceFilter { get; set; }
        public bool IsSelected { get => isSelected; set { isSelected = value; OnPropertyChanged(); } }

        public static Concentrator GenerateFromXml(XElement element)
        {
            if (element == null) throw new ArgumentNullException();

            string id = element.Attribute("ID").Value;
            string name = element.Attribute("Name").Value;
            string @class = element.Element("Class").Value;

            if (!AppVariable.ConcentratorClass.ContainsKey(@class))
            {
                return null;
            }

            return new Concentrator(id, name, @class);
        }
    }
}
