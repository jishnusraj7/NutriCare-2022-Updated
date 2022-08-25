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
    public class MealPlanManager
    {
        public static List<MealPlan> GetFoodPlanList(int dishID)
        {
            return MealPlanDL.GetFoodPlanList(dishID);
        }
    }
}
