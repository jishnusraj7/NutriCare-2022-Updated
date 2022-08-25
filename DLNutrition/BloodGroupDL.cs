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
    public class BloodGroupDL
    {
        public static BloodGroup GetItem(int LanguageID, int BloodGroupID)
        {
            BloodGroup bloodGroup = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * from " + Views.V_BloodGroup() + " Where LanguageID = '" + LanguageID + "' AND BloodGroupID = '" + BloodGroupID + "'"))
                {
                    while (dr.Read())
                    {
                        bloodGroup = FillDataRecord(dr);
                    }
                    dr.Close();
                }
                return bloodGroup;
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

        private static BloodGroup FillDataRecord(IDataReader dataReader)
        {
            BloodGroup bloodGroup = new BloodGroup();
            bloodGroup.BloodGroupID = dataReader.IsDBNull(dataReader.GetOrdinal("BloodGroupID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("BloodGroupID"));
            bloodGroup.BloodGroupName = dataReader.IsDBNull(dataReader.GetOrdinal("BloodGroupName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("BloodGroupName"));
            return bloodGroup;
        }
    }
}
