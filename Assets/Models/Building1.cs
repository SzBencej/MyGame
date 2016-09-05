using Assets.Models;
using System;
using System.Collections.Generic;

public class Building1  : Building {
    

	public Building1 () : base() {
        
		buildingCost = new Resource (5, 2, 2, 1);
		buildingIncome = new Resource (1, 0, 1, 0);
		Id = 1;
        name = "BuildingName";
        actions.Add("Destroy");
        actions.Add("Train");
        units.Add(new Peasant());
	}

    override public Action GetTroopAction()
    {
        Action action = delegate ()
        {
            Resource cost = new Resource(2, 2, 1, 1);
            if (GameManager.instance.Affordable(cost))
            {
                GameManager.instance.DecreaseResource(cost);
            }
        };
        return action;
    }
    
		
}
