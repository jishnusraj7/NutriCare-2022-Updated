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
    public class NSysFoodCategoryManager
    {
        public static List<NSysFoodCategory> GetList()
        {
            return NSysFoodCategoryDL.GetList();
        }
       
        public static NSysFoodCategory GetItem(int foodCategoryID)
        {
            return NSysFoodCategoryDL.GetItem(foodCategoryID);
        }
    }
}
