using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class Religion
    {
        #region VARIABLES

        private byte religionID;
        private byte languageID;
        private string religionName;

        #endregion

        #region PROPERTIES

        public byte ReligionID
        {
            get
            {
                return religionID;
            }
            set
            {
                religionID = value;
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

        public string ReligionName
        {
            get
            {
                return religionName;
            }
            set
            {
                religionName = value;
            }
        }

        #endregion

    }
}
