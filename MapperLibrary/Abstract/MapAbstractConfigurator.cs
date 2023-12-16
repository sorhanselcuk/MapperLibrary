using MapperLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MapperLibrary.Abstract
{
    public abstract class MapAbstractConfigurator<TSource, TTarget>
        where TSource : class, new()
        where TTarget : class, new()
    {
        private Dictionary<PropertyInfo, PropertyInfo> _matchingProperties;
        public MapAbstractConfigurator()
        {
            _matchingProperties = new Dictionary<PropertyInfo, PropertyInfo>();
        }
        public Dictionary<PropertyInfo, PropertyInfo> MatchingProperties => _matchingProperties;

        protected void Register<TSourceProperty, TTargetProperty>(Expression<Func<TSource, TSourceProperty>> sourceExpression,
                                                                  Expression<Func<TTarget, TTargetProperty>> targetExpression)
        {
            var sourcePropertyName = GetMemberName(sourceExpression);
            var targetPropertyName = GetMemberName(targetExpression);
            CheckExistsValuesIfExistsThrowException(sourcePropertyName, targetPropertyName);
            _matchingProperties.Add(GetPropertyAsName<TSource>(sourcePropertyName), GetPropertyAsName<TTarget>(targetPropertyName));
        }

        private void CheckExistsValuesIfExistsThrowException(string sourcePropertyName, string targetPropertyName)
        {
            if (_matchingProperties.Any(x => x.Key.Name == sourcePropertyName))
                throw new PropertyNameIsExistsException($"There is already exists {sourcePropertyName} for source properties");
            if (_matchingProperties.Any(x => x.Value.Name == targetPropertyName))
                throw new PropertyNameIsExistsException($"There is already exists {sourcePropertyName} for target properties");
        }

        private string GetMemberName<TClass, TProperty>(Expression<Func<TClass, TProperty>> expression)
        {
            if (expression.Body.NodeType != ExpressionType.MemberAccess)
                throw new MapperLibraryException($"Mapper library able just using for properties");
            var memberExpression = (MemberExpression)expression.Body;
            return memberExpression.Member.Name;
        }
        private PropertyInfo GetPropertyAsName<T>(string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property == null)
                throw new PropertyNotFoundException($"There is not include property name of {propertyName} in {typeof(T).Name}");
            return property;
        }
    }
}
