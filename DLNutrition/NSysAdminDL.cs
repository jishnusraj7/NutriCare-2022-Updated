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
    public class NSysAdminDL
    {        
        public static List<NSysAdmin> GetAyurvedicFeatures(int FeatureID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select Description,VPK From NSysAyurBodyFeatureOption Where FeatureID = " + FeatureID))
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

        public static List<NSysAdmin> GetFoodHabit()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select FoodHabitID,FoodHabitName From NSysFoodHabit"))
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

        public static NSysAdmin GetItemFoodHabit(int foodHabitID)
        {            
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select FoodHabitID,FoodHabitName From NSysFoodHabit Where FoodHabitID = " + foodHabitID))
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

        public static List<NSysAdmin> GetLifeStyle()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select LifeStyleID,LifeStyleName From NSysLifeStyle"))
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

        public static List<NSysAdmin> GetSex()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select SexID,SexName from NSysSex Where IsMajor = 1 "))
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
        public static List<NSysAdmin> GetChildSex()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select SexID,SexName from NSysSex Where IsMajor = 0"))
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

        public static List<NSysAdmin> GetReligion()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ReligionID,ReligionName From NSysReligion"))
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

        public static List<NSysAdmin> GetCountry()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select CountryID, CountryName from NSysCountry"))
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

        public static List<NSysAdmin> GetState(int CountryID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select StateID,StateName from NSysState where CountryID = " + CountryID + " Order by StateName ASC"))
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

        public static List<NSysAdmin> GetCity(int StateID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select CityID,CityName from NSysCity where StateID = " + StateID + " Order by CityName ASC"))
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

        public static List<NSysAdmin> GetBloodGroup()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT BloodGroupName, BloodGroupID FROM  NSysBloodGroup"))
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

        public static List<NSysAdmin> GetExercise()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ExerciseID,ExerciseName from NSysExerciseCalorie Order by ExerciseID Asc"))
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

        public static List<NSysAdmin> GetBodyType()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select BodyTypeID,BodyTypeName from NSysBodyType"))
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

        public static List<NSysAdmin> GetJobNature()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select JobNatureID,JobNatureName from NSysJobNature"))
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

        public static List<NSysAdmin> GetClinicalUnit()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ClinicalUnitID,ClinicalUnitName from NSysClinicalUnit"))
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

        public static List<NSysAdmin> GetClinicalUnit(int ClinicalUnitID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ClinicalUnitID,ClinicalUnitName from NSysClinicalUnit where ClinicalUnitID = " + ClinicalUnitID + ""))
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

        public static List<NSysAdmin> GetUnit()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select UnitID,UnitName from NSysUnit"))
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


        public static List<NSysAdmin> GetThoughts()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ThoughtID,ThoughtDescription,ThoughtBy From TOTD"))
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

        public static int GetMaxThoughtID()
        {
            int result = 0;
            DBHelper dbManager = null;
            DBHelper.Parameters[] columnParameters = null;
            try
            {
                dbManager = new DBHelper();
                string SqlQry = "Select Coalesce(Max(ThoughtID),0) from TOTD";
                result = Functions.Convert2Int(Convert.ToString(dbManager.ExecuteScalar(CommandType.Text, SqlQry, columnParameters)));
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

        public static List<NSysAdmin> GetClinicalParamValue(int ClinicalParamID, int SexID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select ClinicalParamNormalValueFrom,ClinicalParamNormalValueTo,ClinicalParamNormalValueDescription from NSysClinicalParamValue Where ClinicalParamID = " + ClinicalParamID + " and SexID = " + SexID + " Order By  CInt(ClinicalParamValueID) Asc"))
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
                string SqlQry = "Select UnitValue from NSysUnitValue where UnitIDFrom = " + UnitIDFrom + " And UnitIDTo = " + UnitIDTo;
                result = (float)Functions.Convert2Double(Convert.ToString(dbManager.ExecuteScalar(CommandType.Text, SqlQry, columnParameters)));
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

        public static List<NSysAdmin> GetUnitValue(int UnitID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select UnitIDTo,UnitValue From NSysUnitValue where UnitIDFrom = " + UnitID + " And UnitIDTo <> " + UnitID + " And UnitValue<> 0 Order By UnitIDTo "))
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

        public static List<NSysAdmin> GetKeyWord(int GroupEnum)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select KeyWordID,KeyWordDescription,KeyWordCode From  NSysSearchKeyword Where GroupEnum = '" + GroupEnum + "'"))
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

        public static List<NSysAdmin> GetKeyWord(int GroupEnum, string SearchKeyWord)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select KeyWordID,KeyWordDescription,KeyWordCode From  NSysSearchKeyword Where GroupEnum = '" + GroupEnum + "' and KeywordCode Not In (" + SearchKeyWord + " ) ORDER BY KeyWordID"))
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

        public static List<NSysAdmin> GetInfant()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select InfantID,InfantName from NSysInfant"))
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

        public static List<NSysAdmin> GetInfantHWRange(int SexID, int InfantID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select Height,Weight from NSysInfantHWRange Where SexID = " + SexID + " And InfantId = " + InfantID + ""))
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

        public static List<NSysAdmin> GetBMIImpact()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select BMIImpactID,BMIImpactName,BMIIMpactDescription,BMIIMpactComments,BMIIMpactSuggetions From NSysBMIImpact"))
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

        public static List<NSysAdmin> GetBMIImpact(int BMIImpactID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select BMIIMpactSuggetions,BMIImpactImagePath From NSysBMIImpactImage Where BMIImpactID = " + BMIImpactID + ""))
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

        public static List<NSysAdmin> GetClinicalParamNormalValue(int ClinicalParamID, int ClinicalAgeGroupID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select UnitType,NormalLow,NormalHigh,IsNormal from NSysClinicalParam Where ClinicalParamID = " + ClinicalParamID + " and ClinicalAgeGroupID = " + ClinicalAgeGroupID + ""))
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

        public static List<NSysAdmin> GetListHelp(int HelpItemID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select HelpItemName,HelpItemDescription from NSysHelpItem Where HelpItemID = " + HelpItemID + ""))
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

        public static List<NSysAdmin> GetFormFlow(int FlowID)
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select FormFlowName,FormFlowDescription from NSysFormFlow Where FormFlowID = " + FlowID + ""))
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

        public static List<NSysAdmin> GetPrivacyLegal()
        {
            List<NSysAdmin> adminList = new List<NSysAdmin>();
            NSysAdmin admin = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select PrivacyLegalName,PrivacyLegalDescription from NSysPrivacyLegal"))
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

        private static NSysAdmin FillDataRecord_AyurvedicFeatures(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.Description = dataReader.IsDBNull(dataReader.GetOrdinal("Description")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Description"));
            admin.VPK = dataReader.IsDBNull(dataReader.GetOrdinal("VPK")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("VPK"));
            return admin;

        }
        private static NSysAdmin FillDataRecord_FoodHabit(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.FoodHabitName = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("FoodHabitName"));
            admin.FoodHabitID = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("FoodHabitID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_LifeStyle(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.LifeStyleName = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("LifeStyleName"));
            admin.LifeStyleID = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("LifeStyleID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_Sex(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.SexName = dataReader.IsDBNull(dataReader.GetOrdinal("SexName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("SexName"));
            admin.SexID = dataReader.IsDBNull(dataReader.GetOrdinal("SexID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("SexID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_Religion(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.ReligionName = dataReader.IsDBNull(dataReader.GetOrdinal("ReligionName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ReligionName"));
            admin.ReligionID = dataReader.IsDBNull(dataReader.GetOrdinal("ReligionID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ReligionID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_Country(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.CountryName = dataReader.IsDBNull(dataReader.GetOrdinal("CountryName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("CountryName"));
            admin.CountryID = dataReader.IsDBNull(dataReader.GetOrdinal("CountryID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("CountryID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_State(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.StateName = dataReader.IsDBNull(dataReader.GetOrdinal("StateName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("StateName"));
            admin.StateID = dataReader.IsDBNull(dataReader.GetOrdinal("StateID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StateID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_City(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.CityName = dataReader.IsDBNull(dataReader.GetOrdinal("CityName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("CityName"));
            admin.CityID = dataReader.IsDBNull(dataReader.GetOrdinal("CityID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("CityID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_BloodGroup(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.BloodGroupName = dataReader.IsDBNull(dataReader.GetOrdinal("BloodGroupName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BloodGroupName"));
            admin.BloodGroupID = dataReader.IsDBNull(dataReader.GetOrdinal("BloodGroupID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BloodGroupID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_Exercise(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.ExerciseName = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ExerciseName"));
            admin.ExerciseID = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ExerciseID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_BodyType(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.BodyTypeName = dataReader.IsDBNull(dataReader.GetOrdinal("BodyTypeName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BodyTypeName"));
            admin.BodyTypeID = dataReader.IsDBNull(dataReader.GetOrdinal("BodyTypeID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BodyTypeID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_JobNature(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.JobNatureName = dataReader.IsDBNull(dataReader.GetOrdinal("JobNatureName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("JobNatureName"));
            admin.JobNatureID = dataReader.IsDBNull(dataReader.GetOrdinal("JobNatureID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("JobNatureID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_ClinicalUnit(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.ClinicalUnitName = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalUnitName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ClinicalUnitName"));
            admin.ClinicalUnitID = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalUnitID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ClinicalUnitID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_Thoughts(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.ThoughtDescription = dataReader.IsDBNull(dataReader.GetOrdinal("ThoughtDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ThoughtDescription"));
            admin.ThoughtBy = dataReader.IsDBNull(dataReader.GetOrdinal("ThoughtBy")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ThoughtBy"));
            admin.ThoughtID = dataReader.IsDBNull(dataReader.GetOrdinal("ThoughtID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ThoughtID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_Unit(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.UnitName = dataReader.IsDBNull(dataReader.GetOrdinal("UnitName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("UnitName"));
            admin.UnitID = dataReader.IsDBNull(dataReader.GetOrdinal("UnitID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("UnitID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_UnitValue(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.UnitID = dataReader.IsDBNull(dataReader.GetOrdinal("UnitIDTo")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("UnitIDTo"));
            admin.UnitValue = dataReader.IsDBNull(dataReader.GetOrdinal("UnitValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("UnitValue"));
            return admin;

        }

        private static NSysAdmin FillDataRecord_KeyWord(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.KeyWordCode = dataReader.IsDBNull(dataReader.GetOrdinal("KeyWordCode")) ? "" : dataReader.GetString(dataReader.GetOrdinal("KeyWordCode"));
            admin.KeyWordDescription = dataReader.IsDBNull(dataReader.GetOrdinal("KeyWordDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("KeyWordDescription"));
            admin.KeyWordID = dataReader.IsDBNull(dataReader.GetOrdinal("KeyWordID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("KeyWordID"));
            return admin;

        }

        private static NSysAdmin FillDataRecord_Infant(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.InfantName = dataReader.IsDBNull(dataReader.GetOrdinal("InfantName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("InfantName"));
            admin.InfantID = dataReader.IsDBNull(dataReader.GetOrdinal("InfantID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("InfantID"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_InfantHWRange(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.InfantHeight = dataReader.IsDBNull(dataReader.GetOrdinal("Height")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Height"));
            admin.InfantWeight = dataReader.IsDBNull(dataReader.GetOrdinal("Weight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Weight"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_BMIImpact(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.BMIImpactName = dataReader.IsDBNull(dataReader.GetOrdinal("BMIImpactName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIImpactName"));
            admin.BMIImpactID = dataReader.IsDBNull(dataReader.GetOrdinal("BMIImpactID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BMIImpactID"));
            admin.BMIIMpactDescription = dataReader.IsDBNull(dataReader.GetOrdinal("BMIIMpactDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIIMpactDescription"));
            admin.BMIIMpactComments = dataReader.IsDBNull(dataReader.GetOrdinal("BMIIMpactComments")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIIMpactComments"));
            admin.BMIIMpactSuggetions = dataReader.IsDBNull(dataReader.GetOrdinal("BMIIMpactSuggetions")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIIMpactSuggetions"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_BMIImpactImage(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.BMIIMpactSuggetions = dataReader.IsDBNull(dataReader.GetOrdinal("BMIIMpactSuggetions")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIIMpactSuggetions"));
            admin.BMIIMpactImagePath = dataReader.IsDBNull(dataReader.GetOrdinal("BMIIMpactImagePath")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BMIIMpactImagePath"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_ClinicalParamNormalValue(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.UnitType = dataReader.IsDBNull(dataReader.GetOrdinal("UnitType")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("UnitType"));
            admin.NormalLow = dataReader.IsDBNull(dataReader.GetOrdinal("NormalLow")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NormalLow"));
            admin.NormalHigh = dataReader.IsDBNull(dataReader.GetOrdinal("NormalHigh")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NormalHigh"));
            admin.IsNormal = dataReader.IsDBNull(dataReader.GetOrdinal("IsNormal")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsNormal"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_ClinicalParamValue(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.ClinicalParamNormalValueFrom = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalParamNormalValueFrom")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ClinicalParamNormalValueFrom"));
            admin.ClinicalParamNormalValueTo = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalParamNormalValueTo")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ClinicalParamNormalValueTo"));
            admin.ClinicalParamNormalValueDes = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalParamNormalValueDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ClinicalParamNormalValueDescription"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_HelpItem(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.HelpItemName = dataReader.IsDBNull(dataReader.GetOrdinal("HelpItemName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("HelpItemName"));
            admin.HelpItemDescription = dataReader.IsDBNull(dataReader.GetOrdinal("HelpItemDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("HelpItemDescription"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_FormFlow(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.FormFlowName = dataReader.IsDBNull(dataReader.GetOrdinal("FormFlowName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("FormFlowName"));
            admin.FormFlowDescription = dataReader.IsDBNull(dataReader.GetOrdinal("FormFlowDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("FormFlowDescription"));
            return admin;
        }

        private static NSysAdmin FillDataRecord_PrivacyLegal(IDataReader dataReader)
        {
            NSysAdmin admin = new NSysAdmin();
            admin.PrivacyLegalName = dataReader.IsDBNull(dataReader.GetOrdinal("PrivacyLegalName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("PrivacyLegalName"));
            admin.PrivacyLegalDescription = dataReader.IsDBNull(dataReader.GetOrdinal("PrivacyLegalDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("PrivacyLegalDescription"));
            return admin;
        }
    }
}
