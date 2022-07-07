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
            if (values.Length == 0) return null;
            if (!int.TryParse(values[0].ToString(), out var day)) return null;
            if (!int.TryParse(values[1].ToString(), out var month)) return null;
            if (!int.TryParse(values[2].ToString(), out var year)) return null;
            if (day <= 0 || month <= 0 || year <= 0) return null;

            DateTime dateTime = new DateTime(year, month, day);

            return dateTime;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var date = (DateTime)value;
            return new object[] { date.Day.ToString(), date.Month.ToString(), date.Year.ToString() };
        }
    }

    public class DateRules : ValidationRule
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public override ValidationResult Validate(object value,
            CultureInfo culture)
        {
            if (!int.TryParse(value.ToString(), out var d)) return new ValidationResult(false, "not valid");
            //test
            if(d <= 0||d>31) new ValidationResult(false, "out of bounds");
            //todo
            return new ValidationResult(true, null);
        }
    }

}
