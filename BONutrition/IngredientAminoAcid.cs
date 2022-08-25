using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class IngredientAminoAcid
    {
        #region Variables

        // IngredientAminoAcid
        private int ingredientId;
        private int nutrientID;
        private float nutrientValue;

        // Ingredient Nutrients
        private NSysNutrient sysNutrientItem;
        private string nutrientParam;
        private string nutrientUnit;
        private bool isNutrientMain;
        private byte nutrientGroup;
        private bool isEnabled;
        private bool isReadOnly;

        #endregion

        #region Properties - AminoAcid

        public int IngredientId
        {
            get { return this.ingredientId; }
            set { this.ingredientId = value; }
        }

        public int NutrientID
        {
            get { return this.nutrientID; }
            set { this.nutrientID = value; }
        }

        public float NutrientValue
        {
            get { return this.nutrientValue; }
            set { this.nutrientValue = value; }
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
        
        #endregion

        #region Ingredient Nutrients

        public NSysNutrient SysNutrientItem
        {
            get
            {
                return sysNutrientItem;
            }
            set
            {
                sysNutrientItem = value;
            }
        }

        public string NutrientParam
        {
            get { return this.nutrientParam; }
            set { this.nutrientParam = value; }
        }

        public string NutrientUnit
        {
            get { return this.nutrientUnit; }
            set { this.nutrientUnit = value; }
        }

        public bool IsNutrientMain
        {
            get { return this.isNutrientMain; }
            set { this.isNutrientMain = value; }
        }

        public byte NutrientGroup
        {
            get { return this.nutrientGroup; }
            set { this.nutrientGroup = value; }
        }

        #endregion
    }
}
