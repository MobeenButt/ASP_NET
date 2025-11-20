using System;
using Microsoft.Data.SqlClient;

namespace Practice_2
{
    public class MusicDatabase
    {
        // connection string 
        public string? connString = @"Data Source=(localdb)\ProjectModels;Initial Catalog=Music;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Command Timeout=30";
        private SqlConnection con;

        public MusicDatabase()
        {
            con = new SqlConnection(connString);
        }

        public void OpenConnection()
        {
            if (con.State != System.Data.ConnectionState.Open)
            {
                con.Open();
                Console.WriteLine("Connection Opened Successfully");
            }
        }

        public void CloseConnection()
        {
            if (con.State != System.Data.ConnectionState.Closed)
            {
                con.Close();
                Console.WriteLine("Connection Closed Successfully");
            }
        }

        public void InsertSong(Song song)
        {
            string query = "INSERT INTO Songs (title, artist, duration, genre, isLiked, playCount) VALUES (@title, @artist, @duration, @genre, @isLiked, @playCount)";
            
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@title", song.title ?? string.Empty);
                    cmd.Parameters.AddWithValue("@artist", song.artist ?? string.Empty);
                    cmd.Parameters.AddWithValue("@duration", song.duration);
                    cmd.Parameters.AddWithValue("@genre", song.genre ?? string.Empty);
                    cmd.Parameters.AddWithValue("@isLiked", song.isLiked);
                    cmd.Parameters.AddWithValue("@playCount", song.playCount);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        Console.WriteLine($"Song '{song.title}' inserted successfully");
                    }
                    else
                    {
                        Console.WriteLine("Error inserting song");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        // GET ALL SONGS from DATABASE using ADO.NET 
        public void GetAllSongs()
        {
            string query = "SELECT songid, title, artist, duration, genre, isliked, playcount FROM Songs";
            
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (!dr.HasRows)
                        {
                            Console.WriteLine("No songs found in database.");
                            return;
                        }

                        int count = 0;
                        while (dr.Read())
                        {
                            int id = dr.GetInt32(0);
                            string title = dr.GetString(1);
                            string artist = dr.GetString(2);
                            double duration = dr.GetDouble(3);
                            string genre = dr.GetString(4);
                            bool isLiked = dr.GetBoolean(5);
                            int playCount = dr.GetInt32(6);

                            Console.WriteLine($"\n[Song {++count}]");
                            Console.WriteLine($"Id: {id}, Title: {title}, Artist: {artist}, Duration: {duration}, Genre: {genre}, Liked: {isLiked}, PlayCount: {playCount}");
                        }
                        Console.WriteLine($"\nTotal songs retrieved: {count}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void GetSongCount()
        {
            string query = "SELECT COUNT(*) FROM Songs";
            
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    object result = cmd.ExecuteScalar();
                    int count = Convert.ToInt32(result);
                    Console.WriteLine($"Total songs in database: {count}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

        public string GetSongById(int id)
        {
            string query = "SELECT songid, title, artist, duration, genre, isliked, playcount FROM Songs WHERE songid = @id";
            
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id); // Fixed: was missing this parameter
                    
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            int songId = dr.GetInt32(0);
                            string title = dr.GetString(1);
                            string artist = dr.GetString(2);
                            double duration = dr.GetDouble(3);
                            string genre = dr.GetString(4);
                            bool isLiked = dr.GetBoolean(5);
                            int playCount = dr.GetInt32(6);

                            return $"ID: {songId}, Title: {title}, Artist: {artist}, Duration: {duration}, Genre: {genre}, Liked: {isLiked}, PlayCount: {playCount}";
                        }
                        else
                        {
                            return $"Song with ID: {id} not found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
