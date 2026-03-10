using System;

namespace Practice_2
{
    public class Program
    {
        static Song song = new Song();
        static PlaylistManager pm = new PlaylistManager();
        static FileManager fm = new FileManager();
        static MusicDatabase musicdb = new MusicDatabase();

        static void Main(string[] args)
        {
            int option;
            do
            {
                DisplayMenu();
                Console.Write("Enter your choice: ");
                                                                                                                                                                                                                                                                            
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    PressAnyKey();
                    continue;
                }

                try
                {
                    switch (option)
                    {
                        case 1:
                            CreateSongs();
                            break;
                        case 2:
                            DisplayAllSongs();
                            break;
                        case 3:
                            SaveSongsToTextFile();
                            break;
                        case 4:
                            LoadSongsFromTextFile();
                            break;
                        case 5:
                            InsertSongsIntoDatabase();
                            break;
                        case 6:
                            DisplayAllSongsFromDatabase();
                            break;
                        case 7:
                            GetSongByID();
                            break;
                        case 8:
                            GetTotalSongCount();
                            break;
                        case 9:
                            CreatePlaylist();
                            break;
                        case 10:
                            DisplayPlaylistStatistics();
                            break;
                        case 11:
                            FindSongsByGenre();
                            break;
                        case 12:
                            PlayASong();
                            break;
                        case 13:
                            Console.WriteLine("Thank you for using Music Database Application!");
                            break;
                        default:
                            Console.WriteLine("Invalid option! Please select between 1-13.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                if (option != 13)
                {
                    PressAnyKey();
                }
            } while (option != 13);
        }

        static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Music Database Application ===");
            Console.WriteLine("1. Create Songs (At least 5 songs with details)");
            Console.WriteLine("2. Display All Songs");
            Console.WriteLine("3. Save Songs to Text File");
            Console.WriteLine("4. Load Songs from Text File");
            Console.WriteLine("5. Insert Songs into Database");
            Console.WriteLine("6. Display All Songs from Database");
            Console.WriteLine("7. Get Song by ID (Database)");
            Console.WriteLine("8. Get Total Song Count");
            Console.WriteLine("9. Create Playlist");
            Console.WriteLine("10. Display Playlist Statistics");
            Console.WriteLine("11. Find Songs by Genre");
            Console.WriteLine("12. Play a Song (Increase PlayCount)");
            Console.WriteLine("13. Exit");
            Console.WriteLine();
        }

        static void PressAnyKey()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        public static void CreateSongs()
        {
            Console.WriteLine("=== Creating Songs ===");
            int count = 0;
            while (count < 5)
            {
                Console.WriteLine($"\nCreating Song {count + 1}/5");

                Console.Write("Enter artist name: ");
                string? artist = Console.ReadLine();

                Console.Write("Enter song title: ");
                string? title = Console.ReadLine();

                double duration;
                Console.Write("Enter song duration (in minutes): ");
                while (!double.TryParse(Console.ReadLine(), out duration) || duration <= 0)
                {
                    Console.Write("Invalid duration! Enter a positive number: ");
                }

                Console.Write("Enter song genre: ");
                string? genre = Console.ReadLine();

                Console.Write("Is the song liked? (yes/no): ");
                string? isLikedInput = Console.ReadLine();
                bool isLiked = !string.IsNullOrEmpty(isLikedInput) && isLikedInput.ToLower() == "yes";

                int playCount;
                Console.Write("Enter play count: ");
                while (!int.TryParse(Console.ReadLine(), out playCount) || playCount < 0)
                {
                    Console.Write("Invalid play count! Enter a non-negative number: ");
                }

                song = new Song(title, artist, duration, genre, isLiked, playCount);
                pm.AddMultipleSongs(song);
                count++;
                Console.WriteLine("Song added successfully!");
            }
            Console.WriteLine($"\n{count} Songs Created!");
        }

        public static void DisplayAllSongs()
        {
            Console.WriteLine("=== Displaying All Songs ===");
            if (pm.songs == null || pm.songs.Count == 0)
            {
                Console.WriteLine("No songs found! Please create songs first.");
                return;
            }

            for (int i = 0; i < pm.songs.Count; i++)
            {
                Console.WriteLine($"\n[Song {i + 1}]");
                pm.songs[i].DisplaySongInfo();
                Console.WriteLine("---");
            }
        }

        public static void SaveSongsToTextFile()
        {
            Console.WriteLine("=== Saving Songs to Text File ===");
            if (pm.songs == null || pm.songs.Count == 0)
            {
                Console.WriteLine("No songs to save! Please create songs first.");
                return;
            }

            fm.SaveSongsToText(pm.songs, "songs.txt");
            Console.WriteLine("Songs saved to songs.txt successfully!");
        }

        public static void LoadSongsFromTextFile()
        {
            Console.WriteLine("=== Loading Songs from Text File ===");
            try
            {
                var loadedSongs = fm.LoadSongsFromText("songs.txt");
                if (loadedSongs == null || loadedSongs.Count == 0)
                {
                    Console.WriteLine("No songs found in file or file doesn't exist.");
                    return;
                }

                Console.WriteLine($"Loaded {loadedSongs.Count} songs from file:");
                foreach (var songItem in loadedSongs)
                {
                    songItem.DisplaySongInfo();
                    Console.WriteLine("---");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading songs: {ex.Message}");
            }
        }

        public static void InsertSongsIntoDatabase()
        {
            Console.WriteLine("=== Inserting Songs into Database ===");
            if (pm.songs == null || pm.songs.Count == 0)
            {
                Console.WriteLine("No songs to insert! Please create songs first.");
                return;
            }

            int successCount = 0;
            foreach (var songItem in pm.songs)
            {
                try
                {
                    musicdb.InsertSong(songItem);
                    successCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inserting song '{songItem.title}': {ex.Message}");
                }
            }
            Console.WriteLine($"Finished inserting songs. {successCount} out of {pm.songs.Count} songs inserted successfully.");
        }

        public static void DisplayAllSongsFromDatabase()
        {
            Console.WriteLine("=== Displaying All Songs from Database ===");
            try
            {
                musicdb.GetAllSongs();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving songs from database: {ex.Message}");
            }
        }

        public static void GetSongByID()
        {
            Console.WriteLine("=== Getting Song by ID ===");
            Console.Write("Enter Song ID to retrieve: ");

            if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
            {
                Console.WriteLine("Invalid ID! Please enter a positive number.");
                return;
            }

            try
            {
                string result = musicdb.GetSongById(id);
                Console.WriteLine(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving song: {ex.Message}");
            }
        }

        public static void GetTotalSongCount()
        {
            Console.WriteLine("=== Getting Total Song Count ===");
            try
            {
                musicdb.GetSongCount();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting song count: {ex.Message}");
            }
        }

        public static void CreatePlaylist()
        {
            Console.WriteLine("=== Creating Playlist ===");
            Console.Write("Enter Playlist Name: ");
            string? name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Invalid playlist name!");
                return;
            }

            Console.Write("Enter maximum number of songs (default 100): ");
            if (!int.TryParse(Console.ReadLine(), out int maxSongs) || maxSongs <= 0)
            {
                maxSongs = 100;
            }

            try
            {
                pm.createPlaylist(name, maxSongs);
                Console.WriteLine($"Playlist '{name}' created successfully with max capacity of {maxSongs} songs!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating playlist: {ex.Message}");
            }
        }

        public static void DisplayPlaylistStatistics()
        {
            Console.WriteLine("=== Displaying Playlist Statistics ===");
            try
            {
                pm.getPlaylistStatistics();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error displaying statistics: {ex.Message}");
            }
        }

        public static void FindSongsByGenre()
        {
            Console.WriteLine("=== Finding Songs by Genre ===");
            Console.Write("Enter Genre to search: ");
            string? genre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(genre))
            {
                Console.WriteLine("Invalid genre!");
                return;
            }

            try
            {
                var genreSongs = pm.findSongsByGenre(genre);
                if (genreSongs == null || genreSongs.Count == 0)
                {
                    Console.WriteLine($"No songs found for genre '{genre}'.");
                    return;
                }

                Console.WriteLine($"Found {genreSongs.Count} song(s) for genre '{genre}':");
                foreach (var songItem in genreSongs)
                {
                    songItem.DisplaySongInfo();
                    Console.WriteLine("---");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching songs: {ex.Message}");
            }
        }

        public static void PlayASong()
        {
            Console.WriteLine("=== Playing a Song ===");
            if (pm.songs == null || pm.songs.Count == 0)
            {
                Console.WriteLine("No songs available! Please create songs first.");
                return;
            }

            Console.WriteLine("Available songs:");
            for (int i = 0; i < pm.songs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {pm.songs[i].title} by {pm.songs[i].artist}");
            }

            Console.Write("\nEnter song number to play: ");
            if (!int.TryParse(Console.ReadLine(), out int songIndex) || songIndex < 1 || songIndex > pm.songs.Count)
            {
                Console.WriteLine("Invalid song number!");
                return;
            }

            try
            {
                var selectedSong = pm.songs[songIndex - 1];
                selectedSong.playSong();
                Console.WriteLine($"\nPlaying: {selectedSong.title} by {selectedSong.artist}");
                Console.WriteLine($"Play count increased to: {selectedSong.playCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing song: {ex.Message}");
            }
        }
    }
}