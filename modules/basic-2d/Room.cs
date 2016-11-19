//Room.cs
//Created by Aaron C Gaudette on 18.11.16

using System.Math;

namespace Darkchamber.Basic2D{
   public class Room : Node{
      public Position position;

      //Extend to add parameters and render

      public Room(B2dMap map, Position position):base(map){
         this.position = position;
      }

      public bool Connect(Direction direction, Room other){
         if(!Position.Adjacent(this.position,other.position))
            return false;

         Link(direction,other);
         //Reflect
         other.Link(direction.Opposite(),this);
         return true;
      }
      public bool Disconnect(Direction direction){
         //Reflect
         if(Linked(direction)
            GetLink(direction).Unlink(direction.Opposite());
         return Unlink(direction);
      }
      //also add room functions, but iterate through enum

      //TODO: Implement A* with Position.Distance()
      //public override Room[] PathTo(Room target){}
   }
}
