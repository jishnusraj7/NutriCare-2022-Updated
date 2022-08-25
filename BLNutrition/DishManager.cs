using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Transactions;
using BLNutrition;
using BONutrition;
using DLNutrition;

namespace BLNutrition
{
    public class DishManager
    {
        public static List<Dish> GetList(string searchString)
        {
            return DishDL.GetList(searchString);
        }

        public static List<Dish> GetListFavourite(string searchString)
        {
            List<Dish> dishList = new List<Dish>();
            Dish dishItem = new Dish();
            List<Dish> dishFavList = DishDL.GetListFavourite();
            if (dishFavList != null)
            {
                foreach (Dish dish in dishFavList)
                {
                    dishItem = DishDL.GetItem(dish.DishFavoutiteID, searchString);
                    if (dishItem != null)
                        dishList.Add(dishItem);
                }
            }
            return dishList;
        }
        
        public static Dish GetItem(int DishID,string condition)
        {
            return DishDL.GetItem(DishID, condition);
        }

        public static Dish GetItem(int DishID)
        {
            return DishDL.GetItem(DishID);
        }        

        public static double GetDishCalorie(int DishID)
        {
            return DishDL.GetDishCalorie(DishID);
        }

        public static DataSet GetGramItemSearch(int dishID)
        {
            return DishDL.GetGramItemSearch(dishID);
        }

        public static DataSet GetMilliItemSearch(int dishID)
        {
            return DishDL.GetMilliItemSearch(dishID);
        }

        public static DataSet GetMicroItemSearch(int dishID)
        {
            return DishDL.GetMicroItemSearch(dishID);
        }

        public static List<Dish> GetListSpecialSearch(string SearchString)
        {
            return DishDL.GetListSpecialSearch(SearchString);
        }

        public static List<Dish> GetListAddDish(string SearchString)
        {
            return DishDL.GetListAddDish(SearchString);
        }

        public static Dish GetItemDishNutrients(int dishID)
        {
            return DishDL.GetItemDishNutrients(dishID);
        }

        public static List<Dish> GetDishList(string searchString)
        {
            return DishDL.GetDishList(searchString);
        }

		public static void Save(Dish dish)
		{            
            //using (TransactionScope transactionScope = new TransactionScope())
            {
                DishDL.Save(dish);
                if (dish.DishIngredientList != null)
                {
                    foreach (DishIngredient dishIngredient in dish.DishIngredientList)
                    {
                        DishIngredientDL.Save(dishIngredient);
                    }
                }                
               // transactionScope.Complete();
            }
		}

        public static bool DeleteDish(Dish dish)
        {
            bool IsDelete = false;
            using (TransactionScope transactionScope = new TransactionScope())
            {
                if (DishIngredientDL.DeleteDishIngredients(dish.Id))
                {
                    if (DishDL.DeleteDishFavourite(dish.Id))
                    {
                        if (DishDL.DeleteDish(dish.Id))
                        {
                            IsDelete = true;
                        }

                    }
                }
                transactionScope.Complete();
            }
            return IsDelete;
        }

        public static void UpdateDishPlan(int dishID, float serveCount, float standardWeight)
        {
            DishDL.UpdateDishPlan(dishID, serveCount, standardWeight);
        }

        public static void UpdateDishWeight(int dishID, double totalWeight, Dish dish)
        {
            DishDL.UpdateDishWeight(dishID, totalWeight, dish);
        }

        public static void UpdateDishNutrients(Dish dish)
        {
            DishDL.UpdateDishNutrients(dish);
        }

		public static int GetID()
		{
			return DishDL.GetID();
		}

        public static int GetDishCount(Dish dish)
        {
            return DishDL.GetDishCount(dish);
        }

        public static int GetDishCount(string dishName)
        {
            return DishDL.GetDishCount(dishName);
        }

        public static bool IsDishExists(string dishName)
        {
            return DishDL.IsDishExists(dishName);
        }

        public static string GetDishAuthor(int authorID)
        {
            return DishDL.GetDishAuthor(authorID);
        }
    }
}
