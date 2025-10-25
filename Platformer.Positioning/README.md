# 🧭 Platformer.Positioning Namespace Onboarding

The `Platformer.Positioning` namespace provides foundational types for spatial reasoning and camera-relative rendering in a 2D platformer. It enforces **positive-space constraints** and modular scene alignment via rectangles, vectors and camera logic.

---

## 📦 Components Overview

### 🟥 `PlatformerRectangle`
Represents a non-negative rectangle in scene space.

- Enforces `x`, `y`, `width`, `height ≥ 0`
- Wraps `Microsoft.Xna.Framework.Rectangle`
- Provides:
  - `Contains(Rectangle)` and `Contains(int x, int y)`
  - `Intersects(Rectangle)`
- Immutable via public getters

**Use when:** modeling static or dynamic scene objects with rectangular bounds.

---

### 🟦 `PlatformerVector2`
Represents a non-negative 2D point or offset.

- Enforces `x`, `y ≥ 0`
- Immutable via public getters

**Use when:** positioning objects, calculating offsets, or referencing scene boundaries.

---

### 🎥 `Camera`
Manages player-centric scene alignment and drawing offsets.

- Tracks a `IRectangleSceneObject` (typically the player)
- Uses:
  - **Lens**: the visible scene area
  - **FocusBox**: the region the subject must stay within
  - **EndScene**: the scene boundary limit
- Automatically shifts lens to keep subject visible
- Provides:
  - `SetSubject(IRectangleSceneObject)`
  - `Update()` to re-align lens/focusBox
  - `GetDrawingPosition(PlatformerRectangle)`
  - `GetDrawingPosition(PlatformerVector2)`

**Use when:** drawing scene objects relative to the player’s position, ensuring visibility and alignment.

---

## 🧪 Testing & CI Notes
- All types are covered by `Platformer.Tests.Positioning`

---
