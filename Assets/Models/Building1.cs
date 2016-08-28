using System;

public class Building1  : Building {

	public Building1 () {
		buildingCost = new Resource (5, 2, 2, 1);
		buildingIncome = new Resource (1, 0, 1, 0);
		Id = 1;
	}
		
}
