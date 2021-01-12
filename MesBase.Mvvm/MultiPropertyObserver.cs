using MesBase;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Windows;

namespace MesBase.Mvvm
{
    public class MultiPropertyObserver<TPropertySource> : IWeakEventListener
        where TPropertySource : class, INotifyPropertyChanged, IIdentifiable
    {
        #region Constructor

        public MultiPropertyObserver( )
        {
            _idToPropertyNameToHandlerMapMap = new ConcurrentDictionary<string, IDictionary<string, Action<TPropertySource>>>();
            _idToReferenceMap = new ConcurrentDictionary<string, WeakReference<TPropertySource>>();
        }

        #endregion // Constructor

        #region Public Methods

        #region RegisterHandler

        public MultiPropertyObserver<TPropertySource> RegisterHandler(
            TPropertySource source,
            Expression<Func<TPropertySource, object>> expression,
            Action<TPropertySource> handler)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            string propertyName = GetPropertyName(expression);
            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException("'expression' did not provide a property name.");

            if (handler == null)
                throw new ArgumentNullException("handler");

            if (!_idToPropertyNameToHandlerMapMap.ContainsKey(source.Id))
            {
                _idToPropertyNameToHandlerMapMap[source.Id] = new Dictionary<string, Action<TPropertySource>>();
                _idToReferenceMap[source.Id] = new WeakReference<TPropertySource>(source);
            }

            TPropertySource propertySource = this.GetPropertySource(source.Id);
            if (propertySource != null)
            {
                _idToPropertyNameToHandlerMapMap[source.Id][propertyName] = handler;
                PropertyChangedEventManager.AddListener(propertySource, this, propertyName);
            }

            return this;
        }

        #endregion // RegisterHandler

        #region UnregisterHandler

        public MultiPropertyObserver<TPropertySource> UnregisterHandler(
             string id,
             Expression<Func<TPropertySource, object>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            string propertyName = GetPropertyName(expression);
            if (String.IsNullOrEmpty(propertyName))
                throw new ArgumentException("'expression' did not provide a property name.");

            INotifyPropertyChanged propertySource = this.GetPropertySource(id);
            if (propertySource != null)
            {
                if (_idToPropertyNameToHandlerMapMap[id].ContainsKey(propertyName))
                {
                    _idToPropertyNameToHandlerMapMap[id].Remove(propertyName);
                    PropertyChangedEventManager.RemoveListener(propertySource, this, propertyName);
                }
            }

            return this;
        }

        public MultiPropertyObserver<TPropertySource> UnregisterHandlers(string id)
        {
            TPropertySource propertySource = this.GetPropertySource(id);
            if (propertySource != null)
            {
                var query = from map in _idToPropertyNameToHandlerMapMap
                            where map.Key == propertySource.Id
                            select map;
                
                if (_idToPropertyNameToHandlerMapMap.ContainsKey(id))
                {
                    var dic = _idToPropertyNameToHandlerMapMap[id];
                    foreach(var propertyName in dic.Keys)
                    {
                        PropertyChangedEventManager.RemoveListener(propertySource, this, propertyName);
                    }
                    _idToPropertyNameToHandlerMapMap.Remove(id);
                    _idToReferenceMap.Remove(id);
                }
            }

            return this;
        }

        #endregion // UnregisterHandler

        #endregion // Public Methods

        #region IWeakEventListener Members

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            bool handled = false;

            if (managerType == typeof(PropertyChangedEventManager))
            {
                PropertyChangedEventArgs args = e as PropertyChangedEventArgs;
                if (args != null && sender is TPropertySource changed)
                {
                    string sourceName = changed.Id;
                    string propertyName = args.PropertyName;

                    if (String.IsNullOrEmpty(propertyName))
                    {
                        // When the property name is empty, all properties are considered to be invalidated.
                        // Iterate over a copy of the list of handlers, in case a handler is registered by a callback.
                        foreach (Action<TPropertySource> handler in _idToPropertyNameToHandlerMapMap[sourceName].Values.ToList())
                            handler(changed);

                        handled = true;
                    }
                    else
                    {
                        Action<TPropertySource> handler;
                        if (_idToPropertyNameToHandlerMapMap[sourceName].TryGetValue(propertyName, out handler))
                        {
                            handler(changed);

                            handled = true;
                        }
                    }
                }
            }

            return handled;
        }

        #endregion // IWeakEventListener Members

        #region Private Helpers

        #region GetPropertyName

        static string GetPropertyName(Expression<Func<TPropertySource, object>> expression)
        {
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }

            Debug.Assert(memberExpression != null, "Please provide a lambda expression like 'n => n.PropertyName'");

            if (memberExpression != null)
            {
                var propertyInfo = memberExpression.Member as PropertyInfo;

                return propertyInfo.Name;
            }

            return null;
        }

        #endregion // GetPropertyName

        #region GetPropertySource

        public TPropertySource GetPropertySource(string id)
        {
            TPropertySource target;
            if (_idToReferenceMap[id].TryGetTarget(out target))
            {
                return target;
            }
            else
                return default;
        }

        #endregion // GetPropertySource

        #endregion // Private Helpers

        #region Fields

        readonly IDictionary<string, IDictionary<string, Action<TPropertySource>>> _idToPropertyNameToHandlerMapMap;
        readonly IDictionary<string, WeakReference<TPropertySource>> _idToReferenceMap;
        #endregion // Fields        
    }
}
