# Space fight 2D

This is a small game implemented to test the networking solutions. The project use :

- MLAPI (network)
- Colyseus (network)
- New Unity input system 

## Features

- Main menu
- Multiplayer
- Fire bullets
- Move ship around
- Collisions with asteroids
- Simple dead/respawn


### WIP

- Scores (UI)


## Start the project

- Open the project with Unity (>= 2019.3.0f3)
- Open the Scenes/MainMenu scene
- Select the network layer you will use (MLAPI working, colyseus in progress)
  - MLAPI : Select the NetworkManager and change the IP to connect at the bottom (eg: set 127.0.0.1 for local)
- Build & Run should open the game, click Create a game
- Start the game in unity and click on Join

##Network test

You can use uEcho to create a clone (see the menu close to file/edit/asset/gameobject/component/uEcho/Windows/help in top of unity)   the uecho->"add new quick clone(s) " -> "ending with client" -> " only one". Then it will create a copy of your project. Launch another instance of unity using this new project (which will be called spacefight-2d-maste-Client) in the same root directory. Then choose the main menu scene.
The two projects are now in synced, so when you change something on a project, it will replicate automatically on the other (just hit reload when asked too for scene update). 
