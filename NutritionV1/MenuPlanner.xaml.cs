using System;
using System.Data;
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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Resources;
using System.Reflection;
using System.Collections.ObjectModel;
using BONutrition;
using BLNutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Classes;
using NutritionV1.Enums;
using NutritionV1.Common.Classes;
using NutritionV1.Constants;
using System.IO;
using System.Collections;
using System.Configuration;

namespace NutritionV1
{
    /// <summary>
    /// This form is to set the Daily Meal setting of a Family
    /// if you want to add any Column into the Grid add the column and Modify code in save function only
    /// </summary>
    public partial class MenuPlanner : Page
    {
        #region Variables

        private enum ListViewIndex
        {
            Plan = 3,
        }

        private enum DataColumnIndex
        {
            dishID = 0,
            dishName = 1,
            serveUnit = 4,
            displayName = 6,
            standardWeight = 7,
            quantity = 8,           
        }        

        Dish dish = new Dish();
        List<Dish> dishList = new List<Dish>();
        //bool displayStatus = true;
        private static int editDishID;
        private int familyId = 0; // Store the Family Id of the user
        private int memberID = 0;        
        private int gridid = 0; // Store which Listview is updating Currently
        DataTable dtableUnit = new DataTable(); 
        DataSet foodSetting = new DataSet();
        int visibleFlag = 0;
        Member memberItem = new Member();
        
        DataTable dtFoodSetting1 = new DataTable(); 
        DataTable dtFoodSetting2 = new DataTable();
        DataTable dtFoodSetting3 = new DataTable();
        DataTable dtFoodSetting4 = new DataTable();

        DataTable dtCalorie1 = new DataTable();
        DataTable dtCalorie2 = new DataTable();
        DataTable dtCalorie3 = new DataTable();
        DataTable dtCalorie4 = new DataTable();

        DataTable dtCalorieTotal = new DataTable();

        DataSet foodPlanList = new DataSet();

        DataTable dtMember = new DataTable();

        private static ArrayList searchDishID = new ArrayList();
        ArrayList weekDate = new ArrayList();

        #endregion

        #region Properties

        public static int EditDishID
        {
            get
            {
                return editDishID;
            }
            set
            {
                editDishID = value;
            }
        }

        public static ArrayList SearchDishID
        {
            get
            {
                return searchDishID;
            }
            set
            {
                searchDishID = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Setting the Culture into the form from resource files
        /// </summary>
        private void SetCulture()
        {
            App apps = (App)Application.Current;
            familyId = MemberManager.GetFamilyID();
            ResourceManager rm = apps.getLanguageList;            
        }

        private void LoadTemplate()
        {
            gvColBreakFastPlan.CellTemplate = this.FindResource("planBreakFastDisplayTemplate") as DataTemplate;
            gvColLunchPlan.CellTemplate = this.FindResource("planLunchDisplayTemplate") as DataTemplate;
            gvColDinnerPlan.CellTemplate = this.FindResource("planDinnerDisplayTemplate") as DataTemplate;
            gvColSnacksPlan.CellTemplate = this.FindResource("planSnacksDisplayTemplate") as DataTemplate;
        }

        private void SetLabelStyle(Label lblName,int displayType)
        {
            switch(displayType)
            {                
                case 1:
                    lblName.Foreground = new SolidColorBrush(Color.FromRgb(0,255,255));
                    lblName.FontWeight = FontWeights.Bold;
                    lblName.FontSize = 13;
                    break;
                case 2:
                    lblName.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    lblName.FontWeight = FontWeights.Bold;
                    lblName.FontSize = 13;
                    break;
                case 3:
                    lblName.Foreground = new SolidColorBrush(Color.FromRgb(6, 118, 6));
                    lblName.FontWeight = FontWeights.Bold;
                    lblName.FontSize = 13;
                    break;
                case 4:
                    lblName.Foreground = new SolidColorBrush(Color.FromRgb(199, 12, 12));
                    lblName.FontWeight = FontWeights.Bold;
                    lblName.FontSize = 13;
                    break;
            }
        }

        /// <summary>
        /// Clearing the Values of forms
        /// </summary>
        private void ClearAll()
        {
            SetLabelStyle(lblCalorieIntake, 2);
            SetLabelStyle(lblBreakFastCalorie, 1);
            SetLabelStyle(lblLunchCalorie, 1);
            SetLabelStyle(lblDinnerCalorie, 1);
            SetLabelStyle(lblSnacksCalorie, 1);            

            lvSearchList.ItemsSource = null;  
        }

        /// <summary>
        /// Displaying All the messages inside this module
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessages(string message)
        {
            AlertBox.Show(message);
        }

        //create a table dynamically  
        private DataTable CreateDataTable()
        {
            DataColumn dtCol;

            dtCol = new DataColumn();
            dtCol.DataType = Type.GetType("System.Int32");
            dtCol.ColumnName = "MemberID";
            dtMember.Columns.Add(dtCol);

            dtCol = new DataColumn();
            dtCol.DataType = Type.GetType("System.String");
            dtCol.ColumnName = "MemberName";
            dtMember.Columns.Add(dtCol);

            dtCol = new DataColumn();
            dtCol.DataType = Type.GetType("System.String");
            dtCol.ColumnName = "ImagePath";
            dtMember.Columns.Add(dtCol);

            return dtMember;
        }

        private void AddDataToTable(Member member, DataTable dtData)
        {
            try
            {
                DataRow row;
                row = dtData.NewRow();
                row["MemberID"] = member.MemberID;
                row["MemberName"] = member.MemberName;
                row["ImagePath"] = member.ImagePath;
                dtData.Rows.Add(row);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }  

        /// <summary>
        /// Fill Member Name
        /// </summary>
        private void FillMember()
        {
            try
            {
                Classes.CommonFunctions.FillMemberList(cboMemberName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        /// <summary>
        /// Search for Dish 
        /// </summary>
        private void FillSearchList()
        {
            string searchString = string.Empty;
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                if (txtSearch.Text.Trim() != string.Empty || cboDishCategory.SelectedIndex > 0)
                {
                    searchString = " 1=1 ";
                    if (txtSearch.Text.Trim() != string.Empty)
                    {
                        searchString = searchString + " AND (DishName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%' ";
                        searchString = searchString + " OR DisplayName LIKE '%" + txtSearch.Text.Trim().Replace("'", "''") + "%') ";
                    }

                    switch (gridid)
                    {
                        case 1:
                            searchString = searchString + " And Keywords like '%" + Enum.GetName(typeof(Enums.MealType), 1) + "%' ";
                            break;
                        case 2:
                            searchString = searchString + " And Keywords like '%" + Enum.GetName(typeof(Enums.MealType), 2) + "%' ";
                            break;
                        case 3:
                            searchString = searchString + " And Keywords like '%" + Enum.GetName(typeof(Enums.MealType), 3) + "%' ";
                            break;
                        case 4:
                            searchString = searchString + " And Keywords like '%" + Enum.GetName(typeof(Enums.MealType), 4) + "%' ";
                            break;
                    }
                    if (cboDishCategory.SelectedIndex > 0)
                    {
                        searchString = searchString + " And DishCategoryID=" + cboDishCategory.SelectedIndex + " ";
                    }
                    
                    searchString = " Where " + searchString + "Order By DishName";
                    dishList = DishManager.GetList(searchString);
                    lvSearchList.ItemsSource = dishList;
                    lvSearchList.SelectedIndex = 0;
                    lvSearchList.ScrollIntoView(lvSearchList.SelectedItem);
                    lvSearchList.Focus();
                }
                else
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1083"), "", AlertType.Information, AlertButtons.OK);
                }
            }
        }

        /// <summary>
        /// Running storyboard according to the Storyboardname 
        /// </summary>
        /// <param name="animationname"></param>
        private void GridAnimation(string animationname)
        {
            Storyboard gridAnimation = (Storyboard)FindResource(animationname);
            gridAnimation.Begin(this);
            txtSearch.Text = string.Empty;
            Keyboard.Focus(cboDishCategory);
            cboDishCategory.SelectedIndex = 0;
            UpdateLayout();
            lvSearchList.ItemsSource = null;
        }

        //private void ListGridAnimation(string AnimatioName)
        //{
        //    ListGrid.Visibility = Visibility.Visible;
        //    if (AnimatioName == "ExpandListGrid")
        //    {
        //        ListGrid.Visibility = Visibility.Visible;
        //        ListGrid.Height = 325;
        //    }
        //    else if (AnimatioName == "CollapseListGrid")
        //    {
        //        ListGrid.Visibility = Visibility.Collapsed;
        //        ListGrid.Height = 0;
        //    }
        //}

        /// <summary>
        /// Running storyboard according to the Storyboardname 
        /// </summary>
        /// <param name="animationname"></param>
        private void GridMemberAnimation(string animationname)
        {
            Storyboard gridAnimation = (Storyboard)FindResource(animationname);
            gridAnimation.Begin(this);            
            lvMemberNutrients.ItemsSource = null;            
        }

        private void ClearEach(ListView lvSetting, int MealType)
        {
            switch (MealType)
            {
                case 1:
                    lvSetting.ItemsSource = null;
                    dtFoodSetting1.Rows.Clear();
                    break;
                case 2:
                    lvSetting.ItemsSource = null;
                    dtFoodSetting2.Rows.Clear();
                    break;
                case 3:
                    lvSetting.ItemsSource = null;
                    dtFoodSetting3.Rows.Clear();
                    break;
                case 4:
                    lvSetting.ItemsSource = null;
                    dtFoodSetting4.Rows.Clear();
                    break;

            }
        }
        
        private int GetMemberAge(DateTime DOB)
        {
            int MemberAge;
            DateTime Now = System.DateTime.Now;
            TimeSpan ts = Now.Subtract(DOB);
            MemberAge = Classes.CommonFunctions.Convert2Int(Convert.ToString(ts.Days)) / (int)DayType.Days_Year;
            return MemberAge;
        }

        private void SetGridComboFocus(ListView lvList,string comboName)
        {
            lvList.SelectedIndex = 0;
            ItemContainerGenerator generator = lvList.ItemContainerGenerator;
            ListViewItem selectedItem = (ListViewItem)generator.ContainerFromIndex(lvList.SelectedIndex);
            ComboBox cbo = ListViewHelper.GetDescendantByType(selectedItem, typeof(ComboBox), comboName) as ComboBox;
            if (cbo != null)
            {
                CommonFunctions.SetControlFocus(cbo);
            }
        }

        private void ShowFilledDates()
        {
            if (cboMemberName.SelectedIndex >= 0)
            {
                dtpSelectDate.SelectedDate = DateTime.Now;
                List<MemberMenuPlanner> memberMenuPlannerList = new List<MemberMenuPlanner>();
                memberMenuPlannerList = MemberMenuPlannerManager.GetFoodSettingWeekDayList(memberID);
                calDishSetting.SelectionMode = Microsoft.Windows.Controls.CalendarSelectionMode.MultipleRange;
                calDishSetting.SelectedDates.Clear();
                foreach (MemberMenuPlanner memberMenuPlanner in memberMenuPlannerList)
                {
                    calDishSetting.SelectedDates.Add(memberMenuPlanner.WeekDay);
                }
            }
        }


        private void SetDates()
        {
            dpFromDate.SelectedDate = (DateTime?)DateTime.Now;
            dpTodate.SelectedDate = (DateTime?)DateTime.Now;
            dpToDateWeek.SelectedDate = (DateTime?)DateTime.Now;
            dpCopyFromDate.SelectedDate = (DateTime?)DateTime.Now;
        }

        private bool ValidateDates()
        {
            if (dpFromDate.SelectedDate != null)
            {
                if (dpTodate.SelectedDate != null)
                {
                    if (!Classes.CommonFunctions.IsDate(Convert.ToString(dpTodate.SelectedDate)))
                    {
                        AlertBox.Show(XMLServices.GetXmlMessage("E1172"), "", AlertType.Information, AlertButtons.OK);
                        dpTodate.Focus();
                        return false;
                    }
                }
                else
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1067"), "", AlertType.Information, AlertButtons.OK);
                    return false;
                }
            }
            else
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1068"), "", AlertType.Information, AlertButtons.OK);
                return false;
            }
            return true;
        }

        private void CopyMealDay()
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                List<MemberMenuPlanner> foodsettingList = new List<MemberMenuPlanner>();
                List<MemberMenuPlanner> CopyedfoodsettingList = new List<MemberMenuPlanner>();
                foodsettingList = MemberMenuPlannerManager.GetFoodSettingDairyList(" WeekDay = DateValue('" + Classes.CommonFunctions.Convert2DateTime(Convert.ToString(dpFromDate.SelectedDate)).ToString("yyyy/MMM/dd") + "') AND MemberID = " + memberID);
                if (foodsettingList.Count > 0)
                {
                    List<MemberMenuPlanner> chkfoodsettingList = new List<MemberMenuPlanner>();
                    chkfoodsettingList = MemberMenuPlannerManager.GetFoodSettingDairyList("WeekDay= DateValue('" + Classes.CommonFunctions.Convert2DateTime(Convert.ToString(dpTodate.SelectedDate)).ToString("yyyy/MMM/dd") + "') AND MemberID = " + memberID);
                    if (chkfoodsettingList.Count > 0)
                    {
                        bool result = AlertBox.Show(XMLServices.GetXmlMessage("E1061"), "", AlertType.Exclamation, AlertButtons.YESNO);
                        if (result == true)
                        {
                            for (int j = 0; j < chkfoodsettingList.Count; j++)
                            {
                                MemberMenuPlannerManager.DeleteFoodSettingDairy("MemberMealPlanID=" + chkfoodsettingList[j].MemberMealPlanID);
                            }
                        }
                        else
                        {
                            AlertBox.Show(XMLServices.GetXmlMessage("E1062"), "", AlertType.Information, AlertButtons.OK);
                            return;
                        }
                    }

                    foreach (MemberMenuPlanner memberMenuPlanner in foodsettingList)
                    {                        
                        MemberMenuPlanner CopyedMemberMenuPlanner = new MemberMenuPlanner();
                        CopyedMemberMenuPlanner.DishCount = memberMenuPlanner.DishCount;
                        CopyedMemberMenuPlanner.DishID = memberMenuPlanner.DishID;
                        CopyedMemberMenuPlanner.MealTypeID = memberMenuPlanner.MealTypeID;
                        CopyedMemberMenuPlanner.MemberID = memberMenuPlanner.MemberID;
                        CopyedMemberMenuPlanner.WeekDay = dpTodate.SelectedDate.Value;
                        CopyedMemberMenuPlanner.PlanWeight = memberMenuPlanner.PlanWeight;
                        MemberMenuPlannerManager.SaveFoodSettingDairy(CopyedMemberMenuPlanner);
                    }
                    
                    AlertBox.Show(XMLServices.GetXmlMessage("E1063"), "", AlertType.Information, AlertButtons.OK);
                    ShowFilledDates();
                }
                else
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1064"), "", AlertType.Information, AlertButtons.OK);
                }
            }
        }

        private void CopyMealWeekDay(ArrayList aDate)
        {
            bool checkMsg = false;
            bool result = false;
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {
                List<MemberMenuPlanner> foodsettingList = new List<MemberMenuPlanner>();
                List<MemberMenuPlanner> CopyedfoodsettingList = new List<MemberMenuPlanner>();
                foodsettingList = MemberMenuPlannerManager.GetFoodSettingDairyList("WeekDay= DateValue('" + Convert.ToDateTime(dpFromDate.SelectedDate).ToString("yyyy/MMM/dd") + "') AND MemberID = " + memberID);
                if (foodsettingList.Count > 0)
                {
                    for (int i = 0; i < aDate.Count; i++)
                    {
                        DateTime dtCopytoDate = Convert.ToDateTime(Convert.ToString(aDate[i]));
                        List<MemberMenuPlanner> chkfoodsettingList = new List<MemberMenuPlanner>();
                        chkfoodsettingList = MemberMenuPlannerManager.GetFoodSettingDairyList("WeekDay= DateValue('" + dtCopytoDate.ToString("yyyy/MMM/dd") + "') AND MemberID = " + memberID);
                        if (chkfoodsettingList.Count > 0)
                        {
                            if (!checkMsg)
                                result = AlertBox.Show(XMLServices.GetXmlMessage("E1061"), "", AlertType.Exclamation, AlertButtons.YESNO);

                            if (result == true)
                            {
                                checkMsg = true;
                                for (int j = 0; j < chkfoodsettingList.Count; j++)
                                {
                                    MemberMenuPlannerManager.DeleteFoodSettingDairy("MemberMealPlanID=" + chkfoodsettingList[j].MemberMealPlanID);
                                }
                            }
                            else
                            {
                                AlertBox.Show(XMLServices.GetXmlMessage("E1062"), "", AlertType.Information, AlertButtons.OK);
                                return;
                            }
                        }

                        foreach (MemberMenuPlanner familyMemberFoodSetting in foodsettingList)
                        {
                            if (CommonFunctions.IsDate(Convert.ToString(aDate[i])))
                            {
                                MemberMenuPlanner CopyedMemberMenuPlanner = new MemberMenuPlanner();
                                CopyedMemberMenuPlanner.DishCount = familyMemberFoodSetting.DishCount;
                                CopyedMemberMenuPlanner.DishID = familyMemberFoodSetting.DishID;
                                CopyedMemberMenuPlanner.MealTypeID = familyMemberFoodSetting.MealTypeID;
                                CopyedMemberMenuPlanner.MemberID = familyMemberFoodSetting.MemberID;
                                CopyedMemberMenuPlanner.WeekDay = dtCopytoDate;
                                CopyedMemberMenuPlanner.PlanWeight = familyMemberFoodSetting.PlanWeight;
                                MemberMenuPlannerManager.SaveFoodSettingDairy(CopyedMemberMenuPlanner);
                            }
                        }
                    }
                    
                    AlertBox.Show(XMLServices.GetXmlMessage("E1063"), "", AlertType.Information, AlertButtons.OK);
                    ShowFilledDates();
                }
                else
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1064"), "", AlertType.Information, AlertButtons.OK);
                }
            }
        }

        private void GetDatesOfWeek(DateTime startDate, DateTime endDate, DayOfWeek firstDay)
        {
            DateTime firstWeekDay = startDate.Date;
            DateTime chkStart = startDate;
            while (startDate.DayOfWeek != firstDay)
            {
                startDate = startDate.AddDays(1);
            }
            firstWeekDay = startDate.Date;
            if (CommonFunctions.IsDate(Convert.ToString(firstWeekDay)))
                if (chkStart != firstWeekDay) // to remove the start date  from the arraylist
                    weekDate.Add(firstWeekDay);

            while (endDate >= startDate)
            {
                startDate = startDate.AddDays(7);
                if (CommonFunctions.IsDate(Convert.ToString(startDate)))
                {
                    if (endDate >= startDate)
                    {
                        weekDate.Add(startDate);
                    }
                }
            }
        }

        private void CopyWeekDates()
        {
            TimeSpan tspan = new TimeSpan();
            tspan = dpToDateWeek.SelectedDate.Value.Subtract(dpFromDate.SelectedDate.Value);
            if (ValidateDateRange())
            {
                CopyMealWeekDay(weekDate);
            }
        }

        private bool ValidateDateRange()
        {
            TimeSpan tspan = new TimeSpan();

            if (dpToDateWeek.SelectedDate != null && dpFromDate.SelectedDate != null)
            {
                if (!Classes.CommonFunctions.IsDate(Convert.ToString(dpToDateWeek.SelectedDate)))
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1172"), "", AlertType.Information, AlertButtons.OK);
                    dpToDateWeek.Focus();
                    return false;
                }
                if (!Classes.CommonFunctions.IsDate(Convert.ToString(dpFromDate.SelectedDate)))
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1173"), "", AlertType.Information, AlertButtons.OK);
                    dpFromDate.Focus();
                    return false;
                }
                if (!Classes.CommonFunctions.IsDate(Convert.ToString(dpToDateWeek.SelectedDate)))
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1174"), "", AlertType.Information, AlertButtons.OK);
                    dpToDateWeek.Focus();
                    return false;
                }                
                tspan = dpToDateWeek.SelectedDate.Value.Subtract(dpFromDate.SelectedDate.Value);
                if (tspan.Days < 0)
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1066"), "", AlertType.Information, AlertButtons.OK);
                    return false;
                }
            }
            else
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1067"), "", AlertType.Information, AlertButtons.OK);
                return false;
            }
            return true;
        }        

        private void FillXMLCombo()
        {
            XMLServices.GetXMLData(cboWeek, new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\XML\\WeekDays.xml"));
        }

        private void FillDetails()
        {
            lblCalorieRequired.Content = Convert.ToString(Math.Round(GetCalorie(memberID), 0));
            lblCalorieRequired.ToolTip = lblCalorieRequired.Content;

            GetMealData();
            
            dtFoodSetting1 = new DataTable();
            dtFoodSetting2 = new DataTable();
            dtFoodSetting3 = new DataTable();
            dtFoodSetting4 = new DataTable();
            for (int i = 1; i < 5; i++)
            {
                FillFoodSetting("MealTypeID=" + Convert.ToString(GetmealtypeID(i)), i);
            }

            FillGrid();

            CalculateCalorieYield(lvsetting1, (int)MealType.CRS_BF);
            CalculateCalorieYield(lvsetting2, (int)MealType.CRS_LNC);
            CalculateCalorieYield(lvsetting3, (int)MealType.CRS_DIN);
            CalculateCalorieYield(lvsetting4, (int)MealType.CRS_SNK);

            CalculateCalorieRequired();
        }

        /// <summary>
        /// Get The mealId from database and return it
        /// </summary>
        /// <param name="gridid"></param>
        /// <returns></returns>
        private int GetmealtypeID(int gridid)
        {
            int mealtypeid = 0;
            string mealtypeName = string.Empty;
            NSysMealType mealtype = new NSysMealType();
            switch (gridid)
            {
                case 1:
                    mealtypeName = Enum.GetName(typeof(Enums.MealType), 1);
                    break;
                case 2:
                    mealtypeName = Enum.GetName(typeof(Enums.MealType), 2);
                    break;
                case 3:
                    mealtypeName = Enum.GetName(typeof(Enums.MealType), 3);
                    break;
                case 4:
                    mealtypeName = Enum.GetName(typeof(Enums.MealType), 4);
                    break;
            }
            mealtype = MealTypeManager.GetItemByCondition("MealTypeName='" + mealtypeName + "'");
            if (mealtype != null)
            {
                mealtypeid = mealtype.MealTypeID;
            }
            return mealtypeid;
        }

        private double GetCalorie(int MemberID)
        {
            int SexID, Age, LifeStyleID, LactationType;
            float HeightinInches, WeightinPounds, LifeStyleValue = 0;
            double CalorieRequired = 0;
            bool IsPregnant, IsLactation;
            try
            {
                Member memberBasic = new Member();
                memberBasic = MemberManager.GetItem(MemberID);
                if (memberBasic != null)
                {
                    SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberBasic.SexID));
                    Age = GetMemberAge(Convert.ToDateTime(memberBasic.DOB));
                    LifeStyleID = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberBasic.LifeStyleID));
                    IsPregnant = Convert.ToBoolean(Convert.ToString(memberBasic.Pregnancy));
                    IsLactation = Convert.ToBoolean(Convert.ToString(memberBasic.Lactation));
                    LactationType = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberBasic.LactationType));

                    HeightinInches = Classes.CommonFunctions.Convert2Float(Convert.ToString((memberBasic.Height) * CalorieFormula.CM_INCH));
                    WeightinPounds = Classes.CommonFunctions.Convert2Float(Convert.ToString((memberBasic.Weight) * CalorieFormula.KG_POUND));
                    if (HeightinInches != 0 && WeightinPounds != 0)
                    {
                        switch (LifeStyleID)
                        {
                            case (int)LifeStyleType.Sedentary:
                                LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Constants.LifeStyle.SEDENTARY));
                                break;
                            case (int)LifeStyleType.LightlyActive:
                                LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Constants.LifeStyle.LIGHTLYACTIVE));
                                break;
                            case (int)LifeStyleType.ModeratelyActive:
                                LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Constants.LifeStyle.MODERATELYACTIVE));
                                break;
                            case (int)LifeStyleType.VeryActive:
                                LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Constants.LifeStyle.VERYACTIVE));
                                break;
                            case (int)LifeStyleType.ExtraActive:
                                LifeStyleValue = Classes.CommonFunctions.Convert2Float(Convert.ToString(Constants.LifeStyle.EXTRAACTIVE));
                                break;
                        }

                        if (SexID == 1)
                        {
                            if (Age > 18)
                            {
                                CalorieRequired = Math.Round((((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEWEIGHT_MEN))) + (HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEHEIGHT_MEN))) + Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIECOMMON_MEN))) - Age * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEAGE_MEN))) * LifeStyleValue, 0);
                            }
                            else
                            {
                                CalorieRequired = Math.Round(CalculateChildrenCalorie(Age, SexID, Classes.CommonFunctions.Convert2Double(Convert.ToString((memberBasic.Weight)))), 0);
                            }
                        }
                        else if (SexID == 2)
                        {
                            if (Age > 18)
                            {
                                CalorieRequired = Math.Round((((WeightinPounds * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEWEIGHT_WOMEN))) + (HeightinInches * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEHEIGHT_WOMEN))) + Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIECOMMON_WOMEN))) - Age * Classes.CommonFunctions.Convert2Float(Convert.ToString(Calorie.CALORIEAGE_WOMEN))) * LifeStyleValue, 0);
                            }
                            else
                            {
                                CalorieRequired = Math.Round(CalculateChildrenCalorie(Age, SexID, Classes.CommonFunctions.Convert2Double(Convert.ToString((memberBasic.Weight)))), 0);
                            }

                            if (IsPregnant)
                            {
                                CalorieRequired = CalorieRequired + (int)CalorieType.CaloriePregnancy;
                            }
                            if (IsLactation)
                            {
                                if (LactationType == 1)
                                    CalorieRequired = CalorieRequired + (int)CalorieType.CalorieLactation1;
                                else if (LactationType == 2)
                                    CalorieRequired = CalorieRequired + (int)CalorieType.CalorieLactation2;
                            }
                        }
                    }
                }
                return (Math.Round(CalorieRequired, 0));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {

            }
        }

        private void GetMealData()
        {
            foodSetting = new DataSet();
            foodSetting = MemberMenuPlannerManager.GetListFoodSettingDairy(" FamilyMemberMealPlan.WeekDay = DateValue('" + ((DateTime)dtpSelectDate.SelectedDate).ToString("yyyy/MMM/dd") + "') AND FamilyMemberMealPlan.MemberID = " + memberID + "");
            //if (foodSetting != null)
            //{
            //    if (foodSetting.Tables.Count > 0)
            //    {
            //        for (int i = 0; i < foodSetting.Tables[0].Rows.Count; i++)
            //        {
            //            foodSetting.Tables[0].Rows[i][(int)DataColumnIndex.dishName] = foodSetting.Tables[0].Rows[i][(int)DataColumnIndex.dishName] + " [" + GetUnitName(Convert.ToInt32(foodSetting.Tables[0].Rows[i][(int)DataColumnIndex.serveUnit])) + "]";
            //            foodSetting.Tables[0].Rows[i][(int)DataColumnIndex.displayName] = foodSetting.Tables[0].Rows[i][(int)DataColumnIndex.displayName] + " [" + GetUnitName(Convert.ToInt32(foodSetting.Tables[0].Rows[i][(int)DataColumnIndex.serveUnit])) + "]";
            //        }
            //    }
            //}
        }

        /// <summary>
        /// Adding the Mealtype into Different Datatables from database
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="GridNo"></param>
        private void FillFoodSetting(string Condition, int GridNo)
        {

            DataRow[] FoodData;
            DataTable dtFoodSetting = (DataTable)this.GetType().InvokeMember("dtFoodSetting" + GridNo, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
            //  dtFoodSetting = new DataTable(); 
            dtFoodSetting.Columns.Add("DishID");
            dtFoodSetting.Columns.Add("DishName");
            dtFoodSetting.Columns.Add("MealTypeID");
            dtFoodSetting.Columns.Add("FamilyID");
            dtFoodSetting.Columns.Add("ServeUnit");
            dtFoodSetting.Columns.Add("WeekDay");
            dtFoodSetting.Columns.Add("DisplayName");
            dtFoodSetting.Columns.Add("PlanWeight");
            dtFoodSetting.Columns.Add("DishCount");

            if (foodSetting != null)
            {
                if (foodSetting.Tables.Count > 0)
                {
                    FoodData = foodSetting.Tables[0].Select(Condition);
                    if (FoodData != null)
                    {
                        for (int j = 0; j < FoodData.Length; j++)
                        {
                            DataRow row = dtFoodSetting.NewRow();
                            for (int i = 0; i < FoodData[j].ItemArray.Length; i++)
                            {
                                row[i] = FoodData[j].ItemArray[i];
                            }
                            dtFoodSetting.Rows.Add(row);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Filling the Meal Grids
        /// </summary>
        private void FillGrid()
        {
            for (int i = 1; i < 5; i++)
            {
                DataTable dtFoodSetting = (DataTable)this.GetType().InvokeMember("dtFoodSetting" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                ListView lvsetting = (ListView)this.GetType().InvokeMember("lvsetting" + i, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                lvsetting.ItemsSource = dtFoodSetting.DefaultView;
            }
        }

        private void CalculateCalorieYield(ListView lvSetting, int MealType)
        {
            int dishID = 0;
            double dishCalorie = 0; double dishQuantity = 0;
            double calorieYield = 0;
            float dishPlanWeight;

            DataTable dtCalorie = (DataTable)this.GetType().InvokeMember("dtCalorie" + MealType, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
            if (dtCalorie.Columns.Count == 0)
            {
                dtCalorie.Columns.Add("DishCount");
            }
            else
            {
                dtCalorie.Rows.Clear();
            }


            for (int k = 0; k < lvSetting.Items.Count; k++)
            {
                dishCalorie = 0;
                dishID = Classes.CommonFunctions.Convert2Int((((DataRowView)lvSetting.Items[k]).Row.ItemArray[(int)DataColumnIndex.dishID]).ToString());
                dishCalorie = DishManager.GetDishCalorie(dishID);
                dishPlanWeight = Classes.CommonFunctions.Convert2Float((((DataRowView)lvSetting.Items[k]).Row.ItemArray[(int)DataColumnIndex.standardWeight]).ToString());
                dishQuantity = Classes.CommonFunctions.Convert2Float((((DataRowView)lvSetting.Items[k]).Row.ItemArray[(int)DataColumnIndex.quantity]).ToString());

                calorieYield = calorieYield + (dishQuantity * dishCalorie * dishPlanWeight);
            }

            DataRow row = dtCalorie.NewRow();
            switch (MealType)
            {
                case 1:
                    lblBreakFastCalorie.Content = Convert.ToString(Math.Round(calorieYield, 0));
                    lblBreakFastCalorie.ToolTip = Convert.ToString(Math.Round(calorieYield, 0));
                    row[0] = Math.Round(calorieYield, 0);
                    break;
                case 2:
                    lblLunchCalorie.Content = Convert.ToString(Math.Round(calorieYield, 0));
                    lblLunchCalorie.ToolTip = Convert.ToString(Math.Round(calorieYield, 0));
                    row[0] = Math.Round(calorieYield, 0);
                    break;
                case 3:
                    lblDinnerCalorie.Content = Convert.ToString(Math.Round(calorieYield, 0));
                    lblDinnerCalorie.ToolTip = Convert.ToString(Math.Round(calorieYield, 0));
                    row[0] = Math.Round(calorieYield, 0);
                    break;
                case 4:
                    lblSnacksCalorie.Content = Convert.ToString(Math.Round(calorieYield, 0));
                    lblSnacksCalorie.ToolTip = Convert.ToString(Math.Round(calorieYield, 0));
                    row[0] = Math.Round(calorieYield, 0);
                    break;
            }           
            dtCalorie.Rows.Add(row);
        }

        private void CheckCalorieIntakeEach(int type)
        {
            double CalorieInTakeTotal = 0, DailyDishCalorie = 0, DifferanceCalorie = 0, CalorieIntakeType = 0;
            string Message = string.Empty;
            string MealType = string.Empty;
            Label lblCalorie = null;
            switch (type)
            {
                case 1:
                    lblCalorie = lblBreakFastCalorie;
                    MealType = "BreakFast";
                    break;
                case 2:
                    lblCalorie = lblLunchCalorie;
                    MealType = "Lunch";
                    break;
                case 3:
                    lblCalorie = lblDinnerCalorie;
                    MealType = "Dinner";
                    break;
                case 4:
                    lblCalorie = lblSnacksCalorie;
                    MealType = "Snacks";
                    break;
            }

            SetLabelStyle(lblCalorie, 1);
            DailyDishCalorie = Math.Round(Convert.ToDouble(lblCalorie.Content), 0);
            CalorieInTakeTotal = Math.Round(Convert.ToDouble(lblCalorieRequired.Content), 0);
            if (CalorieInTakeTotal != 0)
            {
                if (type == 4)
                {
                    CalorieIntakeType = Math.Round(Convert.ToDouble(((CalorieInTakeTotal * .15))), 0);
                }
                else
                {
                    CalorieIntakeType = Math.Round(Convert.ToDouble(((CalorieInTakeTotal * .85) / 3)), 0);
                }

                if (DailyDishCalorie > 0)
                {
                    DifferanceCalorie = Math.Round((DailyDishCalorie - CalorieIntakeType), 0);
                    if (Math.Abs(DifferanceCalorie) > 100)
                    {
                        SetLabelStyle(lblCalorie, 4);
                    }
                    else
                    {
                        SetLabelStyle(lblCalorie, 3);
                    }
                }
            }
        }

        public static double CalculateChildrenCalorie(int Age, int SexID, double Weight)
        {
            int EnergyRequired = 0;
            double CalorieRequired = 0, MeanWeight = 0, EnergyPerKg = 0;

            try
            {
                if (Age > 0)
                {
                    EnergyRequired = CalorieCalculatorManager.GetEnergyRequirement(Age, SexID);
                    MeanWeight = CalorieCalculatorManager.GetMeanWeight(Age, SexID);

                    if (EnergyRequired != 0 && MeanWeight != 0)
                    {
                        EnergyPerKg = EnergyRequired / MeanWeight;
                        CalorieRequired = EnergyPerKg * Weight;
                    }
                }

                return CalorieRequired;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return CalorieRequired;
            }
            finally
            {

            }
        }

        private void CalculateCalorieRequired()
        {
            float dishPlanWeight = 0;
            double dishCalorie = 0;
            double calorieReq;
            double dishQuantity = 0;
            int dishID = 0;

            if (dtCalorieTotal.Columns.Count == 0)
            {
                dtCalorieTotal.Columns.Add("DishCount");
            }
            else
            {
                dtCalorieTotal.Rows.Clear();
            }


            calorieReq = 0;

            DataRow row = dtCalorieTotal.NewRow();
            for (int j = 1; j < 5; j++)
            {
                ListView lvsetting = (ListView)this.GetType().InvokeMember("lvsetting" + j, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                DataTable dtFoodSetting = (DataTable)this.GetType().InvokeMember("dtFoodSetting" + j, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                for (int k = 0; k < lvsetting.Items.Count; k++)
                {

                    dishCalorie = 0;
                    dishID = Classes.CommonFunctions.Convert2Int((((DataRowView)lvsetting.Items[k]).Row.ItemArray[(int)DataColumnIndex.dishID]).ToString());
                    dishPlanWeight = Classes.CommonFunctions.Convert2Int((((DataRowView)lvsetting.Items[k]).Row.ItemArray[(int)DataColumnIndex.standardWeight]).ToString());
                    dishCalorie = DishManager.GetDishCalorie(dishID);
                    dishQuantity = Classes.CommonFunctions.Convert2Float((((DataRowView)lvsetting.Items[k]).Row.ItemArray[(int)DataColumnIndex.quantity]).ToString());

                    calorieReq = calorieReq + (dishQuantity * dishCalorie * dishPlanWeight);
                }
            }

            lblCalorieIntake.Content = Convert.ToString(Math.Round(calorieReq, 0));
            lblCalorieIntake.ToolTip = Convert.ToString(Math.Round(calorieReq, 0));
            row[0] = Math.Round(calorieReq, 0);

            dtCalorieTotal.Rows.Add(row);
        }

        private bool CheckCalorieIntake(bool IsMessage, bool IsSave)
        {
            double CalorieInTake = 0, DailyDishCalorie = 0, DifferanceCalorie = 0;
            string lowCalorieMessage = string.Empty;
            string highCalorieMessage = string.Empty;
            string Message = string.Empty;
                       
            DailyDishCalorie = Math.Round(Convert.ToDouble(lblCalorieIntake.Content), 0);
            CalorieInTake = Math.Round(Convert.ToDouble(lblCalorieRequired.Content), 0);
            if (DailyDishCalorie > 0)
            {
                DifferanceCalorie = Math.Round((DailyDishCalorie - CalorieInTake), 0);

                if (DifferanceCalorie > 0)
                {
                    if (CalorieInTake <= 0)
                    {
                        SetLabelStyle(lblCalorieIntake, 2);
                    }
                    else if (Math.Abs(DifferanceCalorie) > 1000)
                    {
                        if (Math.Abs(DailyDishCalorie) > 5000)
                        {
                            SetLabelStyle(lblCalorieIntake, 4);
                            highCalorieMessage = highCalorieMessage + memberID + ", ";
                        }
                        else
                        {
                            SetLabelStyle(lblCalorieIntake, 4);
                            Message = Message + memberID + ", ";
                        }
                    }
                    else
                    {
                        SetLabelStyle(lblCalorieIntake, 3);
                    }
                }
                else if (DifferanceCalorie == 0)
                {

                }
                else if (DifferanceCalorie < 0)
                {
                    if (CalorieInTake <= 0)
                    {
                        SetLabelStyle(lblCalorieIntake, 2);
                    }
                    else if (Math.Abs(DifferanceCalorie) > 500)
                    {
                        if (Math.Abs(DailyDishCalorie) < 1000)
                        {
                            SetLabelStyle(lblCalorieIntake, 4);
                            lowCalorieMessage = lowCalorieMessage + memberID + ", ";
                        }
                        else
                        {
                            SetLabelStyle(lblCalorieIntake, 4);
                            Message = Message + memberID + ", ";
                        }
                    }
                    else
                    {
                        SetLabelStyle(lblCalorieIntake, 3);
                    }
                }
            }


            if (IsSave)
            {
                if (lowCalorieMessage.Length > 0)
                {
                    Message = "Dish Calorie is too low for " + System.Environment.NewLine + lowCalorieMessage.Substring(0, lowCalorieMessage.Length - 2);
                    if (highCalorieMessage.Length > 0)
                    {
                        Message = Message + System.Environment.NewLine + "Dish Calorie is too high for " + System.Environment.NewLine + highCalorieMessage.Substring(0, highCalorieMessage.Length - 2);
                    }
                    return AlertBox.Show(Message + System.Environment.NewLine + "Do you want to continue?", "", AlertType.Exclamation, AlertButtons.YESNO);
                }
                else if (highCalorieMessage.Length > 0)
                {
                    Message = Message + System.Environment.NewLine + "Dish Calorie is too high for " + System.Environment.NewLine + highCalorieMessage.Substring(0, highCalorieMessage.Length - 2);
                    return AlertBox.Show(Message + System.Environment.NewLine + "Do you want to continue?", "", AlertType.Exclamation, AlertButtons.YESNO);
                }
                else if (Message.Length > 0)
                {
                    return AlertBox.Show(XMLServices.GetXmlMessage("E1198") + System.Environment.NewLine + Message.Substring(0, Message.Length - 2) + System.Environment.NewLine + "Do you want to continue?", "", AlertType.Exclamation, AlertButtons.YESNO);
                }
            }
            else if (IsMessage)
            {
                if (Message.Length > 0)
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1198") + System.Environment.NewLine + Message.Substring(0, Message.Length - 2), "", AlertType.Information, AlertButtons.OK);
                }
            }
            return true;
        }

        public void UpdateDishList(int gridID)
        {
            try
            {
                if (searchDishID.Count > 0)
                {
                    for (int i = 0; i < searchDishID.Count; i++)
                    {
                        GetDishDetails(Classes.CommonFunctions.Convert2Int(Convert.ToString(searchDishID[i])));
                    }
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

        /// <summary>
        /// get the Name of Unit using Unit ID
        /// </summary>
        /// <param name="unitID"></param>
        /// <returns></returns>
        private string GetUnitName(int unitID)
        {
            string strunit = string.Empty;
            if (dtableUnit != null)
            {
                if (unitID > 0)
                {
                    DataRow[] rowcol = dtableUnit.Select("ID=" + unitID);
                    strunit = Convert.ToString(rowcol[0].ItemArray[1]);
                }
                else
                {
                    strunit = string.Empty;
                }
            }
            return strunit;
        }

        /// <summary>
        /// Adding the Dish into the Meal Grids
        /// </summary>
        /// <param name="dishid"></param>
        /// <returns></returns>
        private bool GetDishDetails(int dishid)
        {
            BONutrition.Dish dish = new BONutrition.Dish();
            dish = DishManager.GetItem(dishid, " 1=1 ");
            DataRow row;
            DataRow[] foundRows;
            switch (gridid)
            {
                case 1:
                    foundRows = dtFoodSetting1.Select("DishID='" + dishid + "'");
                    if (foundRows.Length > 0)
                    {
                        return true;
                    }
                    row = dtFoodSetting1.NewRow();
                    row[(int)DataColumnIndex.dishID] = dish.Id;
                    row[(int)DataColumnIndex.dishName] = dish.Name + " [" + GetUnitName(Convert.ToInt32(dish.ServeUnit)) + "]"; //+" = " + dish.StandardWeight + " gm";                        
                    row[(int)DataColumnIndex.displayName] = dish.DisplayName + " [" + GetUnitName(Convert.ToInt32(dish.ServeUnit)) + "]"; //+" = " + dish.StandardWeight + " gm";
                    row[(int)DataColumnIndex.standardWeight] = dish.StandardWeight;
                    dtFoodSetting1.Rows.Add(row);
                    break;
                case 2:
                    foundRows = dtFoodSetting2.Select("DishID='" + dishid + "'");
                    if (foundRows.Length > 0)
                    {
                        return true;
                    }
                    row = dtFoodSetting2.NewRow();
                    row[(int)DataColumnIndex.dishID] = dish.Id;
                    row[(int)DataColumnIndex.dishName] = dish.Name + " [" + GetUnitName(Convert.ToInt32(dish.ServeUnit)) + "]"; //+" = " + dish.StandardWeight + " gm";
                    row[(int)DataColumnIndex.displayName] = dish.DisplayName + " [" + GetUnitName(Convert.ToInt32(dish.ServeUnit)) + "]"; //+" = " + dish.StandardWeight + " gm";
                    row[(int)DataColumnIndex.standardWeight] = dish.StandardWeight;
                    dtFoodSetting2.Rows.Add(row);
                    break;
                case 3:
                    foundRows = dtFoodSetting3.Select("DishID='" + dishid + "'");
                    if (foundRows.Length > 0)
                    {
                        return true;
                    }
                    row = dtFoodSetting3.NewRow();
                    row[(int)DataColumnIndex.dishID] = dish.Id;
                    row[(int)DataColumnIndex.dishName] = dish.Name + " [" + GetUnitName(Convert.ToInt32(dish.ServeUnit)) + "]"; //+" = " + dish.StandardWeight + " gm";
                    row[(int)DataColumnIndex.displayName] = dish.DisplayName + " [" + GetUnitName(Convert.ToInt32(dish.ServeUnit)) + "]"; //+" = " + dish.StandardWeight + " gm";
                    row[(int)DataColumnIndex.standardWeight] = dish.StandardWeight;
                    dtFoodSetting3.Rows.Add(row);
                    break;
                case 4:
                    foundRows = dtFoodSetting4.Select("DishID='" + dishid + "'");
                    if (foundRows.Length > 0)
                    {
                        return true;
                    }
                    row = dtFoodSetting4.NewRow();
                    row[(int)DataColumnIndex.dishID] = dish.Id;
                    row[(int)DataColumnIndex.dishName] = dish.Name + " [" + GetUnitName(Convert.ToInt32(dish.ServeUnit)) + "]"; //+" = " + dish.StandardWeight + " gm";
                    row[(int)DataColumnIndex.displayName] = dish.DisplayName + " [" + GetUnitName(Convert.ToInt32(dish.ServeUnit)) + "]"; //+" = " + dish.StandardWeight + " gm";
                    row[(int)DataColumnIndex.standardWeight] = dish.StandardWeight;
                    dtFoodSetting4.Rows.Add(row);
                    break;
            }
            //FillGrid();
            return false;
        }

        private void PopulatePlanCombo(int gridNo)
        {
            List<MemberMenuPlanner> menuPlannerList = new List<MemberMenuPlanner>();
            switch (gridNo)
            {
                case 1:
                    gvColBreakFastPlan.CellTemplate = this.FindResource("planBreakFastTemplate") as DataTemplate;
                    LoadPlan(gridNo, lvsetting1);
                    break;
                case 2:
                    gvColLunchPlan.CellTemplate = this.FindResource("planLunchTemplate") as DataTemplate;
                    LoadPlan(gridNo, lvsetting2);
                    break;
                case 3:
                    gvColDinnerPlan.CellTemplate = this.FindResource("planDinnerTemplate") as DataTemplate;
                    LoadPlan(gridNo, lvsetting3);
                    break;
                case 4:
                    gvColSnacksPlan.CellTemplate = this.FindResource("planSnacksTemplate") as DataTemplate;
                    LoadPlan(gridNo, lvsetting4);
                    break;
            }
        }

        private void LoadPlan(int index, ListView lvSettings)
        {
            List<MemberMenuPlanner> menuPlannerList = new List<MemberMenuPlanner>();
            for (int rowIndex = 0; rowIndex < lvSettings.Items.Count; rowIndex++)
            {
                if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvSettings, (int)ListViewIndex.Plan, rowIndex, "cboPlan" + index))) != null)
                {
                    if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvSettings, (int)ListViewIndex.Plan, rowIndex, "cboPlan" + index))).Items.Count <= 0)
                    {
                        int dishID = CommonFunctions.Convert2Int(Convert.ToString(((DataRowView)lvSettings.Items[rowIndex]).Row.ItemArray[(int)DataColumnIndex.dishID]));
                        menuPlannerList = MemberMenuPlannerManager.GetFoodPlanList(dishID);
                        if (menuPlannerList != null)
                        {
                            ((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvSettings, (int)ListViewIndex.Plan, rowIndex, "cboPlan" + index))).ItemsSource = menuPlannerList;
                            if (((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvSettings, (int)ListViewIndex.Plan, rowIndex, "cboPlan" + index))).SelectedIndex < 0)
                            {
                                ((ComboBox)(ListViewHelper.GetElementFromCellTemplate(lvSettings, (int)ListViewIndex.Plan, rowIndex, "cboPlan" + index))).SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
        }

        private void SaveMenuPlanner()
        {
            CalculateCalorieYield(lvsetting1, (int)MealType.CRS_BF);
            CalculateCalorieYield(lvsetting2, (int)MealType.CRS_LNC);
            CalculateCalorieYield(lvsetting3, (int)MealType.CRS_DIN);
            CalculateCalorieYield(lvsetting4, (int)MealType.CRS_SNK);
            CalculateCalorieRequired();

            if (CheckCalorieIntake(true, true))
            {
                MemberMenuPlannerManager.DeleteFoodSettingDairy(" WeekDay = DateValue('" + ((DateTime)dtpSelectDate.SelectedDate).ToString("yyyy/MM/dd") + "') AND MemberID = "+ memberID +"");
                for (int j = 1; j < 5; j++)
                {
                    ListView lvsetting = (ListView)this.GetType().InvokeMember("lvsetting" + j, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                    DataTable dtFoodSetting = (DataTable)this.GetType().InvokeMember("dtFoodSetting" + j, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField, null, this, null);
                    for (int k = 0; k < lvsetting.Items.Count; k++)
                    {
                        MemberMenuPlanner memberMenu = new MemberMenuPlanner();
                        memberMenu.DishID = CommonFunctions.Convert2Int(Convert.ToString((((DataRowView)lvsetting.Items[k]).Row.ItemArray[(int)DataColumnIndex.dishID])));
                        memberMenu.MealTypeID = GetmealtypeID(j);
                        memberMenu.MemberID = memberID;
                        DateTime dtWeekdate = (DateTime)dtpSelectDate.SelectedDate;
                        dtWeekdate = new DateTime(dtWeekdate.Year, dtWeekdate.Month, dtWeekdate.Day);
                        memberMenu.WeekDay = dtWeekdate;
                        memberMenu.PlanWeight = Classes.CommonFunctions.Convert2Float(Convert.ToString((((DataRowView)lvsetting.Items[k]).Row.ItemArray[(int)DataColumnIndex.standardWeight])));
                        memberMenu.DishCount = Classes.CommonFunctions.Convert2Float(Convert.ToString((((DataRowView)lvsetting.Items[k]).Row.ItemArray[(int)DataColumnIndex.quantity])));
                        MemberMenuPlannerManager.SaveFoodSettingDairy(memberMenu);
                    }
                }
                AlertBox.Show(XMLServices.GetXmlMessage("E1088"), "", AlertType.Information, AlertButtons.OK);
            }
        }

        #endregion
     
        #region Events

        public MenuPlanner()
        {
            InitializeComponent();            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (OverrideCursor cursor = new OverrideCursor(Cursors.Wait))
            {                
                App apps = (App)Application.Current;
                ((NutritionV1.MasterPage)(Window.GetWindow(this))).mnuTop.Visibility = Visibility.Visible;
                SetCulture();

                FillXMLCombo();
                SetDates();                

                LoadTemplate();
                ClearAll();
                Classes.CommonFunctions.FillDishCategory(cboDishCategory);
                FillMember();
                try
                {
                    dtableUnit = XMLServices.GetXMLData(AppDomain.CurrentDomain.BaseDirectory + "\\XML\\ServeUnit.xml", 3);
                }
                catch (Exception ex)
                {                    
                    MessageBox.Show(ex.Message);
                }

                dtpSelectDate.SelectedDate = DateTime.Now;
                FillDetails();
                CheckCalorieIntake(false, false);
            }
        }

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Source is ListView)
            {
                if (e.Key == Key.Return)
                {
                    btnAdd_Click(this, e);
                    e.Handled = true;
                }
            }
        }

        private void txtNumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
              Classes.CommonFunctions.FilterNumeric(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }       

        private void txtQuantity1_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void txtQuantity2_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvsetting2.SelectedIndex = lvsetting2.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void txtQuantity3_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvsetting3.SelectedIndex = lvsetting3.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void txtQuantity4_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvsetting4.SelectedIndex = lvsetting4.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            FillSearchList();
        }
        
        private void imgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GridAnimation("CollapseGrid");
            visibleFlag = 0;
        }

        private void imgMemberClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GridMemberAnimation("CollapseMemberGrid");
            visibleFlag = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
                if (lvi != null)
                {
                    lvSearchList.SelectedIndex = lvSearchList.ItemContainerGenerator.IndexFromContainer(lvi);
                    if (lvSearchList.SelectedIndex >= 0)
                    {
                        int CurrentDishID = ((Dish)lvSearchList.Items[lvSearchList.SelectedIndex]).Id;
                        /* if (GetDishDetails(CurrentDishID) == false)
                        {
                            // GridAnimation("CollapseGrid");
                        }
                        else
                        {
                            AlertBox.Show(XMLServices.GetXmlMessage("E1090"), "", AlertType.Information, AlertButtons.OK);
                        }*/
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void GridSearch_Enter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                if (lvSearchList.SelectedIndex >= 0)
                {
                    int CurrentDishID = ((Dish)lvSearchList.Items[lvSearchList.SelectedIndex]).Id;
                    /*if (GetDishDetails(CurrentDishID) == false)
                    {

                    }
                    else
                    {
                        AlertBox.Show(XMLServices.GetXmlMessage("E1090"), "", AlertType.Information, AlertButtons.OK);
                    }*/
                }
            }
        }

        private void btnAddBreakFast_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gridid = 1;
            visibleFlag = 1;
            searchDishID.Clear();

            BrowseDish objSearchDish = new BrowseDish();
            objSearchDish.FormType = (int)SearchDishType.MealPlanner;
            objSearchDish.Owner = Application.Current.MainWindow;
            objSearchDish.ShowDialog();

            UpdateDishList(gridid);
            SetGridComboFocus(lvsetting1, "cboPlan1");
        }

        private void btnAddLunch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gridid = 2;
            visibleFlag = 1;

            searchDishID.Clear();

            BrowseDish objSearchDish = new BrowseDish();
            objSearchDish.FormType = (int)SearchDishType.MealPlanner;
            objSearchDish.Owner = Application.Current.MainWindow;
            objSearchDish.ShowDialog();

            UpdateDishList(gridid);
            SetGridComboFocus(lvsetting2, "cboPlan2");
        }

        private void btnAddDinner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gridid = 3;
            visibleFlag = 1;

            searchDishID.Clear();

            BrowseDish objSearchDish = new BrowseDish();
            objSearchDish.FormType = (int)SearchDishType.MealPlanner;
            objSearchDish.Owner = Application.Current.MainWindow;
            objSearchDish.ShowDialog();

            UpdateDishList(gridid);
            SetGridComboFocus(lvsetting3, "cboPlan3");
        }

        private void btnAddSnacks_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gridid = 4;
            visibleFlag = 1;

            searchDishID.Clear();

            BrowseDish objSearchDish = new BrowseDish();
            objSearchDish.FormType = (int)SearchDishType.MealPlanner;
            objSearchDish.Owner = Application.Current.MainWindow;
            objSearchDish.ShowDialog();

            UpdateDishList(gridid);
            SetGridComboFocus(lvsetting4, "cboPlan4");  
        }

        private void btnMemberDetails_MouseDown(object sender, MouseButtonEventArgs e)
        {
            visibleFlag = 2;
            GridMemberAnimation("ExpandMemberGrid");
        }

        private void btnRemBreakFast_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gridid = 1;
            if (lvsetting1.SelectedIndex >= 0)
            {
                dtFoodSetting1.Rows[lvsetting1.SelectedIndex].Delete();
                CalculateCalorieYield(lvsetting1, (int)MealType.CRS_BF);
                CalculateCalorieRequired();
                CheckCalorieIntake(false, false);
                CheckCalorieIntakeEach((int)MealType.CRS_BF);
            }
        }

        private void btnRemLunch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gridid = 2;
            if (lvsetting2.SelectedIndex >= 0)
            {
                dtFoodSetting2.Rows[lvsetting2.SelectedIndex].Delete();
                CalculateCalorieYield(lvsetting1, (int)MealType.CRS_LNC);
                CalculateCalorieRequired();
                CheckCalorieIntake(false, false);
                CheckCalorieIntakeEach((int)MealType.CRS_LNC);
            }
        }

        private void btnRemDinner_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gridid = 3;
            if (lvsetting3.SelectedIndex >= 0)
            {
                dtFoodSetting3.Rows[lvsetting3.SelectedIndex].Delete();
                CalculateCalorieYield(lvsetting1, (int)MealType.CRS_DIN);
                CalculateCalorieRequired();
                CheckCalorieIntake(false, false);
                CheckCalorieIntakeEach((int)MealType.CRS_DIN);
            }
        }

        private void btnRemSnacks_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gridid = 4;
            if (lvsetting4.SelectedIndex >= 0)
            {
                dtFoodSetting4.Rows[lvsetting4.SelectedIndex].Delete();
                CalculateCalorieYield(lvsetting1, (int)MealType.CRS_SNK);
                CalculateCalorieRequired();
                CheckCalorieIntake(false, false);
                CheckCalorieIntakeEach((int)MealType.CRS_SNK);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (memberID <= 0)
            {
                AlertBox.Show("Please select a Member", "", AlertType.Information, AlertButtons.OK);
                cboMemberName.Focus();
                return;
            }

            SaveMenuPlanner();
        }
       
        private void cboDishCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void lblSnacksNutrient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void lblLunchNutrient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void lblDinnerNutrient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void lblBreakfastNutrient_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void imgShowLunchNutrition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void imgShowBFNutrition_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgShowDinnerNutrition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void imgShowSnacksNutrition_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }        

        private void Page_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (visibleFlag == 1)
                {
                    visibleFlag = 0;
                    GridAnimation("CollapseGrid");
                }
                else if (visibleFlag == 2)
                {
                    visibleFlag = 0;
                    GridMemberAnimation("CollapseMemberGrid");
                }
                //else if (visibleFlag == 3)
                //{
                //    visibleFlag = 0;
                //    ListGridAnimation("CollapseListGrid");
                //}
            }
        }

        private void chkProfileFilter_Checked(object sender, RoutedEventArgs e)
        {
            FillSearchList();
        }

        private void chkProfileFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            FillSearchList();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FillSearchList();
            }
        }

        private void btnCalculateNutrients_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnSave.Focus();

            CalculateCalorieYield(lvsetting1, (int)MealType.CRS_BF);
            CalculateCalorieYield(lvsetting2, (int)MealType.CRS_LNC);
            CalculateCalorieYield(lvsetting3, (int)MealType.CRS_DIN);
            CalculateCalorieYield(lvsetting4, (int)MealType.CRS_SNK);

            CalculateCalorieRequired();

            CheckCalorieIntakeEach((int)MealType.CRS_BF);
            CheckCalorieIntakeEach((int)MealType.CRS_LNC);
            CheckCalorieIntakeEach((int)MealType.CRS_DIN);
            CheckCalorieIntakeEach((int)MealType.CRS_SNK);

            CheckCalorieIntake(false, false);
        }        
        
        private void lblBreakfastClear_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lvsetting1.Items.Count > 0)
            {
                bool result = AlertBox.Show(XMLServices.GetXmlMessage("E1200"), "", AlertType.Exclamation, AlertButtons.YESNO);
                if (result == true)
                {
                    btnSave.Focus();
                    ClearEach(lvsetting1, (int)MealType.CRS_BF);
                }
            }
            else
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1086"), "", AlertType.Information, AlertButtons.OK);
            }
        }

        private void lblLunchClear_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lvsetting2.Items.Count > 0)
            {
                bool result = AlertBox.Show(XMLServices.GetXmlMessage("E1201"), "", AlertType.Exclamation, AlertButtons.YESNO);
                if (result == true)
                {
                    btnSave.Focus();
                    ClearEach(lvsetting2, (int)MealType.CRS_LNC);
                }
            }
            else
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1086"), "", AlertType.Information, AlertButtons.OK);
            }
        }

        private void lblDinnerClear_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lvsetting3.Items.Count > 0)
            {
                bool result = AlertBox.Show(XMLServices.GetXmlMessage("E1202"), "", AlertType.Exclamation, AlertButtons.YESNO);
                if (result == true)
                {
                    btnSave.Focus();
                    ClearEach(lvsetting3, (int)MealType.CRS_DIN);
                }
            }
            else
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1086"), "", AlertType.Information, AlertButtons.OK);
            }
        }

        private void lblSnacksClear_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lvsetting4.Items.Count > 0)
            {
                bool result = AlertBox.Show(XMLServices.GetXmlMessage("E1203"), "", AlertType.Exclamation, AlertButtons.YESNO);
                if (result == true)
                {
                    btnSave.Focus();
                    ClearEach(lvsetting4, (int)MealType.CRS_SNK);
                }
            }
            else
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1086"), "", AlertType.Information, AlertButtons.OK);
            }
        }           

        private void cboPlan1_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvsetting1.SelectedIndex = lvsetting1.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void cboPlan2_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvsetting2.SelectedIndex = lvsetting2.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void cboPlan3_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvsetting3.SelectedIndex = lvsetting3.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void cboPlan4_GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem lvi = CommonFunctions.GetAncestorByType(e.OriginalSource as DependencyObject, typeof(ListViewItem)) as ListViewItem;
            if (lvi != null)
            {
                lvsetting4.SelectedIndex = lvsetting4.ItemContainerGenerator.IndexFromContainer(lvi);
            }
        }

        private void lvsetting1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.Source is ListView)
            {
                PopulatePlanCombo(1);
            }
        }

        private void lvsetting2_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.Source is ListView)
            {
                PopulatePlanCombo(2);
            }
        }

        private void lvsetting3_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.Source is ListView)
            {
                PopulatePlanCombo(3);
            }
        }

        private void lvsetting4_GotFocus(object sender, RoutedEventArgs e)
        {
            if (e.Source is ListView)
            {
                PopulatePlanCombo(4);
            }
        }

        private void imgPrint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReportViewer dishReport = new ReportViewer();
            dishReport.MenuDate = Convert.ToString(dtpSelectDate.SelectedDate); //Convert.ToString(lblSelectDate.Content);
            dishReport.MemberName = Convert.ToString(cboMemberName.SelectedItem);
            dishReport.FamilyID = familyId;
            dishReport.BreakFast = dtFoodSetting1;
            dishReport.Lunch = dtFoodSetting2;
            dishReport.Dinner = dtFoodSetting3;
            dishReport.Snacks = dtFoodSetting4;
            dishReport.BreakFastCalorie = dtCalorie1;
            dishReport.LunchCalorie = dtCalorie2;
            dishReport.DinnerCalorie = dtCalorie3;
            dishReport.SnacksCalorie = dtCalorie4;
            dishReport.TotalCalorie = dtCalorieTotal;
            dishReport.ReportType = (int)ReportItem.Menu;
            dishReport.Owner = Application.Current.MainWindow;
            dishReport.ShowDialog();
        }
      
        private void txtDate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                CommonFunctions.FilterNumericDate(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dpCopyFromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpFromDate.SelectedDate != null)
            {
                dtpSelectDate.SelectedDate = dpFromDate.SelectedDate;
                txtWeekday.Text = dpFromDate.SelectedDate.Value.DayOfWeek.ToString();
                cboWeek.Text = txtWeekday.Text;
            }
        }

        private void btnCopyDay_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateDates())
            {
                CopyMealDay();
            }
        }

        private void dpFromDate_LostFocus(object sender, RoutedEventArgs e)
        {
            txtWeekday.Text = dpFromDate.SelectedDate.Value.DayOfWeek.ToString();
            cboWeek.Text = txtWeekday.Text;
        }

        private void btnCopyWeek_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan tspan = dpToDateWeek.SelectedDate.Value.Subtract(dpFromDate.SelectedDate.Value);
            if (tspan.Days >= 365)
            {
                AlertBox.Show(XMLServices.GetXmlMessage("E1097"), "", AlertType.Information, AlertButtons.OK);
            }
            else
            {
                switch (cboWeek.SelectedIndex)
                {
                    case 1:
                        GetDatesOfWeek(dpFromDate.SelectedDate.Value, dpToDateWeek.SelectedDate.Value, DayOfWeek.Sunday);
                        break;
                    case 2:
                        GetDatesOfWeek(dpFromDate.SelectedDate.Value, dpToDateWeek.SelectedDate.Value, DayOfWeek.Monday);
                        break;
                    case 3:
                        GetDatesOfWeek(dpFromDate.SelectedDate.Value, dpToDateWeek.SelectedDate.Value, DayOfWeek.Tuesday);
                        break;
                    case 4:
                        GetDatesOfWeek(dpFromDate.SelectedDate.Value, dpToDateWeek.SelectedDate.Value, DayOfWeek.Wednesday);
                        break;
                    case 5:
                        GetDatesOfWeek(dpFromDate.SelectedDate.Value, dpToDateWeek.SelectedDate.Value, DayOfWeek.Thursday);
                        break;
                    case 6:
                        GetDatesOfWeek(dpFromDate.SelectedDate.Value, dpToDateWeek.SelectedDate.Value, DayOfWeek.Friday);
                        break;
                    case 7:
                        GetDatesOfWeek(dpFromDate.SelectedDate.Value, dpToDateWeek.SelectedDate.Value, DayOfWeek.Saturday);
                        break;
                }
                CopyWeekDates();
            }
        }

        private void txtMemberSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                //txtMemberSearch.Text = string.Empty;
            }
        }

        private void cboMemberName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboMemberName.SelectedIndex > 0)
            {
                memberID = CommonFunctions.Convert2Int(Convert.ToString(cboMemberName.SelectedValue));
                ShowFilledDates();
                LoadTemplate();
                FillDetails();
            }
        }

        private void txtMemberSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //DataView dtView = dtMember.DefaultView;
            ////dtView.RowFilter = " MemberName LIKE '" + txtMemberSearch.Text + "%'";
            //DataTable dtFilter = dtView.ToTable();
            //lvMemberName.ItemsSource = dtFilter.DefaultView;
            //lvMemberName.SelectedIndex = 0;

            //visibleFlag = 3;
            //ListGridAnimation("ExpandListGrid");
        }

        private void dtpSelectDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtpSelectDate.SelectedDate != null)
            {
                dtpSelectDate.SelectedDate = (DateTime)dtpSelectDate.SelectedDate;
                dpFromDate.SelectedDate = dtpSelectDate.SelectedDate;
                LoadTemplate();
                FillDetails();
            }
        }
       
        //private void lvMemberName_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    visibleFlag = 0;
        //    ListGridAnimation("CollapseListGrid");
        //}
        
        #endregion                              
               
    }
}
