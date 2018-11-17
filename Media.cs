namespace MPMS___Projection_Management_System.Model
{
    class Media
    {
        public string Title { get; set; }
        public string FileName { get; set; }
        public long Duration { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public string Copyright { get; set; }
        public string Album { get; set; }
        public string TrackNumber { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string Date { get; set; }
        public string Setting { get; set; }
        public string URL { get; set; }
        public string Language { get; set; }
        public string NowPlaying { get; set; }
        public string Publisher { get; set; }
        public string EncodedBy { get; set; }
        public string ArtworkURL { get; set; }
        public int TrackID { get; set; }

        public Media(string URL, string FileName, int TrackID)
        {
            this.URL = URL;
            this.FileName = FileName;
            this.TrackID = TrackID;
        }
    }
}
