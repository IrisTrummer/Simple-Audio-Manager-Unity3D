#if !NO_SINGLETON_AUDIO_MANAGER

namespace SimpleAudioManager
{
    /// <inheritdoc cref="AudioPlayerBase"/>
    public class AudioPlayer : AudioPlayerBase
    {
        protected override AudioManager AudioManager => AudioManager.Instance;
    }
}

#endif