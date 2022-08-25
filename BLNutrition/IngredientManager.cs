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
    public class IngredientManager
    {
        public static Ingredient GetItem(int IngredientID)
        {
            return IngredientDL.GetItem(IngredientID);
        }
        
        public static DataSet GetItemSearch(int IngredientID)
        {
            return IngredientDL.GetItemSearch(IngredientID);
        }

        public static DataSet GetGramItemSearch(int IngredientID)
        {
            return IngredientDL.GetGramItemSearch(IngredientID);
        }

        public static DataSet GetMilliItemSearch(int IngredientID)
        {
            return IngredientDL.GetMilliItemSearch(IngredientID);
        }

        public static DataSet GetMicroItemSearch(int IngredientID)
        {
            return IngredientDL.GetMicroItemSearch(IngredientID);
        }

        public static List<Ingredient> GetList(string searchString)
        {
            return IngredientDL.GetList(searchString);
        }

        public static List<Ingredient> GetDetailedSearchList(string searchString, string searchOrderBy)
        {
            return IngredientDL.GetDetailedSearchList(searchString, searchOrderBy);
        }

        public static List<Ingredient> GetIngredientList(string searchString)
        {
            return IngredientDL.GetIngredientList(searchString);
        }

        public static List<Ingredient> GetListSearch(string searchString)
        {
            return IngredientDL.GetListSearch(searchString);
        }

        public static int GetStandardUnitCount(Ingredient ingredient, int standardUnitID)
        {
            return IngredientDL.GetStandardUnitCount(ingredient, standardUnitID);
        }

        public static int GetID()
        {
            return IngredientDL.GetID();
        }

        public static int GetIngredientCount(Ingredient ingredient)
        {
            return IngredientDL.GetIngredientCount(ingredient);
        }

        public static void Save(Ingredient ingredient)
        {
            //using (TransactionScope transactionScope = new TransactionScope())
            //{
                IngredientDL.Save(ingredient);
                foreach (IngredientAminoAcid ingredientAminoAcid in ingredient.IngredientAminoAcidList)
                {
                    IngredientAminoAcidDL.SaveNutrientValues(ingredientAminoAcid);
                }

                foreach (IngredientFattyAcid ingredientFattyAcid in ingredient.IngredientFattyAcidList)
                {
                    IngredientFattyAcidDL.SaveNutrientValues(ingredientFattyAcid);
                }

                foreach (IngredientNutrients ingredientNutrients in ingredient.IngredientNutrientsList)
                {
                    IngredientNutrientsDL.SaveNutrientValues(ingredientNutrients);
                }
              
                foreach (IngredientStandardUnit ingredientStandardUnit in ingredient.IngredientStandardUnitList)
                {
                    IngredientStandardUnitDL.Save(ingredientStandardUnit);
                }

                //transactionScope.Complete();
            //}
        }

        public static bool DeleteIngredient(Ingredient ingredient)
        {
            bool IsDelete = false;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                if (IngredientStandardUnitDL.DeleteStandardUnit(ingredient.Id))
                {
                    if (IngredientNutrientsDL.DeleteNutrients(ingredient.Id))
                    {
                        if (IngredientAminoAcidDL.DeleteAminoAcids(ingredient.Id))
                        {
                            if (IngredientFattyAcidDL.DeleteFattyAcids(ingredient.Id))
                            {
                                if (IngredientDL.DeleteIngredient(ingredient.Id))
                                {
                                    IsDelete = true;
                                }

                            }
                        }
                    }
                }
                transactionScope.Complete();
            }
            return IsDelete;
        }

        public static void SaveAllergic(int memberID, Ingredient ingredient)
        {
            IngredientDL.SaveAllergic(memberID, ingredient);
        }

        public static void DeleteAllergic(int memberID, Ingredient ingredient)
        {
            IngredientDL.DeleteAllergic(memberID, ingredient);
        }
    }
}
