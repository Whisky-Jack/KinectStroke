# KinectStroke

Code files for kinect stroke game, updated to run with [monogame](https://docs.monogame.net/) rather than XNA. The old version of KinectStroke can be found [here](https://github.com/Whisky-Jack/KinectStroke3.0).

## Table of Contents
1. [Downloading/Installing the Files](#downloading)
2. [Running the Application](#running)
3. [Using KinectStroke](#using)
3. [Modifying the Code](#modifying)

## Downloading/Installing the Files <a name="downloading"></a>

The repository can be cloned by following the standard git instructions and the green code button on the top left. If you are unfamiliar with/confused by git, see [here](https://docs.github.com/en/repositories/creating-and-managing-repositories/cloning-a-repository).

## Running the Application <a name="running"></a>

To run the game on Windows 10 navigate to the folder containing the the KinectStroke application, and run it.

## Using KinectStroke <a name="using"></a>

A quick rundown of the options in the main menu and their significance:

1. Calibration - Calibration is used to calibrate the sensitivity of the gameplay to the range of motion of the player.
    1. Align the player's tracked hand with the upper right, bottom right, top left, and bottom left points, and press W, S, A and Q respectively to identify these points as the limits of their range of motion. When done press Escape to exit.
2. Setup - Used to set parameters such as gameplay difficulty.
    1. Directory - Sets the location to store the gameplay data.
    2. Difficulty - 
3. Gameplay - To play the game, catch the asteroids by guiding the rocket onto them, and then throw them into the sun. Each success earns points, and the difficulty increases with time. To end the game press Escape.
4. Data - Various game parameters are stored as csvs, which can be found under the directory specified in the setup.

## Modifying the Code <a name="modifying"></a>

The game was ported to [monogame](https://docs.monogame.net/) from XNA by following several guides such as that found [here](http://www.knoxgamedesign.org/wp-content/uploads/2020/09/XnaToMonogame.pdf). To work with the code, open the project solution in a version of [Visual Studio](https://visualstudio.microsoft.com/downloads/).
