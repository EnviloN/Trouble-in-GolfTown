[Go Up](../README.md)

# Mechanics

This section is about how to play minigolf and how our inventory system works.

## Minigolf

### VR player

We used internal mechanics of collision detection between club and ball in VR to play minigolf. We just had to tweak surface materials.

<img src="./img/mechanics/VR_playing_minigolf.jpg" height="600">

### PC player

For PC we had to implement very different logic, because the player does not have anything to swing with on the keyboard. So we decided to use player position and duration of press of mouse button to trigger force on the ball.

From the start, we had intended to have PC playing only for debugging (because only two members of our team had compatible VR headsets). But in the end we tried to make it possible to finish the game even on PC.

<img src="./img/mechanics/PC_playing_minigolf.png" height="600">

## Inventory

We tried to minimize differences between VR and PC gameplay, so we implemented the inventories basically the same with minor changes.

### Balls

For PC player, the number of balls in his inventory is displayed in top right corner of the screen. As we soon found out, this cannot be used for VR player. So we implemented basic sign that is displayed above the ball when placing it on the ground.

<img src="./img/mechanics/PC_placing_ball.png" height="600">

<img src="./img/mechanics/VR_placing_ball.jpg" height="600">

### Clubs

For the clubs, we didn't have time to properly think about how to display which club is in player's inventory. So we didn't do it. Player will have to try iterate through to find out, which clubs he has in inventory.

<img src="./img/mechanics/VR_levitating_club.jpg" height="600">

For PC player, we implemented that the club is levitating beside the player, when he has it in hand. This indicates, that he can play minigolf. It is simpler for VR player, he just spawns the club in front of him and grabs it.

<img src="./img/mechanics/PC_club_in_hand.jpg" height="600">
