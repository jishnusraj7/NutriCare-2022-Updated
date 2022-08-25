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
using NutritionV1.Enums;
using System.Windows.Threading;
using System.Windows.Controls.DataVisualization.Charting;
using Visifire.Charts;
using Visifire.Commons;
using NutritionV1.Constants;
using System.IO;
using Microsoft.Win32;
using System.Configuration;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for SimulateYourGoal.xaml
    /// </summary>
    public partial class SimulateYourGoal : Window
    {
        #region Declarations

        Member member = new Member();

        int LanguageID = ResourceSetting.defaultlanguage;

        public static int memberID;
        public static int familyID;
        public static int mPWeight;
        public static int mDWeight;
        public static int mCalorie;
        public static int simulationCalorie;
        public static int exerciseCalorie;
        public static bool isGoal;
        public static bool isExercise;
        public static int calorieInTake;
        public static int simulationPeriod;

        double BMI = 0;

        Visifire.Charts.Chart chart;

        private int selectedLanguage = (int)ResourceSetting.selectlanguage;

        SaveFileDialog SaveDlg = new SaveFileDialog();

        #endregion

        #region Constructor

        public SimulateYourGoal()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
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

        public int MPWeight
        {
            get
            {
                return mPWeight;
            }
            set
            {
                mPWeight = value;
            }
        }

        public int MDWeight
        {
            get
            {
                return mDWeight;
            }
            set
            {
                mDWeight = value;
            }
        }

        public int MCalorie
        {
            get
            {
                return mCalorie;
            }
            set
            {
                mCalorie = value;
            }
        }

        public int SimulationCalorie
        {
            get
            {
                return simulationCalorie;
            }
            set
            {
                simulationCalorie = value;
            }
        }

        public int ExerciseCalorie
        {
            get
            {
                return exerciseCalorie;
            }
            set
            {
                exerciseCalorie = value;
            }
        }

        public int CalorieInTake
        {
            get
            {
                return calorieInTake;
            }
            set
            {
                calorieInTake = value;
            }
        }

        public int SimulationPeriod
        {
            get
            {
                return simulationPeriod;
            }
            set
            {
                simulationPeriod = value;
            }
        }

        public bool IsGoal
        {
            get
            {
                return isGoal;
            }
            set
            {
                isGoal = value;
            }
        }

        public bool IsExercise
        {
            get
            {
                return isExercise;
            }
            set
            {
                isExercise = value;
            }
        }

        private void SetCulture()
        {
            App apps = (App)Application.Current;
            ResourceManager rm = apps.getLanguageList;
        }

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            this.Style = (Style)apps.SetStyle["WinStyle"];
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
            memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);

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
                    dailyCalorieRequirement = " Daily Calorie Requirement : " + Convert.ToString(Classes.CommonFunctions.CalculateCalorieInTake(FamilyID, MemberID));
                }

                lblMemberName.Content = Name + " [ " + Sex + Weight + abnormalities + " ]" + dailyCalorieRequirement;
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
                MessageBox.Show(ex.Message);
                return SexID;
            }
            finally
            {

            }
        }

        public double GetMemberWeight()
        {
            double Weight = 0;
            try
            {
                List<Member> memberList = new List<Member>();
                memberList = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);

                if (memberList.Count > 0)
                {
                    Weight = Classes.CommonFunctions.Convert2Double(Convert.ToString(memberList[0].Weight));

                }
                return Weight;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Message);
                return Weight;
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

        private void FillComments(int MemberID)
        {
            if (MemberID != 0)
            {
                if (calorieInTake > SimulationCalorie)
                {
                    txtComments.Text = "Your Daily Calorie Intake in to Acheive Goal is " + SimulationCalorie + ".  " + "(Calorie From Food is " + (SimulationCalorie - ExerciseCalorie) + " , " + "And Calorie Burned through Exercise is " + ExerciseCalorie + " .)" + System.Environment.NewLine + System.Environment.NewLine +
                                        "And We Are Going to Simulate Your Goal Using " + SimulationCalorie + " Calorie";
                }
                else
                {
                    txtComments.Text = "Your Daily Calorie Intake in to Acheive Goal is " + SimulationCalorie + ".  " + System.Environment.NewLine + System.Environment.NewLine +
                                        "And We Are Going to Simulate Your Goal Using " + SimulationCalorie + " Calorie";
                }
            }
        }

        private double CalculateSimulatedWeight(int MemberID)
        {

            double Calorie = 0, SimulatedWeight = 0, SimulatedCalorie = 0;

            List<BONutrition.GoalSetting> goalSettingList = new List<BONutrition.GoalSetting>();
            List<Member> memberListGeneral = new List<Member>();

            goalSettingList = GoalSettingManager.GetList(FamilyID, MemberID);
            memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);

            //Calorie = Classes.CommonFunctions.Convert2Double(Convert.ToString(txtMCalorie.Text)) - Classes.CommonFunctions.Convert2Double(Convert.ToString(txtExerciseCalorie.Text)) - CalculateCalorieInTake();
            Calorie = Classes.CommonFunctions.Convert2Double(Convert.ToString(MCalorie)) - CalorieInTake;

            SimulatedCalorie = Math.Abs(Calorie / 500);

            if (SimulatedCalorie > 0)
            {
                if (BMI > BodyMassIndex.BMIMAXIMUM)
                {
                    SimulatedWeight = SimulatedCalorie * 3;
                }
                else
                {
                    SimulatedWeight = SimulatedCalorie * 2;
                }
            }

            return SimulatedWeight;
        }

        private int CalculateSimulationType(int MemberID)
        {
            int SimulationType = 2;
            double Calorie = 0;

            List<BONutrition.GoalSetting> goalSettingList = new List<BONutrition.GoalSetting>();
            List<Member> memberListGeneral = new List<Member>();

            memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);
            goalSettingList = GoalSettingManager.GetList(FamilyID, MemberID);

            if (Classes.CommonFunctions.Convert2Float(Convert.ToString(MPWeight)) > Classes.CommonFunctions.Convert2Float(Convert.ToString(MDWeight)))
            {
                Calorie = Classes.CommonFunctions.Convert2Double(Convert.ToString(MCalorie)) - CalorieInTake;
            }
            else if (Classes.CommonFunctions.Convert2Float(Convert.ToString(MPWeight)) == Classes.CommonFunctions.Convert2Float(Convert.ToString(MDWeight)))
            {
                Calorie = CalorieInTake;
            }
            else if (Classes.CommonFunctions.Convert2Float(Convert.ToString(MPWeight)) < Classes.CommonFunctions.Convert2Float(Convert.ToString(MDWeight)))
            {
                Calorie = Classes.CommonFunctions.Convert2Double(Convert.ToString(MCalorie)) - CalorieInTake;
            }

            if (Calorie > 0)
            {
                SimulationType = (int)GoalType.WeightGain;
            }
            else if (Calorie == 0)
            {
                SimulationType = (int)GoalType.MaintainWeight;
            }
            else if (Calorie < 0)
            {
                SimulationType = (int)GoalType.WeightLoss;
            }

            return SimulationType;
        }

        private double CalculateWeightLimit(int SimulationType)
        {
            double Weight = 0, BornWeight = 0;

            List<Member> memberListGeneral = new List<Member>();
            List<BONutrition.GoalSetting> goalSettingList = new List<BONutrition.GoalSetting>();

            memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);
            goalSettingList = GoalSettingManager.GetList(FamilyID, MemberID);


            if (IsGoal == true)
            {
                Weight = Classes.CommonFunctions.Convert2Double(Convert.ToString(goalSettingList[0].PresentWeight));
            }
            else
            {
                Weight = Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Weight));
            }

            if (SimulationType == (int)GoalType.WeightLoss)
            {
                BornWeight = Weight / 2;
            }
            else if (SimulationType == (int)GoalType.WeightGain)
            {
                BornWeight = Weight * 2;
            }

            return BornWeight;
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

        private void txtDecimal_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                Classes.CommonFunctions.FilterDecimal(sender, e);
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
            
            SetMemberName(FamilyID, MemberID);
            FillComments(MemberID);

            CreateGraph(FamilyID, MemberID, SimulationPeriod);
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void lblHelp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Help help = new Help();
            help.HelpItemID = (int)HelpType.GoalSimulation;
            help.ShowDialog();
        }

        private void lblTips_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Tips tips = new Tips();
            tips.TipsItemID = (int)FlowType.GoalSimulation;
            tips.Owner = Application.Current.MainWindow;
            tips.ShowDialog();
        }

        #endregion

        #region Graph

        public void CreateGraph(int FamilyID, int MemberID, int SimulationPeriod)
        {
            int Month = 0, SimulationType = 2;
            double Weight = 0, SimulatedWeight = 0;
            int Period = 0;

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
            title.Text = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)) + " - Goal Simulation";
            chart.Titles.Add(title);

            Visifire.Charts.Axis axisX = new Visifire.Charts.Axis();
            axisX.Title = "Goal Duration";
            axisX.Interval = Convert.ToDouble("1.0");
            axisX.IntervalType = IntervalTypes.Months;
            axisX.Suffix = " Month";
            chart.AxesX.Add(axisX);

            Visifire.Charts.Axis axisY = new Visifire.Charts.Axis();
            axisY.Title = "Weight";
            axisY.Interval = Convert.ToDouble("50.0");
            axisY.IntervalType = IntervalTypes.Number;
            axisY.Suffix = " Kg";
            chart.AxesY.Add(axisY);

            DataSeries dataSeries = new DataSeries();
            dataSeries.RenderAs = RenderAs.Line;
            dataSeries.ToolTipText = "Month : #XValue \n Weight : #YValue";


            List<Member> memberListGeneral = new List<Member>();
            List<BONutrition.GoalSetting> goalSettingList = new List<BONutrition.GoalSetting>();

            memberListGeneral = MemberManager.GetListFamilyGeneral(FamilyID, MemberID);
            goalSettingList = GoalSettingManager.GetList(FamilyID, MemberID);

            if (memberListGeneral.Count > 0)
            {
                if (SimulationPeriod > 0)
                {
                    Period = SimulationPeriod;

                    if (IsGoal == true)
                    {
                        Weight = Classes.CommonFunctions.Convert2Double(Convert.ToString(goalSettingList[0].PresentWeight));
                    }
                    else
                    {
                        Weight = Classes.CommonFunctions.Convert2Double(Convert.ToString(memberListGeneral[0].Weight));
                    }

                    if (Weight > 0 && SimulationCalorie > 0)
                    {
                        SimulationType = CalculateSimulationType(MemberID);
                        SimulatedWeight = CalculateSimulatedWeight(MemberID);

                        Visifire.Charts.DataPoint dataPoint;

                        for (int i = 0; i <= Period; i++)
                        {
                            dataPoint = new Visifire.Charts.DataPoint();

                            dataPoint.XValue = Month;

                            if (SimulationType == (int)GoalType.WeightLoss)
                            {
                                dataPoint.YValue = Weight;
                                Weight = Weight - CalculateSimulatedWeight(MemberID);

                                if (Weight < Classes.CommonFunctions.Convert2Double(Convert.ToString(CalculateWeightLimit(SimulationType))))
                                {
                                    break;
                                }
                            }
                            else if (SimulationType == (int)GoalType.MaintainWeight)
                            {
                                dataPoint.YValue = Weight;
                            }
                            else if (SimulationType == (int)GoalType.WeightGain)
                            {
                                dataPoint.YValue = Weight;
                                Weight = Weight + CalculateSimulatedWeight(MemberID);

                                if (Weight > Classes.CommonFunctions.Convert2Double(Convert.ToString(CalculateWeightLimit(SimulationType))))
                                {
                                    break;
                                }
                            }

                            dataSeries.DataPoints.Add(dataPoint);
                            Month = Month + 1;
                        }
                    }

                }
            }

            chart.Series.Add(dataSeries);
            GraphLayout.Children.Add(chart);

            chart.MouseLeftButtonDown += new MouseButtonEventHandler(chart_MouseLeftButtonDown);
        }

        void chart_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //string FileName = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)) + "-GoalSimulation";

            //FileName = SaveDialog("Save Goal Simulation Chart", "PNG File (*.png)|*.png", FileName + ".png", true);

            //if (FileName == string.Empty)
            //{
            //    return;
            //}
            //else
            //{
            //    ExportToPng(new Uri(FileName), (int)GraphLayout.Width, (int)GraphLayout.Height, chart);
            //}

        }

        public void ExportToPng(Uri Path, int Width, int Height, Visifire.Charts.Chart Surface)
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

            AlertBox.Show(Common.Classes.XMLServices.GetXmlMessage("E1193"), "", AlertType.Information, AlertButtons.OK);
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

        private void lblSaveChart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string FileName = Convert.ToString(MemberManager.GetMemberName(FamilyID, MemberID)) + "-GoalSimulation";

            FileName = SaveDialog("Save Goal Simulation Chart", "PNG File (*.png)|*.png", FileName + ".png", true);

            if (FileName == string.Empty)
            {
                return;
            }
            else
            {
                ExportToPng(new Uri(FileName), (int)GraphLayout.Width, (int)GraphLayout.Height, chart);
            }
        }

        #endregion

        

    }
}
