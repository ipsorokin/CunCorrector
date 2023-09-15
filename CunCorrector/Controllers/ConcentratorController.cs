using CunCorrector.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CunCorrector.Controllers
{
    internal class ConcentratorController
    {

        //private string HWLAYERCONFPATH = "C:\\Users\\user\\Desktop\\hwlayer.conf";
        //private RelayCommand _saveCommand;





        //private string HWLAYERPATH = "C:\\Users\\user\\Desktop\\hwlayer.conf";

        //private RelayCommand _rollback;
        //private RelayCommand _applyChanges;

        //public MainViewModel()
        //{
        //    LoadConfigurationFile(HWLAYERPATH);
        //}


        //public Dictionary<string, string> Concentrators { get; } = new Dictionary<string, string>()
        //{
        //    { "51E9FC49-F2F0-46D2-A243-C5C9C3F83956", "КУН-2Д" },
        //    { "CBAD15FE-E91A-4127-B6EA-9A3D18AC2CE9", "КУН-2Д1" }
        //};


        //public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand((obj) =>
        //{

        //}));

        ///// <summary>
        ///// 
        ///// </summary>
        //public ObservableCollection<HardwareNode> HardwareNodes { get; } = new ObservableCollection<HardwareNode>();
        ///// <summary>
        ///// 
        ///// </summary>
        //public Hardware SelectedHardware { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public bool IsSetFilter { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public RelayCommand Rollback => _rollback ?? (_rollback = new RelayCommand((obj) =>
        //{
        //    SaveChanges(HWLAYERPATH);
        //}));
        ///// <summary>
        ///// 
        ///// </summary>
        //public RelayCommand ApplyChanges => _applyChanges ?? (_applyChanges = new RelayCommand((obj) =>
        //{
        //    MessageBoxResult result = MessageBox.Show(
        //        "ПЕРЕД ПРИМЕНЕНИЕМ ИЗМЕНЕНИЙ РЕКОМЕНДУЕТСЯ СДЕЛАТЬ РЕЗЕРВНУЮ КОПИЮ НАСТРОЕК!\n\nПРОДОЛЖИТЬ?",
        //        "ВНИМАНИЕ",
        //        MessageBoxButton.YesNo,
        //        MessageBoxImage.Warning,
        //        MessageBoxResult.No
        //        );

        //    if (result == MessageBoxResult.No) return;

        //    SaveChanges(HWLAYERPATH);
        //}));

        public ObservableCollection<IPConcentrator> GetIPConcentrators(string path)
        {
            XDocument document = XDocument.Load(path);
            XElement root = document.Element("Configuration");

            ObservableCollection<IPConcentrator> ipConcentrators = new ObservableCollection<IPConcentrator>();

            foreach (XElement node in root.Elements("Item"))
            {
                ipConcentrators.Add(CreateIPConcentratorItem(node));
            }

            return ipConcentrators;
        }

        public IPConcentrator CreateIPConcentratorItem(XElement node)
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

        //private void SaveChanges(string path)
        //{
        //    List<string> selectedConcentratorIds = new List<string>();

        //    foreach (HardwareNode node in HardwareNodes)
        //    {
        //        foreach (HardwareNode hardwareNode in node.Children)
        //        {
        //            if (hardwareNode.IsSelected)
        //            {
        //                selectedConcentratorIds.Add(hardwareNode.Hardware.Id);
        //            }
        //        }
        //    }

        //    XDocument document = XDocument.Load(path);
        //    XElement root = document.Element("Configuration");

        //    if (root != null)
        //    {
        //        foreach (XElement node in root.Elements("Item"))
        //        {
        //            foreach (XElement concentrator in node.Elements("Item"))
        //            {
        //                if (selectedConcentratorIds.Contains(concentrator.Attribute("ID").Value))
        //                    ChangeConcentratorType(concentrator);
        //            }
        //        }
        //    }

        //    document.Save(path);

        //    LoadConfigurationFile(HWLAYERPATH);
        //}

        //private void ChangeConcentratorType(XElement concentrator)
        //{
        //    if (!Concentrators.ContainsKey(concentrator.Element("Class").Value))
        //        return;

        //    concentrator.Element("Class").Value = SelectedHardware.Class;

        //    foreach (XElement channel in concentrator.Elements("Item"))
        //        SetVoiceFilter(channel);
        //}

        //private void SetVoiceFilter(XElement voiceChannel)
        //{
        //    if (voiceChannel.Element("Class").Value != "6F7AAE80-6C7E-41A6-A2F2-CF2F78FB9C10")
        //        return;

        //    if (IsSetFilter)
        //    {
        //        if (voiceChannel.Element("Filter") != null)
        //            voiceChannel.Element("Filter").Remove();
        //    }
        //    else
        //    {
        //        if (voiceChannel.Element("Filter") != null)
        //            voiceChannel.Element("Filter").Value = IsSetFilter.ToString();
        //        else
        //            voiceChannel.Add(new XElement("Filter", "False"));
        //    }
        //}
    }
}
