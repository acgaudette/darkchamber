//B2dMap.cs
//Created by Aaron C Gaudette on 18.11.16

using System.Collections.Generic;

namespace Darkchamber.Basic2D{
   public class B2dMap : Map<Room>{
      Dictionary<Position,Room> grid;

      public B2dMap():base(){
         grid = new Dictionary<Position,Room>();

         //Create root node
         Room root = new Room(this,Position.origin);
         root.Initialize();
      }
      public Room root{get{return this[0];}}

      //Maintain a grid of rooms for easy lookup
      public void RegisterOnGrid(Room room){
         grid[room.position] = room;
      }
      public bool DeregisterOnGrid(Room room){
         return grid.Remove(room.position);
      }
      public Room OnGrid(Position position){
         return grid.ContainsKey(position)?
            grid[position]:null;
      }
   }
}
