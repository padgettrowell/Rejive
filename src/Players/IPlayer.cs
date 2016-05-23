using System;
using System.ComponentModel;

namespace Rejive
{
    public delegate void PlaybackPositionChangedHandler(TimeSpan currentPosition);
    public delegate void PlaybackCompleteHandler();

    public enum PlaybackState : int
    {
        None,
        Playing,
        Paused
    }

    public interface IPlayer : IDisposable
    {
        event PlaybackPositionChangedHandler PlaybackPositionChanged;
        event PlaybackCompleteHandler PlaybackComplete;
        event PropertyChangedEventHandler PropertyChanged;

        //Give the Player an opportunity to do any initialisation.  Provide the host container
        void Init(PlayerForm container);
        float Volume { get; set; }
        void Load(string file);
        void Play();
        void Pause();
        void Stop();
        void SkipTo(TimeSpan newPosition);
        PlaybackState State { get; }
        TimeSpan TrackDuration { get; set; }
        int BytesPerSecond { get; set; }
        int SampleRate { get; set; }
        int Channels { get; set; }
    }
}
