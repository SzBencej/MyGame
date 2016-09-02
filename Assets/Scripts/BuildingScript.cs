using UnityEngine;
using System.Collections;

public class BuildingScript : MonoBehaviour {

	public Building building;
    public bool Placed { set; get; }

	public Resource GetCost() {
		return building.GetCost();
	}

	public Resource GetIncome() {
		return building.GetIncome ();
	}

    void OnMouseOver()
    {
        if (Placed)
        {
            if (Input.GetMouseButtonDown(0)) // Left click
            {
                Color c = gameObject.GetComponent<Renderer>().material.color;
                if (c.a == 1.0f) // It is not selected
                {
                    c.a = 0.7f;
                    gameObject.GetComponent<Renderer>().material.color = c;
                }
                else
                {
                    if (GameManager.instance.Affordable(building.GetCost()))
                    {
                        GameManager.instance.DecreaseResource(building.GetCost());
                        GameManager.instance.RemoveBuilding(gameObject);
                        Destroy(gameObject);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1)) // Right click
            {
                Color c = gameObject.GetComponent<Renderer>().material.color;
                c.a = 1.0f;
                gameObject.GetComponent<Renderer>().material.color = c;
            }

        }
    }
}
