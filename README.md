# Space fight 2D

This is a small game implemented to test the MLAPI. The project use :

- MLAPI (network)
- New Unity input system 

## Features

- Main menu
- Multiplayer
- Fire bullets
- Move ship around
- Collisions with asteroids, bullets
- Simple dead/respawn (only one spawnpoint)

## How to setup

* Clone the repository
* Open as Unity project (tested on Unity 2019.3.3f1)
* Hit play mode
* Choose create game
* Shoot using spaceBar
* Move using arrow keys
* Have fun

## How to setup multiplayer

* Clone the project (you can use mklink (https://www.youtube.com/watch?v=-nS1gqSk458)  or uEcho - https://www.youtube.com/watch?v=9r-hwXPJIMo)
* One host the game using create game button, the other join game as client
* You MUST have the same network config in HostGameMLAPI object, NetworkingManager script, under connect adress (local is 127.0.0.1)

## How to Build

* Use unity build window, you can build for the plateform you want
* If you want to run the server on cloud, use the server build checkbox

## How to setup server on cloud

* After choosing a cloud provider, you should have a ssl connection to a dedicated server or virtualised private server, connect to it.
* You need to perform a server build on the correct plateform (usually linux for those server)
* Copy the files of the build to your server (you can use this handy tool with ssl connection : core ftp le http://www.coreftp.com/)
* Launch the executable (??.x86_64) with commands -batchmode -nographics. First one is to make a headless server, and no graphics to not launch game graphics, saving a lot of memory.
* Server specific code can be found under define #UNITY_SERVER
* Optimization that has to be done is to set 
```Application.TargetFrameRate =30```
 to 30 or 60, otherwise Unity will use all the cpu available, and you don't want that.
* Optimization that can be done is to deactivate all useless server rendering, here is an exemple for renderers :
``` 
if (Application.isBatchMode) //check if batchmode is used, can also use #UNITY_SERVER define to run the code on server.
        {
            //If not set, the targetFrameRate is infinite, burning your cpu
            Application.targetFrameRate = 30;

            Renderer[] rs = (Renderer[])FindObjectsOfType( typeof( Renderer ) );
            foreach( Renderer r in rs )
            {
                r.enabled = false;
            }
            SkinnedMeshRenderer[] smr = (SkinnedMeshRenderer[])FindObjectsOfType( typeof( SkinnedMeshRenderer ) );
            foreach( Renderer r in smr )
            {
                r.enabled = false;
            }
 }
```
