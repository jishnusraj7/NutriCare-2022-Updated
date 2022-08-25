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
    public class IngredientStandardUnitManager
    {
        public static List<IngredientStandardUnit> GetUnitList(int ingredientID)
        {
            return IngredientStandardUnitDL.GetUnitList(ingredientID);
        }

        public static IngredientStandardUnit GetItem(int ingredientID, byte standardUnitID)
        {
            StandardUnit standardUnit = new StandardUnit();
            IngredientStandardUnit ingredientStandardUnitItem = new IngredientStandardUnit();
            ingredientStandardUnitItem = IngredientStandardUnitDL.GetItem(ingredientID, standardUnitID);
            return ingredientStandardUnitItem;
        }

        public static bool DeleteStandardUnit(int ingredientID)
        {
            return IngredientStandardUnitDL.DeleteStandardUnit(ingredientID);
        }
    }
}
