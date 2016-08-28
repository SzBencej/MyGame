using UnityEngine;
using System.Collections;

public class ButtonClick : MonoBehaviour {

	void OnMouseOver()
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
                Resource cost = new Resource(5, 5, 2, 2);
                if (GameManager.instance.Affordable(cost))
                {
                    GameManager.instance.DecreaseResource(cost);
                    Destroy(gameObject);
                }
            }
        } else if (Input.GetMouseButtonDown(1)) // Right click
        {
            Color c = gameObject.GetComponent<Renderer>().material.color;
            c.a = 1.0f;
            gameObject.GetComponent<Renderer>().material.color = c;
        }

        
    }
}               
                