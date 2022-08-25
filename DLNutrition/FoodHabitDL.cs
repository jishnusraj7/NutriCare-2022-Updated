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
    public class FoodHabitDL
    {

        public static FoodHabit GetItem(int LanguageID, int FoodHabitID)
        {
            FoodHabit foodHabit = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader dr = dbManager.ExecuteReader(CommandType.Text, "Select * from " + Views.V_FoodHabit() + " Where LanguageID = '" + LanguageID + "' AND FoodHabitID = '" + FoodHabitID + "'"))
                {
                    while (dr.Read())
                    {
                        foodHabit = FillDataRecord(dr);
                    }
                    dr.Close();
                }
                return foodHabit;
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


        private static FoodHabit FillDataRecord(IDataReader dataReader)
        {
            FoodHabit foodHabit = new FoodHabit();
            foodHabit.FoodHabitID = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitID")) ? (byte)0 : dataReader.GetByte(dataReader.GetOrdinal("FoodHabitID"));
            foodHabit.FoodHabitName = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("FoodHabitName"));
            return foodHabit;
        }
    }
}
