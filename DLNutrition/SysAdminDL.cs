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
    public class SysAdminDL
    {        
        public static List<SysAdmin> GetAyurvedicFeatures(int FeatureID, int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select Description,VPK From " + Views.V_AyurvedicFeatures() + " Where FeatureID = " + FeatureID + " And LanguageID = " + LanguageID))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_AyurvedicFeatures(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetFoodHabit(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select FoodHabitID,FoodHabitName From " + Views.V_FoodHabit() + " Where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_FoodHabit(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static SysAdmin GetItemFoodHabit(int foodHabitID, int LanguageID)
        {            
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select FoodHabitID,FoodHabitName From " + Views.V_FoodHabit() + " Where LanguageID = " + LanguageID + " AND FoodHabitID = " + foodHabitID))
                {
                    if (dr.Read())
                    {
                        admin = FillDataRecord_FoodHabit(dr);
                    }
                    dr.Close();
                }
                return admin;
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

        public static List<SysAdmin> GetLifeStyle(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select LifeStyleID,LifeStyleName From " + Views.V_LifeStyle() + " Where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_LifeStyle(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetSex(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select SexID,SexName from " + Views.V_Sex() + " Where IsMajor = 1  And LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_Sex(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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
        public static List<SysAdmin> GetChildSex(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select SexID,SexName from " + Views.V_Sex() + " Where IsMajor = 0 And LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_Sex(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetReligion(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ReligionID,ReligionName From " + Views.V_Religion() + " Where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_Religion(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetCountry(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select CountryID, CountryName from " + Views.V_Country() + " where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_Country(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetState(int LanguageID, int CountryID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select StateID,StateName from " + Views.V_State() + " where LanguageID  = '" + LanguageID + "' And CountryID = '" + CountryID + "' Order by StateName ASC"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_State(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetCity(int LanguageID, int StateID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select CityID,CityName from " + Views.V_City() + " where LanguageID  = '" + LanguageID + "' And StateID = '" + StateID + "' Order by CityName ASC"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_City(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetBloodGroup(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select BloodGroupID,BloodGroupName from " + Views.V_BloodGroup() + " where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_BloodGroup(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetExercise(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ExerciseID,ExerciseName from " + Views.V_Exercise() + " where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_Exercise(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetExercise()
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ExerciseID,ExerciseName from SysExerciseCalorie Order by ExerciseID Asc"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_Exercise(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetBodyType(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select BodyTypeID,BodyTypeName from " + Views.V_BodyType() + " where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_BodyType(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetJobNature(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select JobNatureID,JobNatureName from " + Views.V_JobNature() + " where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_JobNature(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetClinicalUnit(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ClinicalUnitID,ClinicalUnitName from " + Views.V_ClinicalUnit() + " where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_ClinicalUnit(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetClinicalUnit(int LanguageID, int ClinicalUnitID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ClinicalUnitID,ClinicalUnitName from " + Views.V_ClinicalUnit() + " where ClinicalUnitID = '" + ClinicalUnitID + "' And LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_ClinicalUnit(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetUnit(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select UnitID,UnitName from " + Views.V_Unit() + " where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_Unit(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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


        public static List<SysAdmin> GetThoughts(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ThoughtID,ThoughtDescription,ThoughtBy From " + Views.V_TOTD() + " where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_Thoughts(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static int GetMaxThoughtID(int LanguageID)
        {
            int result = 0;
            DBHelper dbManager = null;
            DBHelper.Parameters[] columnParameters = null;
            try
            {
                dbManager = new DBHelper();
                string SqlQry = "Select Coalesce(Max(ThoughtID),0) from " + Views.V_TOTD() + " where LanguageID = '" + LanguageID + "'";
                result = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.Text, SqlQry, columnParameters));
                return result;
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

        public static List<SysAdmin> GetClinicalParamValue(int ClinicalParamID, int SexID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ClinicalParamNormalValueFrom,ClinicalParamNormalValueTo,ClinicalParamNormalValueDescription from SysClinicalParamValue Where ClinicalParamID = '" + ClinicalParamID + "' and SexID = '" + SexID + "' Order By  Convert(int,ClinicalParamValueID) Asc"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_ClinicalParamValue(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static float GetUnitValue(int UnitIDFrom, int UnitIDTo)
        {
            float result = 0;
            DBHelper dbManager = null;
            DBHelper.Parameters[] columnParameters = null;
            try
            {
                dbManager = new DBHelper();
                string SqlQry = "Select UnitValue from SysUnitValue where UnitIDFrom = " + UnitIDFrom + " And UnitIDTo = " + UnitIDTo;
                result = (float)Convert.ToDouble(dbManager.ExecuteScalar(CommandType.Text, SqlQry, columnParameters));
                return result;
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

        public static List<SysAdmin> GetUnitValue(int UnitID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select UnitIDTo,UnitValue From SysUnitValue where UnitIDFrom = '" + UnitID + "' And UnitIDTo <> '" + UnitID + "' And UnitValue<> 0 Order By UnitIDTo "))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_UnitValue(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetKeyWord(int LanguageID, int GroupEnum)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select KeyWordID,KeyWordDescription,KeyWordCode From  " + Views.V_SearchKeyword() + " Where LanguageID = '" + LanguageID + "' and GroupEnum = '" + GroupEnum + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_KeyWord(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetKeyWord(int LanguageID, int GroupEnum, string SearchKeyWord)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select KeyWordID,KeyWordDescription,KeyWordCode From  " + Views.V_SearchKeyword() + " Where LanguageID = '" + LanguageID + "' and GroupEnum = '" + GroupEnum + "' and KeywordCode Not In (" + SearchKeyWord + " ) ORDER BY KeyWordID"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_KeyWord(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetInfant(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select InfantID,InfantName from " + Views.V_Infant() + " Where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_Infant(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetInfantHWRange(int SexID, int InfantID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select Height,Weight from SysInfantHWRange Where SexID = '" + SexID + "' And InfantId = '" + InfantID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_InfantHWRange(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetBMIImpact(int LanguageID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select BMIImpactID,BMIImpactName,BMIIMpactDescription,BMIIMpactComments,BMIIMpactSuggetions From " + Views.V_BMIImpact() + " Where LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_BMIImpact(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetBMIImpact(int LanguageID, int BMIImpactID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select BMIIMpactSuggetions,BMIImpactImagePath From SysBMIImpactImage Where BMIImpactID = '" + BMIImpactID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_BMIImpactImage(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetClinicalParamNormalValue(int ClinicalParamID, int ClinicalAgeGroupID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select UnitType,NormalLow,NormalHigh,IsNormal from SysClinicalParam Where ClinicalParamID = '" + ClinicalParamID + "' and ClinicalAgeGroupID = '" + ClinicalAgeGroupID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_ClinicalParamNormalValue(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetListHelp(int HelpItemID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select HelpItemName,HelpItemDescription from SysHelpItem Where HelpItemID = '" + HelpItemID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_HelpItem(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetFormFlow(int FlowID)
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select FormFlowName,FormFlowDescription from SysFormFlow Where FormFlowID = '" + FlowID + "'"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_FormFlow(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        public static List<SysAdmin> GetPrivacyLegal()
        {
            List<SysAdmin> adminList = new List<SysAdmin>();
            SysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select PrivacyLegalName,PrivacyLegalDescription from SysPrivacyLegal"))
                {
                    while (dr.Read())
                    {
                        admin = FillDataRecord_PrivacyLegal(dr);

                        if (admin != null)
                        {
                            adminList.Add(admin);
                        }
                    }
                    dr.Close();
                }
                return adminList;
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

        private static SysAdmin FillDataRecord_AyurvedicFeatures(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.Description = dataReader.IsDBNull(dataReader.GetOrdinal("Description")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Description"));
            admin.VPK = dataReader.IsDBNull(dataReader.GetOrdinal("VPK")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("VPK"));
            return admin;

        }
        private static SysAdmin FillDataRecord_FoodHabit(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.FoodHabitName = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("FoodHabitName"));
            admin.FoodHabitID = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("FoodHabitID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_LifeStyle(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.LifeStyleName = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("LifeStyleName"));
            admin.LifeStyleID = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("LifeStyleID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_Sex(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.SexName = dataReader.IsDBNull(dataReader.GetOrdinal("SexName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("SexName"));
            admin.SexID = dataReader.IsDBNull(dataReader.GetOrdinal("SexID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("SexID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_Religion(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.ReligionName = dataReader.IsDBNull(dataReader.GetOrdinal("ReligionName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ReligionName"));
            admin.ReligionID = dataReader.IsDBNull(dataReader.GetOrdinal("ReligionID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ReligionID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_Country(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.CountryName = dataReader.IsDBNull(dataReader.GetOrdinal("CountryName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("CountryName"));
            admin.CountryID = dataReader.IsDBNull(dataReader.GetOrdinal("CountryID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("CountryID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_State(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.StateName = dataReader.IsDBNull(dataReader.GetOrdinal("StateName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("StateName"));
            admin.StateID = dataReader.IsDBNull(dataReader.GetOrdinal("StateID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StateID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_City(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.CityName = dataReader.IsDBNull(dataReader.GetOrdinal("CityName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("CityName"));
            admin.CityID = dataReader.IsDBNull(dataReader.GetOrdinal("CityID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("CityID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_BloodGroup(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.BloodGroupName = dataReader.IsDBNull(dataReader.GetOrdinal("BloodGroupName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BloodGroupName"));
            admin.BloodGroupID = dataReader.IsDBNull(dataReader.GetOrdinal("BloodGroupID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BloodGroupID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_Exercise(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.ExerciseName = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ExerciseName"));
            admin.ExerciseID = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ExerciseID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_BodyType(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.BodyTypeName = dataReader.IsDBNull(dataReader.GetOrdinal("BodyTypeName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BodyTypeName"));
            admin.BodyTypeID = dataReader.IsDBNull(dataReader.GetOrdinal("BodyTypeID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BodyTypeID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_JobNature(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.JobNatureName = dataReader.IsDBNull(dataReader.GetOrdinal("JobNatureName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("JobNatureName"));
            admin.JobNatureID = dataReader.IsDBNull(dataReader.GetOrdinal("JobNatureID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("JobNatureID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_ClinicalUnit(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.ClinicalUnitName = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalUnitName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ClinicalUnitName"));
            admin.ClinicalUnitID = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalUnitID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ClinicalUnitID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_Thoughts(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.ThoughtDescription = dataReader.IsDBNull(dataReader.GetOrdinal("ThoughtDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ThoughtDescription"));
            admin.ThoughtBy = dataReader.IsDBNull(dataReader.GetOrdinal("ThoughtBy")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ThoughtBy"));
            admin.ThoughtID = dataReader.IsDBNull(dataReader.GetOrdinal("ThoughtID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ThoughtID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_Unit(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.UnitName = dataReader.IsDBNull(dataReader.GetOrdinal("UnitName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("UnitName"));
            admin.UnitID = dataReader.IsDBNull(dataReader.GetOrdinal("UnitID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("UnitID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_UnitValue(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.UnitID = dataReader.IsDBNull(dataReader.GetOrdinal("UnitIDTo")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("UnitIDTo"));
            admin.UnitValue = dataReader.IsDBNull(dataReader.GetOrdinal("UnitValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("UnitValue"));
            return admin;

        }

        private static SysAdmin FillDataRecord_KeyWord(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.KeyWordCode = dataReader.IsDBNull(dataReader.GetOrdinal("KeyWordCode")) ? "" : dataReader.GetString(dataReader.GetOrdinal("KeyWordCode"));
            admin.KeyWordDescription = dataReader.IsDBNull(dataReader.GetOrdinal("KeyWordDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("KeyWordDescription"));
            admin.KeyWordID = dataReader.IsDBNull(dataReader.GetOrdinal("KeyWordID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("KeyWordID"));
            return admin;

        }

        private static SysAdmin FillDataRecord_Infant(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.InfantName = dataReader.IsDBNull(dataReader.GetOrdinal("InfantName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("InfantName"));
            admin.InfantID = dataReader.IsDBNull(dataReader.GetOrdinal("InfantID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("InfantID"));
            return admin;
        }

        private static SysAdmin FillDataRecord_InfantHWRange(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.InfantHeight = dataReader.IsDBNull(dataReader.GetOrdinal("Height")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Height"));
            admin.InfantWeight = dataReader.IsDBNull(dataReader.GetOrdinal("Weight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Weight"));
            return admin;
        }

        private static SysAdmin FillDataRecord_BMIImpact(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.BMIImpactName = dataReader.IsDBNull(dataReader.GetOrdinal("BMIImpactName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIImpactName"));
            admin.BMIImpactID = dataReader.IsDBNull(dataReader.GetOrdinal("BMIImpactID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BMIImpactID"));
            admin.BMIIMpactDescription = dataReader.IsDBNull(dataReader.GetOrdinal("BMIIMpactDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIIMpactDescription"));
            admin.BMIIMpactComments = dataReader.IsDBNull(dataReader.GetOrdinal("BMIIMpactComments")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIIMpactComments"));
            admin.BMIIMpactSuggetions = dataReader.IsDBNull(dataReader.GetOrdinal("BMIIMpactSuggetions")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIIMpactSuggetions"));
            return admin;
        }

        private static SysAdmin FillDataRecord_BMIImpactImage(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.BMIIMpactSuggetions = dataReader.IsDBNull(dataReader.GetOrdinal("BMIIMpactSuggetions")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIIMpactSuggetions"));
            admin.BMIIMpactImagePath = dataReader.IsDBNull(dataReader.GetOrdinal("BMIIMpactImagePath")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIIMpactImagePath"));
            return admin;
        }

        private static SysAdmin FillDataRecord_ClinicalParamNormalValue(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.UnitType = dataReader.IsDBNull(dataReader.GetOrdinal("UnitType")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("UnitType"));
            admin.NormalLow = dataReader.IsDBNull(dataReader.GetOrdinal("NormalLow")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NormalLow"));
            admin.NormalHigh = dataReader.IsDBNull(dataReader.GetOrdinal("NormalHigh")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NormalHigh"));
            admin.IsNormal = dataReader.IsDBNull(dataReader.GetOrdinal("IsNormal")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsNormal"));
            return admin;
        }

        private static SysAdmin FillDataRecord_ClinicalParamValue(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.ClinicalParamNormalValueFrom = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalParamNormalValueFrom")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ClinicalParamNormalValueFrom"));
            admin.ClinicalParamNormalValueTo = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalParamNormalValueTo")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ClinicalParamNormalValueTo"));
            admin.ClinicalParamNormalValueDes = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalParamNormalValueDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ClinicalParamNormalValueDescription"));
            return admin;
        }

        private static SysAdmin FillDataRecord_HelpItem(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.HelpItemName = dataReader.IsDBNull(dataReader.GetOrdinal("HelpItemName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("HelpItemName"));
            admin.HelpItemDescription = dataReader.IsDBNull(dataReader.GetOrdinal("HelpItemDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("HelpItemDescription"));
            return admin;
        }

        private static SysAdmin FillDataRecord_FormFlow(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.FormFlowName = dataReader.IsDBNull(dataReader.GetOrdinal("FormFlowName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("FormFlowName"));
            admin.FormFlowDescription = dataReader.IsDBNull(dataReader.GetOrdinal("FormFlowDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("FormFlowDescription"));
            return admin;
        }

        private static SysAdmin FillDataRecord_PrivacyLegal(IDataReader dataReader)
        {
            SysAdmin admin = new SysAdmin();
            admin.PrivacyLegalName = dataReader.IsDBNull(dataReader.GetOrdinal("PrivacyLegalName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("PrivacyLegalName"));
            admin.PrivacyLegalDescription = dataReader.IsDBNull(dataReader.GetOrdinal("PrivacyLegalDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("PrivacyLegalDescription"));
            return admin;
        }
    }
}
