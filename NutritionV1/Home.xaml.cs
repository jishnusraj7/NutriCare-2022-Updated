using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NutritionV1.Common.Classes;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        string imagePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Images\\";        

        public Home()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            LoadImages();
        }

        #region Methods

        private void SetTheme()
        {
            App apps = (App)Application.Current;

            imgIngredients.SetThemes = true;            
            imgIngredientsCompare.SetThemes = true;
            imgDishes.SetThemes = true;
            imgDishCompare.SetThemes = true;
            imgMember.SetThemes = true;
            imgMenuPlanner.SetThemes = true;
            imgHealthCalculator.SetThemes = true;
            imgExit.SetThemes = true;
            ((NutritionV1.MasterPage)(Window.GetWindow(this))).mnuTop.Visibility = Visibility.Collapsed;
        }

        private void LoadImages()
        {
            if (File.Exists(imagePath + "HomeIng.png"))
            {
                imgIngredients.ImageSource = System.IO.Path.Combine(imagePath, imagePath + "HomeIng.png");
                imgIngredients.ImageName = "Ingredients";
            }

            if (File.Exists(imagePath + "HomeCompareIng.png"))
            {
                imgIngredientsCompare.ImageSource = System.IO.Path.Combine(imagePath, imagePath + "HomeCompareIng.png");
                imgIngredientsCompare.ImageName = "Compare Ingredients";
            }

            if (File.Exists(imagePath + "HomeDish.png"))
            {
                imgDishes.ImageSource = System.IO.Path.Combine(imagePath, imagePath + "HomeDish.png");
                imgDishes.ImageName = "Dishes";
            }

            if (File.Exists(imagePath + "HomeCompareDish.png"))
            {
                imgDishCompare.ImageSource = System.IO.Path.Combine(imagePath, imagePath + "HomeCompareDish.png");
                imgDishCompare.ImageName = "Compare Dishes";
            }

            if (File.Exists(imagePath + "HomeMember.png"))
            {
                imgMember.ImageSource = System.IO.Path.Combine(imagePath, imagePath + "HomeMember.png");
                imgMember.ImageName = "Member Profile";
            }

            if (File.Exists(imagePath + "HomeCalorie.png"))
            {
                imgMenuPlanner.ImageSource = System.IO.Path.Combine(imagePath, imagePath + "HomeCalorie.png");
                imgMenuPlanner.ImageName = "Menu Planner";
            }

            if (File.Exists(imagePath + "HomeHealth.png"))
            {
                imgHealthCalculator.ImageSource = System.IO.Path.Combine(imagePath, imagePath + "HomeHealth.png");
                imgHealthCalculator.ImageName = "Health Calculators";
            }

            if (File.Exists(imagePath + "HomeSignOff.png"))
            {
                imgExit.ImageSource = System.IO.Path.Combine(imagePath, imagePath + "HomeSignOff.png");
                imgExit.ImageName = "Sign Off";
            }
        }

        private bool UnloadApp()
        {
            return AlertBox.Show("Do You Want to Exit NutriCare", "NutriCare", AlertType.Exclamation, AlertButtons.YESNO);
        }

        private void WriteSearchRegistry(int[] Result)
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            ini.IniWriteValue("SWREGIONAL", "REGIONAL", Result[0].ToString());
            ini.IniWriteValue("SWCATEGORY", "CATEGORY", Result[1].ToString());
            ini.IniWriteValue("SWHNUTRIENT", "HNUTRIENT", Result[2].ToString());
            ini.IniWriteValue("SWLNUTRIENT", "LNUTRIENT", Result[3].ToString());
            ini.IniWriteValue("SWCOMPARECATEGORY", "COMPARECATEGORY", Result[4].ToString());
            ini = null;
        }

        #endregion


        #region Events

        private void imgIngredients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                IngradientSearch ingrSearch = new IngradientSearch();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(ingrSearch);
            }
        }

        private void imgDishes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                DishSearch dishSearch = new DishSearch();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(dishSearch);
            }
        }

        private void imgMenuPlanner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                MenuPlanner menuPlanner = new MenuPlanner();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(menuPlanner);
            }
        }        

        private void imgHealthCalculator_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                HealthCalculator healthCalculator = new HealthCalculator();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(healthCalculator);
            }
        }

        private void imgIngredientsCompare_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                IngradientCompare Compare = new IngradientCompare();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(Compare);
            }
        }

        private void imgDishCompare_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                DishCompare Compare = new DishCompare();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(Compare);
            }
        }

        private void imgMember_MouseDown(object sender, MouseButtonEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                MemberSearch memberSearch = new MemberSearch();
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(memberSearch);
            }
        }

        private void imgExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (UnloadApp())
            {
                for (int i = 0; i < 5; i++)
                {
                    if (i == 1)
                    {
                        ((NutritionV1.MasterPage)(Window.GetWindow(this))).Results[i] = 8;
                    }
                    else
                    {
                        ((NutritionV1.MasterPage)(Window.GetWindow(this))).Results[i] = 0;
                    }
                }

                WriteSearchRegistry(((NutritionV1.MasterPage)(Window.GetWindow(this))).Results);
                Application.Current.Shutdown();
            }
        }       

        #endregion        
    }
}
