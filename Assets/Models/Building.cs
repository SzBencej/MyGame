using Assets.Models;
using System;
using System.Collections.Generic;

public abstract class Building {

	protected int Id = 0; // TODO: virtual abstract property
	protected Resource buildingCost;
	protected Resource buildingIncome;
    protected List<String> actions;
    protected List<Unit> units;
    protected String name;

	public Building () {
        actions = new List<string>();
        units = new List<Unit>();
	}

	public Resource GetCost() {
		return buildingCost;
	}

	public Resource GetIncome() {
		return buildingIncome;
	}

	public int GetBuildingID() {
		return Id;
	}

    public List<String> GetActions()
    {
        return actions;
    }

    public String GetName()
    {
        return name;
    }

    public List<Unit> GetUnits()
    {
        return units;
    }

    virtual public Action GetTroopAction()
    {
        throw new NotImplementedException();
    }
    
    public bool HasTroops()
    {
        return units.Count != 0;
    }

}

