using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media;
using AvaloniaUIHelp.Models;
using JetBrains.Annotations;



namespace AvaloniaUIHelp.ViewModels
{
    public class VisualProperty : ViewModelBase
    {
        private string m_Name;

       public string Name
       {
           get => m_Name;
           set
           {
               m_Name = value;
               this.RaisePropertyChanged(nameof(Name));
           }
       }

        private object m_Value;

        public object Value
        {
            get => m_Value;
            set
            {
                m_Value = value;
                this.RaisePropertyChanged(nameof(Value));

                if (value != null)
                {
                    if (value.GetType().Name == "DataTemplates")
                    {

                    }
                    m_StringValue = value.ToString();

                }
                else
                {
                    m_StringValue = "null";
                }

                //bool needChange = true;
                //if (m_Value==null)
                //{
                //    needChange = false;
                //}

                //if (m_Value==value)
                //{
                //    needChange = false;
                //    return;
                //}
                //m_Value = value;
                //this.RaisePropertyChanged(nameof(Value));
                //if (Target==null)
                //{
                //    return;
                //}

                //if (PropertyInfo==null)
                //{
                //    return;
                //}


                //Type targetType = this.PropertyInfo.PropertyType;
                //if (targetType.IsAssignableFrom(typeof(string)))
                //{
                //    this.PropertyInfo.SetValue(Target, value);
                //}
                //else
                //{
                //    TypeConverter converter = TypeDescriptor.GetConverter(targetType);
                //    if (converter != null)
                //    {
                //        try
                //        {
                //           bool canConverter= converter.CanConvertFrom(typeof(string));
                //            this.PropertyInfo.SetValue(this.Target, converter.ConvertFrom(value));
                //        }
                //        catch (Exception)
                //        {
                //        }
                //    }
                //}

            }
        }

        private string m_StringValue = "null";

        public string StringValue
        {
            get
            {
                return m_StringValue;
                if (Value == null)
                {
                    return string.Empty;
                }

                string strValue = Value.ToString();
                if (string.IsNullOrEmpty(strValue))
                {
                    return string.Empty;
                }

                return strValue;
            }
            set
            {
                if (this.PropertyInfo == null)
                {
                    // if this is a PropertyInformation object constructed for an item in a collection
                    // then just return, since setting the value via a string doesn't make sense.
                    return;
                }

                try
                {
                    //if (Value == null)
                    //{
                    //    return;
                    //}

                    //string strValue = Value.ToString();
                    //if (strValue == null)
                    //{
                    //    return;
                    //}

                    //if (strValue == value)
                    //{
                    //    return;
                    //}

                    //if (PropertyInfo.PropertyType.IsEnum)
                    //{
                    //    return;
                    //}

                    //if (PropertyInfo.PropertyType == typeof(bool))
                    //{
                    //    return;
                    //}

                    if (value == null)
                    {
                        return;
                    }

                    if (m_StringValue == value)
                    {
                        return;
                    }
                    m_StringValue = value;
                    object setValue = GetObj(value, PropertyInfo, Target);
                    if (setValue != null)
                    {
                        if (this.Target == null)
                        {

                        }
                        else
                        {
                            this.PropertyInfo.SetValue(this.Target, setValue);
                        }

                    }
                }
                catch (Exception e)
                {

                }


                //Type targetType = this.PropertyInfo.PropertyType;
                //if (targetType.IsAssignableFrom(typeof(string)))
                //{
                //    this.PropertyInfo.SetValue(this.Target, value);
                //}
                //else
                //{
                //    TypeConverter converter = TypeDescriptor.GetConverter(targetType);
                //    if (converter != null)
                //    {
                //        try
                //        {
                //            object setValue = converter.ConvertFrom(value);
                //            this.PropertyInfo.SetValue(this.Target, setValue);
                //        }
                //        catch (Exception exception)
                //        {
                //        }
                //    }
                //}
            }
        }

        private bool? m_BolValue;

        public bool? BolValue
        {
            get => m_BolValue;
            set
            {
                if (m_BolValue.HasValue)
                {
                    this.PropertyInfo.SetValue(this.Target, value);
                }
                m_BolValue = value;
            }
        }

        public List<string> LisEnumValues { get; set; } = new List<string>();

        [CanBeNull]
        public static object GetObj(string strValue, PropertyInfo propertyInfo, object target)
        {
            Type targetType = propertyInfo.PropertyType;
            if (targetType.IsAssignableFrom(typeof(string)))
            {
                return strValue;
            }

            try
            {
                if (targetType.IsAssignableFrom(typeof(Thickness)))
                {
                    Thickness thickness = Thickness.Parse(strValue);
                    return thickness;
                }
                else
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(targetType);
                    if (converter != null)
                    {
                        try
                        {
                            object setValue = converter.ConvertFrom(strValue);
                            return setValue;
                        }
                        catch (Exception exception)
                        {
                        }
                    }
                }
            }
            catch (Exception e)
            {
            }
            return null;

        }

        public object Target { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        private TargetType m_TargetType = TargetType.Defult;

        /// <summary>
        /// 目标类型
        /// </summary>
        public TargetType TargetType
        {
            get => m_TargetType;
            set
            {
                m_TargetType = value;
                this.RaisePropertyChanged(nameof(TargetType));
            }
        }

        private FontFamily m_FontFamily=new FontFamily("Microsoft YaHei");

        public FontFamily FontFamily
        {
            get => m_FontFamily;
            set
            {
                m_FontFamily = value;
                this.RaisePropertyChanged(nameof(FontFamily));
            }
        }

    }
}
