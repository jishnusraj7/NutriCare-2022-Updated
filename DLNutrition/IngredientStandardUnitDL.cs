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
    public class IngredientStandardUnitDL
    {

        #region Ingredient StandardUnit       

        public static List<IngredientStandardUnit> GetUnitList(int ingredientID)
        {
            List<IngredientStandardUnit> ingredientUnitList = new List<IngredientStandardUnit>();
            IngredientStandardUnit ingredientUnit = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drIngredientUnit = dbManager.ExecuteReader(CommandType.Text, "SELECT IngredientStandardUnit.IngredientID, IngredientStandardUnit.StandardUnitID, IngredientStandardUnit.StandardWeight,StandardUnit.StandardUnitType, StandardUnit.StandardUnitName, StandardUnit.StandardUnitDisplay FROM IngredientStandardUnit INNER JOIN StandardUnit ON IngredientStandardUnit.StandardUnitID = StandardUnit.StandardUnitID Where IngredientStandardUnit.IngredientID = " + ingredientID))
                {
                    while (drIngredientUnit.Read())
                    {
                        ingredientUnit = FillDataRecordUnitList(drIngredientUnit);
                        if (ingredientUnit != null)
                        {
                            ingredientUnitList.Add(ingredientUnit);
                        }
                    }
                    drIngredientUnit.Close();
                }
                return ingredientUnitList;
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

        public static IngredientStandardUnit GetItem(int ingredientID,byte standardUnitID)
        {
            IngredientStandardUnit ingredientUnit = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drIngredientUnit = dbManager.ExecuteReader(CommandType.Text, "Select IngredientStandardUnit.IngredientID, IngredientStandardUnit.StandardUnitID, IngredientStandardUnit.StandardWeight,StandardUnit.StandardUnitType, StandardUnit.StandardUnitName, StandardUnit.StandardUnitDisplay, Ingredient.WeightChangeRate " +
                                                                                                "FROM (IngredientStandardUnit INNER JOIN StandardUnit ON IngredientStandardUnit.StandardUnitID = StandardUnit.StandardUnitID) INNER JOIN Ingredient ON Ingredient.IngredientID = IngredientStandardUnit.IngredientID " +
                                                                                                "Where IngredientStandardUnit.IngredientID = " + ingredientID + " AND StandardUnit.StandardUnitID = " + standardUnitID))
                {
                    if (drIngredientUnit.Read())
                    {
                        ingredientUnit = FillDataRecordUnit(drIngredientUnit);
                    }
                    drIngredientUnit.Close();
                }
                return ingredientUnit;
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

        public static IngredientStandardUnit GetIngredientUnitItem(int ingredientID, byte standardUnitID)
        {
            IngredientStandardUnit ingredientUnit = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drIngredientUnit = dbManager.ExecuteReader(CommandType.Text, "SELECT StandardUnit.StandardUnitID, StandardUnit.StandardUnitName, StandardUnit.StandardUnitDisplay, StandardUnit.StandardUnitType, Ingredient.WeightChangeRate, IngredientStandardUnit.IngredientID, IngredientStandardUnit.StandardWeight (StandardUnit INNER JOIN IngredientStandardUnit ON StandardUnit.StandardUnitID = IngredientStandardUnit.StandardUnitID) INNER JOIN Ingredient ON IngredientStandardUnit.IngredientID = Ingredient.IngredientID Where Ingredient.IngredientID = " + ingredientID + " AND StandardUnit.StandardUnitID = " + standardUnitID))
                {
                    if (drIngredientUnit.Read())
                    {
                        ingredientUnit = FillDataRecordIngredientUnit(drIngredientUnit);
                    }
                    drIngredientUnit.Close();
                }
                return ingredientUnit;
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

        public static void Save(IngredientStandardUnit ingredientStandardUnit)
        {
            string SqlQry = "";
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "INSERT INTO IngredientStandardUnit(IngredientID,StandardUnitID,StandardWeight)VALUES(" + ingredientStandardUnit.IngredientID + "," + ingredientStandardUnit.StandardUnitID + "," + ingredientStandardUnit.StandardWeight + ")";
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

        public static bool DeleteStandardUnit(int ingredientID)
        {
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "DELETE from IngredientStandardUnit WHERE IngredientID = " + ingredientID;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                dbHelper = null;
            }
        }

        private static IngredientStandardUnit FillDataRecordUnit(IDataReader dataReader)
        {
            IngredientStandardUnit ingredientStandardUnit = new IngredientStandardUnit();
            ingredientStandardUnit.IngredientID = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
            ingredientStandardUnit.StandardUnitID = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitID"));
            ingredientStandardUnit.StandardWeight = dataReader.IsDBNull(dataReader.GetOrdinal("StandardWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("StandardWeight"));
            ingredientStandardUnit.StandardUnitName = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitName")) ? "" : " " + dataReader.GetString(dataReader.GetOrdinal("StandardUnitName"));
            ingredientStandardUnit.StandardUnitDisplay = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitDisplay")) ? "" : " " + dataReader.GetString(dataReader.GetOrdinal("StandardUnitDisplay"));
            ingredientStandardUnit.StandardUnitType = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitType")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitType"));
            ingredientStandardUnit.WeightChangerate = dataReader.IsDBNull(dataReader.GetOrdinal("WeightChangeRate")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("WeightChangeRate"));
            ingredientStandardUnit.IsApplicable = true;
            return ingredientStandardUnit;
        }

        private static IngredientStandardUnit FillDataRecordUnitList(IDataReader dataReader)
        {
            IngredientStandardUnit ingredientStandardUnit = new IngredientStandardUnit();
            ingredientStandardUnit.IngredientID = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
            ingredientStandardUnit.StandardUnitID = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitID"));
            ingredientStandardUnit.StandardWeight = dataReader.IsDBNull(dataReader.GetOrdinal("StandardWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("StandardWeight"));
            ingredientStandardUnit.StandardUnitName = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitName")) ? "" : " " + dataReader.GetString(dataReader.GetOrdinal("StandardUnitName"));
            ingredientStandardUnit.StandardUnitDisplay = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitDisplay")) ? "" : " " + dataReader.GetString(dataReader.GetOrdinal("StandardUnitDisplay"));
            ingredientStandardUnit.StandardUnitType = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitType")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitType"));
            ingredientStandardUnit.IsApplicable = true;
            return ingredientStandardUnit;
        }

        private static IngredientStandardUnit FillDataRecordIngredientUnit(IDataReader dataReader)
        {
            IngredientStandardUnit ingredientStandardUnit = new IngredientStandardUnit();
            ingredientStandardUnit.IngredientID = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
            ingredientStandardUnit.StandardUnitID = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitID"));
            ingredientStandardUnit.StandardWeight = dataReader.IsDBNull(dataReader.GetOrdinal("StandardWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("StandardWeight"));
            ingredientStandardUnit.StandardUnitName = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitName")) ? "" : " " + dataReader.GetString(dataReader.GetOrdinal("StandardUnitName"));
            ingredientStandardUnit.StandardUnitDisplay = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitDisplay")) ? "" : " " + dataReader.GetString(dataReader.GetOrdinal("StandardUnitDisplay"));
            ingredientStandardUnit.StandardUnitType = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitType")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitType"));
            ingredientStandardUnit.WeightChangerate = dataReader.IsDBNull(dataReader.GetOrdinal("WeightChangeRate")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("WeightChangeRate"));
            ingredientStandardUnit.IsApplicable = true;
            return ingredientStandardUnit;
        }

        #endregion

    }
}
