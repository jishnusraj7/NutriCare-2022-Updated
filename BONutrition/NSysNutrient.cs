using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class NSysNutrient
    {
        #region Sys_Nutrient

        private int nutrientID;
        private string nutrientParam;
        private string nutrientUnit;
        private bool isNutrientMain;
        private byte nutrientGroup;
        private float nutrientValue;
        private string nutrientDescription;
        private string nutrientDescriptionEnglish;
        private string nutrientDescriptionRegional;

        private bool isEnabled;
        private bool isReadOnly;        
        private string backgroundStyle;

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

        public float NutrientValue
        {
            get { return this.nutrientValue; }
            set { this.nutrientValue = value; }
        }

        public string BackgroundStyle
        {
            get { return this.backgroundStyle; }
            set { this.backgroundStyle = value; }
        }

        public string NutrientDescription
        {
            get { return this.nutrientDescription; }
            set { this.nutrientDescription = value; }
        }

        public string NutrientDescriptionEnglish
        {
            get { return this.nutrientDescriptionEnglish; }
            set { this.nutrientDescriptionEnglish = value; }
        }

        public string NutrientDescriptionRegional
        {
            get { return this.nutrientDescriptionRegional; }
            set { this.nutrientDescriptionRegional = value; }
        }

        #endregion

        #region Sys_Ayurvedic

        private byte ayurID;        
        private string ayurParam;
        private string ayurDescription;
        private string ayurDescriptionEnglish;
        private string ayurDescriptionRegional;


        public byte AyurID
        {
            get { return this.ayurID; }
            set { this.ayurID = value; }
        }
      
        public string AyurParam
        {
            get { return this.ayurParam; }
            set { this.ayurParam = value; }
        }

        public string AyurDescription
        {
            get { return this.ayurDescription; }
            set { this.ayurDescription = value; }
        }

        public string AyurDescriptionEnglish
        {
            get { return this.ayurDescriptionEnglish; }
            set { this.ayurDescriptionEnglish = value; }
        }

        public string AyurDescriptionRegional
        {
            get { return this.ayurDescriptionRegional; }
            set { this.ayurDescriptionRegional = value; }
        }

        #endregion
    }
}
