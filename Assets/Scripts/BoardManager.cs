using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	private GameObject board;
	private GameObject rock;
	private int numberOfRocks;

	private float boardHeight,boardWidth;

	void Awake() {
        board = (GameObject)Resources.Load("Prefabs/Board", typeof(GameObject));
        rock = (GameObject)Resources.Load("Prefabs/Rock", typeof(GameObject));
        Instantiate(board);
        numberOfRocks = 10;
        boardWidth = board.GetComponent<Renderer> ().bounds.size.x;
		boardHeight = board.GetComponent<Renderer> ().bounds.size.y;
	}

	void Start () {
        PlaceRandomRocks();
        gameObject.AddComponent<UnitSelect>();
	}

    // TODO: array with more rocks or anything
    public void PlaceRandomRocks() {
		// TODO: do not put rocks on each other
		for (int i = 0; i < numberOfRocks; i++) {
			float rockX = Random.Range (-boardWidth/2, boardWidth/2);
			float rockY = Random.Range (-boardHeight/2, boardHeight/2);
			Instantiate (rock, new Vector3 (rockX, rockY, 0.0f), Quaternion.identity);
		}
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("move");
            GameManager.instance.MoveSelectedUnits(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButtonUp(0))
        {
          
        }


    }

}
