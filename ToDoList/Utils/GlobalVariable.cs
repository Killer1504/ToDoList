using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListLib.Models;

namespace ToDoList.Utils
{
    public class GlobalVariable : BaseNotifyPropertyChanged
    {
        public string TODO_LIST_FOLDER { get; private set; }
        public string COMPUTER_ID { get; private set; }
        public string DATA_PATH { get; private set; }
        public string KEY_ENCTYPED { get; private set; }

        private static GlobalVariable _instance;
        public static GlobalVariable Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GlobalVariable();
                }
                return _instance;
            }
            private set { _instance = value; }
        }

        public static GlobalVariable GetInstance()
        {
            if (_instance == null)
            {
                _instance = new GlobalVariable();
            }
            return Instance;
        }
        private GlobalVariable()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var vfast = Path.Combine(appData, "vfast");
            var todoList = Path.Combine(vfast, "ToDoList");
            if(!Directory.Exists(todoList)) { Directory.CreateDirectory(todoList); }
            TODO_LIST_FOLDER = todoList;

            COMPUTER_ID = ToDoListLib.Helper.CommonHelper.GetComputerID();
            KEY_ENCTYPED = ToDoListLib.Helper.EncryptHelper.GetHashSHA256(COMPUTER_ID);
            DATA_PATH = Path.Combine(TODO_LIST_FOLDER, "data.json");
        }
    }
}
