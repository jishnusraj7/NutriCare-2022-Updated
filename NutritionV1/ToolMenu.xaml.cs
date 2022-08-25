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
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using Nutrition.Common.Classes;
using Nutrition.Enums;
using System.Threading;
using System.Globalization;
using System.Resources;
using System.Reflection;
using System.Configuration;

namespace Nutrition
{
    /// <summary>
    /// Interaction logic for ToolMenu.xaml
    /// </summary>
    public partial class ToolMenu : Page
    {
        #region Declarations

        ResourceManager rm = ResourceSetting.GetResource();
        ResourceDictionary theme = new ResourceDictionary();
        App apps = (App)Application.Current;

        #endregion

        #region Constructor

        public ToolMenu()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            SetCulture();
            IncludeAyurveda();
        }

        private void mnuItem1_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.HealthCalculator);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    HealthCalculator healthCalculator = new HealthCalculator();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(healthCalculator);
                }
            }
        }

        private void mnuItem2_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.WeightManagement);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    WeightManagement weightManagement = new WeightManagement();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(weightManagement);
                }
            }
        }

        private void mnuItem3_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.AyurvedicTips);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    AyurvedicTips ayurvedicTips = new AyurvedicTips();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(ayurvedicTips);
                }
            }
        }

        private void mnuItem4_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.AyurvedicInformation);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    AyurvedicInformation ayurvedicInformation = new AyurvedicInformation();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(ayurvedicInformation);
                }
            }
        }

        private void mnuItem5_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.BabyCare);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    BabyCare babyCare = new BabyCare();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(babyCare);
                }
            }          
        }

        private void mnuItem6_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.PregnancyCare);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    PregnancyCare pregnancyCare = new PregnancyCare();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(pregnancyCare);
                }
            }
        }

        private void mnuItem7_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.ElderlyCare);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    ElderlyCare elderlyCare = new ElderlyCare();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(elderlyCare);
                }
            }
        }

        private void mnuItem8_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.SpecialSearchDish);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    SpecialSearchDish specialSearchDish = new SpecialSearchDish();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(specialSearchDish);
                }
            }
        }

        private void mnuItem9_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.SpecialSearchIngradient);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    SpecialSearchIngradient specialSearchIngradient = new SpecialSearchIngradient();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(specialSearchIngradient);
                }
            }
        }

        private void mnuItem10_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.CalorieCalculator);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    CalorieCalculator calorieCalculator = new CalorieCalculator();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(calorieCalculator);
                }
            }
        }

        private void mnuItem11_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.FoodCalorieCalculator);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    FoodCalorieCalculator foodCalorieCalculator = new FoodCalorieCalculator();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(foodCalorieCalculator);
                }
            }   
        }

        private void mnuItem12_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.WaterInTake);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    WaterInTake waterInTake = new WaterInTake();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(waterInTake);
                }
            }
        }

        private void mnuItem13_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.UnitConvertion);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    UnitConvertion unitConvertion = new UnitConvertion();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(unitConvertion);
                }
            }
        }

        private void mnuItem14_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.RecipeAnalyzer);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    RecipeAnalyzer recipeAnalyzer = new RecipeAnalyzer();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(recipeAnalyzer);
                }
            }
        }

        private void mnuItem15_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.CompareFood);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    CompareFood compareFood = new CompareFood();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(compareFood);
                }
            }
        }

        private void mnuItem16_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.TableofContents);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    TableofContents tableofContents = new TableofContents();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(tableofContents);
                }
            }
        }
        private void mnuItem17_Click(object sender, RoutedEventArgs e)
        {
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, (int)MenuItemSelect.CompareIngredient);
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (Window.GetWindow(this) != null)
                {
                    CompareIngredient compareIngredient = new CompareIngredient();
                    ((Nutrition.MasterPage)(Window.GetWindow(this))).MainContent.Navigate(compareIngredient);
                }
            }
        }

        #endregion

        #region Method

        private void SetTheme()
        {
            App apps = (App)Application.Current;
        }

        private void SetCulture()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(ResourceSetting.defaultlanguage);
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ResourceSetting.displayLanguage);
            ApplyStyle((int)MenuItemSelect.ToolMenuCount, 9);
        }

        private void ApplyStyle(int menuCount,int selectIndex)
        {
            for (int i = 1; i <= menuCount; i++)
            {
                MenuItem mnuItem = (MenuItem)this.GetType().InvokeMember("mnuItem" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                if (i == selectIndex)
                {
                    mnuItem.Style = (Style)apps.SetStyle["MenuLeftSelectStyle"];
                }
                else
                {
                    mnuItem.Style = (Style)apps.SetStyle["MenuLeftItemStyle"];
                }
            }
        }

        private void IncludeAyurveda()
        {
            if (ConfigurationManager.AppSettings["IncludeAyurvedic"] == "0")
            {

            }
        }

        #endregion
    }
}
