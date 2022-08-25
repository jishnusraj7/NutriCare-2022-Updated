using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class BloodGroup
    {
        #region VARIABLES

        private byte bloodGroupID;        
        private string bloodGroupName;

        #endregion

        #region PROPERTIES

        public byte BloodGroupID
        {
            get
            {
                return bloodGroupID;
            }
            set
            {
                bloodGroupID = value;
            }
        }       

        public string BloodGroupName
        {
            get
            {
                return bloodGroupName;
            }
            set
            {
                bloodGroupName = value;
            }
        }

        #endregion

    }
}
