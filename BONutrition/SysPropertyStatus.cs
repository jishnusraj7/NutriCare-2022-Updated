using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class SysPropertyStatus
    {
        #region VARIABLES

        private byte propertyStatusID;
        private byte languageID;
        private string propertyStatusName;
        private byte propertyStatusType;

        #endregion

        #region PROPERTIES

        public byte PropertyStatusID
        {
            get { return this.propertyStatusID; }
            set { this.propertyStatusID = value; }
        }

        public byte LanguageID
        {
            get { return this.languageID; }
            set { this.languageID = value; }
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
