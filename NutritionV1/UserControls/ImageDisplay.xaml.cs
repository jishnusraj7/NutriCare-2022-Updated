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
using NutritionV1.Classes;
using System.Resources;
using System.Data;
using System.IO;

namespace NutritionV1
{
    /// <summary>
    /// Interaction logic for ImageDisplay.xaml
    /// </summary>
    public partial class ImageDisplay : UserControl
    {
        public string itemName;
        public int itemID;
        public string controlName;
        public int controlID;

        public ImageDisplay()
        {
            InitializeComponent();           
        }
        private void setTheme()
        {
            App apps = (App)Application.Current;
            RecImage.Style = (Style)apps.SetStyle["ImageDisplay"];
        }

        public string ImageSource
        {
            set
            {
                BitmapImage bmpImage;
                string imgPath = string.Empty;
                try
                {
                    if (value != string.Empty)
                    {

                        FileInfo file = new FileInfo(value);
                        if (file.Exists)
                        {
                            bmpImage = new BitmapImage(new Uri(value));
                            Image.Source = bmpImage;
                        }
                        else
                        {
                            bmpImage = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg"));
                            Image.Source = bmpImage;
                        }
                    }
                    else
                    {
                        bmpImage = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\NoImage.jpg"));
                        Image.Source = bmpImage;
                    }
                }
                catch (Exception ex)
                {
                    bmpImage = null;
                    Image.Source = bmpImage;
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    
                }
            }
        }

        public string ImagePath
        {
            set
            {
                BitmapImage bmpImage;
                string imgPath = string.Empty;
                try
                {
                    if (value != string.Empty)
                    {

                        FileInfo file = new FileInfo(value);
                        if (file.Exists)
                        {
                            bmpImage = new BitmapImage(new Uri(value));
                            Image.Source = bmpImage;
                        }
                        else
                        {
                            bmpImage = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\Images\NoImage.jpg"));
                            Image.Source = bmpImage;
                        }
                    }
                    else
                    {
                        bmpImage = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\Images\NoImage.jpg"));
                        Image.Source = bmpImage;
                    }
                }
                catch (Exception ex)
                {
                    bmpImage = null;
                    Image.Source = bmpImage;
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    
                }
            }
        }

        public string ImageName
        {
            set
            {
                try
                {
                    if (value != string.Empty)
                    {
                        Name.Text = value;
                    }
                    else
                    {
                        Name.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    Name.Text = "";
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    
                }
            }
        }
        
        public int ImageID
        {
            set
            {
                try
                {
                    if (value != 0)
                    {
                        ID.Content = value;
                    }
                    else
                    {
                        ID.Content = 0;
                    }
                }
                catch (Exception ex)
                {
                    ID.Content = 0;
                    MessageBox.Show(ex.Message);
                }
                finally
                {

                }
            }
        }

        public int ItemID
        {
            set
            {
                itemID = value;
            }
            get
            {
                return itemID;
            }
        }

        public string ItemName
        {
            set
            {
                itemName = value;
            }
            get
            {
                return itemName;
            }
        }

        public int ControlID
        {
            set
            {
                controlID = value;
            }
            get
            {
                return controlID;
            }
        }

        public string ControlName
        {
            set
            {
                controlName = value;
            }
            get
            {
                return controlName;
            }
        }

        private void ShowMessages(string p)
        {
            throw new NotImplementedException();
        }

        public bool SetThemes
        {
            set 
            {
                if (value == true)
                {
                    setTheme();
                }
            }
        }

    }
}
