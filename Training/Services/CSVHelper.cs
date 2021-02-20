using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Training.Services
{
    public class CSVHelper
    {
        public List<string> GetCSVLines(string file)
        {
            if (File.Exists(file))
            {
                return File.ReadLines(file).ToList();
            }
            return null;
        }

        public List<T> GetData<T>(string file)
        {
            var list = new List<T>();
            var lines = GetCSVLines(file);
            if (lines!=null && lines.Count > 0)
            {
                var headerLine = lines.First();
                var columnNames = headerLine.Split(',');
                var rows = lines.Skip(1);
                var properties = typeof(T).GetProperties();
                rows.ToList().ForEach(r =>
                {
                    var cells = r.Split(",");
                    T obj = (T) Activator.CreateInstance(typeof(T));
                    var index = 0;
                    foreach (var columnName in columnNames)
                    {
                        var prop = properties.SingleOrDefault(p => p.Name.Equals(columnName));
                        var propertyType = prop.PropertyType;
                        var value = cells[index++];
                        prop.SetValue(obj, Convert.ChangeType(value, propertyType));
                    }
                    list.Add(obj);
                });
            }
            return list;
        }
    }
}