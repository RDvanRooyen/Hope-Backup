using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI
{
    public static class MethodExtension
    {
        public const string DateOnly = "MM/dd/yyyy";
        public const string DateTime = "MM/dd/yyyy hh:mm:ss tt";
        public const string DateSortable = "yyyyMMddHHmmss";

        public static string ToDecimalTwoPoint(this decimal? d)
        {
            if (d == null) return "-";
            return d.Value.ToString("0.00");
        }
        public static string ToSortableDate(this DateTime? d)
        {
            if (d == null) return string.Empty;

            return d?.ToString(DateSortable);
        }
        public static string ToHumanDateOnly(this DateTime? d)
        {
            if (d == null) return string.Empty;

            return d?.ToString(DateOnly);
        }
        public static string ToHumanDateTime(this DateTime? d)
        {
            if (d == null) return string.Empty;

            return d?.ToString(DateTime);
        }
        public static string ToHumanDateTime(this DateTime d)
        {
           

            return d.ToString(DateTime);
        }

        public static string GetFirstSentence(this string s, int totalSentence=1)
        {
            var sentences = s.Split(".".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            List<string> list = new();
            for(var i = 0; i < totalSentence; i++)
            {
                if(sentences.Length > i)
                {
                    list.Add(sentences[i].Trim());
                }
            }

            return string.Join(". ", list);
            //var text = s; 
            //var dot = text?.IndexOf('.');
            //if (dot >= 0)
            //{
            //    return text.Substring(0, dot ?? 0) + ".";
            //}
            //else
            //{
            //    if (noSentenceMaxLength > 0)
            //    {
            //        int subStringLength = (new[] { noSentenceMaxLength, text.Length }).Min();
            //        return s.Substring(0, subStringLength);
            //    }
            //    else
            //    {
            //        return text;
            //    }

            //}
        }
    }
    public static class NavigationManagerExtensions
    {
        public static bool TryGetQueryString<T>(this NavigationManager navManager, string key, out T value)
        {
            var uri = navManager.ToAbsoluteUri(navManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue(key, out var valueFromQueryString))
            {
                if (typeof(T) == typeof(int) && int.TryParse(valueFromQueryString, out var valueAsInt))
                {
                    value = (T)(object)valueAsInt;
                    return true;
                }

                if (typeof(T) == typeof(string))
                {
                    value = (T)(object)valueFromQueryString.ToString();
                    return true;
                }

                if (typeof(T) == typeof(decimal) && decimal.TryParse(valueFromQueryString, out var valueAsDecimal))
                {
                    value = (T)(object)valueAsDecimal;
                    return true;
                }
            }

            value = default;
            return false;
        }
    }
}
