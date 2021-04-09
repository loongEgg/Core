using System;
using System.Windows.Input;

namespace LoongEgg.Core
{
    /// <summary>
    /// 需要手动引发<see cref="CanExecuteChanged"/>的命令
    /// </summary>
    /// <typeparam name="T">命令参数类型</typeparam>
    public class DelegateCommand<T> : ICommand
    {
        /// <summary>
        /// 可执行改变事件的处理器
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 负责执行命令的方法
        /// </summary>
        private readonly Action<T> _ExcuteMethod;

        /// <summary>
        /// 负责判断命令是否可以执行的方向
        /// </summary>
        private readonly Predicate<T> _PredicateMethod;

        /// <summary>
        /// 命令正在执行的标志
        /// </summary>
        public bool IsExecuting { get; private set; } = false;

        /// <summary>
        /// Instance of <see cref="DelegateCommand{T}"/>
        /// </summary>
        /// <param name="executeMethod"><see cref="_ExcuteMethod"/></param>
        public DelegateCommand(Action<T> executeMethod) : this(executeMethod, null) { }

        /// <summary>
        /// Instance of <see cref="DelegateCommand{T}"/>
        /// </summary>
        /// <param name="executeMethod">负责执行的方法</param>
        /// <param name="predicateMethod">负责检测是否可以执行的方法</param>
        public DelegateCommand(Action<T> executeMethod, Predicate<T> predicateMethod)
        {
            if (executeMethod == null)
                throw new ArgumentNullException();
            _ExcuteMethod = executeMethod;
            _PredicateMethod = predicateMethod;
        }

        /// <summary>
        /// 可执行判断
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>[true]: 可执行; [false]: 不可执行</returns>
        public virtual bool CanExecute(object parameter)
        {
            if (_PredicateMethod == null)
                return true;

            return !IsExecuting && _PredicateMethod((T)parameter);
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="parameter">参数</param>
        public void Execute(object parameter)
        {
            IsExecuting = true;
            try
            {
                RaiseCanExecuteChanged();
                _ExcuteMethod((T)parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// 引发<see cref="CanExecuteChanged"/>
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
