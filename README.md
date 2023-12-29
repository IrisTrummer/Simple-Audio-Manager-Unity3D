# Simple-Audio-Manager-Unity3D
A simple-to-use audio manager for Unity

TODO openUPM shield

## Usage
TODO usage examples (Methods, Zenject, ...)

## Installation

The package can be installed using [OpenUPM](https://openupm.com/packages/com.iristrummer.simple-audio-manager)
1. Install the [OpenUPM CLI](https://github.com/openupm/openupm-cli#installation)
2. Run the following command in the command line in your project directory:
```bash
openupm add com.iristrummer.simple-audio-manager
```

TODO installation instructions demo scene

## Getting Started

### Setting up the Audio Mixer
1. Create a new audio mixer asset (right click in the Unity Project window -> Create -> Audio Mixer).
2. Open the Audio Mixer window (Window -> Audio -> Audio Mixer).
3. In the Audio Mixer window, click the "+"-icon next to the Groups tab to add a new group, name the group "Background". Add two more groups in the same way with the names "Effects" and "UI".
4. Click on the Master group. In the hierarchy window, right click the volume field and select "Expose 'Volume (of Master)' to script". Repeat this procedure for the three groups you just created.
5. In the audio mixer on the top right click on the "Exposed Parameters" button. Rename each parameter to its group Name + "Volume", for example "MasterVolume"

### Setting up the Audio Manager
6. Create a new empty game object and add the AudioManager script to it
7. In the Audio Mixer field of the script, add the Audio Mixer asset you just created

## Acknowledgements

Ressources for the Demo are provided by:
- "Unity-UI-Rounded-Corners” by Kir Evdokimov (https://github.com/kirevdokimov). Available for use under the MIT license, at https://github.com/kirevdokimov/Unity-UI-Rounded-Corners
- "Sonus Tres" © Sandro Figo (https://soundcloud.com/sandrofigo/sonus-tres)
- Icons and Sound Effects by Kenney (https://kenney.nl/). Available for use under the  	Creative Commons CC0 License (https://creativecommons.org/publicdomain/zero/1.0/)
