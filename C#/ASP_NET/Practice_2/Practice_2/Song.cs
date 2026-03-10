namespace Practice_2
{
    public class Song
    {
        public int songID { get; set; }
        public string title { get; set; }
        public string artist { get; set; }
        public double duration { get; set; }
        public string genre { get; set; }
        public bool isLiked { get; set; }
        public int playCount { get; set; }
        public Song() { }
        public Song(int songID, string title, string artist, double duration, string genre, bool isLiked = false, int playCount = 0)
        {
            this.songID = songID;
            this.title = title;
            this.artist = artist;
            this.duration = duration;
            this.genre = genre;
            this.isLiked = isLiked;
            this.playCount = playCount;
        }
        public Song( string title, string artist, double duration, string genre, bool isLiked = false, int playCount = 0)
        {
            this.title = title;
            this.artist = artist;
            this.duration = duration;
            this.genre = genre;
            this.isLiked = isLiked;
            this.playCount = playCount;
        }
        public void playSong()
        {
            this.playCount++;
        }
        public void DisplaySongInfo()
        {
            Console.WriteLine($"Song ID: {songID}");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Artist: {artist}");
            Console.WriteLine($"Duration: {duration} minutes");
            Console.WriteLine($"Genere: {genre}");
            Console.WriteLine($"Is Liked: {isLiked}");
            Console.WriteLine($"Play Count: {playCount}");
        }

    }
}
