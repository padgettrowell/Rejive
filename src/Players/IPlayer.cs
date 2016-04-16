using System;
using System.ComponentModel;

namespace Rejive
{
    public delegate void PlaybackPositionChangedHandler(TimeSpan currentPosition);
    public delegate void PlaybackCompleteHandler();

    public interface IPlayer : IDisposable
    {
        event PlaybackPositionChangedHandler PlaybackPositionChanged;
        event PlaybackCompleteHandler PlaybackComplete;
        event PropertyChangedEventHandler PropertyChanged;

        //Give the Player an opportunity to do any initialisation.  Provide the host container
        void Init(PlayerForm container);
        double Volume { get; set; }
        void Load(string file);
        void Play();
        void Pause();
        void Stop();
        void SkipTo(TimeSpan newPosition);
        bool IsPaused { get; }
        TimeSpan TrackDuration { get; set; }
    }
}
