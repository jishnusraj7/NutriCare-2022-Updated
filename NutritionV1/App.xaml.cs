using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using NutritionV1.Enums;
using System.Threading;
using System.Globalization;
using System.Resources;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using System.Windows.Threading;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region DECLARATIONS

        private ResourceDictionary res;
        private ResourceManager langresource;
        private string langstring;
        private int dayRecipeid = -1;
        private int ingredientID = -1;
        private int familyId = 0;
        private DateTime foodsettingDate = DateTime.Now;
        private bool includeAyurvedic;

        #endregion

        #region PROPERTIES

        public DateTime FoodSettingDate
        {
            set
            {
                foodsettingDate = value;
            }
            get
            {
                return foodsettingDate;
            }
        }

        public ResourceDictionary SetStyle
        {
            set
            {
                res = value;
            }
            get
            {
                return res;
            }
        }

        public ResourceManager getLanguageList
        {
            set
            {
                langresource = value;
            }
            get
            {
                return langresource;
            }
        }       

        public int FamilyID
        {
            set
            {
                familyId = value;
            }
            get
            {
                return familyId;
            }
        }

        public int DayRecipeID
        {
            set
            {
                dayRecipeid = value;
            }
            get
            {
                return dayRecipeid;
            }
        }

        public int IngredientID
        {
            set
            {
                ingredientID = value;
            }
            get
            {
                return ingredientID;
            }
        }

        public bool IncludeAyurvedic
        {
            set
            {
                includeAyurvedic = value;
            }
            get
            {
                return includeAyurvedic;
            }
        }

        #endregion

        #region METHODS

        public void setLanguage(LanguageList langlist)
        {
            try
            {
                string langstr = string.Empty;
                switch (langlist)
                {
                    case LanguageList.English:
                        langstr = "en-US";
                        break;
                    case LanguageList.Malayalam:
                        langstr = "ja-JP";
                        break;
                }
                ResourceSetting.displayLanguage = langstr;
                ResourceSetting.selectlanguage = (int)langlist;
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(ResourceSetting.displayLanguage);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ResourceSetting.displayLanguage);
                ResourceManager rm = ResourceSetting.GetResource();
                getLanguageList = rm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void setLanguage(int langlist)
        {
            try
            {
                string langstr = "en-US";
                ResourceSetting.displayLanguage = langstr;
                ResourceSetting.selectlanguage = langlist;
                ResourceSetting.defaultlanguage = 1;
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(ResourceSetting.displayLanguage);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ResourceSetting.displayLanguage);
                ResourceManager rm = ResourceSetting.GetResource();
                getLanguageList = rm;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.Properties["ID"] = 0;
            this.Properties["SYSTEM"] = false;
            this.Properties["DISPLAY"] = true;
            this.Properties["ROWINDEX"] = -1;

            this.Properties["ISSAVED"] = false;
            this.Properties["ISDELETED"] = false;
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent, new RoutedEventHandler(TextBox_GotFocus));
            base.OnStartup(e);
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }
}
