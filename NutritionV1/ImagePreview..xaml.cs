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
    /// Interaction logic for ImagePreview.xaml
    /// </summary>
    public partial class ImagePreview : Window
    {
        #region Declarations
        
        private ItemType displayitem;
        private int itemID;
        private bool isRegional;

        #endregion

        #region PROPERTIES

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

        #region METHODS

        private void getItemImage()
        {
            string head = string.Empty;
            string imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string imgFile = string.Empty;
            if (DisplayItem == ItemType.Dish)
            {
                Dish dish = new Dish();
                dish = DishManager.GetItem(ItemID, " 1=1 ");
                if (dish != null)
                {
                    if (dish.DisplayImage != string.Empty)
                    {
                        imgFile = imgPath + "Pictures\\Dishes" + "\\" + dish.Id + ".jpg";
                        //imgFile = imgPath + "Pictures\\Dishes" + "\\" + CommonFunctions.EncryptString(Convert.ToString(dish.Id)) + ".jpg";
                        if (IsRegional == true)
                        {
                            head = dish.DisplayName;
                            imgDisplay.ImageName = dish.Name;
                        }
                        else
                        {
                            head = dish.Name;
                            imgDisplay.ImageName = dish.Name;
                        }
                    }
                }
            }
            else
            {
                Ingredient ingredient = new Ingredient();
                ingredient = IngredientManager.GetItem(ItemID);
                if (ingredient != null)
                {
                    if (ingredient.DisplayImage != string.Empty)
                    {
                        imgFile = imgPath + "Pictures\\Ingredients" + "\\" + ingredient.Id + ".jpg";
                        //imgFile = imgPath + "Pictures\\Ingredients" + "\\" + CommonFunctions.EncryptString(Convert.ToString(ingredient.Id)) + ".jpg";
                        if (IsRegional == true)
                        {
                            head = ingredient.DisplayName;
                        }
                        else
                        {
                            head = ingredient.Name;
                        }
                    }
                }
            }
            this.Title = "    " + head;
            if (imgFile != string.Empty)
            {
                imgPath = imgFile;
            }
            else
            {
                imgPath = imgPath + "Images\\NoPreview.png";
            }
            imgDisplay.ImageSource = imgPath;
            imgDisplay.Focus();
        }


        private void SetTheme()
        {
            App apps = (App)Application.Current;
            this.Style = (Style)apps.SetStyle["WinStyle"];
            imgDisplay.SetThemes = true;
        }

        #endregion

        #region EVENTS

        public ImagePreview()
        {
            InitializeComponent();
            SetTheme();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getItemImage();
        }

        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Close();
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion
    }
}
