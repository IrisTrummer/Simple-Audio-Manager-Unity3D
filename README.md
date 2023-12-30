# Simple-Audio-Manager-Unity3D
A simple-to-use audio manager for Unity

[![openupm](https://img.shields.io/npm/v/com.iristrummer.simple-audio-manager?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.iristrummer.simple-audio-manager/)

## Usage
```csharp
// Play an audio clip once
AudioManager.Instance.PlayOnce(audioClip, SoundType.Effects);

// Play an audio clip once at a specific position
AudioManager.Instance.PlayAudioClipAtPosition(audioClip, new Vector3(1, 2, 3), SoundType.Effects);

// Play a looping audio clip 
AudioManager.Instance.PlayOnLoop(audioClip, SoundType.Background);


// Set the volume for groups
AudioManager.Instance.SetVolume(0.5f, SoundType.Effects);

// Fade a group's volume over time
AudioManager.Instance.FadeGroupVolumeTo(0f, 3f, SoundType.Master);
```

You can use the `AudioPlayer` component to play audio clips directly in the scene or for setting up background music.

## Installation

The package can be installed using [OpenUPM](https://openupm.com/packages/com.iristrummer.simple-audio-manager)
1. Install the [OpenUPM CLI](https://github.com/openupm/openupm-cli#installation)
2. Run the following command in the command line in your project directory:
```bash
openupm add com.iristrummer.simple-audio-manager
```

## Getting Started

### Setting up the Audio Mixer
1. Create a new audio mixer asset (right click in the Unity project window -> Create -> Audio Mixer)
2. Open the Audio Mixer window (Window -> Audio -> Audio Mixer)
3. In the Audio Mixer window, click the "+"-icon next to the Groups tab to add a new group under the `Master` group, name the group `Background`. Add two more groups in the same way with the names `Effects` and `UI`
4. Select the `Master` group. In the hierarchy window, right click the volume field and select "Expose 'Volume (of Master)' to script". Repeat this procedure for the three groups you created in the previous step
5. In the audio mixer on the top right click on the "Exposed Parameters" button. Rename each parameter to the group name and append "Volume", for example "MasterVolume"

### Setting up the Audio Manager
6. Create a new empty game object and add the AudioManager script to it
7. In the Audio Mixer field of the script, add the Audio Mixer asset you just created

### Dependency Injection Frameworks

If you plan on using your own dependency injection framework follow the instructions below.

Without any modification you access the instance of the `AudioManager` via the singleton `AudioManager.Instance`. If you, however, want to use a dependency injection framework e.g. Zenject to access the instance you need to do the following:

1. Open the menu Tools -> Simple Audio Manager -> Settings
2. Disable "Enable Singleton" (this will remove the default way of accessing the instance via `AudioManager.Instance`)
3. (Optional) If you also want to use the `AudioPlayer` component you need to implement it using your desired dependency injection framework (see below for an example on how to do this with Zenject)

```csharp
// Example AudioPlayer implementation using Zenject to get the instance (binding is set up in e.g. project context)
public class AudioPlayer : AudioPlayerBase
{
    [Inject] private readonly AudioManager audioManager;

    protected override AudioManager AudioManager => audioManager;
}
```

## Acknowledgements

Ressources for the Demo are provided by:
- "Unity-UI-Rounded-Corners” by Kir Evdokimov (https://github.com/kirevdokimov). Available for use under the MIT license, at https://github.com/kirevdokimov/Unity-UI-Rounded-Corners
- "Sonus Tres" © Sandro Figo (https://soundcloud.com/sandrofigo/sonus-tres)
- Icons and Sound Effects by Kenney (https://kenney.nl/). Available for use under the  	Creative Commons CC0 License (https://creativecommons.org/publicdomain/zero/1.0/)
