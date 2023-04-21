using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListLib.Models
{
    public class JobModel : BaseNotifyPropertyChanged
    {
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    RaisePropertyChanged(nameof(Id));
                }
            }
        }

        private int _stt;
        public int STT
        {
            get => _stt;
            set
            {
                if (_stt != value)
                {
                    _stt = value;
                    RaisePropertyChanged(nameof(STT));
                }
            }
        }

        private bool _isDoneJob;
        public bool IsDoneJob
        {
            get => _isDoneJob;
            set
            {
                if(value != _isDoneJob)
                {
                    _isDoneJob = value;
                    RaisePropertyChanged(nameof(IsDoneJob));
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if(_name != value)
                {
                    _name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }
        
        public JobModel()
        {
            Id = ToDoListLib.Helper.CommonHelper.GetIDString();
            STT = 0;
            IsDoneJob = false;
            Name = string.Empty;
        }
    }
}
