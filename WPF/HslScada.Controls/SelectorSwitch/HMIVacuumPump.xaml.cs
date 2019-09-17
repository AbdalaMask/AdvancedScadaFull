using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace HslScada.Controls
{
    /// <summary>
    /// Interaction logic for HMIVacuumPump.xaml
    /// </summary>
    public partial class HMIVacuumPump : UserControl
    {

        public HMIVacuumPump()
        {
            InitializeComponent();
            doubleAnimation = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(3.33333d));
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            BeginAnimation(StartAngleProperty, doubleAnimation);
        }
        /// <summary>
        /// 角度信息
        /// </summary>
        public double StartAngle
        {
            get { return (double)GetValue(StartAngleProperty); }
            set { SetValue(StartAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartAngleProperty =
            DependencyProperty.Register("StartAngle", typeof(double), typeof(HMIVacuumPump), new PropertyMetadata(0d));

        private DoubleAnimation doubleAnimation;

        /// <summary>
        /// 转动速度
        /// </summary>
        public double MoveSpeed
        {
            get { return (double)GetValue(MoveSpeedProperty); }
            set { SetValue(MoveSpeedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MoveSpeed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MoveSpeedProperty =
            DependencyProperty.Register("MoveSpeed", typeof(double), typeof(HMIVacuumPump), new PropertyMetadata(0.3d, new PropertyChangedCallback(MoveSpeedDependencyPropertyChanged)));

        /// <summary>
        /// 更运动速度
        /// </summary>
        public void UpdateMoveSpeed()
        {
            if (MoveSpeed > 0)
            {
                doubleAnimation = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(1 / MoveSpeed));
                doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                BeginAnimation(StartAngleProperty, doubleAnimation);
            }
            else if (MoveSpeed < 0)
            {
                doubleAnimation = new DoubleAnimation(0, -360, TimeSpan.FromSeconds(1 / Math.Abs(MoveSpeed)));
                doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
                BeginAnimation(StartAngleProperty, doubleAnimation);
            }
            else
            {
                BeginAnimation(StartAngleProperty, null);
            }
        }

        public static void MoveSpeedDependencyPropertyChanged(DependencyObject dependency, DependencyPropertyChangedEventArgs e)
        {
            HMIVacuumPump pumpOne = (HMIVacuumPump)dependency;

            if (dependency != null)
            {
                pumpOne.UpdateMoveSpeed();
            }
        }

    }
}
