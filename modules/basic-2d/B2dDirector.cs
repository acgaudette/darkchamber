// B2dDirector.cs
// Created by Aaron C Gaudette on 30.11.16

using System.Collections.Generic;

namespace Darkchamber.Basic2D {

  public class B2dDirector : Director<Room> {
    List<Entity<Room>> entitiesToAdd;
    List<Entity<Room>> entitiesToRemove;

    public B2dDirector() {
      entitiesToAdd = new List<Entity<Room>>();
      entitiesToRemove = new List<Entity<Room>>();
    }

    public override Room Closest<E>(Room node) {
      // Not optimal
      double minDistance = -1;
      Room target = null;

      foreach (Entity<Room> e in entities) {
        if (!(e is E)) continue;
        double d = Position.DistanceSquared(node.position, e.at.position);

        if (minDistance == -1 || d < minDistance) {
          minDistance = d;
          target = e.at;
        }
      }

      return target;
    }

    public E Spawn<E>(E entity) where E : Entity<Room> {
      Spawn(entity as Entity<Room>);
      return entity;
    }

    public void Spawn(Entity<Room> entity) {
      entitiesToAdd.Add(entity);
    }

    public void Despawn(Entity<Room> entity) {
      entitiesToRemove.Add(entity);
    }

    // Update all entities
    public void Poll() {
      foreach (Entity<Room> e in entities) {
        IPollable p = e as IPollable;
        if (p != null) p.Poll();
      }

      Postprocess();
    }

    // Spawn and despawn after poll
    void Postprocess() {
      foreach (Entity<Room> e in entitiesToRemove)
        DespawnImmediate(e);
      entitiesToRemove.Clear();

      foreach (Entity<Room> e in entitiesToAdd)
        SpawnImmediate(e);
      entitiesToAdd.Clear();
    }
  }
}
