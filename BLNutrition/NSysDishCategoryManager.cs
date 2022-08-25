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
    public class NSysDishCategoryManager
    {
        public static List<NSysDishCategory> GetList()
        {
            return NSysDishCategoryDL.GetList();
        }        

        public static NSysDishCategory GetItem(int dishCategoryID)
        {
            return NSysDishCategoryDL.GetItem(dishCategoryID);
        }
    }
}
