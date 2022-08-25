using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BONutrition;
using NutritionV1.Classes;
using NutritionViews;
namespace DLNutrition
{
    public class CalorieCalculatorDL
    {
        public static int GetEnergyRequirement(int Age, int SexID)
        {
            int SysEnergyRequirement = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select EnergyRequirement From NSysEnergyRequirement Where Age = " + Age + " And SexID = " + SexID;
                SysEnergyRequirement = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return SysEnergyRequirement;
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

        public static int GetBMIPercentile(int Age, int SexID, double BMI)
        {
            int SysBMIPercentile = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select BMIPercentile from NSysBMIPercentile Where Age = " + Age + " and SexID = " + SexID + " and (" + BMI + " between BMIFrom and BMITo)";
                SysBMIPercentile = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return SysBMIPercentile;
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

        public static double[] GetBMIIdealRange(int Age, int SexID)
        {
            double[] BMIIdealRange = new double[2];
            string SqlQry;
            DBHelper dbHelper = null;
            DataSet dsBMIIdealRange;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select BMIFrom,BMITo from NSysBMIPercentile Where Age = " + Age + " and SexID = " + SexID + " and BMIPercentile  = 50";
                dsBMIIdealRange = dbHelper.DataAdapter(CommandType.Text, SqlQry);

                if (dsBMIIdealRange.Tables.Count > 0)
                {
                    if (dsBMIIdealRange.Tables[0].Rows.Count > 0)
                    {
                        BMIIdealRange[0] = Functions.Convert2Double(Convert.ToString(dsBMIIdealRange.Tables[0].Rows[0][0]));
                        BMIIdealRange[1] = Functions.Convert2Double(Convert.ToString(dsBMIIdealRange.Tables[0].Rows[0][1]));
                    }
                }

                return BMIIdealRange;
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

        public static double GetMeanHeight(int Age, int SexID)
        {
            double MeanHeight = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select MeanHeight From NSysMeanHeight Where Age = " + Age + " And SexID = " + SexID;
                MeanHeight = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (float)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return MeanHeight;
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

        public static double GetMeanWeight(int Age, int SexID)
        {
            double MeanWeight = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select MeanWeight From NSysMeanWeight Where Age = " + Age + " And SexID = " + SexID;
                MeanWeight = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (double)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return MeanWeight;
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

        public static List<CalorieCalculator> GetDishIngredient(int dishId)
        {
            List<CalorieCalculator> CalorieCalculatorList = new List<CalorieCalculator>();
            CalorieCalculator CalorieCalculator = null;
            DBHelper dbManager = null;

            string sqlQry = "SELECT Ingredient.IngredientID,Ingredient.EthnicID, Ingredient.IngredientName,Ingredient.DisplayName, DishIngredient.Quantity, DishIngredient.DisplayOrder,StandardUnit.StandardUnitName,Ingredient.FoodHabitID,DishIngredient.DishID, " +
                            " SUM(IIF(NutrientParam='Calorie',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Calorie, " +
                            " SUM(IIF(NutrientParam='Protien',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Protien, " +
                            " SUM(IIF(NutrientParam='CarboHydrates',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS CarboHydrate, " +
                            " SUM(IIF(NutrientParam='Fat',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Fat, " +
                            " SUM(IIF(NutrientParam='Fibre',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Fibre, " +
                            " SUM(IIF(NutrientParam='Iron',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Iron, " +
                            " SUM(IIF(NutrientParam='Calcium',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Calcium " +
                            " FROM StandardUnit INNER JOIN ((((Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient "+
                            " ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID) INNER JOIN DishIngredient ON Ingredient.IngredientID = DishIngredient.IngredientID) INNER JOIN IngredientStandardUnit "+
                            " ON Ingredient.IngredientID = IngredientStandardUnit.IngredientID) ON (DishIngredient.StandardUnitID = StandardUnit.StandardUnitID) AND (StandardUnit.StandardUnitID = IngredientStandardUnit.StandardUnitID) "+
                            " WHERE NSys_Nutrient.IsNutrientMain = true AND DishIngredient.DishID = " + dishId + " GROUP BY Ingredient.IngredientID,Ingredient.EthnicID, Ingredient.IngredientName,Ingredient.DisplayName, DishIngredient.Quantity, "+
                            " DishIngredient.DisplayOrder,StandardUnit.StandardUnitName,Ingredient.FoodHabitID,DishIngredient.DishID Order By DishIngredient.DisplayOrder ";
            try
            {
                dbManager = DBHelper.Instance;

                using (IDataReader drdishIngredient = dbManager.ExecuteReader(CommandType.Text, sqlQry))
                {
                    while (drdishIngredient.Read())
                    {
                        CalorieCalculator = FillDataDishIngredient(drdishIngredient);

                        if (CalorieCalculator != null)
                        {
                            CalorieCalculatorList.Add(CalorieCalculator);
                        }
                    }
                    drdishIngredient.Close();
                }
                return CalorieCalculatorList;
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

        private static CalorieCalculator FillDataDishIngredient(IDataReader dataReader)
        {
            CalorieCalculator calorieCalculator = new CalorieCalculator();
            calorieCalculator.IngredientId = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
            calorieCalculator.DishID = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            calorieCalculator.DisplayName = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayName"));
            calorieCalculator.Quantity = dataReader.IsDBNull(dataReader.GetOrdinal("Quantity")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Quantity"));
            calorieCalculator.Unit = dataReader.IsDBNull(dataReader.GetOrdinal("Unit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Unit"));
            calorieCalculator.Calorie = dataReader.IsDBNull(dataReader.GetOrdinal("Calorie")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calorie"));
            calorieCalculator.CarboHydrate = dataReader.IsDBNull(dataReader.GetOrdinal("CarboHydrates")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("CarboHydrates"));
            calorieCalculator.Fat = dataReader.IsDBNull(dataReader.GetOrdinal("Fat")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fat"));
            calorieCalculator.Protien = dataReader.IsDBNull(dataReader.GetOrdinal("Protien")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Protien"));
            calorieCalculator.Iron = dataReader.IsDBNull(dataReader.GetOrdinal("Iron")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Iron"));
            calorieCalculator.Calcium = dataReader.IsDBNull(dataReader.GetOrdinal("Calcium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calcium"));
            return calorieCalculator;
        }

        public static DataSet GetDish(int DishID)
        {
            DataSet dsDish = new DataSet();
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                string sqlQry = "SELECT DishID, DishName,DisplayName, DishRecipe, DishRemarks,DishAyurFeatures, MedicalFavourable, MedicalUnFavourable, Calorie, Protien, FAT, CreatedDate,Iron,Calcium,Phosphorus,DisplayImage, " +
                                " DishCategoryID, EthnicID, Keywords, CarboHydrates, Fibre, IsSystemDish, IsAyurvedic, IsCountable, ItemCount, ServeCount,ServeUnit,CookingTime, FrozenLife, RefrigeratedLife, ShelfLife, "+
                                " VitaminARetinol, VitaminABetaCarotene, Thiamine, Riboflavin, NicotinicAcid, Pyridoxine, FolicAcid, VitaminB12, VitaminC, ServeCount1,ServeCount2,StandardWeight1,StandardWeight2, "+
                                " FoodHabitID, AyurvedicFavourable, AyurvedicUnFavourable, AuthorID,StandardWeight,DishWeight FROM Dish Where DishID = " + DishID;

                dsDish = dbManager.DataAdapter(CommandType.Text, sqlQry);

                return dsDish;
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

        public static DataSet GetDish(int DishID, float Plan)
        {
            DataSet dsDish = new DataSet();
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                string sqlQry = " SELECT DishID, DishName,DisplayName, DishRemarks, DishRecipe, DishRemarks,DishAyurFeatures, MedicalFavourable, MedicalUnFavourable,CreatedDate,DisplayImage, " +
                                " (Calorie / 100) * " + Plan + " As Calorie, (Protien / 100) * " + Plan + " As Protien,(CarboHydrates / 100) * " + Plan + " As CarboHydrates, (Fibre / 100) * " + Plan + " As Fibre, (FAT / 100) * " + Plan + " As FAT, (Iron / 100) * " + Plan + " As Iron,(Calcium / 100) * " + Plan + " As Calcium,(Phosphorus / 100) * " + Plan + " As Phosphorus,(VitaminARetinol / 100) * " + Plan + " As VitaminARetinol, (VitaminABetaCarotene / 100) * " + Plan + " As VitaminABetaCarotene, " +
                                " (Thiamine / 100) * " + Plan + " As Thiamine, (Riboflavin / 100) * " + Plan + " As Riboflavin, (NicotinicAcid / 100) * " + Plan + " As NicotinicAcid, (Pyridoxine / 100) * " + Plan + " As Pyridoxine, (FolicAcid / 100) * " + Plan + " As FolicAcid, (VitaminB12 / 100) * " + Plan + " As VitaminB12, (VitaminC / 100) * " + Plan + " As VitaminC, " +
                                " DishCategoryID, EthnicID, Keywords, IsSystemDish, IsAyurvedic, IsCountable, ItemCount, ServeCount,ServeUnit,CookingTime, FrozenLife, RefrigeratedLife, ShelfLife, " +
                                " ServeCount1,ServeCount2,StandardWeight1,StandardWeight2, " +
                                " FoodHabitID, AyurvedicFavourable, AyurvedicUnFavourable, AuthorID,StandardWeight,DishWeight FROM Dish " +
                                "  Where  DishID = " + DishID ;

                dsDish = dbManager.DataAdapter(CommandType.Text, sqlQry);

                return dsDish;
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

        public static DataSet GetIngredient(int DishID)
        {
            DataSet dsDish = new DataSet();
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;

                string sqlQry = "SELECT Ingredient.IngredientID,Ingredient.EthnicID, Ingredient.IngredientName,Ingredient.DisplayName, DishIngredient.Quantity, DishIngredient.DisplayOrder,StandardUnit.StandardUnitName,Ingredient.FoodHabitID,DishIngredient.DishID, " +
                                " SUM(IIF(NutrientParam='Calorie',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Calorie, " +
                                " SUM(IIF(NutrientParam='Protien',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Protien, " +
                                " SUM(IIF(NutrientParam='CarboHydrates',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS CarboHydrate, " +
                                " SUM(IIF(NutrientParam='Fat',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Fat, " +
                                " SUM(IIF(NutrientParam='Fibre',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Fibre, " +
                                " SUM(IIF(NutrientParam='Iron',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Iron, " +
                                " SUM(IIF(NutrientParam='Calcium',((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity), 0 )) AS Calcium " +
                                " FROM StandardUnit INNER JOIN ((((Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient " +
                                " ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID) INNER JOIN DishIngredient ON Ingredient.IngredientID = DishIngredient.IngredientID) INNER JOIN IngredientStandardUnit " +
                                " ON Ingredient.IngredientID = IngredientStandardUnit.IngredientID) ON (DishIngredient.StandardUnitID = StandardUnit.StandardUnitID) AND (StandardUnit.StandardUnitID = IngredientStandardUnit.StandardUnitID) " +
                                " WHERE NSys_Nutrient.IsNutrientMain = true AND DishIngredient.DishID = " + DishID + " " +
                                " GROUP BY Ingredient.IngredientID,Ingredient.EthnicID, Ingredient.IngredientName,Ingredient.DisplayName, DishIngredient.Quantity, DishIngredient.DisplayOrder,StandardUnit.StandardUnitName,Ingredient.FoodHabitID,DishIngredient.DishID Order By DishIngredient.DisplayOrder";

                dsDish = dbManager.DataAdapter(CommandType.Text, sqlQry);

                return dsDish;
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
    }
}
