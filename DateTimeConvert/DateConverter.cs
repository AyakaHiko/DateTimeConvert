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
            int year = 1, month = 1, day = 1;
            if (!int.TryParse(values[0].ToString(), out day)) return null;
            if (!int.TryParse(values[1].ToString(), out month)) return null;
            if (!int.TryParse(values[2].ToString(), out year)) return null;

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
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int day;
            if (!int.TryParse(value.ToString(), out day)) new ValidationResult(false, "out");
            return day > 0 ? new ValidationResult(true, null) : new ValidationResult(false, "out");
        }
    }

}
