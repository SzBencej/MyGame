using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class Application : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Instantiate(Resources.Load("Prefabs/EventSystem") as GameObject);
        GameObject canvas = Instantiate(Resources.Load("Prefabs/Canvas") as GameObject);

        gameObject.AddComponent<BoardManager>();
        gameObject.AddComponent<CameraMoveScript>();
        if (GameManager.instance == null) {
            gameObject.AddComponent<GameManager>();
            GameManager.instance.AddCanvas(canvas);
        }
	}

}
