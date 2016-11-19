//Map.cs
//Created by Aaron C Gaudette on 18.11.16

using System.Collections.Generic;

namespace Darkchamber{
   public abstract class Map<N> where N:Node{
      ulong idptr = 0;
      Dictionary<ulong,N> nodes;

      public void Map(){
         nodes = new Dictionary<ulong,N>();
      }

      public ulong nodeCount{get{return idptr;}}

      public ulong RegisterNode(Node node){
         nodes.Add(idptr,node);
         return idptr++;
      }
      public bool DeregisterNode(Node node){
         return nodes.Remove(node.ID);
      }
      public bool Registered(Node node){
         return nodes.ContainsKey(node.ID);
      }
   }
}
