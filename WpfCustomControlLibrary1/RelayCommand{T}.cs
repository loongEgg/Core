using System;

namespace WpfCustomControlLibrary1
{
    /// <summary>
    /// 不需要外部参数输入的<see cref="RelayCommand{T}"/>
    /// </summary>
    public class RelayCommand : RelayCommand<object>
    {
        /// <summary>
        /// Instance of <see cref="RelayCommand"/>
        /// </summary> 
        /// <param name="executeMethod"><see cref="RelayCommand{T}."/></param>
        public RelayCommand(Action executeMethod) : base(_ => executeMethod()) { }

        /// <summary>
        /// Instance of <see cref="RelayCommand"/>
        /// </summary> 
        public RelayCommand(Action executeMethod, Func<bool> predicateMethod) : base(_ => executeMethod(), _ => predicateMethod()) { }

    }
}
