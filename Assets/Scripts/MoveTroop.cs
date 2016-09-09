using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MoveTroop : MonoBehaviour {

    public Vector3 targetPosition;
    private float speed;
    private bool needToMove;
    private List<Vector3> path;
    private bool first;
    

    public void SetTargetPosition(Vector3 target)
    {

        speed = gameObject.GetComponent<UnitScript>().unit.GetSpeed();
        needToMove = true;

        targetPosition = target;
        targetPosition.z = 0;
        path = new List<Vector3>();
        path = GameManager.instance.FindPath(gameObject, targetPosition);
        Debug.Log(path.Count);
        first = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (path.Count != 0)
        {
            float step;
            if (first) { step = 0; first = false; } // first needed because the time.deltatime in the frist time is pretty big
            else
            {
                step = speed * Time.deltaTime;
            }
            Debug.Log(step);
            Vector3 temppos = gameObject.transform.position;
            int i = 0;
            do // more effective??
            {
                
                temppos = Vector3.MoveTowards(gameObject.transform.position, path.ElementAt(i), step);
                i++;
            } while (i < path.Count && temppos == path.ElementAt(i-1)); // needed i<=??
            Debug.Log(i);
            gameObject.transform.position = temppos;
            path.RemoveRange(0, i); // or i+1 or i-1
        }
        if (path.Count == 0)
        {
            needToMove = false;
        }

    }
}
