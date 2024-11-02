﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AspectCore.Extensions.Reflection;

namespace AspectCore.Injector
{
    internal sealed class PropertyResolverSelector
    {
        private static readonly ConcurrentDictionary<Type, PropertyResolver[]> propertyInjectorCache = new ConcurrentDictionary<Type, PropertyResolver[]>();

        internal static readonly PropertyResolverSelector Default = new PropertyResolverSelector();

        internal PropertyResolver[] SelectPropertyResolver(Type implementationType)
        {
            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }
            return propertyInjectorCache.GetOrAdd(implementationType, type => SelectPropertyResolverInternal(type).ToArray());
        }

        private IEnumerable<PropertyResolver> SelectPropertyResolverInternal(Type type)
        {
            foreach (var property in type.GetTypeInfo().GetProperties())
            {
                if (property.CanWrite)
                {
                    var reflector = property.GetReflector();
                    if (reflector.IsDefined(typeof(InjectAttribute)))
                    {
                        yield return new PropertyResolver(provider => provider.GetService(property.PropertyType), reflector);
                    }
                }
            }
        }
    }
}