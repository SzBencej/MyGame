using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Models
{
    public class Peasant : Unit
    {
        public Peasant()
        {
         Id = 1;
        name = "Peasant";
        cost = new Resource(1,1,1,1);
        speed = 10f;
        damage = 1;
        life = 5;
        trainingTime = 5f;
    }

    }
}
