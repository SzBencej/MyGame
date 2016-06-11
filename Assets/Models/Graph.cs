using System.Collections.Generic;

public class GraphNode
{
	private int value;
	private bool available;
	private List<GraphNode> children;
	private List<GraphNode> parents;

    public GraphNode() { }
    public GraphNode(int value) {
		this.value = value;
		children = new List<GraphNode> ();
		parents = new List<GraphNode> ();
		available = true;
	}

	public GraphNode(int value, GraphNode parent) {
		this.value = value;
		children = new List<GraphNode> ();
		parents = new List<GraphNode> ();
		parents.Add (parent);
		available = false;
	}

	public GraphNode AddChild(int value) {
		GraphNode child = new GraphNode(value, this);
		children.Add (child);
		child.parents.Add(this);
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
			this.available = available; // false
		}
		// Recursively to children one level
		if (Id != 0) {
			foreach (GraphNode child in children) {
				child.SetAvailable (0, this.available);
			}
		}
	}

	public bool IsAvailable(int Id) {
		if (Id != value) { //TODO: 0 is extermal
			foreach (GraphNode child in children) {
				return child.IsAvailable (Id);
			}
		}
		// TODO: erro if no id found
		return available;
		
	}
}
