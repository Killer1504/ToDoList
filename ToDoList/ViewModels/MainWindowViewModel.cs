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
        public VfxCommand GoToHomePageCommand { get; set; }
        #endregion
        public MainWindowViewModel()
        {
            Init_Command();
            Init_Model();
        }
        public void Init_Command()
        {
            LoadedCommand = new VfxCommand(OnLoaded, () => true);
            GoToHomePageCommand = new VfxCommand(OnGoToHomePage, () => true);
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
