﻿using System;
using System.Reflection;
using AspectCore.Abstractions;

namespace AspectCore.Core.Configuration
{
    public abstract class InterceptorFactory : IInterceptorFactory
    {
        private static readonly AspectPredicate[] EmptyPredicates = new AspectPredicate[0];
        private readonly AspectPredicate[] _predicates;

        public InterceptorFactory(params AspectPredicate[] predicates)
        {
            _predicates = predicates ?? EmptyPredicates;
        }

        public bool CanCreated(MethodInfo method)
        {
            if (_predicates.Length == 0)
            {
                return true;
            }
            foreach (var predicate in _predicates)
            {
                if (predicate(method)) return true;
            }
            return false;
        }

        public abstract IInterceptor CreateInstance(IServiceProvider serviceProvider);
    }
}
