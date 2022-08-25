using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class NSysFoodCategory
    {
        private int foodCategoryID;        
        private string foodCategoryName;

        public int FoodCategoryID
        {
            get { return this.foodCategoryID; }
            set { this.foodCategoryID = value; }
        }        
        
        public string FoodCategoryName
        {
            get { return this.foodCategoryName; }
            set { this.foodCategoryName = value; }
        }

    }
}
