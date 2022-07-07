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
            catch (Exception)
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
        public int MaxValue { get; set; } = 12;
        public override ValidationResult Validate(object value,
            CultureInfo culture)
        {
            if (!int.TryParse(value.ToString(), out var date)) return new ValidationResult(false, "not valid");
            if (MaxValue <= 0) return ValidationResult.ValidResult;
            if (date <= 0 || date > MaxValue)
                return new ValidationResult(false, "out of bounds");
            return ValidationResult.ValidResult;
        }

    }

    public class DayRules : ValidationRule
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public override ValidationResult Validate(object value,
            CultureInfo culture)
        {
            ValidationResult outOfBounds = new ValidationResult(false, "out of bounds");
            if (!int.TryParse(value.ToString(), out var day)) return new ValidationResult(false, "not valid");
            if (day <= 0 || day > 31) return outOfBounds;
            if (Month <= 0) return ValidationResult.ValidResult;
            switch (Month)
            {
                case 4:
                case 6:
                case 9:
                case 11:
                    if (day > 30) return outOfBounds;
                    break;
                case 2:
                    if (Year <= 0) break;
                    if (DateTime.IsLeapYear(Year) && day > 29)
                    {
                        return outOfBounds;
                    }
                    if (day > 28) return outOfBounds;
                    break;
                default:
                    return outOfBounds;
            }
            return ValidationResult.ValidResult;
        }
    }
    


}
