## 🧭 Onboarding: `IElementSelector` and Sequence Evaluation Pipeline

This document explains how `ElementSelectorSceneObject` integrates with the `IElementSelector` interface, how sequences are evaluated and selected and the architectural decisions behind sequence instantiation and control verification.

---

## 🧩 `ElementSelectorSceneObject` and `IElementSelector`

`ElementSelectorSceneObject` depends on an injected `IElementSelector`. This enables developers to write custom selection logic by implementing the interface.

The project includes a default implementation: `SequenceElementSelector`.

---

## 🔁 `SequenceElementSelector`

`SequenceElementSelector` selects the current sequence based on a set of rules defined by `ISequenceExecutionRule`. Each rule is evaluated in order to determine whether a new sequence should replace the current one.

---

## 🧱 Rule Implementation: `PlatformMovementExecutionRule`

The project includes an implementation of `ISequenceExecutionRule` called `PlatformMovementExecutionRule`.

### 🔍 How It Works

- It verifies whether a movement should occur by calling `VerifyMove()`.
- `VerifyMove()` uses `ControlManager` to check which controls have been activated.
- If the move is valid, it calls `PlatformMoveSequence.Create(...)`.

### 🔁 Continuation vs. New Sequence

If the current sequence is the same as the last one returned by this rule, a new sequence is created that behaves like a continuation — for example, holding the right movement key over two batch updates.

If the current sequence is different, a fresh sequence is created using a new `TextureReel`.

---

## 🔐 Why `PlatformMoveSequence.Create()` Is Static

`PlatformMoveSequence` uses a private constructor and exposes a static `Create(...)` method.

This design allows the method to return `null` when conditions are not mets(Factory Method Pattern). It ensures that invalid or non-permissible sequences are never instantiated.

