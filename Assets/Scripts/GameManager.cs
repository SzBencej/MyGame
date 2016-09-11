using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using SimpleAStarExample1;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	private float elapsedTime;
	private int elapsedTimeInt;
	private GameModel gameModel;
	private Text resourceText;
    private GameObject canvas;
    private ArrayList units;
    public BoardManager bm;

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
        units = new ArrayList();
	}

	void Update() {
		elapsedTime += Time.deltaTime;
		if (elapsedTime > elapsedTimeInt + 1) {
			elapsedTimeInt++;
			gameModel.NextRound ();
		}
		resourceText.text = gameModel.GetResource ().ToString();
	}

    internal void MoveSelectedUnits(Vector3 mousePosition)
    {
        foreach(GameObject unit in units)
        {
            if (unit.GetComponent<UnitScript>().selected == true)
            {
                unit.GetComponent<MoveTroop>().SetTargetPosition(mousePosition);
            }
        }
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

    public void AddUnit(GameObject gameObject)
    {
        DecreaseResource(gameObject.GetComponent<UnitScript>().unit.GetCost());
        units.Add(gameObject);
    }

    public IList GetUnits()
    {
        return units;
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

    public List<Vector3> FindPath(GameObject obj, Vector3 position)
    {
        Debug.Log("before start");
        int num = 500;
        int num2 = 10000;
        float blocksizeX = ((bm.boardHeight*num2) / num);
        float blocksizeY = ((bm.boardWidth*num2) / num);
        bool[,] grid = new bool[num, num];
        for(int i = 0; i < num; i++)
        {
            for (int j = 0; j < num; j++)
            {
                grid[i, j] = true;
            }
        }
        // calculate once the grid
        int startX = (int)((obj.transform.position.x * num2) / blocksizeX);
        int startY = (int)((obj.transform.position.y * num2) / blocksizeY);
        Point startPoint = new Point(startX, startY);
        int endX = (int)((position.x * num2) / blocksizeX);
        int endY = (int)((position.y * num2) / blocksizeY);
        Point endPoint = new Point(endX, endY);
        Debug.Log(startPoint.ToString());
        Debug.Log(endPoint.ToString());
        SearchParameters searchParameters = new SearchParameters(startPoint, endPoint, grid);
        PathFinder pathFinder = new PathFinder(searchParameters);
        List<Point> path = pathFinder.FindPath();
        List<Vector3> res = new List<Vector3>();
        foreach(Point p in path)
        {
            Vector3 v = new Vector3((p.X * blocksizeX) /num2, (p.Y * blocksizeY)/num2, 0);
            res.Add(v);
        }
        return res;

    }
}
