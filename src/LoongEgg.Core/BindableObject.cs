using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LoongEgg.Core
{
    /// <summary>
    /// �ɰ󶨶���Ļ���
    /// </summary>
    public abstract class BindableObject : INotifyPropertyChanged
    {
        /// <summary>
        /// ��������"��"����ֵ, ������ɵ�ֵ��һ�������<see cref="RaisePropertyChanged(string)"/>
        /// </summary>
        /// <typeparam name="T">���Ե�����(�Զ��ж�)</typeparam>
        /// <param name="target">�����õ�����</param>
        /// <param name="value">�������"��"ֵ</param>
        /// <param name="propertyName">��������(�Զ���ȡ)</param>
        /// <returns>true: ������Ϊ��ֵ; false: δ����.</returns>
        protected bool SetProperty<T>(
            ref T target, T value,
            [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(target, value))
                return false;

            target = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// ����<see cref="PropertyChanged"/>
        /// </summary>
        /// <param name="propertyName">Ҫ�������Ըı��¼������Ե�����</param>
        public void RaisePropertyChanged(string propertyName)
          => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// ���Ըı��¼�, �ⲿ��֪�����Ըı���, ��ʹ��"+="�����¼������� 
        /// ������<see cref="PropertyChangedEventArgs.PropertyName"/>�ж����ĸ�����
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
