using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class FoodHabit
    {
        #region VARIABLES

        private byte foodHabitID;        
        private string foodHabitName;

        #endregion

        #region PROPERTIES

        public byte FoodHabitID
        {
            get
            {
                return foodHabitID;
            }
            set
            {
                foodHabitID = value;
            }
        }      

        public string FoodHabitName
        {
            get
            {
                return foodHabitName;
            }
            set
            {
                foodHabitName = value;
            }
        }

        #endregion

    }
}
