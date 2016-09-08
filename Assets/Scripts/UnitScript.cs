using UnityEngine;
using System.Collections;
using Assets.Models;
using System;

public class UnitScript : MonoBehaviour {

    public Unit unit;
    public bool selected;
	// Use this for initialization
	void Start () {
	    
	}

    
	
	// Update is called once per frame
	void Update () {
	
	}

    internal void SetSelected(bool v)
    {
        if (v && !selected)
        {
            Color c = gameObject.GetComponent<Renderer>().material.color;
            c.a = 0.7f;
            gameObject.GetComponent<Renderer>().material.color = c;
        } else if(!v && selected)
        {
            Color c = gameObject.GetComponent<Renderer>().material.color;
            c.a = 1f;
            gameObject.GetComponent<Renderer>().material.color = c;
        }
        selected = v;
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                              