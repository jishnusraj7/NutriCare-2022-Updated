using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class CalorieCalculator
    {
        private int ingredientid;
        private int dishID;
        private string displayName;
        private float quantity;
        private string unit;
        private float calorie;
        private float protien;
        private string proteinUnit;
        private float carboHydrate;
        private string carboHydrateUnit;
        private float fat;
        private string fatUnit;
        private float fibre;
        private string fibreUnit;
        private float iron;
        private string ironUnit;
        private float calcium;
        private string calciumUnit;

        public int IngredientId
        {
            get 
            {
                return this.ingredientid;
            }
            set 
            {
                this.ingredientid = value; 
            }
        }

        public int DishID
        {
            get
            { 
                return dishID; 
            }
            set 
            { 
                dishID = value;
            }
        }

        public string DisplayName
        {
            get 
            { 
                return displayName; 
            }
            set
            {
                displayName = value; 
            }
        }

        public float Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
            }
        }

        public string Unit
        {
            get
            {
                return unit;
            }
            set
            {
                unit = value;
            }
        }


        public float Calorie
        {
            get
            { 
                return calorie;
            }
            set
            { calorie = value;
            }
        }

        public float Protien
        {
            get
            { 
                return protien;
            }
            set
            {
                protien = value;
            }
        }

        public string ProtienUnit
        {
            get
            { 
                return proteinUnit; 
            }
            set 
            { 
                proteinUnit = value;
            }
        }

        public float CarboHydrate
        {
            get
            { 
                return carboHydrate;
            }
            set 
            { 
                carboHydrate = value; 
            }
        }

        public string CarboHydrateUnit
        {
            get
            { 
                return carboHydrateUnit;
            }
            set
            {
                carboHydrateUnit = value;
            }
        }

        public float Fat
        {
            get
            { 
                return fat; 
            }
            set
            { 
                fat = value;
            }
        }

        public string FatUnit
        {
            get 
            {
                
                return fatUnit; 
            }
            set 
            { 
                fatUnit = value; 
            }
        }

        public float Fibre
        {
            get
            { 
                return fibre;
            }

            set
            { 
                fibre = value;
            }
        }

        public string FibreUnit
        {
            get
            { 
                return fibreUnit;
            }
            set 
            { 
                fibreUnit = value; 
            }
        }

        public float Iron
        {
            get
            {
                return iron;
            }
            set 
            {
                iron = value; 
            }
        }

        public string IronUnit
        {
            get 
            {
                return ironUnit; 
            }
            set 
            { 
                ironUnit = value;
            }
        }

        public float Calcium
        {
            get
            {
                return calcium;
            }
            set
            { 
                calcium = value;
            }
        }

        public string CalciumUnit
        {
            get 
            { 
                return calciumUnit;
            }
            set 
            {
                calciumUnit = value;
            }
        }

    }
}
