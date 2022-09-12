using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace WebUI.Data.Models
{
    /// <summary>
    /// Defines extension methods for all Enums
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Pretty-prints an enum name in Title case with spaces instead of underscores
        /// </summary>
        public static string ToLabel(this Enum e)
        {
            if (e == null) return null;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            return textInfo.ToTitleCase(e.ToString().ToLower().Replace('_', ' '));
        }

        public static string GetDisplayName(this Enum e)
        {
            string displayName;
            displayName = e.GetType()
                .GetMember(e.ToString())
                .FirstOrDefault()
                .GetCustomAttribute<DisplayAttribute>()?
                .GetName();
            if (String.IsNullOrEmpty(displayName))
            {
                displayName = e.ToString();
            }
            return displayName;
        }
    }
}
