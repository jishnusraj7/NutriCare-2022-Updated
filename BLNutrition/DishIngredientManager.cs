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
	public class DishIngredientManager
	{
        public static List<DishIngredient> GetList(int dishID)
        {
            List<DishIngredient> dishIngredientList = new List<DishIngredient>();
            IngredientStandardUnit ingredientStandardUnitItem = new IngredientStandardUnit();
            dishIngredientList = DishIngredientDL.GetDishIngredientsList(dishID);
            return dishIngredientList;
        }

        public static List<DishIngredient> GetIngredientDisplayList(int dishID)
        {
            return DishIngredientDL.GetIngredientDisplayList(dishID);
        }

        public static List<DishIngredient> GetNutrientsList(int dishID,int plan)
        {
            return DishIngredientDL.GetNutrientsList(dishID,plan);
        }

        public static List<DishIngredient> GetAminoAcidsList(int dishID, int plan)
        {
            return DishIngredientDL.GetAminoAcidsList(dishID,plan);
        }

        public static List<DishIngredient> GetFattyAcidsList(int dishID, int plan)
        {
            return DishIngredientDL.GetFattyAcidsList(dishID,plan);
        }

        public static List<DishIngredient> GetIngredientNutrientList(int dishID)
        {
            return DishIngredientDL.GetIngredientNutrientList(dishID);
        }

        public static bool Delete(DishIngredient dishIngredient)
        {
            return DishIngredientDL.Delete(dishIngredient);
        }
	}
}
