using Stylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalensSDK.StyletEx
{
    public class KeyOneActive<T> : Stylet.Conductor<T>.Collection.OneActive, IInvoke where T : class, IInvoke
    {
        #region IInvoke 实现
        public string ID { get; set; }

        public event Action<InvokeParameter> InvokeEvent;

        public virtual void BeforeInvoke(InvokeParameter parameter)
        {
        }

        public virtual void AfterInvoke(InvokeParameter parameter)
        {
        }

        public virtual void Reset(InvokeParameter parameter)
        {
        }
        #endregion       

        /// <summary>
        /// 注册项
        /// </summary>
        /// <param name="obj"></param>
        protected void RegisterItem(T obj)
        {
            this.Items.Add(obj);
            // 注册事件
            obj.InvokeEvent += InvokeTo;
        }

        protected void RegisterItem<T1>() where T1 : T, new()
        {
            T obj = new T1();
            RegisterItem(obj);
        }

        /// <summary>
        /// 在此处调用其它项
        /// </summary>
        /// <param name="parameter"></param>
        protected void InvokeTo(InvokeParameter parameter)
        {
            if (parameter == null || string.IsNullOrEmpty(parameter.InvokeId)) return;

            T obj = this.Items.Where(item => item.ID == parameter.InvokeId).FirstOrDefault();
            if (obj == null) return;

            // 调用
            obj.BeforeInvoke(parameter);
            this.ActivateItem(obj);
            obj.AfterInvoke(parameter);            
        }
    }
}
