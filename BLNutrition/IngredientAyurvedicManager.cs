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
    public class IngredientAyurvedicManager
    {        
        public static List<IngredientAyurvedic> GetListAyurvedic(int ingredientID)
        {
            List<IngredientAyurvedic> ingredientAyurList = new List<IngredientAyurvedic>();
            ingredientAyurList = IngredientAyurvedicDL.GetListAyurvedic(ingredientID);
            return ingredientAyurList;
        }

        public static List<IngredientAyurvedic> GetListAyurvedicDish(int ingredientID)
        {
            List<IngredientAyurvedic> ingredientAyurList = new List<IngredientAyurvedic>();
            ingredientAyurList = IngredientAyurvedicDL.GetListAyurvedicDish(ingredientID);
            return ingredientAyurList;
        }        
    }
}
