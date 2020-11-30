# Dynamic World

This page is more of an overview of the semestral project requirements and how the game fulfills them. It provides links to the primary documentation, which provides more detailed information and backstory.

## Animations

All of the characters in the game are animated. Some of them move around the map, and some only have idle animations. Our [main characters](../main_characters.md) are so far stationary, having several idle animations and change them regurarely. However, the [secondary NPCs](../other_npcs.md) also move around the map, have their cyclic activities, and follow certain behavior.

We have both created our original animations and used already existing ones.

![](../img/animations/bar.png)

### Most relevant documentation pages:
- [Animations](../animations.md)
- [Characters](../characters.md)
    - [Main characters](../main_characters.md)
    - [Other NPCs](../other_npcs.md)

## Productions

We have created two major productions. One only for aesthetic purposes and the second one has an impact on the gameplay.

![](../img/productions/tumbleweed.png)

### Most relevant documentation pages:
- [Productions](../productions.md)

## Behavior

There are several behaviors some NPCs follow. Some of the behaviors are triggered later in the game when the player performs some actions; some are triggered by a random chance when two certain NPCs meet, or a group of NPCs is present in a specific area.

We have also created a horse behavior that the horses on the Magnate's farm follow. The horses walk around and eat, but when the player approaches, they run away from them.

![](../img/animations/horses/horses_1.png)


### Most relevant documentation pages:
- [Animations](../animations.md)
    - [Oil Tower explosion](../oil_tower.md)
    - [Horse behavior](../horses.md)
- [Characters](../characters.md)
    - [Main characters](../main_characters.md)
    - [Other NPCs](../other_npcs.md)

## Unique events

Currently, there are two unique events either conditional to player's actions or to random chance.

![](../img/animations/UniqueTriggerAnimation.gif)

Sherif is usually walking through the streets of Gold Town, spending some time at the Barber Shop, or sitting on his chair. On the other hand, Old Dave is always changing spots and sleeps on random chairs throughout the town. He knows he is not allowed to sleep on Sherif's chair, but he can forget and go sleep there with a small probability. If during that time, Sherif happens to go to his chair as well, they start to argue.

![](../img/productions/giant_ball.png)

There are always up to three black golf balls hidden in the Golf Plains. There are very hard to find, but if a player manages to find all three of them, a giant golf ball will spawn in the sky and cast a shadow on the town.

### Most relevant documentation pages:
- [Productions](../productions.md)
- [Other NPCs](../other_npcs.md)
