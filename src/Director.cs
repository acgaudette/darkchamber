//Director.cs
//Created by Aaron C Gaudette on 18.11.16
//Base class for managing entities on a map

using System.Collections.Generic;

namespace Darkchamber{
   public class Director<N> where N:Node{
      public readonly Map<N> map;
      protected List<Entity> entities;
      protected List<Agent> agents;

      //Extend in subclasses to add game state and simulation

      public Director(Map<N> map){
         this.map = map;
         entities = new List<Entity>();
         agents = new List<Agent>();
      }

      public bool EntityExists(N node){
         foreach(Entity entity in entities)
            if(entity.at==node)return true;
         return false;
      }
      //An inverted implementation of this method would be faster, but consume more memory
      public Entity[] EntitiesAt<E>(N node) where E:Entity{
         List<E> located = new List<E>();
         foreach(E entity in entities)
            if(entity.at==node)located.Add(entity);
         return located.ToArray();
      }

      public void Spawn(Entity entity){
         entities.Add(entity);
         if(entity is Agent)
            agents.Add((Agent)entity);
      }
      public bool Despawn(Entity entity){
         foreach(Entity e in entities){
            if(e==entity){
               entities.Remove(e);
               if(entity is Agent)
                  agents.Remove((Agent)e);
               return true;
            }
         }
         return false;
      }
   }
}
