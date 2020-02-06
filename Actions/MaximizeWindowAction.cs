using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace OrangeNBTEditor.Actions
{
    public class MaximizeWindowAction : TriggerAction<Button>
    {
        protected override void Invoke(object parameter)
        {
            var window = Window.GetWindow(AssociatedObject);
            if (window.WindowState == WindowState.Normal) window.WindowState = WindowState.Maximized;
            else window.WindowState = WindowState.Normal;
        }
    }
}
