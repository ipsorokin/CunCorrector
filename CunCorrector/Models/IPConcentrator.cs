using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace CunCorrector.Models
{
    internal class IPConcentrator : Hardware
    {
        private bool isSelected;

        public IPConcentrator() : base() { }
        public IPConcentrator(string id, string name, string classId) : base(id, name, classId) { }

        public ObservableCollection<Concentrator> Concentrators { get; private set; }
        public bool IsSelected { get => isSelected; set { isSelected = value; SetSelectedConcentrators(isSelected); OnPropertyChanged(); } }

        public void AddConcentrator(Concentrator concentrator)
        {
            if (Concentrators == null)
            {
                Concentrators = new ObservableCollection<Concentrator>();
            }

            Concentrators.Add(concentrator);
        }

        public void Clear()
        {
            Concentrators?.Clear();
        }

        public bool RemoveConcentrator(Concentrator concentrator)
        {
            if (Concentrators != null)
            {
                return Concentrators.Remove(concentrator);
            }

            return false;
        }

        private void SetSelectedConcentrators(bool value)
        {
            if (Concentrators != null)
            {
                foreach (Concentrator concentrator in Concentrators)
                {
                    concentrator.IsSelected = value;
                }
            }
        }

        public static IPConcentrator GenerateFromXml(XElement element)
        {
            if (element == null) throw new ArgumentNullException();

            string id = element.Attribute("ID").Value;
            string name = element.Attribute("Name").Value;
            string @class = element.Element("Class").Value;

            return new IPConcentrator(id, name, @class);
        }
    }
}
