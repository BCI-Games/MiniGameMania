# BCI Game Jam 2022 - MiniGame Mania Collection.
This is a collection of the compatible minigames from the BCI Game Jam 2022. A so-called minigame megagame! The goal is to have all of the jam submissions in a single place.

## VOTING NOW OPEN: Vote for your favorite game here!
Voting is now open for developers and general public. This will be done on version Minigame-Mania-v1.0.0, as a "frozen" representation of all the games at the submission time (48 hours of development).
You can vote here: https://form.jotform.com/230565534460050

## Controls
Every game in the collection is playable with keyboard and mouse (in addition to BCI). 
### Visual Stimulus Games P300/SSVEP
To play the P300 or SSVEP games with keyboard and mouse, you will need to input 0-9 on your keyboard while the visual stimulus is happening. Some games initiate visual stimulus automatically, giving you the opportunity to select a game object using 0-9 on the keypad, while others will require you to hit "s" (or your chosen keybind) to start or stop the visual stimulus (StartStopStimulus option in the "Options" menu). 

**P300 games have a set period of time for stimulus flashing, while SSVEP will require you to manually stop the stimulus before selection feedback is given**.

In summary: Selection will only occur if (1) Visual stimulus (e.g. flashing) is happening, (2) you press an input, (3) the visual stimulus stops. If you think it isn't work, try hitting "s" again to

### Games with no visual stimuls - MI/Other
For these games there is no visual feedback for "when" the BCI would be on. Traditionally these BCI systems are always monitoring for feedback. You can again hit "s" (or your keybind) to start the feedback collection followed by your input selection, but remember there may be no visual feedback provided. Most of these games can also be played with keyboard inputs.

### Game-specific Controls:
- BCI4Kids + Hardeep - Use mouse to select items when they are at the top of the screen, and "space bar" to pick up the item
- Boat Rescue - Uses P300 controls. (Default "S" to start stimulation, then keyboard input 0-4 for object selection).
- BCI Burgers - Uses SSVEP/MI Controls. (Default "S" to start stimulus, keyboard input 0 or 1 for selection, "S" to send again).

### This work uses BCIEssentials
A Unity base environment for streamlined development of BCI applications.

Pairs nicely with [bci-essentials-python](https://github.com/kirtonBCIlab/bci-essentials-python)

### Note - running on Apple silicon:
To run on Apple silicon (ex: M1), the Lab Streaming Layer plugin library needs to be replaced:

1. Download the latest [liblsl release](https://github.com/sccn/liblsl/releases) for MacOS (arm64)

2. Unzip the archive and copy the lib/*.dylib files into ./Assets/Plugins/lib

### Other Submissions
The other game submissions that were unable to be included in the Minigame Mania Collection can be downloaded here:
https://drive.google.com/drive/folders/1IQIvwG_ZHBXXWVIEBunIhYxC_QxSXByb?usp=sharing
