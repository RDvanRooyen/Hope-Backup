using BlazorTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebUI.Data.Models
{
    /// <summary>
    /// Defines extension methods for BlazorTables
    /// </summary>
    public static class BlazorTableExtension
    {
        /// <summary>
        /// Puts the data from this table into CSV format
        /// </summary>
        /// <returns>a UTF-8-encoded byte array of the complete CSV contents</returns>
        public static byte[] ToCsv<T>(this ITable<T> dataTable)
        {
            var cols = dataTable.Columns;
            var rows = dataTable.Items;

            if (cols == null || rows == null)
            {
                throw new ArgumentException("ToCsv requires a table with column definitions and populated data");
            }

            StringBuilder csv = new StringBuilder();

            List<string> colHeaders = cols.Select(c => $@"""{c.Title}""").ToList();
            csv.AppendLine(String.Join(",", colHeaders));

            foreach (T row in dataTable.Items)
            {
                foreach (var col in cols)
                {
                    // get the cell value
                    var val = col.Field.Compile().Invoke(row);

                    // format if necessary
                    if (col.Format != null)
                    {
                        val = String.Format($"{{0:{col.Format}}}", val);
                    }

                    // wrap in quotes
                    csv.Append('"').Append(val).Append('"').Append(",");
                }

                // next row
                csv.AppendLine();
            }

            // encode in UTF-8
            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return bytes;
        }
    }
}
