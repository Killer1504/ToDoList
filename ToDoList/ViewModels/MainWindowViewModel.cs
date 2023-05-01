using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using ToDoListLib.Commands;
using ToDoListLib.Interfaces;
using ToDoListLib.Models;

namespace ToDoList.ViewModels
{
    public class MainWindowViewModel : BaseNotifyPropertyChanged, IViewModel
    {
        #region properties

        #endregion

        #region commands
        public VfxCommand LoadedCommand { get; set; }
        public VfxCommand SaveCommand { get; set; }
        public VfxCommand CloseCommand { get; set; }
        public VfxCommand UpdateCommand { get; set; }
        #endregion
        public MainWindowViewModel()
        {
            Init_Command();
            Init_Model();
        }
        public void Init_Command()
        {
            LoadedCommand = new VfxCommand(OnLoaded, () => true);
            SaveCommand = new VfxCommand(OnSave, () => true);
            CloseCommand = new VfxCommand(OnClose, () => true);
            UpdateCommand = new VfxCommand(OnUpdate, () => true);
        }

        private void OnUpdate(object obj)
        {
            var url = Utils.GlobalVariable.Instance.URL_UPDATE_SOFTWARE;
            AutoUpdater.Start(url);
        }

        private void OnClose(object obj)
        {
            if (obj is MainWindow wd)
            {
                Environment.Exit(0);
            }
        }

        private void OnSave(object obj)
        {
            if (obj is MainWindow wd)
            {
                var homeViewModel = wd.homeUC.DataContext as HomeViewModel;
                homeViewModel.OnSave(wd.homeUC);
            }
        }

        private void OnGoToHomePage(object obj)
        {
            if (obj is MainWindow wd)
            {

            }
        }

        private void OnLoaded(object obj)
        {
            if (obj is MainWindow wd)
            {

            }
        }

        public void Init_Model()
        {
            AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent;
        }

        private void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            string title = "Cập nhật";
            if (args.IsUpdateAvailable)
            {
                string newestVersion = args.CurrentVersion;
                string msg = "Cập nhật phiên bản";
                string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                MessageBoxResult result = System.Windows.MessageBox.Show($"{msg} từ {version} lên {newestVersion}?", title,
                    MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (AutoUpdater.DownloadUpdate(args))
                        {
                            var current = System.Diagnostics.Process.GetCurrentProcess();
                            var nameProcess = current.ProcessName;
                            var processEs = System.Diagnostics.Process.GetProcessesByName(nameProcess);
                            foreach (var item in processEs)
                            {
                                if (item != current)
                                {
                                    item.Kill();
                                }
                            }
                            Environment.Exit(0);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        string _msg = "Không thể cập nhật phiên bản";
                        _ = System.Windows.MessageBox.Show(_msg + "!",
                            title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    }
                }
            }
            else
            {

                var _msg = $"Bạn đang ở phiên bản mới nhất {args.CurrentVersion}";
                _ = System.Windows.MessageBox.Show(_msg + "!",
                            title, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);


            }
        }
    }
}
