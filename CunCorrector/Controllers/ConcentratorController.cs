using CunCorrector.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CunCorrector.Controllers
{
    internal class ConcentratorController
    {
        private string PATH_TO_SETTINGS = "C:\\1Tekon\\ASUD Scada\\OPC Server\\settings";
        private string FILE_NAME = "hwlayer.conf";
        private string VOICE_CHANNEL_CLASS = "6F7AAE80-6C7E-41A6-A2F2-CF2F78FB9C10";

        public ConcentratorController() { }

        public ObservableCollection<IPConcentrator> GetIPConcentrators()
        {
            string path = Path.Combine(PATH_TO_SETTINGS, FILE_NAME);
            XDocument document = XDocument.Load(path);
            XElement root = document.Element("Configuration");

            ObservableCollection<IPConcentrator> ipConcentrators = new ObservableCollection<IPConcentrator>();

            foreach (XElement node in root.Elements("Item"))
            {
                ipConcentrators.Add(CreateIPConcentrator(node));
            }

            return ipConcentrators;
        }

        public IPConcentrator CreateIPConcentrator(XElement node)
        {
            IPConcentrator ipConcentrator = IPConcentrator.GenerateFromXml(node);

            foreach (XElement concentratorXml in node.Elements("Item"))
            {
                Concentrator concentrator = Concentrator.GenerateFromXml(concentratorXml);

                if (concentrator != null)
                {
                    ipConcentrator.AddConcentrator(concentrator);
                }
            }

            return ipConcentrator;
        }


        public void SaveChanges(IEnumerable<IPConcentrator> ipConcentrators, string selectedConcentratorClass, bool setVoiceFilter = false)
        {
            var b = GetChangedConcentratorIds(ipConcentrators);

            var path = Path.Combine(PATH_TO_SETTINGS, FILE_NAME);

            XDocument document = XDocument.Load(path);
            XElement root = document.Element("Configuration");

            foreach (XElement node in root.Elements("Item"))
            {
                foreach (XElement concentrator in node.Elements("Item"))
                {
                    if (b.Contains(concentrator.Attribute("ID").Value))
                        ChangeConcentratorType(concentrator, selectedConcentratorClass, setVoiceFilter);
                }
            }

            File.Copy(path, Path.Combine(PATH_TO_SETTINGS, FILE_NAME.Insert(FILE_NAME.Length - 5, $"_{Guid.NewGuid()}")));
            document.Save(path);
        }


        private IEnumerable<string> GetChangedConcentratorIds(IEnumerable<IPConcentrator> ipConcentrators)
        {
            List<string> selectedConcentratorIds = new List<string>();

            foreach (IPConcentrator node in ipConcentrators)
            {
                if (node.Concentrators != null)
                    foreach (var hardwareNode in node.Concentrators)
                    {
                        if (hardwareNode.IsSelected)
                        {
                            selectedConcentratorIds.Add(hardwareNode.Id);
                        }
                    }
            }

            return selectedConcentratorIds;
        }

        private void ChangeConcentratorType(XElement concentrator, string selectedConcentratorClass, bool setVoiceFilter)
        {
            if (!AppVariable.ConcentratorClasses.ContainsKey(concentrator.Element("Class").Value))
                return;

            concentrator.Element("Class").Value = selectedConcentratorClass;

            foreach (XElement channel in concentrator.Elements("Item"))
                SetVoiceFilter(channel, setVoiceFilter);
        }

        private void SetVoiceFilter(XElement voiceChannel, bool setVoiceFilter)
        {
            if (voiceChannel.Element("Class").Value != VOICE_CHANNEL_CLASS)
                return;

            if (setVoiceFilter)
            {
                voiceChannel.Element("Filter")?.Remove();
            }
            else
            {
                if (voiceChannel.Element("Filter") != null)
                    voiceChannel.Element("Filter").Value = setVoiceFilter.ToString();
                else
                    voiceChannel.Add(new XElement("Filter", false));
            }
        }
    }
}
