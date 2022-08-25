using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class NSysServeUnit
    {
        #region VARIABLES

        // SysServeUnit
        private byte serveUnitID;
        private string serveUnitName;

        #endregion

        #region PROPERTIES

        public byte ServeUnitID
        {
            get
            {
                return serveUnitID;
            }
            set
            {
                serveUnitID = value;
            }
        }
        
        public string ServeUnitName
        {
            get
            {
                return serveUnitName;
            }
            set
            {
                serveUnitName = value;
            }
        }

        #endregion
    }
}
