using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;

public class MoveTroop : MonoBehaviour {

    //public Vector3 targetPosition;
    private float speed;

    public Vector3 target;

    private Seeker seeker;

    //The calculated path
    public Path path;

    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 0.01f;

    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;





    public void Start()
    {
        seeker = GetComponent<Seeker>();


    }

    public void OnPathComplete(Path p)
    {
        Debug.Log("Yay, we got a path back. Did it have an error? " + p.error);
        if (!p.error)
        {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 1;
        }
    }

    public void FixedUpdate()
    {
        if (path == null)
        {
            //We have no path to move after yet
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            GetComponent<Animator>().SetBool("MovingUp", false);
            //Debug.Log("End Of Path Reached");
            Debug.Log(path.vectorPath[path.vectorPath.Count - 1]);
            path = null;
            return;
        }
        //Debug.Log(path.vectorPath[currentWaypoint]);
        //  Debug.Log(path.vectorPath.Count);
        //  Debug.Log(currentWaypoint);
        //Direction to the next waypoint
        Vector3 dir = (path.vectorPath[currentWaypoint]);
        dir.z = 0f;
        //dir *= speed * Time.fixedDeltaTime;
        float step = speed * Time.deltaTime;
        Debug.Log(path.vectorPath[currentWaypoint]);
        Debug.Log(dir);
        //gameObject.transform.Translate(dir);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, dir, step);
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector3.Distance(transform.position, dir) < 0.01)
        //if (gameObject.transform.position == path.vectorPath[currentWaypoint])
        {
            currentWaypoint++;
            return;
        }
    }









      public void SetTargetPosition(Vector3 target)
      {
        this.target = target;
        seeker = GetComponent<Seeker>();
        speed = gameObject.GetComponent<UnitScript>().unit.GetSpeed();
        //Start a new path to the targetPosition, return the result to the OnPathComplete function
        seeker.StartPath(transform.position, this.target, OnPathComplete);
        GetComponent<Animator>().SetBool("MovingUp", true);

        /*

          needToMove = true;

          targetPosition = target;
          targetPosition.z = 0;
          path = new List<Vector3>();
          Debug.Log("vvv");
          path = GameManager.instance.FindPath(gameObject, targetPosition);
          Debug.Log(path.Count);
          first = true;
          lastPos = gameObject.transform.position;*/
      }

    // Update is called once per frame
    /*	void Update () {
            if (needToMove && path.Count != 0)
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
                } while (step != 0 && i < path.Count && temppos == path.ElementAt(i-1)); // needed i<=??
                Debug.Log(i);
                lastPos = gameObject.transform.position;
                gameObject.transform.position = temppos;
                path.RemoveRange(0, i); // or i+1 or i-1
            } else if (needToMove && path.Count == 0)
            {
                needToMove = false;
                first = true;
            }

        }

        void OnTriggerEnter2D(Collider2D other)
        {
           // path.Clear();
           // gameObject.transform.position = lastPos;
        }


        void OnTriggerStay2D(Collider2D other)
        {

           // path.Clear();
           // gameObject.transform.position = lastPos;
        }

        void OnTriggerExit2D(Collider2D other)
        {
        }*/


}
