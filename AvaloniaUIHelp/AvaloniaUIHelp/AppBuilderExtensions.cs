using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AvaloniaUIHelp
{
    public static class AppBuilderUIExtensions
    {
        public static TAppBuilder UseAvaloniaUIHelp<TAppBuilder>(this TAppBuilder builder)
            where TAppBuilder : AppBuilderBase<TAppBuilder>, new()
        {
            try
            {
                if (Debugger.IsAttached)
                {
                    AvaloniaUIHelp.Init(out _);
                }
            }
            catch (Exception e)
            {

            }
            return builder;
        }
    }


}
