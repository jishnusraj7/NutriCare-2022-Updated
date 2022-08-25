using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
   public class MemberMenuPlanner
    {
        private int _MemberMealPlanID;
        private int _MemberID;
        private DateTime _WeekDay;
        private int _Week;
        private int _MealTypeID;
        private int _DishID;
        private float _DishCount;
        private string _Dishname;
        private float _DishCalorie;
        private float _PlanWeight;
        private string _PlanStatus;

        #region Member Food Setting

        /// <summary>
        /// Get set property for MemberMealPlanID
        /// </summary>
        public int MemberMealPlanID
        {
            get { return _MemberMealPlanID; }
            set { _MemberMealPlanID = value; }
        }

        /// <summary>
        /// Get set property for MemberID
        /// </summary>
        public int MemberID
        {
            get { return _MemberID; }
            set { _MemberID = value; }
        }

        /// <summary>
        /// Get set property for WeekDay
        /// </summary>
        public DateTime WeekDay
        {
            get { return _WeekDay; }
            set { _WeekDay = value; }
        }

        /// <summary>
        /// Get set property for MealTypeID
        /// </summary>
        public int MealTypeID
        {
            get { return _MealTypeID; }
            set { _MealTypeID = value; }
        }

        /// <summary>
        /// Get set property for DishID
        /// </summary>
        public int DishID
        {
            get { return _DishID; }
            set { _DishID = value; }
        }

        /// <summary>
        /// Get set property for DishCount
        /// </summary>
        public float DishCount
        {
            get { return _DishCount; }
            set { _DishCount = value; }
        }

        public string DishName
        {
            get { return _Dishname; }
            set { _Dishname = value; }
        }

        public int Week
        {
            get { return _Week; }
            set { _Week = value; }
        }

        public float DishCalorie
        {
            get { return _DishCalorie; }
            set { _DishCalorie = value; }
        }

        public float PlanWeight
        {
            get { return _PlanWeight; }
            set { _PlanWeight = value; }
        }

        public string PlanStatus
        {
            get { return _PlanStatus; }
            set { _PlanStatus = value; }
        }

        #endregion
    }
}
