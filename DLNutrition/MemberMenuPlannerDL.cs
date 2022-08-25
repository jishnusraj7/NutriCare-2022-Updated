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
    public class MemberMenuPlannerDL
    {
        public static List<MemberMenuPlanner> GetFoodPlanList(int dishID)
        {
            List<MemberMenuPlanner> memberMenuPlannerList = new List<MemberMenuPlanner>();
            MemberMenuPlanner memberMenuPlanner = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                string sqlQuery = "SELECT * FROM (SELECT DishID,StandardWeight As PlanWeight,CStr(StandardWeight) +'g    Medium' AS PlanStatus,0 as PStatus FROM Dish UNION " +
                                  "SELECT DishID,StandardWeight1 As PlanWeight,CStr(StandardWeight1) +'g    Large' AS PlanStatus,1 as PStatus  FROM Dish UNION " +
                                  "SELECT DishID,StandardWeight2  As PlanWeight,CStr(StandardWeight2) +'g    Custom' AS PlanStatus,2 as PStatus FROM Dish) PWL WHERE DishID = " + dishID + " AND PlanWeight > 0 Order By DishId,PStatus";
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, sqlQuery))
                {
                    while (dr.Read())
                    {
                        memberMenuPlanner = FillDataRecord(dr);
                        if (memberMenuPlanner != null)
                        {
                            memberMenuPlannerList.Add(memberMenuPlanner);
                        }
                    }
                    dr.Close();
                }
                return memberMenuPlannerList;
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

        public static DataSet GetListFoodSettingDairy(string condition)
        {
            DataSet dr = new DataSet();
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                dr = dbManager.DataAdapter(CommandType.Text, "SELECT FamilyMemberMealPlan.DishID, Dish.DishName, FamilyMemberMealPlan.MealTypeID, FamilyMember.FamilyID, Dish.ServeUnit, FamilyMemberMealPlan.WeekDay, Dish.DisplayName, FamilyMemberMealPlan.PlanWeight, FamilyMemberMealPlan.DishCount " +
                                                            " FROM (FamilyMemberMealPlan INNER JOIN FamilyMember ON FamilyMemberMealPlan.MemberID = FamilyMember.MemberID) INNER JOIN Dish ON FamilyMemberMealPlan.DishID = Dish.DishID Where " + condition);
                return dr;
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

        public static List<MemberMenuPlanner> GetFoodSettingDairyWeekDayList()
        {
            List<MemberMenuPlanner> memberMenuPlannerList = new List<MemberMenuPlanner>();
            MemberMenuPlanner memberMenuPlanner = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select distinct(WeekDay) from FamilyMemberMealPlan"))
                {
                    while (dr.Read())
                    {
                        memberMenuPlanner = FillMemberFoodSettingDairyDays(dr);
                        if (memberMenuPlanner != null)
                        {
                            memberMenuPlannerList.Add(memberMenuPlanner);
                        }
                    }
                    dr.Close();
                }
                return memberMenuPlannerList;
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

        public static List<MemberMenuPlanner> GetFoodSettingDairyList(string condition)
        {
            List<MemberMenuPlanner> memberMenuPlannerList = new List<MemberMenuPlanner>();
            MemberMenuPlanner memberMenuPlanner = null;
            DBHelper dbManager = null;
            string sqlqry = "";
            try
            {
                dbManager = DBHelper.Instance;
                if (condition != "")
                {
                    sqlqry = "Select * from FamilyMemberMealPlan where " + condition;
                }
                else
                {
                    sqlqry = "Select * from FamilyMemberMealPlan";
                }

                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, sqlqry))
                {
                    while (dr.Read())
                    {
                        memberMenuPlanner = FillMemberFoodSettingsDairy(dr);
                        if (memberMenuPlanner != null)
                        {
                            memberMenuPlannerList.Add(memberMenuPlanner);
                        }
                    }
                    dr.Close();
                }
                return memberMenuPlannerList;
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

        public static List<MemberMenuPlanner> GetFoodSettingWeekDayList(int memberID)
        {
            List<MemberMenuPlanner> memberMenuPlannerList = new List<MemberMenuPlanner>();
            MemberMenuPlanner memberMenuPlanner = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select distinct(WeekDay) from FamilyMemberMealPlan Where MemberID = " + memberID))
                {
                    while (dr.Read())
                    {
                        memberMenuPlanner = FillMemberFoodSettingDays(dr);

                        if (memberMenuPlanner != null)
                        {
                            memberMenuPlannerList.Add(memberMenuPlanner);
                        }
                    }
                    dr.Close();
                }
                return memberMenuPlannerList;
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

        private static MemberMenuPlanner FillDataRecord(IDataReader dataReader)
        {
            MemberMenuPlanner memberMenuPlanner = new MemberMenuPlanner();
            memberMenuPlanner.DishID = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            memberMenuPlanner.PlanWeight = dataReader.IsDBNull(dataReader.GetOrdinal("PlanWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("PlanWeight"));
            memberMenuPlanner.PlanStatus = dataReader.IsDBNull(dataReader.GetOrdinal("PlanStatus")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("PlanStatus"));
            return memberMenuPlanner;
        }

        private static MemberMenuPlanner FillMemberFoodSettingDairyDays(IDataReader dataReader)
        {
            MemberMenuPlanner memberMenuPlanner = new MemberMenuPlanner();
            memberMenuPlanner.Week = dataReader.IsDBNull(dataReader.GetOrdinal("WeekDay")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            return memberMenuPlanner;
        }

        private static MemberMenuPlanner FillMemberFoodSettingsDairy(IDataReader dataReader)
        {
            MemberMenuPlanner memberMenuPlanner = new MemberMenuPlanner();
            memberMenuPlanner.MemberMealPlanID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberMealPlanID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberMealPlanID"));
            memberMenuPlanner.DishID = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            memberMenuPlanner.DishCount = dataReader.IsDBNull(dataReader.GetOrdinal("DishCount")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("DishCount"));
            memberMenuPlanner.PlanWeight = dataReader.IsDBNull(dataReader.GetOrdinal("PlanWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("PlanWeight"));
            memberMenuPlanner.MealTypeID = dataReader.IsDBNull(dataReader.GetOrdinal("MealTypeID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MealTypeID"));
            memberMenuPlanner.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            return memberMenuPlanner;
        }

        private static MemberMenuPlanner FillMemberFoodSettingDays(IDataReader dataReader)
        {
            MemberMenuPlanner memberMenuPlanner = new MemberMenuPlanner();
            memberMenuPlanner.WeekDay = dataReader.IsDBNull(dataReader.GetOrdinal("WeekDay")) ? DateTime.Now : dataReader.GetDateTime(dataReader.GetOrdinal("WeekDay"));
            return memberMenuPlanner;
        }

        public static void SaveFoodSettingDairy(MemberMenuPlanner menuPlanner)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "INSERT INTO FamilyMemberMealPlan(MemberID,WeekDay,MealTypeID,DishID,DishCount,PlanWeight) " +
                            "Values (" + menuPlanner.MemberID + ",'" + menuPlanner.WeekDay.ToString("yyyy/MM/dd") + "'," + menuPlanner.MealTypeID + "," + menuPlanner.DishID + "," + menuPlanner.DishCount + "," + menuPlanner.PlanWeight + ")";
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

        public static void DeleteFoodSettingDairy(string condition)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "DELETE FROM FamilyMemberMealPlan WHERE " + condition;
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

        public static bool DeleteFamilyMemberMealDairy(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                SqlQry = "Delete FamilyMemberMealPlan Where MemberID = " + member.MemberID;

                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
                return true;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                return false;
            }
            finally
            {
                dbHelper = null;
            }
        }
        
        public static int GetDishDays(int MemberID)
        {
            int Days = 0;
            string SqlQry;
            DBHelper dbHelper = null;
            DataSet ds = new DataSet();
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Distinct(WeekDay) From FamilyMemberMealPlan Where MemberID =" + MemberID + " GROUP BY WeekDay";
                ds = dbHelper.DataAdapter(CommandType.Text, SqlQry);

                if (ds.Tables.Count > 0)
                {
                    Days = Convert.ToInt32(ds.Tables[0].Rows.Count);
                }

                return Days;
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
    }
}
