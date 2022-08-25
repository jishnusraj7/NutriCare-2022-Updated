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
    public class GlossaryDL
    {
        public static List<SysNutrient> GetListNutrients(int LanguageID)
        {
            List<SysNutrient> NutrientList = new List<SysNutrient>();
            SysNutrient Nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drNutrient = dbManager.ExecuteReader(CommandType.Text, "Select * from " + Views.V_NutrientGlossary() + " Where LanguageID = '" + LanguageID + "' Order By NutrientID"))
                {
                    while (drNutrient.Read())
                    {
                        Nutrient = FillDataRecordNutrientList(drNutrient);
                        if (Nutrient != null)
                        {
                            NutrientList.Add(Nutrient);
                        }
                    }
                    drNutrient.Close();
                }
                return NutrientList;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }

        public static SysNutrient GetItemNutrients(int LanguageID,int NutrientID)
        {
            SysNutrient Nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drNutrient = dbManager.ExecuteReader(CommandType.Text, "Select NutrientDescription from " + Views.V_NutrientGlossary() + " Where LanguageID = '" + LanguageID + "' And NutrientID = '" + NutrientID + "'"))
                {
                    while (drNutrient.Read())
                    {
                        Nutrient = FillDataRecordNutrientItem(drNutrient);
                    }
                    drNutrient.Close();
                }
                return Nutrient;
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
        public static string GetItemNutrients(int LanguageID, string nutName)
        {
            string nutDesc = string.Empty;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                nutDesc = Convert.ToString(dbManager.ExecuteScalar(CommandType.Text, "Select NutrientDescription from " + Views.V_NutrientGlossary() + " Where LanguageID = '" + LanguageID + "' And NutrientParam = '" + nutName + "'"));
                
                return nutDesc;
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

        private static SysNutrient FillDataRecordNutrientList(IDataReader dataReader)
        {
            SysNutrient nutrient = new SysNutrient();
            nutrient.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientID"));
            nutrient.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            nutrient.NutrientDescription = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientDescription"));
            return nutrient;
        }

        private static SysNutrient FillDataRecordNutrientItem(IDataReader dataReader)
        {
            SysNutrient nutrient = new SysNutrient();
            nutrient.NutrientDescription = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientDescription"));
            return nutrient;
        }

        public static List<SysNutrient> GetListAyurveda(int LanguageID)
        {
            List<SysNutrient> NutrientList = new List<SysNutrient>();
            SysNutrient Nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drNutrient = dbManager.ExecuteReader(CommandType.Text, "Select * from " + Views.V_AyurGlossary() + " Where LanguageID = '" + LanguageID + "'"))
                {
                    while (drNutrient.Read())
                    {
                        Nutrient = FillDataRecordAyurvedaList(drNutrient);
                        if (Nutrient != null)
                        {
                            NutrientList.Add(Nutrient);
                        }
                    }
                    drNutrient.Close();
                }
                return NutrientList;
            }
            catch (Exception ex)
            {
                ExceptionManager.Publish(ex);
                throw ex;
            }
            finally
            {
                dbManager = null;
            }
        }

        public static SysNutrient GetItemAyurveda(int LanguageID, int AyurID)
        {
            SysNutrient Nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drNutrient = dbManager.ExecuteReader(CommandType.Text, "Select AyurDescription from " + Views.V_AyurGlossary() + " Where LanguageID = '" + LanguageID + "' And AyurID = '" + AyurID + "'"))
                {
                    while (drNutrient.Read())
                    {
                        Nutrient = FillDataRecordAyurvedaItem(drNutrient);
                    }
                    drNutrient.Close();
                }
                return Nutrient;
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

        private static SysNutrient FillDataRecordAyurvedaList(IDataReader dataReader)
        {
            SysNutrient nutrient = new SysNutrient();
            nutrient.AyurID = dataReader.IsDBNull(dataReader.GetOrdinal("AyurID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("AyurID"));
            nutrient.AyurParam = dataReader.IsDBNull(dataReader.GetOrdinal("AyurParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurParam"));
            return nutrient;
        }

        private static SysNutrient FillDataRecordAyurvedaItem(IDataReader dataReader)
        {
            SysNutrient nutrient = new SysNutrient();
            nutrient.AyurDescription = dataReader.IsDBNull(dataReader.GetOrdinal("AyurDescription")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurDescription"));
            return nutrient;
        }
    }
}
