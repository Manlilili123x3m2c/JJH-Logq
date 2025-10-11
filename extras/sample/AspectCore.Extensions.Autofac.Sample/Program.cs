﻿using System;
using AspectCore.Configuration;
using AspectCore.Injector;
using Autofac;

namespace AspectCore.Extensions.Autofac.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceContaniner = new ServiceContainer();
            serviceContaniner.AddType<ITaskService, TaskService>();

            var containerBuilder = new ContainerBuilder();

            //调用Populate扩展方法在Autofac中注册已经注册到ServiceContainer中的服务（如果有）。注：此方法调用应在RegisterDynamicProxy之前
            containerBuilder.Populate(serviceContaniner);

            var configuration = serviceContaniner.Configuration;

            //调用RegisterDynamicProxy扩展方法在Autofac中注册动态代理服务和动态代理配置
            containerBuilder.RegisterDynamicProxy(configuration, config =>
             {
                 config.Interceptors.AddTyped<MethodExecuteLoggerInterceptor>(Predicates.ForService("*Service"));
             });

            var container = containerBuilder.Build();

            var taskService = container.Resolve<ITaskService>();

            taskService.Run();

            Console.ReadKey();
        }
    }

    public interface ITaskService
    {
        bool Run();
    }

    public class TaskService : ITaskService
    {
        public bool Run()
        {
            return true;
        }
    }
}