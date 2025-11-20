namespace Practice_2
{
    public class FileManager
    {
        public void SaveSongsToText(List<Song> songs, string filename)
        {
            using (FileStream file = new FileStream(filename, FileMode.Append))
            {
              using(StreamWriter writer =new StreamWriter(file))
              {
                foreach(var song in songs)
                {
                     writer.WriteLine($"{song.title},{song.artist},{song.duration},{song.genre},{song.isLiked},{song.playCount}");
                }
              }
            }
        }
        public List<Song> LoadSongsFromText(string filename)
        {
            List<Song> list = new List<Song>();
            using (FileStream file = new FileStream(filename, FileMode.Open))
            {
                try
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        string ?line;
                        while((line = reader.ReadLine())!= null)
                        {
                            var parts = line.Split(',');
                            if(parts.Length==6)
                            {
                                string title=parts[0];
                                string artist=parts[1];
                                double duration=double.Parse(parts[2]);
                                string genre=parts[3];
                                bool isLiked=bool.Parse(parts[4]);
                                int playCount=int.Parse(parts[5]);
                                Song song=new Song(title,artist,duration,genre,isLiked,playCount);
                                list.Add(song);
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    file.Close();
                }
            }
            return list;
        }
    }
}
