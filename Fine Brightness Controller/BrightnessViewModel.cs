using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;

namespace Fine_Brightness_Controller
{
    public class BrightnessViewModel : INotifyPropertyChanged
    {
        private static readonly double MAX_BRIGHTNESS_LEVEL = 100.0, MIN_BRIGHTNESS_LEVEL = 0.0;
        private static readonly double STEP_MAX_LEVEL = 1.0, STEP_MIN_LEVEL = 0.0, STEP_DIFF = 0.1;

        public event PropertyChangedEventHandler PropertyChanged;
        private Brightness brightnessModel;
        public bool isMainPlusEnabled;// { get { return this.isMainPlusEnabled; }  set { this.isMainPlusEnabled = value; this.OnPropertyChanged(); } }
        public bool isMainMinusEnabled;// { get { return this.isMainMinusEnabled; }  set { this.isMainMinusEnabled = value; this.OnPropertyChanged(); } }
        public bool isStepPlusEnabeld;
        public bool isStepMinusEnabled;

        public BrightnessViewModel()
        {
            brightnessModel = new Brightness();
            isMainPlusEnabled = isMainMinusEnabled = isStepPlusEnabeld = isStepMinusEnabled = true;
            if (brightnessModel.BrightnessValue >= MAX_BRIGHTNESS_LEVEL)
                isMainPlusEnabled = false;
            if (brightnessModel.BrightnessValue <= MIN_BRIGHTNESS_LEVEL)
                isMainMinusEnabled = false;
        }

        public double BrightnessLevel {
            get { return brightnessModel.BrightnessValue; }
            set {
                this.brightnessModel.BrightnessValue = value;
                Debug.WriteLine(value);
                this.OnPropertyChanged();
            }
        }

        public double StepValue {
            get { return brightnessModel.StepValue; }
            set {
                this.brightnessModel.StepValue = value;
                Debug.WriteLine(value);
                this.OnPropertyChanged();
            }
        }

        public void MainMinusClick(object sender, RoutedEventArgs e)
        {
            // check underflow
            // if after subtracting, value gets eqal to or below 0, set value to 0 and disable minus button
            if (!isMainPlusEnabled)
                isMainPlusEnabled = true;

            if (BrightnessLevel - StepValue <= MIN_BRIGHTNESS_LEVEL)
            {
                BrightnessLevel = MIN_BRIGHTNESS_LEVEL;
                isMainMinusEnabled = false;
            }
            else
                BrightnessLevel -= StepValue;
        }

        public void MainPlusClick(object sender, RoutedEventArgs e)
        {
            // check overflow
            if (!isMainMinusEnabled)
                isMainMinusEnabled = true;

            if (BrightnessLevel + StepValue >= MAX_BRIGHTNESS_LEVEL)
            {
                BrightnessLevel = MIN_BRIGHTNESS_LEVEL;
                isMainPlusEnabled = false;
            }

            BrightnessLevel += StepValue;
        }

        public void StepMinusClick(object sender, RoutedEventArgs e)
        {
            // check underflow
            if (!isStepPlusEnabeld)
                isStepPlusEnabeld = true;

            if (StepValue - STEP_DIFF <= STEP_MIN_LEVEL)
            {
                StepValue = STEP_MIN_LEVEL;
                isStepMinusEnabled = false;
            }
            else
                StepValue -= STEP_DIFF;
        }

        public void StepPlusClick(object sender, RoutedEventArgs e)
        {
            // check overflow
            if (!isStepMinusEnabled)
                isStepMinusEnabled = true;

            if (StepValue + STEP_DIFF >= STEP_MAX_LEVEL)
            {
                StepValue = STEP_MAX_LEVEL;
                isStepPlusEnabeld = false;
            }
            else
                StepValue += STEP_DIFF;
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // Raise the PropertyChanged event, passing the name of the property whose value has changed.
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public object this[int index] {
            get { /* return the specified index here */ }
            set { /* set the specified index to value here */ }
        }
    }
}
