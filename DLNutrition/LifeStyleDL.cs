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
    public class LifeStyleDL
    {

        public static LifeStyle GetItem(int LanguageID, int LifeStyleID)
        {
            LifeStyle lifeStyle = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * from " + Views.V_LifeStyle() + " Where LanguageID = '" + LanguageID + "' AND LifeStyleID = '" + LifeStyleID + "'"))
                {
                    while (dr.Read())
                    {
                        lifeStyle = FillDataRecord(dr);
                    }
                    dr.Close();
                }
                return lifeStyle;
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
        private static LifeStyle FillDataRecord(IDataReader dataReader)
        {
            LifeStyle lifeStyle = new LifeStyle();
            lifeStyle.LifeStyleID = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("LifeStyleID"));
            lifeStyle.LifeStyleName = dataReader.IsDBNull(dataReader.GetOrdinal("LifeStyleName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("LifeStyleName"));
            return lifeStyle;
        }
    }
}
