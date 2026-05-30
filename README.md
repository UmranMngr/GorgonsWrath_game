# Gorgon's Wrath

**Gorgon's Wrath** is a 3D atmospheric action-evasion game developed in Unity. You play as Lyra, attempting to escape the ancient forest guardian, the Gorgon, while collecting soul essences.

##  Project Structure
- **/Code:** Contains core logic classes including `PlayerController`, `PathFollower` (AI), `GameManager`, and `LevelExit`.
- **/Scenes:** Contains the game flow from `IntroScene` and `MainMenu` to levels (`Level1, 2, 3`) and `ResultScreen`.
- **/Prefabs:** Includes essential game objects like `Player`, `Enemy`, `GameManager`, `MusicManager`, and `UI_Canvas`.
- **/Controller 1:** Contains specialized controller setups for `Player` and `Enemy`.

##  Gameplay Mechanics
* **Dynamic AI:** The `PathFollower` script records player movement and guides the Gorgon.
* **Flow Control:** Managed by `GameManager` and `TransitionManager` across different scenes.
* **Collection:** Use the `Collectible` script to gather essences and progress through levels.

##  Tech Stack
* **Engine:** Unity
* **Language:** C#
* **UI:** TextMesh Pro

##  Getting Started
1. Open the project in Unity.
2. Navigate to `Assets/Scenes` and open `MainMenu` to start the game.
3. Ensure all prefabs in `Assets/Prefabs` are correctly assigned in the Inspector for your scenes.

##  Credits
Developed as part of a Computer Engineering project at Aydın Adnan Menderes University.

Project Structure

Assets/
├── Animation/           # Animation clips and Animator Controllers

├── Audio/               # Music tracks and sound effect files

├── Code/                # C# script files (Displayed: image_3045e3.png)

├── Controller 1/        # Specialized controller logic and configurations

├── Materials/           # Material files and shader assets

├── Prefabs/             # Game object prefabs (Displayed: image_3045fe.png)

│   ├── Collectibles/    # Collectible items and pick-up objects

│   └── (Enemy, Player, GameManager, etc.)

├── Scenes/              # All level and scene files (Displayed: image_304601.png)

├── Settings/            # Project configuration and settings files

├── TextMesh Pro/        # UI font assets and text materials

└── TutorialInfo/        # Informational documents and tutorial guides

IntroScene
<img width="1615" height="819" alt="image" src="https://github.com/user-attachments/assets/63cee279-0ba2-4605-a817-2fc3b562d600" />
ResultScreen
<img width="1003" height="668" alt="image" src="https://github.com/user-attachments/assets/55bbe2fa-ec38-48dd-a0e8-5035e4133544" />
TransitionScene
<img width="1064" height="675" alt="image" src="https://github.com/user-attachments/assets/912caa29-b485-476a-9747-5ce2c843b0cd" />
MainMenu
<img width="1177" height="795" alt="image" src="https://github.com/user-attachments/assets/b62d5677-6e37-40df-a0d0-033f71f00723" />
Level1
<img width="1417" height="759" alt="image" src="https://github.com/user-attachments/assets/826c15bd-10b3-47a5-b6d2-8d11fa82f3c2" />
Level2
<img width="1402" height="785" alt="image" src="https://github.com/user-attachments/assets/9be417ca-e199-4253-ab26-f59d865ae6cd" />
Level3
<img width="1535" height="761" alt="image" src="https://github.com/user-attachments/assets/efd3d405-05f5-4e4b-8267-8175dc330bca" />


