<div align="center">

![banner](pics/banner.png)

*A 2D Hack'N Slash combat platformer powered by Unity Game Engine, inspired by BERSERK.*

  <a href="">![GitHub last commit](https://img.shields.io/github/last-commit/anorderh/unity_it_takes_guts)</a>
  <a href="">![GitHub contributors](https://img.shields.io/github/contributors/anorderh/unity_it_takes_guts)</a>
  <a href="">![GitHub code size in bytes](https://img.shields.io/github/languages/code-size/anorderh/unity_it_takes_guts)</a>
  <a href="">![GitHub](https://img.shields.io/github/license/anorderh/unity_it_takes_guts)</a>

<p float="left">
[![itch.io](pics/itchLogo.svg)](https://anorde.itch.io/it-takes-guts)
[![Youtube](pics/ytLogo.png)](https://www.youtube.com/watch?v=5odzbZZq-bM)
</p>

</div>

</div>

## Overview

![overview](pics/itg2.jpg)

    **It Takes Guts** is a Hack 'N Slash platformer that pits the player against an onslaught of  enemy waves, known as "Demon Imps". Massively outnumbered, the player is encouraged to practice crowd control and constant movement. The player's goal is to eliminate a number of enemies based on the set difficulty. If the player's health is completely depleted before this, they lose. 

## Controls

![controls](pics/itg4.jpg)

* WASD - *move around* 

* W - *jump*

* W (near a wall) - *wall climb*

* S - *crouch*

* SHIFT - *roll (passes enemy hitboxes)* 

* LEFT CLICK or SPACEBAR - *attack*

## Educational

![educational](pics/itg1.jpg)

    This game was made using **Unity Game Engine** and `C#`/`.NET` scripts implementing Unity's framework (MonoBehaviour). Some core features include:

1. Dynamic enemy behavior with player pathfinding, parryable attacks, and spawning throughout the map

2. Unique sprites & animations for player movement, damage, and attacks

3. Win/lose conditions and difficulty scaling

4. UI management and audio integration

For more information regarding the game's design and features, check out the [General Design Document]([S_22_GDD_2D_Norderhaug_Anthony.docx - Google Docs](https://docs.google.com/document/d/1Wxw3b1kfkffYeZ0WUoScJzD7MEI9GRNE/edit?usp=sharing&ouid=100855899086524971871&rtpof=true&sd=true)).

## Requirements

* An internet connection to access https://anorde.itch.io/it-takes-guts.

* *Recommended* - An NVIDIA graphics card

## Limitations

    To enable enemy pathfinding, the **A-Star Pathfinding Project** was used. However, my implementation is unoptimized and may run near 20-25 fps on slower processors. Moving forward, I would look to implement Unity NavMeshs instead.

## Inspiration

    This game was inspired by various 2D Combat Side-Scrollers such as *Katana Zero* and *Castle Crashers*, and horde mode games such as *Gears of War* and *Left 4 Dead*.

## License

[MIT LICENSE](LICENSE) 
