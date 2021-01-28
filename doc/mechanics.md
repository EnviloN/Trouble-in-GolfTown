[Go Up](../README.md)

# Mechanics
[TODO]
This section is about how to play minigolf and how our inventory system works.

## Minigolf


## Inventory

We tried to minimize differences between VR and PC gameplay, so we implemented the inventories basically the same with minor changes.

### Balls

For PC player, the number of balls in his inventory is displayed in top right corner of the screen. As we soon found out, this cannot be used for VR player. So we implemented basic sign that is displayed above the ball when placing it on the ground.

[TODO Honza: picture of UI when placing ball (PC player)]

[TODO Dominik: picture of placing down ball in VR]

### Clubs

For the clubs, we didn't have time to properly think about how to display which club is in player's inventory. So we didn't do it. Player will have to try iterate through to find out, which clubs he has in inventory.

[TODO Honza: picture of club in hand (PC player)]

[TODO Dominik: picture of club levitating in front of player (VR)]

For PC player, we implemented that the club is levitating beside the player, when he has it in hand. This indicates, that he can play minigolf. It is simpler for VR player, he just spawns the club in front of him and grabs it.

[TODO Dominik: picture of club in hand (VR)]
