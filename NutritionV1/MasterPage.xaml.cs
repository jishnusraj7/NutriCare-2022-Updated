using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Globalization;
using System.IO;
using System.Configuration;
using BONutrition;
using BLNutrition;
using NutritionV1.Common.Classes;
using NutritionV1.Classes;
using NutritionV1.Enums;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Window
    {
        
        #region Declarations

        ResourceManager rm = ResourceSetting.GetResource();
        string imagePath = AppDomain.CurrentDomain.BaseDirectory.ToString() + "Images\\";
        App apps = (App)Application.Current;
        //IngradientSearch Search = new IngradientSearch();
        Home home = new Home();

        int Result;
        public int[] Results = new int[5];

        private System.Windows.Forms.NotifyIcon NotifyIcon;
        private bool IsApplicationExiting = false; 

        #endregion                       

        #region Constructor

        public MasterPage()
        {
            InitializeComponent();
            MainContent.Navigate(home);
            ShowNotifyIcon();
        }

        #endregion

        #region Methods

        private void ShowNotifyIcon()
        {
            NotifyIcon = new System.Windows.Forms.NotifyIcon();
            NotifyIcon.Icon = new System.Drawing.Icon(imagePath + "AppIcon.ico");
            NotifyIcon.Visible = true;
            NotifyIcon.Text = Convert.ToString(ConfigurationManager.AppSettings["ProductName"]);
            NotifyIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseDown);
        }

        private void CloseNotifyIcon()
        {
            if (!IsApplicationExiting)
            {
                NotifyIcon.Visible = false;
            }
        }
        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                
            }
        }

        private void WriteRegistry(int themeIndex)
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            ini.IniWriteValue("SWTHEME", "THEME", themeIndex.ToString());
            ini = null;
        }

        private int ReadRegistry()
        {            
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            Result = CommonFunctions.Convert2Int(ini.IniReadValue("SWTHEME", "THEME"));
            ini = null;
            return Result;
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

        private int[] ReadSearchRegistry()
        {
            INIFileServices ini = new INIFileServices(AppDomain.CurrentDomain.BaseDirectory.ToString() + "Setting.ini");
            Results[0] = CommonFunctions.Convert2Int(ini.IniReadValue("SWREGIONAL", "REGIONAL"));
            Results[1] = CommonFunctions.Convert2Int(ini.IniReadValue("SWCATEGORY", "CATEGORY"));
            Results[2] = CommonFunctions.Convert2Int(ini.IniReadValue("SWHNUTRIENT", "HNUTRIENT"));
            Results[3] = CommonFunctions.Convert2Int(ini.IniReadValue("SWLNUTRIENT", "LNUTRIENT"));
            Results[4] = CommonFunctions.Convert2Int(ini.IniReadValue("SWCOMPARECATEGORY", "COMPARECATEGORY"));
            ini = null;
            return Results;
        }

        private void ChangeTheme(int themeIndex)
        {           
            Uri themeUri = null;
            ResourceDictionary theme = new ResourceDictionary();
            ResourceDictionary commontheme = new ResourceDictionary();
            commontheme = (ResourceDictionary)Application.LoadComponent(new Uri("Common/Xaml/Common.xaml", UriKind.Relative));
            switch (themeIndex)
            {
                case 0:
                    themeUri = new Uri("Common/Xaml/OceanTheme.xaml", UriKind.Relative);
                    if (File.Exists(imagePath + "OceanTheme.png"))
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "OceanTheme.png"));
                    }
                    else
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "NutritionHead.png"));
                    }
                    break;
                case 1:
                    themeUri = new Uri("Common/Xaml/NatureGreen.xaml", UriKind.Relative);
                    if (File.Exists(imagePath + "NatureGreen.png"))
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "NatureGreen.png"));
                    }
                    else
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "NutritionHead.png"));
                    }
                    break;
                case 2:
                    themeUri = new Uri("Common/Xaml/MetalicBlack.xaml", UriKind.Relative);
                    if (File.Exists(imagePath + "MetalicBlack.png"))
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "MetalicBlack.png"));
                    }
                    else
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "NutritionHead.png"));
                    }
                    break;
                case 3:
                    themeUri = new Uri("Common/Xaml/PinkTheme.xaml", UriKind.Relative);
                    if (File.Exists(imagePath + "PinkTheme.png"))
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "PinkTheme.png"));
                    }
                    else
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "NutritionHead.png"));
                    }
                    break;
                case 4:
                    themeUri = new Uri("Common/Xaml/BrownTheme.xaml", UriKind.Relative);
                    if (File.Exists(imagePath + "BrownTheme.png"))
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "BrownTheme.png"));
                    }
                    else
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "NutritionHead.png"));
                    }
                    break;
                case 5:
                    themeUri = new Uri("Common/Xaml/PineappleTheme.xaml", UriKind.Relative);
                    if (File.Exists(imagePath + "PineappleTheme.png"))
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "PineappleTheme.png"));
                    }
                    else
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "NutritionHead.png"));
                    }
                    break;
                case 6:
                    themeUri = new Uri("Common/Xaml/GrapeTheme.xaml", UriKind.Relative);
                    if (File.Exists(imagePath + "GrapeTheme.png"))
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "GrapeTheme.png"));
                    }
                    else
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "NutritionHead.png"));
                    }
                    break;
                default:
                    themeUri = new Uri("Common/Xaml/OceanTheme.xaml", UriKind.Relative);
                    if (File.Exists(imagePath + "OceanTheme.png"))
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "OceanTheme.png"));
                    }
                    else
                    {
                        imgHeader.Source = new BitmapImage(new Uri(imagePath + "NutritionHead.png"));
                    }
                    break;
            }

            theme = (ResourceDictionary)Application.LoadComponent(themeUri);
            ApplyMainMenuStyle(mnubtn1,theme);
            ApplyMainMenuStyle(mnubtn11, theme);
            ApplyMainMenuStyle(mnubtn12, theme);
            ApplyMainMenuStyle(mnubtn2, theme);
            ApplyMainMenuStyle(mnubtn21, theme);
            ApplyMainMenuStyle(mnubtn22, theme);
            ApplyMainMenuStyle(mnubtn3, theme);
            ApplyMainMenuStyle(mnubtn31, theme);
            ApplyMainMenuStyle(mnubtn32, theme);
            ApplyMainMenuStyle(mnubtn4, theme);
            ApplyMainMenuStyle(mnubtn5, theme);
            ApplyMainMenuStyle(mnubtn51, theme);
            ApplyMainMenuStyle(mnubtn52, theme);
            ApplyMainMenuStyle(btnHome, theme);
           
            App.Current.Resources.Clear();
            this.Style = (Style)theme["WinStyle"];
            LayoutRoot.Style = (Style)theme["WindowStyle"];
            recRight.Style = (Style)theme["RightBarStyle"];
            //btnHome.Style = (Style)theme["TransparentButton"];
            //recHome.Style = (Style)theme["BackStyle"];
                  
            App apps = (App)Application.Current;
            apps.SetStyle = theme;
            Resources.MergedDictionaries.Add(commontheme); 
            Resources.MergedDictionaries.Add(theme);
            App.Current.Resources.MergedDictionaries.Add(commontheme);
            App.Current.Resources.MergedDictionaries.Add(theme);
            MainContent.Refresh();
        }

        private void ApplyMainMenuStyle(MenuItem mnu, ResourceDictionary theme)
        {
            if (mnu.IsFocused == true)
                mnu.Style = (Style)theme["MenuItemSelectStyle"];
            else
                mnu.Style = (Style)theme["MenuTopItemStyle"];
        }

        private void GridAnimation(string animationname)
        {
            MyGrid.Visibility = Visibility.Visible;
            Storyboard gridAnimation = (Storyboard)FindResource(animationname);
            gridAnimation.Begin(this);
        }

        private void ApplyStyle(int menuCount, int selectIndex)
        {
            for (int i = 1; i <= menuCount; i++)
            {
                MenuItem mnuItem = (MenuItem)this.GetType().InvokeMember("mnubtn" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                if (i == selectIndex)
                {
                    mnuItem.Style = (Style)apps.SetStyle["MenuItemSelectStyle"];
                }
                else
                {
                    mnuItem.Style = (Style)apps.SetStyle["MenuTopItemStyle"];
                }
            }
        }

        private bool UnloadApp()
        {
            return AlertBox.Show("Do You Want to Exit NutriCare", "NutriCare", AlertType.Exclamation, AlertButtons.YESNO);
        }

        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int Theme = ReadRegistry();
            ChangeTheme(Theme);
            switch (Theme)
            {
                case (int)ThemeStyles.NaturalGreen:
                    btnGreen_Click(btnGreen, e);
                    break;
                case (int)ThemeStyles.MetalicBlack:
                    btnBlack_Click(btnBlack, e);
                    break;
                case (int)ThemeStyles.OceanTheme:
                    btnBlue_Click(btnBlue, e);
                    break;
                case (int)ThemeStyles.PinkTheme:
                    btnPink_Click(btnPink, e);
                    break;
                case (int)ThemeStyles.BrownTheme:
                    btnBrown_Click(btnBrown, e);
                    break;
                case (int)ThemeStyles.PineappleTheme:
                    btnPineapple_Click(btnPineapple, e);
                    break;
                case (int)ThemeStyles.GrapeTheme:
                    btnGrape_Click(btnGrape, e);
                    break;
            }
            ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.Search);
        }

        void NotifyIcon_MouseDown(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        } 

        private void GoToPageExecuteHandler(object sender, ExecutedRoutedEventArgs e)
        {
            MainContent.NavigationService.Navigate(new Uri((string)e.Parameter, UriKind.Relative));
        }

        private void GoToPageCanExecuteHandler(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).ContextMenu.IsEnabled = true;
            (sender as Button).ContextMenu.PlacementTarget = (sender as Button);
            (sender as Button).ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            (sender as Button).ContextMenu.IsOpen = true;
        }

        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            WriteRegistry((int)ThemeStyles.NaturalGreen);
            ChangeTheme((int)ThemeStyles.NaturalGreen);
        }

        private void btnBlack_Click(object sender, RoutedEventArgs e)
        {
            WriteRegistry((int)ThemeStyles.MetalicBlack);
            ChangeTheme((int)ThemeStyles.MetalicBlack);
        }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            WriteRegistry((int)ThemeStyles.OceanTheme);
            ChangeTheme((int)ThemeStyles.OceanTheme);
        }

        private void btnPink_Click(object sender, RoutedEventArgs e)
        {
            WriteRegistry((int)ThemeStyles.PinkTheme);
            ChangeTheme((int)ThemeStyles.PinkTheme);
        }

        private void btnBrown_Click(object sender, RoutedEventArgs e)
        {
            WriteRegistry((int)ThemeStyles.BrownTheme);
            ChangeTheme((int)ThemeStyles.BrownTheme);
        }

        private void btnPineapple_Click(object sender, RoutedEventArgs e)
        {
            WriteRegistry((int)ThemeStyles.PineappleTheme);
            ChangeTheme((int)ThemeStyles.PineappleTheme);
        }

        private void btnGrape_Click(object sender, RoutedEventArgs e)
        {
            WriteRegistry((int)ThemeStyles.GrapeTheme);
            ChangeTheme((int)ThemeStyles.GrapeTheme);
        }
       
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Escape)
            {

            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (UnloadApp())
            {
                CloseNotifyIcon();

                for (int i = 0; i < 5; i++)
                {
                    if (i == 1)
                    {
                        Results[i] = 8;
                    }
                    else
                    {
                        Results[i] = 0;
                    }
                }
                WriteSearchRegistry(Results);
                Application.Current.Shutdown();
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.Home);
                Home home = new Home();
                MainContent.Navigate(home);
            }
        }

        private void IngrSearch_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.Search);
                IngradientSearch Search = new IngradientSearch();
                MainContent.Navigate(Search);

                //ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.New);
                //AddIngredient addIngredient = new AddIngredient();
                //MainContent.Navigate(addIngredient);
            }
        }

        private void DishSearch_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.Search);
                DishSearch Search = new DishSearch();
                MainContent.Navigate(Search);
            }
        }

        private void IngrCompare_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.Compare);
                IngradientCompare Compare = new IngradientCompare();
                MainContent.Navigate(Compare);
            }
        }

        private void DishCompare_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.Compare);
                DishCompare Compare = new DishCompare();
                MainContent.Navigate(Compare);
            }
        }

        private void IngrAdd_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.New);
                AddIngredient addIngredient = new AddIngredient();
                MainContent.Navigate(addIngredient);
            }
        }

        private void DishAdd_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.New);
                AddDish addDish = new AddDish();
                MainContent.Navigate(addDish);
            }

            //using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            //{
            //    ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.New);
            //    AddDishUpdate addDish = new AddDishUpdate();
            //    MainContent.Navigate(addDish);
            //}


        }

        private void MenuPlanner_Click(object sender, RoutedEventArgs e)
        {
            //using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            //{
            //    ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.Calculators);
            //    CalorieCalculator calorieCalculator = new CalorieCalculator();
            //    MainContent.Navigate(calorieCalculator);
            //}

            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.Calculators);
                MenuPlanner menuPlanner = new MenuPlanner();
                MainContent.Navigate(menuPlanner);
            }
        }

        private void HealthCalculators_Click(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                ApplyStyle((int)MenuItemSelect.ItemCount, (int)MenuItemSelect.Calculators);
                HealthCalculator healthCalculator = new HealthCalculator();
                MainContent.Navigate(healthCalculator);
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GridAnimation("CollapseGrid");
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (UnloadApp())
            {
                CloseNotifyIcon();

                for (int i = 0; i < 5; i++)
                {
                    Results[i] = 0;
                }
                WriteSearchRegistry(Results);
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void lblAppClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (UnloadApp())
            {
                CloseNotifyIcon();

                for (int i = 0; i < 5; i++)
                {
                    if (i == 1)
                    {
                        Results[i] = 8;
                    }
                    else
                    {
                        Results[i] = 0;
                    }
                }
                WriteSearchRegistry(Results);
                Application.Current.Shutdown();
            }
        }

        private void lblAppMinimize_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }

       #endregion

        private void mnubtn5_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
