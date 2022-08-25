using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BONutrition
{
    public class Family
    {
        private int familyID;
        private string familyName;
        private string address1;
        private string address2;
        private byte countryID;
        private byte stateID;
        private byte cityID;
        private string postCode;
        private string phone1;
        private string phone2;
        private byte religionID;
        private byte foodHabitID;
        private byte primaryLanguageID;
        private byte secondaryLanguageID;

        private Country countryItem;
        private State stateItem;
        private City cityItem;
        private Religion religionItem;
        private FoodHabit foodHabitItem;

        /// <summary>
        /// Get or Set a FamilyID - int
        /// </summary>
        public int FamilyID
        {
            set
            {
                familyID = value;
            }
            get
            {
                return familyID;

            }

        }

        /// <summary>
        /// Get or Set a FamilyName - nvarchar(50)
        /// </summary>
        public string FamilyName
        {
            set
            {
                familyName = value;
            }
            get
            {
                return familyName;

            }

        }

        /// <summary>
        /// Get or Set a Address1 - nvarchar(50)
        /// </summary>
        public string Address1
        {
            set
            {
                address1 = value;
            }
            get
            {
                return address1;

            }

        }

        /// <summary>
        /// Get or Set a Address2 - nvarchar(50)
        /// </summary>
        public string Address2
        {
            set
            {
                address2 = value;
            }
            get
            {
                return address2;

            }

        }

        /// <summary>
        /// Get or Set a CountryID - tinyint
        /// </summary>
        public byte CountryID
        {
            set
            {
                countryID = value;
            }
            get
            {
                return countryID;

            }

        }

        /// <summary>
        /// Get or Set a StateID - tinyint
        /// </summary>
        public byte StateID
        {
            set
            {
                stateID = value;
            }
            get
            {
                return stateID;

            }

        }

        /// <summary>
        /// Get or Set a CityID - tinyint
        /// </summary>
        public byte CityID
        {
            set
            {
                cityID = value;
            }
            get
            {
                return cityID;

            }

        }

        /// <summary>
        /// Get or Set a PostCode - nvarchar(15)
        /// </summary>
        public string PostCode
        {
            set
            {
                postCode = value;
            }
            get
            {
                return postCode;

            }

        }

        /// <summary>
        /// Get or Set a Phone1 - nvarchar(15)
        /// </summary>
        public string Phone1
        {
            set
            {
                phone1 = value;
            }
            get
            {
                return phone1;

            }

        }

        /// <summary>
        /// Get or Set a Phone2 - nvarchar(15)
        /// </summary>
        public string Phone2
        {
            set
            {
                phone2 = value;
            }
            get
            {
                return phone2;

            }

        }

        /// <summary>
        /// Get or Set a ReligionID - tinyint
        /// </summary>
        public byte ReligionID
        {
            set
            {
                religionID = value;
            }
            get
            {
                return religionID;

            }

        }

        /// <summary>
        /// Get or Set a FoodHabitID - tinyint
        /// </summary>
        public byte FoodHabitID
        {
            set
            {
                foodHabitID = value;
            }
            get
            {
                return foodHabitID;

            }

        }

        /// <summary>
        /// Get or Set a PrimaryLanguageID - tinyint
        /// </summary>
        public byte PrimaryLanguageID
        {
            set
            {
                primaryLanguageID = value;
            }
            get
            {
                return primaryLanguageID;

            }

        }

        /// <summary>
        /// Get or Set a SecondaryLanguageID - tinyint
        /// </summary>
        public byte SecondaryLanguageID
        {
            set
            {
                secondaryLanguageID = value;
            }
            get
            {
                return secondaryLanguageID;

            }

        }

        public Country CountryItem
        {
            set
            {
                countryItem = value;
            }
            get
            {
                return countryItem;
            }
        }

        public State StateItem
        {
            set
            {
                stateItem = value;
            }
            get
            {
                return stateItem;
            }
        }

        public City CityItem
        {
            set
            {
                cityItem = value;
            }
            get
            {
                return cityItem;
            }
        }

        public Religion ReligionItem
        {
            set
            {
                religionItem = value;
            }
            get
            {
                return religionItem;
            }
        }

        public FoodHabit FoodHabitItem
        {
            set
            {
                foodHabitItem = value;
            }
            get
            {
                return foodHabitItem;
            }
        }
    }
}
