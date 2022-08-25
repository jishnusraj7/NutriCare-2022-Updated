using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using BLNutrition;
using BONutrition;
using DLNutrition;

namespace BLNutrition
{
    public class NSysPropertyStatusManager
    {
        public static List<NSysPropertyStatus> GetList(int PropertyType)
        {
            return NSysPropertyStatusDL.GetList(PropertyType);
        }
       
        public static NSysPropertyStatus GetItem(int propertyStatusID)
        {
            return NSysPropertyStatusDL.GetItem(propertyStatusID);
        }
    }
}
