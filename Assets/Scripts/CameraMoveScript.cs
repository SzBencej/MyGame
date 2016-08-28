using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {

	private float camMoveSpeed;
	private Camera cam;

	enum Direction { None, Up, Down, Left, Right };

	private Direction[] directions = {Direction.None, Direction.None, Direction.None, Direction.None};

	private float width, height;

    // zooming things
    public float zoomSpeed = 1;
    public float targetOrtho;
    public float smoothSpeed = 10.0f;
    public float minOrtho = 1.0f;
    public float maxOrtho = 20.0f;

    void Start () {
		if (cam == null) {
			cam = Camera.main;
		}
        camMoveSpeed = 20;
		Vector3 upperCorner = new Vector3 (Screen.width, Screen.height, 0.0f);
		Vector3 WidthAndHeight = cam.ScreenToWorldPoint (upperCorner);
		width = WidthAndHeight.x;
		height = WidthAndHeight.y;
        targetOrtho = Camera.main.orthographicSize;
    }

	
	// Update is called once per frame
	void Update () {
		// TODO: ineffective, maybe later we need to separate the control and execution of movement
		int numOfKeysPressed = 0;
		if(Input.GetKey(KeyCode.LeftArrow)) {
			directions[numOfKeysPressed] = Direction.Left;
			numOfKeysPressed++;
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			directions[numOfKeysPressed] = Direction.Right;
			numOfKeysPressed++;
		}
		if(Input.GetKey(KeyCode.UpArrow)) {
			directions[numOfKeysPressed] = Direction.Up;
			numOfKeysPressed++;
		}
		if(Input.GetKey(KeyCode.DownArrow)) {
			directions[numOfKeysPressed] = Direction.Down;
			numOfKeysPressed++;
		}

		Vector3 movement = new Vector3 ();
		for (int i = 0; i < numOfKeysPressed; i++) {
			if (directions [i] == Direction.Left) {
				movement.x -= Time.deltaTime * camMoveSpeed;
			} else if (directions [i] == Direction.Right) {
				movement.x += Time.deltaTime * camMoveSpeed;
			} else if (directions [i] == Direction.Up) {
				movement.y += Time.deltaTime * camMoveSpeed;
			} else if (directions [i] == Direction.Down) {
				movement.y -= Time.deltaTime * camMoveSpeed;
			}
		}

		//Out of bound check
		// TODO: shoudl check the background size
		if (numOfKeysPressed > 0) {
			movement = new Vector3 (transform.position.x + movement.x,
				transform.position.y + movement.y, transform.position.z);
			movement.x = Mathf.Clamp (movement.x, -width, width);
			movement.y = Mathf.Clamp (movement.y, -height, height);
			transform.position = movement;
		}

        // zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            targetOrtho -= scroll * zoomSpeed;
            targetOrtho = Mathf.Clamp(targetOrtho, minOrtho, maxOrtho);
            Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetOrtho, smoothSpeed * Time.deltaTime);
        }

        
    }	
}
