# Unity3D - Floating Messages

#### DESCRIPTION:

Enables printing lifetimed GUI messages on a running game instance. 

#### USAGE:

Download asset from releases page and import into your project.

Add import statement as follows:

```
using WF.FloatingMessages;
```

Then you can make calls like the following:

![](/Readme/Code_Example.png)

which will print messages over running game instance (utilizing built-in GUI class). 

Messages will start at the top-left side of the screen; and new messages will be appended as new lines.

When lifetime of a message is over, it will disappear and all remaining messages will move upwards to fill the blank space.


Example output:

![](/Readme/InGame_Example.png)

In the example, only top-left portion of the screen is shown. 

In the example, game is running with a main camera that has solid black background color.