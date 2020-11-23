[Go Up](visuals.md)

# Animations

We decided to use online character animation service [Mixamo](https://www.mixamo.com/#/) to help us with animating of all of our characters.

The basic model (Mixamo_POLYGON_Guy_Naked.fbx) used as skeleton for animation is saved in folder [Mixamo_SyntyStudios](./../TGT/Assets/StoreAssets/Mixamo_SyntyStudios).

List of currently functional [Animations](./../TGT/Assets/Animations):
* **Idle** 
    * default standing position
* **Idle Sitting**
    * default sitting position for NPCs, hands on knees
    * *needs:* chair or similar
* **Idle Sitting Male**
    * sitting position for NPCs, sunk into chair, hands on legs
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

* **Standard Walk**
  * default walking animation for NPCs

* **Wave**
  * waving animations

* **Drinking Behind Table**
  * drinking animation for characters sitting on a chair behind a table

* **Sitting on Bar Chair**
  * sitting position on a bar chair

* **Sitting on Bar Chair and Drinking**
  * drinking animation for characters sitting on a bar chair

* **Sitting on Chair**
  * variation of sitting on a chair

* **Sitting on Chair with Legs Crossed**
  * more feminine variation of sitting on chair

* **Sleeping on Bar Chair**
  * position for sleeping on bar chair behind a bar

* **Stare at Sea**
  * Hank animation for staring at sea

* **Look at Sea**
  * Foreign Supplier animation for staring into sea

* **Lean on Roll**
  * Sally animation for leaning on the rolls

* To be continued.. 

Adding animation to a rig: [simple YT tutorial](https://www.youtube.com/watch?v=9H0aJhKSlEQ) we use in our project.
Almost every NPC already has an Animation Controller in folder [Controllers](./../TGT/Assets/Animations/Controllers) with one default animation. Cycles and transitions between animations are in the making.

#### Animation cycles and navigation implementation
In order to move NPC from one position to the second one, we are using NavMeshes. NavMesh is a mesh consisting of multiple faces, which describes possible path NPC could take. Those can be imported to our project from Unity's GitLab. We are using the NavMesh agent to determine speed, rotation velocity, dimensions of NPC and how should NPC avoid obstacles. We determine the exact location of movement with random picks from GameObject, which has 1 layer children, defining target places. After this setup, we've set transitions in the Animator class, initialized them with parameters, which are referenced and controlled from our AnimWithNav.cs script file.

#### List of [Characters](./characters.md) ,Their Controllers and Animations:
* **Barman**
  * DrunkenJoe_deco
    *  Idle
    *  Clean Bar Table
    *  Cup Washing

* **Developer**
  * Developer_deco
    *  Idle

* **Ferryman**
  * Ferryman_deco
    *  Idle

* **Doctor**
  * Doctor_deco
    *  Idle

* **Saloon girl**
  * Dixxi_deco
    *  Idle
    *  Standing

   *Idle animations are unique for each main NPC.*

* **Lisa**
  * CowboyGirl_deco
    * Sitting Laughing

* **Joe**
  * CowBoy_deco
    * Sitting Laughing

* **Sheriff**
  * Sheriff_deco
    * Sitting Idle

* **Barber**
  * Barber_deco
    * Idle

* **Old Dave**
  * OldDave_deco
    * Male Sitting Pose

* **Miss Jane**
  * Jane_deco
    * Female Sitting Pose

* **Supply Owner**
  * SupplyOwner_deco
    * Idle

* **Sister Angelica**
  * Angelica_deco
    * Idle

* **Sister Peggy**
  * Peggy_deco
    * Idle

* **Oil Tower Operators**
  * TOperatorA
    * Idle
  * TOperatorB
    * Idle

* **Widow**
  * Widow_deco
    * Crying

* **Hunter Larry**
  * Hunter_deco
    * Humanoid Crouch

* **Beau**
  * Beau_deco
    * Sitting on Bar Chair
    * Sitting on Bar Chair and Drinking
    * Sleeping on Bar Chair

* **Austin**
  * Austin_deco
    * Sitting on Bar Chair
    * Sitting on Bar Chair and Drinking
    * Sleeping on Bar Chair

* **Visitors**
  * FVisitor_deco
    * Sitting on Chair With Legs Crossed
    * Drinking Behind Table
    * Sittin on Chair
  * MVisitor_deco
    * Idle

* **Frank**
  * Frank_deco
    * Walk in Circle

* **Sally**
  * Sally_deco
    * Idle *(custom idle animation)*
    * Lean on Rolls

* **Dock Hand Hank**
  * DockHand_deco
    * Idle *(custom idle animation)*
    * Stare at Sea

* **Foreign Supplier**
  * FSupplier_deco
    * Idle *(custom idle animation)*
    * Look at Sea
