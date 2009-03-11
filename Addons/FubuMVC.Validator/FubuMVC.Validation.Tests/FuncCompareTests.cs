using System;
using System.Linq.Expressions;
using System.Reflection;
using FubuMVC.Tests;
using FubuMVC.Validation.DSL;
using NUnit.Framework;

namespace FubuMVC.Validation.Tests
{
    [TestFixture]
    public class FuncCompareTests
    {
        private Expression<Func<PropertyInfo, bool>> _func1;
        private Expression<Func<PropertyInfo, bool>> _func2;
        private Expression<Func<PropertyInfo, bool>> _func3;
        private Expression<Func<PropertyInfo, bool>> _func4;

        private UglyStringComparer _uglyStringComparer;

        [SetUp]
        public void SetUp()
        {
            _func1 = p => p.Name.StartsWith("test") && p.Name != "test123";
            _func2 = x => x.Name.StartsWith("test") && x.Name != "test123";
            _func3 = p => p.Name.StartsWith("testing");
            _func4 = p => !p.Name.StartsWith("test");

            _uglyStringComparer = new UglyStringComparer();
        }
    
        [Test]
        public void Both_func1_and_func2_should_be_equal()
        {
            _uglyStringComparer.Compare(_func1, _func2).ShouldBeTrue();
        }

        [Test]
        public void func1_and_func3_should_not_be_equal()
        {
            _uglyStringComparer.Compare(_func1, _func3).ShouldBeFalse();
        }

        [Test]
        public void func1_and_func4_should_not_be_equal()
        {
            _uglyStringComparer.Compare(_func1, _func4).ShouldBeFalse();
        }
    }
}