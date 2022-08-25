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
    public class MemberManager
    {
        #region FamilyMealSetting

        public static double GetDishCalorie(int MemberID)
        {
            return MemberDL.GetDishCalorie(MemberID);
        }

        public static int GetDishDays(int MemberID)
        {
            return MemberDL.GetDishDays(MemberID);
        }
        #endregion 
        #region FamilyMember


        public static int GetDishDays(int MemberID, int Month, int Year)
        {
            return MemberDL.GetDishDays(MemberID, Month, Year);
        }
        public static double GetDishCalorie(int MemberID, int Month, int Year)
        {
            return MemberDL.GetDishCalorie(MemberID, Month, Year);
        }
        public static int GetExerciseCalorie(int ExerciseID, double Weight)
        {
            return MemberDL.GetExerciseCalorie(ExerciseID, Weight);
        }
        public static int GetExerciseType(int ExerciseID)
        {
            return MemberDL.GetExerciseType(ExerciseID);

        }
        public static List<Member> GetListFamilyMember(int FamilyID, int MemberID)
        {
            return MemberDL.GetListFamilyMember(FamilyID, MemberID);
        }

        public static bool SaveFamilyMember(Member member)
        {
            return MemberDL.SaveFamilyMember(member);
        }

        public static bool DeleteFamilyMember(Member family)
        {
            return MemberDL.DeleteFamilyMember(family);
        }

        public static string GetMemberName(int FamilyID, int MemberID)
        {
            return MemberDL.GetMemberName(FamilyID, MemberID);
        }

        public static string[] GetMemberNames(int FamilyID)
        {
            return MemberDL.GetMemberNames(FamilyID);
        }

        public static int GetFamilyID()
        {
            return MemberDL.GetFamilyID();
        }

        public static int GetMemberID()
        {
            return MemberDL.GetMemberID();
        }

        public static Member GetItem(int MemberID)
        {
            return MemberDL.GetItem(MemberID);
        }

        public static List<Member> GetList(string searchCondition)
        {
            return MemberDL.GetList(searchCondition);
        }

        public static List<Member> GetMemberNameList()
        {
            return MemberDL.GetMemberNameList();
        }

        #endregion
        #region FamilyExercise

        public static List<Member> GetListFamilyExercise(int FamilyID, int MemberID)
        {
            return MemberDL.GetListFamilyExercise(FamilyID, MemberID);
        }

        public static bool SaveFamilyExercise(Member member)
        {
            return MemberDL.SaveFamilyExercise(member);
        }

       

        public static bool DeleteMedicalRecords(int MemberID, int ClinicalParamID, DateTime TestDate)
        {
            return MemberDL.DeleteMedicalRecords(MemberID, ClinicalParamID, TestDate);
        }


        public static bool DeleteFamilyExercise(Member member)
        {
            return MemberDL.DeleteFamilyExercise(member);
        }

        #endregion

      
        #region MemberList

        public static List<Member> GetListMember(int LanguageID, int FamilyID, int MemberID)
        {
            List<Member> memberList = new List<Member>();
            memberList = MemberDL.GetListMember(FamilyID, MemberID);

            foreach (Member member in memberList)
            {
                member.SexItem = SexManager.GetItem(LanguageID, member.SexID);
                member.BloodGroupItem = BloodGroupManager.GetItem(LanguageID, member.BloodGroupID);
                member.FoodHabitItem = FoodHabitManager.GetItem(LanguageID, member.FoodHabitID);
                member.LifeStyleItem = LifeStyleManager.GetItem(LanguageID, member.LifeStyleID);
            }
            return memberList;
        }

        #endregion
      

        #region FamilyHistory

        public static List<Member> GetListFamilyHistory(int FamilyID, int MemberID, string ParameterName)
        {
            return MemberDL.GetListFamilyHistory(FamilyID, MemberID, ParameterName);
        }

        public static double GetItemFamilyHistory(int FamilyID, int MemberID, string ParameterName, DateTime Modifieddate)
        {
            return MemberDL.GetItemFamilyHistory(FamilyID, MemberID, ParameterName, Modifieddate);
        }

        public static void SaveFamilyHistory(Member member)
        {
            MemberDL.SaveFamilyHistory(member);
        }

        public static void UpdateFamilyGeneral(int MemberID, double ParameterValue)
        {
            MemberDL.UpdateFamilyGeneral(MemberID, ParameterValue);
        }

        public static double GetLatestWeight(int MemberID, string ParameterName)
        {
            return MemberDL.GetLatestWeight(MemberID, ParameterName);
        }

        public static bool DeleteFamilyHistory(int MemberID, string ParameterName, DateTime TestDate)
        {
            return MemberDL.DeleteFamilyHistory(MemberID, ParameterName, TestDate);
        }

        public static List<Member> GetListGoalHistory(int FamilyID, int MemberID, string ParameterName, DateTime StartDate, DateTime EndDate)
        {
            return MemberDL.GetListGoalHistory(FamilyID, MemberID, ParameterName, StartDate, EndDate);
        }

        public static bool DeleteFamilyHistory(Member member)
        {
            return MemberDL.DeleteFamilyHistory(member);
        }

        #endregion
        #region GetItem

        public static List<Member> GetListFamilyGeneral(int FamilyID, int MemberID)
        {
            return MemberDL.GetListFamilyGeneral(FamilyID, MemberID);
        }
        public static List<Member> GetMemberList(int FamilyID)
        {
            return MemberDL.GetMemberList(FamilyID);
        }
        #endregion
    }
}
