namespace UTorrentPostDownloadScript
{
    public class Arguments
    {
        /// <summary>%F</summary>
        public string NameOfDownloadedFileForSingleFileTorrents { get; set; }
        
        /// <summary>%D</summary>
        public string DirectoryWhereFilesAreSaved { get; set; }
        
        /// <summary>%N</summary>
        public string TitleOfTorrent { get; set; }
        
        /// <summary>%P</summary>
        public StateOfTorrent PreviousStateOfTorrent { get; set; }
        
        /// <summary>%L</summary>
        public string Label { get; set; }
        
        /// <summary>%T</summary>
        public string Tracker { get; set; }
        
        /// <summary>%M</summary>
        public string StatusMessage { get; set; }
        
        /// <summary>%I</summary>
        public string HexEndocdedInfoHash { get; set; }
        
        /// <summary>%S</summary>
        public StateOfTorrent StateOfTorrent { get; set; }
        
        /// <summary>%K</summary>
        public KindOfTorrent KindOfTorrent { get; set; }
    }
}