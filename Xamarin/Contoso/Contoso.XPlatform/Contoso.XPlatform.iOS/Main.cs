using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Contoso.XPlatform.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            
#if DEBUG
            try
            {
#endif
                UIApplication.Main(args, null, typeof(AppDelegate));
#if DEBUG
            }
            catch (System.Exception)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                    System.Diagnostics.Debugger.Break();
                throw;
            }
#endif
        }
    }
}
