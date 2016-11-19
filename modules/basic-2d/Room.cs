//Room.cs
//Created by Aaron C Gaudette on 18.11.16

using System;

namespace Darkchamber.Basic2D{
   public class Room : Node{
      public Position position;

      //Extend in subclasses to add parameters and render

      public Room(Map<Node> map, Position position):base(map){
         this.position = position;
      }

      public bool Connect(Direction direction, Room other){
         if(!Position.Adjacent(this.position,other.position))
            return false;

         Link((int)direction,other);
         //Reflect
         other.Link((int)direction.Opposite(),this);
         return true;
      }
      public bool Disconnect(Direction direction){
         //Reflect
         if(Linked((int)direction))
            GetLink<Room>((int)direction).Unlink((int)direction.Opposite());
         return Unlink((int)direction);
      }

      //TODO: Implement A* with Position.Distance()
      //public override Room[] PathTo(Room target){}
   }
}
