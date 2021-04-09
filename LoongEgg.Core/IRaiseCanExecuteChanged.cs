namespace LoongEgg.Core
{
    /// <summary>
    /// 包含引发可执行改变事件的接口
    /// </summary>
    public interface IRaiseCanExecuteChanged
    {
        /// <summary>
        /// 引发可执行改变事件
        /// </summary>
        void RaiseCanExecuteChanged();
    }
}
