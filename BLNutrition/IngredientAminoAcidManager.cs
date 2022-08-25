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
    public class IngredientAminoAcidManager
    {
        public static List<IngredientAminoAcid> GetListNutrientValues(int ingredientID, byte nutrientGroup)
        {
            List<IngredientAminoAcid> ingredientAminoList = IngredientAminoAcidDL.GetListNutrientValues(ingredientID);
            return ingredientAminoList;
        }

        public static List<IngredientAminoAcid> GetAminoAcidList(int ingredientID, byte nutrientGroup,int DishID)
        {
            List<IngredientAminoAcid> ingredientAminoList = IngredientAminoAcidDL.GetAminoAcidList(ingredientID, DishID);
            return ingredientAminoList;
        }

        public static List<IngredientAminoAcid> GetListNutrientsDish(int DishID, int PlanID, byte nutrientGroup)
        {
            List<IngredientAminoAcid> ingredientAminoList = IngredientAminoAcidDL.GetListNutrientsDish(DishID, PlanID);
            return ingredientAminoList;
        }
       
        public static IngredientAminoAcid GetItemNutrients(int ingredientID, int nutrientID)
        {
            return IngredientAminoAcidDL.GetItemNutrients(ingredientID, nutrientID);
        }

        public static List<IngredientAminoAcid> GetAminoAcidsList(int dishID, int plan)
        {
            return IngredientAminoAcidDL.GetAminoAcidsList(dishID, plan);
        }
    }
}
