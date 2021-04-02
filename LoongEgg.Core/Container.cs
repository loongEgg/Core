using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace LoongEgg.Core
{
    /// <summary>
    /// Container for object instances
    /// </summary>
    public class Container
    {
        /// <summary>
        /// 线程安全的<seealso cref="Dictionary{TKey, TValue}"/>
        /// </summary>
        public ConcurrentDictionary<string, object> Instances => _Instances;

        private readonly ConcurrentDictionary<string, object> _Instances = new ConcurrentDictionary<string, object>();

        internal Container() { }

        /// <summary>
        /// Add or update the specify instance
        /// </summary>
        /// <typeparam name="T">type of instance</typeparam>
        /// <param name="instance">instance to add or update</param>
        public void AddOrUpdate<T>(T instance)
        {
            Type type = typeof(T);

            lock (this)
            {
                if (_Instances.ContainsKey(type.FullName))
                    _Instances[type.FullName] = instance;
                else
                    _Instances.TryAdd(type.FullName, instance);
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
            if (_Instances.ContainsKey(t.FullName))
                return (T)_Instances[t.FullName];

            else throw new ArgumentOutOfRangeException($"未创建类型 {t.FullName} 的实例");
        }

        public T Get<T>(string assemblyName, string typeName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName);
            if (File.Exists(path))
            {
                Assembly assembly = Assembly.LoadFile(path);
                Type type = assembly.GetType(typeName, true, true);
                if (_Instances.ContainsKey(type.FullName))
                    return (T)_Instances[type.FullName];
                else throw new ArgumentOutOfRangeException($"未创建类型 {type.FullName} 的实例");
            }
            else
            {
                throw new FileNotFoundException($"{assemblyName}");
            }
        }
    }
}
