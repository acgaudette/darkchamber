//Agent.cs
//Created by Aaron C Gaudette on 18.11.16

namespace Darkchamber{
   public abstract class Agent : Entity, IMoveable<Agent>{
      //Extend in subclasses to add parameters and render

      public Agent(Node spawn):base(spawn){}

      public virtual bool MoveTo(Node to){
         return Move(at.PathTo(to)[0]);
      }
      public virtual bool Move(Node linked){
         if(!at.Linked(linked))return false;
         at = linked;
         return true;
      }
      public virtual bool Move(int direction){
         Node to = at.GetLink<Node>(direction);
         if(to==null)return false;
         at = to;
         return true;
      }
   }
}
