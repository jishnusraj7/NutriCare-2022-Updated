using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BONutrition;
using BLNutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Classes;
using System.Resources;
using System.Data; 
using NutritionV1.Enums;
namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for HealthDetails.xaml
    /// </summary>
    public partial class HealthDetails : Window
    {
        #region Declarations

        private ItemType displayitem;
        private int itemID;
        private bool isRegional;

        #endregion

        #region Properties

        public ItemType DisplayItem
        {
            set
            {
                displayitem = value;
            }
            get
            {
                return displayitem;
            }
        }

        public int ItemID
        {
            set
            {
                itemID = value;

            }
            get
            {
                return itemID;
            }

        }

        public int IngredientID
        {
            set
            {
                SetTheme();
                GetIngredientHealthDetails(value);
            }

        }

        public int DishID
        {
            set
            {
                SetTheme();
                GetDishHealthDetails(value);
            }

        }

        public bool IsRegional
        {
            set
            {
                isRegional = value;

            }
            get
            {
                return isRegional;
            }

        }


        #endregion

        #region Constructor

        public HealthDetails()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #endregion

        #region Methods

        private void GetIngredientHealthDetails(int ingredintid)
        {
            try
            {
                Ingredient ingredientList = new Ingredient();
                ingredientList = IngredientManager.GetItem(ingredintid);

                if (ingredientList != null)
                {
                    txtGeneralHealthValues.Text = Convert.ToString(ingredientList.GeneralHealthValue);
                    txtAyurvedicHealthValues.Text = Convert.ToString(ingredientList.AyurHealthValue);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void GetDishHealthDetails(int DishID)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            this.Style = (Style)apps.SetStyle["WinStyle"];
        }

        private void GetItemHeader()
        {
            string Header = string.Empty;

            if (DisplayItem == ItemType.Dish)
            {
                Dish Dish = new Dish();
                Dish = DishManager.GetItem(ItemID, " 1=1 ");
                if (Dish != null)
                {
                    if (IsRegional == true)
                    {
                        Header = Dish.DisplayName;
                    }
                    else
                    {
                        Header = Dish.Name;
                    }
                }
            }
            else
            {
                Ingredient Ingredient = new Ingredient();
                Ingredient = IngredientManager.GetItem(ItemID);
                if (Ingredient != null)
                {
                    if (IsRegional == true)
                    {
                        Header = Ingredient.DisplayName;
                    }
                    else
                    {
                        Header = Ingredient.Name;
                    }
                }
            }

            this.Title = "    " + Header;
        }

        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetItemHeader();
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        #endregion

        
    }
}
