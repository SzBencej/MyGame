using System;
using System.Collections.Generic;

public class GameModel {
	
	private List<Building> buildings;
	private Resource resources;
	private GraphNode buildingDepTree;

	public GameModel () {
		buildings = new List<Building> ();
		buildingDepTree = new GraphNode (1);
		GraphNode node = buildingDepTree.AddChild (2);
		node.AddChild (3);
		InitResources ();
	}

	private void InitResources() {
		resources = new Resource ();
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
		buildingDepTree.SetAvailable(building.GetBuildingID(), true);
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
		return buildingDepTree.IsAvailable (buildingID);
	}
}

