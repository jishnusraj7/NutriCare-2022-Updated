using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BONutrition;
using NutritionV1.Classes;
using NutritionViews;
using System.IO;

namespace DLNutrition
{
    public class DishDL
    {
        #region Dish
        
        public static List<Dish> GetList(string searchString)
        {
            List<Dish> dishList = new List<Dish>();
            Dish dish = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, "SELECT DishID, DishName,DisplayName, DishRecipe, DishRemarks,DishAyurFeatures, MedicalFavourable, MedicalUnFavourable, Calorie, Protien, FAT, CreatedDate,Iron,Calcium,Phosphorus,DisplayImage,  " +
                                            " DishCategoryID, EthnicID, Keywords, CarboHydrates, Fibre, IsSystemDish, IsAyurvedic, IsCountable, ItemCount, ServeCount,ServeUnit,CookingTime, FrozenLife, RefrigeratedLife, ShelfLife, " +
                                            " VitaminARetinol, VitaminABetaCarotene, Thiamine, Riboflavin, NicotinicAcid, Pyridoxine, FolicAcid, VitaminB12, VitaminC, ServeCount1,ServeCount2,StandardWeight1,StandardWeight2, " +
                                            " FoodHabitID, AyurvedicFavourable, AyurvedicUnFavourable, AuthorID,StandardWeight,DishWeight FROM Dish  " + searchString))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecord(drDish);
                        if (dish != null)
                        {
                            dishList.Add(dish);
                        }
                    }
                    drDish.Close();
                }
                return dishList;
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
        
        public static Dish GetItem(int dishID, string searchString)
        {
            Dish dish = null;
            DBHelper dbManager = null;
            string sqlstr = "";
            try
            {
                dbManager = DBHelper.Instance;
                if (searchString != "")
                {
                    sqlstr = "SELECT DishID, DishName,DisplayName, DishRecipe, DishRemarks,DishAyurFeatures, MedicalFavourable, MedicalUnFavourable, Calorie, Protien, FAT, CreatedDate,Iron,Calcium,Phosphorus,DisplayImage,  " +
                                            " DishCategoryID, EthnicID, Keywords, CarboHydrates, Fibre, IsSystemDish, IsAyurvedic, IsCountable, ItemCount, ServeCount,ServeUnit,CookingTime, FrozenLife, RefrigeratedLife, ShelfLife, " +
                                            " VitaminARetinol, VitaminABetaCarotene, Thiamine, Riboflavin, NicotinicAcid, Pyridoxine, FolicAcid, VitaminB12, VitaminC, ServeCount1,ServeCount2,StandardWeight1,StandardWeight2, " +
                                            " FoodHabitID, AyurvedicFavourable, AyurvedicUnFavourable, AuthorID,StandardWeight,DishWeight FROM Dish  Where " + searchString + " AND DishID = " + dishID;
                }
                else
                {
                    sqlstr = "SELECT DishID, DishName,DisplayName, DishRecipe, DishRemarks,DishAyurFeatures, MedicalFavourable, MedicalUnFavourable, Calorie, Protien, FAT, CreatedDate,Iron,Calcium,Phosphorus,DisplayImage,  " +
                                            " DishCategoryID, EthnicID, Keywords, CarboHydrates, Fibre, IsSystemDish, IsAyurvedic, IsCountable, ItemCount, ServeCount,ServeUnit,CookingTime, FrozenLife, RefrigeratedLife, ShelfLife, " +
                                            " VitaminARetinol, VitaminABetaCarotene, Thiamine, Riboflavin, NicotinicAcid, Pyridoxine, FolicAcid, VitaminB12, VitaminC, ServeCount1,ServeCount2,StandardWeight1,StandardWeight2, " +
                                            " FoodHabitID, AyurvedicFavourable, AyurvedicUnFavourable, AuthorID,StandardWeight,DishWeight FROM Dish Where DishID = " + dishID;
                }
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, sqlstr))
                {
                    if (drDish.Read())
                    {
                        dish = FillDataRecord(drDish);
                    }
                    drDish.Close();
                }
                return dish;
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

        public static Dish GetItem(int dishID)
        {
            Dish dish = null;
            DBHelper dbManager = null;
            string sqlstr = "";
            try
            {
                dbManager = DBHelper.Instance;                
                sqlstr = "SELECT DishID, DishName,DisplayName, DishRecipe, DishRemarks,DishAyurFeatures, MedicalFavourable, MedicalUnFavourable, Calorie, Protien, FAT, CreatedDate,Iron,Calcium,Phosphorus,DisplayImage,  " +
                                            " DishCategoryID, EthnicID, Keywords, CarboHydrates, Fibre, IsSystemDish, IsAyurvedic, IsCountable, ItemCount, ServeCount,ServeUnit,CookingTime, FrozenLife, RefrigeratedLife, ShelfLife, " +
                                            " VitaminARetinol, VitaminABetaCarotene, Thiamine, Riboflavin, NicotinicAcid, Pyridoxine, FolicAcid, VitaminB12, VitaminC, ServeCount1,ServeCount2,StandardWeight1,StandardWeight2, " +
                                            " FoodHabitID, AyurvedicFavourable, AyurvedicUnFavourable, AuthorID,StandardWeight,DishWeight FROM Dish  WHERE DishID = " + dishID;
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, sqlstr))
                {
                    if (drDish.Read())
                    {
                        dish = FillDataRecord(drDish);
                    }
                    drDish.Close();
                }
                return dish;
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

        public static double GetDishCalorie(int DishID)
        {
            double Calorie;
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select (Calorie/100) AS Calorie from Dish Where DishID = " + DishID;
                Calorie = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (double)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;

                return Calorie;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
        }

        public static DataSet GetGramItemSearch(int dishID)
        {
            DataSet dsDish = new DataSet();
            DBHelper dbManager = null;
            string SqlQuery = "SELECT '' AS Moisture, Fibre, Fat, CarboHydrates, Protien FROM Dish Where DishID = " + dishID;
            try
            {
                dbManager = DBHelper.Instance;
                dsDish = dbManager.DataAdapter(CommandType.Text, SqlQuery);
                return dsDish;
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

        public static DataSet GetMilliItemSearch(int dishID)
        {
            DataSet dsDish = new DataSet();
            DBHelper dbManager = null;
            string SqlQuery = "SELECT '' AS VitaminC,'' AS Pyridoxine,NicotinicAcid,Riboflavin,Thiamine,'' AS Zinc,'' AS Pottasium,'' AS Sodium,Phosphorus, Calcium,Iron FROM Dish Where DishID = " + dishID;
            try
            {
                dbManager = DBHelper.Instance;
                dsDish = dbManager.DataAdapter(CommandType.Text, SqlQuery);
                return dsDish;
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

        public static DataSet GetMicroItemSearch(int dishID)
        {
            DataSet dsDish = new DataSet();
            DBHelper dbManager = null;
            string SqlQuery = "SELECT FolicAcid, VitaminABetaCarotene AS BetaCarotene, VitaminARetinol AS Retinol FROM Dish Where DishID = " + dishID;
            try
            {
                dbManager = DBHelper.Instance;
                dsDish = dbManager.DataAdapter(CommandType.Text, SqlQuery);
                return dsDish;
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

        public static List<Dish> GetDishList(string searchString)
        {
            List<Dish> dishList = new List<Dish>();
            Dish dish = null;
            DBHelper dbManager = null;

            string SqlQuery = "SELECT DishID, DishName FROM Dish " + searchString +" Order By DishName";

            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, SqlQuery))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecordList(drDish);

                        if (dish != null)
                        {
                            dishList.Add(dish);
                        }
                    }
                    drDish.Close();
                }
                return dishList;
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

        private static Dish FillDataRecord(IDataReader dataReader)
        {
            Dish dish = new Dish();
            dish.Id = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            dish.EthnicID = dataReader.IsDBNull(dataReader.GetOrdinal("EthnicID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("EthnicID"));
            dish.FoodHabitID = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("FoodHabitID"));
            dish.DishCategoryID = dataReader.IsDBNull(dataReader.GetOrdinal("DishCategoryID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("DishCategoryID"));
            dish.AyurvedicFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("AyurvedicFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurvedicFavourable"));
            dish.AyurvedicUnFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("AyurvedicUnFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurvedicUnFavourable"));
            dish.MedicalFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("MedicalFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MedicalFavourable"));
            dish.MedicalUnFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("MedicalUnFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MedicalUnFavourable"));
            dish.Keywords = dataReader.IsDBNull(dataReader.GetOrdinal("Keywords")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Keywords"));

            dish.Calorie = dataReader.IsDBNull(dataReader.GetOrdinal("Calorie")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calorie"));            
            dish.Protien = dataReader.IsDBNull(dataReader.GetOrdinal("Protien")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Protien"));
            dish.FAT = dataReader.IsDBNull(dataReader.GetOrdinal("FAT")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("FAT"));
            dish.CarboHydrates = dataReader.IsDBNull(dataReader.GetOrdinal("CarboHydrates")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("CarboHydrates"));
            dish.Fibre = dataReader.IsDBNull(dataReader.GetOrdinal("Fibre")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fibre"));            
            dish.Iron = dataReader.IsDBNull(dataReader.GetOrdinal("Iron")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Iron"));
            dish.Calcium = dataReader.IsDBNull(dataReader.GetOrdinal("Calcium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calcium"));
            dish.Phosphorus = dataReader.IsDBNull(dataReader.GetOrdinal("Phosphorus")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Phosphorus"));
            dish.VitaminARetinol = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminARetinol")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminARetinol"));
            dish.VitaminABetaCarotene = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminABetaCarotene")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminABetaCarotene"));
            dish.Thiamine = dataReader.IsDBNull(dataReader.GetOrdinal("Thiamine")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Thiamine"));
            dish.Riboflavin = dataReader.IsDBNull(dataReader.GetOrdinal("Riboflavin")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Riboflavin"));
            dish.NicotinicAcid = dataReader.IsDBNull(dataReader.GetOrdinal("NicotinicAcid")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NicotinicAcid"));
            dish.Pyridoxine = dataReader.IsDBNull(dataReader.GetOrdinal("Pyridoxine")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Pyridoxine"));
            dish.FolicAcid = dataReader.IsDBNull(dataReader.GetOrdinal("FolicAcid")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("FolicAcid"));
            dish.VitaminB12 = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminB12")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminB12"));
            dish.VitaminC = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminC")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminC"));            

            dish.CreatedDate = dataReader.IsDBNull(dataReader.GetOrdinal("CreatedDate")) ? DateTime.Now : dataReader.GetDateTime(dataReader.GetOrdinal("CreatedDate"));
            dish.DisplayImage = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayImage")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayImage"));
            dish.IsAyurvedic = dataReader.IsDBNull(dataReader.GetOrdinal("IsAyurvedic")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsAyurvedic"));
            dish.IsSystemDish = dataReader.IsDBNull(dataReader.GetOrdinal("IsSystemDish")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsSystemDish"));
            dish.IsCountable = dataReader.IsDBNull(dataReader.GetOrdinal("IsCountable")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsCountable"));
            dish.ItemCount = dataReader.IsDBNull(dataReader.GetOrdinal("ItemCount")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ItemCount"));

            dish.ServeCount = dataReader.IsDBNull(dataReader.GetOrdinal("ServeCount")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ServeCount"));
            dish.StandardWeight = dataReader.IsDBNull(dataReader.GetOrdinal("StandardWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("StandardWeight"));
            dish.ServeCount1 = dataReader.IsDBNull(dataReader.GetOrdinal("ServeCount1")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ServeCount1"));
            dish.StandardWeight1 = dataReader.IsDBNull(dataReader.GetOrdinal("StandardWeight1")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("StandardWeight1"));
            dish.ServeCount2 = dataReader.IsDBNull(dataReader.GetOrdinal("ServeCount2")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("ServeCount2"));
            dish.StandardWeight2 = dataReader.IsDBNull(dataReader.GetOrdinal("StandardWeight2")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("StandardWeight2"));
            dish.DishWeight = dataReader.IsDBNull(dataReader.GetOrdinal("DishWeight")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("DishWeight"));

            dish.CookingTime = dataReader.IsDBNull(dataReader.GetOrdinal("CookingTime")) ? "" : dataReader.GetString(dataReader.GetOrdinal("CookingTime"));
            dish.FrozenLife = dataReader.IsDBNull(dataReader.GetOrdinal("FrozenLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("FrozenLife"));
            dish.RefrigeratedLife = dataReader.IsDBNull(dataReader.GetOrdinal("RefrigeratedLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("RefrigeratedLife"));
            dish.ShelfLife = dataReader.IsDBNull(dataReader.GetOrdinal("ShelfLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ShelfLife"));
            dish.Name = dataReader.IsDBNull(dataReader.GetOrdinal("DishName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DishName"));
            dish.DisplayName = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayName"));

            if (dish.Name.Length > 13)
                dish.IconName = dish.Name.Substring(0, 13) + "..";
            else
                dish.IconName = dish.Name;

            dish.DishRecipe = dataReader.IsDBNull(dataReader.GetOrdinal("DishRecipe")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DishRecipe"));
            dish.Comments = dataReader.IsDBNull(dataReader.GetOrdinal("DishRemarks")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DishRemarks"));
            dish.DishAyurFeatures = dataReader.IsDBNull(dataReader.GetOrdinal("DishAyurFeatures")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DishAyurFeatures"));
            dish.ServeUnit = dataReader.IsDBNull(dataReader.GetOrdinal("ServeUnit")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ServeUnit"));
            dish.AuthorID = dataReader.IsDBNull(dataReader.GetOrdinal("AuthorID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("AuthorID"));

            if (dish.DisplayImage != string.Empty)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Dishes" + "\\" + dish.Id + ".jpg"))
                {
                    dish.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Dishes" + "\\" + dish.Id + ".jpg";
                }
                else
                {
                    dish.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg";
                }
                //dish.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "Pictures\\Dishes" + "\\" + Functions.EncryptString(Convert.ToString(dish.Id)) + ".jpg";
            }
            else
            {
                dish.DisplayImage = AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg";
            }
            return dish;
        }

        private static Dish FillDataRecordLAN(IDataReader dataReader)
        {
            Dish dish = new Dish();
            dish.Id = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            dish.EthnicID = dataReader.IsDBNull(dataReader.GetOrdinal("EthnicID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("EthnicID"));
            dish.FoodHabitID = dataReader.IsDBNull(dataReader.GetOrdinal("FoodHabitID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("FoodHabitID"));
            dish.DishCategoryID = dataReader.IsDBNull(dataReader.GetOrdinal("DishCategoryID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("DishCategoryID"));
            dish.AyurvedicFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("AyurvedicFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurvedicFavourable"));
            dish.AyurvedicUnFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("AyurvedicUnFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("AyurvedicUnFavourable"));
            dish.MedicalFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("MedicalFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MedicalFavourable"));
            dish.MedicalUnFavourable = dataReader.IsDBNull(dataReader.GetOrdinal("MedicalUnFavourable")) ? "" : dataReader.GetString(dataReader.GetOrdinal("MedicalUnFavourable"));
            dish.Keywords = dataReader.IsDBNull(dataReader.GetOrdinal("Keywords")) ? "" : dataReader.GetString(dataReader.GetOrdinal("Keywords"));
            dish.Calorie = dataReader.IsDBNull(dataReader.GetOrdinal("Calorie")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calorie"));
            dish.CarboHydrates = dataReader.IsDBNull(dataReader.GetOrdinal("CarboHydrates")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("CarboHydrates"));
            dish.Protien = dataReader.IsDBNull(dataReader.GetOrdinal("Protien")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Protien"));
            dish.FAT = dataReader.IsDBNull(dataReader.GetOrdinal("FAT")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("FAT"));
            dish.Fibre = dataReader.IsDBNull(dataReader.GetOrdinal("Fibre")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fibre"));
            dish.Iron = dataReader.IsDBNull(dataReader.GetOrdinal("Iron")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Iron"));
            dish.Calcium = dataReader.IsDBNull(dataReader.GetOrdinal("Calcium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calcium"));
            dish.Phosphorus = dataReader.IsDBNull(dataReader.GetOrdinal("Phosphorus")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Phosphorus"));
            dish.VitaminARetinol = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminARetinol")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminARetinol"));
            dish.VitaminABetaCarotene = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminABetaCarotene")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminABetaCarotene"));
            dish.Thiamine = dataReader.IsDBNull(dataReader.GetOrdinal("Thiamine")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Thiamine"));
            dish.Riboflavin = dataReader.IsDBNull(dataReader.GetOrdinal("Riboflavin")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Riboflavin"));
            dish.NicotinicAcid = dataReader.IsDBNull(dataReader.GetOrdinal("NicotinicAcid")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NicotinicAcid"));
            dish.Pyridoxine = dataReader.IsDBNull(dataReader.GetOrdinal("Pyridoxine")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Pyridoxine"));
            dish.FolicAcid = dataReader.IsDBNull(dataReader.GetOrdinal("FolicAcid")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("FolicAcid"));
            dish.VitaminB12 = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminB12")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminB12"));
            dish.VitaminC = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminC")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminC"));
            dish.CreatedDate = dataReader.IsDBNull(dataReader.GetOrdinal("CreatedDate")) ? DateTime.Now : dataReader.GetDateTime(dataReader.GetOrdinal("CreatedDate"));
            dish.DisplayImage = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayImage")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayImage"));
            dish.IsAyurvedic = dataReader.IsDBNull(dataReader.GetOrdinal("IsAyurvedic")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsAyurvedic"));
            dish.IsSystemDish = dataReader.IsDBNull(dataReader.GetOrdinal("IsSystemDish")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsSystemDish"));
            dish.IsCountable = dataReader.IsDBNull(dataReader.GetOrdinal("IsCountable")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsCountable"));
            dish.ItemCount = dataReader.IsDBNull(dataReader.GetOrdinal("ItemCount")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ItemCount"));
            dish.ServeCount = dataReader.IsDBNull(dataReader.GetOrdinal("ServeCount")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ServeCount"));
            dish.CookingTime = dataReader.IsDBNull(dataReader.GetOrdinal("CookingTime")) ? "" : dataReader.GetString(dataReader.GetOrdinal("CookingTime"));
            dish.FrozenLife = dataReader.IsDBNull(dataReader.GetOrdinal("FrozenLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("FrozenLife"));
            dish.RefrigeratedLife = dataReader.IsDBNull(dataReader.GetOrdinal("RefrigeratedLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("RefrigeratedLife"));
            dish.ShelfLife = dataReader.IsDBNull(dataReader.GetOrdinal("ShelfLife")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("ShelfLife"));
            dish.RegionalName = dataReader.IsDBNull(dataReader.GetOrdinal("DishName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DishName"));
            dish.ServeUnit = dataReader.IsDBNull(dataReader.GetOrdinal("ServeUnit")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("ServeUnit"));
            dish.AuthorID = dataReader.IsDBNull(dataReader.GetOrdinal("AuthorID")) ? (Byte)0 : dataReader.GetByte(dataReader.GetOrdinal("AuthorID"));
            return dish;
        }
        
        private static Dish FillDataRecordList(IDataReader dataReader)
        {
            Dish dish = new Dish();
            dish.Id = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            dish.Name = dataReader.IsDBNull(dataReader.GetOrdinal("DishName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DishName"));
            return dish;
        }

        public static void Save(Dish dish)
        {
            string SqlQry = "";
            string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                sqlCondition = "Select count(*) from Dish WHERE DishID = " + dish.Id;
                if (GetCount(sqlCondition) > 0)
                {
                    SqlQry = "UPDATE Dish SET EthnicID = " + dish.EthnicID + ",FoodHabitID = " + dish.FoodHabitID + ",DishCategoryID = " + dish.DishCategoryID + ", DishName= '" + Functions.ProperCase(dish.Name) + "',DisplayName = '" + Functions.ProperCase(dish.DisplayName) + "',DishRecipe = '" + Functions.ReplaceChar(dish.DishRecipe) + "',DishRemarks= '" + Functions.ReplaceChar(dish.Comments) + "', "+
                                " DishAyurFeatures= '" + dish.DishAyurFeatures + "', AyurvedicFavourable = '" + dish.AyurvedicFavourable + "',AyurvedicUnFavourable = '" + dish.AyurvedicUnFavourable + "',MedicalFavourable = '" + dish.MedicalFavourable + "',MedicalUnFavourable = '" + dish.MedicalUnFavourable + "', " +
                                " Keywords = '" + dish.Keywords + "',Calorie = " + dish.Calorie + ",Protien = " + dish.Protien + ",CarboHydrates = " + dish.CarboHydrates + ",FAT = " + dish.FAT + ",Fibre = " + dish.Fibre + ",CreatedDate = '"+ dish.CreatedDate.ToString("yyyy/MMM/dd") +"',DisplayImage = '" + dish.DisplayImage + "', " +
                                " IsSystemDish = " + dish.IsSystemDish + ",IsAyurvedic = " + dish.IsAyurvedic + ",IsCountable = " + dish.IsCountable + ",ItemCount = " + dish.ItemCount + ",ServeCount = " + dish.ServeCount + ",CookingTime = " + dish.CookingTime + ",FrozenLife = " + dish.FrozenLife + ", " +
                                " RefrigeratedLife = " + dish.RefrigeratedLife + ",ShelfLife = " + dish.ShelfLife + ",StandardWeight = " + dish.StandardWeight + ",ServeUnit = " + dish.ServeUnit + ",ServeCount1 = " + dish.ServeCount1 + ",ServeCount2 = " + dish.ServeCount2 + ",StandardWeight1 = " + dish.StandardWeight1 + ",StandardWeight2 = " + dish.StandardWeight2 + "," +
                                " Iron = " + dish.Iron + ",Calcium = " + dish.Calcium + ",Phosphorus = " + dish.Phosphorus + ",VitaminARetinol = " + dish.VitaminARetinol + ",VitaminABetaCarotene = " + dish.VitaminABetaCarotene + "," +
                                " Thiamine = " + dish.Thiamine + ",Riboflavin = " + dish.Riboflavin + ",NicotinicAcid = " + dish.NicotinicAcid + ",Pyridoxine = " + dish.Pyridoxine + ",FolicAcid = " + dish.FolicAcid + ",VitaminB12 = " + dish.VitaminB12 + ",VitaminC = " + dish.VitaminC + ",DishWeight = " + dish.DishWeight + "" +
                                " WHERE DishID = " + dish.Id;
                }
                else
                {
                    SqlQry = "INSERT INTO Dish(DishID,EthnicID,FoodHabitID,DishCategoryID,DishName,DisplayName,DishRecipe,DishRemarks,DishAyurFeatures,AyurvedicFavourable,AyurvedicUnFavourable,MedicalFavourable,MedicalUnFavourable,Keywords,Calorie,Protien,CarboHydrates,FAT,Fibre,Iron,Calcium,Phosphorus,VitaminARetinol,VitaminABetaCarotene,Thiamine,Riboflavin,NicotinicAcid,Pyridoxine,FolicAcid,VitaminB12,VitaminC,CreatedDate,DisplayImage,IsSystemDish,IsAyurvedic,IsCountable,ItemCount,ServeCount,CookingTime,FrozenLife,RefrigeratedLife,ShelfLife,StandardWeight,ServeUnit,AuthorID,ServeCount1,ServeCount2,StandardWeight1,StandardWeight2,DishWeight)" +
                             "VALUES(" + dish.Id + "," + dish.EthnicID + "," + dish.FoodHabitID + "," + dish.DishCategoryID + ",'" + Functions.ProperCase(dish.Name) + "','" + Functions.ProperCase(dish.DisplayName) + "','" + Functions.ReplaceChar(dish.DishRecipe) + "', '" + Functions.ReplaceChar(dish.Comments) + "','" + dish.DishAyurFeatures + "','" + dish.AyurvedicFavourable + "','" + dish.AyurvedicUnFavourable + "','" + dish.MedicalFavourable + "','" + dish.MedicalUnFavourable + "','" + dish.Keywords + "'," + dish.Calorie + "," + dish.Protien + "," + dish.CarboHydrates + "," + dish.FAT + ", " +
                             "" + dish.Fibre + "," + dish.Iron + "," + dish.Calcium + "," + dish.Phosphorus + ", " + dish.VitaminARetinol + ", " + dish.VitaminABetaCarotene + "," + dish.Thiamine + ", " + dish.Riboflavin + ", " + dish.NicotinicAcid + ", " + dish.Pyridoxine + ", " + dish.FolicAcid + ", " + dish.VitaminB12 + "," + dish.VitaminC + ",'"+ dish.CreatedDate.ToString("yyyy/MM/dd") +"','" + dish.DisplayImage + "'," + dish.IsSystemDish + "," + dish.IsAyurvedic + "," + dish.IsCountable + "," + dish.ItemCount + "," + dish.ServeCount + "," + dish.CookingTime + "," + dish.FrozenLife + "," + dish.RefrigeratedLife + "," + dish.ShelfLife + "," + dish.StandardWeight + "," + dish.ServeUnit + "," + dish.AuthorID + "," + dish.ServeCount1 + "," + dish.ServeCount2 + "," + dish.StandardWeight1 + "," + dish.StandardWeight2 + ","+ dish.DishWeight +")";
                }
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
            }
            catch (Exception ex)
            {
                throw ex; 
            }
            finally
            {
                dbHelper = null;
            }
        }				

        public static bool DeleteDish(int dishID)
        {
            string SqlQry = "";
            //string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                //sqlCondition = "Select count(*) from Dish  WHERE DishID = " + dishID;
                //if (GetCount(sqlCondition) > 0)
                //{
                SqlQry = "Delete from Dish  WHERE DishID = " + dishID;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
                return true;
                //}
                //else
                //{
                //    return false;
                //}
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dbHelper = null;
            }
        }       

        public static void UpdateDishPlan(int dishID,float serveCount,float standardWeight)
        {
            string SqlQry = "";
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "UPDATE Dish SET ServeCount2 = " + serveCount + ",StandardWeight2 = " + standardWeight + " WHERE DishID = " + dishID;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                dbHelper = null;
            }
        }

        public static void UpdateDishWeight(int dishID, double totalWeight,Dish dish)
        {
            string SqlQry = "";
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "UPDATE Dish SET DishWeight = " + totalWeight + ",ServeCount = " + dish.ServeCount + ",ServeCount1 = " + dish.ServeCount1 + ",ServeCount2 = " + dish.ServeCount2 + " WHERE DishID = " + dishID;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                dbHelper = null;
            }
        }

        public static void UpdateDishNutrients(Dish dish)
        {
            string SqlQry = "";
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "UPDATE Dish SET Calorie=" + dish.Calorie + ",Protien=" + dish.Protien + ",CarboHydrates=" + dish.CarboHydrates + ",FAT=" + dish.FAT + ",Fibre=" + dish.Fibre + ",Iron=" + dish.Iron + ",Calcium=" + dish.Calcium + ",Phosphorus=" + dish.Phosphorus + ",VitaminARetinol=" + dish.VitaminARetinol + ",VitaminABetaCarotene=" + dish.VitaminABetaCarotene + ",Thiamine=" + dish.Thiamine + ",Riboflavin=" + dish.Riboflavin + ",NicotinicAcid=" + dish.NicotinicAcid + ",Pyridoxine=" + dish.Pyridoxine + ",FolicAcid=" + dish.FolicAcid + ",VitaminB12=" + dish.VitaminB12 + ",VitaminC=" + dish.VitaminC + " WHERE DishID = " + dish.Id;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                dbHelper = null;
            }
        }

        #endregion		

		#region Dish Favourite

		public static List<Dish> GetListFavourite()
        {
            List<Dish> dishList = new List<Dish>();
            Dish dish = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, "Select * From DishFavourite Order By FavouriteID"))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecordFavourite(drDish);
                        if (dish != null)
                        {
                            dishList.Add(dish);
                        }
                    }
                    drDish.Close();
                }
                return dishList;
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

        private static Dish FillDataRecordFavourite(IDataReader dataReader)
        {
            Dish dish = new Dish();
            dish.FavouriteID = dataReader.IsDBNull(dataReader.GetOrdinal("FavouriteID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("FavouriteID"));
            dish.DishFavoutiteID = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            dish.FamilyID = dataReader.IsDBNull(dataReader.GetOrdinal("FamilyID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("FamilyID"));
            return dish;
        }

        public static void SaveDishFavourite(int familyID, Dish dish)
        {
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Insert into DishFavourite (FamilyID,DishID) Values (" + familyID + "," + dish.Id + ")";
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
        }        

        public static bool DeleteDishFavourite(int dishID)
        {
            string SqlQry = "";
            //string sqlCondition;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                //sqlCondition = "Select count(*) from DishFavourite  WHERE DishID = " + dishID;
                //if (GetCount(sqlCondition) > 0)
                //{
                SqlQry = "Delete from DishFavourite  WHERE DishID = " + dishID;
                dbHelper.ExecuteNonQuery(CommandType.Text, SqlQry);
                //}
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                dbHelper = null;
            }
        }        

        #endregion

        #region Dish Search

        public static List<Dish> GetListSpecialSearch(string SearchString)
        {
            List<Dish> dishList = new List<Dish>();
            Dish dish = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, "SELECT Top(100) DishID,DishName + '  (' + CSTR(StandardWeight) + ' gm)' AS DishName,DisplayName + '  (' + CSTR(StandardWeight) + ' gm)' AS DisplayName,(Calorie/100)* StandardWeight AS Calorie,(CarboHydrates/100) * StandardWeight AS CarboHydrates,(Protien/100) * StandardWeight AS Protien,(FAT/100) * StandardWeight AS FAT,(Fibre/100) * StandardWeight AS Fibre,(Iron/100) * StandardWeight AS Iron,(Calcium/100) * StandardWeight AS Calcium,(Phosphorus/100) * StandardWeight AS Phosphorus,(VitaminARetinol/100) * StandardWeight AS VitaminARetinol,(VitaminABetaCarotene/100) * StandardWeight AS VitaminABetaCarotene,(Thiamine/100) * StandardWeight AS Thiamine,(Riboflavin/100) * StandardWeight AS Riboflavin,(NicotinicAcid/100) * StandardWeight AS NicotinicAcid,(Pyridoxine/100) * StandardWeight AS Pyridoxine,(FolicAcid/100) * StandardWeight AS FolicAcid,(VitaminB12/100) * StandardWeight AS VitaminB12,(VitaminC/100) * StandardWeight AS VitaminC,DisplayImage FROM Dish " + SearchString))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecord_SpecialSearch(drDish);
                        if (dish != null)
                        {
                            dishList.Add(dish);
                        }
                    }
                    drDish.Close();
                }
                return dishList;
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

        public static List<Dish> GetListAddDish(string SearchString)
        {
            List<Dish> dishList = new List<Dish>();
            Dish dish = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, "Select DishID,DishName + '  (' + CSTR(StandardWeight) + ' gm)' AS DishName,DisplayName + '  (' + CSTR(StandardWeight) + ' gm)' AS DisplayName,(Calorie/100)*StandardWeight AS Calorie,(CarboHydrates/100)*StandardWeight AS CarboHydrates,(Protien/100)*StandardWeight AS Protien,(FAT/100)*StandardWeight AS FAT,(Fibre/100)*StandardWeight AS Fibre,(Iron/100)*StandardWeight AS Iron,(Calcium/100)*StandardWeight AS Calcium,(Phosphorus/100)*StandardWeight AS Phosphorus,(VitaminARetinol/100)*StandardWeight AS VitaminARetinol,(VitaminABetaCarotene/100)*StandardWeight AS VitaminABetaCarotene,(Thiamine/100)*StandardWeight AS Thiamine,(Riboflavin/100)*StandardWeight AS Riboflavin,(NicotinicAcid/100)*StandardWeight AS NicotinicAcid,(Pyridoxine/100)*StandardWeight AS Pyridoxine,(FolicAcid/100)*StandardWeight AS FolicAcid,(VitaminB12/100)*StandardWeight AS VitaminB12,(VitaminC/100)*StandardWeight AS VitaminC,DisplayImage From Dish " + SearchString))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecord_SpecialSearch(drDish);
                        if (dish != null)
                        {
                            dishList.Add(dish);
                        }
                    }
                    drDish.Close();
                }
                return dishList;
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

        public static Dish GetItemDishNutrients(int dishID)
        {            
            Dish dish = null;
            DBHelper dbManager = null;
            try
            {
                dbManager = DBHelper.Instance;
                using (IDataReader drDish = dbManager.ExecuteReader(CommandType.Text, "Select DishID,DishName,DisplayName,(Calorie/100) AS Calorie,(CarboHydrates/100) AS CarboHydrates,(Protien/100) AS Protien,(FAT/100) AS FAT,(Fibre/100) AS Fibre,(Iron/100) AS Iron,(Calcium/100) AS Calcium,(Phosphorus/100) AS Phosphorus,(VitaminARetinol/100) AS VitaminARetinol,(VitaminABetaCarotene/100) AS VitaminABetaCarotene,(Thiamine/100) AS Thiamine,(Riboflavin/100) AS Riboflavin,(NicotinicAcid/100) AS NicotinicAcid,(Pyridoxine/100) AS Pyridoxine,(FolicAcid/100) AS FolicAcid,(VitaminB12/100) AS VitaminB12,(VitaminC/100) AS VitaminC,DisplayImage  From Dish WHERE DishID = " + dishID))
                {
                    while (drDish.Read())
                    {
                        dish = FillDataRecord_SpecialSearch(drDish);
                    }
                    drDish.Close();
                }
                return dish;
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

        public static string GetDishRegionalName(int DishID)
        {
            string RegionalName;
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select DishName from Dish Where DishID = " + DishID;
                RegionalName = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (string)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : "";
             
                return RegionalName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
        }

        private static Dish FillDataRecord_SpecialSearch(IDataReader dataReader)
        {
            Dish dish = new Dish();
            dish.Id = dataReader.IsDBNull(dataReader.GetOrdinal("DishID")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("DishID"));
            dish.Name = dataReader.IsDBNull(dataReader.GetOrdinal("DishName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DishName"));
            dish.DisplayName = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayName"));
            dish.Calorie = dataReader.IsDBNull(dataReader.GetOrdinal("Calorie")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calorie"));
            dish.CarboHydrates = dataReader.IsDBNull(dataReader.GetOrdinal("CarboHydrates")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("CarboHydrates"));
            dish.Protien = dataReader.IsDBNull(dataReader.GetOrdinal("Protien")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Protien"));
            dish.FAT = dataReader.IsDBNull(dataReader.GetOrdinal("FAT")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("FAT"));
            dish.Fibre = dataReader.IsDBNull(dataReader.GetOrdinal("Fibre")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Fibre"));
            dish.Iron = dataReader.IsDBNull(dataReader.GetOrdinal("Iron")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Iron"));
            dish.Calcium = dataReader.IsDBNull(dataReader.GetOrdinal("Calcium")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Calcium"));
            dish.Phosphorus = dataReader.IsDBNull(dataReader.GetOrdinal("Phosphorus")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Phosphorus"));
            dish.VitaminARetinol = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminARetinol")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminARetinol"));
            dish.VitaminABetaCarotene = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminABetaCarotene")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminABetaCarotene"));
            dish.Thiamine = dataReader.IsDBNull(dataReader.GetOrdinal("Thiamine")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Thiamine"));
            dish.Riboflavin = dataReader.IsDBNull(dataReader.GetOrdinal("Riboflavin")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Riboflavin"));
            dish.NicotinicAcid = dataReader.IsDBNull(dataReader.GetOrdinal("NicotinicAcid")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("NicotinicAcid"));
            dish.Pyridoxine = dataReader.IsDBNull(dataReader.GetOrdinal("Pyridoxine")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("Pyridoxine"));
            dish.FolicAcid = dataReader.IsDBNull(dataReader.GetOrdinal("FolicAcid")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("FolicAcid"));
            dish.VitaminB12 = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminB12")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminB12"));
            dish.VitaminC = dataReader.IsDBNull(dataReader.GetOrdinal("VitaminC")) ? (float)0 : (float)dataReader.GetDouble(dataReader.GetOrdinal("VitaminC"));
            dish.DisplayImage = dataReader.IsDBNull(dataReader.GetOrdinal("DisplayImage")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DisplayImage"));
            return dish;
        }

        private static Dish FillDataRecord_RegionalName(IDataReader dataReader)
        {
            Dish dish = new Dish();
            dish.RegionalName = dataReader.IsDBNull(dataReader.GetOrdinal("DishName")) ? "" : dataReader.GetString(dataReader.GetOrdinal("DishName"));
            return dish;
        }

        #endregion

		#region Functions

		public static int GetID()
		{
			int DishID = 0;
			string SqlQry;
			DBHelper dbHelper = null;
			try
			{
				dbHelper = DBHelper.Instance;
                SqlQry = "Select IIF(ISNULL(Max(DishID)),0,Max(DishID)) + 1 From Dish";
				DishID = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : 0;
				return DishID;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				dbHelper = null;
			}
		}

		private static int GetCount(string sqlQuery)
		{
			int Count = 0;
			DBHelper dbHelper = null;

			try
			{
				dbHelper = DBHelper.Instance;
				Count = dbHelper.ExecuteScalar(CommandType.Text, sqlQuery) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, sqlQuery) : 0;
				return Count;
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				dbHelper = null;
			}
		}

        public static int GetDishCount(string dishName)
        {
            int Count = 0;
            string sqlQuery = "";
            DBHelper dbHelper = null;

            try
            {
                sqlQuery = "Select Count(*) from Dish Where DishName LIKE '" + dishName + "%'";
                dbHelper = DBHelper.Instance;
                Count = dbHelper.ExecuteScalar(CommandType.Text, sqlQuery) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, sqlQuery) : 0;
                return Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
        }

        public static bool IsDishExists(string dishName)
        {
            int Count = 0;
            string sqlQuery = "";
            DBHelper dbHelper = null;
            try
            {
                sqlQuery = "Select Count(*) from Dish Where DishName = '" + dishName + "'";
                dbHelper = DBHelper.Instance;
                Count = dbHelper.ExecuteScalar(CommandType.Text, sqlQuery) != System.DBNull.Value ? (int)dbHelper.ExecuteScalar(CommandType.Text, sqlQuery) : 0;
                if (Count > 0)
                    return true;
                else
                    return false;
            }
            catch 
            {
                return false;
            }
            finally
            {
                dbHelper = null;
            }
        }

        public static int GetDishCount(Dish dish)
        {
            int dishCount;
            dishCount = GetCount("Select Count(*) from FamilyMemberMealPlan Where DishID = " + dish.Id);
            if (dishCount <= 0)
            {
                dishCount = GetCount("Select Count(*) from FamilyMemberMealDairy Where DishID = " + dish.Id);
            }
            return dishCount;
        }

		#endregion

        #region Recipe Author

        public static string GetDishAuthor(int authorID)
        {
            string AuthorName;
            string SqlQry;
            DBHelper dbHelper = null;
            try
            {
                dbHelper = DBHelper.Instance;
                SqlQry = "Select AuthorName from NSysAuthor Where AuthorID = " + authorID;
                AuthorName = dbHelper.ExecuteScalar(CommandType.Text, SqlQry) != System.DBNull.Value ? (string)dbHelper.ExecuteScalar(CommandType.Text, SqlQry) : "";
                return AuthorName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbHelper = null;
            }
        }

        #endregion
    }
}
