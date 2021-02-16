using System;
using System.Windows.Input;

namespace LoongEgg.Core
{
    /// <summary>
    /// 需要手动引发<see cref="CanExecuteChanged"/>的命令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateCommand<T> : ICommand
    {
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
        /// <param name="executeMethod"><see cref="_PredicateMethod"/></param>
        public DelegateCommand(Action<T> executeMethod, Predicate<T> predicateMethod)
        {
            if (executeMethod == null) throw new ArgumentNullException();
            _ExcuteMethod = executeMethod;
            _PredicateMethod = predicateMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (_PredicateMethod == null)
                return true;

            return !IsExecuting && _PredicateMethod((T)parameter);
        }

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
        /// 手动引发<see cref="CanExecuteChanged"/>
        /// </summary>
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
