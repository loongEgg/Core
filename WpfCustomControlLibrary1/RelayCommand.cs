using System;
using System.Windows.Input;

namespace WpfCustomControlLibrary1
{
    /// <summary>
    /// 自动定期查询可执行状态并引发<see cref="CanExecuteChanged"/>的命令
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand
    {
        /// <summary>
        /// 像鼠标移动事件一样会被定期检查
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

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
        /// Instance of <see cref="RelayCommand{T}"/>
        /// </summary>
        /// <param name="executeMethod"><see cref="_ExcuteMethod"/></param>
        public RelayCommand(Action<T> executeMethod) : this(executeMethod, null) { }

        /// <summary>
        /// Instance of <see cref="RelayCommand{T}"/>
        /// </summary>
        /// <param name="executeMethod"><see cref="_ExcuteMethod"/></param>
        /// <param name="predicateMethod"><see cref="_PredicateMethod"/></param>
        public RelayCommand(Action<T> executeMethod, Predicate<T> predicateMethod)
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
                _ExcuteMethod((T)parameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                IsExecuting = false;
            }
        }

    }
}
