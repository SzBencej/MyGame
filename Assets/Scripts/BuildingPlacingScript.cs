using UnityEngine;
using System.Collections;

public class BuildingPlacingScript : MonoBehaviour {

	private Camera cam;

	private bool dragged;
	private bool placeable;

	// Use this for initialization
	void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
		dragged = true;
		placeable = true;
	}
	
	// Update is called once per frame
	// Onescape  remove dragging and remove building
	void Update () {
		if (dragged) {
			Vector3 position = cam.ScreenToWorldPoint (Input.mousePosition); // TODO: utility
			position.z = 0.0f;
			transform.position = position;

			if (placeable && Input.GetMouseButtonUp (0)) {
				dragged = false;
				placeable = false;
				GameManager.instance.AddBuilding (gameObject);
			}
		}
	}

	public void setDragged(bool dragged) {
		this.dragged = dragged;
	}


	void OnTriggerEnter2D (Collider2D other) {
		// TODO: refactor
		Color color = GetComponent<Renderer> ().material.color;
		color.a = 0.5f;
		GetComponent<Renderer> ().material.color = color;
		placeable = false;
	}

	//bug if there are still in collosion after exit with another 
	void OnTriggerStay2D(Collider2D other) {
		// TODO: refactor
		Color color = GetComponent<Renderer> ().material.color;
		color.a = 0.5f;
		GetComponent<Renderer> ().material.color = color;
		placeable = false;
	}

	void OnTriggerExit2D(Collider2D other) {
		Color color = GetComponent<Renderer> ().material.color;
		color.a = 1.0f;
		GetComponent<Renderer> ().material.color = color;
		placeable = true;
	}

}