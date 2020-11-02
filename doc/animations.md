[Go Up](visuals.md)

# Animations

We decided to use online character animation service [Mixamo](https://www.mixamo.com/#/) to help us with animating of all of our characters.

The basic model (Mixamo_POLYGON_Guy_Naked.fbx) used as skeleton for animation is saved in folder [Mixamo_SyntyStudios](./../TGT/Assets/StoreAssets/Mixamo_SyntyStudios).

List of currently functional [Animations](./../TGT/Assets/Animations):
* **Idle** 
    * default standing position
* **Idle Sitting**
    * default sitting position for all NPCs, hands on knees
    * *needs:* chair or similar
* **Idle Sitting Male**
    * sitting position for all NPCs, sunk into chair, hands on legs
    * *needs:* chair or similar
* **Idle Sitting Female**
    * slightly effeminate sitting position, crossed leg, hands on one knee
    * *needs:* chair or similar
* **Crouching**
    * position suitable for a hunter (or a drunk).
* **Walking in Circle and drinking**
    * uninterrupted slow walking in predefined circle, NPC carries a bottle and takes a sip mid-circle.
    * *needs:* bottle or similar
* **Sitting Talking**
    * sitting position with talking animation. Face does not move, but both hands are gesturing.
    * *needs:* chair or similar
* **Sitting Laughing**
    * sitting position with loud laughing animation. NPCs moves back and forth, throws head back and waves a hand.
    * *needs:* chair or similar
* **Crying**
    * standing position with crying animation. Hands are held to the chest and then wave out as if to dry tears.
* **Thinking**
    * standing position with speculative hand and head animation.

* To be continued.. 

Adding animation to a rig: [simple YT tutorial](https://www.youtube.com/watch?v=9H0aJhKSlEQ) we use in our project.
Almost every NPC already has an Animation Controller in folder [Controllers](./../TGT/Assets/Animations/Controllers) with one default animation. Cycles and transitions between animations are in the making.


#### List of [Characters](./characters.md) and Their Animation Cycles:
[TODO] (this chapter might be added to character descriptions instead)