using System;

namespace LoongEgg.Core
{
    /// <summary>
    /// 会自动引发可执行改变事件的命令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : DelegateCommand<T>
    {
        /// <summary>
        /// instance of <see cref="ReadOnlyMemory{T}"/>
        /// </summary>
        /// <param name="executeMethod">执行命令的方法</param>
        /// <param name="predicateMethod">判断命令可以执行的方法</param>
        public RelayCommand(Action<T> executeMethod, Predicate<T> predicateMethod) 
            : base(executeMethod, predicateMethod)
        {
            if (predicateMethod == null)
                throw new ArgumentNullException(nameof(predicateMethod));
        }
 
    }
}
