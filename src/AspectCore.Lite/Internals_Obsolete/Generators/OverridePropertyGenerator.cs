﻿using AspectCore.Lite.Abstractions;
using System;
using System.Reflection;
using System.Reflection.Emit;

namespace AspectCore.Lite.Internals.Generators
{
    internal class OverridePropertyGenerator : PropertyGenerator
    {
        public OverridePropertyGenerator(TypeBuilder typeBuilder, PropertyInfo propertyInfo, FieldGenerator serviceInstanceGenerator, FieldGenerator serviceProviderGenerator, IPointcut pointcut)
            : base(typeBuilder, propertyInfo, serviceInstanceGenerator, serviceProviderGenerator, pointcut)
        {
        }

        public override void GenerateProperty()
        {
            var property = typeBuilder.DefineProperty(propertyInfo.Name, PropertyAttributes.None, propertyInfo.PropertyType, Type.EmptyTypes);

            if (propertyInfo.CanRead)
            {
                var methodGenerator = new OverrideMethodGenerator(typeBuilder, propertyInfo.GetMethod, serviceInstanceGenerator, serviceProviderGenerator, pointcut);
                methodGenerator.GenerateMethod();
                property.SetGetMethod(methodGenerator.MethodBuilder);
            }

            if (propertyInfo.CanWrite)
            {
                var methodGenerator = new OverrideMethodGenerator(typeBuilder, propertyInfo.SetMethod, serviceInstanceGenerator, serviceProviderGenerator, pointcut);
                methodGenerator.GenerateMethod();
                property.SetSetMethod(methodGenerator.MethodBuilder);
            }
        }
    }
}
