using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace WpfCustomControlLibrary1
{
    /// <summary>
    /// Container for object instances
    /// </summary>
    public class Container
    {
        /// <summary>
        /// 线程安全的<seealso cref="Dictionary{TKey, TValue}"/>
        /// </summary>
        public ConcurrentDictionary<Type, object> Instances => _Instances;

        private readonly ConcurrentDictionary<Type, object> _Instances = new ConcurrentDictionary<Type, object>();

        internal Container() { }

        /// <summary>
        /// Add or update the specify instance
        /// </summary>
        /// <typeparam name="T">type of instance</typeparam>
        /// <param name="instance">instance to add or update</param>
        public void AddOrUpdate<T>(T instance)
        {
            Type type = instance.GetType();

            lock (this)
            {
                if (_Instances.ContainsKey(type))
                    _Instances[type] = instance;
                else
                    _Instances.TryAdd(type, instance);
            }
        }

        /// <summary>
        /// 获取指定类型的实例
        /// </summary>
        /// <typeparam name="T">type of instance to get</typeparam>
        /// <returns>Instance of specify type</returns>
        public T Get<T>()
        {
            var t = typeof(T);
            if (_Instances.ContainsKey(t))
                return (T)_Instances[t];

            else throw new ArgumentOutOfRangeException($"未创建类型 {t.FullName} 的实例");
        }

        public object Get(Type type)
        {  
            if (_Instances.ContainsKey(type))
                return _Instances[type];
            else throw new ArgumentOutOfRangeException($"未创建类型 {type.FullName} 的实例"); 
        }
    }
}
