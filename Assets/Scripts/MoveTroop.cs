using UnityEngine;
using System.Collections;

public class MoveTroop : MonoBehaviour {

    public Vector3 targetPosition;
    private float speed;
    private bool needToMove;
    

    public void SetTargetPosition(Vector3 target)
    {

        speed = gameObject.GetComponent<UnitScript>().unit.GetSpeed();
        needToMove = true;

        targetPosition = target;
    }
	
	// Update is called once per frame
	void Update () {
        if (needToMove)
        {
            float step = speed * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, step);
            Debug.Log(speed);
        }
        if (gameObject.transform.position == targetPosition)
        {
            needToMove = false;
        }

    }
}
