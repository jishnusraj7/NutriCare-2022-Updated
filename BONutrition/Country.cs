using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class Country
    {
        #region VARIABLES

        private byte countryID;
        private byte languageID;
        private string countryName;        

        #endregion

        #region PROPERTIES

        public byte CountryID
        {
            get 
            { 
                return countryID; 
            }
            set 
            { 
                countryID = value; 
            }
        }

        public byte LanguageID
        {
            get 
            { 
                return languageID; 
            }
            set 
            { 
                languageID = value; 
            }
        }

        public string CountryName
        {
            get 
            { 
                return countryName; 
            }
            set 
            { 
                countryName = value; 
            }
        }

        #endregion

    }
}
