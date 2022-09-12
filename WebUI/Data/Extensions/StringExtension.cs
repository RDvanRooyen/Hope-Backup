using System;
using System.Linq;
using System.Text;
using WebUI.Data.Models;

namespace WebUI.Data
{
    public static class StringExtension
    {
        /// <summary>
        /// A convenience to avoid tediously long lines of code for a common check 
        /// that the given string is not null, empty, or comprised only of whitespace.
        /// Equivalent to !String.IsNullOrWhiteSpace(s)
        /// </summary>
        public static bool HasValue(this string s)
        {
            return !String.IsNullOrWhiteSpace(s);
        }

        /// <summary>
        /// Split this string across spaces and find the max number of characters in any one of the split parts.
        /// Null-safe.
        /// </summary>
        /// <returns>0 if this is a null string; otherwise, the character count of the longest substring between spaces</returns>
        public static int LengthOfLongestWord(this string s)
        {
            if (s == null) return 0;

            var words = s.Split(' ');
            return words.Select(x => x.Length).Max();
        }

        /// <summary>
        /// Turn this object into a string by using its default format.
        /// Null-safe. Guaranteed to return a non-null string.
        /// </summary>
        /// <param name="value">any object, or null</param>
        /// <param name="isMoney">is the value expected to represent currency?</param>
        /// <returns>
        /// an empty string if value is null;
        /// the value itself if it's already a string;
        /// value.ToString if there's no default format defined for value's Type;
        /// otherwise, applies string.Format to the value using the determined format
        /// </returns>
        public static string Format(this object value, bool isMoney)
        {
            if (value == null) return "";

            if (value is string s) return s;

            // lookup the default format for the value's Type
            string format = value.GetType()?.GetDefaultFormat(isMoney);

            // get the appropriate string representation of the given value
            string text = (format == null) ? value.ToString()
                : string.Format($"{{0:{format}}}", value);

            return text;
        }

        /// <summary>
        /// Combines parts of a person's name, some of which may or may not be set.
        /// </summary>
        /// <param name="first">First name</param>
        /// <param name="middle">Middle initial/name</param>
        /// <param name="last">Last name</param>
        /// <param name="suffix">Suffix (Jr, III, etc)</param>
        /// <param name="lastNameFirst">Should the formatted name be in a sortable last-name-first style?</param>
        public static string FormatName(string first, string middle, string last, string suffix, bool lastNameFirst)
        {
            StringBuilder sb = new StringBuilder();

            if (lastNameFirst)
            {
                sb.Append($"{last}, {first}");
                if (!String.IsNullOrWhiteSpace(middle))
                {
                    sb.Append($" {middle}");
                }
            }
            else
            {
                sb.Append(first);
                if (!String.IsNullOrWhiteSpace(middle))
                {
                    sb.Append($" {middle}");
                }
                sb.Append($" {last}");
            }

            if (!String.IsNullOrWhiteSpace(suffix))
            {
                sb.Append($" {suffix}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Produces a multi-line string using typical address formatting and the given address parts.
        /// Starts with any number of address lines, which could include name, company, etc.
        /// Then a line like [city], [state] [postalCode].
        /// Then country on the final line.
        /// Any empty parts or empty lines are omitted.
        /// </summary>
        public static string FormatAddress(string city, string state, string postalCode, string country, params string[] addressLines)
        {
            var sb = new StringBuilder();

            foreach (var line in addressLines)
            {
                if (line.HasValue())
                {
                    sb.AppendLine(line);
                }
            }

            var cityStateZip = new StringBuilder();
            if (city.HasValue())
            {
                cityStateZip.Append($"{city}, ");
            }
            if (state.HasValue())
            {
                cityStateZip.Append($"{state} ");
            }
            if (postalCode.HasValue())
            {
                cityStateZip.Append(postalCode);
            }

            if (cityStateZip.Length > 0)
            {
                sb.AppendLine(cityStateZip.ToString());
            }

            if (country.HasValue())
            {
                sb.AppendLine(country);
            }

            return sb.ToString();
        }
    }
}
