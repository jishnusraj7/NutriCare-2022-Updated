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
    public class SysAdminManager
    {
        public static List<SysAdmin> GetAyurvedicFeatures(int FeatureID, int LanguageID)
        {
            return SysAdminDL.GetAyurvedicFeatures(FeatureID, LanguageID);
        }

        public static List<SysAdmin> GetFoodHabit(int LanguageID)
        {
            return SysAdminDL.GetFoodHabit(LanguageID);
        }

        public static SysAdmin GetItemFoodHabit(int foodHabitID, int LanguageID)
        { 
            return SysAdminDL.GetItemFoodHabit(foodHabitID,LanguageID);
        }

        public static List<SysAdmin> GetLifeStyle(int LanguageID)
        {
            return SysAdminDL.GetLifeStyle(LanguageID);
        }

        public static List<SysAdmin> GetSex(int LanguageID)
        {
            return SysAdminDL.GetSex(LanguageID);
        }
        public static List<SysAdmin> GetChildSex(int LanguageID)
        {
            return SysAdminDL.GetChildSex(LanguageID);
        }

        public static List<SysAdmin> GetReligion(int LanguageID)
        {
            return SysAdminDL.GetReligion(LanguageID);
        }

        public static List<SysAdmin> GetCountry(int LanguageID)
        {
            return SysAdminDL.GetCountry(LanguageID);
        }

        public static List<SysAdmin> GetState(int LanguageID, int CountryID)
        {
            return SysAdminDL.GetState(LanguageID,CountryID);
        }

        public static List<SysAdmin> GetCity(int LanguageID, int StateID)
        {
            return SysAdminDL.GetCity(LanguageID, StateID);
        }

        public static List<SysAdmin> GetBloodGroup(int LanguageID)
        {
            return SysAdminDL.GetBloodGroup(LanguageID);
        }

        public static List<SysAdmin> GetExercise(int LanguageID)
        {
            return SysAdminDL.GetExercise(LanguageID);
        }

        public static List<SysAdmin> GetExercise()
        {
            return SysAdminDL.GetExercise();
        }

        public static List<SysAdmin> GetBodyType(int LanguageID)
        {
            return SysAdminDL.GetBodyType(LanguageID);
        }

        public static List<SysAdmin> GetJobNature(int LanguageID)
        {
            return SysAdminDL.GetJobNature(LanguageID);
        }

        public static List<SysAdmin> GetThoughts(int LanguageID)
        {
            return SysAdminDL.GetThoughts(LanguageID);
        }

        public static List<SysAdmin> GetClinicalUnit(int LanguageID)
        {
            return SysAdminDL.GetClinicalUnit(LanguageID);
        }

        public static List<SysAdmin> GetClinicalUnit(int LanguageID, int ClinicalUnitID)
        {
            return SysAdminDL.GetClinicalUnit(LanguageID,ClinicalUnitID);
        }

        public static int GetMaxThoughtID(int LanguageID)
        {
            return SysAdminDL.GetMaxThoughtID(LanguageID);
        }

        public static List<SysAdmin> GetClinicalParamValue(int ClinicalParamID, int SexID)
        {
            return SysAdminDL.GetClinicalParamValue(ClinicalParamID, SexID);
        }

        public static List<SysAdmin> GetUnit(int LanguageID)
        {
            return SysAdminDL.GetUnit(LanguageID);
        }

        public static float GetUnitValue(int UnitIDFrom, int UnitIDTo)
        {
            return SysAdminDL.GetUnitValue(UnitIDFrom, UnitIDTo);
        }

        public static List<SysAdmin> GetUnitValue(int UnitID)
        {
            return SysAdminDL.GetUnitValue(UnitID);
        }

        public static List<SysAdmin> GetKeyWord(int LanguageID, int GroupEnum)
        {
            return SysAdminDL.GetKeyWord(LanguageID, GroupEnum);
        }

        public static List<SysAdmin> GetKeyWord(int LanguageID, int GroupEnum, string SearchKeyWord)
        {
            return SysAdminDL.GetKeyWord(LanguageID, GroupEnum,SearchKeyWord);
        }

        public static List<SysAdmin> GetInfant(int LanguageID)
        {
            return SysAdminDL.GetInfant(LanguageID);
        }

        public static List<SysAdmin> GetInfantHWRange(int SexID, int InfantID)
        {
            return SysAdminDL.GetInfantHWRange(SexID, InfantID);
        }

        public static List<SysAdmin> GetBMIImpact(int LanguageID)
        {
            return SysAdminDL.GetBMIImpact(LanguageID);
        }

        public static List<SysAdmin> GetBMIImpact(int LanguageID, int BMIImpactID)
        {
            return SysAdminDL.GetBMIImpact(LanguageID, BMIImpactID);
        }

        public static List<SysAdmin> GetClinicalParamNormalValue(int ClinicalParamID, int ClinicalAgeGroupID)
        {
            return SysAdminDL.GetClinicalParamNormalValue(ClinicalParamID, ClinicalAgeGroupID);
        }

        public static List<SysAdmin> GetListHelp(int HelpItemID)
        {
            return SysAdminDL.GetListHelp(HelpItemID);
        }

        public static List<SysAdmin> GetFormFlow(int FlowID)
        {
            return SysAdminDL.GetFormFlow(FlowID);
        }

        public static List<SysAdmin> GetPrivacyLegal()
        {
            return SysAdminDL.GetPrivacyLegal();
        }
    }
}
