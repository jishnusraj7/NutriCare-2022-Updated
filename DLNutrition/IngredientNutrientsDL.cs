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
    public class IngredientNutrientsDL
    {
        #region Ingredient Nutrients        

        public static List<IngredientNutrients> GetListNutrientValues(int ingredientID)
        {
            List<IngredientNutrients> ingredientNutrientList = new List<IngredientNutrients>();
            IngredientNutrients ingredientNutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientNutrient = dbManager.ExecuteReader(CommandType.Text, "Select IngredientNutrients.IngredientID,IngredientNutrients.NutrientID,IngredientNutrients.NutrientValue,NSys_Nutrient.NutrientParam,NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM IngredientNutrients INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID Where IngredientNutrients.IngredientID = " + ingredientID + " Order By IngredientNutrients.NutrientID"))
                {
                    while (dringredientNutrient.Read())
                    {
                        ingredientNutrient = FillDataRecordNutrientValues(dringredientNutrient);
                        if (ingredientNutrient != null)
                        {
                            ingredientNutrientList.Add(ingredientNutrient);
                        }
                    }
                    dringredientNutrient.Close();
                }
                return ingredientNutrientList;
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

        public static List<IngredientNutrients> GetIngredientNutrients(int ingredientID,int DishId)
        {
            List<IngredientNutrients> ingredientNutrientList = new List<IngredientNutrients>();
            IngredientNutrients ingredientNutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientNutrient = dbManager.ExecuteReader(CommandType.Text, Views.V_IngredientNutrients() + " Where DishID = " + DishId + " and IngredientID=" + ingredientID + "  Order By NutrientID"))
                {
                    while (dringredientNutrient.Read())
                    {
                        ingredientNutrient = FillDataRecordNutrientValues(dringredientNutrient);
                        if (ingredientNutrient != null)
                        {
                            ingredientNutrientList.Add(ingredientNutrient);
                        }
                    }
                    dringredientNutrient.Close();
                }
                return ingredientNutrientList;
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

        public static List<IngredientNutrients> GetListNutrientsDish(int DishID, int PlanID)
        {
            List<IngredientNutrients> ingredientNutrientList = new List<IngredientNutrients>();
            IngredientNutrients ingredientNutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                string sqlQuery = "";
                switch(PlanID)
                {
                    case 0:
                        sqlQuery = "Select * from " + Views.V_NutrientDishValuePlanI() + " Where DishID = " + DishID + " Order By NutrientId";
                        break;
                    case 1:
                        sqlQuery = "Select * from " + Views.V_NutrientDishValuePlanII() + " Where DishID = " + DishID + " Order By NutrientId";
                        break;
                    case 2:
                        sqlQuery = "Select * from " + Views.V_NutrientDishValuePlanIII() + " Where DishID = " + DishID + " Order By NutrientId";
                        break;
                }

                using (IDataReader dringredientNutrient = dbManager.ExecuteReader(CommandType.Text, sqlQuery))
                {
                    while (dringredientNutrient.Read())
                    {
                        ingredientNutrient = FillDataRecordNutrientValuesIngredient(dringredientNutrient);
                        if (ingredientNutrient != null)
                        {
                            ingredientNutrientList.Add(ingredientNutrient);
                        }
                    }
                    dringredientNutrient.Close();
                }
                return ingredientNutrientList;
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

        
        public static IngredientNutrients GetItemNutrients(int ingredientID, int nutrientID)
        {            
            IngredientNutrients ingredientNutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientNutrient = dbManager.ExecuteReader(CommandType.Text, "Select IngredientNutrients.IngredientID,IngredientNutrients.NutrientID,IngredientNutrients.NutrientValue AS NutrientValue,NSys_Nutrient.NutrientParam,NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM IngredientNutrients INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID Where IngredientNutrients.IngredientID = " + ingredientID + " AND NSys_Nutrient.NutrientID = " + nutrientID))
                {
                    if (dringredientNutrient.Read())
                    {
                        ingredientNutrient = FillDataRecordNutrientValues(dringredientNutrient);                        
                    }
                    dringredientNutrient.Close();
                }
                return ingredientNutrient;
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

        public static List<IngredientNutrients> GetListNutrientMain(int ingredientID)
        {
            List<IngredientNutrients> ingredientNutrientList = new List<IngredientNutrients>();
            IngredientNutrients ingredientNutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientNutrient = dbManager.ExecuteReader(CommandType.Text, "Select IngredientNutrients.IngredientID,IngredientNutrients.NutrientID,IngredientNutrients.NutrientValue AS NutrientValue,NSys_Nutrient.NutrientParam,NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM IngredientNutrients INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID Where IngredientNutrients.IngredientID = " + ingredientID + " AND NSys_Nutrient.IsNutrientmain = true"))
                {
                    while (dringredientNutrient.Read())
                    {
                        ingredientNutrient = FillDataRecordNutrientValues(dringredientNutrient);
                        if (ingredientNutrient != null)
                        {
                            ingredientNutrientList.Add(ingredientNutrient);
                        }
                    }
                    dringredientNutrient.Close();
                }
                return ingredientNutrientList;
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

        public static List<IngredientNutrients> GetItemNutrientsMain(int ingredientID)
        {
            List<IngredientNutrients> ingredientNutrientList = new List<IngredientNutrients>();
            IngredientNutrients ingredientNutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientNutrient = dbManager.ExecuteReader(CommandType.Text, "Select IngredientNutrients.IngredientID,IngredientNutrients.NutrientID,IngredientNutrients.NutrientValue AS NutrientValue,NSys_Nutrient.NutrientParam,NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM IngredientNutrients INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID Where IngredientNutrients.IngredientID = " + ingredientID + " and IngredientNutrients.NutrientID IN (1,2,3,4,5,8,9,10,13,12,14,16,17,18,26,27,29,31,47,122,123) ORDER BY IngredientNutrients.NutrientID"))
                {
                    while (dringredientNutrient.Read())
                    {
                        ingredientNutrient = FillDataRecordNutrientValuesIngredient(dringredientNutrient);
                        if (ingredientNutrient != null)
                        {
                            ingredientNutrientList.Add(ingredientNutrient);
                        }
                    }
                    dringredientNutrient.Close();
                }
                return ingredientNutrientList;
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

        public static List<IngredientNutrients> GetNutrientsList(int dishID, int plan)
        {
            List<IngredientNutrients> dishIngredientList = new List<IngredientNutrients>();
            IngredientNutrients dishIngredient = null;
            DBHelper dbManager = null;
            string sqlQry = "";

            if (plan == 0)
            {                
                //sqlQry = "SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, " +
                //        " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / (CASE (Dish.ServeCount * Dish.StandardWeight) WHEN 0 THEN 1 ELSE (Dish.ServeCount * Dish.StandardWeight) END)) * 100, 2) AS NutrientValue, " +
                //        " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM NSys_Nutrient INNER JOIN IngredientStandardUnit INNER JOIN DishIngredient   INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID ON  IngredientStandardUnit.StandardUnitID = DishIngredient.StandardUnitID AND IngredientStandardUnit.IngredientID = DishIngredient.IngredientID ON   NSys_Nutrient.NutrientID = IngredientNutrients.NutrientID " +
                //        " INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID Where DishIngredient.DishID = " + dishID + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight,Dish.ServeCount ORDER BY IngredientNutrients.NutrientID";

                sqlQry = " SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, " +
                         " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / IIF(Dish.ServeCount * Dish.StandardWeight = 0,1,Dish.ServeCount * Dish.StandardWeight)) * 100, 2) AS NutrientValue, " +
                         " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
                         " Where DishIngredient.DishID = " + dishID + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight,Dish.ServeCount ORDER BY IngredientNutrients.NutrientID";
            }
            else if (plan == 1)
            {
                //sqlQry = "Select * From " + Views.V_NutrientDishValuePlanI() + " Where DishID = " + dishID + " ORDER BY NutrientID";
                //sqlQry = "SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, "+
                //        " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / (CASE (Dish.ServeCount * Dish.StandardWeight) WHEN 0 THEN 1 ELSE (Dish.ServeCount * Dish.StandardWeight) END)) * Dish.StandardWeight, 2) AS NutrientValue, "+
                //        " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM NSys_Nutrient INNER JOIN IngredientStandardUnit INNER JOIN DishIngredient   INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID ON  IngredientStandardUnit.StandardUnitID = DishIngredient.StandardUnitID AND IngredientStandardUnit.IngredientID = DishIngredient.IngredientID ON   NSys_Nutrient.NutrientID = IngredientNutrients.NutrientID " +
                //        " INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID Where DishIngredient.DishID = " + dishID + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight,Dish.ServeCount ORDER BY IngredientNutrients.NutrientID";

                sqlQry = " SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, " +
                         " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / IIF(Dish.ServeCount * Dish.StandardWeight = 0,1,Dish.ServeCount * Dish.StandardWeight)) * Dish.StandardWeight, 2) AS NutrientValue, " +
                         " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
                         " Where DishIngredient.DishID = " + dishID + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight,Dish.ServeCount ORDER BY IngredientNutrients.NutrientID";
            }
            else if (plan == 2)
            {
                //sqlQry = "Select * From " + Views.V_NutrientDishValuePlanII() + " Where DishID = " + dishID + " ORDER BY NutrientID";
                //sqlQry = "SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, " +
                //        " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / (CASE (Dish.ServeCount1 * Dish.StandardWeight1) WHEN 0 THEN 1 ELSE (Dish.ServeCount1 * Dish.StandardWeight1) END)) * Dish.StandardWeight1, 2) AS NutrientValue, " +
                //        " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM NSys_Nutrient INNER JOIN IngredientStandardUnit INNER JOIN DishIngredient   INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID ON  IngredientStandardUnit.StandardUnitID = DishIngredient.StandardUnitID AND IngredientStandardUnit.IngredientID = DishIngredient.IngredientID ON   NSys_Nutrient.NutrientID = IngredientNutrients.NutrientID " +
                //        " INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID Where DishIngredient.DishID = " + dishID + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight1,Dish.ServeCount1 ORDER BY IngredientNutrients.NutrientID";

                sqlQry = " SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, " +
                         " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / IIF(Dish.ServeCount1 * Dish.StandardWeight1 = 0,1,Dish.ServeCount1 * Dish.StandardWeight1)) * Dish.StandardWeight1, 2) AS NutrientValue, " +
                         " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
                         " Where DishIngredient.DishID = " + dishID + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight1,Dish.ServeCount1 ORDER BY IngredientNutrients.NutrientID";
            }
            else if (plan == 3)
            {
                //sqlQry = "Select * From " + Views.V_NutrientDishValuePlanIII() + " Where DishID = " + dishID + " ORDER BY NutrientID";
                //sqlQry = "SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, " +
                //        " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / (CASE (Dish.ServeCount2 * Dish.StandardWeight2) WHEN 0 THEN 1 ELSE (Dish.ServeCount2 * Dish.StandardWeight2) END)) * Dish.StandardWeight2, 2) AS NutrientValue, " +
                //        " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM NSys_Nutrient INNER JOIN IngredientStandardUnit INNER JOIN DishIngredient   INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID ON  IngredientStandardUnit.StandardUnitID = DishIngredient.StandardUnitID AND IngredientStandardUnit.IngredientID = DishIngredient.IngredientID ON   NSys_Nutrient.NutrientID = IngredientNutrients.NutrientID " +
                //        " INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID Where DishIngredient.DishID = " + dishID + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight2,Dish.ServeCount2 ORDER BY IngredientNutrients.NutrientID";

                sqlQry = " SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, " +
                         " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / IIF(Dish.ServeCount2 * Dish.StandardWeight2 = 0,1,Dish.ServeCount2 * Dish.StandardWeight2)) * Dish.StandardWeight2, 2) AS NutrientValue, " +
                         " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID " +
                         " Where DishIngredient.DishID = " + dishID + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight2,Dish.ServeCount2 ORDER BY IngredientNutrients.NutrientID";
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

        public static void SaveNutrientValues(IngredientNutrients ingredientNutrient)
        {
            string SqlQry = "";
            string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                sqlCondition = "Select count(*) from IngredientNutrients WHERE IngredientID = " + ingredientNutrient.IngredientId + " AND NutrientID = " + ingredientNutrient.NutrientID;
                if (GetCount(sqlCondition) > 0)
                {
                    SqlQry = "UPDATE IngredientNutrients SET NutrientValue = " + ingredientNutrient.NutrientValue + " WHERE IngredientID = " + ingredientNutrient.IngredientId + " AND NutrientID = " + ingredientNutrient.NutrientID;
                }
                else
                {
                    SqlQry = "INSERT INTO IngredientNutrients(IngredientID,NutrientID,NutrientValue)VALUES(" + ingredientNutrient.IngredientId + "," + ingredientNutrient.NutrientID + "," + ingredientNutrient.NutrientValue + ")";
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

        public static bool DeleteNutrients(int ingredientID)
        {
            string SqlQry = "";
            //string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                //sqlCondition = "Select count(*) from IngredientNutrients  WHERE IngredientID = " + ingredientID;
                //if (GetCount(sqlCondition) > 0)
                //{
                SqlQry = "Delete from IngredientNutrients WHERE IngredientID = " + ingredientID;
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

        private static IngredientNutrients FillDataRecordNutrients(IDataReader dataReader)
        {
            IngredientNutrients dishIngredient = new IngredientNutrients();
            dishIngredient.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            dishIngredient.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            dishIngredient.NutrientValue = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NutrientValue"));
            dishIngredient.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            return dishIngredient;
        }

        private static IngredientNutrients FillDataRecordNutrientValues(IDataReader dataReader)
        {
            IngredientNutrients ingredientNutrient = new IngredientNutrients();
            ingredientNutrient.IngredientId = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientId"));
            ingredientNutrient.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            ingredientNutrient.NutrientValue = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NutrientValue"));
            ingredientNutrient.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            ingredientNutrient.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            ingredientNutrient.NutrientGroup = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientGroup")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientGroup"));
            return ingredientNutrient;
        }

        private static IngredientNutrients FillDataRecordNutrientValuesIngredient(IDataReader dataReader)
        {
            IngredientNutrients ingredientNutrient = new IngredientNutrients();
            ingredientNutrient.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            ingredientNutrient.NutrientValue = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NutrientValue"));
            ingredientNutrient.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            ingredientNutrient.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            ingredientNutrient.NutrientGroup = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientGroup")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientGroup"));
            return ingredientNutrient;
        }

        #endregion        
    }
}
