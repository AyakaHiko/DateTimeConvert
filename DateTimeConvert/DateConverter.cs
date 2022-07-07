using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace DateTimeConvert
{
    public class DateConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 0) return " ";
            int year , month , day ;
            if (!int.TryParse(values[0].ToString(), out day)) return null;
            if (!int.TryParse(values[1].ToString(), out month)) return null;
            if (!int.TryParse(values[2].ToString(), out year)) return null;
            if (day < 0 || month < 0 || year < 0) return null;

            DateTime dateTime = new DateTime(year, month, day);

            return dateTime;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return new object[] { date.Day, date.Month, date.Year };
        }
    }

    public class DateRules : ValidationRule
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public override ValidationResult Validate(object value,
            CultureInfo culture)
        {
            
            return new ValidationResult(true, null);
        }
    }

}
