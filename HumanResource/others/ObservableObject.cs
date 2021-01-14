using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HumanResource
{
    /// <summary>
    /// 通知属性更改 基类，ViewModel需要通知界面时，需要继承<see cref="INotifyPropertyChanged"/>这个接口
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        /// <summary>
        /// 通知属性更改
        /// </summary>
        /// <param name="propertyName"></param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// 通知属性更改，每次触发不需要添加名字参数
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            RaisePropertyChanged(propertyName);
        }
        /// <summary>
        /// 通知属性更改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Field"></param>
        /// <param name="Value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected bool SetProperty<T>(ref T Field, T Value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(Field, Value))
            {
                return false;
            }
            Field = Value;

            RaisePropertyChanged(propertyName);

            return true;
        }
        /// <summary>
        /// 通知类里所有属性
        /// </summary>
        public void RaiseAllChanged()
        {
            RaisePropertyChanged("");
        }
    }
}
