using UnityEngine;

namespace Testing.MethodConfigurationMenus.MethodParameters
{
    public class FadeGroupVolumeMethodParameters : MethodParameters
    {
        public string GroupName { get; private set; }
        public Vector2 FromTo { get; private set; }
        public float Duration { get; private set; }

        public FadeGroupVolumeMethodParameters(string groupName, Vector2 fromTo, float duration)
        {
            GroupName = groupName;
            FromTo = fromTo;
            Duration = duration;
        }
    }
}
