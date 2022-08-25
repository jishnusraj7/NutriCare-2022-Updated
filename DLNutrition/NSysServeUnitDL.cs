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
    public class NSysServeUnitDL
    {
        public static List<NSysServeUnit> GetList()
        {
            List<NSysServeUnit> serveUnitList = new List<NSysServeUnit>();
            NSysServeUnit serveUnit = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drServeUnit = dbManager.ExecuteReader(CommandType.Text, "SELECT  ServeUnitID, ServeUnitName FROM NSysServeUnit Order By ServeUnitID"))
                {
                    while (drServeUnit.Read())
                    {
                        serveUnit = FillDataRecord(drServeUnit);
                        if (serveUnit != null)
                        {
                            serveUnitList.Add(serveUnit);
                        }
                    }
                    drServeUnit.Close();
                }
                return serveUnitList;
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

        public static NSysServeUnit GetItem(int serveUnitID)
        {
            NSysServeUnit serveUnit = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT  ServeUnitID, ServeUnitName FROM NSysServeUnit Where ServeUnitID = " + serveUnitID))
                {
                    if (dr.Read())
                    {
                        serveUnit = FillDataRecord(dr);
                    }
                    dr.Close();
                }
                return serveUnit;
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

        private static NSysServeUnit FillDataRecord(IDataReader dataReader)
        {
            NSysServeUnit serveUnit = new NSysServeUnit();
            serveUnit.ServeUnitID = dataReader.IsDBNull(dataReader.GetOrdinal("ServeUnitID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ServeUnitID"));
            serveUnit.ServeUnitName = dataReader.IsDBNull(dataReader.GetOrdinal("ServeUnitName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("ServeUnitName"));
            return serveUnit;
        }
    }
}
