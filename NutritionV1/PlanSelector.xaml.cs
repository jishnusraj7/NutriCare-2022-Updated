using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Media.Animation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;
using System.Xml;
using System.Resources;
using System.Threading;
using System.Configuration;

using BONutrition;
using BLNutrition;
using NutritionV1.Enums;
using NutritionV1.Classes;
using NutritionV1.Common.Classes;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for PlanSelector.xaml
    /// </summary>
    public partial class PlanSelector : Window
    {
        private int dishID;
        private float SelectedPlan;
        private bool isRegional;
        private int planID;
        private float Plan1;
        private float Plan2;
        private float Plan3;

        public int DishID
        {
            set
            {
                dishID = value;
            }
            get
            {
                return dishID;
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

        public int PlanID
        {
            set
            {
                planID = value;
            }
            get
            {
                return planID;
            }
        }

        public PlanSelector()
        {
            InitializeComponent();
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            this.Style = (Style)apps.SetStyle["WinStyle"];
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            Initialize();
            FillData();
        }


        private void Initialize()
        {
            lblPlan1.Visibility = Visibility.Hidden;
            rbPlan1.Visibility = Visibility.Hidden;
            lblPlan2.Visibility = Visibility.Hidden;
            rbPlan2.Visibility = Visibility.Hidden;
            lblPlan3.Visibility = Visibility.Hidden;
            rbPlan3.Visibility = Visibility.Hidden;
        }

        private void FillData()
        {
            Dish dish = new Dish();
            dish = DishManager.GetItem(dishID);

            if (dish != null)
            {
                if (dish.DisplayName != dish.Name)
                {
                    lblDishName.Content = dish.Name + " / " + dish.DisplayName;
                }
                else
                {
                    lblDishName.Content = dish.Name;
                }

                if (dish.StandardWeight > 0)
                {
                    lblPlan1.Content = "Plan I" + "   " + Convert.ToString(dish.StandardWeight) + " gm   " + Convert.ToString(dish.ServeCount) + " Nos";
                    lblPlan1.Visibility = Visibility.Visible;
                    rbPlan1.Visibility = Visibility.Visible;
                    Plan1 = dish.StandardWeight;
                }
                if (dish.StandardWeight1 > 0)
                {
                    lblPlan2.Content = "Plan II" + "   " + Convert.ToString(dish.StandardWeight1) + " gm   " + Convert.ToString(dish.ServeCount1) + " Nos";
                    lblPlan2.Visibility = Visibility.Visible;
                    rbPlan2.Visibility = Visibility.Visible;
                    Plan2 = dish.StandardWeight1;
                }
                if (dish.StandardWeight2 > 0)
                {
                    lblPlan3.Content = "Plan III" + "   " + Convert.ToString(dish.StandardWeight2) + " gm   " + Convert.ToString(dish.ServeCount2) + " Nos";
                    lblPlan3.Visibility = Visibility.Visible;
                    rbPlan3.Visibility = Visibility.Visible;
                    Plan3 = dish.StandardWeight2;
                }

                switch (PlanID)
                { 
                    case 0:
                        rbPlan1.IsChecked = true;
                        break;
                    case 1:
                        rbPlan2.IsChecked = true;
                        break;
                    case 2:
                        rbPlan3.IsChecked = true;
                        break;
                }
            }
        }

        private void imgPrint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReportViewer dishReport = new ReportViewer();
            dishReport.DishID = dishID;
            dishReport.DisplayItem = ItemType.Dish;
            dishReport.ItemID = dishID;
            dishReport.Plan = SelectedPlan;
            dishReport.ReportType = (int)ReportItem.Dish;
            if (IsRegional == true)
            {
                dishReport.IsRegional = true;
            }
            else
            {
                dishReport.IsRegional = false;
            }
            dishReport.Owner = Application.Current.MainWindow;
            dishReport.ShowDialog();
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void rbPlan1_Checked(object sender, RoutedEventArgs e)
        {
            SelectedPlan = Plan1;
        }

        private void rbPlan2_Checked(object sender, RoutedEventArgs e)
        {
            SelectedPlan = Plan2;
        }

        private void rbPlan3_Checked(object sender, RoutedEventArgs e)
        {
            SelectedPlan = Plan3;
        }

        private void txtTitle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
