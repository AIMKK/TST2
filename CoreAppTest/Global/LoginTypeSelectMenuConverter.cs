using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CoreAppTest
{
    public class LoginTypeSelectMenuConverter : IMultiValueConverter
    {
        class stateEnum
        {
            public const string Normal = "Normal";
            public const string MouseOver = "MouseOver";
            public const string Selected = "Selected";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string imagePath = "";
            object result = null;
            try
            {
                if (values != null && values.Length >= 2)
                {
                    string typeMenu = values[0] as string;
                    string state = values[1] as string;
                    //
                    #region convertToImagePath

                    if (string.Equals(typeMenu, "RoadShow", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.Equals(state, stateEnum.Normal, StringComparison.OrdinalIgnoreCase))
                        {
                            imagePath = "pack://application:,,,/Image/btn_RS_CHS.png";
                        }
                        else if (string.Equals(state, stateEnum.MouseOver, StringComparison.OrdinalIgnoreCase))
                        {
                            imagePath = "pack://application:,,,/Image/btn_RS_CHS_over.png";
                        }
                        else if (string.Equals(state, stateEnum.Selected, StringComparison.OrdinalIgnoreCase))
                        {
                            imagePath = "pack://application:,,,/Image/btn_RS_CHS_click.png";
                        }
                    }
                    else if (string.Equals(typeMenu, "SHOP", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.Equals(state, stateEnum.Normal, StringComparison.OrdinalIgnoreCase))
                        {
                            imagePath = "pack://application:,,,/Image/btn_SHOP_CHS.png";
                        }
                        else if (string.Equals(state, stateEnum.MouseOver, StringComparison.OrdinalIgnoreCase))
                        {
                            imagePath = "pack://application:,,,/Image/btn_SHOP_CHS_over.png";
                        }
                        else if (string.Equals(state, stateEnum.Selected, StringComparison.OrdinalIgnoreCase))
                        {
                            imagePath = "pack://application:,,,/Image/btn_SHOP_CHS_click.png";
                        }
                    }
                    else if (string.Equals(typeMenu, "OFFICE", StringComparison.OrdinalIgnoreCase))
                    {
                        if (string.Equals(state, stateEnum.Normal, StringComparison.OrdinalIgnoreCase))
                        {
                            imagePath = "pack://application:,,,/Image/btn_WH_CHS.png";
                        }
                        else if (string.Equals(state, stateEnum.MouseOver, StringComparison.OrdinalIgnoreCase))
                        {
                            imagePath = "pack://application:,,,/Image/btn_WH_CHS_over.png";
                        }
                        else if (string.Equals(state, stateEnum.Selected, StringComparison.OrdinalIgnoreCase))
                        {
                            imagePath = "pack://application:,,,/Image/btn_WH_CHS_click.png";
                        }
                    }
                    #endregion convertToImagePath
                }
                if (imagePath != null)
                {
                    result= new ImageSourceConverter().ConvertFromString(imagePath);
                }               
            }
            catch (Exception ex)
            {
                result = null;
                CoreLibrary.NlogHelper.LogToFile(ex.ToString());
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
