# 🕹️ Platformer Project

Welcome to the Platformer codebase — a modular, extensible framework for building 2D platformer mechanics in MonoGame. This project is structured around clear architectural boundaries, enabling developers to plug in custom rendering, element selection, and movement logic with minimal friction.

---

## 🚀 Getting Started

This repository is organized into modular components. To onboard effectively, follow the sequence below:

---

### 1️⃣ [`PlatformerSceneEngine`](https://github.com/Arthur-Woodlee/Platformer/tree/main/Platformer.PlatformerSceneEngine)

This module contains the core scene object logic, including:

- `ElementSelectorSceneObject`: a drawable object that delegates element selection
- `DrawConfig`: declarative rendering configuration
- Texture encapsulation and draw orchestration

Start here to understand how rendering and update cycles are managed.

---

### 2️⃣ [`PlatformerElementSelector`](https://github.com/Arthur-Woodlee/Platformer/tree/main/Platformer.PlatformerElementSelector)

This module defines the `IElementSelector` interface — the strategy for selecting visual elements per update cycle.

- Enables custom element selection logic
- Used by `ElementSelectorSceneObject` to update texture and position
- Includes default implementation: `SequenceElementSelector`

Explore this module to learn how rendering decisions are decoupled from scene objects.

---

### 3️⃣ [`PlatformerSequenceElementSelector`](https://github.com/Arthur-Woodlee/Platformer/tree/main/Platformer.PlatformerSequenceElementSelector)

This module introduces sequence-driven logic and control-based movement:

- `ISequenceExecutionRule`: interface for evolving sequences
- `PlatformMovementExecutionRule`: verifies control input and returns movement sequences
- `PlatformMoveSequence`: encapsulates movement, collision, and texture sequencing

Dive into this module to understand how input and rules drive animation and movement.

---

## 🧩 Architecture Overview

The Platformer framework is built around:

- **Encapsulation**: `Texture2D` access is private and controlled
- **Modularity**: Each component is independently testable and extensible
- **Declarative rendering**: via `DrawConfig`
- **Sequence-driven animation**: using `ISequence` and `ISequenceExecutionRule`

---

## 🧪 Contributing & Testing

- Most module's include its own `README.md` for deeper onboarding

