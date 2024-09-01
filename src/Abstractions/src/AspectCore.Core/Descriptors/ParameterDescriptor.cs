﻿using System;
using System.Reflection;
using AspectCore.Abstractions;

namespace AspectCore.Core
{
    internal class ParameterDescriptor : IParameterDescriptor
    {
        private object _value;
        private ParameterInfo _parameterInfo;

        public ParameterDescriptor(object value, ParameterInfo parameterInfo)
        {
            _parameterInfo = parameterInfo ?? throw new ArgumentNullException(nameof(parameterInfo));
            _value = value;
        }

        public string Name
        {
            get
            {
                return _parameterInfo.Name;
            }
        }

        public virtual object Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (value == null)
                {
                    if (ParameterType.GetTypeInfo().IsValueType && !(ParameterType.GetTypeInfo().IsGenericType && ParameterType.GetTypeInfo().GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        throw new InvalidOperationException($"object type are not equal \"{Name}\" parameter type or not a derived type of parameter type.");
                    }
                    _value = value;
                    return;
                }
                var valueType = value.GetType();
                if (valueType != ParameterType && !ParameterType.GetTypeInfo().IsAssignableFrom(valueType.GetTypeInfo()))
                {
                    throw new InvalidOperationException($"object type are not equal \"{Name}\" parameter type or not a derived type of parameter type.");
                }
                _value = value;
            }
        }

        public Type ParameterType
        {
            get
            {
                return _parameterInfo.ParameterType;
            }
        }

        public ParameterInfo ParameterInfo
        {
            get
            {
                return _parameterInfo;
            }
        }
    }
}