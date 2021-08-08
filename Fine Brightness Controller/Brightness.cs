using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Display;

namespace Fine_Brightness_Controller
{
    public class Brightness
    {
        public double BrightnessValue { get; set; }
        public double StepValue { get; set; }
        private BrightnessOverride brightnessOverride;

        public Brightness()
        {
            //brightnessOverride = BrightnessOverride.GetDefaultForSystem();
            brightnessOverride = BrightnessOverride.GetForCurrentView();

            StepValue = 0.5;
            BrightnessValue = brightnessOverride.BrightnessLevel;
            // get brightness from windows
            Debug.WriteLine(brightnessOverride.BrightnessLevel);
        }
    }
}
