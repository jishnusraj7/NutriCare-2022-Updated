using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class GoalSetting
    {
        private int goalID;
        private int familyID;
        private int memberID;
        private DateTime startDate;
        private DateTime endDate;
        private float presentWeight;
        private int desiredGoal;
        private float desiredWeight;
        private bool considerCalorieInTake;
        private float presentCalorieInTake;
        private float exerciseCalorieInTake;
        private float calorieInTake;
        private string exerciseName;
        private byte exerciseID;
        private int exerciseDuration;

        public int GoalID
        {
            get
            {
                return goalID;
            }
            set
            {
                goalID = value;
            }
        }

        public int FamilyID
        {
            get 
            { 
                return familyID; 
            }
            set 
            { 
                familyID = value; 
            }
        }

        public int MemberID
        {
            get 
            { 
                return memberID; 
            }
            set 
            { 
                memberID = value; 
            }
        }

        public DateTime StartDate
        {
            get 
            { 
                return startDate; 
            }
            set 
            { 
                startDate = value; 
            }
        }

        public DateTime EndDate
        {
            get 
            { 
                return endDate; 
            }
            set 
            { 
                endDate = value; 
            }
        }

        public int DesiredGoal
        {
            get 
            { 
                return desiredGoal; 
            }
            set 
            { 
                desiredGoal = value; 
            }
        }

        public float PresentWeight
        {
            get
            {
                return presentWeight;
            }
            set
            {
                presentWeight = value;
            }
        }

        public float DesiredWeight
        {
            get 
            { 
                return desiredWeight; 
            }
            set 
            { 
                desiredWeight = value; 
            }
        }

        public bool ConsiderCalorieInTake
        {
            get
            {
                return considerCalorieInTake;
            }
            set
            {
                considerCalorieInTake = value;
            }
        }

        public float PresentCalorieInTake
        {
            get
            {
                return presentCalorieInTake;
            }
            set
            {
                presentCalorieInTake = value;
            }
        }

        public float ExerciseCalorieInTake
        {
            get
            {
                return exerciseCalorieInTake;
            }
            set
            {
                exerciseCalorieInTake = value;
            }
        }


        public float CalorieInTake
        {
            get 
            { 
                return calorieInTake; 
            }
            set 
            { 
                calorieInTake = value; 
            }
        }

        public string ExerciseName
        {
            get
            {
                return exerciseName;
            }
            set
            {
                exerciseName = value;
            }
        }

        public byte ExerciseID
        {
            set
            {
                exerciseID = value;
            }
            get
            {
                return exerciseID;

            }

        }

        public int ExerciseDuration
        {
            set
            {
                exerciseDuration = value;
            }
            get
            {
                return exerciseDuration;

            }

        }


    }
}
