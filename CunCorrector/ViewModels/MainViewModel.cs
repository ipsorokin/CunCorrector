using CunCorrector.Commands;
using CunCorrector.Controllers;
using CunCorrector.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace CunCorrector.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private ConcentratorController _controller;
        private RelayCommand _saveCommand;
        private ObservableCollection<IPConcentrator> _ipConcentrators;
        private string _selectedConcentratorClass;
        private bool _setVoiceFilter;

        public MainViewModel()
        {
            _controller = new ConcentratorController();
            LoadConfigFile();
        }

        public ObservableCollection<IPConcentrator> IPConcentrators { get => _ipConcentrators; set { _ipConcentrators = value; OnPropertyChanged(); } }
        public string SelectedConcentratorClass { get => _selectedConcentratorClass; set { _selectedConcentratorClass = value; OnPropertyChanged(); } }
        public bool SetVoiceFilter { get => _setVoiceFilter; set { _setVoiceFilter = value; OnPropertyChanged(); } }
        public Dictionary<string, string> ConcentratorClasses { get; } = AppVariable.ConcentratorClasses;

        public RelayCommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand((obj) =>
        {
            try
            {
                _controller.SaveChanges(IPConcentrators, SelectedConcentratorClass, SetVoiceFilter);
                MessageBox.Show("Изменения сохранены. Резервная копия создана.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadConfigFile(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }));


        public void LoadConfigFile(bool reload = false)
        {
            if (reload && IPConcentrators != null)
            {
                IPConcentrators.Clear();
            }

            IPConcentrators = _controller.GetIPConcentrators();
        }
    }
}
