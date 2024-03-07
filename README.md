# Color Blast Game

This is a Color Blast game like Toon Blast that was developed in a week.

Written with OOP artchitecture, it includes Patterns Such as:
* Builder
* Factory
* Strategy
* State Machine
* Observer
* Command
* Singleton

Features:
* Resizable Board, the board width and height values as well as the board placement can be adjusted from the level editor scriptable object.
* **Generic 2DGrid** which the board uses to place tiles which hold the item datas.
* The level editor features a **serialized 2d grid** of ItemDatas which is also a scriptable object. This grid can be filled randomly by an editor button or by drag and drop in the inspector.
* The Item Datas hold the sprite and ItemColor(enum) values.
* Items are created using **Builder** and **Factory** patterns. So it is extremely easy to write a recipe of BoardItemComponents and create a new kind of BoardItem such as Duck, Balloon or PowerUp.
* Unity's so called **New Input System** is used supporting mouse and touch inputs with future development in mind.
* **Dependency Inversion** princaple is used so most systems can easily be replaced.
* The **MatchFinder** finds all neighbouring same colored tiles using a **Flow Field Algorithm**.
* The MatchManager processes the found matched tiles and either creates a powerup or simply pops the matched tiles or the clicked power up.
* Each popped item uses it's own popping logic, requesting particles, audios and/or sending an event to register itself for being processed by game loop manager.
* With fluent gameplay in mind (you can keep popping items while more items are falling or rocket is exploding etc) the game has a very loose game loop manager. Which currently depends on move and goal managers but can easily be replaced by another type of loop manager.
* The game loop manager processes the popped item through GoalManager to check if goals are completed and than check move manager after board settlement to set the Win/Lose status.
* Very basic particle and audio systems.

## Thigs that could be improved

Since the project was developed in a limited amount of time there are alot of things that could be improved but here are somethings that come to mind:
* The UI Animation system is currently very rigid and can only play the "Path" goal collection animation. This could be replaced with a proper animation system.
* The Builder and Factory systems for board items could be improved.
* The project has a very basic audio manager.
* I would like to add more particle effects. It is currently extremely easy to add particle and sound effects but it takes valuable time to chose and import them.
* There are many places that a null check should be added with some Logging system.
* The Service Locator isn't written properly. It's more or less just a singleton holding references.
* The project doesn't really support landscape screen sizes.
* The mobile input using the unity input system isn't as fluent as I would like it to be. Propaply something about the press amount settings.
* The project is begging for Addressable and Zenject usage, Alas I am not super fluent with them and thought it would be better to focus my time on other areas of the project.
* Adding a main menu and a loading screen would be nice.
 
### Game Play
![](https://github.com/TrasqualInterviewCases/Peak-Case/blob/main/Gifs/Gameplay.gif)


### Adjustable Board Size and Placement
![](https://github.com/TrasqualInterviewCases/Peak-Case/blob/main/Gifs/boardsize.gif)


### Can Easily Add Levels
![](https://github.com/TrasqualInterviewCases/Peak-Case/blob/main/Gifs/differentlevels.gif)


### Adjustable For Different Screens
![](https://github.com/TrasqualInterviewCases/Peak-Case/blob/main/Gifs/screensize.gif)

### Trello was used for tracking the progress of the project
![](https://github.com/TrasqualInterviewCases/Peak-Case/blob/main/Gifs/Trello.png)
