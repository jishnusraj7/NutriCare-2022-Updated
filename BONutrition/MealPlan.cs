using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class MealPlan
    {

        private int dishID;
        private float standardWeight;
        private string planStatus;

        public int DishID
        {
            set
            {
                dishID = value;
            }
            get
            {
                return dishID;

            }
        }

        public float StandardWeight
        {
            set
            {
                standardWeight = value;
            }
            get
            {
                return standardWeight;

            }
        }
        public string PlanStatus
        {
            set
            {
                planStatus = value;
            }
            get
            {
                return planStatus;

            }
        }

    }
}
