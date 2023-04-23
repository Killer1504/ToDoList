using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }
    }
}
