namespace Testing.MethodConfigurationMenus.MethodParameters
{
    public class SetVolumeMethodParameters : MethodParameters
    {
        public string GroupName { get; private set; }
        public float Volume { get; private set; }

        public SetVolumeMethodParameters(string groupName, float volume)
        {
            GroupName = groupName;
            Volume = volume;
        }
    }
}
