namespace Testing.MethodConfigurationMenus.MethodParameters
{
    public class PlayClipMethodParameters : MethodParameters
    {
        public string AudioClipName { get; private set; }
        public string GroupName { get; private set; }
        public float Volume { get; private set; }
        public float Pitch { get; private set; }
        // TODO missing random pitch range

        public PlayClipMethodParameters(string audioClipName, string groupName, float volume, float pitch)
        {
            AudioClipName = audioClipName;
            GroupName = groupName;
            Volume = volume;
            Pitch = pitch;
        }
    }
}
