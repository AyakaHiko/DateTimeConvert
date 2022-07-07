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
            try
            {
                DateTime dateTime = new DateTime(year, month, day);
                return dateTime;
            }
            catch(Exception)
            {
                return null;
            }
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
            return new ValidationResult(true, null);

            if (!int.TryParse(value.ToString(), out var d)) return new ValidationResult(false, "not valid");
            //test
            if (d <= 0) return new ValidationResult(false, "out of bounds");
            //todo
            return new ValidationResult(true, null);
        }

    }

    public class DayRules : DateRules
    {
        public override ValidationResult Validate(object value,
            CultureInfo culture)
        {
            ValidationResult outOfBounds = new ValidationResult(false, "out of bounds");
            if (!int.TryParse(value.ToString(), out var day)) return new ValidationResult(false, "not valid");
            if (day <= 0 || day > 31) return outOfBounds;
            switch (Date.Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    if (day > 31) return outOfBounds;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    if (day > 30) return outOfBounds;
                    break;
                case 2:
                    if (DateTime.IsLeapYear(Date.Year) && day > 29)
                    {
                        return outOfBounds;
                    }
                    if (day > 28) return outOfBounds;
                    break;
                default:
                    return outOfBounds;
            }

            return new ValidationResult(true, null);
        }
    }


}
