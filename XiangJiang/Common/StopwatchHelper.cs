﻿using System;
using System.Diagnostics;

namespace XiangJiang.Common
{
    public sealed class StopwatchHelper
    {
        public static double Execute(Action executeFactory)
        {
            var sw = Stopwatch.StartNew();
            executeFactory?.Invoke();
            return sw.ElapsedMilliseconds;
        }
    }
}