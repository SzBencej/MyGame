using System;

public abstract class Building {

	protected int Id = 0; // TODO: virtual abstract property
	protected Resource buildingCost;
	protected Resource buildingIncome;

	public Building () {
			
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
}

