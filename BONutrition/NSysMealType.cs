using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
   public class NSysMealType
    {
        #region properties
        private int _MealTypeID;
        private string _MealTypeName;
        private string _MealTypeDescription;


        /// <summary>
        /// Get set property for MealTypeID
        /// </summary>
        public int MealTypeID
        {
            get { return _MealTypeID; }
            set { _MealTypeID = value; }
        }

        /// <summary>
        /// Get set property for MealTypeName
        /// </summary>
        public string MealTypeName
        {
            get { return _MealTypeName; }
            set { _MealTypeName = value.Trim(); }
        }

        /// <summary>
        /// Get set property for MealTypeDescription
        /// </summary>
        public string MealTypeDescription
        {
            get { return _MealTypeDescription; }
            set { _MealTypeDescription = value.Trim(); }
        }

        #endregion

    }
}
