using System;
using System.Reflection;

namespace Infragistics.Composite.Wpf.Proxies
{
    /// <summary>
    /// This attribute is used to map an enumeration value to a corresponding value from a different enumeration.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    internal class EnumMappingAttribute : Attribute
    {
        internal object MappedValue { get; private set; }

        internal EnumMappingAttribute(object mappedValue)
        {
            if (mappedValue is Enum == false)
                throw new ArgumentException("'mappedValue' is not of an enum type.");

            this.MappedValue = mappedValue;
        }

        internal static object GetMappedValue(object enumValue)
        {
            if (enumValue is Enum == false)
                throw new ArgumentException("'enumValue' is not of an enum type.");

            string valueName = enumValue.ToString();
            FieldInfo valueField = enumValue.GetType().GetField(valueName);
            EnumMappingAttribute attribute = Attribute.GetCustomAttribute(valueField, typeof(EnumMappingAttribute)) as EnumMappingAttribute;
            if (attribute == null)
                throw new ArgumentException("'enumValue' is not decorated with an EnumMappingAttribute.");

            return attribute.MappedValue;
        }
    }
}