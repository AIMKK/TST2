using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CoreLibrary
{
    /// <summary>
    /// OpenNewViewParamConverter
    /// </summary>
    public class OpenNewViewParamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values==null||values.Length==0)
            {
                return null;
            }
            Type newViewType = null;
            object valueFromCurrentView = null;
            Window currentView = null;
            //
            if (values.Length>=1)
            {
                newViewType= values[0] as Type; 
            }
            //
            if (values.Length >= 2)
            {
                valueFromCurrentView= values[1] as object; 
            }
            //
            if (values.Length >= 3)
            {
                currentView = values[2] as Window;
            }
            //
            return new OpenNewViewParam { NewViewType = newViewType, ParamValueFromCurrentView = valueFromCurrentView ,CurrentView=currentView};
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
