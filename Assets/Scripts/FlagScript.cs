using UnityEngine;
using System.Collections;

public class FlagScript : MonoBehaviour
{

    private Camera cam;
    private bool held;
    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        held = false;
    }
    // Use this for initialization
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            held = true;
            GameManager.instance.bm.GetComponent<UnitSelect>().canSelect = false;
            gameObject.GetComponent<PlacingScript>().SetPlceable(true);
        } else if (Input.GetMouseButtonUp(0))
        {
            held = false;
            GameManager.instance.bm.GetComponent<UnitSelect>().canSelect = true;
            gameObject.GetComponent<PlacingScript>().SetPlceable(false);
        }
       
 
    }

    void Update()
    {
        if (held)
        {
            held = true;
            Vector3 position = cam.ScreenToWorldPoint(Input.mousePosition); // TODO: utility
            position.z = 0.0f;
            transform.position = position;
        }
    }
}
