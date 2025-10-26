# 🧭 `ElementSelectorSceneObject` Onboarding Guide

This document outlines the purpose, design decisions, and extension points for `ElementSelectorSceneObject`. It’s intended for contributors working within the Platformer rendering system.

---

## 📦 Purpose

`ElementSelectorSceneObject` is a drawable scene object that delegates element selection to an injected `IElementSelector`. It tracks position and texture state internally and renders using a declarative `DrawConfig`.

> 🔒 **Note**: The object itself does not select elements — that responsibility lies with the `IElementSelector` implementation.

---

## 🔐 Texture Privacy & Architectural Tradeoffs

We intentionally hide `Texture2D` from consumers of the assembly to prevent external mutation and misuse. While copying `Texture2D` would offer stronger encapsulation, it is not viable in a real-time rendering context due to performance constraints.

Instead, we’ve chosen to:

- Keep `_texture` private
- Avoid exposing it via public or protected accessors
- Centralize all draw logic within the `Draw()` method

This design does **not conform to the Open/Closed Principle (OCP)**, as extending draw behavior requires modifying `Draw()` and `DrawConfig`. However, this tradeoff is **localized and intentional**, ensuring that:

- All rendering logic is centralized
- Texture access remains encapsulated
- Extension points are clear and maintainable

---