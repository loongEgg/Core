using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit.Sdk;

namespace LoongEgg.Core.Test
{
    [TestClass]
    public class BindableObject_Test
    {
        [TestMethod]
        public void BindableObject_IsAbstract()
        {
            Assert.IsTrue(typeof(BindableObject).IsAbstract);
        }


        class StubViewModel : BindableObject
        { 
            public double StubProperty
            {
                get { return _StubProperty; }
                set { SetProperty(ref _StubProperty, value); }
            }
            private double _StubProperty;

        }

        [TestMethod]
        public void PropertyChanged_CanRaised()
        {
            var stubVM = new StubViewModel();

            bool isRaised = false;
            stubVM.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(StubViewModel.StubProperty))
                    isRaised = true;
            };

            stubVM.StubProperty = 10;
            Assert.IsTrue(isRaised);
        }
    }
}
