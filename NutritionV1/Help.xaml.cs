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
    /// Interaction logic for Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        #region Declarations

        int LanguageID = ResourceSetting.defaultlanguage;
        private ItemType displayitem;
        private int helpItemID;

        #endregion

        #region Properties

        public int HelpItemID
        {
            set
            {
                helpItemID = value;

            }
            get
            {
                return helpItemID;
            }

        }

        #endregion

        #region Constructor

        public Help()
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
            List<SysAdmin> sysAdminList = new List<SysAdmin>();
            sysAdminList = SysAdminManager.GetListHelp(HelpItemID);
            
            if (sysAdminList.Count > 0)
            {
                this.Title = "   " + Convert.ToString(sysAdminList[0].HelpItemName);
                LoadXAMLTemplate(Convert.ToString(sysAdminList[0].HelpItemDescription));
                //txtHelp.Html = Convert.ToString(sysAdminList[0].HelpItemDescription);
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

                RichTextBox rtbHelp = new RichTextBox();
                rtbHelp.Background = new SolidColorBrush(Colors.Transparent);
                rtbHelp.Foreground = new SolidColorBrush(Colors.White);
                rtbHelp.FontSize = 12;
                rtbHelp.IsHitTestVisible = false;
                rtbHelp.BorderThickness = new Thickness(0);
                rtbHelp.Document = flowDoc;
                stackPanel.Children.Add(rtbHelp);
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
