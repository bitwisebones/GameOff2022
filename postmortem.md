# Github Game Off 2022 Postmortem

(TL;DR - Summary at the bottom)

This is the first game jam I've participated in since the [Candy Jam](https://itch.io/jam/candyjam) (under a different alias) back in 2014. Hard to believe it's been that long! I've started and abandoned many game projects in the 8 years since then, so I decided to use this jam as an opportunity to just _finish something_...._ANYTHING_!

## Game Design Choices

This game ended up being a mashup inspired by a few things that I've encountered recently. 

- The grid-based dungeon-crawl-esque movement was likely inspired by [Untold](https://johngabrieluk.itch.io/untold). 
- The sort of point-and-click puzzles and the rural English setting were likely inspired by [The Excavation of Hob's Barrow](https://store.steampowered.com/app/1182310/The_Excavation_of_Hobs_Barrow/)
- The whole "weremouse" thing was likely inspired by [Chetney](https://criticalrole.fandom.com/wiki/Chetney_Pock_O%27Pea) from [Critical Role](https://critrole.com/).

Ultimately I think the movement style and interaction style work well together. HOWEVER, choosing to create a game that's mostly content-based (as opposed to system-based) was probably a bad idea (at least for me). I had the majority of the game's systems created in a few days, and then I had to spend the rest of the month grinding out models/textures/dialogue/etc which is a) not something I'm good at, b) not something I enjoy, and c) very time consuming. In the end, I feel like I've spent an immense amount of time on something that I'm really not that proud of (the perfect recipe for burnout!). Next time I'll try to lean into a game style/genre that requires much less asset creation.

## Implementation

From a high level, the game's architecture isn't very interesting. There's a bunch of singletons that manage stages, scenes, resources, game state, etc, and then a few big chunks of "data" code - dialogue, scene objects, etc.

I did like how the scene system turned out. The SceneManager maintains a stack of scenes - all scenes in the stack are rendered, but only the top scene is updated. This made it really easy to just push a DialogueScene onto the stack and still have the "world" rendered behind the dialogue UI. 

Maintaining the dialogue and scene data in C# (as opposed to JSON or some other format) was a blessing and a curse. On one hand, it's really nice to be able to write "hooks" into dialogue (adding an item to your inventory, for example) without having to implement a scripting system or some more complicated parsing. On the other hand, C# can be quite verbose. If you take a peek at any of the *Dialogue.cs files in src/Data/Dialogue, you'll notice that I used some shorthand static methods to create DialogueNodes/DialogueLinks in an attempt to keep the boilerplate to a minimum.

Using Raylib was pretty straightforward, but I did run into a few issues that I couldn't figure out. Initially I wanted to use a single raycast (from the camera, pointing forward) for collision detection, but the ray->mesh collision detection was giving me all sorts of issues. Instead, I ended up flagging each tile on the grid as traversable or not (both as a man and as a mouse). Each scene/area has a separate image in src/Resources/Images that defines the tile states (magenta = traversable as mouse or man, red = traversable only as mouse, white = can transform here), and then NavigationGrid.cs reads in the image data and constructs some dictionaries that I later use to determine if a tile is traversable or not.

I also ran into some issues with the Raylib-cs example for the skybox, so I ended up settling on a big ol' cube mesh and a texture created using some ancient program called Terragen I think. It all worked out alright in the end.

## Summary

- I'm glad I finished the game. It's a complete thing that I can now forget about :)
- For future jams, I'll need to be much more cognizant of the genre I choose and how it impacts the type of work I'll be doing (code vs assets)
- C#/Raylib was a decent choice for a small game like this

Thanks for reading!