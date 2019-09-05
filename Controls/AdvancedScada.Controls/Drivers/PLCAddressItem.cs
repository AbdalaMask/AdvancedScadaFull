
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using AdvancedScada;
using AdvancedScada.Controls.Drivers;
using AdvancedScada;
using AdvancedScada.Controls;

namespace AdvancedScada.Controls.Drivers
{
    [Serializable]
    [TypeConverter(typeof(PLCAddressItemTypeConverter))]
    public class PLCAddressItem
    {
        private string m_PLCAddress;

        private int m_NumberOfElements;

        private double m_ScaleFactor;

        private double m_ScaleOffset;

        private string m_LastValue;

        private int m_SubscriptionID;

        private string m_Description;

        private string m_Name;

        public string PLCAddress
        {
            get
            {
                return m_PLCAddress;
            }
            set
            {
                m_PLCAddress = value;
            }
        }

        public int NumberOfElements
        {
            get
            {
                return m_NumberOfElements;
            }
            set
            {
                m_NumberOfElements = value;
            }
        }

        public double ScaleFactor
        {
            get
            {
                return m_ScaleFactor;
            }
            set
            {
                m_ScaleFactor = value;
            }
        }

        public double ScaleOffset
        {
            get
            {
                return m_ScaleOffset;
            }
            set
            {
                m_ScaleOffset = value;
            }
        }

        [Browsable(false)]
        public string LastValue
        {
            get
            {
                return m_LastValue;
            }
            set
            {
                m_LastValue = value;
            }
        }

        [Browsable(false)]
        public int SubscriptionID
        {
            get
            {
                return m_SubscriptionID;
            }
            set
            {
                m_SubscriptionID = value;
            }
        }

        public string Description
        {
            get
            {
                return m_Description;
            }
            set
            {
                m_Description = value;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        public PLCAddressItem()
        {
            m_NumberOfElements = 1;
            m_ScaleFactor = 1.0;
        }

        public PLCAddressItem(string plcAddress)
            : this()
        {
            m_PLCAddress = plcAddress;
        }

        public PLCAddressItem(string plcAddress, int numberOfElements)
            : this(plcAddress)
        {
            m_NumberOfElements = numberOfElements;
        }

        public PLCAddressItem(string plcAddress, int numberOfElements, string name)
            : this(plcAddress, numberOfElements)
        {
            m_Name = name;
        }

        public PLCAddressItem(string plcAddress, int numberOfElements, string name, string description)
            : this(plcAddress, numberOfElements, name)
        {
            m_Description = description;
        }

        public PLCAddressItem(string plcAddress, int numberOfElements, string name, string description, double scaleFactor)
            : this(plcAddress, numberOfElements, name, description)
        {
            m_ScaleFactor = scaleFactor;
        }

        public PLCAddressItem(string plcAddress, int numberOfElements, string name, string description, double scaleFactor, double scaleOffset)
            : this(plcAddress, numberOfElements, name, description, scaleFactor)
        {
            m_ScaleOffset = scaleOffset;
        }

        public string GetScaledValue(string value)
        {
            if ((m_ScaleFactor == 1.0) & (m_ScaleOffset == 0.0))
            {
                return value;
            }
            double result;
            if (double.TryParse(value, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result))
            {
                return Conversions.ToString(result * m_ScaleFactor + m_ScaleOffset);
            }
            return value;
        }

        public double GetScaledValue(double value)
        {
            if ((m_ScaleFactor == 1.0) & (m_ScaleOffset == 0.0))
            {
                return value;
            }
            return value * m_ScaleFactor + m_ScaleOffset;
        }

        public PLCAddressItem Clone()
        {
            PLCAddressItem pLCAddressItem = new PLCAddressItem();
            pLCAddressItem.Name = m_Name;
            pLCAddressItem.PLCAddress = m_PLCAddress;
            pLCAddressItem.NumberOfElements = m_NumberOfElements;
            pLCAddressItem.ScaleFactor = m_ScaleFactor;
            pLCAddressItem.ScaleOffset = m_ScaleOffset;
            pLCAddressItem.LastValue = m_LastValue;
            pLCAddressItem.Description = m_Description;
            pLCAddressItem.SubscriptionID = m_SubscriptionID;
            return pLCAddressItem;
        }
    }

    public class PLCAddressItemTypeConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType.Equals(typeof(string)) | sourceType.Equals(typeof(PLCAddressItem)))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType.Equals(typeof(string)) | (destinationType.Equals(typeof(PLCAddressItem)) | destinationType.Equals(typeof(InstanceDescriptor))))
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string text = Conversions.ToString(value);
                char c = culture.TextInfo.ListSeparator[0];
                string[] array = text.Split(c);
                try
                {
                    if (string.IsNullOrEmpty(text))
                    {
                        return null;
                    }
                    if (array.Length == 1)
                    {
                        return new PLCAddressItem(array[0]);
                    }
                    if (array.Length == 2)
                    {
                        return new PLCAddressItem(array[0], Convert.ToInt32(array[1]));
                    }
                    if (array.Length == 3)
                    {
                        return new PLCAddressItem(array[0], Convert.ToInt32(array[1]), array[2]);
                    }
                    if (array.Length == 4)
                    {
                        return new PLCAddressItem(array[0], Convert.ToInt32(array[1]), array[2], array[3]);
                    }
                    if (array.Length == 5)
                    {
                        return new PLCAddressItem(array[0], Convert.ToInt32(array[1]), array[2], array[3], Convert.ToDouble(array[4]));
                    }
                    if (array.Length >= 6)
                    {
                        return new PLCAddressItem(array[0], Convert.ToInt32(array[1]), array[2], array[3], Convert.ToDouble(array[4]), Convert.ToDouble(array[5]));
                    }
                    return new PLCAddressItem();
                }
                catch (Exception ex)
                {
                    ProjectData.SetProjectError(ex);
                    Exception ex2 = ex;
                    throw new InvalidCastException(value.ToString());
                }
            }
            return base.ConvertFrom(context, culture, RuntimeHelpers.GetObjectValue(value));
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType.Equals(typeof(string)) && value is PLCAddressItem)
            {
                PLCAddressItem pLCAddressItem = (PLCAddressItem)value;
                char value2 = culture.TextInfo.ListSeparator[0];
                string text = string.Empty;
                if (!string.IsNullOrEmpty(pLCAddressItem.PLCAddress))
                {
                    text = pLCAddressItem.PLCAddress;
                }
                string text2 = string.Empty;
                if (!string.IsNullOrEmpty(pLCAddressItem.Name))
                {
                    text2 = pLCAddressItem.Name;
                }
                string text3 = string.Empty;
                if (!string.IsNullOrEmpty(pLCAddressItem.Description))
                {
                    text3 = pLCAddressItem.Description;
                }
                if ((pLCAddressItem.NumberOfElements == 1) & (Operators.CompareString(text2, string.Empty, TextCompare: false) == 0) & (Operators.CompareString(text3, string.Empty, TextCompare: false) == 0) & (pLCAddressItem.ScaleFactor == 1.0) & (pLCAddressItem.ScaleOffset == 0.0))
                {
                    return text;
                }
                return text + Conversions.ToString(value2) + Conversions.ToString(pLCAddressItem.NumberOfElements) + Conversions.ToString(value2) + text2 + Conversions.ToString(value2) + text3 + Conversions.ToString(value2) + Conversions.ToString(pLCAddressItem.ScaleFactor) + Conversions.ToString(value2) + Conversions.ToString(pLCAddressItem.ScaleOffset);
            }
            if (destinationType.Equals(typeof(InstanceDescriptor)))
            {
                PLCAddressItem pLCAddressItem2 = (PLCAddressItem)value;
                if (pLCAddressItem2.ScaleOffset != 0.0)
                {
                    ConstructorInfo constructor = typeof(PLCAddressItem).GetConstructor(new Type[6]
                    {
                    typeof(string),
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(double),
                    typeof(double)
                    });
                    return new InstanceDescriptor(constructor, new object[6]
                    {
                    pLCAddressItem2.PLCAddress,
                    pLCAddressItem2.NumberOfElements,
                    pLCAddressItem2.Name,
                    pLCAddressItem2.Description,
                    pLCAddressItem2.ScaleFactor,
                    pLCAddressItem2.ScaleOffset
                    });
                }
                if (pLCAddressItem2.ScaleFactor != 1.0)
                {
                    ConstructorInfo constructor2 = typeof(PLCAddressItem).GetConstructor(new Type[5]
                    {
                    typeof(string),
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(double)
                    });
                    return new InstanceDescriptor(constructor2, new object[5]
                    {
                    pLCAddressItem2.PLCAddress,
                    pLCAddressItem2.NumberOfElements,
                    pLCAddressItem2.Name,
                    pLCAddressItem2.Description,
                    pLCAddressItem2.ScaleFactor
                    });
                }
                if (!string.IsNullOrEmpty(pLCAddressItem2.Description))
                {
                    ConstructorInfo constructor3 = typeof(PLCAddressItem).GetConstructor(new Type[4]
                    {
                    typeof(string),
                    typeof(int),
                    typeof(string),
                    typeof(string)
                    });
                    return new InstanceDescriptor(constructor3, new object[4]
                    {
                    pLCAddressItem2.PLCAddress,
                    pLCAddressItem2.NumberOfElements,
                    pLCAddressItem2.Name,
                    pLCAddressItem2.Description
                    });
                }
                if (!string.IsNullOrEmpty(pLCAddressItem2.Name))
                {
                    ConstructorInfo constructor4 = typeof(PLCAddressItem).GetConstructor(new Type[3]
                    {
                    typeof(string),
                    typeof(int),
                    typeof(string)
                    });
                    return new InstanceDescriptor(constructor4, new object[3]
                    {
                    pLCAddressItem2.PLCAddress,
                    pLCAddressItem2.NumberOfElements,
                    pLCAddressItem2.Name
                    });
                }
                ConstructorInfo constructor5 = typeof(PLCAddressItem).GetConstructor(new Type[2]
                {
                typeof(string),
                typeof(int)
                });
                return new InstanceDescriptor(constructor5, new object[2]
                {
                pLCAddressItem2.PLCAddress,
                pLCAddressItem2.NumberOfElements
                });
            }
            return base.ConvertTo(context, culture, RuntimeHelpers.GetObjectValue(value), destinationType);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(PLCAddressItem), attributes);
        }
    }
}