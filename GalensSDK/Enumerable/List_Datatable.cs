using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GalensSDK.Enumerable
{
    public static class List_Datatable
    {
        /// <summary>
        /// list to datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static DataTable ConvertToDt<T>(this IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new
            DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }


        /// <summary>
        /// datatable to list
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> ConvertToModel<T>(this DataTable dt) where T: new()
        {

            List<T> ts = new List<T>();// 定义集合
            Type type = typeof(T); // 获得此模型的类型            
            foreach (DataRow dr in dt.Rows)
            {                
                ts.Add(dr.ConvertToModel<T>());
            }
            return ts;
        }

        /// <summary>
        /// 将 dataRow 转成实体类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="row"></param>
        /// <returns></returns>
        public static T ConvertToModel<T> (this DataRow row) where T : new()
        {
            T t = new T();
            PropertyInfo[] propertys = t.GetType().GetProperties();// 获得此模型的公共属性
            foreach (PropertyInfo pi in propertys)
            {
                string tempName = pi.Name;
                if (row.Table.Columns.Contains(tempName))
                {
                    if (!pi.CanWrite) continue;
                    object value = row[tempName];
                    if (value != DBNull.Value)
                        pi.SetValue(t, value, null);
                }
            }
            return t;
        }

        /// <summary>
        /// 从实体类中拷贝数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataRow"></param>
        /// <param name="arg"></param>
        public static void CopyFrom<T>(this DataRow row, T model)
        {
            PropertyInfo[] propertys = model.GetType().GetProperties();// 获得此模型的公共属性
            foreach (PropertyInfo pi in propertys)
            {
                string tempName = pi.Name;
                if (row.Table.Columns.Contains(tempName))
                {
                    if (!pi.CanRead) continue;
                    row[tempName] = pi.GetValue(model);
                }
            }
        }
    }
}
