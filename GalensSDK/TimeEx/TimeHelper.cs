using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalensSDK.TimeEx
{
    public class TimeHelper
    {
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static long TimestampNow()
        {
            return DateTime.Now.ConvertToTimestamp();
        }
    }
}
