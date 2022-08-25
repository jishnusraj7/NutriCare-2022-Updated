using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class City
    {
        #region VARIABLES

        private byte cityID;
        private byte languageID;
        private string cityName;

        #endregion

        #region PROPERTIES

        public byte CityID
        {
            get
            {
                return cityID;
            }
            set
            {
                cityID = value;
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

        public string CityName
        {
            get
            {
                return cityName;
            }
            set
            {
                cityName = value;
            }
        }

        #endregion
    }
}
