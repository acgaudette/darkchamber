// Entity.cs
// Created by Aaron C Gaudette on 18.11.16
// Base class for agents, items, features, etc.

namespace Darkchamber {

  public abstract class Entity<N> where N : Map<N>.Node {
    public N at;

    public Entity(N spawn) {
      at = spawn;
    }
  }

  interface IMoveable<E, N> where E : Entity<N> where N : Map<N>.Node {
    bool MoveTo(N to);
    bool Move(N linked);
    bool Move(int direction);
  }
}
