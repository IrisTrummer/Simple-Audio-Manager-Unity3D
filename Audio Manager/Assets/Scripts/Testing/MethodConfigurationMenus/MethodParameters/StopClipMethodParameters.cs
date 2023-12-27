namespace Testing.MethodConfigurationMenus.MethodParameters
{
    public class StopClipMethodParameters : MethodParameters
    {
        public string AudioClipName { get; private set; }

        public StopClipMethodParameters(string audioClipName)
        {
            AudioClipName = audioClipName;
        }
    }
}
