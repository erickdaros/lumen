using MPMS___Projection_Management_System.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vlc.DotNet.Core;
using Vlc.DotNet.Forms;

namespace MPMS___Projection_Management_System
{
    /// <summary>
    /// Lógica interna para ProjectionTimeline.xaml
    /// </summary>
    public partial class ProjectionTimelineMetro
    {
        public Projection p;
        private Media currentMedia;
        private bool mediaReprodutionFinished = false;
        private int currentTrackID = 0;
        private int lastTrackID = 0;
        private List<Media> playList = new List<Media>();
        private bool isMuted = false;
        private int lastVolumeValue = 100;
        private System.Windows.Threading.DispatcherTimer dt;
        private System.Windows.Threading.DispatcherTimer checker;
        private PlayerState lastPlayerState = PlayerState.Unknown;
        private SliderDragState lastSliderDragState = SliderDragState.Unknown;
        string file = null;
        private enum PlayerState
        {
            Unknown = 0,
            Running = 1,
            Paused = 2,
            Stopped = 3,
        }
        private enum SliderDragState
        {
            Unknown = 0,
            DragTrue = 1,
            DragFalse = 2,
            DragIntervalReached = 3,
        }

        public Projection Projection
        {
            get { return p; }
            set { p = value; }
        }

        public ProjectionTimelineMetro()
        {
            InitializeComponent();
            dt = new System.Windows.Threading.DispatcherTimer();
            dt.Tick += new EventHandler(OneSecondTick);
            dt.Interval = new TimeSpan(100 * 10000); sldTimeline.ApplyTemplate();

            checker = new System.Windows.Threading.DispatcherTimer();
            checker.Tick += new EventHandler(CheckerTick);
            checker.Interval = new TimeSpan(100 * 10000);

            Thumb thumb = (sldTimeline.Template.FindName(
                "PART_Track", sldTimeline) as Track).Thumb;

            thumb.MouseEnter += new MouseEventHandler(thumb_MouseEnter);
            Focus();
            //Topmost = true;
        }

        private void CheckerTick(object sender, EventArgs e)
        {
            if (currentTrackID != lastTrackID || (lastTrackID == 0 && currentTrackID == lastTrackID))
            {
                checker.Stop();
                SetMedia(currentMedia);
                PlayMedia();
            }
        }

        private void thumb_MouseEnter(object sender, MouseEventArgs e)

        {

            if (e.LeftButton == MouseButtonState.Pressed && e.MouseDevice.Captured == null)
            {

                // the left button is pressed on mouse enter

                // but the mouse isn't captured, so the thumb

                // must have been moved under the mouse in response

                // to a click on the track.

                // Generate a MouseLeftButtonDown event.

                MouseButtonEventArgs args = new MouseButtonEventArgs(e.MouseDevice, e.Timestamp, MouseButton.Left);

                args.RoutedEvent = MouseLeftButtonDownEvent;

                (sender as Thumb).RaiseEvent(args);

            }

        }

        private void OneSecondTick(object sender, EventArgs e)
        {
            //sldTimeline.Value = (int)timeline.MediaPlayer.VlcMediaPlayer.Time;
            //Console.WriteLine((int)timeline.MediaPlayer.VlcMediaPlayer.Time);
            if (timeline.SourceProvider.MediaPlayer.GetMedia() == null)
            {
                sldTimeline.Value = (int)timeline.SourceProvider.MediaPlayer.Time;
            }
     
            

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            p.Close();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {

            //timeline.MediaPlayer.Pause();
            //p.projection.MediaPlayer.Pause();
            PauseTimelineAndProjection();

            //meTest.Pause();
            //long time = p.projection.MediaPlayer.VlcMediaPlayer.Time;
            //p.projection.MediaPlayer.VlcMediaPlayer.Time = time;
            //timeline.MediaPlayer.VlcMediaPlayer.Time = p.projection.MediaPlayer.VlcMediaPlayer.Time;
            //Console.WriteLine(timeline.MediaPlayer.VlcMediaPlayer.Time+" "+ p.projection.MediaPlayer.VlcMediaPlayer.Time);
            dt.Stop();
            lastPlayerState = PlayerState.Paused;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {

            //p.projection.MediaPlayer.Play();
            //timeline.MediaPlayer.Play();
            if (file == null)
            {
                System.Windows.MessageBox.Show("Não foi possível iniciar a reproducção. Selecione um arquivo de mídia e tente novamente", "Erro ao Reproduzir", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if ((currentTrackID + 1 == playList.Count) && mediaReprodutionFinished)
                {
                    lastTrackID = currentTrackID;
                    checker.Start();
                    currentMedia = playList[0];
                    currentTrackID = 0;
                    mediaReprodutionFinished = false;
                }
                else
                {
                    PlayMedia();
                    mediaReprodutionFinished = false;
                }


            }
        }

        private void TimelineWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
            var currentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var libDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
            var options = new string[]
            {
                // VLC options can be given here. Please refer to the VLC command line documentation.
            };

            vctAux.SourceProvider.CreatePlayer(libDirectory, options);

            options = new string[]
            {
                "--video-filter=adjust"
            };

            timeline.SourceProvider.CreatePlayer(libDirectory, options);
            p.projection.SourceProvider.CreatePlayer(libDirectory, options);

            vctAux.SourceProvider.MediaPlayer.Audio.Volume = 0;

            timeline.SourceProvider.MediaPlayer.Playing += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(SetProgressMax);
            timeline.SourceProvider.MediaPlayer.Audio.Volume = 0;

            p.projection.SourceProvider.MediaPlayer.EndReached += new EventHandler<VlcMediaPlayerEndReachedEventArgs>(EndReached);

            nudBrightness.Maximum = 400;
            nudBrightness.Minimum = 0;
            nudBrightness.Value = 200;

            

            nudContrast.Maximum = 400;
            nudContrast.Value = 200;

            nudSaturation.Maximum = 400;
            nudSaturation.Value = 200;

            nudGamma.Maximum = 400;
            nudGamma.Value = 200;

            nudHUE.Maximum = 400;
            nudHUE.Value = 200;

            //p.projection.MediaPlayer.VlcLibDirectory = new DirectoryInfo(libDirectory.FullName);
            //timeline.MediaPlayer.VlcLibDirectory = new DirectoryInfo(libDirectory.FullName);

            //p.projection.MediaPlayer.EndInit();
            //timeline.MediaPlayer.EndInit();
            //p.projection.MediaPlayer.SetMedia(new FileInfo(@"C:\test.mkv"));
            //timeline.MediaPlayer.SetMedia(new FileInfo(@"C:\test.mkv"));
            //timeline.MediaPlayer.Playing += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(SetProgressMax);
            //timeline.MediaPlayer.Audio.Volume = 0;


            Focus();
        }

        private void EndReached(object sender, VlcMediaPlayerEndReachedEventArgs e)
        {
            Console.WriteLine("FIM!");
            Dispatcher.Invoke(new Action(() =>
            {
                mediaReprodutionFinished = true;
                dt.Stop();
                lastTrackID = currentTrackID;
                sldTimeline.Value = sldTimeline.Maximum;
                if (currentTrackID + 1 < playList.Count)
                {
                    checker.Start();
                    currentMedia = playList[currentTrackID + 1];
                    currentTrackID = currentTrackID + 1;
                }
            }));
        }

        private int GetCurrentTrackID()
        {
            return Convert.ToInt32(timeline.SourceProvider.MediaPlayer.GetMedia().TrackID);
        }

        private void SetMedia(Media media)
        {
            currentTrackID = Convert.ToInt32(media.TrackID);
            Console.WriteLine(media.TrackID + " " + media.URL);
            timeline.SourceProvider.MediaPlayer.SetMedia(new FileInfo(media.URL));
            p.projection.SourceProvider.MediaPlayer.SetMedia(new FileInfo(media.URL));
            timeline.SourceProvider.MediaPlayer.Playing += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(SetProgressMax);
        }

        private void PlayMedia()
        {
            
            timeline.SourceProvider.MediaPlayer.Play();
            p.projection.SourceProvider.MediaPlayer.Play();
            
            //p.projection.SourceProvider.MediaPlayer.Time = timeline.SourceProvider.MediaPlayer.Time;
            //timeline.SourceProvider.MediaPlayer.Time = p.projection.SourceProvider.MediaPlayer.Time;
            dt.Start();
            lastPlayerState = PlayerState.Running;
            mediaReprodutionFinished = false;
        }

        private void SetProgressMax(object sender, VlcMediaPlayerPlayingEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                var vlc = (VlcMediaPlayer)sender;
                sldTimeline.Maximum = (int)vlc.Length;
                Console.WriteLine("Nome:" + timeline.SourceProvider.MediaPlayer.GetMedia().Title);
                Console.WriteLine("Duração (Lenght):" + (int)vlc.Length);
                //timeline.MediaPlayer.Playing -= new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(SetProgressMax);
                timeline.SourceProvider.MediaPlayer.Playing -= new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(SetProgressMax);

            }
            ));
        }

        private void PauseTimeline()
        {
            if (timeline.SourceProvider.MediaPlayer.IsPlaying())
            {
                timeline.SourceProvider.MediaPlayer.Pause();
            }
        }

        private void PauseProjection()
        {
            if (p.projection.SourceProvider.MediaPlayer.IsPlaying())
            {
                p.projection.SourceProvider.MediaPlayer.Pause();
            }
        }

        private void PauseTimelineAndProjection()
        {
            timeline.SourceProvider.MediaPlayer.Time = p.projection.SourceProvider.MediaPlayer.Time;
            p.projection.SourceProvider.MediaPlayer.Time = timeline.SourceProvider.MediaPlayer.Time;
            PauseTimeline();
            PauseProjection();
        }

        private void cbFlipHorizontally_Checked(object sender, RoutedEventArgs e)
        {
            //timeline.RenderTransform.

        }

        private void sldTimeline_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (dt.IsEnabled == false)
            {
                //timeline.MediaPlayer.VlcMediaPlayer.Time = (long)sldTimeline.Value;

                timeline.SourceProvider.MediaPlayer.Time = (long)sldTimeline.Value;


                //p.projection.MediaPlayer.VlcMediaPlayer.Time = timeline.MediaPlayer.VlcMediaPlayer.Time;
                //p.projection.SourceProvider.MediaPlayer.Time = timeline.SourceProvider.MediaPlayer.Time;
                // timeline.MediaPlayer.VlcMediaPlayer.Time = p.projection.MediaPlayer.VlcMediaPlayer.Time;
                //timeline.SourceProvider.MediaPlayer.Time = p.projection.SourceProvider.MediaPlayer.Time;
                // p.projection.MediaPlayer.VlcMediaPlayer.Time = timeline.MediaPlayer.VlcMediaPlayer.Time;
                //Console.WriteLine("Changed! " + (long)sldTimeline.Value);
                /*
                Task.Factory.StartNew(() => {
                    Thread.Sleep(150);
                    if(lastSliderDragState == SliderDragState.DragTrue)
                    {
                        sldTimeline.Value = timeline.MediaPlayer.VlcMediaPlayer.Time;
                        lastSliderDragState = SliderDragState.DragIntervalReached;
                    }
                });
                */

            }
        }

        private void sldTimeline_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void sldTimeline_DragLeave(object sender, DragEventArgs e)
        {
        }

        private void sldTimeline_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            dt.Start();
            dt.IsEnabled = true;
            /*
            if(lastSliderDragState == SliderDragState.DragIntervalReached)
            {
                //sldTimeline.Value = timeline.MediaPlayer.VlcMediaPlayer.Time;
                sldTimeline.Value = timeline.SourceProvider.MediaPlayer.Time;
            }
            lastSliderDragState = SliderDragState.DragFalse;
            */

            //p.projection.MediaPlayer.VlcMediaPlayer.Time = timeline.MediaPlayer.VlcMediaPlayer.Time;
            PauseTimelineAndProjection();
            //p.projection.MediaPlayer.VlcMediaPlayer.Time = (long)sldTimeline.Value;
            //timeline.MediaPlayer.VlcMediaPlayer.Time = (long)sldTimeline.Value;

            p.projection.SourceProvider.MediaPlayer.Time = (long)sldTimeline.Value;
            timeline.SourceProvider.MediaPlayer.Time = (long)sldTimeline.Value;

            if (lastPlayerState == PlayerState.Running)
            {
                //p.projection.MediaPlayer.Play();
                //timeline.MediaPlayer.Play();
                p.projection.SourceProvider.MediaPlayer.Play();
                timeline.SourceProvider.MediaPlayer.Play();
            }

            //timeline.MediaPlayer.VlcMediaPlayer.Time = p.projection.MediaPlayer.VlcMediaPlayer.Time;
            //p.projection.MediaPlayer.VlcMediaPlayer.Time = timeline.MediaPlayer.VlcMediaPlayer.Time;


            //timeline.SourceProvider.MediaPlayer.Time = p.projection.SourceProvider.MediaPlayer.Time;

            //p.projection.SourceProvider.MediaPlayer.Time = timeline.SourceProvider.MediaPlayer.Time;
            //timeline.SourceProvider.MediaPlayer.Time = p.projection.SourceProvider.MediaPlayer.Time;


            timeline.SourceProvider.MediaPlayer.Time = p.projection.SourceProvider.MediaPlayer.Time;
            p.projection.SourceProvider.MediaPlayer.Time = timeline.SourceProvider.MediaPlayer.Time;
        }

        private void sldTimeline_DragStarted(object sender, DragStartedEventArgs e)
        {
            lastSliderDragState = SliderDragState.DragTrue;
            dt.Stop();
            dt.IsEnabled = false;
        }



        private void sldTimeline_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dt.Stop();
            dt.IsEnabled = false;
        }

        private void sldTimeline_ManipulationStarting(object sender, ManipulationStartingEventArgs e)
        {
            dt.Stop();
        }

        private void sldTimeline_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void sldTimeline_IsMouseDirectlyOverChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void sldTimeline_GotMouseCapture(object sender, MouseEventArgs e)
        {

        }

        private void cbxInvertVertically_Checked(object sender, RoutedEventArgs e)
        {
            InvertPlayer();
        }

        private void cbxInvertHorizontally_Checked(object sender, RoutedEventArgs e)
        {
            InvertPlayer();
        }

        private void InvertPlayer()
        {
            ScaleTransform defaultScaleTransform = new ScaleTransform();

            ScaleTransform myScaleTransform = new ScaleTransform();
            //myScaleTransform.ScaleY = 1;


            RotateTransform myRotateTransform = new RotateTransform();
            myRotateTransform.Angle = 0;

            TranslateTransform myTranslate = new TranslateTransform();
            //myTranslate.X = 12;
            //myTranslate.X = 15;

            SkewTransform mySkew = new SkewTransform();
            mySkew.AngleX = 0;
            mySkew.AngleY = 0;
            TransformGroup myTransformGroup = new TransformGroup();
            myTransformGroup.Children.Add(myScaleTransform);
            myTransformGroup.Children.Add(myRotateTransform);
            myTransformGroup.Children.Add(myTranslate);
            myTransformGroup.Children.Add(mySkew);

            TransformGroup defaultTransformGroup = new TransformGroup();
            myTransformGroup.Children.Add(defaultScaleTransform);
            myTransformGroup.Children.Add(myRotateTransform);
            myTransformGroup.Children.Add(myTranslate);
            myTransformGroup.Children.Add(mySkew);

            if (cbxInvertHorizontally.IsChecked == true)
            {
                myScaleTransform.ScaleX = -1;
            }
            else
            {
                myScaleTransform.ScaleX = 1;
            }
            if (cbxInvertVertically.IsChecked == true)
            {
                myScaleTransform.ScaleY = -1;
            }
            else
            {
                myScaleTransform.ScaleY = 1;
            }

            p.projection.RenderTransform = myTransformGroup;

            if (!cbxInvertOnlyProjection.IsChecked == true)
            {
                timeline.RenderTransform = myTransformGroup;
            }
            else
            {
                defaultScaleTransform.ScaleX = 1;
                defaultScaleTransform.ScaleY = 1;
                timeline.RenderTransform = defaultTransformGroup;
            }


        }

        private void cbxInvertHorizontally_Click(object sender, RoutedEventArgs e)
        {
            InvertPlayer();
        }

        private void cbxInvertVertically_Click(object sender, RoutedEventArgs e)
        {
            InvertPlayer();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            //timeline.MediaPlayer.Stop();
            //p.projection.MediaPlayer.Stop();

            PauseTimelineAndProjection();
            p.projection.SourceProvider.MediaPlayer.Time = 0;
            timeline.SourceProvider.MediaPlayer.Time = 0;
            dt.Stop();
            sldTimeline.Value = 0;

            lastPlayerState = PlayerState.Stopped;
        }

        private void cbxInvertOnlyProjection_Click(object sender, RoutedEventArgs e)
        {
            InvertPlayer();
        }

        private void btnSync_Click(object sender, RoutedEventArgs e)
        {
            p.projection.SourceProvider.MediaPlayer.Time = timeline.SourceProvider.MediaPlayer.Time;
            timeline.SourceProvider.MediaPlayer.Time = p.projection.SourceProvider.MediaPlayer.Time;
            p.FullScreen();
        }

        private void sldVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.IsLoaded)
            {
                p.projection.SourceProvider.MediaPlayer.Audio.Volume = (int)sldVolume.Value;
                lblVolume.Content = "Volume: " + (int)sldVolume.Value + "%";
            }
        }

        private void btnAddFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            //dlg.DefaultExt = ".mp4";
            dlg.Filter = "Video Files (*.*)|*.*";


            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                file = dlg.FileName;
                if (p.projection.SourceProvider.MediaPlayer.IsPlaying() == false && lastPlayerState != PlayerState.Paused)
                {
                    timeline.SourceProvider.MediaPlayer.Playing += new EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(SetProgressMax);
                    PauseTimelineAndProjection();
                    p.projection.SourceProvider.MediaPlayer.Time = 0;
                    timeline.SourceProvider.MediaPlayer.Time = 0;
                    dt.Stop();
                    sldTimeline.Value = 0;
                    lastPlayerState = PlayerState.Stopped;

                    if (playList.Count == 0)
                    {
                        timeline.SourceProvider.MediaPlayer.SetMedia(new FileInfo(file));
                        p.projection.SourceProvider.MediaPlayer.SetMedia(new FileInfo(file));
                    }
                }
                addToPlaylist(file, dlg.SafeFileName);



                Console.WriteLine(
                    //timeline.SourceProvider.MediaPlayer.GetMedia().Title + " \n" +
                    //timeline.SourceProvider.MediaPlayer.GetMedia().Duration + " \n" +
                    //timeline.SourceProvider.MediaPlayer.GetMedia().TrackID+" \n"+
                    //timeline.SourceProvider.MediaPlayer.GetMedia().TrackNumber + " \n" +
                    //timeline.SourceProvider.MediaPlayer.GetMedia().State + " \n" +
                    //timeline.SourceProvider.MediaPlayer.State
                    );
                //timeline.SourceProvider.MediaPlayer = p.projection.SourceProvider.MediaPlayer;
            }
        }

        private void cbxIsVideoVisible_Click(object sender, RoutedEventArgs e)
        {
            if (cbxIsVideoVisible.IsChecked == true)
            {
                timeline.Visibility = Visibility.Visible;
                p.projection.Visibility = Visibility.Visible;
            }
            else
            {
                timeline.Visibility = Visibility.Hidden;
                p.projection.Visibility = Visibility.Hidden;
            }
        }

        private void cbxIsProjectionVisible_Click(object sender, RoutedEventArgs e)
        {
            if (cbxIsProjectionVisible.IsChecked == true)
            {
                p.projection.Visibility = Visibility.Visible;
            }
            else
            {

                p.projection.Visibility = Visibility.Hidden;

            }
        }

        private void btnMute_Click(object sender, RoutedEventArgs e)
        {
            if (!isMuted)
            {
                btnMute.Content = "Unmute";
                lastVolumeValue = (int)sldVolume.Value;
                sldVolume.Value = 0;
                sldVolume.IsEnabled = false;
                lblVolume.Content = "Volume: Mute";
                isMuted = true;
            }
            else
            {
                btnMute.Content = "Mute";
                sldVolume.Value = lastVolumeValue;
                sldVolume.IsEnabled = true;
                isMuted = false;
            }
        }

        private void addToPlaylist(string filePath, string fileName)
        {
            vctAux.SourceProvider.MediaPlayer.SetMedia(filePath);
            vctAux.SourceProvider.MediaPlayer.Audio.Volume = 0;
            vctAux.SourceProvider.MediaPlayer.Play();
            Media newMedia = new Media(filePath, fileName, playList.Count);

            newMedia.Duration = vctAux.SourceProvider.MediaPlayer.Length;
            TimeSpan t = TimeSpan.FromMilliseconds(vctAux.SourceProvider.MediaPlayer.Length);
            vctAux.SourceProvider.MediaPlayer.Stop();
            newMedia.URL = filePath;
            //if (newMedia.Title.Equals(filePath.Replace(fileName, "")))
            //{
            //    newMedia.Title = fileName;
            //}
            playList.Add(newMedia);
            ListBoxItem lbi = new ListBoxItem();

            string duration = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                                    t.Hours,
                                    t.Minutes,
                                    t.Seconds);
            lbi.Content = newMedia.TrackID + " - " + fileName + " - " + duration + " - " + newMedia.URL;
            lsbTestList.Items.Add(lbi);
        }

        private void cbxAspectRatio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (timeline != null & p != null)
            {
                switch (cbxAspectRatio.DataContext)
                {
                    case "16:9":
                        timeline.SourceProvider.MediaPlayer.Video.AspectRatio = "16:9";
                        p.projection.SourceProvider.MediaPlayer.Video.AspectRatio = "16:9";

                        break;
                    case "4:3":
                        timeline.SourceProvider.MediaPlayer.Video.AspectRatio = "4:3";
                        p.projection.SourceProvider.MediaPlayer.Video.AspectRatio = "4:3";
                        break;
                    default:
                        timeline.SourceProvider.MediaPlayer.Video.AspectRatio = "";
                        p.projection.SourceProvider.MediaPlayer.Video.AspectRatio = "";
                        break;
                }
            }
        }

        private void NumericUpDown_ValueChanged(object sender, ControlLib.ValueChangedEventArgs e)
        {
            timeline.SourceProvider.MediaPlayer.Video.Adjustments.Brightness = (float)nudBrightness.Value / 200;
            p.projection.SourceProvider.MediaPlayer.Video.Adjustments.Brightness = (float)nudBrightness.Value / 200;
        }

        private void nudContrast_ValueChanged(object sender, ControlLib.ValueChangedEventArgs e)
        {
            timeline.SourceProvider.MediaPlayer.Video.Adjustments.Contrast = (float)nudContrast.Value / 200;
            p.projection.SourceProvider.MediaPlayer.Video.Adjustments.Contrast = (float)nudContrast.Value / 200;
        }

        private void nudSaturation_ValueChanged(object sender, ControlLib.ValueChangedEventArgs e)
        {
            timeline.SourceProvider.MediaPlayer.Video.Adjustments.Saturation = (float)nudSaturation.Value / 200;
            p.projection.SourceProvider.MediaPlayer.Video.Adjustments.Saturation = (float)nudSaturation.Value / 200;
        }

        private void nudGama_ValueChanged(object sender, ControlLib.ValueChangedEventArgs e)
        {
            timeline.SourceProvider.MediaPlayer.Video.Adjustments.Gamma = (float)nudGamma.Value / 200;
            p.projection.SourceProvider.MediaPlayer.Video.Adjustments.Gamma = (float)nudGamma.Value / 200;
        }

        private void nudHUD_ValueChanged(object sender, ControlLib.ValueChangedEventArgs e)
        {
            timeline.SourceProvider.MediaPlayer.Video.Adjustments.Hue = (float)nudHUE.Value / 200;
            p.projection.SourceProvider.MediaPlayer.Video.Adjustments.Hue = (float)nudHUE.Value / 200;
        }

        private void TimelineWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.Cursor.Clip = p.workingArea;
        }

        private void nudMetro_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {

        }

        private void NumericUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            timeline.SourceProvider.MediaPlayer.Video.Adjustments.Brightness = (float)nudBrightness.Value / 200;
            p.projection.SourceProvider.MediaPlayer.Video.Adjustments.Brightness = (float)nudBrightness.Value / 200;
        }

        private void nudContrast_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            timeline.SourceProvider.MediaPlayer.Video.Adjustments.Contrast = (float)nudContrast.Value / 200;
            p.projection.SourceProvider.MediaPlayer.Video.Adjustments.Contrast = (float)nudContrast.Value / 200;
        }

        private void nudSaturation_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            timeline.SourceProvider.MediaPlayer.Video.Adjustments.Saturation = (float)nudSaturation.Value / 200;
            p.projection.SourceProvider.MediaPlayer.Video.Adjustments.Saturation = (float)nudSaturation.Value / 200;
        }

        private void nudGamma_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            timeline.SourceProvider.MediaPlayer.Video.Adjustments.Gamma = (float)nudGamma.Value / 200;
            p.projection.SourceProvider.MediaPlayer.Video.Adjustments.Gamma = (float)nudGamma.Value / 200;
        }

        private void nudHUE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double?> e)
        {
            timeline.SourceProvider.MediaPlayer.Video.Adjustments.Hue = (float)nudHUE.Value / 200;
            p.projection.SourceProvider.MediaPlayer.Video.Adjustments.Hue = (float)nudHUE.Value / 200;
        }
    }
}
