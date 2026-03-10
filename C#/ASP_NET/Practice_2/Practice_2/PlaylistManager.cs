using System;
using System.Collections.Generic;

namespace Practice_2
{
    public class PlaylistManager
    {
        public List<Song> songs;
        public string PlaylistName;
        public int maxSongs = 100;

        public PlaylistManager()
        {
            songs = new List<Song>();
        }

        public void AddMultipleSongs(params Song[] songs)
        {
            foreach (var song in songs)
            {
                if (this.songs.Count < maxSongs)
                {
                    this.songs.Add(song);
                }
                else
                {
                    Console.WriteLine("Playlist is full. Cannot add more songs.");
                    break;
                }
            }
        }

        public void createPlaylist(string name, int maxSongs = 100)
        {
            this.PlaylistName = name;
            this.maxSongs = maxSongs;
            Console.WriteLine($"Playlist '{name}' initialized with max capacity: {maxSongs}");
        }

        public List<Song> findSongsByGenre(string genre)
        {
            List<Song> genreSongs = new List<Song>();
            foreach (var song in this.songs) // Fixed: was iterating over empty genreSongs
            {
                if (song.genre != null && song.genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
                {
                    genreSongs.Add(song);
                }
            }
            return genreSongs;
        }

        public void getPlaylistStatistics()
        {
            if (songs == null || songs.Count == 0)
            {
                Console.WriteLine("No songs in playlist.");
                return;
            }

            Console.WriteLine($"Playlist Name: {PlaylistName ?? "Unnamed"}");
            Console.WriteLine($"Total Songs: {songs.Count}");

            double totalDuration = 0;
            int likedCount = 0;
            int totalPlayCount = 0;

            foreach (var song in songs)
            {
                totalDuration += song.duration;
                if (song.isLiked)
                {
                    likedCount++;
                }
                totalPlayCount += song.playCount;
            }

            Console.WriteLine($"Total Duration: {totalDuration:F2} minutes");
            Console.WriteLine($"Total Liked Songs: {likedCount}");
            Console.WriteLine($"Total Play Count: {totalPlayCount}");
            Console.WriteLine($"Average Duration: {(totalDuration / songs.Count):F2} minutes");
        }
    }
}