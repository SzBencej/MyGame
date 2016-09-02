using System;
using System.Collections.Generic;

public abstract class Building {

	protected int Id = 0; // TODO: virtual abstract property
	protected Resource buildingCost;
	protected Resource buildingIncome;
    protected List<String> actions;

	public Building () {
        actions = new List<string>();
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

}

