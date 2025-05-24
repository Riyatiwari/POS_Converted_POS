using System;
using System.Collections.Generic;
using System.Data;

namespace Converted_POS.Extensions
{
    public static class DataTableExtensions
    {
        /// <summary>
        /// Converts a DataTable to an enumerable collection of DataRow objects
        /// </summary>
        public static IEnumerable<DataRow> AsEnumerable(this DataTable dataTable)
        {
            if (dataTable == null)
                throw new ArgumentNullException(nameof(dataTable));

            foreach (DataRow row in dataTable.Rows)
            {
                yield return row;
            }
        }

        /// <summary>
        /// Gets the number of rows in a DataTable, returns 0 if the DataTable is null
        /// </summary>
        public static int Count(this DataTable dataTable)
        {
            return dataTable?.Rows.Count ?? 0;
        }
        
        /// <summary>
        /// Converts DataTable to a list of objects of type T
        /// </summary>
        public static List<T> ToList<T>(this DataTable dataTable) where T : new()
        {
            var list = new List<T>();
            
            if (dataTable == null)
                return list;
                
            var properties = typeof(T).GetProperties();
            
            foreach (DataRow row in dataTable.Rows)
            {
                var item = new T();
                
                foreach (var property in properties)
                {
                    if (dataTable.Columns.Contains(property.Name))
                    {
                        var value = row[property.Name];
                        if (value != DBNull.Value)
                        {
                            property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                        }
                    }
                }
                
                list.Add(item);
            }
            
            return list;
        }
    }
} 