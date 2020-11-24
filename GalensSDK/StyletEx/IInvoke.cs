using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalensSDK.StyletEx
{
    public interface IInvoke
    {
        string ID { get; set; }

        /// <summary>
        /// 调用前
        /// </summary>
        void BeforeInvoke(InvokeParameter parameter);

        /// <summary>
        /// 调用后
        /// </summary>
        void AfterInvoke(InvokeParameter parameter);

        /// <summary>
        /// 要在Invoke中调用
        /// </summary>
        event Action<InvokeParameter> InvokeEvent;

        void Reset(InvokeParameter parameter);
    }
}
