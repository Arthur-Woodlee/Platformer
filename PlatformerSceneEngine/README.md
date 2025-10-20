# PlatformerSceneEngine

This module defines the core scene lifecycle for the Platformer game engine. It provides a deterministic, iteration-based framework for updating and rendering scene objects in a way that approximates real-time movement.

---

## Evolution 1: Iteration-Based Scene Lifecycle

*Initial implementation of scene orchestration and iteration-driven object updates.*

### Core Concepts

#### Scene

The `Scene` class manages a collection of `ISceneObject` instances and orchestrates their update and draw cycles.

- `UpdateScene()` runs a fixed number of iterations per scene update.
- Each `ISceneObject` receives all iteration steps and decides when to act.
- `DrawScene()` delegates drawing to `ISceneObject`s.

#### ISceneObject

An interface that defines two methods:

- `Update(ushort updateCycleLength, ushort cycleNumber)`
- `Draw()`

Each object uses the total number of iterations and the current cycle number to control its behavior.

---

### Iteration-Based Movement Model

This engine simulates near real-time movement by dividing a scene update into multiple fixed iterations. Each object decides how frequently to act based on its desired speed or behavior.

#### Example

Two objects start at `x = 0`:

- **Object A** wants to move `+10` units
- **Object B** wants to move `+100` units
- Scene is configured with `iterations = 100`

##### Behavior

- Object A moves once every 10 iterations → 10 total moves
- Object B moves every iteration → 100 total moves

This creates a natural pacing: faster objects reach their destination sooner while slower ones progress more gradually. The result is a simulation that approximates real-time movement.

> **Note:** If two objects move at the same rate but start at different iterations, one will reach its destination before the other — reinforcing the "close to real-time" behavior.

---

### Usage

```csharp
var scene = new Scene(100);
scene.AddSceneObject(new Player());
scene.AddSceneObject(new Enemy());

scene.UpdateScene();
scene.DrawScene();