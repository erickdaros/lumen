using System;
using System.ComponentModel;
using System.IO.MemoryMappedFiles;
using System.Windows.Media;
using System.Windows.Threading;
using Vlc.DotNet.Core;
using Vlc.DotNet.Wpf;

namespace MPMS___Projection_Management_System
{
    // Token: 0x02000004 RID: 4
    public class VlcVideoSourceProviderModded : VlcVideoSourceProvider, INotifyPropertyChanged, IDisposable
    {
        // Token: 0x0600000C RID: 12 RVA: 0x0000233B File Offset: 0x0000053B
        public VlcVideoSourceProviderModded(Dispatcher dis) : base(dis)
        {
            dispatcher = dis;
        }

        // Token: 0x17000006 RID: 6
        // (get) Token: 0x0600000D RID: 13 RVA: 0x0000234A File Offset: 0x0000054A
        // (set) Token: 0x0600000E RID: 14 RVA: 0x00002352 File Offset: 0x00000552
        public ImageSource VideoSource
        {
            get
            {
                return this.videoSource;
            }
            private set
            {
                if (this.videoSource != value)
                {
                    this.videoSource = value;
                    this.OnPropertyChanged("VideoSource");
                }
            }
        }

        // Token: 0x17000007 RID: 7
        // (get) Token: 0x0600000F RID: 15 RVA: 0x0000236F File Offset: 0x0000056F
        // (set) Token: 0x06000010 RID: 16 RVA: 0x00002377 File Offset: 0x00000577
        public VlcMediaPlayer MediaPlayer { get; set; }

        // Token: 0x06000011 RID: 17 RVA: 0x00002380 File Offset: 0x00000580

        // Token: 0x06000012 RID: 18 RVA: 0x000023F7 File Offset: 0x000005F7
        private uint GetAlignedDimension(uint dimension, uint mod)
        {
            if (dimension % mod == 0u)
            {
                return dimension;
            }
            return dimension + mod - dimension % mod;
        }

        // Token: 0x04000008 RID: 8
        private MemoryMappedFile memoryMappedFile;

        // Token: 0x04000009 RID: 9
        private MemoryMappedViewAccessor memoryMappedView;

        // Token: 0x0400000A RID: 10
        private ImageSource videoSource;

        // Token: 0x0400000B RID: 11
        private static Dispatcher dispatcher;

        // Token: 0x0400000D RID: 13
        private bool disposedValue;
    }
}
