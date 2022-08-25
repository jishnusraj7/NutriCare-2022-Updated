using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NutritionViews
{
    /// <summary>
    /// SQL Queries for Getting Data from database
    /// CretedBy: Aby Kurian
    /// Created Date: 27 Aug 2009
    /// </summary>
    public static class Views
    {
        #region PUBLIC METHODS   

        public static string V_ClinicalParam()
        {
            string QueryBuilder = " (SELECT SysClinicalParam.ClinicalParamID, SysClinicalParam.ClinicalParamCode, SysClinicalParam.UnitType, SysClinicalParam.NormalLow, SysClinicalParam.NormalHigh, SysClinicalParam.IsNormal, SysClinicalAgeGroup.ClinicalAgeGroupID, ";
            QueryBuilder = QueryBuilder + " SysClinicalParam.ClinicalParamName, SysClinicalAgeGroup.AgeFrom, SysClinicalAgeGroup.AgeTo, SysClinicalAgeGroup.Sex, SysClinicalParam.ClinicalUnitID FROM SysClinicalParam INNER JOIN SysClinicalAgeGroup ON SysClinicalParam.ClinicalAgeGroupID = SysClinicalAgeGroup.ClinicalAgeGroupID)CP  ";
            return QueryBuilder;
        }

        public static string V_BMIImpact()
        {
            string QueryBuilder = " (SELECT  SysBMIImpact.BMIImpactID, SysLanguage.LanguageID, SysBMIImpact_LAN.BMIImpactName, ";
            QueryBuilder = QueryBuilder + " SysBMIImpact_LAN.BMIIMpactDescription, SysBMIImpact_LAN.BMIIMpactComments, ";
            QueryBuilder = QueryBuilder + " SysBMIImpact_LAN.BMIIMpactSuggetions FROM SysBMIImpact INNER JOIN SysBMIImpact_LAN ON SysBMIImpact.BMIImpactID = SysBMIImpact_LAN.BMIImpactID INNER JOIN SysLanguage ON SysBMIImpact_LAN.LanguageID = SysLanguage.LanguageID)BI ";
            return QueryBuilder;
        }

        public static string V_ClinicalUnit()
        {
            string QueryBuilder = " (SELECT SysClinicalUnit.ClinicalUnitID, SysClinicalUnit_LAN.ClinicalUnitName, SysClinicalUnit_LAN.ClinicalUnitDescription, SysLanguage.LanguageID FROM SysClinicalUnit INNER JOIN SysClinicalUnit_LAN ON  ";
            QueryBuilder = QueryBuilder + " SysClinicalUnit.ClinicalUnitID = SysClinicalUnit_LAN.ClinicalUnitID INNER JOIN SysLanguage ON SysClinicalUnit_LAN.LanguageID = SysLanguage.LanguageID )CU  ";
            return QueryBuilder;
        }
        public static string V_JobNature()
        {
            string QueryBuilder = " (SELECT SysJobNature_LAN.JobNatureName, SysJobNature.JobNatureID, SysLanguage.LanguageID FROM SysJobNature INNER JOIN SysJobNature_LAN ON SysJobNature.JobNatureID = SysJobNature_LAN.JobNatureID  ";
            QueryBuilder = QueryBuilder + " INNER JOIN SysLanguage ON SysJobNature_LAN.LanguageID = SysLanguage.LanguageID )JN ";
            return QueryBuilder;
        }
        public static string V_Country()
        {
            string QueryBuilder = " (SELECT SysCountry_LAN.CountryName, SysCountry.CountryID, SysLanguage.LanguageID FROM SysLanguage INNER JOIN SysCountry_LAN ON SysLanguage.LanguageID = SysCountry_LAN.LanguageID INNER JOIN SysCountry ON SysCountry_LAN.CountryID = SysCountry.CountryID)CO ";
            return QueryBuilder;
        }

        public static string V_BodyType()
        {
            string QueryBuilder = " (SELECT  SysBodyType_LAN.BodyTypeName, SysBodyType.BodyTypeID, SysLanguage.LanguageID, ";
            QueryBuilder = QueryBuilder + " SysBodyType_LAN.BodyTypeDescription FROM SysBodyType INNER JOIN SysBodyType_LAN ON SysBodyType.BodyTypeID = SysBodyType_LAN.BodyTypeID INNER JOIN SysLanguage ON SysBodyType_LAN.LanguageID = SysLanguage.LanguageID  )BT ";
            return QueryBuilder;
        }
        public static string V_LifeStyle()
        {
            string QueryBuilder = " (SELECT SysLanguage.LanguageID, SysLifeStyle.LifeStyleID, SysLifeStyle_LAN.LifeStyleName FROM SysLifeStyle INNER JOIN SysLifeStyle_LAN ON SysLifeStyle.LifeStyleID = SysLifeStyle_LAN.LifeStyleID  ";
            QueryBuilder = QueryBuilder + " INNER JOIN SysLanguage ON SysLifeStyle_LAN.LanguageID = SysLanguage.LanguageID )LS ";
            return QueryBuilder;
        }

        public static string V_Meal()
        {
            string QueryBuilder = " (SELECT Meal.MealID, Meal_LAN.MealName, Meal_LAN.MealRemarks, SysLanguage.LanguageID, Meal.DisplayImage, Meal.Calorie, Meal.Protien, Meal.Fat, Meal.Sugar, Meal.Salt FROM Meal  ";
            QueryBuilder = QueryBuilder + " INNER JOIN Meal_LAN ON Meal.MealID = Meal_LAN.MealID INNER JOIN SysLanguage ON Meal_LAN.LanguageID = SysLanguage.LanguageID )ML ";
            return QueryBuilder;
        }

        public static string V_Religion()
        {
            string QueryBuilder = " (SELECT SysReligion_LAN.ReligionName, SysReligion.ReligionID, SysLanguage.LanguageID, SysReligion_LAN.ReligionDescription FROM SysReligion INNER JOIN SysReligion_LAN ON  ";
            QueryBuilder = QueryBuilder + " SysReligion.ReligionID = SysReligion_LAN.ReligionID INNER JOIN SysLanguage ON SysReligion_LAN.LanguageID = SysLanguage.LanguageID  )RG ";
            return QueryBuilder;
        }

        public static string V_SearchKeyword()
        {
            string QueryBuilder = " (SELECT  SysSearchKeyword.KeywordID, SysSearchKeyword_LAN.KeywordDescription, SysLanguage.LanguageID, SysSearchKeyword.KeywordCode, SysSearchKeyword.GroupEnum FROM SysSearchKeyword  ";
            QueryBuilder = QueryBuilder + " INNER JOIN SysSearchKeyword_LAN ON SysSearchKeyword.KeywordID = SysSearchKeyword_LAN.KeywordID INNER JOIN SysLanguage ON SysSearchKeyword_LAN.LanguageID = SysLanguage.LanguageID )SK ";
            return QueryBuilder;
        }

        public static string V_Sex()
        {
            string QueryBuilder = " (SELECT SysSex_LAN.SexName, SysSex.SexID, SysLanguage.LanguageID,SysSex_LAN.IsMajor FROM SysSex INNER JOIN SysSex_LAN ON SysSex.SexID = SysSex_LAN.SexID INNER JOIN SysLanguage ON SysSex_LAN.LanguageID = SysLanguage.LanguageID)SX ";
            return QueryBuilder;
        }

        public static string V_Infant()
        {
            string QueryBuilder = " (SELECT SysInfant.InfantID, SysInfant_LAN.InfantName, SysLanguage.LanguageID FROM SysInfant INNER JOIN SysInfant_LAN ON SysInfant.InfantID = SysInfant_LAN.InfantID INNER JOIN SysLanguage ON SysInfant_LAN.LanguageID = SysLanguage.LanguageID)IT";
            return QueryBuilder;
        }

        public static string V_StandardUnit()
        {
            string QueryBuilder = " (SELECT StandardUnit.StandardUnitID, StandardUnit_LAN.StandardUnitName, StandardUnit_LAN.StandardUnitDisplay, SysLanguage.LanguageID,StandardUnit.StandardUnitType FROM StandardUnit INNER JOIN StandardUnit_LAN ON ";
            QueryBuilder = QueryBuilder + " StandardUnit.StandardUnitID = StandardUnit_LAN.StandardUnitID INNER JOIN SysLanguage ON StandardUnit_LAN.LanguageID = SysLanguage.LanguageID  )SU ";
            return QueryBuilder;
        }

        public static string V_State()
        {
            string QueryBuilder = " (SELECT  SysState_LAN.StateName, SysLanguage.LanguageID, SysCountry.CountryID, SysCountry_LAN.CountryName, SysState.StateID FROM  SysLanguage INNER JOIN SysState_LAN ON ";
            QueryBuilder = QueryBuilder + " SysLanguage.LanguageID = SysState_LAN.LanguageID INNER JOIN SysState ON SysState_LAN.StateID = SysState.StateID INNER JOIN SysCountry ON ";
            QueryBuilder = QueryBuilder + " SysState_LAN.CountryID = SysCountry.CountryID INNER JOIN SysCountry_LAN ON SysLanguage.LanguageID = SysCountry_LAN.LanguageID AND  SysCountry.CountryID = SysCountry_LAN.CountryID )ST ";
            return QueryBuilder;
        }

        public static string V_SysAyurvedic()
        {
            string QueryBuilder = " (SELECT  Sys_Ayurvedic.AyurID, SysLanguage.LanguageID, Sys_Ayurvedic_LAN.AyurParam FROM Sys_Ayurvedic INNER JOIN Sys_Ayurvedic_LAN ON Sys_Ayurvedic.AyurID = Sys_Ayurvedic_LAN.AyurID  ";
            QueryBuilder = QueryBuilder + " INNER JOIN SysLanguage ON Sys_Ayurvedic_LAN.LanguageID = SysLanguage.LanguageID )SA ";
            return QueryBuilder;
        }

        public static string V_SysServeUnit()
        {
            string QueryBuilder = " (SELECT  SysServeUnit.ServeUnitID, SysLanguage.LanguageID, SysServeUnit_LAN.ServeUnitName FROM SysServeUnit INNER JOIN SysServeUnit_LAN ON SysServeUnit.ServeUnitID = SysServeUnit_LAN.ServeUnitID  ";
            QueryBuilder = QueryBuilder + " INNER JOIN SysLanguage ON SysServeUnit_LAN.LanguageID = SysLanguage.LanguageID )SU ";
            return QueryBuilder;
        }

        public static string V_TOTD()
        {
            string QueryBuilder = " (SELECT  TOTD_LAN.ThoughtDescription, TOTD_LAN.ThoughtBy, TOTD.ThoughtID, SysLanguage.LanguageID FROM TOTD INNER JOIN TOTD_LAN ON TOTD.ThoughtID = TOTD_LAN.ThoughtID INNER JOIN SysLanguage ON TOTD_LAN.LanguageID = SysLanguage.LanguageID )TOT ";
            return QueryBuilder;
        }

        public static string V_Unit()
        {
            string QueryBuilder = " (SELECT SysUnit.UnitId, SysLanguage.LanguageID, SysUnit_LAN.UnitName, SysUnit_LAN.UnitDescription FROM SysUnit INNER JOIN SysUnit_LAN ON SysUnit.UnitId = SysUnit_LAN.UnitID INNER JOIN SysLanguage ON SysUnit_LAN.LanguageID = SysLanguage.LanguageID)UT ";
            return QueryBuilder;
        }

        public static string V_HealthTip()
        {
            string QueryBuilder = " (SELECT SysHealthTip.HealthTipID, SysHealthTip.HealthTipGroupEnum, SysHealthTip_LAN.HealthTipTopic,SysHealthTip_LAN.HealthTipDescription, SysHealthTip_LAN.IsPriority, SysLanguage.LanguageID FROM SysHealthTip INNER JOIN SysHealthTip_LAN ON SysHealthTip.HealthTipID = SysHealthTip_LAN.HealthTipID INNER JOIN SysLanguage ON SysHealthTip_LAN.LanguageID = SysLanguage.LanguageID)HT ";
            return QueryBuilder;
        }

        public static string V_CountServings()
        {
            string QueryBuilder = " (SELECT SysCountServings.CountServingsID, SysCountServings.CountServingsTopic,SysCountServings.CountServingsDescription FROM SysCountServings )CS ";
            return QueryBuilder;
        }

        public static string V_BloodGroup()
        {
            string QueryBuilder = " (SELECT SysBloodGroup_LAN.BloodGroupName, SysBloodGroup.BloodGroupID, SysLanguage.LanguageID FROM  SysBloodGroup INNER JOIN ";
            QueryBuilder = QueryBuilder + " SysBloodGroup_LAN ON SysBloodGroup.BloodGroupID = SysBloodGroup_LAN.BloodGroupID INNER JOIN SysLanguage ON SysBloodGroup_LAN.LanguageID = SysLanguage.LanguageID )BG ";
            return QueryBuilder;
        }

        public static string V_MemberMedicalRecords(int Parameter1, int Parameter2)
        {
#if SQLCE
            string QueryBuilder = "select MemberId,Modifieddate,IsHigh,FamilyMemberClinical.UnitType,FamilyMemberClinical.IsNormal," +
                                  " SUM((CASE ClinicalparamName WHEN 'Haemoglobin' THEN CurrentValue ELSE 0 END)) AS Haemoglobin, " +
                                  " SUM((CASE ClinicalparamName WHEN 'TotalCholesterol' THEN CurrentValue ELSE 0 END)) AS TotalCholesterol, " +
                                  " SUM((CASE ClinicalparamName WHEN 'CholesterolHDL' THEN CurrentValue ELSE 0 END)) AS CholesterolHDL, " +
                                  " SUM((CASE ClinicalparamName WHEN 'CholesterolLDL' THEN CurrentValue ELSE 0 END)) AS CholesterolLDL, " +
                                  " SUM((CASE ClinicalparamName WHEN 'CholesterolTriglicerides' THEN CurrentValue ELSE 0 END)) AS CholesterolTriglicerides, " +
                                  " SUM((CASE ClinicalparamName WHEN 'DiabetesFasting' THEN CurrentValue ELSE 0 END)) AS DiabetesFasting, " +
                                  " SUM((CASE ClinicalparamName WHEN 'DiabetesPPHrs' THEN CurrentValue ELSE 0 END)) AS DiabetesPPHrs, " +
                                  " SUM((CASE ClinicalparamName WHEN 'DiabetesRandom' THEN CurrentValue ELSE 0 END)) AS DiabetesRandom, " +
                                  " SUM((CASE ClinicalparamName WHEN 'LowBloodPressure' THEN IsNormal ELSE 0 END)) AS LowBloodPressure, " +
                                  " SUM((CASE ClinicalparamName WHEN 'HighBloodPressure' THEN IsNormal ELSE 0 END)) AS HighBloodPressure, " +
                                  " SUM((CASE ClinicalparamName WHEN 'HyperThyroid' THEN IsNormal ELSE 0 END)) AS HyperThyroid, " +
                                  " SUM((CASE ClinicalparamName WHEN 'HypoThyroid' THEN IsNormal ELSE 0 END)) AS HypoThyroid, " +
                                  " SUM((CASE ClinicalparamName WHEN 'LiverProblem' THEN IsNormal ELSE 0 END)) AS LiverProblem, " +
                                  " SUM((CASE ClinicalparamName WHEN 'KidneyProblem' THEN IsNormal ELSE 0 END)) AS KidneyProblem, " +
                                  " SUM((CASE ClinicalparamName WHEN 'UrinalInfection' THEN IsNormal ELSE 0 END)) AS UrinalInfection, " +
                                  " SUM((CASE ClinicalparamName WHEN 'Allergy' THEN IsNormal ELSE 0 END)) AS Allergy " +
                                  " from FamilyMemberClinical LEFT JOIN SysClinicalParam ON FamilyMemberClinical.ClinicalparamID = SysClinicalParam.ClinicalparamID WHERE SysClinicalParam.ClinicalAgeGroupID = " + Parameter1 + " AND MemberID = " + Parameter2 +
                                  " GROUP BY MemberId,Modifieddate,IsHigh,FamilyMemberClinical.UnitType,FamilyMemberClinical.IsNormal";
#else
            string QueryBuilder = "DECLARE @listCol VARCHAR(2000);DECLARE @cols VARCHAR(2000);DECLARE @query VARCHAR(4000);SELECT  @listCol =(SELECT  '[' + convert(nvarchar,ClinicalparamName) +'],' FROM SysClinicalParam where ClinicalAgeGroupID = " + Parameter1 + " FOR XML PATH(''));set @cols=SUBString(@listCol,0,(len(@listCol))); " +
                                  " SET @query ='Select * from (select MemberId,Modifieddate,ClinicalparamName,CurrentValue,IsHigh,FamilyMemberClinical.UnitType,FamilyMemberClinical.IsNormal from FamilyMemberClinical LEFT JOIN SysClinicalParam ON FamilyMemberClinical.ClinicalparamID = SysClinicalParam.ClinicalparamID " +
                                  " where SysClinicalParam.ClinicalAgeGroupID = " + Parameter1 + " ) src  PIVOT (SUM(CurrentValue) FOR  ClinicalparamName IN ('+ @cols +')) AS pvt where MemberID = " + Parameter2 + "' ;EXECUTE (@query);";
#endif
            return QueryBuilder;
        }

        public static string V_FoodHabit()
        {
            string QueryBuilder = " (SELECT SysFoodHabit_LAN.FoodHabitName, SysLanguage.LanguageID, SysFoodHabit.FoodHabitID, SysFoodHabit_LAN.FoodHabitDescription FROM SysFoodHabit INNER JOIN  ";
            QueryBuilder = QueryBuilder + " SysFoodHabit_LAN ON SysFoodHabit.FoodHabitID = SysFoodHabit_LAN.FoodHabitID INNER JOIN SysLanguage ON SysFoodHabit_LAN.LanguageID = SysLanguage.LanguageID   )FH ";
            return QueryBuilder;
        }

        public static string V_AyurvedicFeatures()
        {
            string QueryBuilder = " (SELECT SysAyurBodyFeatureOption.VPK, SysAyurBodyFeature_LAN.FeatureDescription, SysAyurBodyFeatureOption_LAN.Description, ";
            QueryBuilder = QueryBuilder + " SysAyurBodyFeature.FeatureID, SysLanguage.LanguageID FROM SysAyurBodyFeature INNER JOIN SysAyurBodyFeature_LAN ";
            QueryBuilder = QueryBuilder + " ON SysAyurBodyFeature.FeatureID = SysAyurBodyFeature_LAN.FeatureID INNER JOIN SysAyurBodyFeatureOption ON ";
            QueryBuilder = QueryBuilder + " SysAyurBodyFeature.FeatureID = SysAyurBodyFeatureOption.FeatureID INNER JOIN SysAyurBodyFeatureOption_LAN ON ";
            QueryBuilder = QueryBuilder + " SysAyurBodyFeatureOption.OptionID = SysAyurBodyFeatureOption_LAN.OptionID AND ";
            QueryBuilder = QueryBuilder + " SysAyurBodyFeature_LAN.LanguageID = SysAyurBodyFeatureOption_LAN.LanguageID  INNER JOIN SysLanguage ON ";
            QueryBuilder = QueryBuilder + " SysAyurBodyFeatureOption_LAN.LanguageID = SysLanguage.LanguageID )AF ";
            return QueryBuilder;
        }
        public static string V_Exercise()
        {
            string QueryBuilder = " (SELECT  SysExercise.ExerciseID, SysExercise_LAN.ExerciseName, SysExercise_LAN.ExerciseDescription, SysLanguage.LanguageID, SysExercise_LAN.ExerciseTypeID FROM  SysLanguage INNER JOIN SysExercise_LAN ON  ";
            QueryBuilder = QueryBuilder + " SysLanguage.LanguageID = SysExercise_LAN.LanguageID INNER JOIN SysExercise ON SysExercise_LAN.ExcerciseID = SysExercise.ExerciseID )EX ";
            return QueryBuilder;
        }

        public static string V_City()
        {
            string QueryBuilder = " (SELECT SysCity_LAN.CityName, SysLanguage.LanguageID, SysCity.CityID, SysCountry.CountryID, SysState.StateID, SysState_LAN.StateName, SysCountry_LAN.CountryName FROM SysCity INNER JOIN SysCity_LAN ON SysCity.CityID = SysCity_LAN.CityID  ";
            QueryBuilder = QueryBuilder + " INNER JOIN SysLanguage ON SysCity_LAN.LanguageID = SysLanguage.LanguageID INNER JOIN SysCountry_LAN ON SysLanguage.LanguageID = SysCountry_LAN.LanguageID INNER JOIN SysCountry ON SysCountry_LAN.CountryID = SysCountry.CountryID INNER JOIN ";
            QueryBuilder = QueryBuilder + " SysState ON SysCity_LAN.StateID = SysState.StateID INNER JOIN SysState_LAN ON SysLanguage.LanguageID = SysState_LAN.LanguageID AND SysCountry.CountryID = SysState_LAN.CountryID AND SysState.StateID = SysState_LAN.StateID )CT ";
            return QueryBuilder;
        }
        public static string V_IngredientAminoAcid()
        {
            string QueryBuilder = "Select * from (SELECT NSys_Nutrient.NutrientID,DishId,DishIngredient.IngredientID as IngredientID,(Quantity * StandardWeight * (NutrientValue /100)) as NutrientValue,NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit, NSys_Nutrient.NutrientGroup " +
                                  " FROM ((DishIngredient INNER JOIN IngredientStandardUnit ON (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID) AND (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID)) INNER JOIN IngredientAminoAcid ON DishIngredient.IngredientID = IngredientAminoAcid.IngredientID) INNER JOIN NSys_Nutrient ON IngredientAminoAcid.NutrientID = NSys_Nutrient.NutrientID) VAA ";
            return QueryBuilder;
        }

        public static string V_IngredientFattyAcid()
        {
            string QueryBuilder = "Select * from (SELECT NSys_Nutrient.NutrientID,DishId,DishIngredient.IngredientID as IngredientID,(Quantity * StandardWeight * (NutrientValue /100)) as NutrientValue,NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit, NSys_Nutrient.NutrientGroup " +
                                  " FROM ((DishIngredient inner join IngredientStandardUnit on (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID) AND (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID)) INNER JOIN IngredientFattyAcid ON DishIngredient.IngredientID=IngredientFattyAcid.IngredientID) INNER JOIN NSys_Nutrient ON IngredientFattyAcid.NutrientID = NSys_Nutrient.NutrientID) VFA ";
            return QueryBuilder;
        }

        public static string V_IngredientNutrients()
        {
            string QueryBuilder = "Select * from (SELECT NSys_Nutrient.NutrientID,DishId,DishIngredient.IngredientID as IngredientID,(Quantity * StandardWeight * (NutrientValue /100))as NutrientValue,NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit, NSys_Nutrient.NutrientGroup  " +
                                  " FROM ((DishIngredient inner join IngredientStandardUnit on (DishIngredient.StandardUnitID=IngredientStandardUnit.StandardUnitID) AND (DishIngredient.IngredientID=IngredientStandardUnit.IngredientID)) INNER JOIN IngredientNutrients on DishIngredient.IngredientID=IngredientNutrients.IngredientID) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID) VIN ";
            return QueryBuilder;
        }

        public static string V_MemberFoodSetting(string conditions)
        {
            string QueryBuilder = "DECLARE @listCol VARCHAR(2000);DECLARE @cols VARCHAR(2000);DECLARE @query VARCHAR(4000);SELECT  @listCol =(SELECT  '[' + MemberName +'],' FROM FamilyMember where MemberName<>''  Order By DisplayOrder FOR XML PATH(''));set @cols=SUBString(@listCol,0,(len(@listCol))); " +
                              " SET @query ='SELECT * FROM (select FamilyMemberMealPlan.DishID,DishCount,DishName,MemberName,LanguageId,MealTypeID,FamilyID,ServeUnit,WeekDay,DisplayName,PlanWeight from FamilyMemberMealPlan inner join Dish_LAN on FamilyMemberMealPlan.DishID=Dish_LAN.DishID inner join  " +
                              " FamilyMember on FamilyMemberMealPlan.MemberID=FamilyMember.MemberID inner join Dish on Dish.DishID=FamilyMemberMealPlan.DishID ) src PIVOT (SUM(DishCount) FOR  MemberName IN ('+ @cols +')) AS pvt " + conditions + " ' ;EXECUTE (@query);";
            return QueryBuilder;
        }

        public static string V_MemberFoodSettingRegional(string conditions)
        {
            string QueryBuilder = "DECLARE @listCol VARCHAR(2000);DECLARE @cols VARCHAR(2000);DECLARE @query VARCHAR(4000);SELECT  @listCol =(SELECT  '[' + MemberName +'],' FROM FamilyMember where MemberName<>''  Order By DisplayOrder FOR XML PATH(''));set @cols=SUBString(@listCol,0,(len(@listCol))); " +
                                  " SET @query ='SELECT * FROM (select FamilyMemberMealPlan.DishID,DishCount,DisplayName AS DishName,MemberName,LanguageId,MealTypeID,FamilyID,ServeUnit,WeekDay from FamilyMemberMealPlan inner join Dish_LAN on FamilyMemberMealPlan.DishID=Dish_LAN.DishID inner join  " +
                                  " FamilyMember on FamilyMemberMealPlan.MemberID=FamilyMember.MemberID inner join Dish on Dish.DishID=FamilyMemberMealPlan.DishID ) src PIVOT (SUM(DishCount) FOR  MemberName IN ('+ @cols +')) AS pvt " + conditions + " order by DishID ' ;EXECUTE (@query);";
            return QueryBuilder;
        }

        public static string V_MemberFoodSettingDairy(string conditions)
        {
            string QueryBuilder = "Select FamilyMemberMealDairy.DishID,DishName,LanguageId,MealTypeID,FamilyID,ServeUnit,WeekDay,DishCount,MemberName,DisplayName,PlanWeight from FamilyMemberMealDairy inner join Dish_LAN " +
                                  " on FamilyMemberMealDairy.DishID=Dish_LAN.DishID inner join FamilyMember on FamilyMemberMealDairy.MemberID=FamilyMember.MemberID inner join Dish on Dish.DishID=FamilyMemberMealDairy.DishID " + conditions;
            return QueryBuilder;
        }

        public static string V_NutrientGlossary()
        {
            string QueryBuilder = " (SELECT Sys_Nutrient.NutrientID, Sys_Nutrient_LAN.NutrientParam, Sys_Nutrient_LAN.NutrientDescription,SysLanguage.LanguageID FROM Sys_Nutrient INNER JOIN ";
            QueryBuilder = QueryBuilder + " Sys_Nutrient_LAN ON Sys_Nutrient.NutrientID = Sys_Nutrient_LAN.NutrientID INNER JOIN SysLanguage ON Sys_Nutrient_LAN.LanguageID = SysLanguage.LanguageID )NG ";
            return QueryBuilder;
        }
        public static string V_AyurGlossary()
        {
            string QueryBuilder = " (SELECT Sys_Ayurvedic.AyurID, Sys_Ayurvedic_LAN.AyurParam, Sys_Ayurvedic_LAN.AyurDescription,SysLanguage.LanguageID FROM Sys_Ayurvedic INNER JOIN ";
            QueryBuilder = QueryBuilder + " Sys_Ayurvedic_LAN ON Sys_Ayurvedic.AyurID = Sys_Ayurvedic_LAN.AyurID INNER JOIN SysLanguage ON Sys_Ayurvedic_LAN.LanguageID = SysLanguage.LanguageID )AG ";
            return QueryBuilder;
        }
        public static string V_NutrientDishValuePlanI()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / IIF(Dish.ServeCount * Dish.StandardWeight = 0,1,Dish.ServeCount * Dish.StandardWeight)) * Dish.StandardWeight, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight,Dish.ServeCount )NDV ";
            return QueryBuilder;
        }

        public static string V_NutrientDishValuePlanII()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / IIF(Dish.ServeCount1 * Dish.StandardWeight1 = 0,1,Dish.ServeCount1 * Dish.StandardWeight1)) * Dish.StandardWeight1, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight1,Dish.ServeCount1 )NDV ";
            return QueryBuilder;
        }

        public static string V_NutrientDishValuePlanIII()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientNutrients.NutrientValue / 100)) / IIF(Dish.ServeCount2 * Dish.StandardWeight2 = 0,1,Dish.ServeCount2 * Dish.StandardWeight2)) * Dish.StandardWeight2, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientNutrients ON DishIngredient.IngredientID = IngredientNutrients.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientNutrients.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientNutrients.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight2,Dish.ServeCount2 )NDV ";
            return QueryBuilder;
        }

        public static string V_AminoDishValuePlan0()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientAminoAcid.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientAminoAcid.NutrientValue / 100)) / IIF(Dish.ServeCount * Dish.StandardWeight = 0,1,Dish.ServeCount * Dish.StandardWeight)) * 100, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientAminoAcid ON DishIngredient.IngredientID = IngredientAminoAcid.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientAminoAcid.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientAminoAcid.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight,Dish.ServeCount )NAV ";
            return QueryBuilder;
        }

        public static string V_AminoDishValuePlanI()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientAminoAcid.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientAminoAcid.NutrientValue / 100)) / IIF(Dish.ServeCount * Dish.StandardWeight = 0,1,Dish.ServeCount * Dish.StandardWeight)) * Dish.StandardWeight, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientAminoAcid ON DishIngredient.IngredientID = IngredientAminoAcid.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientAminoAcid.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientAminoAcid.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight,Dish.ServeCount )NAV ";
            return QueryBuilder;
        }

        public static string V_AminoDishValuePlanII()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientAminoAcid.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientAminoAcid.NutrientValue / 100)) / IIF(Dish.ServeCount1 * Dish.StandardWeight1 = 0,1,Dish.ServeCount1 * Dish.StandardWeight1)) * Dish.StandardWeight1, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientAminoAcid ON DishIngredient.IngredientID = IngredientAminoAcid.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientAminoAcid.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientAminoAcid.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight1,Dish.ServeCount1 )NAV ";
            return QueryBuilder;
        }

        public static string V_AminoDishValuePlanIII()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientAminoAcid.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientAminoAcid.NutrientValue / 100)) / IIF(Dish.ServeCount2 * Dish.StandardWeight2 = 0,1,Dish.ServeCount2 * Dish.StandardWeight2)) * Dish.StandardWeight2, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientAminoAcid ON DishIngredient.IngredientID = IngredientAminoAcid.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientAminoAcid.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientAminoAcid.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight2,Dish.ServeCount2 )NAV ";
            return QueryBuilder;
        }

        public static string V_FattyDishValuePlan0()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientFattyAcid.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientFattyAcid.NutrientValue / 100)) / IIF(Dish.ServeCount * Dish.StandardWeight = 0,1,Dish.ServeCount * Dish.StandardWeight)) * 100, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientFattyAcid ON DishIngredient.IngredientID = IngredientFattyAcid.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientFattyAcid.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientFattyAcid.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight,Dish.ServeCount )NAV ";
            return QueryBuilder;
        }

        public static string V_FattyDishValuePlanI()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientFattyAcid.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientFattyAcid.NutrientValue / 100)) / IIF(Dish.ServeCount * Dish.StandardWeight = 0,1,Dish.ServeCount * Dish.StandardWeight)) * Dish.StandardWeight, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientFattyAcid ON DishIngredient.IngredientID = IngredientFattyAcid.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientFattyAcid.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientFattyAcid.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight,Dish.ServeCount )NAV ";
            return QueryBuilder;
        }

        public static string V_FattyDishValuePlanII()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientFattyAcid.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientFattyAcid.NutrientValue / 100)) / IIF(Dish.ServeCount1 * Dish.StandardWeight1 = 0,1,Dish.ServeCount1 * Dish.StandardWeight1)) * Dish.StandardWeight1, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientFattyAcid ON DishIngredient.IngredientID = IngredientFattyAcid.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientFattyAcid.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientFattyAcid.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight1,Dish.ServeCount1 )NAV ";
            return QueryBuilder;
        }

        public static string V_FattyDishValuePlanIII()
        {
            string QueryBuilder = " (SELECT DishIngredient.DishID, IngredientFattyAcid.NutrientID, NSys_Nutrient.NutrientParam,  ";
            QueryBuilder = QueryBuilder + " ROUND((SUM((IngredientStandardUnit.StandardWeight * DishIngredient.Quantity) * (IngredientFattyAcid.NutrientValue / 100)) / IIF(Dish.ServeCount2 * Dish.StandardWeight2 = 0,1,Dish.ServeCount2 * Dish.StandardWeight2)) * Dish.StandardWeight2, 2) AS NutrientValue,  ";
            QueryBuilder = QueryBuilder + " NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup  ";
            QueryBuilder = QueryBuilder + " FROM (((DishIngredient INNER JOIN Dish ON DishIngredient.DishID = Dish.DishID) INNER JOIN IngredientFattyAcid ON DishIngredient.IngredientID = IngredientFattyAcid.IngredientID) INNER JOIN IngredientStandardUnit ON (DishIngredient.IngredientID = IngredientStandardUnit.IngredientID) AND (DishIngredient.StandardUnitID = IngredientStandardUnit.StandardUnitID)) INNER JOIN NSys_Nutrient ON IngredientFattyAcid.NutrientID = NSys_Nutrient.NutrientID  ";
            QueryBuilder = QueryBuilder + " GROUP BY  DishIngredient.DishID, IngredientFattyAcid.NutrientID, NSys_Nutrient.NutrientParam, NSys_Nutrient.NutrientUnit,NSys_Nutrient.NutrientGroup,Dish.StandardWeight2,Dish.ServeCount2 )NAV ";
            return QueryBuilder;
        }                   

        #endregion
    }
}
