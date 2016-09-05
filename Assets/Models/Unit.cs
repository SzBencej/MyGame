using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Models
{
    public abstract class Unit : ICloneable
    {
        protected int Id = 0;
    protected String name;
        protected Resource cost;
        protected float speed;
        protected int damage;
        protected int life;
        protected float trainingTime;
        public Resource GetCost()
        {
            return cost;
        }

        public String GetName()
        {
            return name;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    
}
