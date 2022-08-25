using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class IngredientStandardUnit
    {
        // Ingredient Unit
        private int ingredientID;
        private byte standardUnitID;
        private byte standardUnitType;
        private float standardWeight;
        private float weightChangerate;

        private string standardUnitName;
        private string standardUnitDisplay;

        private bool isApplicable;

        #region Properties - Ingredient Unit

        /// <summary>
        /// Get set property for IngredientID
        /// </summary>
        public int IngredientID
        {
            get { return this.ingredientID; }
            set { this.ingredientID = value; }
        }

        /// <summary>
        /// Get set property for StandardUnitID
        /// </summary>
        public byte StandardUnitID
        {
            get { return this.standardUnitID; }
            set { this.standardUnitID = value; }
        }

        /// <summary>
        /// Get set property for StandardUnitType
        /// </summary>
        public byte StandardUnitType
        {
            get { return this.standardUnitType; }
            set { this.standardUnitType = value; }
        }

        /// <summary>
        /// Get set property for StandardWeight
        /// </summary>
        public float StandardWeight
        {
            get { return this.standardWeight; }
            set { this.standardWeight = value; }
        }
        public float WeightChangerate
        {
            get { return this.weightChangerate; }
            set { this.weightChangerate = value; }
        }

        /// <summary>
        /// Get set property for StandardUnitName
        /// </summary>
        public string StandardUnitName
        {
            get { return this.standardUnitName; }
            set { this.standardUnitName = value; }
        }

        /// <summary>
        /// Get set property for StandardUnitDisplay
        /// </summary>
        public string StandardUnitDisplay
        {
            get { return this.standardUnitDisplay; }
            set { this.standardUnitDisplay = value; }
        }

        /// <summary>
        /// Get set property for IsApplicable
        /// </summary>
        public bool IsApplicable
        {
            get { return this.isApplicable; }
            set { this.isApplicable = value; }
        }

        #endregion

    }
}
