using UnityEngine;
using System.Collections;

public class BoardManager : MonoBehaviour {

	public GameObject board;
	public GameObject rock;
	public int numberOfRocks;

	private float boardHeight,boardWidth;

	void Awake() {
		boardWidth = board.GetComponent<Renderer> ().bounds.size.x;
		boardHeight = board.GetComponent<Renderer> ().bounds.size.y;
	}

	void Start () {
		Instantiate (board); PlaceRandomRocks ();
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
}
