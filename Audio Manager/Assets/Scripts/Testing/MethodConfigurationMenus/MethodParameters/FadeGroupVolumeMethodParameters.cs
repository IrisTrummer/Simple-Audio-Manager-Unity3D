using UnityEngine;

namespace Testing.MethodConfigurationMenus.MethodParameters
{
    public class FadeGroupVolumeMethodParameters : MethodParameters
    {
        public string GroupName { get; private set; }
        public float Volume { get; private set; }
        public Vector2 FromTo { get; private set; }
        public float Duration { get; private set; }
        public bool IsUsingFromToVolume { get; private set; }

        public FadeGroupVolumeMethodParameters(string groupName, float volume, Vector2 fromTo, float duration, bool isUsingFromToVolume)
        {
            GroupName = groupName;
            Volume = volume;
            FromTo = fromTo;
            Duration = duration;
            IsUsingFromToVolume = isUsingFromToVolume;
        }
    }
}
