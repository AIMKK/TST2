using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WinTest
{
    public class ShowTargetViewParamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null && values.Length >= 3)
            {
                Type type = values[0] as Type;
                object param = values[1] as object;
                bool modalShow = false;
                bool.TryParse(values[2].ToString(), out modalShow);

                return new ShowTargetViewParam { ViewType = type, InitParamValue = param, ModalShow = modalShow };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
