using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class NSysPropertyStatus
    {
        #region Varaibles

        private byte propertyStatusID;
        private string propertyStatusName;
        private byte propertyStatusType;

        #endregion

        #region Properties

        public byte PropertyStatusID
        {
            get { return this.propertyStatusID; }
            set { this.propertyStatusID = value; }
        }        

        public string PropertyStatusName
        {
            get { return this.propertyStatusName; }
            set { this.propertyStatusName = value; }
        }

        public byte PropertyStatusType
        {
            get { return this.propertyStatusType; }
            set { this.propertyStatusType = value; }
        }

        #endregion
    }
}
