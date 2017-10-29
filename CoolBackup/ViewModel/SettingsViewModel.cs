using CoolBackup.Containers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MahApps.Metro;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CoolBackup.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        #region Property MySettingsContainer
        private SettingsContainer _mySettingsContainer;
        public SettingsContainer MySettingsContainer
        {
            get
            {
                if (_mySettingsContainer == null)
                {
                    _mySettingsContainer = SettingsSingleton.getInstance().getSettings();
                }

                return _mySettingsContainer;
            }

            set
            {
                _mySettingsContainer = value;
                RaisePropertyChanged("MySettingsContainer");
            }
        }
        #endregion

        #region ICommand DoChooseBackupDirectory
        private ICommand _doChooseBackupDirectory;
        public ICommand DoChooseBackupDirectory
        {
            get
            {
                if (_doChooseBackupDirectory == null)
                {
                    _doChooseBackupDirectory = new RelayCommand(DoChooseBackupDirectoryExecute); // RelayCommand
                }
                return _doChooseBackupDirectory;
            }
        }
        private void DoChooseBackupDirectoryExecute()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {
                using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
                {
                    System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        this.MySettingsContainer.DefaultBackupDirectory = fbd.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found on DoChooseBackupDirectoryExecute :" + ex.Message);
            }
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
        #endregion

        #region ICommand DoChooseTemporaryDirectory
        private ICommand _doChooseTemporaryDirectory;
        public ICommand DoChooseTemporaryDirectory
        {
            get
            {
                if (_doChooseTemporaryDirectory == null)
                {
                    _doChooseTemporaryDirectory = new RelayCommand(DoChooseTemporaryDirectoryExecute); // RelayCommand
                }
                return _doChooseTemporaryDirectory;
            }
        }
        private void DoChooseTemporaryDirectoryExecute()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {
                using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
                {
                    System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        this.MySettingsContainer.TemporaryDirectory = fbd.SelectedPath;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found on DoChooseTemporaryDirectoryExecute :" + ex.Message);
            }
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }
        #endregion

        #region ICommand DoSaveSettings
        private ICommand _doSaveSettings;
        public ICommand DoSaveSettings
        {
            get
            {
                if (_doSaveSettings == null)
                {
                    _doSaveSettings = new RelayCommand(DoSaveSettingsExecute); // RelayCommand
                }
                return _doSaveSettings;
            }
        }
        private void DoSaveSettingsExecute()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {
                changeTheme("red");
                SettingsSingleton.getInstance().saveContainer();
                changeTheme("green");
                //System.Threading.Thread.Sleep(600);
                changeTheme("blue");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found on DoSaveSettingsExecute :" + ex.Message);
            }
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }


        //TODO: move this to UI helper
        private void changeTheme(string newTheme)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            var accent = ThemeManager.GetAccent(newTheme);
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme.Item1);
        }

        #endregion

    }
}
