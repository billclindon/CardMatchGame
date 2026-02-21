# CardMatch Prototype

Unity 2021 LTS – Card Match Game Prototype  
Focused on scalable architecture, clean separation of concerns, and smooth interruptible gameplay.

---

## 📌 Development Progress (Commit Breakdown)

### ✅ Commit 1 – Initial Empty Project
- Created new Unity 2021 LTS project
- Clean folder structure
- Git repository initialized
- Added proper `.gitignore`
- Marked official start of development

---

### ✅ Commit 2 – Core Data Layer
- Added `CardData` ScriptableObject
- Implemented `BoardPreset` enum for layout configurations
- Established scalable data-driven architecture
- Separated data layer from gameplay logic

**Purpose:**  
Provide flexible board configuration and extensibility for future content.

---

### ✅ Commit 3 – Card Logic + View Layer
- Implemented `Card` core logic class
- Implemented `CardView`
- Added scale-based flip animation (no rotation dependency)
- Clean separation between model and presentation

**Features:**
- Smooth flip animation
- State-driven behavior
- Prepared for non-blocking gameplay flow

---

### ✅ Commit 4 – Board System + Input + Match Resolution
- Implemented `Board` class
- Seed-based deterministic board generation
- GridLayout-based dynamic card instantiation
- Implemented `InputController`
- Implemented `MatchResolver`
- Interruptible resolution logic

**Key Behavior:**
- Continuous card flipping allowed
- No hard input lock during comparison
- Clean asynchronous match resolution
- Supports multiple layouts:
  - 2x2
  - 2x3
  - 5x6
  - Easily expandable

---

### ✅ Commit 5 – Score System
- Added `ScoreSystem`
- Implemented combo-based scoring logic
- Event-driven score updates
- Decoupled from match resolution logic

**Features:**
- Rewards consecutive matches
- Scales score dynamically via combo multiplier
- Designed for UI and future leaderboard integration

---

## 🏗 Architecture Overview

### Data Layer
- `CardData`
- `BoardPreset`

### Core Gameplay
- `Card`
- `Board`
- `MatchResolver`
- `ScoreSystem`

### Presentation Layer
- `CardView`

### Control Layer
- `InputController`

Clear separation of:
- Data
- Logic
- View
- Input
- Resolution

---

## 🎯 Design Priorities

1. Requirement compliance
2. Clean, modular code
3. Scalable layout system
4. Deterministic generation
5. Meaningful Git commit history
6. Optimized and warning-free implementation

---

## 🔧 Technical Goals

- Unity 2021 LTS
- Smooth scale-based flip animation
- Interruptible match resolution
- Deterministic board generation (seed-based)
- Layout auto-scaling
- Combo-based scoring
- Event-driven architecture

---

## 🚀 Upcoming Additions

- Save / Load system
- Sound effects integration
- Desktop + Android build testing

---

## 📁 High-Level Structure

```
Scripts/
 ├── Data/
 ├── Core/
 ├── View/
 ├── Systems/
```

---

## 🧠 Notes

This prototype prioritizes gameplay architecture and clean system design over visual polish.

Commit history reflects incremental, team-style development.