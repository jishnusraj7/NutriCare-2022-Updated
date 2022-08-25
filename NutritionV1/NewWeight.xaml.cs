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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Transactions;
using System.Resources;
using BONutrition;
using BLNutrition;
using Indocosmo.Framework.CommonManagement;
using Indocosmo.Framework.ExceptionManagement;
using NutritionV1.Common.Classes;
using System.Windows.Threading;
using System.Windows.Controls.DataVisualization.Charting;
using Visifire.Charts;
using Visifire.Commons;
using System.IO;
using Microsoft.Win32;
using System.Configuration;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for NewWeight.xaml
    /// </summary>
    public partial class NewWeight : Window
    {
        #region Declarations

        Member member = new Member();
        int LanguageID = ResourceSetting.defaultlanguage;
        public static int memberID;
        public static int familyID;
        public bool IsDataSaved = false;

        MemberProfile objMemberProfile;

        Visifire.Charts.Chart chart;
        SaveFileDialog SaveDlg = new SaveFileDialog();

        #endregion

        #region Constructor

        public NewWeight(MemberProfile MemberProfile)
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
            objMemberProfile = MemberProfile;
        }

        #endregion

        #region MemberClinical

        private void FillWeightItem(int FamilyID, int MemberID, string ParameterName, DateTime TestDate)
        {
            double Value = 0;
            try
            {
                Value = MemberManager.GetItemFamilyHistory(FamilyID, MemberID, ParameterName, TestDate);
                if (Value != 0)
                {
                    txtWeight.Text = Convert.ToString(Value);
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

        private void FillFamilyMedical(int FamilyID, int MemberID)
        {
            try
            {
                FillWeightHistory(FamilyID, MemberID, "Weight");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
        private void CloseOnEscape(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void FillWeightHistory(int FamilyID, int MemberID, string ParameterName)
        {
            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetListFamilyHistory(FamilyID, MemberID, ParameterName);
                if (memberList.Count > 0)
                {
                    lvWeightHistory.ItemsSource = memberList;
                }
                else if (memberList.Count == 0)
                {
                    lvWeightHistory.ItemsSource = null;
                    txtWeight.Text = string.Empty;
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

        public bool SaveFamilyMedical(int FamilyID, int MemberID)
        {
            try
            {
                if (Validate() == true)
                {
                    SaveWeightHistory(FamilyID, MemberID, "Weight");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            finally
            {

            }
        }

        public void SaveWeightHistory(int FamilyID, int MemberID, string ParameterName)
        {
            try
            {
                member.FamilyID = FamilyID;
                member.MemberID = MemberID;
                member.ParameterName = ParameterName;
                member.ParameterValue = Classes.CommonFunctions.Convert2Float(txtWeight.Text.Trim());
                member.ModifiedDate = Convert.ToDateTime(dtpTestDate.SelectedDate).Date;
                if (txtWeight.Text.Trim() != "0" && txtWeight.Text.Trim() != string.Empty)
                {
                    MemberManager.SaveFamilyHistory(member);
                    SetLatestWeight();
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

        public void SetLatestWeight()
        {
            double LatestWeight;

            try
            {
                LatestWeight = MemberManager.GetLatestWeight(MemberID, "Weight");
                MemberManager.UpdateFamilyGeneral(MemberID, LatestWeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }


        public int GetMemberAge()
        {
            int MemberAge = 0;
            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetListFamilyMember(FamilyID, MemberID);

                if (memberList.Count > 0)
                {
                    DateTime Now = System.DateTime.Now;
                    DateTime DOB = Classes.CommonFunctions.Convert2DateTime(Convert.ToString(Convert.ToDateTime(memberList[0].DOB)));

                    TimeSpan ts = Now.Subtract(DOB);
                    MemberAge = Classes.CommonFunctions.Convert2Int(Convert.ToString(ts.Days)) / (int)DayType.Days_Year;
                }

                return MemberAge;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return MemberAge;
            }
            finally
            {

            }
        }

        public int GetMemberSex()
        {
            int SexID = 0;
            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetListFamilyMember(FamilyID, MemberID);

                if (memberList.Count > 0)
                {
                    SexID = Classes.CommonFunctions.Convert2Int(Convert.ToString(memberList[0].SexID));

                }
                return SexID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return SexID;
            }
            finally
            {

            }
        }

        #endregion

        #region Methods

        public int FamilyID
        {
            get
            {
                return familyID;
            }
            set
            {
                familyID = value;
            }
        }

        public int MemberID
        {
            get
            {
                return memberID;
            }
            set
            {
                memberID = value;
            }
        }

        private void SetCulture()
        {
            App apps = (App)Application.Current;
            tiNewMedicalRecord.Header = "New Weight";
            btnSave.Content = "Save";
            lblTestDate.Content = "Clinical Test Date";
            lblWeight.Content = "Weight (Kg)";
            lvNutritionCol1.Header = "Clinical Test Date";
            lvNutritionCol2.Header = "Weight (Kg)";
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;

            this.Style = (Style)apps.SetStyle["WinStyle"];
            lblTestDate.Style = (Style)apps.SetStyle["LabelStyle"];
            lblWeight.Style = (Style)apps.SetStyle["LabelStyle"];
        }


        public void SetMemberName(int FamilyID, int MemberID)
        {
            string Name = string.Empty;
            string Sex = string.Empty;
            string Weight = string.Empty;
            string dailyCalorieRequirement = string.Empty;
            string abnormalities = string.Empty;

            List<Member> memberListBasic = new List<Member>();
            List<Member> memberListGeneral = new List<Member>();

      
            memberListBasic = MemberManager.GetListMember(LanguageID, FamilyID, MemberID);
            lblMemberName.FontSize = 15;

            if (memberListBasic.Count > 0)
            {
                if (Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)).Length > 13)
                {
                    Name = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)).Substring(0, 13) + "..";
                }
                else
                {
                    Name = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID));
                }

                if (memberListBasic[0].SexItem != null)
                {
                    Sex = Convert.ToString(memberListBasic[0].SexItem.SexName) + " ";
                }

                if (memberListGeneral.Count > 0)
                {
                    if (memberListGeneral[0].Weight != 0)
                    {
                        Weight = Convert.ToString(memberListGeneral[0].Weight) + " kg ";
                    }
                    else
                    {
                        Sex = Sex.Replace(" ", "");
                    }
                }

                abnormalities = Classes.CommonFunctions.CalculatePersonalAbnormalitiesNew(FamilyID, MemberID);
                if (abnormalities == string.Empty)
                {
                    Weight = Weight.Replace(" ", "");
                }

                if (Classes.CommonFunctions.CalculateCalorieInTake(FamilyID, MemberID) != 0.0)
                {
                    dailyCalorieRequirement = " Daily CalorieRequirement : " + Convert.ToString(Classes.CommonFunctions.CalculateCalorieInTake(FamilyID, MemberID));
                }

                lblMemberName.Content = Name + " [ " + Sex + Weight + abnormalities + " ]" + dailyCalorieRequirement;
            }
        }

        public void SetMaxLength()
        {
            txtWeight.MaxLength = 6;
        }

        private bool Validate()
        {
            if (dtpTestDate.SelectedDate == null )
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1082"), "Member Details", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1247"), "", AlertType.Information, AlertButtons.OK);
                dtpTestDate.Focus();
                return false;
            }
            if (dtpTestDate.SelectedDate != null)
            {
                if (dtpTestDate.SelectedDate > System.DateTime.Now)
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1084"), "", AlertType.Information, AlertButtons.OK);
                    //AlertBox.Show("Enter TestDate", "", AlertType.Error, AlertButtons.OK);
                    dtpTestDate.Focus();
                    return false;
                }
                if (!Classes.CommonFunctions.IsDate(Convert.ToString(dtpTestDate.SelectedDate)))
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1028"), "", AlertType.Information, AlertButtons.OK);
                    //AlertBox.Show("Enter a Valid DOB", "", AlertType.Error, AlertButtons.OK);
                    dtpTestDate.Focus();
                    return false;
                }
            }
            
            if (Classes.CommonFunctions.Convert2Double(txtWeight.Text) != 0)
            {
                if (Classes.CommonFunctions.Convert2Double(txtWeight.Text) < 1 || Classes.CommonFunctions.Convert2Double(txtWeight.Text) > 200)
                {
                    //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1039"), "", AlertType.Error, AlertButtons.OK);
                    AlertBox.Show(XMLServices.GetXmlMessage("E1232"), "", AlertType.Information, AlertButtons.OK);
                    txtWeight.Focus();
                    return false;
                }
            }
            else
            {
                //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1011"), "", AlertType.Error, AlertButtons.OK);
                AlertBox.Show(XMLServices.GetXmlMessage("E1114"), "", AlertType.Information, AlertButtons.OK);
                txtWeight.Focus();
                return false;
            }

            return true;
        }
      
        #endregion

        #region Events

        private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var uie = e.OriginalSource as UIElement;
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                uie.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
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

        private void txtDate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Classes.CommonFunctions.FilterNumericDate(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            SetCulture();
            SetMaxLength();
            SetMemberName(FamilyID, MemberID);
            dtpTestDate.SelectedDate = (DateTime?)DateTime.Now.Date;
            
            FillFamilyMedical(FamilyID, MemberID);
            
            CreateGraph();
            //CreateWPFGraph();
        }
       
        private void memberlist_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                SetMemberName(FamilyID, MemberID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MemberID != 0)
                {
                    if (SaveFamilyMedical(FamilyID, MemberID) == true)
                    {
                        //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1001"), "", AlertType.Information, AlertButtons.OK);
                        AlertBox.Show(XMLServices.GetXmlMessage("E1261"), "", AlertType.Information, AlertButtons.OK);
                        this.Close();
                    }
                }
                else
                {
                    AlertBox.Show(XMLServices.GetXmlMessage("E1260"), "", AlertType.Information, AlertButtons.OK);
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

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            objMemberProfile.FillData();   
        }

        private void imgDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DeleteWeight();
        }
        private void DeleteWeight()
        {
            DateTime TestDate;

            if (lvWeightHistory.SelectedIndex >= 0)
            {
                TestDate = ((Member)lvWeightHistory.Items[lvWeightHistory.SelectedIndex]).ModifiedDate;
                bool result = AlertBox.Show(XMLServices.GetXmlMessage("E1069"), "", AlertType.Exclamation, AlertButtons.YESNO);
                if (result == true)
                {
                    try
                    {
                        if (MemberManager.DeleteFamilyHistory(MemberID, "Weight", TestDate))
                        {
                            //AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1070"), "", AlertType.Error, AlertButtons.OK);
                            FillFamilyMedical(FamilyID, MemberID);
                            AlertBox.Show(XMLServices.GetXmlMessage("E1262"), "", AlertType.Information, AlertButtons.OK);
                            dtpTestDate.SelectedDate = (DateTime?)DateTime.Now.Date;
                            txtWeight.Text = string.Empty;

                            SetLatestWeight();
                            CreateGraph();
                            //CreateWPFGraph();
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
                else
                {
                    return;
                }
            }
        }

        private void imgEdit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UpdateWeight();
        }
        private void lvWeightHistory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdateWeight();
        }

        private void lvWeightHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateWeight();
        }

        private void UpdateWeight()
        {
            DateTime TestDate;
            if (lvWeightHistory.SelectedIndex >= 0)
            {
                TestDate = Convert.ToDateTime(((Member)lvWeightHistory.Items[lvWeightHistory.SelectedIndex]).ModifiedDate.ToShortDateString());
                dtpTestDate.SelectedDate = TestDate.Date;
                FillWeightItem(FamilyID, MemberID, "Weight", TestDate);
                SetLatestWeight();
            }
        }
        private void dtpTestDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txtWeight.Text = string.Empty;
        }        

        private void lblSaveChart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string FileName = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)) + "-WeightHistory";

            FileName = SaveDialog("Save Weight History Chart", "PNG File (*.png)|*.png", FileName + ".png", true);

            if (FileName == string.Empty)
            {
                return;
            }
            else
            {
                ExportToPng(new Uri(FileName), (int)GraphLayout.Width, (int)GraphLayout.Height, chart);
            }
        }

        private void lvWeightHistory_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                DeleteWeight();
            }
        }

        #endregion

        #region Graph

        public void CreateGraph()
        {
            chart = new Visifire.Charts.Chart();
            chart.Cursor = Cursors.Hand;
            chart.ScrollingEnabled = false;

            Title header = new Title();
            header.FontSize = 13;
            header.FontWeight = FontWeights.Bold;
            header.FontStyle = FontStyles.Italic;
            header.VerticalAlignment = VerticalAlignment.Top;
            header.HorizontalAlignment = HorizontalAlignment.Left;
            header.Text = Convert.ToString(ConfigurationManager.AppSettings["ProductName"]);
            chart.Titles.Add(header);

            Title footer = new Title();
            footer.FontSize = 10;
            footer.FontWeight = FontWeights.Bold;
            footer.VerticalAlignment = VerticalAlignment.Bottom;
            footer.HorizontalAlignment = HorizontalAlignment.Right;
            footer.Text = "Indocosmo Systems Pvt Ltd";
            chart.Titles.Add(footer);

            Title title = new Title();
            title.Text = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)) + " - Weight History";
            chart.Titles.Add(title);

            Visifire.Charts.Axis axisX = new Visifire.Charts.Axis();
            axisX.Title = "Modified Date";
            axisX.Interval = Convert.ToDouble("6.0");
            axisX.IntervalType = IntervalTypes.Months;
            axisX.ValueFormatString = "dd/MMM/yy";
            chart.AxesX.Add(axisX);

            Visifire.Charts.Axis axisY = new Visifire.Charts.Axis();
            axisY.Title = "Weight";
            axisY.Interval = Convert.ToDouble("50.0");
            axisY.IntervalType = IntervalTypes.Number;
            axisY.Suffix = " Kg";
            chart.AxesY.Add(axisY);

            DataSeries dataSeries = new DataSeries();
            dataSeries.RenderAs = RenderAs.Line;
            dataSeries.XValueFormatString = "dd/MMM/yy";
            dataSeries.ToolTipText = "Modified Date : #XValue \n Weight : #YValue";

            Visifire.Charts.DataPoint dataPoint;

            for (int i = 0; i < lvWeightHistory.Items.Count; i++)
            {
                dataPoint = new Visifire.Charts.DataPoint();
                dataPoint.XValue = ((Member)lvWeightHistory.Items[i]).ModifiedDate.Date.ToString("dd/MMM/yyyy");
                dataPoint.YValue = Classes.CommonFunctions.Convert2Double(Convert.ToString(((Member)lvWeightHistory.Items[i]).ParameterValue));
                dataSeries.DataPoints.Add(dataPoint);
            }

            chart.Series.Add(dataSeries);
            GraphLayout.Children.Add(chart);
            
            chart.MouseLeftButtonDown += new MouseButtonEventHandler(chart_MouseLeftButtonDown);
        }

        void chart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //string FileName = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)) + "-WeightHistory";

            //FileName = SaveDialog("Save Weight History Chart", "PNG File (*.png)|*.png", FileName + ".png", true);

            //if (FileName == string.Empty)
            //{
            //    return;
            //}
            //else
            //{
            //    ExportToPng(new Uri(FileName), (int)GraphLayout.Width, (int)GraphLayout.Height, chart);
            //}
            
        }

        public void ExportToPng(Uri Path,int Width, int Height, Visifire.Charts.Chart Surface)
        {
            if (Path == null)
            {
                return;
            }

            Transform Transform = Surface.LayoutTransform;
            Surface.LayoutTransform = null;
            Surface.Width = Width;
            Surface.Height = Height;

            RenderTargetBitmap RenderBitmap = new RenderTargetBitmap((int)Surface.Width, (int)Surface.Height, 96d, 96d, PixelFormats.Pbgra32);
            RenderBitmap.Render(Surface);

            using (FileStream outStream = new FileStream(Path.LocalPath, FileMode.Create))
            {
                PngBitmapEncoder Encoder = new PngBitmapEncoder();
                Encoder.Frames.Add(BitmapFrame.Create(RenderBitmap));
                Encoder.Save(outStream);
            }

            Surface.LayoutTransform = Transform;

            AlertBox.Show(XMLServices.GetXmlMessage("E1193"), "", AlertType.Information, AlertButtons.OK);
        }

        public string SaveDialog(string Title, string Filter, string FileName, bool IsOverWrite)
        {
            SaveDlg.Title = Title;
            SaveDlg.Filter = Filter;
            SaveDlg.FilterIndex = 1;
            SaveDlg.OverwritePrompt = IsOverWrite;
            SaveDlg.FileName = FileName;
            if (SaveDlg.ShowDialog() == true)
            {
                return (SaveDlg.FileName);
            }
            else
            {
                return ("");
            }
        }
       
        #endregion

    }
}
