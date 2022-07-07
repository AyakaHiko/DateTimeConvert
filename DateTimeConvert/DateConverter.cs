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
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            string s = (string)value;
            if (string.IsNullOrEmpty(s)) return null;
            try
            {
                return DateTime.Parse(s);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return $"{date.Day}/{date.Month}/{date.Year}";
        }
    }

    public class DateRules : ValidationRule
    {
        public override ValidationResult Validate(object value,
            CultureInfo culture)
        {
            var values = (string)value;
            try
            {
                DateTime d;
                if(DateTime.TryParse(values, out d))
                    return ValidationResult.ValidResult;
            }
            catch (Exception)
            {
                return new ValidationResult(false, null);
            }
            return new ValidationResult(false, null);
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
