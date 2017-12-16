using dnGREP;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace CoolBackup.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private SettingsWindow settingsWindow;

        public MainViewModel()
        {
            var v = Environment.GetCommandLineArgs();
            BackupTarget = v[1];
        }

        #region ICommand DoOpenSettingsWindow

        private ICommand _doOpenSettingsWindow;

        public ICommand DoOpenSettingsWindow
        {
            get
            {
                if (_doOpenSettingsWindow == null)
                {
                    _doOpenSettingsWindow = new RelayCommand(DoOpenSettingsWindowExecute); // RelayCommand
                }
                return _doOpenSettingsWindow;
            }
        }

        private void DoOpenSettingsWindowExecute()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {
                if (settingsWindow != null)
                {
                    settingsWindow.Close();
                }
                settingsWindow = new SettingsWindow();
                settingsWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found on DoOpenSettingsWindowExecute :" + ex.Message);
            }
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        #endregion ICommand DoOpenSettingsWindow

        #region Property BackupTarget

        private String _backupTarget;

        public String BackupTarget
        {
            get
            {
                return _backupTarget;
            }
            set
            {
                _backupTarget = value;
                RaisePropertyChanged("BackupTarget");
                DestinationZip = null;
                RaisePropertyChanged("DestinationZip");
            }
        }

        #endregion Property BackupTarget

        private string getDestinationZip()
        {
            string DefaultNameFormat = (string)SettingsSingleton.getInstance().getSettings().DefaultNameFormat.Clone();
            string DefaultBackupDirectory = (string)SettingsSingleton.getInstance().getSettings().DefaultBackupDirectory.Clone();

            FileAttributes attr = File.GetAttributes(BackupTarget);

            string zipName = "";

            if (attr.HasFlag(FileAttributes.Directory))
            {
                zipName = BackupTarget.Replace(Path.GetDirectoryName(BackupTarget), "")
                                      .Replace("\\", "");
            }
            else
            {
                zipName = Path.GetFileNameWithoutExtension(BackupTarget);
            }

            // {Name}_YYYY-MM-DD-HH-mm-ss
            var oficialTime = DateTime.Now;
            DefaultNameFormat = DefaultNameFormat.Replace("YYYY", oficialTime.Year.ToString())
                .Replace("MM", oficialTime.Month.ToString())
                .Replace("DD", oficialTime.Day.ToString())
                .Replace("HH", oficialTime.Hour.ToString())
                .Replace("mm", oficialTime.Minute.ToString())
                .Replace("ss", oficialTime.Second.ToString())
                .Replace("{Name}", zipName);

            return DefaultBackupDirectory + DefaultNameFormat + ".7z";
        }

        #region Property DestinationZip

        private String _destinationZip;

        public String DestinationZip
        {
            get
            {
                if (_destinationZip == null)
                {
                    _destinationZip = getDestinationZip();
                }
                return _destinationZip;
            }
            set
            {
                _destinationZip = value;
                RaisePropertyChanged("DestinationZip");
            }
        }

        #endregion Property DestinationZip

        #region ICommand DoBackup

        private ICommand _doBackup;

        public ICommand DoBackup
        {
            get
            {
                if (_doBackup == null)
                {
                    _doBackup = new RelayCommand(DoBackupExecute); // RelayCommand
                }
                return _doBackup;
            }
        }

        private void DoBackupExecute()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {
                string SevenZipLocation = (string)SettingsSingleton.getInstance().getSettings().SevenZipLocation.Clone();

                string ZipCommand = " a -r \"{DestinationZip}\" \"{fileOrFolder}\""
                                        .Replace("{DestinationZip}", DestinationZip)
                                        .Replace("{fileOrFolder}", BackupTarget);

                System.Diagnostics.Process p = System.Diagnostics.Process.Start(SevenZipLocation + "7z.exe", ZipCommand);
                p.WaitForExit();

                if (p.ExitCode == 0)
                {
                    MessageBox.Show("Success!!");
                }
                else
                {
                    MessageBox.Show("A intervenit o exceptie!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found on DoBackupExecute :" + ex.Message);
            }
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        #endregion ICommand DoBackup

        #region ICommand SelectFileOrFolder

        private ICommand _selectFileOrFolder;

        public ICommand SelectFileOrFolder
        {
            get
            {
                if (_selectFileOrFolder == null)
                {
                    _selectFileOrFolder = new RelayCommand(SelectFileOrFolderExecute); // RelayCommand
                }
                return _selectFileOrFolder;
            }
        }

        private void SelectFileOrFolderExecute()
        {
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
            try
            {
                //BackupTarget

                dnGREP.FileFolderDialog dlg = new FileFolderDialog();

                var v = dlg.ShowDialog();

                if (dlg.SelectedPath != null)
                {
                    BackupTarget = dlg.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception found on SelectFileOrFolderExecute :" + ex.Message);
            }
            Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
        }

        #endregion ICommand SelectFileOrFolder
    }
}