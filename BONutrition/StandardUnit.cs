using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class StandardUnit
    {
        #region VARIABLES

        private int ingredientID;
        private byte standardUnitID;         
        private string standardUnitName; 
        private string standardUnitDisplay;        
        private float standardWeight;
        private byte standardUnitType;

        private bool isApplicable;

        #endregion
 
        #region PROPERTIES

        /// <summary>
        /// Get set property for StandardUnitID
        /// </summary>
        public byte StandardUnitID
        {
            get { return standardUnitID; }
            set { standardUnitID = value; }
        }
        
        /// <summary>
        /// Get set property for StandardUnitName
        /// </summary>
        public string StandardUnitName
        {
            get { return standardUnitName; }
            set { standardUnitName = value; }
        } 

        /// <summary>
        /// Get set property for StandardUnitDisplay
        /// </summary>
        public string StandardUnitDisplay
        {
            get { return standardUnitDisplay; }
            set { standardUnitDisplay = value; }
        }

        /// <summary>
        /// Get set property for StandardWeight
        /// </summary>
        public float StandardWeight
        {
            get { return this.standardWeight; }
            set { this.standardWeight = value; }
        }

        /// <summary>
        /// Get set property for IngredientID
        /// </summary>
        public int IngredientID
        {
            get { return this.ingredientID; }
            set { this.ingredientID = value; }
        }

        /// <summary>
        /// Get set property for IsApplicable
        /// </summary>
        public bool IsApplicable
        {
            get { return this.isApplicable; }
            set { this.isApplicable = value; }
        }

        /// <summary>
        /// Get set property for StandardUnitType
        /// </summary>
        public byte StandardUnitType
        {
            get { return standardUnitType; }
            set { standardUnitType = value; }
        } 

        #endregion

    }
}
