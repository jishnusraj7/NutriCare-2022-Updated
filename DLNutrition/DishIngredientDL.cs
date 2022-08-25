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
	public class DishIngredientDL
	{
		#region Dish Ingredient

		public static List<DishIngredient> GetList(int dishID)
		{
			List<DishIngredient> dishList = new List<DishIngredient>();
			DishIngredient dish = null;
			DBHelper dbManager = null;
			try
			{
				dbManager = DBHelper.Instance;
				using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, "SELECT DishIngredient.DishIngredientID,DishIngredient.DishID,DishIngredient.IngredientID, DishIngredient.Quantity, DishIngredient.DisplayOrder, DishIngredient.StandardUnitID, DishIngredient.SectionName, " +
                                                                    "Ingredient.IngredientName, Ingredient.DisplayName, Ingredient.WeightChangeRate FROM DishIngredient INNER JOIN Ingredient ON DishIngredient.IngredientID = Ingredient.IngredientID Where DishIngredient.DishID = " + dishID +" Order By DishIngredient.DisplayOrder"))
				{
					while (drDish.Read())
					{
						dish = FillDataRecord(drDish);
						if (dish != null)
						{
							dishList.Add(dish);
						}
					}
					drDish.Close();
				}
				return dishList;
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

        public static List<DishIngredient> GetDishIngredientsList(int dishID)
        {
            List<DishIngredient> dishList = new List<DishIngredient>();
            DishIngredient dish = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, "SELECT DishIngredient.DishIngredientID, DishIngredient.DishID, DishIngredient.IngredientID, DishIngredient.Quantity, DishIngredient.DisplayOrder, DishIngredient.StandardUnitID, DishIngredient.SectionName,Ingredient.IngredientName,Ingredient.DisplayName, Ingredient.WeightChangeRate,IngredientStandardUnit.StandardWeight,StandardUnit.StandardUnitType "+
                                                                                      " FROM IngredientStandardUnit INNER JOIN ((DishIngredient INNER JOIN Ingredient ON DishIngredient.IngredientID = Ingredient.IngredientID) INNER JOIN StandardUnit ON DishIngredient.StandardUnitID = StandardUnit.StandardUnitID) ON (IngredientStandardUnit.StandardUnitID = StandardUnit.StandardUnitID) AND (IngredientStandardUnit.IngredientID = Ingredient.IngredientID) "+
                                                                                      " Where DishIngredient.DishID = " + dishID + " AND StandardUnit.StandardUnitID = DishIngredient.StandardUnitID Order By DishIngredient.DisplayOrder"))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecordList(drDish);
                        if (dish != null)
                        {
                            dishList.Add(dish);
                        }
                    }
                    drDish.Close();
                }
                return dishList;
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

        public static List<DishIngredient> GetIngredientDisplayList(int dishID)
        {
            List<DishIngredient> dishList = new List<DishIngredient>();
            DishIngredient dish = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, "SELECT Ingredient.IngredientName, DishIngredient.Quantity, StandardUnit.StandardUnitName " +
                                                                                      " FROM IngredientStandardUnit INNER JOIN ((DishIngredient INNER JOIN Ingredient ON DishIngredient.IngredientID = Ingredient.IngredientID) INNER JOIN StandardUnit ON DishIngredient.StandardUnitID = StandardUnit.StandardUnitID) ON (IngredientStandardUnit.StandardUnitID = StandardUnit.StandardUnitID) AND (IngredientStandardUnit.IngredientID = Ingredient.IngredientID) " +
                                                                                      " Where DishIngredient.DishID = " + dishID + " AND StandardUnit.StandardUnitID = DishIngredient.StandardUnitID Order By DishIngredient.DisplayOrder"))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecordDisplayList(drDish);
                        if (dish != null)
                        {
                            dishList.Add(dish);
                        }
                    }
                    drDish.Close();
                }
                return dishList;
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

		private static DishIngredient FillDataRecord(IDataReader dataReader)
		{
			DishIngredient dish = new DishIngredient();
			dish.DishId = dataReader.IsDBNull(dataReader.GetOrdinal("DishId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishId"));
			dish.DishIngredientID = dataReader.IsDBNull(dataReader.GetOrdinal("DishIngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishIngredientID"));
			dish.IngredientID = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
			dish.Quantity = dataReader.IsDBNull(dataReader.GetOrdinal("Quantity")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Quantity"));
			dish.DisplayOrder = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayOrder")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("DisplayOrder"));
			dish.StandardUnitID = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitID"));
			dish.SectionName = dataReader.IsDBNull(dataReader.GetOrdinal("SectionName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("SectionName"));
            dish.IngredientName = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("IngredientName"));
            dish.DisplayName = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayName"));
            dish.WeightChangeRate = dataReader.IsDBNull(dataReader.GetOrdinal("WeightChangeRate")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("WeightChangeRate"));
			return dish;
		}

        private static DishIngredient FillDataRecordList(IDataReader dataReader)
        {
            DishIngredient dish = new DishIngredient();
            dish.DishId = dataReader.IsDBNull(dataReader.GetOrdinal("DishId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishId"));
            dish.DishIngredientID = dataReader.IsDBNull(dataReader.GetOrdinal("DishIngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishIngredientID"));
            dish.IngredientID = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
            dish.Quantity = dataReader.IsDBNull(dataReader.GetOrdinal("Quantity")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Quantity"));
            dish.DisplayOrder = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayOrder")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("DisplayOrder"));
            dish.StandardUnitID = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitID"));
            dish.SectionName = dataReader.IsDBNull(dataReader.GetOrdinal("SectionName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("SectionName"));
            dish.IngredientName = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("IngredientName"));
            dish.DisplayName = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayName"));
            dish.WeightChangeRate = dataReader.IsDBNull(dataReader.GetOrdinal("WeightChangeRate")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("WeightChangeRate"));
            dish.StandardWeight = dataReader.IsDBNull(dataReader.GetOrdinal("StandardWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("StandardWeight"));
            dish.StandardUnitType = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitType")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitType"));
            return dish;
        }

        private static DishIngredient FillDataRecordDisplayList(IDataReader dataReader)
        {
            DishIngredient dish = new DishIngredient();
            dish.IngredientName = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("IngredientName"));
            dish.Quantity = dataReader.IsDBNull(dataReader.GetOrdinal("Quantity")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Quantity"));            
            dish.StandardUnitName = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("StandardUnitName"));
            return dish;
        }

        public static List<DishIngredient> GetNutrientsList(int dishID,int plan)
        {
            List<DishIngredient> dishList = new List<DishIngredient>();
            DishIngredient dish = null;
            DBHelper dbManager = null;
            string sqlQry = "";

            if (plan == 1)
            {
                sqlQry = "Select * From " + Views.V_NutrientDishValuePlanI() + " Where DishID = " + dishID + " ORDER BY NutrientID";
            }
            else if (plan == 2)
            {
                sqlQry = "Select * From " + Views.V_NutrientDishValuePlanII() + " Where DishID = " + dishID + " ORDER BY NutrientID";
            }
            else if (plan == 3)
            {
                sqlQry = "Select * From " + Views.V_NutrientDishValuePlanIII() + " Where DishID = " + dishID + " ORDER BY NutrientID";
            }

            //sqlQry = "Select * From " + Views.V_NutrientDishValuePlan() + " Where DishID = " + dishID + " ORDER BY NutrientID";

            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, sqlQry))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecordNutrients(drDish);
                        if (dish != null)
                        {
                            dishList.Add(dish);
                        }
                    }
                    drDish.Close();
                }
                return dishList;
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

        public static List<DishIngredient> GetAminoAcidsList(int dishID, int plan)
        {
            List<DishIngredient> dishList = new List<DishIngredient>();
            DishIngredient dish = null;
            DBHelper dbManager = null;
            string sqlQry = "";

            if (plan == 1)
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
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, sqlQry))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecordNutrients(drDish);
                        if (dish != null)
                        {
                            dishList.Add(dish);
                        }
                    }
                    drDish.Close();
                }
                return dishList;
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

        public static List<DishIngredient> GetFattyAcidsList(int dishID, int plan)
        {
            List<DishIngredient> dishList = new List<DishIngredient>();
            DishIngredient dish = null;
            DBHelper dbManager = null;
            string sqlQry = "";

            if (plan == 1)
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
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, sqlQry))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecordNutrients(drDish);
                        if (dish != null)
                        {
                            dishList.Add(dish);
                        }
                    }
                    drDish.Close();
                }
                return dishList;
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

        public static List<DishIngredient> GetIngredientNutrientList(int dishID)
        {
            List<DishIngredient> dishIngredientList = new List<DishIngredient>();
            DishIngredient dishIngredient = null;
            DBHelper dbManager = null;

            string sqlQry = "SELECT Ingredient.IngredientID,Ingredient.EthnicID, Ingredient.IngredientName,Ingredient.DisplayName, DishIngredient.Quantity, DishIngredient.DisplayOrder,StandardUnit.StandardUnitDisplay,Ingredient.FoodHabitID,DishIngredient.DishID, "+
                            "SUM(IIF(NutrientParam='Calorie',ROUND((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity,3) , 0 )) AS Calorie, "+
                            "SUM(IIF(NutrientParam='Protien',ROUND((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity,3) , 0 )) AS Protien, "+
                            "SUM(IIF(NutrientParam='CarboHydrates',ROUND((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity,3) , 0 )) AS CarboHydrate, "+
                            "SUM(IIF(NutrientParam='Fat',ROUND((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity,3) , 0 )) AS Fat, "+
                            "SUM(IIF(NutrientParam='Fibre',ROUND((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity,3) , 0 )) AS Fibre, "+
                            "SUM(IIF(NutrientParam='Iron',ROUND((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity,3) , 0 )) AS Iron, "+
                            "SUM(IIF(NutrientParam='Calcium',ROUND((IngredientNutrients.NutrientValue / 100) * IngredientStandardUnit.StandardWeight * DishIngredient.Quantity,3) , 0 )) AS Calcium "+
                            "FROM ((((Ingredient INNER JOIN IngredientNutrients ON Ingredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID) "+
                            "INNER JOIN DishIngredient ON Ingredient.IngredientID = DishIngredient.IngredientID) INNER JOIN IngredientStandardUnit ON Ingredient.IngredientID = IngredientStandardUnit.IngredientID) "+
                            "INNER JOIN StandardUnit ON (DishIngredient.StandardUnitID = StandardUnit.StandardUnitID) AND (IngredientStandardUnit.StandardUnitID = StandardUnit.StandardUnitID)" +
                            "WHERE NSys_Nutrient.IsNutrientMain = true AND DishID = " + dishID + " " +
                            "GROUP BY Ingredient.IngredientID,Ingredient.EthnicID, Ingredient.IngredientName,Ingredient.DisplayName, DishIngredient.Quantity, DishIngredient.DisplayOrder,StandardUnit.StandardUnitDisplay,Ingredient.FoodHabitID,DishIngredient.DishID Order By DishIngredient.DisplayOrder";

            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drNutrients = dbManager.ExecuteReader(CommandType.Text, sqlQry))
                {
                    while (drNutrients.Read())
                    {
                        dishIngredient = FillDataRecordIngredientNutrients(drNutrients);
                        if (dishIngredient != null)
                        {
                            dishIngredientList.Add(dishIngredient);
                        }
                    }
                    drNutrients.Close();
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

        private static DishIngredient FillDataRecordIngredientNutrients(IDataReader dataReader)
        {
            DishIngredient dish = new DishIngredient();
            dish.DishId = dataReader.IsDBNull(dataReader.GetOrdinal("DishId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishId"));
            dish.IngredientID = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientID"));
            dish.IngredientName = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("IngredientName"));
            dish.DisplayName = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayName"));
            dish.Quantity = dataReader.IsDBNull(dataReader.GetOrdinal("Quantity")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Quantity"));
            dish.StandardUnitName = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitDisplay")) ? "" : dataReader.GetString(dataReader.GetOrdinal("StandardUnitDisplay"));
            dish.Calorie = dataReader.IsDBNull(dataReader.GetOrdinal("Calorie")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calorie"));
            dish.Protien = dataReader.IsDBNull(dataReader.GetOrdinal("Protien")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Protien"));
            dish.CarboHydrate = dataReader.IsDBNull(dataReader.GetOrdinal("CarboHydrate")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("CarboHydrate"));
            dish.Fat = dataReader.IsDBNull(dataReader.GetOrdinal("Fat")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fat"));
            dish.Fibre = dataReader.IsDBNull(dataReader.GetOrdinal("Fibre")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fibre"));
            dish.Iron = dataReader.IsDBNull(dataReader.GetOrdinal("Iron")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Iron"));
            dish.Calcium = dataReader.IsDBNull(dataReader.GetOrdinal("Calcium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calcium"));
            return dish;
        }

        private static DishIngredient FillDataRecordNutrients(IDataReader dataReader)
        {
            DishIngredient dish = new DishIngredient();            
            dish.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? 0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            dish.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            dish.NutrientValue = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NutrientValue"));
            dish.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            return dish;
        }

		public static void Save(DishIngredient dish)
		{
			string SqlQry = "";
			string sqlCondition;
			DBHelper dbHelper = null;
			try
			{
				dbHelper = DBHelper.Instance;
				sqlCondition = "Select count(*) from DishIngredient  WHERE DishID = " + dish.DishId + " AND IngredientID = " + dish.IngredientID;
				if (GetCount(sqlCondition) > 0)
				{
					SqlQry = "UPDATE DishIngredient SET Quantity = " + dish.Quantity + ",DisplayOrder = " + dish.DisplayOrder + ",StandardUnitID = " + dish.StandardUnitID + ",SectionName = '" + dish.SectionName + "' WHERE DishID = " + dish.DishId + "  AND IngredientID = " + dish.IngredientID;
				}
				else
				{
                    int dishIngrID = GetID();
                    SqlQry = "INSERT INTO DishIngredient(DishIngredientID,DishID,IngredientID,Quantity,DisplayOrder,StandardUnitID,SectionName)" +
                             "VALUES (" + dishIngrID + "," + dish.DishId + "," + dish.IngredientID + "," + dish.Quantity + "," + dish.DisplayOrder + "," + dish.StandardUnitID + ",'" + dish.SectionName + "')";
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

        private static int GetID()
        {
            int DishIngredientID = 0;
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select IIF(ISNULL(Max(DishIngredientID)),0,Max(DishIngredientID)) + 1 From DishIngredient";
                DishIngredientID = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;
                return DishIngredientID;
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

        public static bool Delete(DishIngredient dishIngredient)
        {
            string SqlQry = "";
            string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                sqlCondition = "Select count(*) from DishIngredient  WHERE DishID = " + dishIngredient.DishId + " AND IngredientID = " + dishIngredient.IngredientID;
                if (GetCount(sqlCondition) > 0)
                {
                    SqlQry = "Delete from DishIngredient  WHERE DishID = " + dishIngredient.DishId + " AND IngredientID = " + dishIngredient.IngredientID;
                    dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
                    return true;
                }
                else
                {
                    return false;
                }
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

        public static bool DeleteDishIngredients(int dishID)
        {
            string SqlQry = "";
            //string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                //sqlCondition = "Select count(*) from DishIngredient  WHERE DishID = " + dishID;
                //if (GetCount(sqlCondition) > 0)
                //{
                SqlQry = "Delete from DishIngredient  WHERE DishID = " + dishID;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
                //}
                return true;
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

		#endregion

		#region Functions

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

		#endregion
	}
}
