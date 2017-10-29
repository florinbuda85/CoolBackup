using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Input;

namespace CoolBackup.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        private SettingsWindow settingsWindow;


        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
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
        #endregion



    }
}