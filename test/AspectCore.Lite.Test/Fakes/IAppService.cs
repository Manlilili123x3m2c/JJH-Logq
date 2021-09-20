﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspectCore.Lite.Test.Fakes
{
    [ExecutionTimerInterceptor]
    public interface IAppService
    {
        int AppId { get; set; }
        string AppName { get; set; }
        [ParameterNotNull]
        bool RunApp(string[] args);
        [InterceptorApp]
        string GetAppType();
        void ExitApp();
    }
}
