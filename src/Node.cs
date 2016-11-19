//Node.cs
//Created by Aaron C Gaudette on 18.11.16

using System.Collections.Generic;

namespace Darkchamber{
   public abstract class Node{
      readonly ulong id;
      SortedDictionary<int,Node> links;
      protected Map<Node> map; //Map reference

      //Extend in subclasses to add parameters and render

      public Node(Map<Node> map){
         this.map = map;
         id = map.RegisterNode(this);
         links = new SortedDictionary<int,Node>();
      }
      ~Node(){
         map.DeregisterNode(this);
         //Remove links to this node
         foreach(Node link in links.Values)
            link.Unlink(this);
      }

      public ulong ID{get{return id;}}

      public bool Linked(Node other){
         return GetLink<Node>(other)!=null;
      }
      public N GetLink<N>(Node other) where N:Node{
         if(this==other && this is N)return (N)this;

         foreach(Node link in links.Values)
            if(link==other && link is N)return (N)link;
         return null;
      }
      public bool Linked(int direction){
         return links.ContainsKey(direction);
      }
      public N GetLink<N>(int direction) where N:Node{
         Node result;
         links.TryGetValue(direction,out result);
         return result is N?(N)result:null;
      }

      public void Link(int direction, Node other){
         links[direction] = other;
      }
      public bool Unlink(int direction){
         return links.Remove(direction);
      }
      public void Unlink(Node other){
         //Remove all links with other node
         foreach(var link in links)
            if(link.Value==other)links.Remove(link.Key);
      }

      public Node[] PathTo(Node target){
         if(!map.Registered(target) || !CompareDomain(this,target))
            return null;

         //BFS
         Queue<Node> frontier = new Queue<Node>();
         Dictionary<Node,Node> previous = new Dictionary<Node,Node>();
         Node current = this;
         frontier.Enqueue(this);
         previous[this] = null;

         while(frontier.Count!=0){
            current = frontier.Dequeue();
            if(current==target)break; //Early exit

            foreach(Node link in links.Values){
               frontier.Enqueue(link);
               previous[link] = current;
            }
         }

         //Trace back, create path
         List<Node> path = new List<Node>();
         path.Add(target); //current
         while(current!=this){
            current = previous[current];
            path.Add(current);
         }
         path.Reverse();
         return path.ToArray();
      }

      public static bool CompareDomain(Node n0, Node n1){
         if(n0==null && n1==null)return true;
         else if(n0==null || n1==null)return false;
         return n0.map==n1.map;
      }

      //TODO: Override Object.Equals() and Object.GetHashCode()
      public static bool operator==(Node n0, Node n1){
         return CompareDomain(n0,n1) && n0.ID==n1.ID;
      }
      public static bool operator!=(Node n0, Node n1){
         return !CompareDomain(n0,n1) || n0.ID!=n1.ID;
      }
   }
}
