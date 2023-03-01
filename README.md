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
P300
- Boat Rescue - Uses standard P300 controls. Does *not* automatically start. Default "S" to start stimulation, then keyboard input 0-4 for object selection.
- Mutant Clash - Uses standard P300 controls. *Automatically starts*. Use 0-3 (characters), and 0-2 (lanes) to select.
- Brain Buddy - Uses a special gtec simulated P300. Use WASD to move, space bar to jump.
- Where's Willy? - Uses P300 controls. *Automatically starts*. Use 0-1 (menu) selections, and 0-9 to select spaces where Willy might be.
- Gardens of the Galaxy - Uses standard P300 controls. Does *not* automatically start. Default "S" to start stimulation, then select planets with 0-3.

SSVEP
- Star Defense - Uses standard SSVEP controls. *Automatically starts and automatically stops*. Use 0 or 1 for selection.
- WacDonalds - Uses standard SSVEP controls. Automatically starts *the first time* but *does not auto stop or begin again*. Default "S" to stop stimulation, keyboard input 0-8 for selection. Will need to start/stop manually after the first round. 
- BCI Burgers - Can use SSVEP or MI Controls. Does *not* automatically start. Default "S" to start stimulus, keyboard input 0 or 1 for selection, "S" to finish selection.
- The Prophecy - Uses standard SSVEP controls. Automatically starts *the first time* but *does not auto stop or begin again*.  Default "S" to stop stimulation, keyboard input 0-n for selection, where n is the number of arrows-1 (e.g. 3 arrows uses keyboard inputs 0,1,2). Will need to start/stop manually after the first round. 
- Star Skirmish - Uses standard SSVEP controls. Does 

Other
- BCI4Kids + Hardeep - Use mouse to select items when they are at the top of the screen, and "space bar" to pick up the item
- Purr-for-the-course - Be sure to add the number of players to the game in the top left. Then use "Spacebar" to play.

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
