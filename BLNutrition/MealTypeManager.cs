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
   public static class MealTypeManager
    {
       public static NSysMealType GetItem(int MealTypeID)
       {
           return MealTypeDL.GetItem(MealTypeID);
       }
       public static NSysMealType GetItemByCondition(string condition)
       {
           return MealTypeDL.GetItemByCondition(condition);
       }
    }
}
