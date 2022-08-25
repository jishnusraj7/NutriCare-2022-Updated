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
using DLNutrition.Common;

namespace DLNutrition
{
    public class FamilyDL
    {
        public static List<Family> GetList()
        {
            List<Family> familyList = new List<Family>();
            Family family = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select FamilyID,FamilyName,Address1,Address2,CountryID,StateID,CityID,PostCode," +
                                                                                  "Phone1,Phone2,ReligionID,FoodHabitID from Family"))
                {
                    while (dr.Read())
                    {
                        family = FillDataRecord(dr);
                        if (family != null)
                        {                            
                            familyList.Add(family);
                        }
                    }
                    dr.Close();
                }
                return familyList;
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

        private static Family FillDataRecord(IDataReader dataReader)
        {
            Family family = new Family();
            family.FamilyID = dataReader.IsDBNull(dataReader.GetOrdinal("FamilyID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("FamilyID"));
            family.FamilyName = dataReader.IsDBNull(dataReader.GetOrdinal("FamilyName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("FamilyName"));
            family.Address1 = dataReader.IsDBNull(dataReader.GetOrdinal("Address1")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Address1"));
            family.Address2 = dataReader.IsDBNull(dataReader.GetOrdinal("Address2")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Address2"));
            family.CountryID = dataReader.IsDBNull(dataReader.GetOrdinal("CountryID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("CountryID"));
            family.StateID = dataReader.IsDBNull(dataReader.GetOrdinal("StateID")) ? (byte)0 :dataReader.GetByte(dataReader.GetOrdinal("StateID"));
            family.CityID = dataReader.IsDBNull(dataReader.GetOrdinal("CityID")) ? (byte)0 :dataReader.GetByte(dataReader.GetOrdinal("CityID"));
            family.PostCode = dataReader.IsDBNull(dataReader.GetOrdinal("PostCode")) ? "" :dataReader.GetString(dataReader.GetOrdinal("PostCode"));
            family.Phone1 = dataReader.IsDBNull(dataReader.GetOrdinal("Phone1")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Phone1"));
            family.Phone2 = dataReader.IsDBNull(dataReader.GetOrdinal("Phone2")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Phone2"));
            family.ReligionID = dataReader.IsDBNull(dataReader.GetOrdinal("ReligionID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ReligionID"));
            family.FoodHabitID = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("FoodHabitID"));

            return family;
        }

        public static void Save(Family family)
        {
            string SqlQry;
            DBHelper dbHelper = null;
            
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Insert into Family (FamilyID,FamilyName,Address1,Address2,CountryID,StateID,CityID,PostCode,Phone1,Phone2,ReligionID,FoodHabitID," +
                            "PrimaryLanguageID,SecondaryLanguageID) " +
                            "Values (" + family.FamilyID + ",'" + Functions.ProperCase(family.FamilyName) + "','" + family.Address1 + "','" + family.Address2 + "'," +
                            "" + family.CountryID + "," + family.StateID + "," + family.CityID + ",'" + family.PostCode + "','" + family.Phone1 + "','" + family.Phone2 + "'," +
                            "" + family.ReligionID + "," + family.FoodHabitID + ","+
                            "" + family.PrimaryLanguageID + "," + family.SecondaryLanguageID + ")";
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

        public static void Edit(Family family)
        {
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Update Family Set FamilyID = " + family.FamilyID + ",FamilyName = '" + Functions.ProperCase(family.FamilyName) + "',Address1 = '" + family.Address1 + "'," +
                        "Address2 = '" + family.Address2 + "',CountryID = " + family.CountryID + ",StateID = " + family.StateID + ",CityID = " + family.CityID + "," +
                        "PostCode = '" + family.PostCode + "',Phone1 = '" + family.Phone1 + "',Phone2 = '" + family.Phone2 + "',ReligionID = " + family.ReligionID + "," +
                        "FoodHabitID = " + family.FoodHabitID + ",PrimaryLanguageID = " + family.PrimaryLanguageID;
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

        public static int GetCount(int FamilyID)
        {
            int Count = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select count(*) from  FamilyMember where  FamilyID = '" + FamilyID + "'";
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

        public static int GetID()
        {
            int FamilyID = 0;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select Coalesce(Max(FamilyID),0) From Family";
                FamilyID = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;
                return FamilyID;
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

        public static string GetStateKeyWord(int CountryID, int StateID)
        {
            string KeyWordCode = "";
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select KeyWordCode from SysState_LAN where CountryID = '" + CountryID + "' and StateID = '" + StateID + "'";
                KeyWordCode = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (string)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : "";

                return KeyWordCode;
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

        public static int GetEthnicID(int CountryID, int StateID)
        {
            int EthnicID;
            string SqlQry;
            DBHelper dbHelper = null;

            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select EthnicID from SysState_LAN where CountryID = '" + CountryID + "' and StateID = '" + StateID + "'";
                EthnicID = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return EthnicID;
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

        

    }
}
