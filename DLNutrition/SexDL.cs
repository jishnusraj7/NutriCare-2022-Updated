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
    public class SexDL
    {
        public static Sex GetItem(int LanguageID, int SexID)
        {
            Sex sex = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * from " + Views.V_Sex() + " Where LanguageID = '" + LanguageID + "' AND SexID = '" + SexID + "'"))
                {
                    while (dr.Read())
                    {
                        sex = FillDataRecord(dr);
                    }
                    dr.Close();
                }
                return sex;
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


        private static Sex FillDataRecord(IDataReader dataReader)
        {
            Sex sex = new Sex();
            sex.SexID = dataReader.IsDBNull(dataReader.GetOrdinal("SexID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("SexID"));
            sex.SexName = dataReader.IsDBNull(dataReader.GetOrdinal("SexName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("SexName"));
            return sex;
        }
    }
}
