using CunCorrector.Commands;
using CunCorrector.Controllers;
using CunCorrector.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CunCorrector.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private string PathToConfig = "C:\\Users\\user\\Desktop\\hwlayer.conf";
        private ConcentratorController _controller;
        private RelayCommand _saveCommand;

        public MainViewModel()
        {
            _controller = new ConcentratorController();
            LoadConfigFile(PathToConfig);
        }

        public ObservableCollection<IPConcentrator> IPConcentrators { get; set; }

        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(obj =>
        {
            try
            {
                var a = IPConcentrators;
                MessageBox.Show("Резервная копия успешно создана.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }));


        private void LoadConfigFile(string path, bool reload = false)
        {
            if (reload && IPConcentrators != null)
            {
                IPConcentrators.Clear();
            }

            IPConcentrators = _controller.GetIPConcentrators(path);
        }
    }
}
