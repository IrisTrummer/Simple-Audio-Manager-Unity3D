using UnityEngine;

namespace Testing.MethodConfigurationMenus.MethodParameters
{
    public class PlayClipAtPositionMethodParameters : PlayClipMethodParameters
    {
        public Vector3 Position { get; private set; }
        public bool Loop { get; private set; }

        public PlayClipAtPositionMethodParameters(string audioClipName, string groupName, float volume, float pitch, string pitchShiftTypeName, bool isUsingPitchShiftType, Vector3 position, bool loop)
            : base(audioClipName, groupName, volume, pitch, pitchShiftTypeName, isUsingPitchShiftType)
        {
            Position = position;
            Loop = loop;
        }
    }
}