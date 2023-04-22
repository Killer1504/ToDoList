using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListLib.Commands;
using ToDoListLib.Interfaces;
using ToDoListLib.Models;

namespace ToDoList.ViewModels
{
    public class JobNameWDViewModel : BaseNotifyPropertyChanged, IViewModel
    {
        #region properties
        private string _jobName;
        public string JobName
        {
            get => _jobName;
            set
            {
                if (_jobName != value)
                {
                    _jobName = value;
                    RaisePropertyChanged(nameof(JobName));
                }
            }
        }

        #endregion

        #region commands
        public VfxCommand LoadedCommand { get; set; }
        public VfxCommand OKCommand { get; set; }
        public VfxCommand CancelCommand { get; set; }
        #endregion


        public JobNameWDViewModel()
        {
            Init_Model();
            Init_Command();
        }
        public void Init_Command()
        {
            LoadedCommand = new VfxCommand(OnLoaded, () => true);
            OKCommand = new VfxCommand(OnOkCommand, () => true);
            CancelCommand = new VfxCommand(OnCancel, () => true);
        }

        private void OnCancel(object obj)
        {
            if (obj is Views.JobNameWD wd)
            {
                JobName = string.Empty;
                wd.Tag = JobName;
                wd.Close();
            }
        }

        private void OnOkCommand(object obj)
        {
            if (obj is Views.JobNameWD wd)
            {
                wd.Tag = JobName;
                wd.Close();
            }
        }

        private void OnLoaded(object obj)
        {
            if (obj is Views.JobNameWD wd)
            {
                wd.txtJobName.Focus();
            }
        }

        public void Init_Model()
        {

        }
    }
}
