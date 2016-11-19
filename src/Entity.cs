//Entity.cs
//Created by Aaron C Gaudette on 18.11.16
//Base class for agents, items, features, etc.

namespace Darkchamber{
   public abstract class Entity{
      public Node at;

      public Entity(Node spawn){
         at = spawn;
      }
   }

   interface IMoveable<E> where E:Entity{
      bool MoveTo(Node to);
      bool Move(Node linked);
      bool Move(int direction);
   }
}
