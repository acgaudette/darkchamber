//Director.cs
//Created by Aaron C Gaudette on 18.11.16
//Base class for managing entities

using System.Collections.Generic;

namespace Darkchamber{
   public abstract class Director<N> where N:Map<N>.Node{
      protected List<Entity<N>> entities;
      protected List<Agent<N>> agents;

      //Extend in subclasses to add game state and simulation

      public Director(){
         entities = new List<Entity<N>>();
         agents = new List<Agent<N>>();
      }

      //Is a given entity present?
      public bool Live(Entity<N> entity){
         return entities.Contains(entity);
      }

      public bool EntityExists<E>(N node) where E:Entity<N>{
         foreach(Entity<N> entity in entities)
            if(entity.at==node && entity is E)return true;
         return false;
      }
      //An inverted implementation of this method would be faster, but consume more memory
      public E[] EntitiesAt<E>(N node) where E:Entity<N>{
         List<E> located = new List<E>();
         foreach(Entity<N> entity in entities)
            if(entity.at==node && entity is E)located.Add(entity as E);
         return located.ToArray();
      }

      public abstract N Closest<E>(N node) where E:Entity<N>;

      public E SpawnImmediate<E>(E entity) where E:Entity<N>{
         SpawnImmediate(entity as Entity<N>);
         return entity;
      }
      public void SpawnImmediate(Entity<N> entity){
         entities.Add(entity);
         if(entity is Agent<N>)
            agents.Add((Agent<N>)entity);
      }
      public bool DespawnImmediate(Entity<N> entity){
         foreach(Entity<N> e in entities){
            if(e==entity){
               entities.Remove(e);
               if(entity is Agent<N>)
                  agents.Remove((Agent<N>)e);
               return true;
            }
         }
         return false;
      }
   }
}
