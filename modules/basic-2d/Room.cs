//Room.cs
//Created by Aaron C Gaudette on 18.11.16

using System;

namespace Darkchamber.Basic2D{
   public class Room : Map<Room>.Node{
      public Position position;

      //Extend in subclasses to add parameters and render

      public Room(B2dMap map, Position position):base(map){
         this.position = position;
      }
      public override void Initialize(){
         base.Initialize();
         (map as B2dMap).RegisterOnGrid(this);
      }
      public override void Remove(){
         (map as B2dMap).DeregisterOnGrid(this);
         base.Remove();
      }

      public bool Connected(Direction direction){
         return Linked((int)direction);
      }
      public Room GetNeighbor(Direction direction){
         return GetLink<Room>((int)direction);
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

      public Room AddNew(Direction direction, bool autoLink = true){
         Room r = new Room(map as B2dMap,position.Neighbor(direction));
         r.Initialize();
         if(!Connect(direction,r))
            return null;

         //Link new room to adjacent rooms automatically
         if(autoLink){
            foreach(Direction d in Direction.GetValues(typeof(Direction))){
               if(d==direction.Opposite())continue;
               Room neighbor = (map as B2dMap).OnGrid(r.position.Neighbor(d));
               if(neighbor!=null)
                  r.Connect(d,neighbor);
            }
         }
         return r;
      }

      //TODO: Implement A* with Position.Distance(), cache:
      //public override Room[] PathTo(Room target){}
   }
}
