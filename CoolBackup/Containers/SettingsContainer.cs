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

        #endregion Property DefaultBackupDirectory

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

        #endregion Property DefaultNameFormat

        #region Property SevenZipLocation

        private String _sevenZipLocation;

        public String SevenZipLocation
        {
            get
            {
                return _sevenZipLocation;
            }
            set
            {
                _sevenZipLocation = value;
                RaisePropertyChanged("SevenZipLocation");
            }
        }

        #endregion Property SevenZipLocation
    }
}