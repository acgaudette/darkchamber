// Node.cs
// Created by Aaron C Gaudette on 18.11.16

using System.Collections.Generic;

namespace Darkchamber {

  public partial class Map<N> where N : Map<N>.Node {
    // Nested
    public class Node {
      protected Map<N> map; // Map reference

      ulong id;
      SortedDictionary<int, N> links;

      // Extend in subclasses to add parameters and render

      public ulong ID { get { return id; } }

      public bool Live { get { return init; } }
      bool init;

      public Node(Map<N> map) {
        this.map = map;
        links = new SortedDictionary<int, N>();
      }

      // Registration separated to allow multiple initialization
      public void Initialize(Map<N> map) {
        this.map = map;
        Initialize();
      }

      public virtual void Initialize() {
        if (init) return;
        init = true;
        id = map.RegisterNode(this as N);
      }

      // Isolate
      public virtual void Remove() {
        map.DeregisterNode(this as N);

        // Remove links to this node
        foreach (N link in links.Values)
          link.Unlink(this as N);

        links.Clear();
        map = null;
        init = false;
      }

      public bool Linked(N other) {
        return GetLink<N>(other) != null;
      }

      public T GetLink<T>(N other) where T : N {
        if (this is T && this == other)
          return (T)(this as N);

        foreach (N link in links.Values)
          if (link == other && link is T) return (T)link;

        return null;
      }

      public bool Linked(int direction) {
        return links.ContainsKey(direction);
      }

      public T GetLink<T>(int direction) where T : N {
        N result;
        links.TryGetValue(direction, out result);
        return result is T ? (T)result : null;
      }

      public void Link(int direction, N other) {
        links[direction] = other;
      }

      public bool Unlink(int direction) {
        return links.Remove(direction);
      }

      public void Unlink(N other) {
        // Remove all links with other node
        List<int> keys = new List<int>(links.Keys);
        foreach (int key in keys)
          if (links[key]==other) links.Remove(key);
      }

      // Pathfinding
      public N[] PathTo(N target) {
        if (!map.Registered(target as N) || !CompareDomain(this, target))
          return null;

        // BFS
        Queue<N> frontier = new Queue<N>();
        Dictionary<N, N> previous = new Dictionary<N, N>();
        N current = this as N;
        frontier.Enqueue(current);
        previous[current] = null;

        while (frontier.Count != 0) {
          current = frontier.Dequeue();
          if (current==target) break; // Early exit

          foreach (N link in current.links.Values) {
            if (previous.ContainsKey(link)) continue;
            frontier.Enqueue(link);
            previous[link] = current;
          }
        }

        // Broken map
        if (current!=target) return null;

        // Trace back, create path
        List<N> path = new List<N>();
        path.Add(current);

        while (current!=this as N) {
          current = previous[current];
          path.Add(current);
        }

        path.Reverse();
        path.RemoveAt(0);
        return path.ToArray();
      }

      public static bool CompareDomain(Node n0, Node n1) {
        return n0.map==n1.map;
      }

      // TODO: Override Object.Equals() and Object.GetHashCode()
      public static bool operator ==(Node n0, Node n1) {
        bool n0Null = object.ReferenceEquals(n0, null);
        bool n1Null = object.ReferenceEquals(n1, null);
        if (n0Null && n1Null) return true;
        if (n0Null || n1Null) return false;
        return CompareDomain(n0, n1) && n0.ID==n1.ID;
      }

      public static bool operator !=(Node n0, Node n1) {
        bool n0Null = object.ReferenceEquals(n0, null);
        bool n1Null = object.ReferenceEquals(n1, null);
        if (n0Null && n1Null) return false;
        if (n0Null || n1Null) return true;
        return !CompareDomain(n0, n1) || n0.ID != n1.ID;
      }
    }
  }
}
