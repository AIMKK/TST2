using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CoreAppTest
{
    /// <summary>
    /// loginTypeValue
    /// </summary>
    public class loginTypeValue
    {
        public string TypeValue { get; set; }
    }

    /// <summary>
    /// loginTypeValueConverter
    /// </summary>
    public class loginTypeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new loginTypeValue() { TypeValue= (value ?? "").ToString().Trim() };            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
