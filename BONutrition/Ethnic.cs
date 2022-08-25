using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class Ethnic
    {
        #region VARIABLES

        private int ethnicID;        
        private string ethnicName;

        #endregion

        #region PROPERTIES

        public int EthnicID
        {
            get { return this.ethnicID; }
            set { this.ethnicID = value; }
        }
        
        public string EthnicName
        {
            get { return this.ethnicName; }
            set { this.ethnicName = value; }
        }

        #endregion
    }
}
