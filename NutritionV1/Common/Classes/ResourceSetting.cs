using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Resources;
using System.Reflection;

namespace NutritionV1
{    
    public class ResourceSetting
    {
        public static string displayLanguage = null;
        public static int selectlanguage = 0;
        public static int defaultlanguage = 0;

        public static ResourceManager GetResource()
        {
            ResourceManager rm = new ResourceManager("NutritionV1.Properties.Resources", Assembly.GetExecutingAssembly());
            return rm;
        }
    }
}
