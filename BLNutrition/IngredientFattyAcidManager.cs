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
    public class IngredientFattyAcidManager
    {
        public static List<IngredientFattyAcid> GetListNutrientValues(int ingredientID, byte nutrientGroup)
        {
            List<IngredientFattyAcid> ingredientFattyList = IngredientFattyAcidDL.GetListNutrientValues(ingredientID);
            return ingredientFattyList;
        }

        public static List<IngredientFattyAcid> GetFattyAcidList(int ingredientID, byte nutrientGroup,int DishID)
        {
            List<IngredientFattyAcid> ingredientFattyList = IngredientFattyAcidDL.GetFattyAcidList(ingredientID, DishID);
            return ingredientFattyList;
        }

        public static List<IngredientFattyAcid> GetListNutrientsDish(int DishID,int PlanID, byte nutrientGroup)
        {
            List<IngredientFattyAcid> ingredientFattyList = IngredientFattyAcidDL.GetListNutrientsDish(DishID, PlanID);
            return ingredientFattyList;
        }
       
        public static IngredientFattyAcid GetItemNutrients(int ingredientID, int nutrientID)
        {
            return IngredientFattyAcidDL.GetItemNutrients(ingredientID, nutrientID);
        }

        public static List<IngredientFattyAcid> GetFattyAcidsList(int dishID, int plan)
        {
            return IngredientFattyAcidDL.GetFattyAcidsList(dishID, plan);
        }
    }
}
