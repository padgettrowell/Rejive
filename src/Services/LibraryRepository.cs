using System;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;

namespace uAmp
{
    public class LibraryRepository
    {

        private string _connectionString = string.Empty;

        private string ConnectionString
        {
            get { return _connectionString;}
        }

        public LibraryRepository()
        {
            //If we don't have one, create a library
            var filePathName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Library.sdf";

            if (!File.Exists(filePathName))
            {
                File.Copy(new DirectoryInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).Parent.FullName + @"\Library.sdf", filePathName);
            }
            
            _connectionString = string.Format("Data Source={0};Persist Security Info=False;", filePathName);

        }

        //private const string ConnectionString = "Data Source=Library.sdf;Persist Security Info=False;";

        public int ArtistCount()
        {
            var ret = 0;
            using (var objConn = new SqlCeConnection(ConnectionString))
            {
                objConn.Open();

                //SQL CE Doesn't support (Select Count(Distinct x) from y so we're using a nested query.
                var command = new SqlCeCommand("Select count(1) from (Select Distinct Artist From Track) t", objConn);
                ret = (int)command.ExecuteScalar();
                objConn.Close();
            }

            return ret;
        }

        public int AlbumCount()
        {
            var ret = 0;
            using (var objConn = new SqlCeConnection(ConnectionString))
            {
                objConn.Open();
                var command = new SqlCeCommand("Select count(1) from (Select Distinct Album From Track) t", objConn);
                ret = (int)command.ExecuteScalar();
                objConn.Close();
            }

            return ret;
        }
        
        public int TrackCount()
        {
            var ret = 0;
            using (var objConn = new SqlCeConnection(ConnectionString))
            {
                objConn.Open();
                var command = new SqlCeCommand("Select count(1) from Track", objConn);
                ret = (int)command.ExecuteScalar();
                objConn.Close();
            }

            return ret;
        }

        public NavigatableCollection<Track> FetchLibrary()
        {
            var ret = new  NavigatableCollection<Track>();
            using (var objConn = new SqlCeConnection(ConnectionString))
            {
                var command = new SqlCeCommand("Select Id, TrackName, TrackPathName, Artist, TrackNumber, Album From Track Order By Artist, Album, TrackName", objConn);
                objConn.Open();
                using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    ret = ReaderToTrackCollection(reader);
                }
            }

            return ret;
        }

        public NavigatableCollection<Track> Search(SearchOptions options, string criteria)
        {
            // If we have no criteria, return the entire library
            if (string.IsNullOrEmpty(criteria) || criteria.Length == 0)
                return FetchLibrary();

            if (criteria.Length < options.MinimumCriteriaLength)
                 return null;

            // If the length of the criteria matches or exceeds the MinimumCriteriaLength , do a search 
            using (var objConn = new SqlCeConnection(ConnectionString))
            {
                var command = new SqlCeCommand("Select Id, TrackName, TrackPathName, Artist, TrackNumber, Album From Track WHERE TrackName Like @criteria OR  Artist Like @criteria OR  Album Like @criteria Order By Artist, Album, TrackName", objConn);
                if (options.SearchMethod == SearchMethod.Contains)
                {
                    command.Parameters.AddWithValue("@criteria",  '%' + criteria + '%');    
                }
                else
                {
                    command.Parameters.AddWithValue("@criteria", criteria + '%');      
                }

                objConn.Open();
                using (var reader = command.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    return ReaderToTrackCollection(reader);
                }
            }
            
        }


        private NavigatableCollection<Track> ReaderToTrackCollection(SqlCeDataReader reader)
        {
            var coll = new NavigatableCollection<Track>();
            while (reader.Read())
            {
                var track = new Track
                {
                    Id = (Guid)reader[0],
                    TrackName = reader[1] == DBNull.Value ? "" : (string)reader[1],
                    TrackPathName = reader[2] == DBNull.Value ? "" : (string)reader[2],
                    Artist = reader[3] == DBNull.Value ? "" : (string)reader[3],
                    Album = reader[5] == DBNull.Value ? "" : (string)reader[5],
                };

                coll.Add(track);
            }
            return coll;
        }

        //This is kinda dirty.  Be interesting to time delete / recreate all versus, seek - update or insert.
        //TODO:  Wrapp a stopwatch around this
        public void SaveLibrary(NavigatableCollection<Track> library)
        {
            //Delete the library first
            DeleteLibrary();

            //TODO:  Repopulate it
            DataTable dt = new DataTable();

            using (var objConn = new SqlCeConnection(ConnectionString))
            {
                objConn.Open();
                var adapter = new SqlCeDataAdapter("Select Id, TrackName, TrackPathName, Artist, TrackNumber, Album From Track", objConn);
                var objCommandBuilder = new SqlCeCommandBuilder(adapter);
                adapter.Fill(dt);

                foreach (var track in library)
                {
                    dt.Rows.Add(new object[] { track.Id, track.TrackName, track.TrackPathName, track.Artist, null, track.Album }); //We're not currently parsing the track number
                }

                adapter.Update(dt);

                objConn.Close();
            }
        }

        public void DeleteTrack(Track track)
        {
            using (var objConn = new SqlCeConnection(ConnectionString))
            {
                objConn.Open();
                var command = new SqlCeCommand("Delete from Track WHERE Id=@Id", objConn);
                command.Parameters.AddWithValue("@Id", track.Id);    
                command.ExecuteNonQuery();
                objConn.Close();
            }
        }

        private void DeleteLibrary()
        {
            using (var objConn = new SqlCeConnection(ConnectionString))
            {
                objConn.Open();
                var command = new SqlCeCommand("Delete from Track", objConn);
                command.ExecuteNonQuery();
                objConn.Close();
            }
        }
    }
}
