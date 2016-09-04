using UnityEngine;
using System.Collections;

public class BuildButtonScript : MonoBehaviour {

	public int buildingID;

	private Camera cam;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		// TODO: use pictures etc
	}

	public void OnBuildingButtonClick() {
		Vector3 position = cam.ScreenToWorldPoint (Input.mousePosition);
		position.z = 0.0f; // TODO z should be 0 everywhere universally
		// TODO: refactor, buildingclass needsto be set after instantination
		GameObject building = GameManager.instance.GetBuildingByID (buildingID);
		Building buildingClass = building.GetComponent<BuildingScript>().building;
		if (GameManager.instance.Affordable (buildingClass.GetCost ()) &&
			GameManager.instance.IsAvailable(buildingID)) {
			building = Instantiate (building, position, Quaternion.identity) as GameObject;
            building.GetComponent<BuildingScript>().SetBuilding(buildingClass);
		}
	}
}
