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
    public class MinimizeWindowAction : TriggerAction<Button>
    {
        protected override void Invoke(object parameter) => Window.GetWindow(AssociatedObject).WindowState = WindowState.Minimized;
    }
}
