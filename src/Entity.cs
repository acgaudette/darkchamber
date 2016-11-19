//Entity.cs
//Created by Aaron C Gaudette on 18.11.16
//Base class for agents, items, features, etc.

namespace Darkchamber{
   public abstract class Entity<N> where N:Node{
      N at;

      public Entity(Node spawn){
         at = spawn;
      }
   }

   interface IMoveable<E<N>> where E:Entity<N>{
      bool MoveTo(N to);
      bool Move(N linked);
      bool Move(int direction);
   }
}
