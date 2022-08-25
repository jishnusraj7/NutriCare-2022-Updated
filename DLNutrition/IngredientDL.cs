using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using BONutrition;
using NutritionV1.Classes;
using NutritionViews;

namespace DLNutrition
{
    public class IngredientDL
    {
        public static Ingredient GetItem(int IngredientID)
        {
            Ingredient ingredient = null;
            DBHelper dbManager = null;

            // SQL Server
            //string SqlQuery = "SELECT Ingredient.IngredientID, Ingredient.IngredientName, Ingredient.DisplayName, Ingredient.Comments, Ingredient.MedicalFavourable," +
            //                  " Ingredient.MedicalUnFavourable,Ingredient.MedicalBalanced, Ingredient.ScrappageRate, Ingredient.WeightChangeRate, Ingredient.Keywords, Ingredient.DisplayImage, Ingredient.IsAllergic," +
            //                  " Ingredient.IsSystemIngredient, Ingredient.FrozenLife, Ingredient.RefrigeratedLife, Ingredient.ShelfLife, Ingredient.EthnicID, Ingredient.FoodHabitID, Ingredient.DisplayOrder,Ingredient.GeneralHealthValue," +
            //                  " Ingredient.AyurHealthValue,Ingredient.AyurvedicFavourable, Ingredient.AyurvedicUnFavourable,Ingredient.AyurvedicBalanced, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Calorie' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Calorie, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Protien' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Protien,  " +
            //                  " SUM((CASE WHEN NutrientParam = 'CarboHydrates' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS CarboHydrates, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Fat' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Fat,  " +
            //                  " SUM((CASE WHEN NutrientParam = 'Fibre' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Fibre, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Iron' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Iron,  " +
            //                  " SUM((CASE WHEN NutrientParam = 'Calcium' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Calcium, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Phosphorus' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Phosphorus," +
            //                  " SUM((CASE WHEN NutrientParam = 'VitaminARetinol' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS VitaminARetinol," +
            //                  " SUM((CASE WHEN NutrientParam = 'VitaminABetaCarotene' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS VitaminABetaCarotene," +
            //                  " SUM((CASE WHEN NutrientParam = 'Thiamine' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Thiamine, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Riboflavin' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Riboflavin, " +
            //                  " SUM((CASE WHEN NutrientParam = 'NicotinicAcid' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS NicotinicAcid, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Pyridoxine' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Pyridoxine, " +
            //                  " SUM((CASE WHEN NutrientParam = 'FolicAcid' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS FolicAcid, " +
            //                  " SUM((CASE WHEN NutrientParam = 'VitaminB12' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS VitaminB12, " +
            //                  " SUM((CASE WHEN NutrientParam = 'VitaminC' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS VitaminC, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Moisture' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Moisture, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Sodium' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Sodium, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Pottasium' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Pottasium, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Zinc' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Zinc " +
            //                  " FROM Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
            //                  " Where Ingredient.IngredientID = " + IngredientID + " GROUP BY Ingredient.IngredientID, Ingredient.IngredientName,Ingredient.DisplayName, Ingredient.Comments, Ingredient.MedicalFavourable," +
            //                  " Ingredient.MedicalUnFavourable,Ingredient.MedicalBalanced, Ingredient.ScrappageRate, Ingredient.WeightChangeRate, Ingredient.Keywords, Ingredient.DisplayImage, Ingredient.IsAllergic," +
            //                  " Ingredient.IsSystemIngredient, Ingredient.FrozenLife, Ingredient.RefrigeratedLife, Ingredient.ShelfLife, Ingredient.EthnicID, Ingredient.FoodHabitID, Ingredient.DisplayOrder,Ingredient.GeneralHealthValue," +
            //                  " Ingredient.AyurHealthValue,Ingredient.AyurvedicFavourable, Ingredient.AyurvedicUnFavourable,Ingredient.AyurvedicBalanced ";


            // MS Access
            string SqlQuery = "SELECT Ingredient.IngredientID, Ingredient.IngredientName, Ingredient.DisplayName, Ingredient.Comments, Ingredient.MedicalFavourable," +
                              " Ingredient.MedicalUnFavourable,Ingredient.MedicalBalanced, Ingredient.ScrappageRate, Ingredient.WeightChangeRate, Ingredient.Keywords, Ingredient.DisplayImage, Ingredient.IsAllergic," +
                              " Ingredient.IsSystemIngredient, Ingredient.FrozenLife, Ingredient.RefrigeratedLife, Ingredient.ShelfLife, Ingredient.EthnicID, Ingredient.FoodHabitID, Ingredient.DisplayOrder,Ingredient.GeneralHealthValue," +
                              " Ingredient.AyurHealthValue,Ingredient.AyurvedicFavourable, Ingredient.AyurvedicUnFavourable,Ingredient.AyurvedicBalanced, " +
                              " SUM(IIF(NutrientParam='Calorie',IngredientNutrients.NutrientValue, 0 )) AS Calorie, " +
                              " SUM(IIF(NutrientParam='Protien',IngredientNutrients.NutrientValue, 0 )) AS Protien, " +
                              " SUM(IIF(NutrientParam='CarboHydrates',IngredientNutrients.NutrientValue, 0)) AS CarboHydrates, " +
                              " SUM(IIF(NutrientParam='Fat',IngredientNutrients.NutrientValue, 0 )) AS Fat, " +
                              " SUM(IIF(NutrientParam='Fibre',IngredientNutrients.NutrientValue, 0 )) AS Fibre, " +
                              " SUM(IIF(NutrientParam='Iron',IngredientNutrients.NutrientValue, 0 )) AS Iron, " +
                              " SUM(IIF(NutrientParam='Calcium',IngredientNutrients.NutrientValue, 0 )) AS Calcium, " +
                              " SUM(IIF(NutrientParam='Phosphorus',IngredientNutrients.NutrientValue, 0 )) AS Phosphorus, " +
                              " SUM(IIF(NutrientParam='VitaminARetinol',IngredientNutrients.NutrientValue, 0)) AS VitaminARetinol, " +
                              " SUM(IIF(NutrientParam='VitaminABetaCarotene',IngredientNutrients.NutrientValue, 0 )) AS VitaminABetaCarotene, " +
                              " SUM(IIF(NutrientParam='Thiamine',IngredientNutrients.NutrientValue, 0 )) AS Thiamine, " +
                              " SUM(IIF(NutrientParam='Riboflavin',IngredientNutrients.NutrientValue, 0 )) AS Riboflavin, " +
                              " SUM(IIF(NutrientParam='NicotinicAcid',IngredientNutrients.NutrientValue, 0 )) AS NicotinicAcid, " +
                              " SUM(IIF(NutrientParam='Pyridoxine',IngredientNutrients.NutrientValue, 0 )) AS Pyridoxine, " +
                              " SUM(IIF(NutrientParam='FolicAcid',IngredientNutrients.NutrientValue, 0 )) AS FolicAcid, " +
                              " SUM(IIF(NutrientParam='VitaminB12',IngredientNutrients.NutrientValue, 0)) AS VitaminB12, " +
                              " SUM(IIF(NutrientParam='VitaminC',IngredientNutrients.NutrientValue, 0 )) AS VitaminC, " +
                              " SUM(IIF(NutrientParam='Moisture',IngredientNutrients.NutrientValue, 0 )) AS Moisture, " +
                              " SUM(IIF(NutrientParam='Sodium',IngredientNutrients.NutrientValue, 0 )) AS Sodium, " +
                              " SUM(IIF(NutrientParam='Pottasium',IngredientNutrients.NutrientValue, 0 )) AS Pottasium, " +
                              " SUM(IIF(NutrientParam='Zinc',IngredientNutrients.NutrientValue, 0 )) AS Zinc " +
                              " FROM (Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
                              " Where Ingredient.IngredientID = " + IngredientID + " GROUP BY Ingredient.IngredientID, Ingredient.IngredientName,Ingredient.DisplayName, Ingredient.Comments, Ingredient.MedicalFavourable," +
                              " Ingredient.MedicalUnFavourable,Ingredient.MedicalBalanced, Ingredient.ScrappageRate, Ingredient.WeightChangeRate, Ingredient.Keywords, Ingredient.DisplayImage, Ingredient.IsAllergic," +
                              " Ingredient.IsSystemIngredient, Ingredient.FrozenLife, Ingredient.RefrigeratedLife, Ingredient.ShelfLife, Ingredient.EthnicID, Ingredient.FoodHabitID, Ingredient.DisplayOrder,Ingredient.GeneralHealthValue," +
                              " Ingredient.AyurHealthValue,Ingredient.AyurvedicFavourable, Ingredient.AyurvedicUnFavourable,Ingredient.AyurvedicBalanced ";




            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drIngredient = dbManager.ExecuteReader(CommandType.Text, SqlQuery))
                {
                    if (drIngredient.Read())
                    {
                        ingredient = FillDataRecord(drIngredient);
                    }
                    drIngredient.Close();
                }
                return ingredient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }
       
        public static DataSet GetItemSearch(int IngredientID)
        {
            DataSet dsIngredient = new DataSet();
            DBHelper dbManager = null;

            string SqlQuery = "SELECT " +
                              " SUM((CASE WHEN NutrientParam = 'VitaminC' THEN (IngredientNutrients.NutrientValue / 1000) ELSE 0 END)) AS VitaminC, " +
                              " SUM((CASE WHEN NutrientParam = 'FolicAcid' THEN (IngredientNutrients.NutrientValue / 1000000) ELSE 0 END)) AS FolicAcid, " +
                              " SUM((CASE WHEN NutrientParam = 'Pyridoxine' THEN (IngredientNutrients.NutrientValue / 1000) ELSE 0 END)) AS Pyridoxine,  " +
                              " SUM((CASE WHEN NutrientParam = 'NicotinicAcid' THEN (IngredientNutrients.NutrientValue / 1000) ELSE 0 END)) AS NicotinicAcid, " +
                              " SUM((CASE WHEN NutrientParam = 'Riboflavin' THEN (IngredientNutrients.NutrientValue / 1000) ELSE 0 END)) AS Riboflavin,  " +
                              " SUM((CASE WHEN NutrientParam = 'Thiamine' THEN (IngredientNutrients.NutrientValue / 1000) ELSE 0 END)) AS Thiamine, " +
                              " SUM((CASE WHEN NutrientParam = 'VitaminABetaCarotene' THEN (IngredientNutrients.NutrientValue / 1000000) ELSE 0 END)) AS BetaCarotene," +
                              " SUM((CASE WHEN NutrientParam = 'VitaminARetinol' THEN (IngredientNutrients.NutrientValue / 1000000) ELSE 0 END)) AS Retinol," +
                              " SUM((CASE WHEN NutrientParam = 'Zinc' THEN IngredientNutrients.NutrientValue / 1000 ELSE 0 END)) AS Zinc, " +
                              " SUM((CASE WHEN NutrientParam = 'Pottasium' THEN IngredientNutrients.NutrientValue / 1000 ELSE 0 END)) AS Pottasium, " +
                              " SUM((CASE WHEN NutrientParam = 'Sodium' THEN IngredientNutrients.NutrientValue / 1000 ELSE 0 END)) AS Sodium, " +
                              " SUM((CASE WHEN NutrientParam = 'Phosphorus' THEN (IngredientNutrients.NutrientValue / 1000) ELSE 0 END)) AS Phosphorus," +
                              " SUM((CASE WHEN NutrientParam = 'Calcium' THEN (IngredientNutrients.NutrientValue / 1000) ELSE 0 END)) AS Calcium, " +
                              " SUM((CASE WHEN NutrientParam = 'Iron' THEN (IngredientNutrients.NutrientValue / 1000) ELSE 0 END)) AS Iron, " +
                              " SUM((CASE WHEN NutrientParam = 'Moisture' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Moisture, " +
                              " SUM((CASE WHEN NutrientParam = 'Fibre' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Fibre, " +
                              " SUM((CASE WHEN NutrientParam = 'Fat' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Fat, " +
                              " SUM((CASE WHEN NutrientParam = 'CarboHydrates' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS CarboHydrates, " +
                              " SUM((CASE WHEN NutrientParam = 'Protien' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Protien " +
                              " FROM Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
                              " Where Ingredient.IngredientID = " + IngredientID;

            //string SqlQuery = "SELECT " +
            //                  " SUM(IIF(NutrientParam='VitaminC',(IngredientNutrients.NutrientValue / 1000), 0 )) AS VitaminC, " +
            //                  " SUM(IIF(NutrientParam='FolicAcid',(IngredientNutrients.NutrientValue / 1000000), 0 )) AS FolicAcid, " +
            //                  " SUM(IIF(NutrientParam='Pyridoxine',(IngredientNutrients.NutrientValue / 1000), 0 )) AS Pyridoxine, " +
            //                  " SUM(IIF(NutrientParam='NicotinicAcid',(IngredientNutrients.NutrientValue / 1000), 0 )) AS NicotinicAcid, " +
            //                  " SUM(IIF(NutrientParam='Riboflavin',(IngredientNutrients.NutrientValue / 1000), 0 )) AS Riboflavin, " +
            //                  " SUM(IIF(NutrientParam='Thiamine',(IngredientNutrients.NutrientValue / 1000), 0 )) AS Thiamine, " +
            //                  " SUM(IIF(NutrientParam='VitaminABetaCarotene',(IngredientNutrients.NutrientValue / 1000000), 0 )) AS BetaCarotene, " +
            //                  " SUM(IIF(NutrientParam='VitaminARetinol',(IngredientNutrients.NutrientValue / 1000000), 0 )) AS Retinol, " +
            //                  " SUM(IIF(NutrientParam='Zinc',(IngredientNutrients.NutrientValue / 1000), 0 )) AS Zinc, " +
            //                  " SUM(IIF(NutrientParam='Pottasium',(IngredientNutrients.NutrientValue / 1000), 0 )) AS Pottasium, " +
            //                  " SUM(IIF(NutrientParam='Sodium',(IngredientNutrients.NutrientValue / 1000), 0 )) AS Sodium, " +
            //                  " SUM(IIF(NutrientParam='Phosphorus',(IngredientNutrients.NutrientValue / 1000), 0 )) AS Phosphorus, " +
            //                  " SUM(IIF(NutrientParam='Calcium',(IngredientNutrients.NutrientValue / 1000), 0 )) AS Calcium, " +
            //                  " SUM(IIF(NutrientParam='Iron',(IngredientNutrients.NutrientValue / 1000), 0 )) AS Iron, " +
            //                  " SUM(IIF(NutrientParam='Moisture',IngredientNutrients.NutrientValue, 0 )) AS Moisture, " +
            //                  " SUM(IIF(NutrientParam='Fibre',IngredientNutrients.NutrientValue, 0 )) AS Fibre, " +
            //                  " SUM(IIF(NutrientParam='Fat',IngredientNutrients.NutrientValue, 0 )) AS Fat, " +
            //                  " SUM(IIF(NutrientParam='CarboHydrates',IngredientNutrients.NutrientValue, 0 )) AS CarboHydrates, " +
            //                  " SUM(IIF(NutrientParam='Protien',IngredientNutrients.NutrientValue, 0 )) AS Protien " +
            //                  " FROM (Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
            //                  " Where Ingredient.IngredientID = " + IngredientID;


            try
            {
                dbManager = DBHelper.Instance;
                dsIngredient = dbManager.DataAdapter(CommandType.Text, SqlQuery);
                return dsIngredient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }

        public static DataSet GetGramItemSearch(int IngredientID)
        {
            DataSet dsIngredient = new DataSet();
            DBHelper dbManager = null;

            //string SqlQuery = "SELECT " +
            //                  " SUM((CASE WHEN NutrientParam = 'Moisture' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Moisture, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Fibre' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Fibre, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Fat' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Fat, " +
            //                  " SUM((CASE WHEN NutrientParam = 'CarboHydrates' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS CarboHydrates, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Protien' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Protien " +
            //                  " FROM Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
            //                  " Where Ingredient.IngredientID = " + IngredientID;

            string SqlQuery = "SELECT " +
                              " SUM(IIF(NutrientParam='Moisture',IngredientNutrients.NutrientValue, 0 )) AS Moisture, " +
                              " SUM(IIF(NutrientParam='Fibre',IngredientNutrients.NutrientValue, 0 )) AS Fibre, " +
                              " SUM(IIF(NutrientParam='Fat',IngredientNutrients.NutrientValue, 0 )) AS Fat, " +
                              " SUM(IIF(NutrientParam='CarboHydrates',IngredientNutrients.NutrientValue, 0 )) AS CarboHydrates, " +
                              " SUM(IIF(NutrientParam='Protien',IngredientNutrients.NutrientValue, 0 )) AS Protien " +
                              " FROM (Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
                              " Where Ingredient.IngredientID = " + IngredientID;


            try
            {
                dbManager = DBHelper.Instance;
                dsIngredient = dbManager.DataAdapter(CommandType.Text, SqlQuery);
                return dsIngredient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }

        public static DataSet GetMilliItemSearch(int IngredientID)
        {
            DataSet dsIngredient = new DataSet();
            DBHelper dbManager = null;

            //string SqlQuery = "SELECT " +
            //                  " SUM((CASE WHEN NutrientParam = 'VitaminC' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS VitaminC, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Pyridoxine' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS Pyridoxine,  " +
            //                  " SUM((CASE WHEN NutrientParam = 'NicotinicAcid' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS NicotinicAcid, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Riboflavin' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS Riboflavin,  " +
            //                  " SUM((CASE WHEN NutrientParam = 'Thiamine' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS Thiamine, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Zinc' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS Zinc, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Pottasium' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS Pottasium, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Sodium' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS Sodium, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Phosphorus' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS Phosphorus," +
            //                  " SUM((CASE WHEN NutrientParam = 'Calcium' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS Calcium, " +
            //                  " SUM((CASE WHEN NutrientParam = 'Iron' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS Iron " +
            //                  " FROM Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
            //                  " Where Ingredient.IngredientID = " + IngredientID;

            string SqlQuery = "SELECT " +
                             " SUM(IIF(NutrientParam='VitaminC',IngredientNutrients.NutrientValue, 0 )) AS VitaminC, " +
                             " SUM(IIF(NutrientParam='Pyridoxine',IngredientNutrients.NutrientValue, 0 )) AS Pyridoxine, " +
                             " SUM(IIF(NutrientParam='NicotinicAcid',IngredientNutrients.NutrientValue, 0 )) AS NicotinicAcid, " +
                             " SUM(IIF(NutrientParam='Riboflavin',IngredientNutrients.NutrientValue, 0 )) AS Riboflavin, " +
                             " SUM(IIF(NutrientParam='Thiamine',IngredientNutrients.NutrientValue, 0 )) AS Thiamine, " +
                             " SUM(IIF(NutrientParam='Zinc',IngredientNutrients.NutrientValue, 0 )) AS Zinc, " +
                             " SUM(IIF(NutrientParam='Pottasium',IngredientNutrients.NutrientValue, 0 )) AS Pottasium, " +
                             " SUM(IIF(NutrientParam='Sodium',IngredientNutrients.NutrientValue, 0 )) AS Sodium, " +
                             " SUM(IIF(NutrientParam='Phosphorus',IngredientNutrients.NutrientValue, 0 )) AS Phosphorus, " +
                             " SUM(IIF(NutrientParam='Calcium',IngredientNutrients.NutrientValue, 0 )) AS Calcium, " +
                             " SUM(IIF(NutrientParam='Iron',IngredientNutrients.NutrientValue, 0 )) AS Iron " +
                             " FROM (Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
                             " Where Ingredient.IngredientID = " + IngredientID;


            try
            {
                dbManager = DBHelper.Instance;
                dsIngredient = dbManager.DataAdapter(CommandType.Text, SqlQuery);
                return dsIngredient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }

        public static DataSet GetMicroItemSearch(int IngredientID)
        {
            DataSet dsIngredient = new DataSet();
            DBHelper dbManager = null;

            //string SqlQuery = "SELECT " +
            //                  " SUM((CASE WHEN NutrientParam = 'FolicAcid' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS FolicAcid, " +
            //                  " SUM((CASE WHEN NutrientParam = 'VitaminABetaCarotene' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS BetaCarotene," +
            //                  " SUM((CASE WHEN NutrientParam = 'VitaminARetinol' THEN (IngredientNutrients.NutrientValue) ELSE 0 END)) AS Retinol" +
            //                  " FROM Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
            //                  " Where Ingredient.IngredientID = " + IngredientID;

            string SqlQuery = "SELECT " +
                              " SUM(IIF(NutrientParam='FolicAcid',IngredientNutrients.NutrientValue, 0 )) AS FolicAcid, " +
                              " SUM(IIF(NutrientParam='VitaminABetaCarotene',IngredientNutrients.NutrientValue, 0 )) AS BetaCarotene, " +
                              " SUM(IIF(NutrientParam='VitaminARetinol',IngredientNutrients.NutrientValue, 0 )) AS Retinol " +
                              " FROM (Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
                              " Where Ingredient.IngredientID = " + IngredientID;

            try
            {
                dbManager = DBHelper.Instance;
                dsIngredient = dbManager.DataAdapter(CommandType.Text, SqlQuery);
                return dsIngredient;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }

        public static List<Ingredient> GetList(string searchString)
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            Ingredient ingredient = null;
            DBHelper dbManager = null;

            // MS ACCESS
            string SqlQuery = "SELECT Top 100 * FROM (SELECT Ingredient.IngredientID, Ingredient.IngredientName, Ingredient.DisplayName, Ingredient.Comments, Ingredient.MedicalFavourable," +
                              " Ingredient.MedicalUnFavourable,Ingredient.MedicalBalanced, Ingredient.ScrappageRate, Ingredient.WeightChangeRate, Ingredient.Keywords, Ingredient.DisplayImage, Ingredient.IsAllergic," +
                              " Ingredient.IsSystemIngredient, Ingredient.FrozenLife, Ingredient.RefrigeratedLife, Ingredient.ShelfLife, Ingredient.EthnicID, Ingredient.FoodHabitID, Ingredient.DisplayOrder,Ingredient.GeneralHealthValue," +
                              " Ingredient.AyurHealthValue,Ingredient.AyurvedicFavourable, Ingredient.AyurvedicUnFavourable,Ingredient.AyurvedicBalanced, " +
                              " SUM(IIF(NutrientParam='Calorie',IngredientNutrients.NutrientValue, 0 )) AS Calorie, " +
                              " SUM(IIF(NutrientParam='Protien',IngredientNutrients.NutrientValue, 0 )) AS Protien, " +
                              " SUM(IIF(NutrientParam='CarboHydrates',IngredientNutrients.NutrientValue, 0)) AS CarboHydrates, " +
                              " SUM(IIF(NutrientParam='Fat',IngredientNutrients.NutrientValue, 0 )) AS Fat, " +
                              " SUM(IIF(NutrientParam='Fibre',IngredientNutrients.NutrientValue, 0 )) AS Fibre, " +
                              " SUM(IIF(NutrientParam='Iron',IngredientNutrients.NutrientValue, 0 )) AS Iron, " +
                              " SUM(IIF(NutrientParam='Calcium',IngredientNutrients.NutrientValue, 0 )) AS Calcium, " +
                              " SUM(IIF(NutrientParam='Phosphorus',IngredientNutrients.NutrientValue, 0 )) AS Phosphorus, " +
                              " SUM(IIF(NutrientParam='VitaminARetinol',IngredientNutrients.NutrientValue, 0)) AS VitaminARetinol, " +
                              " SUM(IIF(NutrientParam='VitaminABetaCarotene',IngredientNutrients.NutrientValue, 0 )) AS VitaminABetaCarotene, " +
                              " SUM(IIF(NutrientParam='Thiamine',IngredientNutrients.NutrientValue, 0 )) AS Thiamine, " +
                              " SUM(IIF(NutrientParam='Riboflavin',IngredientNutrients.NutrientValue, 0 )) AS Riboflavin, " +
                              " SUM(IIF(NutrientParam='NicotinicAcid',IngredientNutrients.NutrientValue, 0 )) AS NicotinicAcid, " +
                              " SUM(IIF(NutrientParam='Pyridoxine',IngredientNutrients.NutrientValue, 0 )) AS Pyridoxine, " +
                              " SUM(IIF(NutrientParam='FolicAcid',IngredientNutrients.NutrientValue, 0 )) AS FolicAcid, " +
                              " SUM(IIF(NutrientParam='VitaminB12',IngredientNutrients.NutrientValue, 0)) AS VitaminB12, " +
                              " SUM(IIF(NutrientParam='VitaminC',IngredientNutrients.NutrientValue, 0 )) AS VitaminC, " +
                              " SUM(IIF(NutrientParam='Moisture',IngredientNutrients.NutrientValue, 0 )) AS Moisture, " +
                              " SUM(IIF(NutrientParam='Sodium',IngredientNutrients.NutrientValue, 0 )) AS Sodium, " +
                              " SUM(IIF(NutrientParam='Pottasium',IngredientNutrients.NutrientValue, 0 )) AS Pottasium, " +
                              " SUM(IIF(NutrientParam='Zinc',IngredientNutrients.NutrientValue, 0 )) AS Zinc " +
                              " FROM (Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
                              " GROUP BY Ingredient.IngredientID, Ingredient.IngredientName,Ingredient.DisplayName, Ingredient.Comments, Ingredient.MedicalFavourable," +
                              " Ingredient.MedicalUnFavourable,Ingredient.MedicalBalanced, Ingredient.ScrappageRate, Ingredient.WeightChangeRate, Ingredient.Keywords, Ingredient.DisplayImage, Ingredient.IsAllergic," +
                              " Ingredient.IsSystemIngredient, Ingredient.FrozenLife, Ingredient.RefrigeratedLife, Ingredient.ShelfLife, Ingredient.EthnicID, Ingredient.FoodHabitID, Ingredient.DisplayOrder,Ingredient.GeneralHealthValue," +
                              " Ingredient.AyurHealthValue,Ingredient.AyurvedicFavourable, Ingredient.AyurvedicUnFavourable,Ingredient.AyurvedicBalanced )ING " + searchString;

            //My SQL
            //string SqlQuery = "SELECT * FROM (SELECT Ingredient.IngredientID, Ingredient.IngredientName, Ingredient.DisplayName, Ingredient.Comments, Ingredient.MedicalFavourable," +
            //                  " Ingredient.MedicalUnFavourable,Ingredient.MedicalBalanced, Ingredient.ScrappageRate, Ingredient.WeightChangeRate, Ingredient.Keywords, Ingredient.DisplayImage, Ingredient.IsAllergic," +
            //                  " Ingredient.IsSystemIngredient, Ingredient.FrozenLife, Ingredient.RefrigeratedLife, Ingredient.ShelfLife, Ingredient.EthnicID, Ingredient.FoodHabitID, Ingredient.DisplayOrder,Ingredient.GeneralHealthValue," +
            //                  " Ingredient.AyurHealthValue,Ingredient.AyurvedicFavourable, Ingredient.AyurvedicUnFavourable,Ingredient.AyurvedicBalanced, " +
            //                  " (CASE WHEN NutrientParam = 'Calorie' THEN IngredientNutrients.NutrientValue END) AS Calorie, " +
            //                  " (CASE WHEN NutrientParam = 'Protien' THEN IngredientNutrients.NutrientValue END) AS Protien, " +
            //                  " (CASE WHEN NutrientParam = 'CarboHydrates' THEN IngredientNutrients.NutrientValue END) AS CarboHydrates, " +
            //                  " (CASE WHEN NutrientParam = 'Fat' THEN IngredientNutrients.NutrientValue END) AS Fat, " +
            //                  " (CASE WHEN NutrientParam = 'Fibre' THEN IngredientNutrients.NutrientValue END) AS Fibre, " +
            //                  " (CASE WHEN NutrientParam = 'Iron' THEN IngredientNutrients.NutrientValue END) AS Iron, " +
            //                  " (CASE WHEN NutrientParam = 'Calcium' THEN IngredientNutrients.NutrientValue END) AS Calcium, " +
            //                  " (CASE WHEN NutrientParam = 'Phosphorus' THEN IngredientNutrients.NutrientValue END) AS Phosphorus, " +
            //                  " (CASE WHEN NutrientParam = 'VitaminARetinol' THEN IngredientNutrients.NutrientValue END) AS VitaminARetinol, " +
            //                  " (CASE WHEN NutrientParam = 'VitaminABetaCarotene' THEN IngredientNutrients.NutrientValue END) AS VitaminABetaCarotene, " +
            //                  " (CASE WHEN NutrientParam = 'Thiamine' THEN IngredientNutrients.NutrientValue END) AS Thiamine, " +
            //                  " (CASE WHEN NutrientParam = 'Riboflavin' THEN IngredientNutrients.NutrientValue END) AS Riboflavin, " +
            //                  " (CASE WHEN NutrientParam = 'NicotinicAcid' THEN IngredientNutrients.NutrientValue END) AS NicotinicAcid, " +
            //                  " (CASE WHEN NutrientParam = 'Pyridoxine' THEN IngredientNutrients.NutrientValue END) AS Pyridoxine, " +
            //                  " (CASE WHEN NutrientParam = 'FolicAcid' THEN IngredientNutrients.NutrientValue END) AS FolicAcid, " +
            //                  " (CASE WHEN NutrientParam = 'VitaminB12' THEN IngredientNutrients.NutrientValue END) AS VitaminB12, " +
            //                  " (CASE WHEN NutrientParam = 'VitaminC' THEN IngredientNutrients.NutrientValue END) AS VitaminC, " +
            //                  " (CASE WHEN NutrientParam = 'Moisture' THEN IngredientNutrients.NutrientValue END) AS Moisture, " +
            //                  " (CASE WHEN NutrientParam = 'Sodium' THEN IngredientNutrients.NutrientValue END) AS Sodium, " +
            //                  " (CASE WHEN NutrientParam = 'Pottasium' THEN IngredientNutrients.NutrientValue END) AS Pottasium, " +
            //                  " (CASE WHEN NutrientParam = 'Zinc' THEN IngredientNutrients.NutrientValue END) AS Zinc " +
            //                  " FROM (Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
            //                  " GROUP BY Ingredient.IngredientID, Ingredient.IngredientName,Ingredient.DisplayName, Ingredient.Comments, Ingredient.MedicalFavourable," +
            //                  " Ingredient.MedicalUnFavourable,Ingredient.MedicalBalanced, Ingredient.ScrappageRate, Ingredient.WeightChangeRate, Ingredient.Keywords, Ingredient.DisplayImage, Ingredient.IsAllergic," +
            //                  " Ingredient.IsSystemIngredient, Ingredient.FrozenLife, Ingredient.RefrigeratedLife, Ingredient.ShelfLife, Ingredient.EthnicID, Ingredient.FoodHabitID, Ingredient.DisplayOrder,Ingredient.GeneralHealthValue," +
            //                  " Ingredient.AyurHealthValue,Ingredient.AyurvedicFavourable, Ingredient.AyurvedicUnFavourable,Ingredient.AyurvedicBalanced )ING " + searchString + " LIMIT 100";

            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drIngredient = dbManager.ExecuteReader(CommandType.Text, SqlQuery))
                {
                    while (drIngredient.Read())
                    {
                        ingredient = FillDataRecord(drIngredient);

                        if (ingredient != null)
                        {
                            ingredientList.Add(ingredient);
                        }
                    }
                    drIngredient.Close();
                }
                return ingredientList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }

        public static List<Ingredient> GetDetailedSearchList(string searchString, string searchOrderBy)
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            Ingredient ingredient = null;
            DBHelper dbManager = null;

            //string sqlQry = "SELECT Ingredient.IngredientID,Ingredient.EthnicID,Ingredient.IngredientName,Ingredient.DisplayName, Ingredient.FoodHabitID, " +
            //               " SUM((CASE WHEN NutrientParam = 'Calorie' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Calorie, " +
            //               " SUM((CASE WHEN NutrientParam = 'Protien' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Protien, " +
            //               " SUM((CASE WHEN NutrientParam = 'CarboHydrates' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS CarboHydrate, " +
            //               " SUM((CASE WHEN NutrientParam = 'Fat' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Fat, " +
            //               " SUM((CASE WHEN NutrientParam = 'Fibre' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Fibre," +
            //               " SUM((CASE WHEN NutrientParam = 'Iron' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Iron, " +
            //               " SUM((CASE WHEN NutrientParam = 'Calcium' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Calcium " +
            //               " FROM Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID WHERE NSys_Nutrient.IsNutrientMain = 1 AND " + searchString + " " +
            //               " GROUP BY Ingredient.IngredientID,Ingredient.EthnicID,Ingredient.IngredientName,Ingredient.DisplayName,Ingredient.FoodHabitID ORDER BY " + searchOrderBy;

            string sqlQry = "SELECT Ingredient.IngredientID,Ingredient.EthnicID,Ingredient.IngredientName,Ingredient.DisplayName, Ingredient.FoodHabitID, " +
                           " SUM(IIF(NutrientParam='Calorie',IngredientNutrients.NutrientValue, 0 )) AS Calorie, " +
                           " SUM(IIF(NutrientParam='Protien',IngredientNutrients.NutrientValue, 0 )) AS Protien, " +
                           " SUM(IIF(NutrientParam='CarboHydrates',IngredientNutrients.NutrientValue, 0 )) AS CarboHydrate, " +
                           " SUM(IIF(NutrientParam='Fat',IngredientNutrients.NutrientValue, 0 )) AS Fat, " +
                           " SUM(IIF(NutrientParam='Fibre',IngredientNutrients.NutrientValue, 0 )) AS Fibre, " +
                           " SUM(IIF(NutrientParam='Iron',IngredientNutrients.NutrientValue, 0 )) AS Iron, " +
                           " SUM(IIF(NutrientParam='Calcium',IngredientNutrients.NutrientValue, 0 )) AS Calcium " +
                           " FROM (Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID WHERE NSys_Nutrient.IsNutrientMain = true AND " + searchString + " " +
                           " GROUP BY Ingredient.IngredientID,Ingredient.EthnicID,Ingredient.IngredientName,Ingredient.DisplayName,Ingredient.FoodHabitID ORDER BY " + searchOrderBy;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drSerach = dbManager.ExecuteReader(CommandType.Text, sqlQry))
                {
                    while (drSerach.Read())
                    {
                        ingredient = FillDataSearch(drSerach);

                        if (ingredient != null)
                        {
                            ingredientList.Add(ingredient);
                        }
                    }
                    drSerach.Close();
                }
                return ingredientList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }

        public static List<Ingredient> GetIngredientList(string searchString)
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            Ingredient ingredient = null;
            DBHelper dbManager = null;

            string SqlQuery = "SELECT IngredientID, IngredientName FROM Ingredient " + searchString;

            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drIngredient = dbManager.ExecuteReader(CommandType.Text, SqlQuery))
                {
                    while (drIngredient.Read())
                    {
                        ingredient = FillComboDataRecord(drIngredient);

                        if (ingredient != null)
                        {
                            ingredientList.Add(ingredient);
                        }
                    }
                    drIngredient.Close();
                }
                return ingredientList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }

        public static List<Ingredient> GetListSearch(string searchString)
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            Ingredient ingredient = null;
            DBHelper dbManager = null;

            // MS Access
            string SqlQry = " SELECT Top(100) * FROM (SELECT " +
                             " SUM(IIF(NutrientParam='Calorie',IngredientNutrients.NutrientValue, 0 )) AS Calorie, " +
                              " SUM(IIF(NutrientParam='Protien',IngredientNutrients.NutrientValue, 0 )) AS Protien, " +
                              " SUM(IIF(NutrientParam='CarboHydrates',IngredientNutrients.NutrientValue, 0)) AS CarboHydrates, " +
                              " SUM(IIF(NutrientParam='Fat',IngredientNutrients.NutrientValue, 0 )) AS Fat, " +
                              " SUM(IIF(NutrientParam='Fibre',IngredientNutrients.NutrientValue, 0 )) AS Fibre, " +
                              " SUM(IIF(NutrientParam='Iron',IngredientNutrients.NutrientValue, 0 )) AS Iron, " +
                              " SUM(IIF(NutrientParam='Calcium',IngredientNutrients.NutrientValue, 0 )) AS Calcium, " +
                              " SUM(IIF(NutrientParam='Phosphorus',IngredientNutrients.NutrientValue, 0 )) AS Phosphorus, " +
                              " SUM(IIF(NutrientParam='VitaminARetinol',IngredientNutrients.NutrientValue, 0)) AS VitaminARetinol, " +
                              " SUM(IIF(NutrientParam='VitaminABetaCarotene',IngredientNutrients.NutrientValue, 0 )) AS VitaminABetaCarotene, " +
                              " SUM(IIF(NutrientParam='Thiamine',IngredientNutrients.NutrientValue, 0 )) AS Thiamine, " +
                              " SUM(IIF(NutrientParam='Riboflavin',IngredientNutrients.NutrientValue, 0 )) AS Riboflavin, " +
                              " SUM(IIF(NutrientParam='NicotinicAcid',IngredientNutrients.NutrientValue, 0 )) AS NicotinicAcid, " +
                              " SUM(IIF(NutrientParam='Pyridoxine',IngredientNutrients.NutrientValue, 0 )) AS Pyridoxine, " +
                              " SUM(IIF(NutrientParam='FolicAcid',IngredientNutrients.NutrientValue, 0 )) AS FolicAcid, " +
                              " SUM(IIF(NutrientParam='VitaminB12',IngredientNutrients.NutrientValue, 0)) AS VitaminB12, " +
                              " SUM(IIF(NutrientParam='VitaminC',IngredientNutrients.NutrientValue, 0 )) AS VitaminC, " +
                              " SUM(IIF(NutrientParam='Moisture',IngredientNutrients.NutrientValue, 0 )) AS Moisture, " +
                              " SUM(IIF(NutrientParam='Sodium',IngredientNutrients.NutrientValue, 0 )) AS Sodium, " +
                              " SUM(IIF(NutrientParam='Pottasium',IngredientNutrients.NutrientValue, 0 )) AS Pottasium, " +
                              " SUM(IIF(NutrientParam='Zinc',IngredientNutrients.NutrientValue, 0 )) AS Zinc " +
                             " FROM (Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID )ING " + searchString;

            //SQL Server
            //string SqlQry = " SELECT * FROM (SELECT " +
            //                " SUM((CASE WHEN NutrientParam = 'Calorie' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Calorie, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Protien' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Protien, " +
            //                 " SUM((CASE WHEN NutrientParam = 'CarboHydrates' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS CarboHydrates, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Fat' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Fat, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Fibre' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Fibre, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Iron' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Iron, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Calcium' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Calcium, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Phosphorus' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Phosphorus, " +
            //                 " SUM((CASE WHEN NutrientParam = 'VitaminARetinol' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS VitaminARetinol, " +
            //                 " SUM((CASE WHEN NutrientParam = 'VitaminABetaCarotene' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS VitaminABetaCarotene, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Thiamine' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Thiamine, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Riboflavin' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Riboflavin, " +
            //                 " SUM((CASE WHEN NutrientParam = 'NicotinicAcid' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS NicotinicAcid, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Pyridoxine' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Pyridoxine, " +
            //                 " SUM((CASE WHEN NutrientParam = 'FolicAcid' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS FolicAcid, " +
            //                 " SUM((CASE WHEN NutrientParam = 'VitaminB12' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS VitaminB12, " +
            //                 " SUM((CASE WHEN NutrientParam = 'VitaminC' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS VitaminC, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Moisture' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Moisture, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Sodium' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Sodium, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Pottasium' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Pottasium, " +
            //                 " SUM((CASE WHEN NutrientParam = 'Zinc' THEN IngredientNutrients.NutrientValue ELSE 0 END)) AS Zinc " +
            //                " FROM (Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID )ING " + searchString + " LIMIT 100";

            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drSerach = dbManager.ExecuteReader(CommandType.Text, SqlQry))
                {
                    while (drSerach.Read())
                    {
                        ingredient = FillDataRecordSearch(drSerach);

                        if (ingredient != null)
                        {
                            ingredientList.Add(ingredient);
                        }
                    }
                    drSerach.Close();
                }
                return ingredientList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }

        public static int GetID()
        {
            int IngredientID = 0;
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select IIF(ISNULL(Max(IngredientID)),0,Max(IngredientID)) + 1 From Ingredient";
                IngredientID = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;
                return IngredientID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
        }

        public static void Save(Ingredient ingredient)
        {
            string SqlQry = "";
            string sqlCondition;
            DBHelper dbHelper = null;
            try 
            {
                dbHelper = DBHelper.Instance;
                sqlCondition = "Select count(*) from Ingredient  WHERE IngredientID = " + ingredient.Id;
                if (GetCount(sqlCondition) > 0)
                {
                    SqlQry = "UPDATE Ingredient SET EthnicID = " + ingredient.EthnicID + ",FoodHabitID =" + ingredient.FoodHabitID + ",IngredientName = '" + Functions.ProperCase(ingredient.Name) + "',DisplayName = '" + Functions.ProperCase(ingredient.DisplayName) + "',Comments = '" + Functions.ReplaceChar(ingredient.Comments) + "',AyurvedicFavourable = '" + ingredient.AyurvedicFavourable + "',AyurvedicUnFavourable = '" + ingredient.AyurvedicUnFavourable + "', AyurvedicBalanced ='" + ingredient.AyurvedicBalanced + "'" +
                             ", MedicalFavourable ='" + ingredient.MedicalFavourable + "',MedicalUnFavourable = '" + ingredient.MedicalUnFavourable + "',MedicalBalanced = '" + ingredient.MedicalBalanced + "',Keywords ='" + ingredient.Keywords + "' ,ScrappageRate =" + ingredient.ScrappageRate + " ,WeightChangeRate = " + ingredient.WeightChangeRate + "" +
                             ",DisplayImage = '" + ingredient.DisplayImage + "',IsAllergic = " + ingredient.IsAllergic + ",IsSystemIngredient =" + ingredient.IsSystemIngredient + " ,FrozenLife = " + ingredient.FrozenLife + ",RefrigeratedLife = " + ingredient.RefrigeratedLife + ",ShelfLife =" + ingredient.ShelfLife + ",DisplayOrder =" + ingredient.DisplayOrder + "" +
                             ",GeneralHealthValue = '" + Functions.ReplaceChar(ingredient.GeneralHealthValue) + "',AyurHealthValue = '" + Functions.ReplaceChar(ingredient.AyurHealthValue) + "'" +
                             " WHERE IngredientID = " + ingredient.Id;
                }
                else
                {
                    SqlQry = "INSERT INTO Ingredient(IngredientID,EthnicID,FoodHabitID,IngredientName,DisplayName,Comments,AyurvedicFavourable,AyurvedicUnFavourable,AyurvedicBalanced,MedicalFavourable,MedicalUnFavourable,MedicalBalanced,Keywords,ScrappageRate,WeightChangeRate,DisplayImage,IsAllergic,IsSystemIngredient,FrozenLife,RefrigeratedLife,ShelfLife,DisplayOrder,GeneralHealthValue,AyurHealthValue)" +
                             "VALUES(" + ingredient.Id + ", " + ingredient.EthnicID + "," + ingredient.FoodHabitID + ",'" + Functions.ProperCase(ingredient.Name) + "','" + Functions.ProperCase(ingredient.DisplayName) + "','" + Functions.ReplaceChar(ingredient.Comments) + "','" + ingredient.AyurvedicFavourable + "','" + ingredient.AyurvedicUnFavourable + "','" + ingredient.AyurvedicBalanced + "','" + ingredient.MedicalFavourable + "','" + ingredient.MedicalUnFavourable + "','" + ingredient.MedicalBalanced + "'," +
                                     " '" + ingredient.Keywords + "'," + ingredient.ScrappageRate + "," + ingredient.WeightChangeRate + ",'" + ingredient.DisplayImage + "'," + ingredient.IsAllergic + "," + ingredient.IsSystemIngredient + "," + ingredient.FrozenLife + "," + ingredient.RefrigeratedLife + "," + ingredient.ShelfLife + "," + ingredient.DisplayOrder + ",'" + Functions.ReplaceChar(ingredient.GeneralHealthValue) + "','" + Functions.ReplaceChar(ingredient.AyurHealthValue) + "')";
                }
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
        }        

        public static int GetIngredientCount(Ingredient ingredient)
        {
            int ingredientCount;
            ingredientCount = GetCount("Select Count(*) from DishIngredient Where IngredientID = " + ingredient.Id);
            return ingredientCount;
        }

        public static int GetStandardUnitCount(Ingredient ingredient, int standardUnitID)
        {
            int unitCount;
            unitCount = GetCount("Select Count(*) from DishIngredient Where IngredientID = " + ingredient.Id + " AND StandardUnitID = " + standardUnitID);
            return unitCount;
        }
        
        private static int GetCount(string sqlQuery)
        {
            int Count = 0;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                Count = dbHelper.ExecuteScalar(CommandType.Text, sqlQuery) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, sqlQuery) : 0;
                return Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
        }
        
        public static bool DeleteIngredient(int ingredientID)
        {
            string SqlQry = "";
            //string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                //sqlCondition = "Select count(*) from Ingredient  WHERE IngredientID = " + ingredientID;
                //if (GetCount(sqlCondition) > 0)
                //{
                SqlQry = "Delete from Ingredient  WHERE IngredientID = " + ingredientID;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
                return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dbHelper = null;
            }
        }

        public static void SaveAllergic(int memberID, Ingredient ingredient)
        {
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Insert into IngredientFavourite (MemberID,IngredientID) Values (" + memberID + "," + ingredient.Id + ")";
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
        }

        public static void DeleteAllergic(int memberID, Ingredient ingredient)
        {
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Delete from IngredientFavourite where MemberID=" + memberID + " and IngredientID=" + ingredient.Id;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
        }

        private static Ingredient FillDataRecord(IDataReader dataReader)
        {
            string SqlQry;
            DBHelper dbHelper = null;
            dbHelper = DBHelper.Instance;
            
            
            
            Ingredient ingredient = new Ingredient();
            ingredient.Id = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));

            //SqlQry = "select comments from Ingredient where IngredientID=" + ingredient.Id;
            //DataTable dt=dbHelper.DataAdapter(CommandType.Text, SqlQry).Tables[0];
            //if (dt.Rows.Count > 0)
            //    ingredient.Comments = Convert.ToString(dt.Rows[0][0]);
            //else
            //    ingredient.Comments = "";

            ingredient.EthnicID = dataReader.IsDBNull(dataReader.GetOrdinal("EthnicID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("EthnicID"));
            ingredient.FoodHabitID = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("FoodHabitID"));
            ingredient.AyurvedicFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("AyurvedicFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurvedicFavourable"));
            ingredient.AyurvedicUnFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("AyurvedicUnFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurvedicUnFavourable"));
            ingredient.AyurvedicBalanced = dataReader.IsDBNull(dataReader.GetOrdinal("AyurvedicBalanced")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurvedicBalanced"));
            ingredient.MedicalFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("MedicalFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MedicalFavourable"));
            ingredient.MedicalUnFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("MedicalUnFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MedicalUnFavourable"));
            ingredient.MedicalBalanced = dataReader.IsDBNull(dataReader.GetOrdinal("MedicalBalanced")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MedicalBalanced"));
            ingredient.Keywords = dataReader.IsDBNull(dataReader.GetOrdinal("Keywords")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Keywords"));
            ingredient.ScrappageRate = dataReader.IsDBNull(dataReader.GetOrdinal("ScrappageRate")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ScrappageRate"));
            ingredient.WeightChangeRate = dataReader.IsDBNull(dataReader.GetOrdinal("WeightChangeRate")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("WeightChangeRate"));
            ingredient.DisplayImage = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayImage")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayImage"));
            ingredient.IsAllergic = dataReader.IsDBNull(dataReader.GetOrdinal("IsAllergic")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsAllergic"));
            ingredient.IsSystemIngredient = dataReader.IsDBNull(dataReader.GetOrdinal("IsSystemIngredient")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsSystemIngredient"));
            ingredient.FrozenLife = dataReader.IsDBNull(dataReader.GetOrdinal("FrozenLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("FrozenLife"));
            ingredient.RefrigeratedLife = dataReader.IsDBNull(dataReader.GetOrdinal("RefrigeratedLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("RefrigeratedLife"));
            ingredient.ShelfLife = dataReader.IsDBNull(dataReader.GetOrdinal("ShelfLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ShelfLife"));
            ingredient.Name = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("IngredientName"));
            ingredient.DisplayName = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayName"));

            if (ingredient.Name.Length > 13)
                ingredient.IconName = ingredient.Name.Substring(0, 13) + "..";
            else
                ingredient.IconName = ingredient.Name;

            
            //ingredient.Comments = dataReader.IsDBNull(dataReader.GetOrdinal("Comments")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Comments"));
            ingredient.DisplayOrder = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayOrder")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("DisplayOrder"));
            ingredient.GeneralHealthValue = dataReader.IsDBNull(dataReader.GetOrdinal("GeneralHealthValue")) ? "" : dataReader.GetString(dataReader.GetOrdinal("GeneralHealthValue"));
            ingredient.AyurHealthValue = dataReader.IsDBNull(dataReader.GetOrdinal("AyurHealthValue")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurHealthValue"));
            ingredient.Calorie = dataReader.IsDBNull(dataReader.GetOrdinal("Calorie")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calorie"));
            ingredient.CarboHydrate = dataReader.IsDBNull(dataReader.GetOrdinal("CarboHydrates")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("CarboHydrates"));
            ingredient.Fat = dataReader.IsDBNull(dataReader.GetOrdinal("Fat")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fat"));
            ingredient.Protien = dataReader.IsDBNull(dataReader.GetOrdinal("Protien")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Protien"));
            ingredient.Fibre = dataReader.IsDBNull(dataReader.GetOrdinal("Fibre")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fibre"));
            ingredient.Iron = dataReader.IsDBNull(dataReader.GetOrdinal("Iron")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Iron"));
            ingredient.Calcium = dataReader.IsDBNull(dataReader.GetOrdinal("Calcium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calcium"));
            ingredient.Phosphorus = dataReader.IsDBNull(dataReader.GetOrdinal("Phosphorus")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Phosphorus"));
            ingredient.VitaminARetinol = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminARetinol")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminARetinol"));
            ingredient.VitaminABetaCarotene = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminABetaCarotene")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminABetaCarotene"));
            ingredient.Thiamine = dataReader.IsDBNull(dataReader.GetOrdinal("Thiamine")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Thiamine"));
            ingredient.Riboflavin = dataReader.IsDBNull(dataReader.GetOrdinal("Riboflavin")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Riboflavin"));
            ingredient.NicotinicAcid = dataReader.IsDBNull(dataReader.GetOrdinal("NicotinicAcid")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NicotinicAcid"));
            ingredient.Pyridoxine = dataReader.IsDBNull(dataReader.GetOrdinal("Pyridoxine")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Pyridoxine"));
            ingredient.FolicAcid = dataReader.IsDBNull(dataReader.GetOrdinal("FolicAcid")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("FolicAcid"));
            ingredient.VitaminB12 = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminB12")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminB12"));
            ingredient.VitaminC = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminC")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminC"));
            ingredient.Moisture = dataReader.IsDBNull(dataReader.GetOrdinal("Moisture")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Moisture"));
            ingredient.Sodium = dataReader.IsDBNull(dataReader.GetOrdinal("Sodium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Sodium"));
            ingredient.Pottasium = dataReader.IsDBNull(dataReader.GetOrdinal("Pottasium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Pottasium"));
            ingredient.Zinc = dataReader.IsDBNull(dataReader.GetOrdinal("Zinc")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Zinc"));
            if (ingredient.DisplayImage != string.Empty)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Ingredients" + "\\" + ingredient.Id + ".jpg"))
                {
                    ingredient.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Ingredients" + "\\" + ingredient.Id + ".jpg";
                }
                else
                {
                    ingredient.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg";
                }
            }
            else
            {
                ingredient.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg";
            }
            return ingredient;
        }

        private static Ingredient FillDataSearch(IDataReader dataReader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Id = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
            ingredient.EthnicID = dataReader.IsDBNull(dataReader.GetOrdinal("EthnicID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("EthnicID"));
            ingredient.Name = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("IngredientName"));
            ingredient.DisplayName = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayName"));
            ingredient.Calorie = dataReader.IsDBNull(dataReader.GetOrdinal("Calorie")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calorie"));
            ingredient.CarboHydrate = dataReader.IsDBNull(dataReader.GetOrdinal("CarboHydrate")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("CarboHydrate"));
            ingredient.Fat = dataReader.IsDBNull(dataReader.GetOrdinal("Fat")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fat"));
            ingredient.Protien = dataReader.IsDBNull(dataReader.GetOrdinal("Protien")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Protien"));
            ingredient.Fibre = dataReader.IsDBNull(dataReader.GetOrdinal("Fibre")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fibre"));
            return ingredient;
        }

        private static Ingredient FillComboDataRecord(IDataReader dataReader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Id = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
            ingredient.Name = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("IngredientName"));
            if (ingredient.Name.Length > 13)
                ingredient.IconName = ingredient.Name.Substring(0, 13) + "..";
            else
                ingredient.IconName = ingredient.Name;

            if (ingredient.DisplayImage != string.Empty)
            {
                ingredient.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Ingredients" + "\\" + ingredient.Id + ".jpg";
            }
            return ingredient;
        }

        private static Ingredient FillDataRecordLAN(IDataReader dataReader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Id = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
            ingredient.EthnicID = dataReader.IsDBNull(dataReader.GetOrdinal("EthnicID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("EthnicID"));
            ingredient.FoodHabitID = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("FoodHabitID"));
            ingredient.AyurvedicFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("AyurvedicFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurvedicFavourable"));
            ingredient.AyurvedicUnFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("AyurvedicUnFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurvedicUnFavourable"));
            ingredient.AyurvedicBalanced = dataReader.IsDBNull(dataReader.GetOrdinal("AyurvedicBalanced")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurvedicBalanced"));
            ingredient.MedicalFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("MedicalFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MedicalFavourable"));
            ingredient.MedicalUnFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("MedicalUnFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MedicalUnFavourable"));
            ingredient.MedicalBalanced = dataReader.IsDBNull(dataReader.GetOrdinal("MedicalBalanced")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MedicalBalanced"));
            ingredient.Keywords = dataReader.IsDBNull(dataReader.GetOrdinal("Keywords")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Keywords"));
            ingredient.ScrappageRate = dataReader.IsDBNull(dataReader.GetOrdinal("ScrappageRate")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ScrappageRate"));
            ingredient.WeightChangeRate = dataReader.IsDBNull(dataReader.GetOrdinal("WeightChangeRate")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("WeightChangeRate"));
            ingredient.DisplayImage = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayImage")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayImage"));
            ingredient.IsAllergic = dataReader.IsDBNull(dataReader.GetOrdinal("IsAllergic")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsAllergic"));
            ingredient.IsSystemIngredient = dataReader.IsDBNull(dataReader.GetOrdinal("IsSystemIngredient")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsSystemIngredient"));
            ingredient.FrozenLife = dataReader.IsDBNull(dataReader.GetOrdinal("FrozenLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("FrozenLife"));
            ingredient.RefrigeratedLife = dataReader.IsDBNull(dataReader.GetOrdinal("RefrigeratedLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("RefrigeratedLife"));
            ingredient.ShelfLife = dataReader.IsDBNull(dataReader.GetOrdinal("ShelfLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ShelfLife"));
            ingredient.Name = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("IngredientName"));
            ingredient.DisplayName = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayName"));
            ingredient.Comments = dataReader.IsDBNull(dataReader.GetOrdinal("Comments")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Comments"));
            ingredient.DisplayOrder = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayOrder")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("DisplayOrder"));
            ingredient.GeneralHealthValue = dataReader.IsDBNull(dataReader.GetOrdinal("GeneralHealthValue")) ? "" : dataReader.GetString(dataReader.GetOrdinal("GeneralHealthValue"));
            ingredient.AyurHealthValue = dataReader.IsDBNull(dataReader.GetOrdinal("AyurHealthValue")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurHealthValue"));
            return ingredient;
        }
        
        private static Ingredient FillDataRecordSearch(IDataReader dataReader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.Id = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
            ingredient.Name = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("IngredientName"));
            ingredient.DisplayName = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayName"));
            ingredient.Calorie = dataReader.IsDBNull(dataReader.GetOrdinal("Calorie")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calorie"));
            ingredient.CarboHydrate = dataReader.IsDBNull(dataReader.GetOrdinal("CarboHydrates")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("CarboHydrates"));
            ingredient.Fat = dataReader.IsDBNull(dataReader.GetOrdinal("Fat")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fat"));
            ingredient.Protien = dataReader.IsDBNull(dataReader.GetOrdinal("Protien")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Protien"));
            ingredient.Fibre = dataReader.IsDBNull(dataReader.GetOrdinal("Fibre")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fibre"));
            ingredient.Iron = dataReader.IsDBNull(dataReader.GetOrdinal("Iron")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Iron"));
            ingredient.Calcium = dataReader.IsDBNull(dataReader.GetOrdinal("Calcium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calcium"));
            ingredient.Phosphorus = dataReader.IsDBNull(dataReader.GetOrdinal("Phosphorus")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Phosphorus"));
            ingredient.VitaminARetinol = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminARetinol")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminARetinol"));
            ingredient.VitaminABetaCarotene = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminABetaCarotene")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminABetaCarotene"));
            ingredient.Thiamine = dataReader.IsDBNull(dataReader.GetOrdinal("Thiamine")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Thiamine"));
            ingredient.Riboflavin = dataReader.IsDBNull(dataReader.GetOrdinal("Riboflavin")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Riboflavin"));
            ingredient.NicotinicAcid = dataReader.IsDBNull(dataReader.GetOrdinal("NicotinicAcid")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NicotinicAcid"));
            ingredient.Pyridoxine = dataReader.IsDBNull(dataReader.GetOrdinal("Pyridoxine")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Pyridoxine"));
            ingredient.FolicAcid = dataReader.IsDBNull(dataReader.GetOrdinal("FolicAcid")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("FolicAcid"));
            ingredient.VitaminB12 = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminB12")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminB12"));
            ingredient.VitaminC = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminC")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminC"));
            ingredient.Moisture = dataReader.IsDBNull(dataReader.GetOrdinal("Moisture")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Moisture"));
            ingredient.Sodium = dataReader.IsDBNull(dataReader.GetOrdinal("Sodium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Sodium"));
            ingredient.Pottasium = dataReader.IsDBNull(dataReader.GetOrdinal("Pottasium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Pottasium"));
            ingredient.Zinc = dataReader.IsDBNull(dataReader.GetOrdinal("Zinc")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Zinc"));
            if (ingredient.DisplayImage != string.Empty)
            {
                ingredient.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Ingredients" + "\\" + ingredient.Id + ".jpg";
            }

            return ingredient;
        }
        
    }
}
