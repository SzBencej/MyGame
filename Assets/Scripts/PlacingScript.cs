using UnityEngine;
using System.Collections;

public class PlacingScript : MonoBehaviour {

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
	
    public void SetPlceable(bool val)
    {
        placeable = val;
    }

    public void SetDragged(bool val)
    {
        dragged = val;
    }
	
	public bool IsDragged()
    {
        return dragged;
    }

    public bool IsPlaceable()
    {
        return placeable;
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