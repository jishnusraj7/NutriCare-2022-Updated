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
    public class StandardUnitManager
    {
        public static StandardUnit GetItem(int standardUnitID)
        {
            return StandardUnitDL.GetItem(standardUnitID);
        }

        public static List<StandardUnit> GetList()
        {
            return StandardUnitDL.GetList();
        }

        public static List<StandardUnit> GetList(int ingredientID)
        {
            List<StandardUnit> standardUnitList = new List<StandardUnit>();
            IngredientStandardUnit ingredientUnitItem = new IngredientStandardUnit();
            standardUnitList = StandardUnitDL.GetList();
            if (ingredientID > 0)
            {
                foreach (StandardUnit standardUnitItem in standardUnitList)
                {
                    ingredientUnitItem = IngredientStandardUnitDL.GetItem(ingredientID, standardUnitItem.StandardUnitID);
                    if (ingredientUnitItem != null)
                    {
                        standardUnitItem.StandardWeight = ingredientUnitItem.StandardWeight;
                        standardUnitItem.IsApplicable = true;
                    }
                }
            }
            return standardUnitList;
        }
    }
}
