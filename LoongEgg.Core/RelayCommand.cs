using System;

namespace LoongEgg.Core
{
    /// <summary>
    /// 不需要参数的Relay command
    /// </summary>
    public class RelayCommand : DelegateCommand
    {
        /// <summary>
        /// instance of <see cref="RelayCommand"/>
        /// </summary>
        /// <param name="executeMethod">执行命令的方法</param>
        /// <param name="predicateMethod">判断命令可以执行的方法</param>
        public RelayCommand(Action executeMethod, Func<bool> predicateMethod) : base(executeMethod, predicateMethod)
        {
            if (predicateMethod == null)
                throw new ArgumentNullException(nameof(predicateMethod));
        }
    }
}
