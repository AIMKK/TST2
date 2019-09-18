using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WindowUITest
{
    /// <summary>
    /// IsCurrentLoginLocationTypeConverter
    /// </summary>
    public class IsCurrentLoginLocationTypeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;
            try
            {
                result = false;
                if (values==null|| values.Length<=1)
                {
                    result= false;
                }
                string typeValue1 = values[0] as string;
                string typeValue2 = values[1] as string;
                //
                if (string.Equals(typeValue1, typeValue2,StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                WindowUI.NlogHelper.LogToFile(ex.ToString());
            }  
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
