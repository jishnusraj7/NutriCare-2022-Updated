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
    public class NSysFoodCategoryDL
    {
        public static List<NSysFoodCategory> GetList()
        {
            List<NSysFoodCategory> foodCategoryList = new List<NSysFoodCategory>();
            NSysFoodCategory foodCategory = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT FoodCategoryID, FoodCategoryName FROM  NSysFoodCategory Order By FoodCategoryID"))
                {
                    while (dr.Read())
                    {
                        foodCategory = FillDataRecord(dr);

                        if (foodCategory != null)
                        {
                            foodCategoryList.Add(foodCategory);
                        }
                    }
                    dr.Close();
                }
                return foodCategoryList;
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
        
        public static NSysFoodCategory GetItem(int foodCategoryID)
        {            
            NSysFoodCategory foodCategory = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT FoodCategoryID, FoodCategoryName FROM  NSysFoodCategory Where FoodCategoryID = " + foodCategoryID + " Order By FoodCategoryID"))
                {
                    if (dr.Read())
                    {
                        foodCategory = FillDataRecord(dr);
                    }
                    dr.Close();
                }
                return foodCategory;
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

        private static NSysFoodCategory FillDataRecord(IDataReader dataReader)
        {
            NSysFoodCategory foodCategory = new NSysFoodCategory();
            foodCategory.FoodCategoryID = dataReader.IsDBNull(dataReader.GetOrdinal("FoodCategoryID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("FoodCategoryID"));            
            foodCategory.FoodCategoryName = dataReader.IsDBNull(dataReader.GetOrdinal("FoodCategoryName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("FoodCategoryName"));
            return foodCategory;
        }
    }
}
