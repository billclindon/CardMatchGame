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

---

### ✅ Commit 3 – Card Logic + View Layer
- Implemented `Card` core logic class
- Implemented `CardView`
- Added scale-based flip animation (no rotation dependency)
- Clean separation between model and presentation

---

### ✅ Commit 4 – Board System + Input + Match Resolution
- Implemented `Board`
- Seed-based deterministic board generation
- GridLayout-based dynamic card instantiation
- Implemented `InputController`
- Implemented `MatchResolver`
- Interruptible resolution logic

---

### ✅ Commit 5 – Score System
- Added `ScoreSystem`
- Implemented combo-based scoring
- Event-driven score updates
- Fully decoupled from match logic

---

### ✅ Commit 6 – Game Controller
- Added `GameController`
- Board completion detection
- End-of-game handling
- Replay / restart flow
- Centralized game state orchestration

---

### ✅ Commit 7 – Save / Load System
- Added `SaveLoadSystem`
- Snapshot-based persistence
- Serializes board state, score, combo, and seed
- Force-resolves pending matches before saving
- Restores full gameplay state on load

---

### ✅ Commit 8 – Sound System Integration
- Integrated `SoundSystem`
- Event-driven audio triggers
- Connected to gameplay events:
  - Card flip
  - Match success
  - Match failure
  - Game over
- Fully decoupled from core gameplay logic

**Features:**
- Centralized audio management
- Clean event subscription model
- Easy extension for additional SFX
- Designed for platform-safe playback

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
- `GameController`
- `SaveLoadSystem`

### Systems
- `SoundSystem`

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
- Game flow orchestration
- Persistence
- Audio

---

## 🎯 Design Priorities

1. Requirement compliance  
2. Clean, modular architecture  
3. Scalable layout system  
4. Deterministic generation  
5. Meaningful Git commit history  
6. Optimized and warning-free implementation  

---

## 🔧 Technical Highlights

- Unity 2021 LTS
- Scale-based flip animation
- Interruptible match resolution
- Combo-based scoring
- Event-driven architecture
- Snapshot-based save/load
- Integrated sound system
- Clean game state orchestration

---

## 🧠 Notes

This prototype prioritizes gameplay architecture and clean system design over visual polish.

Commit history reflects incremental, production-style development.