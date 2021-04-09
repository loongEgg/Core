using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfCustomControlLibrary1
{
    /// <summary>
    /// 可绑定对象的基类
    /// </summary>
    public abstract class BindableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// 尝试设置"新"的数值, 如果跟旧的值不一样会调用<see cref="RaisePropertyChanged(string)"/>
        /// </summary>
        /// <typeparam name="T">属性的类型(自动判断)</typeparam>
        /// <param name="target">待设置的属性</param>
        /// <param name="value">待赋予的"新"值</param>
        /// <param name="propertyName">属性名称(自动获取)</param>
        /// <returns>true: 已设置为新值; false: 未设置.</returns>
        protected bool SetProperty<T>(
            ref T target, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(target, value))
                return false;

            target = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// 引发<see cref="PropertyChanged"/>
        /// </summary>
        /// <param name="propertyName">要引发属性改变事件的属性的名称</param>
        public void RaisePropertyChanged(string propertyName)
          => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// 属性改变事件, 外部想知道属性改变了, 就使用"+="新增一个观察点, 
        /// 并根据<see cref="PropertyChangedEventArgs.PropertyName"/>判断是哪个属性
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
