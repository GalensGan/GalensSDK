using LiteDB;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalensSDK.LiteDbEx
{
    /// <summary>
    /// BsonDocument 与 datatable 的相互转换
    /// </summary>
    public static class BsonDoc_Datatable
    {
        /// <summary>
        /// BsonDocument 转换成 datatable
        /// </summary>
        /// <param name="bdocs"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this IEnumerable<BsonDocument> bdocs)
        {
            if (bdocs == null || bdocs.Count() ==0)
            {
                return new DataTable();
            }

            List<string> keys = new List<string>();
            foreach(BsonDocument bdoc in bdocs)
            {
                keys.AddRange(bdoc.Keys);
            }

            // 对 keys 对重
            keys = keys.Distinct().ToList();

            var dt = new DataTable();
            dt.Columns.AddRange(keys.ConvertAll(key=> new DataColumn(key)).ToArray());

            // 添加具体的元素
            foreach(var bdoc in bdocs)
            {
                ArrayList array = new ArrayList();
                foreach(var key in keys)
                {
                    if (bdoc.TryGetValue(key, out BsonValue value))
                    {
                        array.Add(value.ToString());
                    }
                    else array.Add(string.Empty);
                }

                dt.LoadDataRow(array.ToArray(), true);
            }

            return dt;
        }
    }
}
