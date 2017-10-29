using GalaSoft.MvvmLight;
using System;

namespace CoolBackup.Containers
{
    public class SettingsContainer : ViewModelBase
    {
        #region Property DefaultBackupDirectory
        private String _defaultBackupDirectory;
        public String DefaultBackupDirectory
        {
            get
            {
                return _defaultBackupDirectory;
            }
            set
            {
                _defaultBackupDirectory = value;
                RaisePropertyChanged("DefaultBackupDirectory");
            }
        }
        #endregion

        #region Property DefaultNameFormat
        private String _defaultNameFormat;
        public String DefaultNameFormat
        {
            get
            {
                return _defaultNameFormat;
            }
            set
            {
                _defaultNameFormat = value;
                RaisePropertyChanged("DefaultNameFormat");
            }
        }
        #endregion

        #region Property ZipCommand
        private String _zipCommand;
        public String ZipCommand
        {
            get
            {
                return _zipCommand;
            }
            set
            {
                _zipCommand = value;
                RaisePropertyChanged("ZipCommand");
            }
        }
        #endregion

        #region Property TemporaryDirectory
        private String _temporaryDirectory;
        public String TemporaryDirectory
        {
            get
            {
                return _temporaryDirectory;
            }
            set
            {
                _temporaryDirectory = value;
                RaisePropertyChanged("TemporaryDirectory");
            }
        }
        #endregion

    }
}
