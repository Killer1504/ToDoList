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
        public ObservableCollection<JobModel> TodoJobs_Daily { get; set; }
        public ObservableCollection<JobModel> TodoJobs_Weekly { get; set; }
        public ObservableCollection<JobModel> TodoJobs_Monthly { get; set; }
        public ObservableCollection<JobModel> TodoJobs_Yearly { get; set; }
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
        public VfxCommand DeleteJobCommand { get; set; }
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
            DeleteJobCommand = new VfxCommand(OnDeleteJobCommand, () => true);

        }

        private void OnDeleteJobCommand(object obj)
        {
            MessageBox.Show(obj.ToString());
        }

        public void OnSave(object obj)
        {

            var ret = OnSaveData();
            if (ret.Item1)
            {
                _ = MessageBox.Show("OK", "Lưu", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            else
            {
                _ = MessageBox.Show($"Lưu lỗi\r\n{ret.Item2}", "Lưu", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }


        public (bool, string) OnSaveData()
        {
            try
            {
                var path = Utils.GlobalVariable.Instance.DATA_PATH;
                var jsonStr_Daily = JsonConvert.SerializeObject(TodoJobs_Daily);
                var jsonStr_Weekly = JsonConvert.SerializeObject(TodoJobs_Weekly);
                var jsonStr_Monthly = JsonConvert.SerializeObject(TodoJobs_Monthly);
                var jsonStr_Yearly = JsonConvert.SerializeObject(TodoJobs_Yearly);

                var key = Utils.GlobalVariable.Instance.KEY_ENCTYPED;

                var jsonEncrypted_Daily = ToDoListLib.Helper.EncryptHelper.EncryptString(jsonStr_Daily, key);
                var jsonEncrypted_Weekly = ToDoListLib.Helper.EncryptHelper.EncryptString(jsonStr_Weekly, key);
                var jsonEncrypted_Monthly = ToDoListLib.Helper.EncryptHelper.EncryptString(jsonStr_Monthly, key);
                var jsonEncrypted_Yearly = ToDoListLib.Helper.EncryptHelper.EncryptString(jsonStr_Yearly, key);
                using (var sw = new StreamWriter(path))
                {
                    var dictionary = new Dictionary<string, string>
                {
                    { "daily", jsonEncrypted_Daily },
                    { "weekly", jsonEncrypted_Weekly },
                    { "monthly", jsonEncrypted_Monthly },
                    { "yearly", jsonEncrypted_Yearly },
                };

                    var jsonStr = JsonConvert.SerializeObject(dictionary);
                    sw.WriteLine(jsonStr);
                    sw.Flush();
                }
                return (true, "");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);

            }

        }

        private void OnReadData()
        {
            try
            {
                var path = Utils.GlobalVariable.Instance.DATA_PATH;
                if (File.Exists(path))
                {
                    using (var sr = new StreamReader(path))
                    {
                        var jsonStr = sr.ReadToEnd();
                        var keyEnctypt = Utils.GlobalVariable.Instance.KEY_ENCTYPED;
                        var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonStr);

                        foreach (var _key in dictionary.Keys)
                        {
                            if (_key.ToLower() == "daily")
                            {
                                var valueEncrypted = dictionary[_key];
                                var valuePlain = ToDoListLib.Helper.EncryptHelper.DecryptString(valueEncrypted, keyEnctypt);
                                var models = JsonConvert.DeserializeObject<ObservableCollection<JobModel>>(valuePlain);
                                if (models != null && models.Count > 0)
                                {
                                    foreach (var item in models)
                                    {
                                        TodoJobs_Daily.Add(item);
                                    }
                                }
                            }
                            else if (_key.ToLower() == "weekly")
                            {
                                var valueEncrypted = dictionary[_key];
                                var valuePlain = ToDoListLib.Helper.EncryptHelper.DecryptString(valueEncrypted, keyEnctypt);
                                var models = JsonConvert.DeserializeObject<ObservableCollection<JobModel>>(valuePlain);
                                if (models != null && models.Count > 0)
                                {
                                    foreach (var item in models)
                                    {
                                        TodoJobs_Weekly.Add(item);
                                    }
                                }
                            }
                            else if (_key.ToLower() == "monthly")
                            {
                                var valueEncrypted = dictionary[_key];
                                var valuePlain = ToDoListLib.Helper.EncryptHelper.DecryptString(valueEncrypted, keyEnctypt);
                                var models = JsonConvert.DeserializeObject<ObservableCollection<JobModel>>(valuePlain);
                                if (models != null && models.Count > 0)
                                {
                                    foreach (var item in models)
                                    {
                                        TodoJobs_Monthly.Add(item);
                                    }
                                }
                            }
                            else if (_key.ToLower() == "yearly")
                            {
                                var valueEncrypted = dictionary[_key];
                                var valuePlain = ToDoListLib.Helper.EncryptHelper.DecryptString(valueEncrypted, keyEnctypt);
                                var models = JsonConvert.DeserializeObject<ObservableCollection<JobModel>>(valuePlain);
                                if (models != null && models.Count > 0)
                                {
                                    foreach (var item in models)
                                    {
                                        TodoJobs_Yearly.Add(item);
                                    }
                                }
                            }
                        }


                    }
                }

            }
            catch (Exception)
            {

            }

            return;


        }

        private void OnRfresh(object obj)
        {

            if (obj is string str)
            {
                switch (str.ToLower())
                {
                    case "daily":
                        TodoJobs_Daily.ToList().ForEach(s => s.IsDoneJob = false);
                        break;
                    case "weekly":
                        TodoJobs_Weekly.ToList().ForEach(s => s.IsDoneJob = false);
                        break;
                    case "monthly":
                        TodoJobs_Monthly.ToList().ForEach(s => s.IsDoneJob = false);
                        break;
                    case "yearly":
                        TodoJobs_Yearly.ToList().ForEach(s => s.IsDoneJob = false);
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnLoaded(object obj)
        {
            if (obj is Views.HomeUC uc)
            {
                OnReadData();

                if (TodoJobs_Daily.Count == 0)
                {
                    TodoJobs_Daily.Add(new JobModel()
                    {
                        Name = "Học Tiếng Anh",
                    });

                    TodoJobs_Daily.Add(new JobModel()
                    {
                        Name = "Tập thể dục",
                    });

                }
            }
        }

        public void Init_Model()
        {
            TodoJobs_Daily = new ObservableCollection<JobModel>();
            TodoJobs_Weekly = new ObservableCollection<JobModel>();
            TodoJobs_Monthly = new ObservableCollection<JobModel>();
            TodoJobs_Yearly = new ObservableCollection<JobModel>();
            ToDay = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture.DateTimeFormat);

            TodoJobs_Daily.CollectionChanged += TodoJobs_CollectionChanged;
            TodoJobs_Weekly.CollectionChanged += TodoJobs_CollectionChanged;
            TodoJobs_Monthly.CollectionChanged += TodoJobs_CollectionChanged;
            TodoJobs_Yearly.CollectionChanged += TodoJobs_CollectionChanged;
        }

        private void TodoJobs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (sender != null)
            {
                if (sender is ObservableCollection<JobModel> _jobs)
                {
                    int _stt = 0;
                    foreach (var job in _jobs)
                    {
                        job.STT = ++_stt;
                    }
                }
                OnSaveData();
            }

        }

        private void OnAddJob(object obj)
        {
            if (obj is string str)
            {
                var wd = new Views.JobNameWD();
                wd.txtJobHint.Text += " " + str;
                wd.ShowDialog();
                var tag = wd.Tag?.ToString();
                if (string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag))
                {

                }
                else
                {

                    switch (str.ToLower())
                    {
                        case "daily":
                            TodoJobs_Daily.Add(new JobModel()
                            {
                                Name = tag,
                            });
                            break;
                        case "weekly":
                            TodoJobs_Weekly.Add(new JobModel()
                            {
                                Name = tag,
                            });
                            break;
                        case "monthly":
                            TodoJobs_Monthly.Add(new JobModel()
                            {
                                Name = tag,
                            });
                            break;
                        case "yearly":
                            TodoJobs_Yearly.Add(new JobModel()
                            {
                                Name = tag,
                            });
                            break;
                        default:
                            break;
                    }


                }
            }
        }
    }
}
