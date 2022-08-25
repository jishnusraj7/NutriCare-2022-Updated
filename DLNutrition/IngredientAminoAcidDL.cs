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
    public class IngredientAminoAcidDL
    {
        #region Ingredient AminoAcid               

        public static List<IngredientAminoAcid> GetListNutrientValues(int ingredientID)
        {
            List<IngredientAminoAcid> ingredientAminoList = new List<IngredientAminoAcid>();
            IngredientAminoAcid ingredientAmino = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientAmino = dbManager.ExecuteReader(CommandType.Text, "SELECT IngredientAminoAcid.IngredientID,IngredientAminoAcid.NutrientID,IngredientAminoAcid.NutrientValue AS NutValue,NSys_Nutrient.NutrientParam,NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM IngredientAminoAcid INNER JOIN NSys_Nutrient ON IngredientAminoAcid.NutrientID = NSys_Nutrient.NutrientID Where IngredientAminoAcid.IngredientID = " + ingredientID + " ORDER BY IngredientAminoAcid.NutrientID"))
                {
                    while (dringredientAmino.Read())
                    {
                        ingredientAmino = FillDataRecordNutrientValues(dringredientAmino);
                        if (ingredientAmino != null)
                        {
                            ingredientAminoList.Add(ingredientAmino);
                        }
                    }
                    dringredientAmino.Close();
                }
                return ingredientAminoList;
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

        public static List<IngredientAminoAcid> GetAminoAcidList(int ingredientID, int DishId)
        {
            List<IngredientAminoAcid> ingredientAminoList = new List<IngredientAminoAcid>();
            IngredientAminoAcid ingredientAmino = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientAmino = dbManager.ExecuteReader(CommandType.Text, Views.V_IngredientAminoAcid() + " Where DishID = " + DishId + " and IngredientID=" + ingredientID))
                {
                    while (dringredientAmino.Read())
                    {
                        ingredientAmino = FillDataRecordNutrientValues(dringredientAmino);
                        if (ingredientAmino != null)
                        {
                            ingredientAminoList.Add(ingredientAmino);
                        }
                    }
                    dringredientAmino.Close();
                }
                return ingredientAminoList;
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

        public static List<IngredientAminoAcid> GetListNutrientsDish(int DishID, int PlanID)
        {
            List<IngredientAminoAcid> ingredientAminoList = new List<IngredientAminoAcid>();
            IngredientAminoAcid ingredientAmino = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                string sqlQuery = "";
                switch (PlanID)
                {
                    case 0:
                        sqlQuery = "Select * from " + Views.V_AminoDishValuePlanI() + " Where DishID = " + DishID + " Order By NutrientId";
                        break;
                    case 1:
                        sqlQuery = "Select * from " + Views.V_AminoDishValuePlanII() + " Where DishID = " + DishID + " Order By NutrientId";
                        break;
                    case 2:
                        sqlQuery = "Select * from " + Views.V_AminoDishValuePlanIII() + " Where DishID = " + DishID + " Order By NutrientId";
                        break;
                }
                using (IDataReader dringredientAmino = dbManager.ExecuteReader(CommandType.Text, sqlQuery))
                {
                    while (dringredientAmino.Read())
                    {
                        ingredientAmino = FillDataRecordNutrientValuesDish(dringredientAmino);
                        if (ingredientAmino != null)
                        {
                            ingredientAminoList.Add(ingredientAmino);
                        }
                    }
                    dringredientAmino.Close();
                }
                return ingredientAminoList;
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

        public static IngredientAminoAcid GetItemNutrients(int ingredientID, int nutrientID)
        {
            IngredientAminoAcid ingredientAmino = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientAmino = dbManager.ExecuteReader(CommandType.Text, "Select IngredientId,NutrientID,NutrientValue AS NutValue FROM IngredientAminoAcid Where IngredientID = " + ingredientID + " AND NutrientID = " + nutrientID))
                {
                    if (dringredientAmino.Read())
                    {
                        ingredientAmino = FillDataRecordNutrientValues(dringredientAmino);
                    }
                    dringredientAmino.Close();
                }
                return ingredientAmino;
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

        public static List<IngredientAminoAcid> GetAminoAcidsList(int dishID, int plan)
        {
            List<IngredientAminoAcid> dishIngredientList = new List<IngredientAminoAcid>();
            IngredientAminoAcid dishIngredient = null;
            DBHelper dbManager = null;
            string sqlQry = "";

            if (plan == 0)
            {
                sqlQry = "Select * From " + Views.V_AminoDishValuePlan0() + " Where DishID = " + dishID + " ORDER BY NutrientID";
            }
            else if (plan == 1)
            {
                sqlQry = "Select * From " + Views.V_AminoDishValuePlanI() + " Where DishID = " + dishID + " ORDER BY NutrientID";
            }
            else if (plan == 2)
            {
                sqlQry = "Select * From " + Views.V_AminoDishValuePlanII() + " Where DishID = " + dishID + " ORDER BY NutrientID";
            }
            else if (plan == 3)
            {
                sqlQry = "Select * From " + Views.V_AminoDishValuePlanIII() + " Where DishID = " + dishID + " ORDER BY NutrientID";
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

        public static void SaveNutrientValues(IngredientAminoAcid ingredientAmino)
        {
            string SqlQry = "";
            string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                sqlCondition = "Select count(*) from IngredientAminoAcid WHERE IngredientID = " + ingredientAmino.IngredientId + " AND NutrientID = " + ingredientAmino.NutrientID;
                if (GetCount(sqlCondition) > 0)
                {
                    SqlQry = "UPDATE IngredientAminoAcid SET NutrientValue = " + ingredientAmino.NutrientValue + " WHERE IngredientID = " + ingredientAmino.IngredientId + " AND NutrientID = " + ingredientAmino.NutrientID;
                }
                else
                {
                    SqlQry = "INSERT INTO IngredientAminoAcid(IngredientID,NutrientID,NutrientValue)VALUES(" + ingredientAmino.IngredientId + "," + ingredientAmino.NutrientID + "," + ingredientAmino.NutrientValue + ")";
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

        public static bool DeleteAminoAcids(int ingredientID)
        {
            string SqlQry = "";
            //string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                //sqlCondition = "Select count(*) from IngredientAminoAcid  WHERE IngredientID = " + ingredientID;
                //if (GetCount(sqlCondition) > 0)
                //{
                SqlQry = "Delete from IngredientAminoAcid WHERE IngredientID = " + ingredientID;
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

        private static IngredientAminoAcid FillDataRecordNutrients(IDataReader dataReader)
        {
            IngredientAminoAcid dishIngredient = new IngredientAminoAcid();
            dishIngredient.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            dishIngredient.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            dishIngredient.NutrientValue = dataReader.IsDBNull(dataReader.GetOrdinal("NutValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NutValue"));
            dishIngredient.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            return dishIngredient;
        }
     
        private static IngredientAminoAcid FillDataRecordNutrientValues(IDataReader dataReader)
        {
            IngredientAminoAcid ingredientAmino = new IngredientAminoAcid();
            ingredientAmino.IngredientId = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientId"));
            ingredientAmino.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            ingredientAmino.NutrientValue = dataReader.IsDBNull(dataReader.GetOrdinal("NutValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NutValue"));
            ingredientAmino.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            ingredientAmino.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            ingredientAmino.NutrientGroup = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientGroup")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientGroup"));
            return ingredientAmino;
        }

        private static IngredientAminoAcid FillDataRecordNutrientValuesDish(IDataReader dataReader)
        {
            IngredientAminoAcid ingredientAmino = new IngredientAminoAcid();
            ingredientAmino.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            ingredientAmino.NutrientValue = dataReader.IsDBNull(dataReader.GetOrdinal("NutValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NutrientValue"));
            ingredientAmino.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            ingredientAmino.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            ingredientAmino.NutrientGroup = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientGroup")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientGroup"));
            return ingredientAmino;
        }

        #endregion        
    }
}
