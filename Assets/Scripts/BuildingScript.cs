using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Assets.Models;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildingScript : MonoBehaviour {

	public Building building;
    private List<Tuple<string, UnityAction>> actions;
    private GameObject flag;
    public bool Placed { set; get; }

	public Resource GetCost() {
		return building.GetCost();
	}

	public Resource GetIncome() {
		return building.GetIncome ();
	}

    void OnMouseOver()
    {
        if (Placed)
        {
            if (Input.GetMouseButtonDown(0)) // Left click, selected
            {

                flag.GetComponent<Renderer>().enabled = true;
                Color c = gameObject.GetComponent<Renderer>().material.color;
                if (c.a == 1.0f) // It is not selected
                {
                    c.a = 0.7f;
                    gameObject.GetComponent<Renderer>().material.color = c;
                }
                else
                {
                    c.a = 1.0f;
                    gameObject.GetComponent<Renderer>().material.color = c;
                }
            }
            else if (Input.GetMouseButtonDown(1)) // Right click, selected
            {
                Color c = gameObject.GetComponent<Renderer>().material.color;
                c.a = 1.0f;
                gameObject.GetComponent<Renderer>().material.color = c;
                SetRightClickPanel(true);
                flag.GetComponent<Renderer>().enabled = true;
            }

        }
    }

    public void AddFlag()
    {
        //if (building.HasTroops())
        {
            GameObject FlagObject = Resources.Load("Prefabs/Flag") as GameObject;
            flag = Instantiate(FlagObject);
            flag.GetComponent<Renderer>().enabled = false;
        }
    }

    private void DestroyBuilding()
    {
        if (GameManager.instance.Affordable(building.GetCost()))
        {
            SetRightClickPanel(false); //Move to builiing
            GameManager.instance.DecreaseResource(building.GetCost());
            GameManager.instance.RemoveBuilding(gameObject); ;
            Destroy(flag);
            Destroy(gameObject);
        }
    }

    internal void SetBuilding(Building buildingClass)
    {
        actions = new List<Tuple<string, UnityAction>>();
        building = buildingClass;
        foreach(string act in building.GetActions())
        {
            switch (act)
            {
            case "Destroy":
                {
                    UnityAction action = delegate ()
                    {
                        DestroyBuilding();
                    };
                    actions.Add(new Tuple<string, UnityAction>(act, action));
                    break;
                 }
                case "Train":
                    {
                        foreach (Unit u in building.GetUnits())
                        {
                            UnityAction action = delegate ()
                            {
                                if (GameManager.instance.Affordable(u.GetCost())) // move to building
                                {
                                    GameObject unit = Resources.Load("Prefabs/Unit") as GameObject;

                                    GameObject unitObject = Instantiate(unit);

                                    unitObject.GetComponent<UnitScript>().unit = u;
                                    unitObject.GetComponent<MoveTroop>().SetTargetPosition(flag.transform.position);
                                    GameManager.instance.AddUnit(unitObject);
                                }
                            };
                            actions.Add(new Tuple<string, UnityAction>(act, action));
                        }
                        break;
                    }
            default:
                    throw new Exception("Not handled case in SetBuilding");
            }
        }
    }

    private void SetRightClickPanel(bool visible)
    {
        GameObject panel = GameManager.instance.GetCanvas().GetComponentInChildren<Transform>().Find("BuildingRightClickPanel").gameObject;
        if (!visible)
        {
           panel.SetActive(false);
            panel.GetComponent<CanvasRenderer>().SetAlpha(0f);
        } else
        {
            panel.SetActive(true);
            panel.GetComponent<CanvasRenderer>().SetAlpha(1f);
            panel.transform.position = Input.mousePosition;
            GameObject button = panel.GetComponentInChildren<Transform>().Find("First").gameObject;
            Text text = button.GetComponentInChildren<Text>();
            text.text = actions[0].First;
            Button buttonObj = button.GetComponent<Button>();
            buttonObj.onClick.AddListener(actions[0].Second);
            if (actions.Count > 1)
            {
                button = panel.GetComponentInChildren<Transform>().Find("Second").gameObject;
                text = button.GetComponentInChildren<Text>();
                text.text = actions[1].First;
                buttonObj = button.GetComponent<Button>();
                buttonObj.onClick.AddListener(actions[1].Second);
            }
        }
    }
}
