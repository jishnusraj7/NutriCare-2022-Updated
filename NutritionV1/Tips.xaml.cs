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
using System.IO;
using System.Xml;
using System.Windows.Markup;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for Tips.xaml
    /// </summary>
    public partial class Tips : Window
    {
        #region Declarations

        int LanguageID = ResourceSetting.defaultlanguage;
        int tipsItemID;

        #endregion

        #region Properties

        public int TipsItemID
        {
            get 
            {
                return tipsItemID;
            }
            set
            {
                tipsItemID = value;
            }
        }


        #endregion

        #region Constructor

        public Tips()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(CloseOnEscape);
        }

        #endregion

        #region Methods
        
        private void SetTheme()
        {
            App apps = (App)Application.Current;
            this.Style = (Style)apps.SetStyle["WinStyle"];
        }

        private void FillData()
        {
            try
            {
                List<SysAdmin> sysAdminList = new List<SysAdmin>();
                sysAdminList = SysAdminManager.GetFormFlow(TipsItemID);

                if (sysAdminList.Count > 0)
                {
                    this.Title = "   " + Convert.ToString(sysAdminList[0].FormFlowName);
                    LoadXAMLTemplate(Convert.ToString(sysAdminList[0].FormFlowDescription));
                    //txtTips.Html = Convert.ToString(sysAdminList[0].FormFlowDescription);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        private void LoadXAMLTemplate(string templateString)
        {
            try
            {
                StackPanel stackPanel = new StackPanel();

                string templateStringStart = "<FlowDocument xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph>";
                string templateStringEnd = "</Paragraph></FlowDocument>";
                templateString = templateStringStart + templateString + templateStringEnd;
                StringReader stringReader = new StringReader(templateString);
                XmlReader xmlReader = XmlReader.Create(stringReader);
                FlowDocument flowDoc = new FlowDocument();
                flowDoc = (FlowDocument)XamlReader.Load(xmlReader);

                RichTextBox rtbTips = new RichTextBox();
                rtbTips.Background = new SolidColorBrush(Colors.Transparent);
                rtbTips.Foreground = new SolidColorBrush(Colors.White);
                rtbTips.FontSize = 12;
                rtbTips.IsHitTestVisible = false;
                rtbTips.BorderThickness = new Thickness(0);
                rtbTips.Document = flowDoc;

                stackPanel.Children.Add(rtbTips);
                svContent.Content = stackPanel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }

        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            FillData();
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
