//Utility.cs
//Created by Aaron C Gaudette on 18.11.16
//

using System.Math;

namespace Darkchamber.Basic2D{
   enum Direction{NORTH,EAST,SOUTH,WEST};
   public static class Extensions{
      public static Direction Opposite(this Direction direction){
         switch(direction){
            case Direction.NORTH:
               return Direction.SOUTH;
            case Direction.EAST:
               return Direction.WEST;
            case Direction.SOUTH:
               return Direction.NORTH;
            case Direction.WEST:
               return Direction.EAST;
         }
      }
   }

   public struct Position{
      public int x, y;
      public Position(x,y){
         this.x = x; this.y = y;
      }

      public static Position operator +(Position p0, Position p1){
         return new Position(p0.x+p1.x,p0.y+p1.y);
      }
      public static Position operator -(Position p){
         return new Position(-p.x,-p.y);
      }
      public override string ToString(){
         return "("+x+","+y+")";
      }

      public static double DistanceSquared(Position p0, Position p1){
         return (p1.x-p0.x)*(p1.x-p0.x) + (p1.y-p0.y)*(p1.y-p0.y);
      }
      public static double Distance(Position p0, Position p1){
         return Math.Sqrt(DistanceSquared(p0,p1));
      }

      public static bool Adjacent(Position p0, Position p1){
         return Math.Abs(p1.x-p0.x==1) && Math.Abs(p1.y-p0.y==1);
      }
   }
}
