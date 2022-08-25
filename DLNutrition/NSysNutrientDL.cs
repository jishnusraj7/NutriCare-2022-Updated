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
    public class NSysNutrientDL
    {
        #region SysNutrient

        public static List<NSysNutrient> GetListNutrient()
        {
            List<NSysNutrient> nutrientList = new List<NSysNutrient>();
            NSysNutrient nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * From NSys_Nutrient Order By NutrientGroup,NutrientID"))
                {
                    while (dr.Read())
                    {
                        nutrient = FillDataRecordNutrient(dr);

                        if (nutrient != null)
                        {
                            nutrientList.Add(nutrient);
                        }
                    }
                    dr.Close();
                }
                return nutrientList;
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

        public static List<NSysNutrient> GetListNutrient(byte nutrientGroup)
        {
            List<NSysNutrient> nutrientList = new List<NSysNutrient>();
            NSysNutrient nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * From NSys_Nutrient Where NutrientGroup = " + nutrientGroup))
                {
                    while (dr.Read())
                    {
                        nutrient = FillDataRecordNutrient(dr);

                        if (nutrient != null)
                        {
                            nutrientList.Add(nutrient);
                        }
                    }
                    dr.Close();
                }
                return nutrientList;
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

        public static List<NSysNutrient> GetListNutrientMain(byte nutrientGroup,bool IsMain)
        {
            List<NSysNutrient> nutrientList = new List<NSysNutrient>();
            NSysNutrient nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * From NSys_Nutrient Where NutrientGroup = " + nutrientGroup + " AND IsNutrientMain = " + IsMain))
                {
                    while (dr.Read())
                    {
                        nutrient = FillDataRecordNutrient(dr);

                        if (nutrient != null)
                        {
                            nutrientList.Add(nutrient);
                        }
                    }
                    dr.Close();
                }
                return nutrientList;
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

        public static NSysNutrient GetItemNutrient(byte nutrientID, byte nutrientGroup)
        {            
            NSysNutrient nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * From NSys_Nutrient Where NutrientID =" + nutrientID +" AND NutrientGroup = "+ nutrientGroup))
                {
                    if (dr.Read())
                    {
                        nutrient = FillDataRecordNutrient(dr);                        
                    }
                    dr.Close();
                }
                return nutrient;
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

        public static NSysNutrient GetNutrientID(string nutrientParam)
        {
            NSysNutrient nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * From NSys_Nutrient Where NutrientParam = '" + nutrientParam + "'"))
                {
                    if (dr.Read())
                    {
                        nutrient = FillDataRecordNutrient(dr);
                    }
                    dr.Close();
                }
                return nutrient;
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

        private static NSysNutrient FillDataRecordNutrient(IDataReader dataReader)
        {
            NSysNutrient nutrient = new NSysNutrient();
            nutrient.NutrientID = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientID")) ? (int)0 : dataReader.GetInt32(dataReader.GetOrdinal("NutrientID"));
            nutrient.NutrientParam = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientParam"));
            nutrient.NutrientUnit = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientUnit")) ? "" : dataReader.GetString(dataReader.GetOrdinal("NutrientUnit"));
            nutrient.IsNutrientMain = dataReader.IsDBNull(dataReader.GetOrdinal("IsNutrientMain")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsNutrientMain"));
            nutrient.NutrientGroup = dataReader.IsDBNull(dataReader.GetOrdinal("NutrientGroup")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("NutrientGroup"));
            return nutrient;
        }

        #endregion

        #region SysAyurvedic

        public static List<NSysNutrient> GetListAyurvedic()
        {
            List<NSysNutrient> nutrientList = new List<NSysNutrient>();
            NSysNutrient nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * From NSys_Ayurvedic Order By AyurID"))
                {
                    while (dr.Read())
                    {
                        nutrient = FillDataRecordAyurvedic(dr);
                        if (nutrient != null)
                        {
                            nutrientList.Add(nutrient);
                        }
                    }
                    dr.Close();
                }
                return nutrientList;
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

        public static NSysNutrient GetItemAyurvedic(int ayurID)
        {            
            NSysNutrient nutrient = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * From Where AyurID = " + ayurID ))
                {
                    if (dr.Read())
                    {
                        nutrient = FillDataRecordAyurvedic(dr);                        
                    }
                    dr.Close();
                }
                return nutrient;
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

        private static NSysNutrient FillDataRecordAyurvedic(IDataReader dataReader)
        {
            NSysNutrient nutrient = new NSysNutrient();
            nutrient.AyurID = dataReader.IsDBNull(dataReader.GetOrdinal("AyurID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("AyurID"));
            nutrient.AyurParam = dataReader.IsDBNull(dataReader.GetOrdinal("AyurParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurParam"));
            return nutrient;
        }

        #endregion
    }
}
