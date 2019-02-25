using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MVVMDemo.ViewModel.Converters
{
    public class DatetimeToDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Recibo un dateTime pero yo sólo quiero mostrar la fecha...
            DateTime date = (DateTime)value;
            return date.ToString("MM/d/yyyy"); 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
