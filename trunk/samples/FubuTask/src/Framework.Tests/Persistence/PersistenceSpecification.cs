using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FluentNHibernate;
using FubuMVC.Core;
using FubuMVC.Core.Util;
using FubuMVC.Framework.NHibernate;
using FubuMVC.Framework.Persistence;
using StructureMap;

namespace FubuMVC.Framework.Tests.Persistence
{
    public class PersistenceSpecification<TEntity, TId> where TEntity : class, new()
    {
        private readonly ArrayList _allProperties = new ArrayList();
        private readonly Func<TEntity, TId> _idFunc;
        private readonly NHibernateUnitOfWork _uow;
        private readonly NHibernateRepository<TId> _repository;
        private readonly ISessionSource _source;

        public PersistenceSpecification(Func<TEntity, TId> idFunc)
        {
            _idFunc = idFunc;
            _source = ObjectFactory.GetInstance<ISessionSource>();
            
            _uow = new NHibernateUnitOfWork(_source);
            _uow.Initialize();
            _repository = new NHibernateRepository<TId>(_uow);
        }

        public PersistenceSpecification<TEntity, TId> CheckProperty<P>(Expression<Func<TEntity, object>> expression, P propertyValue)
        {
            var property = ReflectionHelper.GetProperty(expression);
            _allProperties.Add(new PropertyValue<P>(property, propertyValue));

            return this;
        }

        public PersistenceSpecification<TEntity, TId> CheckReadOnlyProperty<P>(Expression<Func<TEntity, object>> expression,
                                                                    P propertyValue, Action<TEntity, P> setterAction)
        {
            var property = ReflectionHelper.GetProperty(expression);
            _allProperties.Add(new ReadOnlyPropertyValue<P>(property, propertyValue, setterAction));

            return this;
        }

        public PersistenceSpecification<TEntity, TId> CheckReference<P>(Expression<Func<TEntity, object>> expression, P propertyValue)
        {
            _repository.Save(propertyValue);

            var property = ReflectionHelper.GetProperty(expression);
            _allProperties.Add(new PropertyValue<P>(property, propertyValue));

            return this;
        }

        public PersistenceSpecification<TEntity, TId> CheckReadOnlyReference<P, U>(Expression<Func<TEntity, U>> expression,
                                                                        P propertyValue, Action<TEntity, P> setterAction)
        {
            _repository.Save(propertyValue);

            var property = ReflectionHelper.GetProperty(expression);
            _allProperties.Add(new ReadOnlyPropertyValue<P>(property, propertyValue, setterAction));

            return this;
        }

        public PersistenceSpecification<TEntity, TId> CheckReadOnlyImmutableReference<P, U>(Expression<Func<TEntity, U>> expression,
                                                                                 P refValue, Func<TEntity, P, U> setterAction)
        {
            var property = ReflectionHelper.GetProperty(expression);
            _allProperties.Add(new ReadOnlyImmutablePropertyValue<P, U>(property, refValue, setterAction));

            return this;
        }


        public PersistenceSpecification<TEntity, TId> CheckList<LIST>(Expression<Func<TEntity, object>> expression,
                                                           IList<LIST> propertyValue)
        {
            foreach (var item in propertyValue)
            {
                _repository.Save(item);
            }


            var property = ReflectionHelper.GetProperty(expression);
            _allProperties.Add(new ListValue<LIST>(property, propertyValue));

            return this;
        }

        public PersistenceSpecification<TEntity, TId> CheckReadOnlyList<P>(Expression<Func<TEntity, IEnumerable<P>>> getterMethodExpression,
                                                                IList<P> listOfValues,
                                                                Action<TEntity, P> setterAction)
        {
            CheckReadOnlyList(getterMethodExpression, listOfValues, setterAction, p => 0);
            return this;
        }

        public PersistenceSpecification<TEntity, TId> CheckReadOnlyList<P, U>(Expression<Func<TEntity, IEnumerable<U>>> getterMethodExpression,
                                                                   IList<P> listOfValues,
                                                                   Func<TEntity, P, U> setterAction)
        {
            CheckReadOnlyList(getterMethodExpression, listOfValues, setterAction, p => 0);

            return this;
        }

        public PersistenceSpecification<TEntity, TId> CheckReadOnlyList<P>(Expression<Func<TEntity, IEnumerable<P>>> getterMethodExpression,
                                                                IList<P> listOfValues,
                                                                Action<TEntity, P> setterAction,
                                                                Func<P, object> orderBy)
        {
            var getterMethod = ReflectionHelper.GetMethod(getterMethodExpression);
            _allProperties.Add(new ReadOnlyListValue<P, P>(getterMethod, listOfValues, (t, p) =>
            {
                setterAction(t, p);
                return p;
            },
                                                           orderBy));

            return this;
        }

        public PersistenceSpecification<TEntity, TId> CheckReadOnlyList<P, U>(Expression<Func<TEntity, IEnumerable<U>>> getterMethodExpression,
                                                                   IList<P> listOfValues,
                                                                   Func<TEntity, P, U> setterAction,
                                                                   Func<U, object> orderBy)
        {
            var getterMethod = ReflectionHelper.GetMethod(getterMethodExpression);
            _allProperties.Add(new ReadOnlyListValue<P, U>(getterMethod, listOfValues, setterAction, orderBy));

            return this;
        }

        public void VerifyTheMappings()
        {
            // Create the initial copy
            var first = new TEntity();

            // Set the "suggested" properties, including references
            // to other entities and possibly collections
            foreach (var o in _allProperties)
            {
                ((IPropertyValue)o).SetValue(first);
            }

            _repository.Save(first);
            _uow.Commit();
            _uow.Dispose();
            
            using( var newUow = new NHibernateUnitOfWork(_source) )
            {
                // Get a completely different IRepository
                newUow.Initialize();
                var secondRepository = new NHibernateRepository<TId>(newUow);

                // "Find" the same entity from the second IRepository
                var second = secondRepository.Load<TEntity>(_idFunc(first));

                // Validate that each specified property and value
                // made the round trip
                // It's a bit naive right now because it fails on the first failure
                foreach (var o in _allProperties)
                {
                    ((IPropertyValue)o).CheckValue(second);
                }
            }
        }

        #region Nested type: ListValue

        internal class ReadOnlyListValue<P, ITEM> : IPropertyValue
        {
            private readonly Func<TEntity, P, ITEM> _setterAction;
            private readonly MethodInfo _method;
            private readonly IList<P> _refValues;
            private readonly IList<ITEM> _expected = new List<ITEM>();
            private readonly Func<ITEM, object> _orderBy;

            public ReadOnlyListValue(MethodInfo method, IList<P> refValues, Func<TEntity, P, ITEM> setterAction, Func<ITEM, object> orderBy)
            {
                _method = method;
                _refValues = refValues;
                _setterAction = setterAction;
                _orderBy = orderBy;
            }

            public void SetValue(object target)
            {
                try
                {
                    foreach (var refValue in _refValues)
                    {
                        var item = _setterAction((TEntity)target, refValue);
                        _expected.Add(item);
                    }
                }
                catch (Exception e)
                {
                    var message = "Error while trying to add item to read-only collection " + _method.Name;
                    throw new ApplicationException(message, e);
                }
            }

            public void CheckValue(object target)
            {
                var actual = ((IEnumerable<ITEM>)_method.Invoke(target, null)).ToList();

                var actualSorted = actual.OrderBy(_orderBy).ToList();
                var expectedSorted = _expected.OrderBy(_orderBy).ToList();

                ListValue<ITEM>.AssertGenericListMatches(actualSorted, expectedSorted, "read only list {0}".ToFormat(_method.Name));
            }
        }

        internal class ListValue<ITEM> : PropertyValue<IList<ITEM>>
        {
            private readonly IList<ITEM> _expected;

            public ListValue(PropertyInfo property, IList<ITEM> propertyValue)
                : base(property, propertyValue)
            {
                _expected = propertyValue;
            }

            public override void CheckValue(object target)
            {
                var actual = ((IEnumerable<ITEM>)_property.GetValue(target, null)).ToList();

                AssertGenericListMatches(actual, _expected, "list {0}".ToFormat(_property.Name));
            }

            public static void AssertGenericListMatches<LIST>(IList<LIST> actual, IList<LIST> expected, string context)
            {
                var list = new ArrayList(actual.ToArray());

                if (expected.Count != list.Count)
                {
                    throw new ApplicationException("While comparing {0}, the counts between actual ({1}) and expected ({2}) do not match"
                                                       .ToFormat(context, actual.Count, expected.Count));
                }


                for (var i = 0; i < expected.Count; i++)
                {
                    object expectedValue = expected[i];
                    var actualValue = actual[i];
                    if (!expectedValue.Equals(actualValue))
                    {
                        var message =
                            string.Format(
                                "Expected '{0}' but got '{1}' at position {2}",
                                expectedValue,
                                actualValue, i);

                        throw new ApplicationException(message);
                    }
                }
            }
        }

        #endregion

        #region Nested type: ReadOnlyPropertyValue

        public class ReadOnlyPropertyValue<P> : PropertyValue<P>
        {
            private readonly Action<TEntity, P> _setterAction;

            public ReadOnlyPropertyValue(PropertyInfo property, P propertyValue, Action<TEntity, P> setterAction)
                : base(property, propertyValue)
            {
                _setterAction = setterAction;
            }

            public override void SetValue(object target)
            {

                _setterAction((TEntity)target, _propertyValue);
            }
        }

        public class ReadOnlyImmutablePropertyValue<P, U> : IPropertyValue
        {
            private readonly Func<TEntity, P, U> _setterAction;
            private U _propertyValue;
            private readonly P _refValue;
            private readonly PropertyInfo _property;

            public ReadOnlyImmutablePropertyValue(PropertyInfo property, P refValue, Func<TEntity, P, U> setterAction)
            {
                _property = property;
                _refValue = refValue;
                _setterAction = setterAction;
            }

            public void SetValue(object target)
            {
                _propertyValue = _setterAction((TEntity)target, _refValue);
            }

            public void CheckValue(object target)
            {
                var propertyValue = _property.GetValue(target, null);
                PropertyValue<U>.AssertEqual(_property.Name, _propertyValue, propertyValue);
            }
        }

        #endregion
    }

    public interface IPropertyValue
    {
        void SetValue(object target);
        void CheckValue(object target);
    }

    public class PropertyValue<T> : IPropertyValue
    {
        protected readonly PropertyInfo _property;
        protected readonly T _propertyValue;

        internal PropertyValue(PropertyInfo property, T propertyValue)
        {
            _property = property;
            _propertyValue = propertyValue;
        }

        #region IPropertyValue Members

        public virtual void SetValue(object target)
        {
            try
            {
                _property.SetValue(target, _propertyValue, null);
            }
            catch (Exception e)
            {
                var message = "Error while trying to set property " + _property.Name;
                throw new ApplicationException(message, e);
            }
        }

        public virtual void CheckValue(object target)
        {
            AssertEqual(_property.Name, _propertyValue, _property.GetValue(target, null));
        }

        public static void AssertEqual(string name, T first, object second)
        {
            var actual = (T)Convert.ChangeType(second, typeof(T));

            if (!first.Equals(actual))
            {
                var message =
                    string.Format(
                        "Expected '{0}' but got '{1}' for Property '{2}'",
                        first,
                        actual,
                        name);

                throw new ApplicationException(message);
            }
        }

        #endregion
    }
}