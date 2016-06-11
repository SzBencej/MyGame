using UnityEngine;
using System.Collections;

public class Application : MonoBehaviour {

	public GameObject gameManager;

	// Use this for initialization
	void Awake () {
		if (GameManager.instance == null) {
			Instantiate (gameManager);
		}
	}

}
