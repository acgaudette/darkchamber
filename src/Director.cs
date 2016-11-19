//Director.cs
//Created by Aaron C Gaudette on 18.11.16
//Base class for managing entities on a map

using System.Collections.Generic;

namespace Darkchamber{
   public class Director<N> where N:Node{
      public readonly Map<N> map;
      protected List<Entity<N>> entities;
      protected List<Agent<N>> agents;

      //Extend in subclasses to add game state and simulation

      public Director(Map<N> map){
         this.map = map;
         entities = new List<Entity<N>>();
         agents = new List<Agent<N>>();
      }

      public bool EntityExists(N node){
         foreach(Entity entity in entities)
            if(entity.at==node)return true;
      }
      //An inverted implementation of this method would be faster, but consume more memory
      public Entity[] EntitiesAt(N node){
         List<Entity<N>> located = new List<Entity<N>>();
         foreach(Entity entity in entities)
            if(entity.at==node)located.Add(entity);
         return located.ToArray();
      }

      public void Spawn(Entity entity){
         entities.Add(entity);
         if(entity is Agent)
            agents.Add(entity);
      }
      public bool Despawn(Entity entity){
         foreach(Entity e in entities){
            if(e==entity){
               entities.Remove(e);
               if(entity is Agent)
                  agents.Remove(e);
               return true;
            }
         }
         return false;
      }
   }
}
