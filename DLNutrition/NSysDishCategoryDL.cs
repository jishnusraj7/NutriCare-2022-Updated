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
    public class NSysDishCategoryDL
    {
        public static List<NSysDishCategory> GetList()
        {
            List<NSysDishCategory> dishCategoryList = new List<NSysDishCategory>();
            NSysDishCategory dishCategory = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT DishCategoryID, DishCategoryName FROM  NSysDishCategory Order By DishCategoryID"))
                {
                    while (dr.Read())
                    {
                        dishCategory = FillDataRecord(dr);

                        if (dishCategory != null)
                        {
                            dishCategoryList.Add(dishCategory);
                        }
                    }
                    dr.Close();
                }
                return dishCategoryList;
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

        public static NSysDishCategory GetItem(int DishCategoryID)
        {
            NSysDishCategory dishCategory = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT DishCategoryID, DishCategoryName FROM  NSysDishCategory Where DishCategoryID = " + DishCategoryID + " Order By DishCategoryID"))
                {
                    if (dr.Read())
                    {
                        dishCategory = FillDataRecord(dr);
                    }
                    dr.Close();
                }
                return dishCategory;
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

        private static NSysDishCategory FillDataRecord(IDataReader dataReader)
        {
            NSysDishCategory dishCategory = new NSysDishCategory();
            dishCategory.DishCategoryID = dataReader.IsDBNull(dataReader.GetOrdinal("DishCategoryID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("DishCategoryID"));
            dishCategory.DishCategoryName = dataReader.IsDBNull(dataReader.GetOrdinal("DishCategoryName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DishCategoryName"));
            return dishCategory;
        }
    }
}
