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
    public class EthnicDL
    {
        public static List<Ethnic> GetEthnic()
        {
            List<Ethnic> ethnicList = new List<Ethnic>();
            Ethnic ethnic = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select EthnicID,EthnicName From NSysEthnic"))
                {
                    while (dr.Read())
                    {
                        ethnic = FillDataRecordEthnic(dr);

                        if (ethnic != null)
                        {
                            ethnicList.Add(ethnic);
                        }
                    }
                    dr.Close();
                }
                return ethnicList;
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

        public static Ethnic GetItem(int ethnicID)
        {            
            Ethnic ethnic = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select EthnicID,EthnicName From NSysEthnic Where EthnicID = " + ethnicID))
                {
                    if (dr.Read())
                    {
                        ethnic = FillDataRecordEthnic(dr);
                    }
                    dr.Close();
                }
                return ethnic;
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

        private static Ethnic FillDataRecordEthnic(IDataReader dataReader)
        {
            Ethnic ethnic = new Ethnic();
            ethnic.EthnicID = dataReader.IsDBNull(dataReader.GetOrdinal("EthnicID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("EthnicID"));
            ethnic.EthnicName = dataReader.IsDBNull(dataReader.GetOrdinal("EthnicName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("EthnicName"));
            return ethnic;
        }
    }
}
