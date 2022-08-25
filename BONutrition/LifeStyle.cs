using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class LifeStyle
    {
        #region VARIABLES

        private byte lifeStyleID;
        private string lifeStyleName;

        #endregion

        #region PROPERTIES

        public byte LifeStyleID
        {
            get
            {
                return lifeStyleID;
            }
            set
            {
                lifeStyleID = value;
            }
        }
      
        public string LifeStyleName
        {
            get
            {
                return lifeStyleName;
            }
            set
            {
                lifeStyleName = value;
            }
        }

        #endregion
    }
}
