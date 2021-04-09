namespace LoongEgg.Core
{
    /// <summary>
    /// 依赖注入工具类
    /// </summary>
    public static class IoC
    {
        /// <summary>
        /// 实例容器
        /// </summary>
        public static Container Container { get; } = new Container();
    }
}
