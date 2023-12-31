﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using VokabelTrainer.Model;
using VokabelTrainer.Model.Api;
using VokabelTrainer.Services;

namespace VokabelTrainer.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        private string _username;
        private string _dbPath;
        private bool _isDarkTheme;
        private AppSettings _settings;

        public string Username 
        { 
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string DBPath
        {
            get => _dbPath;
            set => SetProperty(ref _dbPath, value);
        }

        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set => SetProperty(ref _isDarkTheme, value);
        }

        public SettingsViewModel() 
        {
            Load();
        }

        public override string Title => "Settings";


        private RelayCommand _SaveCommand = null;
        public ICommand SaveCommand
        {
            get
            {
                if (_SaveCommand == null)
                {
                    _SaveCommand = new RelayCommand(Save);
                }
                return _SaveCommand;
            }
        }


        public void Load()
        {
            _settings = CommonServices.Instance.Settings.Load();
            this.Username = _settings.Username;
            this.DBPath = _settings.DBPath;
            this.IsDarkTheme = _settings.IsDarkTheme;
        }

        public void Save()
        {
            if (_settings != null)
            {
                this._settings.Username = this.Username;
                this._settings.DBPath = this.DBPath;
                this._settings.IsDarkTheme = this.IsDarkTheme;
                CommonServices.Instance.Settings.Save(this._settings);

                App.Current.UserAppTheme = this.IsDarkTheme ? AppTheme.Dark : AppTheme.Light;

                CommonServices.Instance.Navigation.Back();
            }
        }
    }
}
