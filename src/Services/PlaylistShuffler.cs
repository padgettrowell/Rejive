using System;
using System.Collections.Generic;
using System.Linq;

namespace Rejive
{
    public static class PlaylistShuffler
    {
        private static Random _rng = new Random();

        public static NavigatableCollection<Track> Shuffle(NavigatableCollection<Track> playlist)
        {
            var map = new Dictionary<int, Guid>(playlist.Count);

            //Create a map between the track id and a new guid
            for (int i = 0; i < playlist.Count; i++)
            {
                map.Add(i, Guid.NewGuid());
            }

            //Create a new Playlist
            var shuffledList = new NavigatableCollection<Track>();

            //Sort the map by the new Guid
            foreach (var kvp in map.OrderBy(x => x.Value))
            {
                KeyValuePair<int, Guid> pair = kvp;
                shuffledList.Add(playlist[pair.Key]);
            }

            //Set the current item
            shuffledList.MoveFirst();

            return shuffledList;
        }

        public static Track PickRandomTrack(NavigatableCollection<Track> playlist)
        {
            if (playlist.Count <= 0)
                return null;

            var index = GetRandomInt(0, playlist.Count);  //This is count, not count -1 as apparently the rng won't ever return the upper bounds.

            return playlist[index];
        }

        private static int GetRandomInt(int min, int max)
        {
            return _rng.Next(min, max);
        }

    }

}
