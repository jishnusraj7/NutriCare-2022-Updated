using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using BONutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Classes;
using NutritionViews;

namespace DLNutrition
{
    public class MemberDL
    {
        #region FamilyProfile

        public static List<Member> GetList(int FamilyID)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberID,MemberName,SexID,DOB,DisplayOrder from FamilyMember where FamilyID = '" + FamilyID + "' Order By DisplayOrder"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecord(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        public static List<Member> GetMemberList(int FamilyID)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberID,MemberName,SexID,DOB,DisplayOrder from FamilyMember where MemberName<>'' and FamilyID = '" + FamilyID + "' Order By DisplayOrder"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecord(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        public static List<Member> GetAllMemberList(int FamilyID)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberID,MemberName,SexID,DOB,DisplayOrder from FamilyMember where FamilyID = '" + FamilyID + "' Order By DisplayOrder"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecord(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        //private static Member FillDataRecord(IDataReader dataReader)
        //{
        //    Member member = new Member();
        //    member.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
        //    member.MemberName = dataReader.IsDBNull(dataReader.GetOrdinal("MemberName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MemberName"));
        //    member.SexID = dataReader.IsDBNull(dataReader.GetOrdinal("SexID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("SexID"));
        //    member.DOB = dataReader.IsDBNull(dataReader.GetOrdinal("DOB")) ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("DOB"));
        //    member.DisplayOrder = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayOrder")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("DisplayOrder"));
        //    return member;
        //}

        public static void Save(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Insert into FamilyMember (FamilyID,MemberName,SexID,DOB,DisplayOrder) " +
                            "Values (" + member.FamilyID + ",N'" + Functions.ProperCase(member.MemberName) + "'," + member.SexID + ",'" + member.DOB.ToString("dd/MMM/yyyy") + "'," + member.DisplayOrder + ")";
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

        public static void Edit(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Update FamilyMember Set MemberName = N'" + Functions.ProperCase(member.MemberName) + "',SexID = " + member.SexID + "," +
                        "DOB = '" + member.DOB.ToString("dd/MMM/yyyy") + "',DisplayOrder = " + member.DisplayOrder + " Where MemberID = " + member.MemberID + " and FamilyID = " + member.FamilyID;
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

        #endregion
        #region FamilyExercise

        public static List<Member> GetListFamilyExercise(int FamilyID, int MemberID)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select FamilyMember.MemberID,ExerciseID,ExerciseDurationMinutes,LifeStyleID from FamilyMemberExercise inner join FamilyMember on FamilyMember.MemberID = FamilyMemberExercise.MemberID where FamilyMember.MemberID = '" + MemberID + "'"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordExercise(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        private static Member FillDataRecordExercise(IDataReader dataReader)
        {
            Member member = new Member();
            member.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            member.ExerciseID = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ExerciseID"));
            member.ExerciseDuration = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseDurationMinutes")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ExerciseDurationMinutes"));
            member.LifeStyleID = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("LifeStyleID"));

            return member;
        }

        public static bool SaveFamilyExercise(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                if (FamilyExerciseIsExists(member.MemberID, member.ExerciseID) == 0)
                {
                    SqlQry = "Insert into FamilyMemberExercise (MemberID,ExerciseID,ExerciseDurationMinutes) " +
                                "Values (" + member.MemberID + "," + member.ExerciseID + "," + member.ExerciseDuration + ")";
                }
                else
                {
                    SqlQry = "Update FamilyMemberExercise Set ExerciseID = " + member.ExerciseID + ",ExerciseDurationMinutes = " + member.ExerciseDuration +
                             "Where MemberID = " + member.MemberID + " and ExerciseID = " + member.ExerciseID;
                }
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

        public static bool DeleteFamilyExercise(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                SqlQry = "Delete FamilyMemberExercise Where MemberID = " + member.MemberID;

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

        private static int FamilyExerciseIsExists(int MemberID, int ExerciseID)
        {
            int Count = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from FamilyMemberExercise where MemberID = " + MemberID + " and ExerciseID = " + ExerciseID;
                Count = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return Count;
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

        public static int GetExerciseType(int ExerciseID)
        {
            int ExerciseType;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select ExercisetypeID from " + Views.V_Exercise() + " where ExerciseID = " + ExerciseID;
                ExerciseType = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? Convert.ToInt32(dbHelper.ExecuteScalar(CommandType.Text, SqlQry)) : 0;

                return ExerciseType;
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

        public static int GetExerciseCalorie(int ExerciseID, double Weight)
        {
            int ExerciseCalorie;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select CalorieBurned from SysExerciseCalorie_LAN where ExerciseID  = " + ExerciseID + " and " + Weight + " >= WeightFrom  and " + Weight + " <= WeightTo ";
                ExerciseCalorie = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? Convert.ToInt32(dbHelper.ExecuteScalar(CommandType.Text, SqlQry)) : 0;

                return ExerciseCalorie;
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

        #endregion

        #region FamilyAyurvedic

        public static List<Member> GetListFamilyAyurvedic(int FamilyID, int MemberID)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberID,FeatureID,VPK from FamilyMemberAyurvedic where MemberID = '" + MemberID + "'"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordAyurvedic(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        private static Member FillDataRecordAyurvedic(IDataReader dataReader)
        {
            Member member = new Member();
            member.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            member.FeatureID = dataReader.IsDBNull(dataReader.GetOrdinal("FeatureID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("FeatureID"));
            member.VPK = dataReader.IsDBNull(dataReader.GetOrdinal("VPK")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("VPK"));

            return member;
        }

        public static bool SaveFamilyAyurvedic(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                if (FamilyAyurvedicIsExists(member.MemberID, member.FeatureID) == 0)
                {
                    SqlQry = "Insert into FamilyMemberAyurvedic (MemberID,FeatureID,VPK) " +
                                "Values (" + member.MemberID + "," + member.FeatureID + "," + member.VPK + ")";
                }
                else
                {
                    SqlQry = "Update FamilyMemberAyurvedic Set FeatureID = " + member.FeatureID + ",VPK = " + member.VPK +
                             "Where MemberID = " + member.MemberID + " and FeatureID = " + member.FeatureID;
                }
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

        public static void DeleteFamilyAyurvedic(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Update FamilyMemberAyurvedic Set FeatureID = " + member.FeatureID + ",VPK = " + 0 +
                              "Where MemberID = " + member.MemberID + " and FeatureID = " + member.FeatureID;
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

        private static int FamilyAyurvedicIsExists(int MemberID, int FeatureID)
        {
            int Count = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from FamilyMemberAyurvedic where MemberID = " + MemberID + " and FeatureID =" + FeatureID;
                Count = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return Count;
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

        public static int GetAyurvedicFeatureCount()
        {
            int Count = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from SysAyurBodyFeature";
                Count = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return Count;
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

        #endregion

        #region FamilyGeneral

        public static List<Member> GetListFamilyGeneral(int FamilyID, int MemberID)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberID,Height,Weight,BodyTypeID,JobNatureID,JobDuration,SleepTime,Smoking,Drinking,Diabetes,Cholosterol from FamilyMemberGeneral where MemberID = '" + MemberID + "'"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordGeneral(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        private static Member FillDataRecordGeneral(IDataReader dataReader)
        {
            Member member = new Member();
            member.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            member.Height = dataReader.IsDBNull(dataReader.GetOrdinal("Height")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Height"));
            member.Weight = dataReader.IsDBNull(dataReader.GetOrdinal("Weight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Weight"));
            member.BodyTypeID = dataReader.IsDBNull(dataReader.GetOrdinal("BodyTypeID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BodyTypeID"));
            member.JobNatureID = dataReader.IsDBNull(dataReader.GetOrdinal("JobNatureID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("JobNatureID"));
            member.JobDuration = dataReader.IsDBNull(dataReader.GetOrdinal("JobDuration")) ? "" : dataReader.GetString(dataReader.GetOrdinal("JobDuration"));
            member.SleepTime = dataReader.IsDBNull(dataReader.GetOrdinal("SleepTime")) ? "" : dataReader.GetString(dataReader.GetOrdinal("SleepTime"));
            member.Smoking = dataReader.IsDBNull(dataReader.GetOrdinal("Smoking")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("Smoking"));
            member.Drinking = dataReader.IsDBNull(dataReader.GetOrdinal("Drinking")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("Drinking"));
            member.Diabetes = dataReader.IsDBNull(dataReader.GetOrdinal("Diabetes")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("Diabetes"));
            member.Cholosterol = dataReader.IsDBNull(dataReader.GetOrdinal("Cholosterol")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("Cholosterol"));

            return member;
        }

        public static bool SaveFamilyGeneral(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                if (FamilyGeneralIsExists(member.MemberID) == 0)
                {
                    SqlQry = "Insert into FamilyMemberGeneral (MemberID,Height,Weight,BodyTypeID,JobNatureID,JobDuration,SleepTime,Smoking,Drinking,Diabetes,Cholosterol) " +
                                "Values (" + member.MemberID + "," + member.Height + "," + member.Weight + "," + member.BodyTypeID + "," + member.JobNatureID + ",'" + member.JobDuration + "'," +
                                "'" + member.SleepTime + "'," + Convert.ToByte(member.Smoking) + "," + Convert.ToByte(member.Drinking) + "," + Convert.ToByte(member.Diabetes) + "," + Convert.ToByte(member.Cholosterol) + ")";
                }
                else
                {
                    SqlQry = "Update FamilyMemberGeneral Set Height = " + member.Height + ",Weight = " + member.Weight + "," +
                        "BodyTypeID = " + member.BodyTypeID + ",JobNatureID = " + member.JobNatureID + ",JobDuration = '" + member.JobDuration + "'" +
                        ",SleepTime = '" + member.SleepTime + "',Smoking = " + Convert.ToByte(member.Smoking) + ",Drinking = " + Convert.ToByte(member.Drinking) + ",Diabetes = " + Convert.ToByte(member.Diabetes) + ",Cholosterol = " + Convert.ToByte(member.Cholosterol) +
                        " Where MemberID = " + member.MemberID;
                }
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

        public static void UpdateFamilyGeneralDiabetes(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Update FamilyMemberGeneral Set Diabetes = " + Convert.ToByte(1) + " Where MemberID = " + member.MemberID;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                dbHelper = null;
            }
        }

        public static void UpdateFamilyGeneralCholosterol(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Update FamilyMemberGeneral Set Cholosterol = " + Convert.ToByte(1) + " Where MemberID = " + member.MemberID;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
            }
            finally
            {
                dbHelper = null;
            }
        }

        public static bool DeleteFamilyGeneral(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Update FamilyMemberGeneral Set Height = " + 0 + ",Weight = " + 0 + "," +
                        "BodyTypeID = " + 0 + ",JobNatureID = " + 0 + ",JobDuration = '" + 0 + "'" +
                        ",SleepTime = '" + 0 + "',Smoking = " + Convert.ToByte(0) + ",Drinking = " + Convert.ToByte(0) + ",Diabetes = " + Convert.ToByte(0) + ",Cholosterol = " + Convert.ToByte(0) +
                        " Where MemberID = " + member.MemberID;
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

        private static int FamilyGeneralIsExists(int MemberID)
        {
            int Count = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from FamilyMemberGeneral where MemberID = '" + MemberID + "'"; ;
                Count = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return Count;
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

        #endregion

        #region FamilyClinical

        public static DateTime GetModifiedDate(int MemberID, int ClinicalParamID)
        {
            string SqlQry;
            DateTime dtModifiedDate;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Max(Modifieddate) from FamilyMemberClinical Where MemberID = " + MemberID + " and ClinicalParamID = " + ClinicalParamID;
                dtModifiedDate = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? Convert.ToDateTime(dbHelper.ExecuteScalar(CommandType.Text, SqlQry)) : Convert.ToDateTime("1900/01/01");
                return dtModifiedDate;
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
        public static List<Member> GetListFamilyClinical(int FamilyID, int MemberID, int ClinicalParamID)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberID,ClinicalParamID,ClinicalAgeGroupID,ModifiedDate,IsHigh,UnitType,ClinicalUnitID,CurrentValue,IsNormal from FamilyMemberClinical where MemberID = " + MemberID + " and ClinicalParamID = " + ClinicalParamID + " " +
                                                                                   "and Modifieddate = (Select Max(Modifieddate) from FamilyMemberClinical Where MemberID = " + MemberID + " and ClinicalParamID = " + ClinicalParamID + ")"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordClinical(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        public static List<Member> GetListFamilyClinical(int FamilyID, int MemberID, int ClinicalParamID, DateTime TestDate)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberID,ClinicalParamID,ClinicalAgeGroupID,ModifiedDate,IsHigh,UnitType,ClinicalUnitID,CurrentValue,IsNormal from FamilyMemberClinical where MemberID = " + MemberID + " and ClinicalParamID = " + ClinicalParamID + " and ModifiedDate = '" + TestDate.ToString("yyyy/MMM/dd") + "'"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordClinical(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        public static List<Member> GetListClinicalParamValue(int FamilyID, int MemberID, int ClinicalParamID)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "select MemberID,ClinicalParamID,ClinicalAgeGroupID,ModifiedDate,IsHigh,UnitType,ClinicalUnitID,CurrentValue,IsNormal from FamilyMemberClinical Where MemberID = " + MemberID + " and ClinicalParamID = " + ClinicalParamID + "order by Modifieddate Desc"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordClinical(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        private static Member FillDataRecordClinical(IDataReader dataReader)
        {
            Member member = new Member();
            member.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            member.ClinicalParamID = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalParamID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ClinicalParamID"));
            member.ClinicalAgeGroupID = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalAgeGroupID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ClinicalAgeGroupID"));
            member.ModifiedDate = dataReader.IsDBNull(dataReader.GetOrdinal("ModifiedDate")) ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("ModifiedDate"));
            member.IsHigh = dataReader.IsDBNull(dataReader.GetOrdinal("IsHigh")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsHigh"));
            member.UnitType = dataReader.IsDBNull(dataReader.GetOrdinal("UnitType")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("UnitType"));
            member.ClinicalUnitID = dataReader.IsDBNull(dataReader.GetOrdinal("ClinicalUnitID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ClinicalUnitID"));
            member.CurrentValue = dataReader.IsDBNull(dataReader.GetOrdinal("CurrentValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("CurrentValue"));
            member.IsNormal = dataReader.IsDBNull(dataReader.GetOrdinal("IsNormal")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsNormal"));

            return member;
        }

        public static bool SaveFamilyClinical(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                if (IsExistsMedicalRecords(member.MemberID, member.ClinicalParamID, member.ModifiedDate) == 0)
                {
                    SqlQry = "Insert into FamilyMemberClinical (MemberID,ClinicalParamID,ClinicalAgeGroupID,ModifiedDate,IsHigh,UnitType,ClinicalUnitID,CurrentValue,IsNormal,KeyWordCode) " +
                                "Values (" + member.MemberID + "," + member.ClinicalParamID + "," + member.ClinicalAgeGroupID + ",'" + member.ModifiedDate + "'," + Convert.ToByte(member.IsHigh) + "," + Convert.ToByte(member.UnitType) +
                                "," + Convert.ToByte(member.ClinicalUnitID) + "," + member.CurrentValue + "," + Convert.ToByte(member.IsNormal) + ",'" + member.KeyWordCode + "' )";
                }
                else
                {
                    SqlQry = "Update FamilyMemberClinical Set ClinicalAgeGroupID = " + member.ClinicalAgeGroupID +
                       ",IsHigh = " + Convert.ToByte(member.IsHigh) + ",UnitType = " + Convert.ToByte(member.UnitType) +
                       ",ClinicalUnitID = " + Convert.ToByte(member.ClinicalUnitID) + ",CurrentValue = " + member.CurrentValue + ",IsNormal = " + Convert.ToByte(member.IsNormal) +
                       ",KeyWordCode = '" + member.KeyWordCode + "'" +
                       " Where MemberID = " + member.MemberID + " and ClinicalParamID = " + member.ClinicalParamID + " and ModifiedDate = '" + member.ModifiedDate.ToString("yyyy/MMM/dd") + "'";
                }

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

        public static int IsExistsMedicalRecords(int MemberID, int ClinicalparamID, DateTime TestDate)
        {
            int Count = 0;
            string cnt = "";
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from FamilyMemberClinical" +
                        " Where MemberID = " + MemberID + " and ClinicalparamID = " + ClinicalparamID + " and ModifiedDate = '" + TestDate.ToString("yyyy/MMM/dd") + "'";

                cnt = Convert.ToString(dbHelper.ExecuteScalar(CommandType.Text, SqlQry));
                Count = cnt != "" ? int.Parse(cnt) : 0;

                return Count;
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

        public static void DeleteFamilyClinical(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Update FamilyMemberClinical Set ClinicalAgeGroupID = " + 1 + "," +
                        "ModifiedDate = '" + member.ModifiedDate + "',IsHigh = " + Convert.ToByte(0) + ",UnitType = " + Convert.ToByte(0) +
                        ",ClinicalUnitID = " + Convert.ToByte(0) + ",CurrentValue = " + 0 + ",IsNormal = " + Convert.ToByte(0) +
                        ",KeyWordCode = '" + member.KeyWordCode + "'" +
                        " Where MemberID = " + member.MemberID + " and ClinicalParamID = " + member.ClinicalParamID;
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

        public static bool DeleteMedicalRecords(int MemberID, int ClinicalParamID, DateTime TestDate)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Delete from FamilyMemberClinical" +
                        " Where MemberID = " + MemberID + " and ClinicalParamID = '" + ClinicalParamID + "' and ModifiedDate = '" + TestDate.ToShortDateString() + "'";
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

        private static int FamilyClinicalIsExists(int MemberID, int ClinicalParamID)
        {
            int Count = 0;
            string cnt = "";
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from FamilyMemberClinical where MemberID = " + MemberID + " and ClinicalParamID = " + ClinicalParamID;
                cnt = Convert.ToString(dbHelper.ExecuteScalar(CommandType.Text, SqlQry));
                Count = cnt != "" ? int.Parse(cnt) : 0;
                return Count;
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

        public static int ClinicalParamIDIsExists(int MemberID, int ClinicalParamID)
        {
            int Count = 0;
            string cnt = "";
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from FamilyMemberClinical where MemberID = " + MemberID + " and ClinicalParamID = " + ClinicalParamID +
                         " and (( unittype = 1 and CurrentValue <> 0) or ( unittype = 0 and IsNormal <> 0))";

                cnt = Convert.ToString(dbHelper.ExecuteScalar(CommandType.Text, SqlQry));
                Count = cnt != "" ? int.Parse(cnt) : 0;
                return Count;
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

        public static int GetClinicalUnitID(int ClinicalParamID)
        {
            int ClinicalUnitID = 0;
            string clinicalUnit = "";
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select ClinicalUnitID from SysClinicalParam where ClinicalParamID = " + ClinicalParamID;
                clinicalUnit = Convert.ToString(dbHelper.ExecuteScalar(CommandType.Text, SqlQry));
                ClinicalUnitID = clinicalUnit != "" ? int.Parse(clinicalUnit) : 0;
                return ClinicalUnitID;
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

        public static int GetClinicalAgeGroupID(int sexID, int Age)
        {
            int ClinicalAgeGroupID = 0;
            string ageGroup = "";
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select ClinicalAgeGroupID from SysClinicalAgeGroup where sexID = " + sexID + " and Agefrom <= " + Age + " AND AgeTo >= " + Age;
                ageGroup = Convert.ToString(dbHelper.ExecuteScalar(CommandType.Text, SqlQry));
                ClinicalAgeGroupID = ageGroup != "" ? int.Parse(ageGroup) : 0;
                return ClinicalAgeGroupID;
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

        public static int GetMaximumClinicalParamID()
        {
            int MaximumClinicalParamID = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Max(ClinicalParamID) from SysClinicalParam";
                MaximumClinicalParamID = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? Convert.ToInt32(dbHelper.ExecuteScalar(CommandType.Text, SqlQry)) : 0;

                return MaximumClinicalParamID;
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

        #endregion
        #region FamilyMember

        public static List<Member> GetListFamilyMember(int FamilyID,int MemberID)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberID,MemberName,SexID,DOB,LifeStyleID,BloodGroupID,Pregnancy,Lactation,LactationType,DisplayImage from FamilyMember where FamilyID = " + FamilyID + " and MemberID = " + MemberID + ""))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordFamilyMember(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        public static Member GetItem(int MemberID)
        {            
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT FamilyMember.MemberID, FamilyMember.MemberName, FamilyMember.SexID, FamilyMember.DOB, FamilyMember.LifeStyleID, FamilyMember.BloodGroupID, FamilyMember.BodyTypeID, FamilyMember.Pregnancy, FamilyMember.Lactation, "+
                                                                                 " FamilyMember.LactationType, FamilyMember.DisplayImage, FamilyMember.Height, FamilyMember.Weight, FamilyMember.Waist, NSysSex.SexName, NSysBloodGroup.BloodGroupName, NSysLifeStyle.LifeStyleName, NSysBodyType.BodyTypeName "+
                                                                                 " FROM (((FamilyMember INNER JOIN NSysSex ON FamilyMember.SexID = NSysSex.SexID) INNER JOIN NSysBloodGroup ON FamilyMember.BloodGroupID = NSysBloodGroup.BloodGroupID) INNER JOIN NSysLifeStyle ON FamilyMember.LifeStyleID = NSysLifeStyle.LifeStyleID) "+
                                                                                 " INNER JOIN NSysBodyType ON FamilyMember.BodyTypeID = NSysBodyType.BodyTypeID Where FamilyMember.MemberID = " + MemberID))
                {
                    if (dr.Read())
                    {
                        member = FillDataRecord(dr);                        
                    }
                    dr.Close();
                }
                return member;
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

        public static List<Member> GetList(string searchCondition)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT FamilyMember.MemberID,FamilyMember.MemberName,FamilyMember.SexID,FamilyMember.DOB,FamilyMember.LifeStyleID,FamilyMember.BloodGroupID, " +
                                                                                  " FamilyMember.BodyTypeID,FamilyMember.Pregnancy,FamilyMember.Lactation,FamilyMember.LactationType,FamilyMember.DisplayImage, FamilyMember.Height,FamilyMember.Weight, "+
                                                                                  " FamilyMember.Waist,NSysSex.SexName, NSysBloodGroup.BloodGroupName, NSysLifeStyle.LifeStyleName, NSysBodyType.BodyTypeName "+
                                                                                  " FROM (((FamilyMember INNER JOIN NSysSex ON FamilyMember.SexID = NSysSex.SexID) INNER JOIN NSysBloodGroup ON FamilyMember.BloodGroupID = NSysBloodGroup.BloodGroupID) "+
                                                                                  " INNER JOIN NSysLifeStyle ON FamilyMember.LifeStyleID = NSysLifeStyle.LifeStyleID) INNER JOIN NSysBodyType ON FamilyMember.BodyTypeID = NSysBodyType.BodyTypeID Where 1=1 " + searchCondition + " ORDER BY FamilyMember.MemberName"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecord(dr);
                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        public static List<Member> GetMemberNameList()
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberID,MemberName FROM FamilyMember ORDER BY MemberName"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordMemberList(dr);
                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        private static Member FillDataRecordMemberList(IDataReader dataReader)
        {
            Member member = new Member();
            member.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            member.MemberName = dataReader.IsDBNull(dataReader.GetOrdinal("MemberName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MemberName"));
            if (member.ImagePath != string.Empty)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Members" + "\\" + member.MemberName + ".jpg"))
                {
                    member.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Members" + "\\" + member.MemberName + ".jpg";
                }
                else
                {
                    member.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg";
                }
            }
            else
            {
                member.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg";
            }
            return member;
        }

        private static Member FillDataRecord(IDataReader dataReader)
        {
            Member member = new Member();
            member.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            member.MemberName = dataReader.IsDBNull(dataReader.GetOrdinal("MemberName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MemberName"));
            member.SexID = dataReader.IsDBNull(dataReader.GetOrdinal("SexID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("SexID"));
            member.SexName = dataReader.IsDBNull(dataReader.GetOrdinal("SexName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("SexName"));
            member.DOB = dataReader.IsDBNull(dataReader.GetOrdinal("DOB")) ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("DOB"));
            member.LifeStyleID = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("LifeStyleID"));
            member.LifeStyleName = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("LifeStyleName"));
            member.BloodGroupID = dataReader.IsDBNull(dataReader.GetOrdinal("BloodGroupID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BloodGroupID"));
            member.BloodGroupName = dataReader.IsDBNull(dataReader.GetOrdinal("BloodGroupName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BloodGroupName"));
            member.BodyTypeID = dataReader.IsDBNull(dataReader.GetOrdinal("BodyTypeID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BodyTypeID"));
            member.BodyTypeName = dataReader.IsDBNull(dataReader.GetOrdinal("BodyTypeName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BodyTypeName"));
            member.Pregnancy = dataReader.IsDBNull(dataReader.GetOrdinal("Pregnancy")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("Pregnancy"));
            member.Lactation = dataReader.IsDBNull(dataReader.GetOrdinal("Lactation")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("Lactation"));
            member.LactationType = dataReader.IsDBNull(dataReader.GetOrdinal("LactationType")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("LactationType"));
            member.ImagePath = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayImage")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayImage"));
            member.Height = dataReader.IsDBNull(dataReader.GetOrdinal("Height")) ? (float)0 : (float) dataReader.GetDouble(dataReader.GetOrdinal("Height"));
            member.Weight = dataReader.IsDBNull(dataReader.GetOrdinal("Weight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Weight"));
            member.Waist = dataReader.IsDBNull(dataReader.GetOrdinal("Waist")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Waist"));

            if (member.ImagePath != string.Empty)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Members" + "\\" + member.MemberName + ".jpg"))
                {
                    member.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Members" + "\\" + member.MemberName + ".jpg";
                }
                else
                {
                    member.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg";
                }
            }
            else
            {
                member.ImagePath = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg";
            }

            DateTime Now = System.DateTime.Now;
            TimeSpan ts = Now.Subtract(member.DOB);
            member.Age = Functions.Convert2Int(Convert.ToString(ts.Days)) / 365;
            return member;
        }

        private static Member FillDataRecordFamilyMember(IDataReader dataReader)
        {
            Member member = new Member();
            member.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            member.MemberName = dataReader.IsDBNull(dataReader.GetOrdinal("MemberName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MemberName"));
            member.SexID = dataReader.IsDBNull(dataReader.GetOrdinal("SexID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("SexID"));
            member.DOB = dataReader.IsDBNull(dataReader.GetOrdinal("DOB")) ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("DOB"));            
            member.LifeStyleID = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("LifeStyleID"));
            member.BloodGroupID = dataReader.IsDBNull(dataReader.GetOrdinal("BloodGroupID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BloodGroupID"));
            member.Pregnancy = dataReader.IsDBNull(dataReader.GetOrdinal("Pregnancy")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("Pregnancy"));
            member.Lactation = dataReader.IsDBNull(dataReader.GetOrdinal("Lactation")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("Lactation"));
            member.LactationType = dataReader.IsDBNull(dataReader.GetOrdinal("LactationType")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("LactationType"));
            member.ImagePath = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayImage")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayImage"));            
            return member;
        }

        public static bool SaveFamilyMember(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;
            int memberID = 0;
            try
            {
                dbHelper = DBHelper.Instance;

                if (FamilyMemberIsExists(member.MemberID) == 0)
                {
                    SqlQry = "Insert into FamilyMember(MemberID,FamilyID,MemberName,SexID,DOB,LifeStyleID,BloodGroupID,Pregnancy,Lactation,LactationType,DisplayImage,Height,Weight,BodyTypeID,Waist) " +
                                "Values (" + member.MemberID + "," + member.FamilyID + ",'" + Functions.ProperCase(member.MemberName) + "'," + member.SexID + ",'" + member.DOB.ToString("dd/MMM/yyyy") + "'," + member.LifeStyleID + "," + member.BloodGroupID + "," + Convert.ToByte(member.Pregnancy) + ", " + Convert.ToByte(member.Lactation) + "," + member.LactationType + ",'" + member.ImagePath + "'," + member.Height + "," + member.Weight + "," + member.BodyTypeID + "," + member.Waist + ")";
                }
                else
                {
                    SqlQry = "Update FamilyMember Set MemberName = '" + Functions.ProperCase(member.MemberName) + "',SexID = " + member.SexID + "," +
                        " DOB = '" + member.DOB.ToString("dd/MMM/yyyy") + "',LifeStyleID = " + member.LifeStyleID + ",BloodGroupID = " + member.BloodGroupID + ",Pregnancy = " + Convert.ToByte(member.Pregnancy) + ",Lactation = " + Convert.ToByte(member.Lactation) + ",LactationType = " + member.LactationType + ",DisplayImage = '" + member.ImagePath + "', Height = " + member.Height + ",Weight = " + member.Weight + ", BodyTypeID = " + member.BodyTypeID + ", Waist = " + member.Waist + " Where MemberID = " + member.MemberID;
                }
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

        public static bool DeleteFamilyMember(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Delete from FamilyMember Where MemberID = " + member.MemberID;
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

        private static int FamilyMemberIsExists(int MemberID)
        {
            int Count = 0;
            string cnt = "";
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from FamilyMember where MemberID = " + MemberID ; ;                
                cnt = Convert.ToString(dbHelper.ExecuteScalar(CommandType.Text, SqlQry));
                Count = cnt != "" ? int.Parse(cnt) : 0;
                return Count;
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

        public static string GetMemberName(int FamilyID, int MemberID)
        {
            string MemberName = "";
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select MemberName from FamilyMember where MemberID = " + MemberID + ""; ;
                MemberName = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (string)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : "";

                return MemberName;
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

        public static string[] GetMemberNames(int FamilyID)
        {
            string[] MemberName = new string[10];
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select MemberName from FamilyMember where FamilyID = " + FamilyID + "";
                MemberName = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (string[]) dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : null;
                return MemberName;
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

        public static int GetFamilyID()
        {
            int ID = 0;
            string id = "";
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select FamilyID from Family ";
                id = Convert.ToString(dbHelper.ExecuteScalar(CommandType.Text, SqlQry));
                ID = id != "" ? int.Parse(id) : 0;
                return ID;
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

        public static int GetMemberID()
        {
            int ID = 0;
            string id = "";
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select IIF(ISNULL(Max(MemberID)),0,Max(MemberID)) + 1  From FamilyMember";
                ID = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;
                return ID;                
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

        #endregion

        #region MemberList

        public static List<Member> GetListMember(int FamilyID,int MemberID)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberName,SexID,DOB,BloodGroupID,LifeStyleID,DisplayImage from FamilyMember where FamilyID = " + FamilyID + " And MemberID = " + +MemberID + ""))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordMember(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        private static Member FillDataRecordMember(IDataReader dataReader)
        {
            Member member = new Member();
            member.MemberName = dataReader.IsDBNull(dataReader.GetOrdinal("MemberName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MemberName"));
            member.SexID = dataReader.IsDBNull(dataReader.GetOrdinal("SexID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("SexID"));
            member.DOB = dataReader.IsDBNull(dataReader.GetOrdinal("DOB")) ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("DOB"));
            member.BloodGroupID = dataReader.IsDBNull(dataReader.GetOrdinal("BloodGroupID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BloodGroupID"));
            member.LifeStyleID = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("LifeStyleID"));
            member.ImagePath = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayImage")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayImage"));

            return member;
        }

        #endregion

        #region FamilyHistory

        public static List<Member> GetListFamilyHistory(int FamilyID, int MemberID, string ParameterName)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select Modifieddate,ParameterValue from FamilyMemberHistory where MemberID = " + MemberID + " and ParameterName = '" + ParameterName + "' order by Modifieddate Desc"))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordHistory(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        public static double GetItemFamilyHistory(int FamilyID, int MemberID, string ParameterName, DateTime Modifieddate)
        {
            double Value = 0;
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select ParameterValue from FamilyMemberHistory where MemberID = " + MemberID + " and ParameterName = '" + ParameterName + "' and Modifieddate = DateValue('" + Modifieddate.ToString("yyyy/MMM/dd") + "')";
                Value = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (double)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return Value;
                
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

        public static List<Member> GetListGoalHistory(int FamilyID, int MemberID, string ParameterName, DateTime StartDate, DateTime EndDate)
        {
            List<Member> memberList = new List<Member>();
            Member member = null;
            DBHelper dbManager = null;
            string SqlQuery = "";
            try
            {
                dbManager = DBHelper.Instance;
                SqlQuery = "Select Modifieddate,ParameterValue From FamilyMemberHistory Where MemberID = '" + MemberID + "' And ParameterName = '" + ParameterName + "' And Modifieddate Between '" + StartDate.ToString("yyyy/MMM/dd") + "' And '" + EndDate.ToString("yyyy/MMM/dd") + "' Order By Modifieddate Desc";
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, SqlQuery))
                {
                    while (dr.Read())
                    {
                        member = FillDataRecordHistory(dr);

                        if (member != null)
                        {
                            memberList.Add(member);
                        }
                    }
                    dr.Close();
                }
                return memberList;
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

        private static Member FillDataRecordHistory(IDataReader dataReader)
        {
            Member member = new Member();
            member.ModifiedDate = dataReader.IsDBNull(dataReader.GetOrdinal("ModifiedDate")) ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("ModifiedDate"));
            member.ParameterValue = dataReader.IsDBNull(dataReader.GetOrdinal("ParameterValue")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ParameterValue"));

            return member;
        }

        public static void SaveFamilyHistory(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                if (FamilyHistoryIsExists(member.MemberID, member.ModifiedDate, member.ParameterName) == 0)
                {
                    SqlQry = "Insert into FamilyMemberHistory (MemberID,ParameterName,ParameterValue,ModifiedDate) " +
                                    "Values (" + member.MemberID + ",'" + member.ParameterName + "'," + member.ParameterValue + ",'" + member.ModifiedDate.ToString("yyyy/MMM/dd") + "')";
                }
                else
                {
                    SqlQry = "Update FamilyMemberHistory set ParameterValue = " + member.ParameterValue +
                                    " Where MemberID = " + member.MemberID + " and ParameterName = '" + member.ParameterName + "' and ModifiedDate = DateValue('" + member.ModifiedDate.ToString("yyyy/MMM/dd") + "')";
                }
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

        public static void UpdateFamilyGeneral(int MemberID, double ParameterValue)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
               
                SqlQry = "Update FamilyMemberGeneral set Weight = " + ParameterValue +
                                    " Where MemberID = " + MemberID;
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

        private static int FamilyHistoryIsExists(int MemberID, DateTime Modifieddate, string ParameterName)
        {
            int Count = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from FamilyMemberHistory where MemberID = " + MemberID + " and Modifieddate = DateValue('" + Modifieddate.ToString("yyyy/MMM/dd") + "') and ParameterName = '" + ParameterName + "'";
                Count = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return Count;
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

        public static double GetLatestWeight(int MemberID, string ParameterName)
        {
            double LatestWeight = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select ParameterValue from FamilyMemberHistory where MemberID = " + MemberID + " and Modifieddate IN (Select Max(Modifieddate) from FamilyMemberHistory where MemberID = " + MemberID + "" +
                         " and ParameterName = '" + ParameterName + "') and ParameterName = '" + ParameterName + "'";
                LatestWeight = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != null ? (double)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return LatestWeight;
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

        public static bool DeleteFamilyHistory(int MemberID, string ParameterName ,DateTime TestDate)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Delete from FamilyMemberHistory" +
                        " Where MemberID = " + MemberID + " and ParameterName = '" + ParameterName + "' and ModifiedDate = DateValue('" + TestDate.ToString("yyyy/MMM/dd") + "')";
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

        public static bool DeleteFamilyHistory(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                SqlQry = "Delete FamilyMemberHistory Where MemberID = " + member.MemberID;

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

        #endregion

        #region MemberFoodSetting

        public static DataSet GetListFoodSetting(string condition)
        {
            DataSet dr = new DataSet();
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                dr = dbManager.DataAdapter(CommandType.Text, Views.V_MemberFoodSetting("where " + condition));

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

        public static DataSet GetListFoodSetting(string condition, List<Member> memberList)
        {
            DataSet dr = new DataSet();
            DBHelper dbManager = null;
            try
            {
                string sql = "Select DishID,DishName,LanguageID,MealTypeID,FamilyID,ServeUnit,WeekDay,DisplayName,PlanWeight";
                foreach (Member memberItem in memberList)
                {
                    if (memberItem.MemberName != string.Empty)
                    {
                        sql = sql + ",SUM((CASE MemberName WHEN '" + memberItem.MemberName + "' THEN DishCount ELSE 0 END)) [" + memberItem.MemberName + "]";
                    }
                }
                sql = sql + " FROM (select FamilyMember.MemberID,FamilyMemberMealPlan.DishID,DishCount,DishName,MemberName,LanguageId,MealTypeID,FamilyID,ServeUnit,WeekDay,DisplayName,PlanWeight from FamilyMemberMealPlan inner join Dish_LAN on FamilyMemberMealPlan.DishID=Dish_LAN.DishID inner join  FamilyMember on FamilyMemberMealPlan.MemberID=FamilyMember.MemberID inner join Dish on Dish.DishID=FamilyMemberMealPlan.DishID ) MFS where " + condition;
                sql = sql + " GROUP BY DishID,MealTypeID,DishName,DisplayName,LanguageID,FamilyID,ServeUnit,WeekDay,PlanWeight";
                dbManager = DBHelper.Instance;
                dr = dbManager.DataAdapter(CommandType.Text, sql);
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

        public static DataSet GetListFoodPlan(int dishID)
        {
            DataSet dr = new DataSet();
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                dr = dbManager.DataAdapter(CommandType.Text, "SELECT DishID,StandardWeight FROM Dish UNION SELECT DishID,StandardWeight1 FROM Dish UNION SELECT DishID,StandardWeight2 FROM Dish WHERE DishID = " + dishID);

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

        public static DataSet GetListFoodSetting(string condition, bool IsRegional)
        {
            DataSet dr = new DataSet();
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                if (IsRegional)
                {
                    dr = dbManager.DataAdapter(CommandType.Text, Views.V_MemberFoodSettingRegional("where " + condition));
                }
                else
                {
                    dr = dbManager.DataAdapter(CommandType.Text, Views.V_MemberFoodSetting("where " + condition));
                }
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

        public static List<FamilyMemberFoodSetting> GetFoodSettingWeekDayList()
        {
            List<FamilyMemberFoodSetting> familyMemberFoodSettingList = new List<FamilyMemberFoodSetting>();
            FamilyMemberFoodSetting familyMemberFoodSetting = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select distinct(WeekDay) from FamilyMemberMealPlan"))
                {
                    while (dr.Read())
                    {
                        familyMemberFoodSetting = FillMemberFoodSettingDays(dr);

                        if (familyMemberFoodSetting != null)
                        {
                            familyMemberFoodSettingList.Add(familyMemberFoodSetting);
                        }
                    }
                    dr.Close();
                }
                return familyMemberFoodSettingList;
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

        public static List<FamilyMemberFoodSetting> GetFoodSettingList(string condition)
        {
            List<FamilyMemberFoodSetting> familyMemberFoodSettingList = new List<FamilyMemberFoodSetting>();
            FamilyMemberFoodSetting familyMemberFoodSetting = null;
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
                        familyMemberFoodSetting = FillMemberFoodSettings(dr);

                        if (familyMemberFoodSetting != null)
                        {
                            familyMemberFoodSettingList.Add(familyMemberFoodSetting);
                        }
                    }
                    dr.Close();
                }
                return familyMemberFoodSettingList;
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

        private static FamilyMemberFoodSetting FillMemberFoodSettingDays(IDataReader dataReader)
        {
            FamilyMemberFoodSetting membFoodSetting = new FamilyMemberFoodSetting();
            membFoodSetting.WeekDay = dataReader.IsDBNull(dataReader.GetOrdinal("WeekDay")) ? DateTime.Now : dataReader.GetDateTime(dataReader.GetOrdinal("WeekDay"));
            return membFoodSetting;
        }

        private static FamilyMemberFoodSetting FillMemberFoodSettings(IDataReader dataReader)
        {
            FamilyMemberFoodSetting membFoodSetting = new FamilyMemberFoodSetting();
            membFoodSetting.MemberMealPlanID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberMealPlanID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberMealPlanID"));
            membFoodSetting.DishID = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            membFoodSetting.WeekDay = dataReader.IsDBNull(dataReader.GetOrdinal("WeekDay")) ? DateTime.Now : dataReader.GetDateTime(dataReader.GetOrdinal("WeekDay"));
            membFoodSetting.DishCount = dataReader.IsDBNull(dataReader.GetOrdinal("DishCount")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("DishCount"));
            membFoodSetting.PlanWeight = dataReader.IsDBNull(dataReader.GetOrdinal("PlanWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("PlanWeight"));
            membFoodSetting.MealTypeID = dataReader.IsDBNull(dataReader.GetOrdinal("MealTypeID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MealTypeID"));
            membFoodSetting.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            return membFoodSetting;
        }

        public static void SaveFoodSetting(FamilyMemberFoodSetting foodsetting)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Insert into FamilyMemberMealPlan (MemberID,WeekDay,MealTypeID,DishID,DishCount,PlanWeight) " +
                            "Values (" + foodsetting.MemberID + ",'" + foodsetting.WeekDay.ToString("dd/MMM/yyyy") + "'," + foodsetting.MealTypeID + "," + foodsetting.DishID + "," + foodsetting.DishCount + "," + foodsetting.PlanWeight + ")";
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

        public static void DeleteFoodSetting(string condition)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Delete from FamilyMemberMealPlan where " + condition;
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

        public static bool DeleteFamilyMemberMealPlan(Member member)
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

        public static double GetDishCalorie(int MemberID, int Month, int Year)
        {
            double Calorie = 0;
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Sum((Dish.Calorie / 100) * MemberMealPlan.DishCount * MemberMealPlan.PlanWeight) from ( SELECT * FROM FamilyMemberMealPlan Where MemberID =" + MemberID + " and DATEPART(month,weekday) = " + Month + " and DATEPART(year,weekday) = " + Year + ") MemberMealPlan " +
                         "INNER JOIN Dish on MemberMealPlan.DishID = Dish.DishID";
                Calorie = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (double)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return Calorie;
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

        public static int GetDishDays(int MemberID, int Month, int Year)
        {
            int Days = 0;
            string SqlQry;
            DBHelper dbHelper = null;
            DataSet ds = new DataSet();
            try
            {
                dbHelper = DBHelper.Instance;
                //SqlQry = "Select Count(Distinct(WeekDay)) From FamilyMemberMealPlan Where MemberID =" + MemberID + " and DATEPART(month,weekday) = " + Month + " and DATEPART(year,weekday) = " + Year + " GROUP BY WeekDay";
                //Days = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;
                SqlQry = "Select Distinct WeekDay From FamilyMemberMealPlan Where MemberID = " + MemberID + " and DATEPART(month,weekday) = " + Month + " and DATEPART(year,weekday) = " + Year + " GROUP BY WeekDay";
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

        #endregion

        #region MemberFoodSettingDairy

        public static DataSet GetListFoodSettingDairy(string condition)
        {
            DataSet dr = new DataSet();
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                dr = dbManager.DataAdapter(CommandType.Text, Views.V_MemberFoodSettingDairy("where " + condition));

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

        public static List<FamilyMemberFoodSetting> GetFoodSettingDairyWeekDayList()
        {
            List<FamilyMemberFoodSetting> familyMemberFoodSettingList = new List<FamilyMemberFoodSetting>();
            FamilyMemberFoodSetting familyMemberFoodSetting = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select distinct(WeekDay) from FamilyMemberMealDairy"))
                {
                    while (dr.Read())
                    {
                        familyMemberFoodSetting = FillMemberFoodSettingDairyDays(dr);

                        if (familyMemberFoodSetting != null)
                        {
                            familyMemberFoodSettingList.Add(familyMemberFoodSetting);
                        }
                    }
                    dr.Close();
                }
                return familyMemberFoodSettingList;
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

        public static List<FamilyMemberFoodSetting> GetFoodSettingDairyList(string condition)
        {
            List<FamilyMemberFoodSetting> familyMemberFoodSettingList = new List<FamilyMemberFoodSetting>();
            FamilyMemberFoodSetting familyMemberFoodSetting = null;
            DBHelper dbManager = null;
            string sqlqry = "";
            try
            {
                dbManager = DBHelper.Instance;
                if (condition != "")
                {
                    sqlqry = "Select * from FamilyMemberMealDairy where " + condition;
                }
                else
                {
                    sqlqry = "Select * from FamilyMemberMealDairy";
                }
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, sqlqry))
                {
                    while (dr.Read())
                    {
                        familyMemberFoodSetting = FillMemberFoodSettingsDairy(dr);

                        if (familyMemberFoodSetting != null)
                        {
                            familyMemberFoodSettingList.Add(familyMemberFoodSetting);
                        }
                    }
                    dr.Close();
                }
                return familyMemberFoodSettingList;
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

        private static FamilyMemberFoodSetting FillMemberFoodSettingDairyDays(IDataReader dataReader)
        {
            FamilyMemberFoodSetting membFoodSetting = new FamilyMemberFoodSetting();
            membFoodSetting.Week = dataReader.IsDBNull(dataReader.GetOrdinal("WeekDay")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            return membFoodSetting;
        }

        private static FamilyMemberFoodSetting FillMemberFoodSettingsDairy(IDataReader dataReader)
        {
            FamilyMemberFoodSetting membFoodSetting = new FamilyMemberFoodSetting();
            membFoodSetting.MemberMealPlanID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberMealDiaryID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberMealDiaryID"));
            membFoodSetting.DishID = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            membFoodSetting.Week = dataReader.IsDBNull(dataReader.GetOrdinal("WeekDay")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            membFoodSetting.DishCount = dataReader.IsDBNull(dataReader.GetOrdinal("DishCount")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("DishCount"));
            membFoodSetting.DishCalorie = dataReader.IsDBNull(dataReader.GetOrdinal("DishCalorie")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("DishCalorie"));
            membFoodSetting.PlanWeight = dataReader.IsDBNull(dataReader.GetOrdinal("PlanWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("PlanWeight"));
            membFoodSetting.MealTypeID = dataReader.IsDBNull(dataReader.GetOrdinal("MealTypeID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MealTypeID"));
            membFoodSetting.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            return membFoodSetting;
        }

        public static void SaveFoodSettingDairy(FamilyMemberFoodSetting foodsetting)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Insert into FamilyMemberMealDairy (MemberID,WeekDay,MealTypeID,DishID,DishCount,PlanWeight,DishCalorie) " +
                            "Values (" + foodsetting.MemberID + ",'" + foodsetting.Week + "'," + foodsetting.MealTypeID + "," + foodsetting.DishID + "," + foodsetting.DishCount + "," + foodsetting.PlanWeight + "," + foodsetting.DishCalorie + ")";
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
                SqlQry = "Delete from FamilyMemberMealDairy where " + condition;
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

                SqlQry = "Delete FamilyMemberMealDairy Where MemberID = " + member.MemberID;

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

        public static double GetDishCalorie(int MemberID)
        {
            double Calorie;
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                //SqlQry = "Select Sum((DishCalorie/100) * DishCount * PlanWeight) from FamilyMemberMealDairy Where MemberID  = " + MemberID;
                SqlQry = "Select Sum(DishCalorie * DishCount * PlanWeight) from FamilyMemberMealDairy Where MemberID  = " + MemberID;
                Calorie = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (double)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return Calorie;
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

        public static int GetDishDays(int MemberID)
        {
            int Days = 0;
            string SqlQry;
            DBHelper dbHelper = null;
            DataSet ds = new DataSet();
            try
            {
                dbHelper = DBHelper.Instance;
                //SqlQry = "Select Count(Distinct(WeekDay)) From FamilyMemberMealDairy Where MemberID =" + MemberID;
                //Days = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;
                SqlQry = "Select Distinct(WeekDay) From FamilyMemberMealDairy Where MemberID =" + MemberID + " GROUP BY WeekDay";
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

        #endregion

        #region FamilyMedical

        public static DataSet GetListMedicalRecords(int ClinicalAgeGroupID, int MemberID)
        {
            DataSet dr = new DataSet();
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                dr = dbManager.DataAdapter(CommandType.Text, Views.V_MemberMedicalRecords(ClinicalAgeGroupID, MemberID));

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

        public static bool DeleteMedicalRecords(int MemberID, DateTime TestDate)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Delete from FamilyMemberClinical" +
                        " Where MemberID = " + MemberID + " and ModifiedDate = '" + TestDate.ToShortDateString() + "'";
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

        #endregion


        #region GoalSetting

        public static bool DeleteFamilyGoalSetting(Member member)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                SqlQry = "Delete GoalSetting Where MemberID = " + member.MemberID;

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

        #endregion

    }
}
