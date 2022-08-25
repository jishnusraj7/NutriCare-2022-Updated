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
    public class GoalSettingManager
    {
        public static List<GoalSetting> GetList(int FamilyID, int MemberID)
        {
            return GoalSettingDL.GetList(FamilyID, MemberID);
        }

        public static List<GoalSetting> GetItem(int FamilyID, int MemberID, int GoalID)
        {
            return GoalSettingDL.GetItem(FamilyID, MemberID,GoalID);
        }

        public static GoalSetting GetItem(int FamilyID, int MemberID)
        {
            return GoalSettingDL.GetItem(FamilyID, MemberID);
        }

        public static bool SaveGoal(GoalSetting goalSetting)
        {
            return GoalSettingDL.SaveGoal(goalSetting);
        }

        public static int GoalIsExists(int FamilyID, int MemberID)
        {
            return GoalSettingDL.GoalIsExists(FamilyID, MemberID);
        }

        public static List<GoalSetting> GetExerciseName(int LanguageID, int ExerciseTypeID)
        {
            return GoalSettingDL.GetExerciseName(LanguageID, ExerciseTypeID);
        }

        public static bool DeleteGoal(int FamilyID, int MemberID)
        {
            return GoalSettingDL.DeleteGoal(FamilyID, MemberID);
        }

        public static bool DeleteSimulationExercise(int MemberID)
        {
            return GoalSettingDL.DeleteSimulationExercise(MemberID);
        }

        public static bool SaveSimulationExercise(GoalSetting goalSetting)
        {
            return GoalSettingDL.SaveSimulationExercise(goalSetting);
        }

        public static bool DeleteSimulationExercise(GoalSetting goalSetting)
        {
            return GoalSettingDL.DeleteSimulationExercise(goalSetting);
        }

        public static List<GoalSetting> GetListSimulationExercise(int FamilyID, int MemberID)
        {
            return GoalSettingDL.GetListSimulationExercise(FamilyID, MemberID);
        }

        public static List<GoalSetting> GetListSimulationExerciseName(int FamilyID, int MemberID)
        {
            return GoalSettingDL.GetListSimulationExerciseName(FamilyID, MemberID);
        }
    }
}
