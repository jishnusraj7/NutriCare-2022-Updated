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
    public class GoalSettingDL
    {
        public static List<GoalSetting> GetList(int FamilyID, int MemberID)
        {
            List<GoalSetting> goalSettingList = new List<GoalSetting>();
            GoalSetting goalSetting = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * from GoalSetting where FamilyID = '" + FamilyID + "'and MemberID = '" + MemberID + "'"))
                {
                    while (dr.Read())
                    {
                        goalSetting = FillDataRecord(dr);
                        if (goalSetting != null)
                        {
                            goalSettingList.Add(goalSetting);
                        }
                    }
                    dr.Close();
                }
                return goalSettingList;
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

        public static List<GoalSetting> GetItem(int FamilyID, int MemberID, int GoalID)
        {
            List<GoalSetting> goalSettingList = new List<GoalSetting>();
            GoalSetting goalSetting = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * from GoalSetting where FamilyID = '" + FamilyID + "'and MemberID = '" + MemberID + "'and GoalSettingID = '" + GoalID + "'"))
                {
                    while (dr.Read())
                    {
                        goalSetting = FillDataRecord(dr);
                        if (goalSetting != null)
                        {
                            goalSettingList.Add(goalSetting);
                        }
                    }
                    dr.Close();
                }
                return goalSettingList;
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

        public static GoalSetting GetItem(int FamilyID, int MemberID)
        {
            GoalSetting goalSetting = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * from GoalSetting where FamilyID = '" + FamilyID + "'and MemberID = '" + MemberID + "'"))
                {
                    if (dr.Read())
                    {
                        goalSetting= new GoalSetting();
                        goalSetting = FillDataRecord(dr);
                       
                    }
                    dr.Close();
                }
                return goalSetting;
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

        private static GoalSetting FillDataRecord(IDataReader dataReader)
        {
            GoalSetting goalSetting = new GoalSetting();
            goalSetting.GoalID = dataReader.IsDBNull(dataReader.GetOrdinal("GoalSettingID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("GoalSettingID"));
            goalSetting.StartDate = dataReader.IsDBNull(dataReader.GetOrdinal("StartDate")) ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("StartDate"));
            goalSetting.EndDate = dataReader.IsDBNull(dataReader.GetOrdinal("EndDate")) ? DateTime.MinValue : dataReader.GetDateTime(dataReader.GetOrdinal("EndDate"));
            goalSetting.PresentWeight = dataReader.IsDBNull(dataReader.GetOrdinal("PresentWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("PresentWeight"));
            goalSetting.DesiredGoal = dataReader.IsDBNull(dataReader.GetOrdinal("DesiredGoal")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DesiredGoal"));
            goalSetting.DesiredWeight = dataReader.IsDBNull(dataReader.GetOrdinal("DesiredWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("DesiredWeight"));
            goalSetting.ConsiderCalorieInTake = dataReader.IsDBNull(dataReader.GetOrdinal("ConsiderCalorieInTake")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("ConsiderCalorieInTake"));
            goalSetting.PresentCalorieInTake = dataReader.IsDBNull(dataReader.GetOrdinal("PresentCalorieInTake")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("PresentCalorieInTake"));
            goalSetting.ExerciseCalorieInTake = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseCalorieInTake")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ExerciseCalorieInTake"));
            goalSetting.CalorieInTake = dataReader.IsDBNull(dataReader.GetOrdinal("CalorieInTake")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("CalorieInTake"));

            return goalSetting;
        }

        public static bool SaveGoal(GoalSetting goalSetting)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                if (GoalIsExists(goalSetting.FamilyID, goalSetting.MemberID) == 0)
                {
                    SqlQry = "Insert into GoalSetting (FamilyID,MemberID,StartDate,EndDate,PresentWeight,DesiredGoal,DesiredWeight,ConsiderCalorieInTake,PresentCalorieIntake,ExerciseCalorieInTake,CalorieIntake) " +
                                "Values (" + goalSetting.FamilyID + "," + goalSetting.MemberID + ",'" + goalSetting.StartDate.ToString("dd/MMM/yyyy") + "','" + goalSetting.EndDate.ToString("dd/MMM/yyyy") + "'," +
                                goalSetting.PresentWeight + "," + goalSetting.DesiredGoal + "," + goalSetting.DesiredWeight + "," + Convert.ToByte(goalSetting.ConsiderCalorieInTake) + "," + goalSetting.PresentCalorieInTake + "," + goalSetting.ExerciseCalorieInTake + "," + goalSetting.CalorieInTake + ")";
                }
                else
                {
                    SqlQry = "Update GoalSetting Set StartDate = '" + goalSetting.StartDate.ToString("dd/MMM/yyyy") + "',EndDate = '" + goalSetting.EndDate.ToString("dd/MMM/yyyy") + "'," +
                        "PresentWeight = " + goalSetting.PresentWeight + ",DesiredGoal = " + goalSetting.DesiredGoal + ",DesiredWeight = " + goalSetting.DesiredWeight + "," +
                        "ConsiderCalorieInTake = " + Convert.ToByte(goalSetting.ConsiderCalorieInTake) + ",PresentCalorieIntake = " + goalSetting.PresentCalorieInTake + ",ExerciseCalorieInTake = " + goalSetting.ExerciseCalorieInTake + ",CalorieIntake = " + goalSetting.CalorieInTake +
                        " Where FamilyID = " + goalSetting.FamilyID + " and MemberID = " + goalSetting.MemberID; 
                }
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
                return true;
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

        private static int GoalIsExists(int FamilyID, int MemberID, int GoalID)
        {
            int Count = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from GoalSetting where FamilyID = '" + FamilyID + "'and MemberID = '" + MemberID + "'and GoalSettingID = '" + GoalID + "'";
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

        public static int GoalIsExists(int FamilyID, int MemberID)
        {
            int Count = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from GoalSetting where FamilyID = '" + FamilyID + "'and MemberID = '" + MemberID + "' and enddate >= getdate() ";
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

        public static List<GoalSetting> GetExerciseName(int LanguageID, int ExerciseTypeID)
        {
            List<GoalSetting> goalSettingList = new List<GoalSetting>();
            GoalSetting goalSetting = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select Top(5) ExerciseName from " + Views.V_Exercise() + " where ExerciseTypeID = '" + ExerciseTypeID + "' and  LanguageID = '" + LanguageID + "'"))
                {
                    while (dr.Read())
                    {
                        goalSetting = FillDataRecord_Exercise(dr);

                        if (goalSetting != null)
                        {
                            goalSettingList.Add(goalSetting);
                        }
                    }
                    dr.Close();
                }
                return goalSettingList;
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

        private static GoalSetting FillDataRecord_Exercise(IDataReader dataReader)
        {
            GoalSetting goalSetting = new GoalSetting();
            goalSetting.ExerciseName = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ExerciseName"));
            return goalSetting;
        }

        public static bool DeleteGoal(int FamilyID, int MemberID)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Delete from GoalSetting Where FamilyID = " + FamilyID + " and MemberID = " + MemberID;
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

        public static bool DeleteSimulationExercise(int MemberID)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                SqlQry = "Delete SimulationExercise Where MemberID = " + MemberID;

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


        public static bool SaveSimulationExercise(GoalSetting goalSetting)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                if (SimulationExerciseIsExists(goalSetting.MemberID, goalSetting.ExerciseID) == 0)
                {
                    SqlQry = "Insert into SimulationExercise (MemberID,ExerciseID,ExerciseDurationMinutes) " +
                                "Values (" + goalSetting.MemberID + "," + goalSetting.ExerciseID + "," + goalSetting.ExerciseDuration + ")";
                }
                else
                {
                    SqlQry = "Update SimulationExercise Set ExerciseID = " + goalSetting.ExerciseID + ",ExerciseDurationMinutes = " + goalSetting.ExerciseDuration +
                             "Where MemberID = " + goalSetting.MemberID + " and ExerciseID = " + goalSetting.ExerciseID;
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

        private static int SimulationExerciseIsExists(int MemberID, int ExerciseID)
        {
            int Count = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Count(*) from SimulationExercise where MemberID = " + MemberID + " and ExerciseID = " + ExerciseID;
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

        public static bool DeleteSimulationExercise(GoalSetting goalSetting)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;

                SqlQry = "Delete SimulationExercise Where MemberID = " + goalSetting.MemberID;

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

        public static List<GoalSetting> GetListSimulationExercise(int FamilyID, int MemberID)
        {
            List<GoalSetting> goalSettingList = new List<GoalSetting>();
            GoalSetting goalSetting = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select MemberID,ExerciseID,ExerciseDurationMinutes from SimulationExercise where MemberID = '" + MemberID + "'"))
                {
                    while (dr.Read())
                    {
                        goalSetting = FillDataRecordExercise(dr);

                        if (goalSetting != null)
                        {
                            goalSettingList.Add(goalSetting);
                        }
                    }
                    dr.Close();
                }
                return goalSettingList;
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

        public static List<GoalSetting> GetListSimulationExerciseName(int FamilyID, int MemberID)
        {
            List<GoalSetting> goalSettingList = new List<GoalSetting>();
            GoalSetting goalSetting = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select SimulationExercise.ExerciseID as ExerciseID,ExerciseName,ExerciseDurationMinutes From SysExerciseCalorie inner join SimulationExercise on SysExerciseCalorie.ExerciseID = SimulationExercise.ExerciseID where MemberID = '" + MemberID + "'"))
                {
                    while (dr.Read())
                    {
                        goalSetting = FillDataRecordExerciseName(dr);

                        if (goalSetting != null)
                        {
                            goalSettingList.Add(goalSetting);
                        }
                    }
                    dr.Close();
                }
                return goalSettingList;
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

        private static GoalSetting FillDataRecordExercise(IDataReader dataReader)
        {
            GoalSetting goalSetting = new GoalSetting();
            goalSetting.MemberID = dataReader.IsDBNull(dataReader.GetOrdinal("MemberID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MemberID"));
            goalSetting.ExerciseID = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ExerciseID"));
            goalSetting.ExerciseDuration = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseDurationMinutes")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ExerciseDurationMinutes"));

            return goalSetting;
        }

        private static GoalSetting FillDataRecordExerciseName(IDataReader dataReader)
        {
            GoalSetting goalSetting = new GoalSetting();
            goalSetting.ExerciseID = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ExerciseID"));
            goalSetting.ExerciseName = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ExerciseName"));
            goalSetting.ExerciseDuration = dataReader.IsDBNull(dataReader.GetOrdinal("ExerciseDurationMinutes")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ExerciseDurationMinutes"));

            return goalSetting;
        }



    }
}
