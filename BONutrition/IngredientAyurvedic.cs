using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class IngredientAyurvedic
    {
        #region Variables

        // IngredientAyurvedic
        private int ingredientId;
        private bool isVata;
        private bool isPita;
        private bool isKapa;        
        private byte ayurID;
        private string ayurValue;
        private string ayurValueREG;

        private string ayurParam;
        private string ayurParamREG;

        #endregion        

        public int IngredientId
        {
            get { return this.ingredientId; }
            set { this.ingredientId = value; }
        }
        
        public bool IsVata
        {
            get { return this.isVata; }
            set { this.isVata = value; }
        }

        public bool IsPita
        {
            get { return this.isPita; }
            set { this.isPita = value; }
        }

        public bool IsKapa
        {
            get { return this.isKapa; }
            set { this.isKapa = value; }
        }
       
        public byte AyurID
        {
            get { return this.ayurID; }
            set { this.ayurID = value; }
        }
      
        public string AyurValue
        {
            get { return this.ayurValue; }
            set { this.ayurValue = value; }
        }

        public string AyurValueREG
        {
            get { return this.ayurValueREG; }
            set { this.ayurValueREG = value; }
        }

        public string AyurParam
        {
            get { return this.ayurParam; }
            set { this.ayurParam = value; }
        }

        public string AyurParamREG
        {
            get { return this.ayurParamREG; }
            set { this.ayurParamREG = value; }
        }        
    }
}
