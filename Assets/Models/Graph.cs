using System;
using System.Collections.Generic;

public class GraphNode
{
	private int value;
	private bool available;
    private bool standard;
	private List<GraphNode> children;
	private List<GraphNode> parents;

    public GraphNode() { }
    public GraphNode(int value, bool standard = false) {
		this.value = value;
		children = new List<GraphNode> ();
		parents = new List<GraphNode> ();
		available = true;
        this.standard = standard;
	}

	public GraphNode(int value, GraphNode parent) {
		this.value = value;
		children = new List<GraphNode> ();
		parents = new List<GraphNode> ();
		parents.Add (parent);
        parent.children.Add(this);
		available = false;
	}

	public GraphNode AddChild(int value) {
		GraphNode child = new GraphNode(value, this);
		return child;
	}

	//Recursive availability set
	public void SetAvailable(int Id, bool available) {
		//TODO: erorr if no id found
		//Find recursively Id node
		if (Id != value && Id != 0) { //TODO: 0 is extermal
			foreach (GraphNode child in children) {
				child.SetAvailable (Id, available);
			}
			return;
		}
		if (available) {
			this.available = available; // true
			foreach (GraphNode parent in parents) {
				this.available = this.available && parent.available;
			}
		} else {
			this.available = available || standard; // false
		}
		// Recursively to children one level
		if (Id != 0) {
			foreach (GraphNode child in children) {
				child.SetAvailable (0, available);
			}
		}
	}

	public bool IsAvailable(int Id) {
		if (Id != value) { //TODO: 0 is extermal
			foreach (GraphNode child in children) {
                Boolean? result = child.IsAvailable (Id);
                if (result != null)
                {
                    return (bool)result;
                }
			}
		}
        if (Id == value)
        {
            // TODO: erro if no id found
            return available;
        }
        return false; // null if no found
	}
}
