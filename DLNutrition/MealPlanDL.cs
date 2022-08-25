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
    public class MealPlanDL
    {
        public static List<MealPlan> GetFoodPlanList(int dishID)
        {
            List<MealPlan> mealPlanList = new List<MealPlan>();
            MealPlan mealPlan = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                string sqlQuery = "SELECT * FROM (SELECT DishID,StandardWeight ,CStr(StandardWeight) +'g    Medium' AS PlanStatus,0 as PStatus FROM Dish UNION SELECT DishID,StandardWeight1  AS StandardWeight,CStr(StandardWeight1) +'g    Large' AS PlanStatus,1 as PStatus  FROM Dish UNION SELECT DishID,StandardWeight2  AS StandardWeight,CStr(StandardWeight2) +'g    Custom' AS PlanStatus,2 as PStatus FROM Dish) PWL WHERE DishID = " + dishID + " AND StandardWeight > 0 Order By DishID,PStatus";
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, sqlQuery))
                {
                    while (dr.Read())
                    {
                        mealPlan = FillDataRecord(dr);
                        if (mealPlan != null)
                        {
                            mealPlanList.Add(mealPlan);
                        }
                    }
                    dr.Close();
                }
                return mealPlanList;
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

        private static MealPlan FillDataRecord(IDataReader dataReader)
        {
            MealPlan mealPlan = new MealPlan();
            mealPlan.DishID = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            mealPlan.StandardWeight = dataReader.IsDBNull(dataReader.GetOrdinal("StandardWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("StandardWeight"));
            mealPlan.PlanStatus = dataReader.IsDBNull(dataReader.GetOrdinal("PlanStatus")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("PlanStatus"));
            return mealPlan;
        }
    }
}
