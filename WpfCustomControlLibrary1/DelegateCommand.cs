using System;

namespace WpfCustomControlLibrary1
{
    /// <summary>
    /// 不需要外部参数输入的<see cref="DelegateCommand{T}"/>
    /// </summary>
    public class DelegateCommand : DelegateCommand<object>
    {
        /// <summary>
        /// Instance of <see cref="DelegateCommand"/>
        /// </summary>
        public DelegateCommand(Action executeMethod) : base(_ => executeMethod()) { }

        /// <summary>
        /// Instance of <see cref="DelegateCommand"/>
        /// </summary>
        public DelegateCommand(Action executeMethod, Func<bool> predicateMethod) : base(_ => executeMethod(), _ => predicateMethod()) { }
    }
}
