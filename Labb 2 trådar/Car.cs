using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_2_trådar
{
	internal class Car
	{
		public string Name { get; set; }
		public double Speed { get; set; }
		public double distanceTraveled {  get; set; }

		//Sätter grundfarten för en bil och börjar totaldistansen på 0
		public Car(string name)
		{
			Name = name;
			Speed = 120;
			distanceTraveled = 0;
		}
		
		
	}
}
