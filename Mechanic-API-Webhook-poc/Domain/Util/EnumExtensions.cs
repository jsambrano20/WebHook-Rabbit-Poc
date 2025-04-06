using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString());

        if (field?.GetCustomAttribute<DescriptionAttribute>() is DescriptionAttribute attribute)
        {
            return attribute.Description;
        }

        return value.ToString();
    }
}
