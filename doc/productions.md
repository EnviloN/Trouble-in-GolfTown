[Go Up](mechanics.md)

# Productions

So far, there are two productions in the game—a tumbleweed generation and a golf ball spawning.

## Golf Ball Spawning

We have implemented a random golf ball spawning mechanic. This mechanic spawns golf balls randomly in the predefined locations. This mechanic is essential for the gameplay because it ensures that the player can run out of golf balls and always look for more in the world.

![](./img/productions/ball_location1.png)

The spawn locations can be added to the game simply by placing the spawn location prefabs to the scene. Each of these locations can be configured separately to set up the probability with which a ball is spawned on that location. During the game, the Game Manager can refresh all locations and respawn a new batch of balls. This can be done, for example, during a scene change. 


When a ball is spawned, a prefab that has the necessary scripts to support the player's interactions is instantiated.

![](./img/productions/ball_location2.png)


### Giant Ball
In the main scene (outside world), also unique black golf balls spawn. The black balls can be picked up as regular balls and added to the player's inventory as standard golf balls. However, If a player manages to find three black balls, a giant golf ball will spawn in the sky. The balls are generated at random locations around the map and are hard to find. It is always guaranteed that the same number of black balls spawned as the player is missing to the three black balls collceted.

![](./img/productions/giant_ball.png)


## Tumbleweed

We have placed several tumbleweed effects on the main scene to enhance the world's aesthetic and dynamicity. These effects generate tumbleweeds that are then moving through the scene and bumping into other objects.

![](./img/productions/tumbleweed.png)
