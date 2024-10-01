# VonderGamesTest
 
29/09/2024 
13:00-14:30 | Design Time Hop System ( 1h 30m )
15:00-16:30 | Implement Player Controller ( 1h 30m )
16:30-19:30 | Implement Time Hop System ( 3h )
20:00-22:00 | Reserch about Inventory System ( 2h )
22:00-01:00 | Implement Inventory System ( 3h )
* Problem : Can't get mouse position value correctly.
Solve by item ui parent at canvas and change logic get mouse position.

30/09/2024
01:00-01:30 | Fix get wrong mouse position ( 30m )
* Problem : Merge item stack work incorrect.
Solve by update text and destroy game object not script.
01:30-02:00 | Fix can't merge item ui ( 30m )

20:30-22:00 | Implement remove item from inventory and select item from hot bar ( 1h 30m )
22:00-22:30 | Change logic time hop trigger ( 30m )
23:00-00:30 | Implement enemy AI ( 1h 30m )

01/10/2024
20:30-21:30 | Implement base entity ( 1h )
21:30-23:30 | Implement combat system ( 2h )
23:30-00:30 | Update log ( 1h )

= Inventory System =
I've never made a game with an inventory system before, so it was quite challenging to manage the data and design the structure in this system.

= Time =
Due to limited time, it was very difficult for me to allocate time to design the code structure properly. 
As a result, many parts of the code have an inappropriate structure and were intended to be temporary.

= Completed =
- Player input system
- Time hop system
- Inventory system
- Combat system

= Remaining to be done =
- Camera system for make this game is 2D side-scrolling game.
- Crafting system (Instant Crafting and Station Crafting)

= Estimate of additional time needed =
- More 1 day for complete camera and crafting system.
But I think each system should have more time to structure its code better, such as large systems like Inventory and Combat system.

= Next steps =
- Add camera system
- Add crafting system by implementing a function that calculates all items in the inventory and checks if an item can be craft when the player presses the craft button (e.g., if the player wants to craft a Storage Chest, press at the Storage Chest button in the crafting panel).
- Add crafting data to collect the ingredients needed for each crafting item.