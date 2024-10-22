using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HarpenTech.Services.Handler
{
    // Customized Toast class that extends the base Toast class and includes an icon
    public class ToastWithIcon : Toast
    {
        // Property to set or get the icon for the toast
        public IImageSource Icon { get; set; }

        // Factory method to create an instance of ToastWithIcon
        public new static ToastWithIcon Make(string message, ToastDuration duration = ToastDuration.Short, double textSize = AlertDefaults.FontSize)
        {
            // Create a new instance of ToastWithIcon and set its properties
            return new ToastWithIcon
            {
                Text = message,
                Duration = duration,
                TextSize = textSize
            };
        }
    }

}
