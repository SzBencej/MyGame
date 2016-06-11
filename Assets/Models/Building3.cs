using System;

public class Building3  : Building {

	public Building3 () {
		buildingCost = new Resource (2, 2, 2, 2);
		buildingIncome = new Resource (0, 0, 1, 1);
		Id = 3;
	}

}
