using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoongEgg.Core.Test
{
    [TestClass]
    public class IoC_Test
    {
        class StubClass : BindableObject
        {

            public string StubProperty
            {
                get { return _StubProperty; }
                set { SetProperty(ref _StubProperty, value); }
            }
            private string _StubProperty;

        }

        [TestMethod]
        public void IoC_CanAddAndGetInstance()
        {
            var instance_1 = new StubClass { StubProperty = "property of instance 1" };
            IoC.Container.AddOrUpdate(instance_1);

            Assert.IsNotNull(IoC.Container);
            Assert.IsTrue(IoC.Container.Instances.ContainsKey(typeof(StubClass)));
            Assert.ReferenceEquals(instance_1, IoC.Container.Get<StubClass>());
            Assert.AreEqual("property of instance 1", IoC.Container.Get<StubClass>().StubProperty);
        }

        [TestMethod]
        public void IoC_CanUpdateInstanceLikeSingleton()
        {
            var instance_1 = new StubClass { StubProperty = "property of instance 1" };
            var instance_2 = new StubClass { StubProperty = "property of instance 2" };
            IoC.Container.AddOrUpdate(instance_1);
            IoC.Container.AddOrUpdate(instance_2);

            Assert.IsNotNull(IoC.Container);
            Assert.IsTrue(IoC.Container.Instances.ContainsKey(typeof(StubClass)));
            Assert.ReferenceEquals(instance_2, IoC.Container.Get<StubClass>());
            Assert.AreEqual("property of instance 2", IoC.Container.Get<StubClass>().StubProperty);
            Assert.AreEqual(1, IoC.Container.Instances.Count);
        }
    }
}
