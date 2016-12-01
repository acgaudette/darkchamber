//Agent.cs
//Created by Aaron C Gaudette on 18.11.16

namespace Darkchamber{
   public abstract class Agent<N> : Entity<N>, IMoveable<Agent<N>,N> where N:Map<N>.Node{
      //Extend in subclasses to add parameters and render

      public Agent(N spawn):base(spawn){}

      public virtual bool MoveTo(N target){
         N[] path = at.PathTo(target);
         return (path==null || path.Length==0)?false:Move(path[0]);
      }
      public virtual bool Move(N linked){
         if(!at.Linked(linked))return false;
         at = linked;
         return true;
      }
      public virtual bool Move(int direction){
         N to = at.GetLink<N>(direction);
         if(to==null)return false;
         at = to;
         return true;
      }
   }
}
