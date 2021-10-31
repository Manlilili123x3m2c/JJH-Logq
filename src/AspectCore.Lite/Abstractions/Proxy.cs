﻿using AspectCore.Lite.Internal;
using System;
using System.Linq;
using System.Reflection;

namespace AspectCore.Lite.Abstractions
{
    public sealed class Proxy
    {
        public object Instance { get; }
        public MethodInfo Method { get; }
        public Type ProxyType { get; }

        internal Proxy(object instance, MethodInfo method, Type proxyType)
        {
            ExceptionUtilities.ThrowArgumentNull(instance , nameof(instance));
            ExceptionUtilities.ThrowArgumentNull(method , nameof(method));
            ExceptionUtilities.ThrowArgumentNull(proxyType , nameof(proxyType));

            Instance = instance;
            Method = method;
            ProxyType = proxyType;
        }
    }
}