using System;

public struct Resource
{
	private int gold, iron, water, manPower;
	public int Gold { set {gold = value;} get {return gold;} }
	public int Iron { set {iron = value;} get {return iron;} }
	public int Water { set {water = value;} get {return water;} }
	public int ManPower { set {manPower = value;} get {return manPower;} }

	public Resource(int gold, int iron, int water, int manPower) {
		this.gold = gold;
		this.iron = iron;
		this.water = water;
		this.manPower = manPower;
	}


	// TODO += operatro
	public void Add(Resource other) {
		this.gold += other.Gold;
		this.iron +=  other.Iron;
		this.water +=  other.Water;
		this.manPower +=  other.ManPower;
	}
	// TODO - operator
	public void Decrease(Resource other) {
		this.gold -= other.Gold;
		this.iron -=  other.Iron;
		this.water -=  other.Water;
		this.manPower -=  other.ManPower;
	}

	public override string ToString ()
	{
		return string.Format ("[Resource: Gold={0}, Iron={1}, Water={2}, ManPower={3}]", Gold, Iron, Water, ManPower);
	}
}

