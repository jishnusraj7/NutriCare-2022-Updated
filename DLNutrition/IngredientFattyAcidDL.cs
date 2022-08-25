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
    public class IngredientFattyAcidDL
    {
        #region Ingredient FattyAcid        
        
        public static List<IngredientFattyAcid> GetListNutrientValues(int ingredientID)
        {
            List<IngredientFattyAcid> ingredientFattyList = new List<IngredientFattyAcid>();
            IngredientFattyAcid ingredientFatty = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientFatty = dbManager.ExecuteReader(CommandType.Text, "SELECT IngredientFattyAcid.IngredientID,IngredientFattyAcid.NutrientID,IngredientFattyAcid.NutrientValue AS NutValue,NSys_Nutrient.NutrientParam,NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM IngredientFattyAcid INNER JOIN NSys_Nutrient ON IngredientFattyAcid.NutrientID = NSys_Nutrient.NutrientID Where IngredientFattyAcid.IngredientID = " + ingredientID + " ORDER BY IngredientFattyAcid.NutrientID"))
                {
                    while (dringredientFatty.Read())
                    {
                        ingredientFatty = FillDataRecordNutrientValues(dringredientFatty);
                        if (ingredientFatty != null)
                        {
                            ingredientFattyList.Add(ingredientFatty);
                        }
                    }
                    dringredientFatty.Close();
                }
                return ingredientFattyList;
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

        public static List<IngredientFattyAcid> GetFattyAcidList(int ingredientID, int DishId)
        {
            List<IngredientFattyAcid> ingredientFattyList = new List<IngredientFattyAcid>();
            IngredientFattyAcid ingredientFatty = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientFatty = dbManager.ExecuteReader(CommandType.Text, Views.V_IngredientFattyAcid() + " Where DishID = " + DishId + " and IngredientID=" + ingredientID))
                {
                    while (dringredientFatty.Read())
                    {
                        ingredientFatty = FillDataRecordNutrientValues(dringredientFatty);
                        if (ingredientFatty != null)
                        {
                            ingredientFattyList.Add(ingredientFatty);
                        }
                    }
                    dringredientFatty.Close();
                }
                return ingredientFattyList;
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

        public static List<IngredientFattyAcid> GetListNutrientsDish(int DishID, int PlanID)
        {
            List<IngredientFattyAcid> ingredientFattyList = new List<IngredientFattyAcid>();
            IngredientFattyAcid ingredientFatty = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                string sqlQuery = "";
                switch (PlanID)
                {
                    case 0:
                        sqlQuery = "Select * from " + Views.V_FattyDishValuePlanI() + " Where DishID = " + DishID + " Order By NutrientId";
                        break;
                    case 1:
                        sqlQuery = "Select * from " + Views.V_FattyDishValuePlanII() + " Where DishID = " + DishID + " Order By NutrientId";
                        break;
                    case 2:
                        sqlQuery = "Select * from " + Views.V_FattyDishValuePlanIII() + " Where DishID = " + DishID + " Order By NutrientId";
                        break;
                }
                using (IDataReader dringredientFatty = dbManager.ExecuteReader(CommandType.Text, sqlQuery))
                {
                    while (dringredientFatty.Read())
                    {
                        ingredientFatty = FillDataRecordNutrientValuesDish(dringredientFatty);
                        if (ingredientFatty != null)
                        {
                            ingredientFattyList.Add(ingredientFatty);
                        }
                    }
                    dringredientFatty.Close();
                }
                return ingredientFattyList;
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
       
        public static IngredientFattyAcid GetItemNutrients(int ingredientID, int nutrientID)
        {
            IngredientFattyAcid ingredientFatty = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientFatty = dbManager.ExecuteReader(CommandType.Text, "Select IngredientId,NutrientID,NutrientValue AS NutValue FROM IngredientFattyAcid Where IngredientID = " + ingredientID + " AND NutrientID = " + nutrientID))
                {
                    if (dringredientFatty.Read())
                    {
                        ingredientFatty = FillDataRecordNutrientValues(dringredientFatty);
                    }
                    dringredientFatty.Close();
                }
                return ingredientFatty;
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

        public static List<IngredientFattyAcid> GetFattyAcidsList(int dishID, int plan)
        {
            List<IngredientFattyAcid> dishIngredientList = new List<IngredientFattyAcid>();
            IngredientFattyAcid dishIngredient = null;
            DBHelper dbManager = null;
            string sqlQry = "";

            if (plan == 0)
            {
                sqlQry = "Select * From " + Views.V_FattyDishValuePlan0() + " Where DishID = " + dishID + " ORDER BY NutrientID";
            }
            else if (plan == 1)
            {
                sqlQry = "Select * From " + Views.V_FattyDishValuePlanI() + " Where DishID = " + dishID + " ORDER BY NutrientID";
            }
            else if (plan == 2)
            {
                sqlQry = "Select * From " + Views.V_FattyDishValuePlanII() + " Where DishID = " + dishID + " ORDER BY NutrientID";
            }
            else if (plan == 3)
            {
                sqlQry = "Select * From " + Views.V_FattyDishValuePlanIII() + " Where DishID = " + dishID + " ORDER BY NutrientID";
            }

            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drDishIngredient = dbManager.ExecuteReader(CommandType.Text, sqlQry))
                {
                    while (drDishIngredient.Read())
                    {
                        dishIngredient = FillDataRecordNutrients(drDishIngredient);
                        if (dishIngredient != null)
                        {
                            dishIngredientList.Add(dishIngredient);
                        }
                    }
                    drDishIngredient.Close();
                }
                return dishIngredientList;
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

        public static void SaveNutrientValues(IngredientFattyAcid ingredientFatty)
        {
            string SqlQry = "";
            string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                sqlCondition = "Select count(*) from IngredientFattyAcid WHERE IngredientID = " + ingredientFatty.IngredientId + " AND NutrientID = " + ingredientFatty.NutrientID;
                if (GetCount(sqlCondition) > 0)
                {
                    SqlQry = "UPDATE IngredientFattyAcid SET NutrientValue = " + ingredientFatty.NutrientValue + " WHERE IngredientID = " + ingredientFatty.IngredientId + " AND NutrientID = " + ingredientFatty.NutrientID;
                }
                else
                {
                    SqlQry = "INSERT INTO IngredientFattyAcid(IngredientID,NutrientID,NutrientValue)VALUES(" + ingredientFatty.IngredientId + "," + ingredientFatty.NutrientID + "," + ingredientFatty.NutrientValue + ")";
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

        public static bool DeleteFattyAcids(int ingredientID)
        {
            string SqlQry = "";
            //string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                //sqlCondition = "Select count(*) from IngredientFattyAcid  WHERE IngredientID = " + ingredientID;
                //if (GetCount(sqlCondition) > 0)
                //{
                SqlQry = "Delete from IngredientFattyAcid WHERE IngredientID = " + ingredientID;
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

        private static IngredientFattyAcid FillDataRecordNutrients(IDataReader dataReader)
        {
            IngredientFattyAcid dishIngredient = new IngredientFattyAcid();
            dishIngredient.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            dishIngredient.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            dishIngredient.NutrientValue = dataReader.IsDBNull(dataReader.GetOrdinal("NutValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NutValue"));
            dishIngredient.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            return dishIngredient;
        }
        
        private static IngredientFattyAcid FillDataRecordNutrientValues(IDataReader dataReader)
        {
            IngredientFattyAcid ingredientFatty = new IngredientFattyAcid();
            ingredientFatty.IngredientId = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientId"));
            ingredientFatty.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            ingredientFatty.NutrientValue = dataReader.IsDBNull(dataReader.GetOrdinal("NutValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NutValue"));
            ingredientFatty.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            ingredientFatty.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            ingredientFatty.NutrientGroup = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientGroup")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientGroup"));
            return ingredientFatty;
        }

        private static IngredientFattyAcid FillDataRecordNutrientValuesDish(IDataReader dataReader)
        {
            IngredientFattyAcid ingredientFatty = new IngredientFattyAcid();
            ingredientFatty.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            ingredientFatty.NutrientValue = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NutrientValue"));
            ingredientFatty.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            ingredientFatty.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            ingredientFatty.NutrientGroup = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientGroup")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientGroup"));
            return ingredientFatty;
        }

        #endregion        
    }
}
