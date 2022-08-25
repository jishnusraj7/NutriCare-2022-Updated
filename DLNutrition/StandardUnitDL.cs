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
    public class StandardUnitDL
    {
        public static List<StandardUnit> GetList()
        {
            List<StandardUnit> standardUnitList = new List<StandardUnit>();
            StandardUnit standardUnit = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT StandardUnitID, StandardUnitName, StandardUnitDisplay, StandardUnitType FROM StandardUnit"))
                {
                    while (dr.Read())
                    {
                        standardUnit = FillDataRecord(dr);

                        if (standardUnit != null)
                        {
                            standardUnitList.Add(standardUnit);
                        }
                    }
                    dr.Close();
                }
                return standardUnitList;
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

        public static StandardUnit GetItem(int standardUnitID)
        {
            StandardUnit standardUnit = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT StandardUnitID, StandardUnitName, StandardUnitDisplay, StandardUnitType FROM StandardUnit Where StandardUnitID = " + standardUnitID + ""))
                {
                    while (dr.Read())
                    {
                        standardUnit = FillDataRecord(dr);
                    }
                    dr.Close();
                }
                return standardUnit;
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

        private static StandardUnit FillDataRecord(IDataReader dataReader)
        {
            StandardUnit standardUnit = new StandardUnit();
            standardUnit.StandardUnitID = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitID"));
            standardUnit.StandardUnitType = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitType")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("StandardUnitType"));
            standardUnit.StandardUnitName = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitName")) ? "" : " " + dataReader.GetString(dataReader.GetOrdinal("StandardUnitName"));
            standardUnit.StandardUnitDisplay = dataReader.IsDBNull(dataReader.GetOrdinal("StandardUnitDisplay")) ? "" : " " + dataReader.GetString(dataReader.GetOrdinal("StandardUnitDisplay"));
            standardUnit.IsApplicable = false;
            return standardUnit;
        }
    }
}
