using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class Application : MonoBehaviour {

	public GameObject gameManager;
	public GameObject eventSystem;
	public GameObject canvas;

	// Use this for initialization
	void Awake () {
        Assert.IsNotNull(eventSystem); // TODO: dynamic insert
        Assert.IsNotNull(canvas);
		Instantiate (eventSystem);
		Instantiate (canvas); // TODO: make canvas the child of camera
		//canvas.transform.parent = transform;
		if (GameManager.instance == null) {
			Instantiate (gameManager);
		}
	}

}
