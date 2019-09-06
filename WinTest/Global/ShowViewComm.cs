using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WinTest
{
    public class ShowTargetView
    {
        public static void ShowView(TargetViewCreateMessage message)
        {
            Window view = null;
            if (message != null && message.TargetViewType != null)
            {
                view = Activator.CreateInstance(message.TargetViewType) as Window;
            }
            if (view == null)
            {
                return;
            }
            //
            object dataContext = view.DataContext;
            if (dataContext != null)//
            {
                if (message != null)
                {
                    SetPropValue(dataContext, message.TargetViewModelInitPropName, message.TargetViewModelInitPropValue);
                }
            }
            //
            if (message.TargetViewModalShow)
            {
                view.ShowDialog();
            }
            else
            {
                view.Show();
            }

        }

        /// <summary>
        /// 直接设置field值的值
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="ignoreCase"></param>
        protected static void SetPropValue(object setValuetoObj, string propName, object newValue, bool ignorePropCase = true)
        {
            if (setValuetoObj == null || (propName ?? "").Trim().Length == 0)
            {
                return;
            }
            string pName = (propName ?? "").Trim();
            System.Reflection.PropertyInfo property = null;
            if (ignorePropCase)
                property = setValuetoObj.GetType().GetProperty(pName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            else
                property = setValuetoObj.GetType().GetProperty(pName);
            //
            if (property != null)
            {
                reflectSetValue(setValuetoObj, property, newValue);
            }

        }

        protected static void reflectSetValue(object setValuetoObj, PropertyInfo setValuePropInfo, object value)
        {
            if (setValuePropInfo == null || setValuetoObj == null)
            {
                return;
            }
            object v = null;
            if (value == null || value == DBNull.Value)
            {
                if (setValuePropInfo.PropertyType.IsGenericType
                        || setValuePropInfo.PropertyType.IsClass)
                    v = null;
                else
                    v = Activator.CreateInstance(setValuePropInfo.PropertyType);
            }
            else
            {
                if (setValuePropInfo.PropertyType.IsEnum)
                {
                    v = Convert.ChangeType(value, System.TypeCode.Int32);
                }
                else if (setValuePropInfo.PropertyType.IsGenericType &&
                    setValuePropInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    if (value.ToString().Trim() == string.Empty)
                        v = null;
                    else
                    {
                        if (Nullable.GetUnderlyingType(setValuePropInfo.PropertyType) == typeof(Boolean))
                        {
                            if (value.ToString().Trim() == "1")
                            {
                                value = bool.TrueString;
                            }
                            else if (value.ToString().Trim() == "0")
                            {
                                value = bool.FalseString;
                            }
                            else if (value.ToString().Trim().ToLower() != bool.TrueString.ToLower() && value.ToString().Trim().ToLower() != bool.FalseString.ToLower())
                            {
                                value = bool.FalseString;
                            }

                        }
                        Type underlyingType = Nullable.GetUnderlyingType(setValuePropInfo.PropertyType);

                        v = Convert.ChangeType(value, underlyingType);
                    }
                }
                else
                {
                    if (setValuePropInfo.PropertyType == typeof(Boolean))
                    {
                        value = value.ToString() == "1" ? bool.TrueString : bool.FalseString;
                        if (value.ToString().Trim() == "1")
                            value = bool.TrueString;
                        else if (value.ToString().Trim() == "0")
                            value = bool.FalseString;
                    }

                    //
                    if (value is IConvertible)
                    {
                        v = Convert.ChangeType(value, setValuePropInfo.PropertyType);
                    }
                    else
                    {
                        v = value;
                    }

                }
            }
            setValuePropInfo.SetValue(setValuetoObj, v, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        protected static object GetPropValue(object getValueFromObj, string propName, bool ignoreCase)
        {
            if (getValueFromObj == null || (propName ?? "").Trim().Length == 0)
            {
                return null;
            }
            string pName = (propName ?? "").Trim();
            object result = null;
            System.Reflection.PropertyInfo property = null;
            if (ignoreCase)
                property = getValueFromObj.GetType().GetProperty(pName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            else
                property = getValueFromObj.GetType().GetProperty(pName);
            //
            if (property != null)
            {
                result = property.GetValue(getValueFromObj, null);
            }
            return result;
        }
    }
}
