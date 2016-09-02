using System;
using System.Collections.Generic;

public class GameModel {
	
	private List<Building> buildings;
	private Resource resources;
	public GraphNode BuildingDepTree { get; set; }

	public GameModel () {
		buildings = new List<Building> ();
        InitBuildingDepTree();
		InitResources ();
	}

	private void InitResources() {
		resources = new Resource ();
	}

    private void InitBuildingDepTree()
    {
        BuildingDepTree = new GraphNode(1, true);
        GraphNode node = BuildingDepTree.AddChild(2);
        node.AddChild(3);
    }


	public void NextRound() {
		resources.Gold += 1;
		resources.Iron += 1;
		resources.Water += 1;
		resources.ManPower += 1;
		foreach(Building building in buildings) {
			resources.Add (building.GetIncome ());
		}
	}

	public void AddBuilding(Building building) { 
		buildings.Add (building);
		resources.Decrease (building.GetCost ());
		BuildingDepTree.SetAvailable(building.GetBuildingID(), true);
	}

    public void RemoveBuilding(Building building)
    {
        buildings.Remove(building);
        BuildingDepTree.SetAvailable(building.GetBuildingID(), false);
    }

    public void Decrease (Resource other)
    {
        resources.Decrease(other);
    }

	public Resource GetResource() {
		return resources;
	}

	// TODO: > operator in resource
	public bool Affordable(Resource other) {
		return other.Gold <= resources.Gold &&
		other.Iron <= resources.Iron &&
		other.Water <= resources.Water &&
		other.ManPower <= resources.ManPower;
	}

	public bool IsAvailable(int buildingID) {
		return BuildingDepTree.IsAvailable (buildingID);
	}
}

