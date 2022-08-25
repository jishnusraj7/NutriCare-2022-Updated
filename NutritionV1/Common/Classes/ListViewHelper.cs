using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections;
using BONutrition;
using NutritionV1.Classes;

namespace NutritionV1.Common.Classes
{
    public class ListViewHelper
    {
        public static FrameworkElement GetElementFromCellTemplate(ListView listView, Int32 column, Int32 row, String name)
        {
            if (row >= listView.Items.Count || row < 0)
            {
                throw new ArgumentOutOfRangeException("row");
            }

            GridView gridView = listView.View as GridView;
            if (gridView == null)
            {
                return null;
            }

            if (column >= gridView.Columns.Count || column < 0)
            {
                throw new ArgumentOutOfRangeException("column");
            }

            ListViewItem item = listView.ItemContainerGenerator.ContainerFromItem(listView.Items[row]) as ListViewItem;
            if (item != null)
            {
                GridViewRowPresenter rowPresenter = GetFrameworkElementByName<GridViewRowPresenter>(item);
                if (rowPresenter != null)
                {
                    ContentPresenter templatedParent = VisualTreeHelper.GetChild(rowPresenter, column) as ContentPresenter;
                    templatedParent.ApplyTemplate();
                    DataTemplate dataTemplate = gridView.Columns[column].CellTemplate;
                    if (dataTemplate != null && templatedParent != null)
                    {
                        return dataTemplate.FindName(name, templatedParent) as FrameworkElement;
                    }
                }
            }

            return null;
        }

        private static T GetFrameworkElementByName<T>(FrameworkElement referenceElement) where T : FrameworkElement
        {
            FrameworkElement child = null;
            for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(referenceElement); i++)
            {
                child = VisualTreeHelper.GetChild(referenceElement, i) as FrameworkElement;
                System.Diagnostics.Debug.WriteLine(child);
                if (child != null && child.GetType() == typeof(T))
                {
                    break;
                }
                else if (child != null)
                {
                    child = GetFrameworkElementByName<T>(child);
                    if (child != null && child.GetType() == typeof(T))
                    {
                        break;
                    }
                }
            }
            return child as T;
        }

        public static Visual GetDescendantByType(Visual element, Type type, string name)
        {
            if (element == null) return null;
            if (element.GetType() == type)
            {
                FrameworkElement fe = element as FrameworkElement;
                if (fe != null)
                {
                    if (fe.Name == name)
                    {
                        return fe;
                    }
                }
            }
            Visual foundElement = null;
            if (element is FrameworkElement)
                (element as FrameworkElement).ApplyTemplate();
            for (int i = 0;
                i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = GetDescendantByType(visual, type, name);
                if (foundElement != null)
                    break;
            }
            return foundElement;
        }                
    }
}
