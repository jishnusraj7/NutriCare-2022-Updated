using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using BLNutrition;
using BONutrition;
using DLNutrition;

namespace BLNutrition
{
    public class MemberMenuPlannerManager
    {
        public static List<MemberMenuPlanner> GetFoodPlanList(int dishID)
        {
            return MemberMenuPlannerDL.GetFoodPlanList(dishID);
        }

        public static DataSet GetListFoodSettingDairy(string condition)
        {
            return MemberMenuPlannerDL.GetListFoodSettingDairy(condition);
        }

        public static List<MemberMenuPlanner> GetFoodSettingDairyWeekDayList()
        {
            return MemberMenuPlannerDL.GetFoodSettingDairyWeekDayList();
        }

        public static List<MemberMenuPlanner> GetFoodSettingDairyList(string Condition)
        {
            return MemberMenuPlannerDL.GetFoodSettingDairyList(Condition);
        }

        public static List<MemberMenuPlanner> GetFoodSettingWeekDayList(int memberID)
        {
            return MemberMenuPlannerDL.GetFoodSettingWeekDayList(memberID);
        }

        public static void SaveFoodSettingDairy(MemberMenuPlanner menuPlanner)
        {
            MemberMenuPlannerDL.SaveFoodSettingDairy(menuPlanner);
        }

        public static void DeleteFoodSettingDairy(string condition)
        {
            MemberMenuPlannerDL.DeleteFoodSettingDairy(condition);
        }

        public static bool DeleteFamilyMemberMealDairy(Member member)
        {
            return MemberMenuPlannerDL.DeleteFamilyMemberMealDairy(member);
        }
       
        public static int GetDishDays(int MemberID)
        {
            return MemberMenuPlannerDL.GetDishDays(MemberID);
        }        
    }
}
