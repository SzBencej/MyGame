using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private float elapsedTime;
	private int elapsedTimeInt;
	private GameModel gameModel;
	private Text resourceText;
    private GameObject canvas;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else if(instance != this) {
			Destroy (gameObject);
		}
        canvas = null;
		elapsedTime = 0f;
		elapsedTimeInt = 0;

		DontDestroyOnLoad(gameObject);
		gameModel = new GameModel ();
		resourceText = GameObject.Find ("ResourceText").GetComponent<Text>();
	}

    internal void AddCanvas(GameObject canvas)
    {
        if (this.canvas != null)
        {
            throw new Exception("Canvas added twice");
        }
        this.canvas = canvas;
    }

    public GameObject GetCanvas()
    {
        return canvas;
    }

    // Use this for initialization
    void Start () {
		
	}

	void Update() {
		elapsedTime += Time.deltaTime;
		if (elapsedTime > elapsedTimeInt + 1) {
			elapsedTimeInt++;
			gameModel.NextRound ();
		}
		resourceText.text = gameModel.GetResource ().ToString();
	}

	public GameObject GetBuildingByID(int buildingID) {
		GameObject building = Resources.Load ("Prefabs/Building") as GameObject;
		if (buildingID == 1) {
			building.GetComponent<BuildingScript> ().building = new Building1 ();
		} else if (buildingID == 2) {
			building.GetComponent<BuildingScript> ().building = new Building2 ();
		} else if (buildingID == 3) {
			building.GetComponent<BuildingScript> ().building = new Building3 ();
		}
		return building;
	}

	public bool IsAvailable(int buildingID) {
		return gameModel.IsAvailable(buildingID);
	}

	public void AddBuilding(GameObject gameObject) {
		gameModel.AddBuilding(gameObject.GetComponent<BuildingScript> ().building);
	}

    public void RemoveBuilding(GameObject gameObject)
    {
        gameModel.RemoveBuilding(gameObject.GetComponent<BuildingScript>().building);
    }

    public void DecreaseResource(Resource other)
    {
        gameModel.Decrease(other);
    }

	public bool Affordable(Resource other) {
		return gameModel.Affordable(other);
	}
}
