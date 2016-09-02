using System;

public class Resource
{
	public int Gold { set; get; }
	public int Iron { set; get; }
    public int Water { set; get; }
    public int ManPower { set; get; }

    public Resource()
    {
        this.Gold = 0;
        this.Iron = 0;
        this.Water = 0;
        this.ManPower = 0;
    }

	public Resource(int gold, int iron, int water, int manPower) {
		this.Gold = gold;
		this.Iron = iron;
		this.Water = water;
		this.ManPower = manPower;
	}


	// TODO += operatro
	public void Add(Resource other) {
		this.Gold += other.Gold;
		this.Iron +=  other.Iron;
		this.Water +=  other.Water;
		this.ManPower +=  other.ManPower;
	}
    public static Resource operator +(Resource x, Resource y)
    {
        return new Resource(x.Gold + y.Gold, x.Iron + y.Iron, x.Water + y.Water, x.ManPower + y.ManPower);
    }

    // TODO assert to not be less than 0
    public static Resource operator -(Resource x, Resource y)
    {
        return new Resource(x.Gold - y.Gold, x.Iron - y.Iron, x.Water - y.Water, x.ManPower - y.ManPower);
    }

    public void Decrease(Resource other) {
		this.Gold -= other.Gold;
		this.Iron -=  other.Iron;
		this.Water -=  other.Water;
		this.ManPower -=  other.ManPower;
	}

    public override bool Equals(object obj)
    {
        Resource item = obj as Resource;

        if (item == null)
        {
            return false;
        }

        return this.Gold == item.Gold && this.Iron == item.Iron && this.Water == item.Water && this.ManPower == item.ManPower;
    }


    public override string ToString ()
	{
		return string.Format ("[Resource: Gold={0}, Iron={1}, Water={2}, ManPower={3}]", Gold, Iron, Water, ManPower);
	}
}

