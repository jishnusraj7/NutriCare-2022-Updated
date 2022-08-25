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
    public class CalorieCalculatorManager
    {
        public static int GetEnergyRequirement(int Age, int SexID)
        {
            return CalorieCalculatorDL.GetEnergyRequirement(Age, SexID);
        }

        public static int GetBMIPercentile(int Age, int SexID, double BMI)
        {
            return CalorieCalculatorDL.GetBMIPercentile(Age, SexID, BMI);
        }

        public static double[] GetBMIIdealRange(int Age, int SexID)
        {
            return CalorieCalculatorDL.GetBMIIdealRange(Age, SexID);
        }

        public static double GetMeanHeight(int Age, int SexID)
        {
            return CalorieCalculatorDL.GetMeanHeight(Age, SexID);
        }

        public static double GetMeanWeight(int Age, int SexID)
        {
            return CalorieCalculatorDL.GetMeanWeight(Age, SexID);
        }

        public static List<CalorieCalculator> GetDishIngredient(int dishId)
        {
            return CalorieCalculatorDL.GetDishIngredient(dishId);
        }

        public static DataSet GetDish(int DishID)
        {
            return CalorieCalculatorDL.GetDish(DishID);
        }

        public static DataSet GetDish(int DishID, float Plan)
        {
            return CalorieCalculatorDL.GetDish(DishID, Plan);
        }

        public static DataSet GetIngredient(int DishID)
        {
            return CalorieCalculatorDL.GetIngredient(DishID);
        }

    }
}
