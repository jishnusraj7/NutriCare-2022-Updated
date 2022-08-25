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
    public class FoodHabitManager
    {

        public static FoodHabit GetItem(int LanguageID, int FoodHabitID)
        {
            return FoodHabitDL.GetItem(LanguageID, FoodHabitID);
        }
    }
}
