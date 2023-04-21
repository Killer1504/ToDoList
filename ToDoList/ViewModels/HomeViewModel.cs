using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListLib.Commands;
using ToDoListLib.Interfaces;
using ToDoListLib.Models;

namespace ToDoList.ViewModels
{
    public class HomeViewModel : BaseNotifyPropertyChanged, IViewModel
    {
        #region properties
        public ObservableCollection<JobModel> TodoJobs { get; set; }
        #endregion

        #region commands
        public VfxCommand LoadedCommand { get; set; }
        public VfxCommand AddJobCommand { get; set; }
        #endregion

        public HomeViewModel()
        {
            Init_Model();
            Init_Command();
        }
        public void Init_Command()
        {
            LoadedCommand = new VfxCommand(OnLoaded, () => true);
            AddJobCommand = new VfxCommand(OnAddJob, () => true);

        }

        private void OnLoaded(object obj)
        {
            if (obj is Views.HomeUC uc)
            {
                if (TodoJobs.Count == 0)
                {
                    TodoJobs.Add(new JobModel()
                    {
                        Name = "Học Tiếng Anh",
                    });

                    TodoJobs.Add(new JobModel()
                    {
                        Name = "Tập thể dục",
                    });

                    TodoJobs.Add(new JobModel()
                    {
                        Name = "Học c# (MVC)",
                    });
                }
            }
        }

        public void Init_Model()
        {
            TodoJobs = new ObservableCollection<JobModel>();
            TodoJobs.CollectionChanged += TodoJobs_CollectionChanged;
        }

        private void TodoJobs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (TodoJobs != null && TodoJobs.Count > 0)
            {
                int _stt = 0;
                foreach (var item in TodoJobs)
                {
                    item.STT = ++_stt;
                }
            }

        }

        private void OnAddJob(object obj)
        {

        }
    }
}
