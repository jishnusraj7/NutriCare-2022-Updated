using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class State
    {
        #region VARIABLES

        private byte stateID;
        private byte languageID;
        private string stateName;

        #endregion

        #region PROPERTIES

        public byte StateID
        {
            get
            {
                return stateID;
            }
            set
            {
                stateID = value;
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

        public string StateName
        {
            get
            {
                return stateName;
            }
            set
            {
                stateName = value;
            }
        }

        #endregion
    }
}
