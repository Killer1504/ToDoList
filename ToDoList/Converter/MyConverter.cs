using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ToDoList.Converter
{
    public class JobDoneConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection<ToDoListLib.Models.JobModel> models)
            {
                var numberChecked = models.Where(s=>s.IsDoneJob).ToArray().Length;
                return $"{numberChecked:00}/{models.Count:00}";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
