using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MPMS___Projection_Management_System
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class Projection : Window
    {
        private Screen screen;
        private ProjectionTimelineMetro ptlm;
        public System.Drawing.Rectangle workingArea;
        private System.Windows.Threading.DispatcherTimer mouseLockTimer;

        [DllImport("user32.dll")]
        static extern bool EnumDisplayDevices(string lpDevice, uint iDevNum, ref DISPLAY_DEVICE lpDisplayDevice, uint dwFlags);

        public Projection()
        {
            screen = GetSecondaryScreen();
            if (screen == null)
            {
                System.Windows.MessageBox.Show("O sistema está operando com um monitor apenas. Insira o Projetor como segundo monitor e tente novamente.", "Erro ao Inicializar", MessageBoxButton.OK, MessageBoxImage.Error);
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    // WinForms app
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    // Console app
                    System.Environment.Exit(1);
                }
            }

            //Display.SetDisplayMode(Display.DisplayMode.Extend);

            WindowStartupLocation = WindowStartupLocation.Manual;
    
            InitializeComponent();

            System.Drawing.Rectangle r2 = screen.WorkingArea;
            Top = r2.Top;
            Left = r2.Left;

            ShowInTaskbar = false;
            this.Background = Brushes.Black;
            projection.Background = Brushes.Black;
        }

        private void projection_Loaded(object sender, RoutedEventArgs e)
        {
            workingArea = Screen.PrimaryScreen.WorkingArea;
            workingArea.Height += 400;
          

            mouseLockTimer = new System.Windows.Threading.DispatcherTimer();
            mouseLockTimer.Tick += new EventHandler(LockMouse);
            mouseLockTimer.Interval = new TimeSpan(1 * 10000);

            ptlm = new ProjectionTimelineMetro();
            ptlm.p = this;
            ptlm.Show();

            mouseLockTimer.Start();

            Visibility = Visibility.Visible;
            FullScreen();
            
        }

        private void LockMouse(object sender, EventArgs e)
        {
            System.Windows.Forms.Cursor.Clip = workingArea;
        }

        private void ProjectionWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void FullScreen()
        {
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
            System.Windows.Forms.Cursor.Clip = workingArea;
            Topmost = true;
           
        }

        public Screen GetSecondaryScreen()
        {
            if (Screen.AllScreens.Length == 1)
            {
                return null;
            }
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Primary == false)
                {
                    return screen;
                }
            }
            return null;
        }
    }
}
