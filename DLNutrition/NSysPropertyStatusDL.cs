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
    public class NSysPropertyStatusDL
    {
        public static List<NSysPropertyStatus> GetList(int PropertyType)
        {
            List<NSysPropertyStatus> propertyStatusList = new List<NSysPropertyStatus>();
            NSysPropertyStatus propertyStatus = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT PropertyStatusID,PropertyStatusType, PropertyStatusName FROM  NSysPropertyStatus Where PropertyStatusType = " + PropertyType + " Order By PropertyStatusID"))
                {
                    while (dr.Read())
                    {
                        propertyStatus = FillDataRecord(dr);

                        if (propertyStatus != null)
                        {
                            propertyStatusList.Add(propertyStatus);
                        }
                    }
                    dr.Close();
                }
                return propertyStatusList;
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
       
        public static NSysPropertyStatus GetItem(int propertyStatusID)
        {
            NSysPropertyStatus propertyStatus = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "SELECT PropertyStatusID,PropertyStatusType, PropertyStatusName FROM  NSysPropertyStatus Where PropertyStatusID = " + propertyStatusID + "Order By PropertyStatusID"))
                {
                    if (dr.Read())
                    {
                        propertyStatus = FillDataRecord(dr);
                    }
                    dr.Close();
                }
                return propertyStatus;
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

        private static NSysPropertyStatus FillDataRecord(IDataReader dataReader)
        {
            NSysPropertyStatus propertyStatus = new NSysPropertyStatus();
            propertyStatus.PropertyStatusID = dataReader.IsDBNull(dataReader.GetOrdinal("PropertyStatusID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("PropertyStatusID"));
            propertyStatus.PropertyStatusName = dataReader.IsDBNull(dataReader.GetOrdinal("PropertyStatusName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("PropertyStatusName"));
            propertyStatus.PropertyStatusType = dataReader.IsDBNull(dataReader.GetOrdinal("PropertyStatusType")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("PropertyStatusType"));
            return propertyStatus;
        }
    }
}
