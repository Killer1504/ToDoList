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
                    var homeViewModel = _this.DataContext as HomeViewModel;
                    homeViewModel?.OnSaveData();
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
                else if (commandParameter == "Yearly_Jobs")
                {
                    homeViewModel.TodoJobs_Yearly.Remove(jobModel);
                }
            }

        }

        private void MenuItem_Move_Up_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = e.Source as MenuItem;
            var jobModel = menuItem?.DataContext as JobModel;
            if (jobModel != null)
            {
                var commandParameter = menuItem.CommandParameter?.ToString();
                var homeViewModel = _this.DataContext as HomeViewModel;

                if (commandParameter == "Daily_Jobs")
                {
                    var index = homeViewModel.TodoJobs_Daily.IndexOf(jobModel);
                    if (index > 0 && index < homeViewModel.TodoJobs_Daily.Count)
                    {
                        homeViewModel.TodoJobs_Daily.Move(index, index - 1);
                    }
                }
                else if (commandParameter == "Weekly_Jobs")
                {
                    var index = homeViewModel.TodoJobs_Weekly.IndexOf(jobModel);
                    if (index > 0 && index < homeViewModel.TodoJobs_Weekly.Count)
                    {
                        homeViewModel.TodoJobs_Weekly.Move(index, index - 1);
                    }
                }
                else if (commandParameter == "Monthly_Jobs")
                {
                    var index = homeViewModel.TodoJobs_Monthly.IndexOf(jobModel);
                    if (index > 0 && index < homeViewModel.TodoJobs_Monthly.Count)
                    {
                        homeViewModel.TodoJobs_Monthly.Move(index, index - 1);
                    }
                }
                else if (commandParameter == "Yearly_Jobs")
                {
                    var index = homeViewModel.TodoJobs_Yearly.IndexOf(jobModel);
                    if (index > 0 && index < homeViewModel.TodoJobs_Yearly.Count)
                    {
                        homeViewModel.TodoJobs_Yearly.Move(index, index - 1);
                    }
                }
                homeViewModel?.OnSaveData();
            }
        }

        private void MenuItem_Move_Down_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = e.Source as MenuItem;
            var jobModel = menuItem?.DataContext as JobModel;
            if (jobModel != null)
            {
                var commandParameter = menuItem.CommandParameter?.ToString();
                var homeViewModel = _this.DataContext as HomeViewModel;

                if (commandParameter == "Daily_Jobs")
                {
                    var index = homeViewModel.TodoJobs_Daily.IndexOf(jobModel);
                    if (index >= 0 && index < homeViewModel.TodoJobs_Daily.Count - 1)
                    {
                        homeViewModel.TodoJobs_Daily.Move(index, index + 1);
                    }
                }
                else if (commandParameter == "Weekly_Jobs")
                {
                    var index = homeViewModel.TodoJobs_Weekly.IndexOf(jobModel);
                    if (index > 0 && index < homeViewModel.TodoJobs_Weekly.Count - 1)
                    {
                        homeViewModel.TodoJobs_Weekly.Move(index, index + 1);
                    }
                }
                else if (commandParameter == "Monthly_Jobs")
                {
                    var index = homeViewModel.TodoJobs_Monthly.IndexOf(jobModel);
                    if (index > 0 && index < homeViewModel.TodoJobs_Monthly.Count - 1)
                    {
                        homeViewModel.TodoJobs_Monthly.Move(index, index + 1);
                    }
                }
                else if (commandParameter == "Yearly_Jobs")
                {
                    var index = homeViewModel.TodoJobs_Yearly.IndexOf(jobModel);
                    if (index > 0 && index < homeViewModel.TodoJobs_Yearly.Count - 1)
                    {
                        homeViewModel.TodoJobs_Yearly.Move(index, index + 1);
                    }
                }

                homeViewModel?.OnSaveData();
            }
        }
    }
}
