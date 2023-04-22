using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListLib.Commands;
using ToDoListLib.Interfaces;
using ToDoListLib.Models;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace ToDoList.ViewModels
{
    public class HomeViewModel : BaseNotifyPropertyChanged, IViewModel
    {
        #region properties
        public ObservableCollection<JobModel> TodoJobs { get; set; }
        private string _today;
        public string ToDay
        {
            get => _today;
            set
            {
                if (_today != value)
                {
                    _today = value;
                    RaisePropertyChanged(nameof(ToDay));
                }
            }
        }
        #endregion

        #region commands
        public VfxCommand LoadedCommand { get; set; }
        public VfxCommand AddJobCommand { get; set; }
        public VfxCommand SaveJobCommand { get; set; }
        public VfxCommand RefreshJobCommand { get; set; }
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
            SaveJobCommand = new VfxCommand(OnSave, () => true);
            RefreshJobCommand = new VfxCommand(OnRfresh, () => true);

        }

        private void OnSave(object obj)
        {
            try
            {
                OnSaveData();
                _ = MessageBox.Show("OK", "Lưu", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"Lưu lỗi\r\n{ex.Message}", "Lưu", MessageBoxButton.OK, MessageBoxImage.Information);

            }


        }

        private void OnSaveData()
        {
            var path = Utils.GlobalVariable.Instance.DATA_PATH;
            var jsonStr = JsonConvert.SerializeObject(TodoJobs);

            var key = Utils.GlobalVariable.Instance.KEY_ENCTYPED;

            var jsonEncrypted = ToDoListLib.Helper.EncryptHelper.EncryptString(jsonStr, key);
            using (var sw = new StreamWriter(path))
            {
                sw.Write(jsonEncrypted);
                sw.Flush();
            }
        }

        private ObservableCollection<JobModel> OnReadData()
        {
            try
            {
                var path = Utils.GlobalVariable.Instance.DATA_PATH;
                if (File.Exists(path))
                {
                    using (var sr = new StreamReader(path))
                    {
                        var jsonEnctyped = sr.ReadToEnd();
                        var key = Utils.GlobalVariable.Instance.KEY_ENCTYPED;
                        var jsonStr = ToDoListLib.Helper.EncryptHelper.DecryptString(jsonEnctyped, key);

                        var models = JsonConvert.DeserializeObject<ObservableCollection<JobModel>>(jsonStr);
                        return models;
                    }
                }

            }
            catch (Exception)
            {

            }

            return null;


        }

        private void OnRfresh(object obj)
        {
            TodoJobs.ToList().ForEach(s=>s.IsDoneJob = false);
        }

        private void OnLoaded(object obj)
        {
            if (obj is Views.HomeUC uc)
            {
                var models = OnReadData();
                if (models != null && models.Count > 0)
                {
                    foreach (var item in models)
                    {
                        TodoJobs.Add(item);
                    }
                }

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
            ToDay = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture.DateTimeFormat);
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
            var wd = new Views.JobNameWD();
            wd.ShowDialog();
            var tag = wd.Tag?.ToString();
            if (string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag))
            {

            }
            else
            {
                TodoJobs.Add(new JobModel()
                {
                    Name = tag,
                });
            }
        }
    }
}
