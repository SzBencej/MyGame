using UnityEngine;
using System.Collections;

public class BuildingScript : MonoBehaviour {

	public Building building;

	public Resource GetCost() {
		return building.GetCost();
	}

	public Resource GetIncome() {
		return building.GetIncome ();
	}
}
