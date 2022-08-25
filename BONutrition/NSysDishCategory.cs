using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class NSysDishCategory
    {
        #region VARIABLES

        private int dishCategoryID;        
        private string dishCategoryName;

        #endregion

        #region PROPERTIES

        public int DishCategoryID
        {
            get { return this.dishCategoryID; }
            set { this.dishCategoryID = value; }
        }
       
        public string DishCategoryName
        {
            get { return this.dishCategoryName; }
            set { this.dishCategoryName = value; }
        }

        #endregion
    }
}
