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
    public class NSysAdminManager
    {
        public static List<NSysAdmin> GetAyurvedicFeatures(int FeatureID)
        {
            return NSysAdminDL.GetAyurvedicFeatures(FeatureID);
        }

        public static List<NSysAdmin> GetFoodHabit()
        {
            return NSysAdminDL.GetFoodHabit();
        }

        public static NSysAdmin GetItemFoodHabit(int foodHabitID)
        { 
            return NSysAdminDL.GetItemFoodHabit(foodHabitID);
        }

        public static List<NSysAdmin> GetLifeStyle()
        {
            return NSysAdminDL.GetLifeStyle();
        }

        public static List<NSysAdmin> GetSex()
        {
            return NSysAdminDL.GetSex();
        }
        public static List<NSysAdmin> GetChildSex()
        {
            return NSysAdminDL.GetChildSex();
        }

        public static List<NSysAdmin> GetReligion()
        {
            return NSysAdminDL.GetReligion();
        }

        public static List<NSysAdmin> GetCountry()
        {
            return NSysAdminDL.GetCountry();
        }

        public static List<NSysAdmin> GetState(int CountryID)
        {
            return NSysAdminDL.GetState(CountryID);
        }

        public static List<NSysAdmin> GetCity(int StateID)
        {
            return NSysAdminDL.GetCity(StateID);
        }

        public static List<NSysAdmin> GetBloodGroup()
        {
            return NSysAdminDL.GetBloodGroup();
        }

        public static List<NSysAdmin> GetExercise()
        {
            return NSysAdminDL.GetExercise();
        }        

        public static List<NSysAdmin> GetBodyType()
        {
            return NSysAdminDL.GetBodyType();
        }

        public static List<NSysAdmin> GetJobNature()
        {
            return NSysAdminDL.GetJobNature();
        }

        public static List<NSysAdmin> GetThoughts()
        {
            return NSysAdminDL.GetThoughts();
        }

        public static List<NSysAdmin> GetClinicalUnit()
        {
            return NSysAdminDL.GetClinicalUnit();
        }

        public static List<NSysAdmin> GetClinicalUnit(int ClinicalUnitID)
        {
            return NSysAdminDL.GetClinicalUnit(ClinicalUnitID);
        }

        public static int GetMaxThoughtID()
        {
            return NSysAdminDL.GetMaxThoughtID();
        }

        public static List<NSysAdmin> GetClinicalParamValue(int ClinicalParamID, int SexID)
        {
            return NSysAdminDL.GetClinicalParamValue(ClinicalParamID, SexID);
        }

        public static List<NSysAdmin> GetUnit()
        {
            return NSysAdminDL.GetUnit();
        }

        public static float GetUnitValue(int UnitIDFrom, int UnitIDTo)
        {
            return NSysAdminDL.GetUnitValue(UnitIDFrom, UnitIDTo);
        }

        public static List<NSysAdmin> GetUnitValue(int UnitID)
        {
            return NSysAdminDL.GetUnitValue(UnitID);
        }

        public static List<NSysAdmin> GetKeyWord(int GroupEnum)
        {
            return NSysAdminDL.GetKeyWord(GroupEnum);
        }

        public static List<NSysAdmin> GetKeyWord(int GroupEnum, string SearchKeyWord)
        {
            return NSysAdminDL.GetKeyWord(GroupEnum,SearchKeyWord);
        }

        public static List<NSysAdmin> GetInfant()
        {
            return NSysAdminDL.GetInfant();
        }

        public static List<NSysAdmin> GetInfantHWRange(int SexID, int InfantID)
        {
            return NSysAdminDL.GetInfantHWRange(SexID, InfantID);
        }

        public static List<NSysAdmin> GetBMIImpact()
        {
            return NSysAdminDL.GetBMIImpact();
        }

        public static List<NSysAdmin> GetBMIImpact(int BMIImpactID)
        {
            return NSysAdminDL.GetBMIImpact(BMIImpactID);
        }

        public static List<NSysAdmin> GetClinicalParamNormalValue(int ClinicalParamID, int ClinicalAgeGroupID)
        {
            return NSysAdminDL.GetClinicalParamNormalValue(ClinicalParamID, ClinicalAgeGroupID);
        }

        public static List<NSysAdmin> GetListHelp(int HelpItemID)
        {
            return NSysAdminDL.GetListHelp(HelpItemID);
        }

        public static List<NSysAdmin> GetFormFlow(int FlowID)
        {
            return NSysAdminDL.GetFormFlow(FlowID);
        }

        public static List<NSysAdmin> GetPrivacyLegal()
        {
            return NSysAdminDL.GetPrivacyLegal();
        }
    }
}
