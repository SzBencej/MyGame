using System;

public class Building2  : Building {



	public Building2 () {
		buildingCost = new Resource (1, 2, 2, 1);
		buildingIncome = new Resource (0, 1, 0, 1);
		Id = 2;
	}

}
