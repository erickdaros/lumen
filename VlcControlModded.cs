using MPMS___Projection_Management_System;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Vlc.DotNet.Wpf;

namespace VlcControlModded
{
    // Token: 0x02000003 RID: 3
    public class VlcControlModded : UserControl, IDisposable
    {
        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000009 RID: 9 RVA: 0x00002292 File Offset: 0x00000492
        public VlcVideoSourceProvider SourceProvider
        {
            get
            {
                return this.sourceProvider;
            }

        }

        // Token: 0x0600000A RID: 10 RVA: 0x0000229C File Offset: 0x0000049C
        public VlcControlModded()
        {
            this.sourceProvider = new VlcVideoSourceProviderModded(base.Dispatcher);
            this.viewBox = new Viewbox
            {
                Child = this.videoContent,
                Stretch = Stretch.Uniform
            };
            base.Content = this.viewBox;
            base.Background = Brushes.Black;
            this.videoContent.SetBinding(Image.SourceProperty, new Binding("VideoSource")
            {
                Source = this.sourceProvider
            });
        }

        // Token: 0x0600000B RID: 11 RVA: 0x0000232E File Offset: 0x0000052E
        public void Dispose()
        {
            this.sourceProvider.Dispose();
        }

        // Token: 0x04000005 RID: 5
        private Viewbox viewBox;

        // Token: 0x04000006 RID: 6
        private readonly Image videoContent = new Image
        {
            ClipToBounds = true
        };

        // Token: 0x04000007 RID: 7
        private VlcVideoSourceProviderModded sourceProvider;
    }
}
