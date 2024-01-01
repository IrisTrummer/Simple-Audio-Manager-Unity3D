namespace Testing.MethodConfigurationMenus.MethodParameters
{
    public class PlayClipMethodParameters : MethodParameters
    {
        public string AudioClipName { get; private set; }
        public string GroupName { get; private set; }
        public float Volume { get; private set; }
        public float Pitch { get; private set; }
        public string PitchShiftTypeName { get; private set; }
        
        public bool IsUsingPitchShiftType { get; private set; }

        public PlayClipMethodParameters(string audioClipName, string groupName, float volume, float pitch, string pitchShiftTypeName, bool isUsingPitchShiftType)
        {
            AudioClipName = audioClipName;
            GroupName = groupName;
            Volume = volume;
            Pitch = pitch;
            PitchShiftTypeName = pitchShiftTypeName;

            IsUsingPitchShiftType = isUsingPitchShiftType;
        }
    }
}