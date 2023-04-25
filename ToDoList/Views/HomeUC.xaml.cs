using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoList.ViewModels;
using ToDoListLib.Models;

namespace ToDoList.Views
{
    /// <summary>
    /// Interaction logic for HomeUC.xaml
    /// </summary>
    public partial class HomeUC : UserControl
    {
        public HomeUC()
        {
            InitializeComponent();
        }

        private void MenuItem_Edit_Click(object sender, RoutedEventArgs e)
        {
            // find model
            var menuItem = e.Source as MenuItem;
            var jobModel = menuItem?.DataContext as JobModel;
            if (jobModel != null)
            {
                var wd = new Views.JobNameWD()
                {

                };
                wd.txtJobName.Text = jobModel.Name;
                wd.txtJobName.Focus();
                wd.txtJobName.CaretIndex = jobModel.Name.Length;

                wd.ShowDialog();
                var tag = wd.Tag?.ToString();
                if (string.IsNullOrEmpty(tag) || string.IsNullOrWhiteSpace(tag))
                {

                }
                else
                {
                    jobModel.Name = tag;
                }
            }
            e.Handled = true;
        }

        private void MenuItem_Delete_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = e.Source as MenuItem;
            var jobModel = menuItem?.DataContext as JobModel;
            if (jobModel != null)
            {
                var commandParameter = menuItem.CommandParameter?.ToString();
                var homeViewModel = _this.DataContext as HomeViewModel;

                if (commandParameter == "Daily_Jobs")
                {
                    homeViewModel.TodoJobs_Daily.Remove(jobModel);
                }
                else if (commandParameter == "Weekly_Jobs")
                {
                    homeViewModel.TodoJobs_Daily.Remove(jobModel);
                }
                else if (commandParameter == "Monthly_Jobs")
                {
                    homeViewModel.TodoJobs_Daily.Remove(jobModel);
                }
            }

        }
    }
}
