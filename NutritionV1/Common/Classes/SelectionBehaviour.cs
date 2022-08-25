using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls.Primitives;

namespace NutritionV1
{
    public class SelectionBehaviour
    {
        public static DependencyProperty SelectionChangedProperty = DependencyProperty.RegisterAttached("SelectionChanged",
        typeof(ICommand),typeof(SelectionBehaviour),new UIPropertyMetadata(SelectionBehaviour.SelectedItemChanged));

        public static void SetSelectionChanged(DependencyObject target, ICommand value)
        {
            target.SetValue(SelectionBehaviour.SelectionChangedProperty, value);
        }

        private static void SelectedItemChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            Selector element = target as Selector;
            if (element == null) throw new InvalidOperationException("This behavior can be attached to Selector item only.");
            if ((e.NewValue != null) && (e.OldValue == null))
            {
                element.SelectionChanged += SelectionChanged;
            }
            else if ((e.NewValue == null) && (e.OldValue != null))
            {
                element.SelectionChanged -= SelectionChanged;
            }
        }
        private static void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UIElement element = (UIElement)sender;
            ICommand command = (ICommand)element.GetValue(SelectionBehaviour.SelectionChangedProperty);
            command.Execute(((Selector)sender).SelectedValue);
        }

    }
}
