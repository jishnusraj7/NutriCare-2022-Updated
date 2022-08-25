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
using System.Resources; 

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for ShowAlertBox.xaml
    /// </summary>
    public partial class ShowAlertBox : Window
    {

        #region Methods

        private void SetTheme()
        {
            App apps = (App)Application.Current;
            grdBack.Style = (Style)apps.SetStyle["MessageStyle"];
            this.Style = (Style)apps.SetStyle["WinStyle"];

        }

        private void SetCulture()
        {
            App apps = (App)Application.Current;
            ResourceManager rm = apps.getLanguageList;
            if (btnOK.Content.ToString().ToUpper() == "OK")
            {
                btnOK.Content = "OK";
            }
            else
            {
                btnOK.Content = "Yes";
            }

            if (btnCancel.Content.ToString().ToUpper() == "CANCEL")
            {
                btnCancel.Content = "Cancel";
            }
            else if (btnCancel.Content.ToString().ToUpper() == "OK")
            {
                btnCancel.Content = "OK";
            }
            else
            {
                btnCancel.Content = "No";
            }

        }

        #endregion

        #region Properties

        public bool returnResult
        {
            get
            {
                bool result = false;
                result = btnCancel.IsCancel;
                if (result == false)
                    return true;
                else
                    return false; 
            }
        }

        public string OKContent
        {
            set
            {
                if (value != string.Empty)
                {
                    btnOK.Visibility = Visibility.Visible; 
                    btnOK.Content = value;
                    btnOK.TabIndex = 0;
                }
                else
                {
                    btnOK.Visibility = Visibility.Hidden;
                    btnOK.TabIndex = 0;
                }
            }
        }

        public string CancelContent
        {
            set
            {
                btnCancel.Content = value;
                SetCulture();
            }
        }

        public string MessageContent
        {
            set
            {
                lblMessage.Text = value;
            }
        }

        public string MessageSubject
        {
            set
            {
                lblSubject.Content = value;
            }
        }

        public string MessageTitle
        {
            set
            {
                this.Title = value;
            }
        }

        public int MessageAlertType
        {
            set
            {
                if (value>0)
                {
                    string imgPath = AppDomain.CurrentDomain.BaseDirectory.ToString()+"Images\\";
                    int val = value;
                    switch (val)
                    {
                        case 1:
                            imgPath = imgPath + "Information.png";
                            break;
                        case 2:
                            imgPath = imgPath + "Exclamation.png";
                            break;
                        case 3:
                            imgPath = imgPath + "Error.png";
                            break;
                    }
                    try
                    {
                        imgInfo.Source = new BitmapImage(new Uri(imgPath));
                    }
                    catch
                    { }
                }
            }
        }

        public ShowAlertBox()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            btnCancel.IsCancel = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            btnCancel.IsCancel = true; 
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetTheme();
            btnCancel.Focus();
        }

        #endregion

    }
}
