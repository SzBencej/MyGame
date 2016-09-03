using System;
using System.Collections.Generic;
using System.Linq;

public class Graph
{
    GraphNode RootNode { get; set; }
    public Graph() { 
        RootNode = new GraphNode();
    }
    private GraphNode FindNode(int value, GraphNode origin)
    {
        GraphNode n = origin;
        bool found = false;
        while (!found)
        {
            if (n.Value == value)
            {
                return n;
            }else
            {
                foreach(GraphNode child in n.Children)
                {
                    GraphNode n2 = FindNode(value, child);
                    if (n2 != null) { return n2; }
                }
                return null;
            }
        }
        return n;
    }
    public void AddChild(int parent, int value)
    {
        GraphNode parentNode = FindNode(parent, RootNode);
        if (parentNode == null)
        {
            throw new System.ArgumentException("The node with parent does not exists in AddChild call");
        }
        GraphNode child = FindNode(value, RootNode);
        if (child == null)
        {
            child = new GraphNode(value, parentNode.Parents.Count == 0);
        }

        if (!parentNode.Children.Exists(x => x == child))
        {
            parentNode.Children.Add(child);
        }
        if (!child.Parents.Exists(x => x == parentNode))
        {
            child.Parents.Add(parentNode);
        }
    }

    public bool IsAvailable(int id)
    {
        GraphNode n = FindNode(id, RootNode);
        if (n == null)
        {
            throw new System.ArgumentException("The node with id does not exists in IsAvailable call");
        }
        return (n.Available) || n.Standard;
    }

    public void SetAvailable(int id, bool available)
    {   // Find the node
        GraphNode n = FindNode(id, RootNode);
        if (n == null)
        {
            throw new System.ArgumentException("The node with id does not exists in SetAvailable call");
        }
        if (available)
        {
            // We are available if every parent is too
            n.Available = n.Parents.All(x => x.Available);
            n.Count++;
        } else
        {
            n.Available = n.Standard || (n.Parents.All(x => x.Available) && n.Parents.All(x => x.Count > 0));
            n.Count--;
        }
        // Recursive children availablility set
        Action<GraphNode> recurse = null;
        recurse = new Action<GraphNode>((node) =>
        {
            // Cannot be circular dependency
            foreach (GraphNode c in node.Children)
            {
                c.Available = c.Parents.All(x => x.Available) && c.Parents.All(x => x.Count > 0);
                    recurse(c);
            }
        });
        recurse(n);
    }


class GraphNode
{

    public int Value { get; set; }
    public bool Available { get; set; }
    public bool Standard { get; set; }
    public List<GraphNode> Children { get; set; }
    public List<GraphNode> Parents { get; set; }
    public int Count
        {
            get { return count; }
            set
            {
                count = value; if (count < 0)
                {
                    throw new ArgumentException("Building count is less than 0");
                }
            }
        }
        private int count;

        public GraphNode()
    { // The root node
        this.Value = 0;
        Children = new List<GraphNode>();
        Parents = new List<GraphNode>();
        Available = true;
        this.Standard = true;
            count = 0;
    }
    public GraphNode(int value, bool standard)
    {
        this.Value = value;
        Children = new List<GraphNode>();
        Parents = new List<GraphNode>();
        Available = false;
        Standard = standard;
            count = 0;
    }
}

}

