using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
	public class DishIngredient
	{
		#region VARIABLES

		// Dish Table		
		private int dishIngredientID;
		private int dishId;
		private int ingredientID;
		private float quantity;
        private float gramWeight;
        private float weightChangeRate;
		private byte displayOrder;
		private byte standardUnitID;
        private byte standardUnitType;
        private string standardUnitDisplay;
        private string standardUnitName;
		private string sectionName;
		private string ingredientName;
        private string displayName;
		private string ingredientREGName;
        private List<IngredientStandardUnit> ingredientUnitList;

        private bool isEnabled;
        private bool isReadOnly;

        private int nutrientID;
        private string nutrientParam;
        private float standardWeight;
        private float nutrientIngrValue;
        private float nutrientValue;
        private string nutrientUnit;

        private float calorie;
        private float protien;
        private float carboHydrate;
        private float fat;
        private float fibre;
        private float iron;
        private float calcium;        

		#endregion

		#region PROPERTIES - Dish Ingredient

		public int DishIngredientID
		{
			get { return this.dishIngredientID; }
			set { this.dishIngredientID = value; }
		}
		public int DishId
		{
			get { return this.dishId; }
			set { this.dishId = value; }
		}
		public int IngredientID
		{
			get { return this.ingredientID; }
			set { this.ingredientID = value; }
		}
		public float Quantity
		{
			get { return this.quantity; }
			set { this.quantity = value; }
		}
        public float GramWeight
        {
            get { return this.gramWeight; }
            set { this.gramWeight = value; }
        }
        public float WeightChangeRate
        {
            get { return this.weightChangeRate; }
            set { this.weightChangeRate = value; }
        }
		public byte DisplayOrder
		{
			get { return this.displayOrder; }
			set { this.displayOrder = value; }
		}
		public byte StandardUnitID
		{
			get { return this.standardUnitID; }
			set { this.standardUnitID = value; }
		}
        public byte StandardUnitType
        {
            get { return this.standardUnitType; }
            set { this.standardUnitType = value; }
        }
        public string StandardUnitDisplay
        {
            get { return this.standardUnitDisplay; }
            set { this.standardUnitDisplay = value; }
        }

        public string StandardUnitName
        {
            get { return this.standardUnitName; }
            set { this.standardUnitName = value; }
        }

		public string SectionName
		{
			get { return this.sectionName; }
			set { this.sectionName = value; }
		}
		public string IngredientName
		{
			get { return this.ingredientName; }
			set { this.ingredientName = value; }
		}
        public string DisplayName
        {
            get { return this.displayName; }
            set { this.displayName = value; }
        }
		public string IngredientREGName
		{
			get { return this.ingredientREGName; }
			set { this.ingredientREGName = value; }
		}

        public List<IngredientStandardUnit> IngredientUnitList
        {
            get { return this.ingredientUnitList;}
            set { this.ingredientUnitList = value;}
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.isEnabled = value; }
        }

        public bool IsReadOnly
        {
            get { return this.isReadOnly; }
            set { this.isReadOnly = value; }
        }

        public int NutrientID
        {
            get { return this.nutrientID; }
            set { this.nutrientID = value; }
        }
        public string NutrientParam
        {
            get { return this.nutrientParam; }
            set { this.nutrientParam = value; }
        }
        public float StandardWeight
        {
            get { return this.standardWeight; }
            set { this.standardWeight = value; }
        }
        public float NutrientIngrValue
        {
            get { return this.nutrientIngrValue; }
            set { this.nutrientIngrValue = value; }
        }
        public float NutrientValue
        {
            get { return this.nutrientValue; }
            set { this.nutrientValue = value; }
        }
        public string NutrientUnit
        {
            get { return this.nutrientUnit; }
            set { this.nutrientUnit = value; }
        }

        public float Calorie
        {
            get { return this.calorie; }
            set { this.calorie = value; }
        }
        public float Protien
        {
            get { return this.protien; }
            set { this.protien = value; }
        }
        public float CarboHydrate
        {
            get { return this.carboHydrate; }
            set { this.carboHydrate = value; }
        }
        public float Fat
        {
            get { return this.fat; }
            set { this.fat = value; }
        }
        public float Fibre
        {
            get { return this.fibre; }
            set { this.fibre = value; }
        }
        public float Calcium
        {
            get { return this.calcium; }
            set { this.calcium = value; }
        }
        public float Iron
        {
            get { return this.iron; }
            set { this.iron = value; }
        }

		#endregion
	}
}
