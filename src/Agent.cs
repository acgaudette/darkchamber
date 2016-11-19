//Agent.cs
//Created by Aaron C Gaudette on 18.11.16

namespace Darkchamber{
   public abstract class Agent<N> : Entity<N> : IMoveable<Agent<N>>{
      //Extend in subclasses to add parameters and render

      public virtual bool MoveTo(Node to){
         return Move(at.PathTo(to)[0]);
      }
      public virtual bool Move(Node linked){
         if(!at.Linked(adjacent))return false;
         at = adjacent;
         return true;
      }
      public virtual bool Move(int direction){
         Node to = at.GetLink(direction);
         if(to==null)return false;
         at = to;
         return true;
      }
   }
}
