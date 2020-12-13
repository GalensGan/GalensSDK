using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalensSDK.DatatableEx
{
    public static class DtEx
    {
        public static List<string> GetColumnNames(this DataTable datatable)
        {
            if (datatable == null) return new List<string>();

            List<string> names = new List<string>();

            foreach (DataColumn col in datatable.Columns)
            {
                names.Add(col.ColumnName);
            }
            return names;
        }

        /// <summary>
        /// 获取字符列的列名
        /// </summary>
        /// <param name="datatable"></param>
        /// <returns></returns>
        public static List<string> GetColumnNamesOfStringColumn(this DataTable datatable)
        {
            if (datatable == null) return new List<string>();

            List<string> names = new List<string>();

            foreach (DataColumn col in datatable.Columns)
            {
                if (col.DataType == typeof(string)) names.Add(col.ColumnName);
            }
            return names;
        }
    }
}
