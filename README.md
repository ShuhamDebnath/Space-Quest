# Space Quest 🚀

![Unity](https://img.shields.io/badge/Made%20with-Unity-black?style=for-the-badge&logo=unity)
![C#](https://img.shields.io/badge/Language-C%23-239120?style=for-the-badge&logo=c-sharp)
![Status](https://img.shields.io/badge/Status-Prototype-blue?style=for-the-badge)

> **A retro-style 2D Space Shooter focusing on physics-based movement and resource management.**

---

## 🎮 About The Project

**Space Quest** is a 2D arcade shooter developed as a technical study in Unity engine fundamentals. The goal of this project was to transition from application development to game architecture, focusing on the Unity Lifecycle, Input Systems, and Object-Oriented Programming patterns in a game context.

While the core concept is based on classic arcade mechanics, the implementation focuses on clean code architecture using Singleton patterns and efficient physics calculations.

---

## ✨ Key Features

* **Physics-Based Movement:** Smooth player controls using Rigidbody2D mechanics rather than transform manipulation.
* **Energy Management System:** A custom resource system where players must balance usage between "Boosting" (Speed) and "Regeneration" (Survival).
* **Reactive UI:** Real-time HUD updates using Observer-like patterns to sync Player Data with UI Elements.
* **Input Handling:** Supports keyboard interaction with responsive input smoothing.

---

## 📸 Gameplay Gallery

| Main Menu | Combat Action |
|:---:|:---:|
| <img src="./Screenshots/menu.png" width="400" alt="Main Menu"> | <img src="./Screenshots/gameplay.png" width="400" alt="Gameplay"> |

> *Note: Screenshots demonstrate the current build state.*

---

## 🛠️ Technical Implementation

This project implements several core Game Development concepts:

* **Singleton Pattern:** Used for the `GameManager` and `UIController` to ensure centralized state management.
* **FixedUpdate vs Update:** Physics calculations (velocity, drag) are strictly handled in fixed time steps to ensure frame-rate independent gameplay.
* **Mathf & Vectors:** Usage of normalized vectors for consistent diagonal movement speed.

---

## 🚀 How to Run

1.  Clone the repository:
    ```bash
    git clone [https://github.com/YOUR_USERNAME/SpaceQuest.git](https://github.com/YOUR_USERNAME/SpaceQuest.git)
    ```
2.  Open **Unity Hub**.
3.  Click **Add** and select the cloned folder.
4.  Open with Unity Version **2022.3 LTS** (or higher).
5.  Press **Play** in the Editor.

---

## 📝 Disclaimer

This project is created for **educational purposes** to demonstrate proficiency in Unity 2D development. It serves as a study of game loops, component-based architecture, and C# scripting.

---
