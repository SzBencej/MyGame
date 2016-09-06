using UnityEngine;
using System.Collections;

public class BuildingPlacedScript : MonoBehaviour {

    private Camera cam;

    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }



    void Update()
    {// todo gameobject.getcomponent kiemelve
        bool dragged = gameObject.GetComponent<PlacingScript>().IsDragged();
        if (dragged)
        {
         
            Vector3 position = cam.ScreenToWorldPoint(Input.mousePosition); // TODO: utility
            position.z = 0.0f;
            transform.position = position;

            bool placeable = gameObject.GetComponent<PlacingScript>().IsPlaceable();
            if (placeable && Input.GetMouseButtonUp(0))
            {
                gameObject.GetComponent<PlacingScript>().SetDragged(false);
                gameObject.GetComponent<PlacingScript>().SetPlceable(false);
                GameManager.instance.AddBuilding(gameObject);
                gameObject.GetComponent<BuildingScript>().Placed = true;
                gameObject.GetComponent<BuildingScript>().AddFlag();
            }
            else if (placeable && Input.GetMouseButtonUp(1))
            {
                Destroy(gameObject);
            }
        }
    }

}
