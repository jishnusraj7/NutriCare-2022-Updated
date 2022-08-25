using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class Sex
    {
        #region VARIABLES

        private byte sexID;
        private string sexName;

        #endregion

        #region PROPERTIES

        public byte SexID
        {
            get
            {
                return sexID;
            }
            set
            {
                sexID = value;
            }
        }
       
        public string SexName
        {
            get
            {
                return sexName;
            }
            set
            {
                sexName = value;
            }
        }

        #endregion
    }
}
