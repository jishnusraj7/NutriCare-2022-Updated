using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BONutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Classes;
using NutritionViews;

namespace DLNutrition
{
   public class MealTypeDL
    {
       public static NSysMealType GetItem(int MealTypeID)
        {
            NSysMealType mealType = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * from NSysMealType Where MealTypeID = " + MealTypeID))
                {
                    while (dr.Read())
                    {
                        mealType = FillDataRecord(dr);
                    }
                    dr.Close();
                }
                return mealType;
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

       public static NSysMealType GetItemByCondition(string condition)
       {
           NSysMealType mealType = null;
           DBHelper dbManager = null;
           try
           {
               dbManager = DBHelper.Instance;
               using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * from NSysMealType Where " + condition ))
               {
                   while (dr.Read())
                   {
                       mealType = FillDataRecord(dr);
                   }
                   dr.Close();
               }
               return mealType;
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

       private static NSysMealType FillDataRecord(IDataReader dataReader)
        {
            NSysMealType sysMealType = new NSysMealType();
            sysMealType.MealTypeID = dataReader.IsDBNull(dataReader.GetOrdinal("MealTypeID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MealTypeID"));
            sysMealType.MealTypeName = dataReader.IsDBNull(dataReader.GetOrdinal("MealTypeName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MealTypeName"));
            sysMealType.MealTypeDescription = dataReader.IsDBNull(dataReader.GetOrdinal("MealTypeDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MealTypeDescription"));
            return sysMealType;
        }
    }
}
