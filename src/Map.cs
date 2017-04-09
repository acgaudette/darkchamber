// Map.cs
// Created by Aaron C Gaudette on 18.11.16

using System.Collections.Generic;

namespace Darkchamber {

  public partial class Map<N> where N : Map<N>.Node {
    ulong idptr, offset;
    Dictionary<ulong,N> nodes;

    public Map() {
      idptr = offset = 0;
      nodes = new Dictionary<ulong, N>();
    }

    public N this[ulong i]{
      get {
        return nodes.ContainsKey(i) ? nodes[i] : null;
      }
    }

    public ulong NodeCount {
      get { return idptr-offset; }
    }

    public bool Registered(N node) {
      return node != null && nodes.ContainsKey(node.ID);
    }

    ulong RegisterNode(N node) {
      nodes.Add(idptr,node);
      return idptr++;
    }

    bool DeregisterNode(N node) {
      if (nodes.Remove(node.ID)) {
        offset++;
        return true;
      }
      return false; // Not registered
    }
  }
}
