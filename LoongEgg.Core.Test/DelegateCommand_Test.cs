using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace LoongEgg.Core.Test
{
    [TestClass]
    public class DelegateCommand_Test
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_ThrowException_IfExecuteMethodIsNull()
        {
            var command = new DelegateCommand<object>(null);

            Assert.IsNull(command);
        }

        string _Parameter;
        void ExecuteMethod(string p)
        {
            _Parameter = p;
        }

        [TestMethod]
        public void ExecuteMethod_CanExecuteWithParameterInput()
        {
            var command = new DelegateCommand<string>(s => ExecuteMethod(s));

            _Parameter = "havn't raised";
            command.Execute("raised");

            Assert.AreEqual("raised", _Parameter);
        }

        [TestMethod]
        public void PredicateMethod_CanPredicateWithParameterInput()
        {
            var command = new DelegateCommand<string>(s => ExecuteMethod(s), s => s.ToLower() == "canExecute".ToLower());


            Assert.IsFalse(command.CanExecute("tryExecute"));
            Assert.IsTrue(command.CanExecute("canExecute"));
        }

        [TestMethod]
        public void AlwaysCanExecute_IfNotDefinePredicateMethod()
        {
            int executeCount = 0;

            var command = new DelegateCommand<object>((s) => executeCount++);

            Assert.IsTrue(command.CanExecute(null));

            command.Execute(null);
            command.Execute(null);
            command.Execute(null);

            Assert.AreEqual(3, executeCount);
        }

       
        bool PredicateMethod(string s) => s.ToLower() == "canExecute".ToLower();
        DelegateCommand<string>StubCommand;
        [TestMethod]
        public void CanExecuteChanged_CanRaisedManually()
        {
            StubCommand = new DelegateCommand<string>(ExecuteMethod, PredicateMethod);
            bool isCanExecuteChangedRaised = false;
            StubCommand.CanExecuteChanged += (s, e) =>
            {
                isCanExecuteChangedRaised = true;
            };

            _Parameter = "havn't raised";
            StubCommand.Execute("raised");
            StubCommand.RaiseCanExecuteChanged();

            Assert.IsTrue(isCanExecuteChangedRaised);
        }

        void ExecuteAndRaiseCanExecuteChanged()
        {
            ExecuteMethod(_Parameter);
            AutoRaisedCommand?.RaiseCanExecuteChanged();
        }
        bool PredicateMethod_WithoutParameter() => PredicateMethod(_Parameter);
        DelegateCommand AutoRaisedCommand;
        [TestMethod]
        public void CanExecuteChanged_CanRaisedInExecuteMethodAuto()
        {
            AutoRaisedCommand = new DelegateCommand(ExecuteAndRaiseCanExecuteChanged, PredicateMethod_WithoutParameter);
            bool isCanExecuteChangedRaised = false;
            int raisedCount = 0;
            AutoRaisedCommand.CanExecuteChanged += (s, e) =>
            {
                isCanExecuteChangedRaised = true;
                raisedCount++;
            };

            _Parameter = "havn't raised";
            ExecuteAndRaiseCanExecuteChanged();
            Assert.IsFalse(AutoRaisedCommand.CanExecute(null));

            _Parameter = "canExecute";
            ExecuteAndRaiseCanExecuteChanged();
            Assert.IsTrue(AutoRaisedCommand.CanExecute(null));

            Assert.IsTrue(isCanExecuteChangedRaised);
            Assert.AreEqual(2, raisedCount);
        }
    }
}
