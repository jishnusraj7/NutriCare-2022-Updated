using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Transactions;
using BLNutrition;
using BONutrition;
using DLNutrition;

namespace BLNutrition
{
    public class IngredientNutrientsManager
    {
        public static List<IngredientNutrients> GetListNutrientValues(int ingredientID, byte nutrientGroup)
        {
            List<IngredientNutrients> ingredientNutrientsList = IngredientNutrientsDL.GetListNutrientValues(ingredientID);
            return ingredientNutrientsList;
        }

        public static List<IngredientNutrients> GetIngredientNutrients(int ingredientID, byte nutrientGroup,int DishID)
        {
            List<IngredientNutrients> ingredientNutrientsList = IngredientNutrientsDL.GetIngredientNutrients(ingredientID, DishID);
            return ingredientNutrientsList;
        }

        public static List<IngredientNutrients> GetListNutrientsDish(int DishID, int PlanID, byte nutrientGroup)
        {
            List<IngredientNutrients> ingredientNutrientsList = IngredientNutrientsDL.GetListNutrientsDish(DishID, PlanID);
            return ingredientNutrientsList;
        }


        public static IngredientNutrients GetItemNutrients(int ingredientID, int nutrientID)
        {
            return IngredientNutrientsDL.GetItemNutrients(ingredientID, nutrientID);
        }

        public static List<IngredientNutrients> GetListNutrientMain(int ingredientID)
        {
            return IngredientNutrientsDL.GetListNutrientMain(ingredientID);
        }

        public static List<IngredientNutrients> GetItemNutrientsMain(int ingredientID)
        {
            return IngredientNutrientsDL.GetItemNutrientsMain(ingredientID);
        }

        public static List<IngredientNutrients> GetNutrientsList(int dishID, int plan)
        {
            return IngredientNutrientsDL.GetNutrientsList(dishID, plan);
        }
    }
}
