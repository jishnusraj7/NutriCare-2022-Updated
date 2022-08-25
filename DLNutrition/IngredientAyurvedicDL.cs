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
    public class IngredientAyurvedicDL
    {
        #region Ingredient Ayurvedic        

        public static List<IngredientAyurvedic> GetListAyurvedic(int ingredientID)
        {
            List<IngredientAyurvedic> ingredientAyurList = new List<IngredientAyurvedic>();
            IngredientAyurvedic ingredientAyur = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientAyur = dbManager.ExecuteReader(CommandType.Text, "SELECT IngredientID, AyurValue, AyurID, IsVata, IsPita, IsKapa, AyurParam FROM IngredientAyurvedic Where IngredientID = '" + ingredientID + "'"))
                {
                    while (dringredientAyur.Read())
                    {
                        ingredientAyur = FillDataRecordAyurValues(dringredientAyur);
                        if (ingredientAyur != null)
                        {
                            ingredientAyurList.Add(ingredientAyur);
                        }
                    }
                    dringredientAyur.Close();
                }
                return ingredientAyurList;
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

        public static List<IngredientAyurvedic> GetListAyurvedicDish(int ingredientID)
        {
            List<IngredientAyurvedic> ingredientAyurList = new List<IngredientAyurvedic>();
            IngredientAyurvedic ingredientAyur = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dringredientAyur = dbManager.ExecuteReader(CommandType.Text, "SELECT IngredientID, AyurValue, AyurID, IsVata, IsPita, IsKapa, AyurParam FROM IngredientAyurvedic Where IngredientID = '" + ingredientID + "'"))
                {
                    while (dringredientAyur.Read())
                    {
                        ingredientAyur = FillDataRecordAyurValuesDish(dringredientAyur);
                        if (ingredientAyur != null)
                        {
                            ingredientAyurList.Add(ingredientAyur);
                        }
                    }
                    dringredientAyur.Close();
                }
                return ingredientAyurList;
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

        private static IngredientAyurvedic FillDataRecordAyurValues(IDataReader dataReader)
        {
            IngredientAyurvedic ingredientAyur = new IngredientAyurvedic();
            ingredientAyur.IngredientId = dataReader.IsDBNull(dataReader.GetOrdinal("IngredientId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("IngredientId"));
            ingredientAyur.AyurParam = dataReader.IsDBNull(dataReader.GetOrdinal("AyurParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurParam"));
            ingredientAyur.AyurValue = dataReader.IsDBNull(dataReader.GetOrdinal("AyurValue")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurValue"));
            ingredientAyur.AyurID = dataReader.IsDBNull(dataReader.GetOrdinal("AyurID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("AyurID"));
            ingredientAyur.IsVata = dataReader.IsDBNull(dataReader.GetOrdinal("IsVata")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsVata"));
            ingredientAyur.IsPita = dataReader.IsDBNull(dataReader.GetOrdinal("IsPita")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsPita"));
            ingredientAyur.IsKapa = dataReader.IsDBNull(dataReader.GetOrdinal("IsKapa")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsKapa"));
            return ingredientAyur;
        }

        private static IngredientAyurvedic FillDataRecordAyurValuesDish(IDataReader dataReader)
        {
            IngredientAyurvedic ingredientAyur = new IngredientAyurvedic();
            ingredientAyur.AyurParam = dataReader.IsDBNull(dataReader.GetOrdinal("AyurParam")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurParam"));
            ingredientAyur.AyurID = dataReader.IsDBNull(dataReader.GetOrdinal("AyurID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("AyurID"));
            ingredientAyur.AyurValue = dataReader.IsDBNull(dataReader.GetOrdinal("AyurValue")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurValue"));
            ingredientAyur.IsVata = dataReader.IsDBNull(dataReader.GetOrdinal("IsVata")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsVata"));
            ingredientAyur.IsPita = dataReader.IsDBNull(dataReader.GetOrdinal("IsPita")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsPita"));
            ingredientAyur.IsKapa = dataReader.IsDBNull(dataReader.GetOrdinal("IsKapa")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsKapa"));
            return ingredientAyur;
        }       
       
        #endregion
    }
}
