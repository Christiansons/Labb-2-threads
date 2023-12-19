namespace Labb_2_trådar
{
	
	internal class Program
	{
		//Lock-objekt som bara kan låsa in en vinnare
		static object lockObject = new object();
		static void Main(string[] args)
		{
			Console.WriteLine("Racet startar! (Tryck enter för att kolla läget på bilarna)");
			
			//Skapar två bilar och trådar, samt lägger in metoden StartRace som de ska köra
			Car car1 = new Car("Toyota corolla -97 aka 'pärlan'");
			Thread thread1 = new Thread(() => StartRace(car1));
			
			Car car2 = new Car("McLaren 720s");
			Thread thread2 = new Thread(() => StartRace(car2));

			//Startar båda trådarna
			thread1.Start();
			thread2.Start();
			
			List<Car> carList = new List<Car>() { car2, car1 };

			//Loop to check the status for the cars in the race, while the race is still going
			while (carList.Any(c => c.distanceTraveled < 10))
			{
				if (Console.ReadLine() == "")
				{
					Console.WriteLine();
					foreach (Car car in carList)
					{
						Console.WriteLine($"{car.Name}s hastighet är: {car.Speed} och den har åkt: " + String.Format("{0:0.00}",car.distanceTraveled) + " km hittills!");
					}
					Console.WriteLine();
				}
			}

			//Väntar in båda trådarna
			thread1.Join();
			thread2.Join();
		}

		//Metod för att starta racet
		public static void StartRace(Car car)
		{
			int counter = 0;
			while (true)
			{
				
				//Formula for calculating distance per second and sleeping the thread for 1 second
				car.distanceTraveled += car.Speed / 3600;
				Thread.Sleep(1000);

				//Räknare som går upp med 1 varje sekund, och if-statement som håller koll på intervall om 30s, och sedan testar ett random event för bilen
				counter++;
				if (counter % 5 == 0)
				{
					RandomEvent(car);
				}

				//Om någon av bilarna uppnår 10km låser dem in sig i lock--object och skriver ut vinnarmeddelandet
				if (car.distanceTraveled > 3)
				{
					DeclareWinner(car);
					break;
				}
			}
		}
		
		//Tar emot bilen som vinner och låser 
		static void DeclareWinner(Car car)
		{
			lock (lockObject)
			{
				Console.WriteLine($"Vinnaren är {car.Name}!!");
				
			}
		}

		public static void RandomEvent(Car car)
		{
			//Skapar ett random-event som sker 18/50 gånger, sedan delar jag upp de 18 på de olika sannolikheterna med en switch
			Random random = new Random();
			int randoms = random.Next(1, 51);
			if (randoms <= 18)
			{
				switch (randoms)
				{
					case 1:
						OutOfGas(car);
						break;

					case 2 or 3:
						FlatTire(car);
						break;

					case >= 4 and <= 8:
						CleanWindshield(car);
						break;

					case >= 9 and <= 18:
						EngineTrouble(car);
						break;

				}
			}
		}

		//Skriver ut alla saker som kan hända och sleepar tråden/eller tar bort 1km/h
		static void OutOfGas(Car car)
		{
			Console.WriteLine();
			Console.WriteLine($"{car.Name} Stops to fill up gas");
			Thread.Sleep(30000);
			Console.WriteLine();
			Console.WriteLine($"The gas is refilled for {car.Name} and it speeds of!");
		}
		static void FlatTire(Car car)
		{
			Console.WriteLine();
			Console.WriteLine($"{car.Name} got a flat tire and needs to change wheels!");
			Thread.Sleep(20000);
			Console.WriteLine();
			Console.WriteLine($"The tires are changed and {car.Name} speeds of!");
		}
		static void CleanWindshield(Car car)
		{
			Console.WriteLine();
			Console.WriteLine($"{car.Name}s windshield got hit by a bird and needs to be cleaned!");
			Thread.Sleep(10000);
			Console.WriteLine();
			Console.WriteLine($"the windshield is clean and {car.Name} speeds of!");
		}

		static void EngineTrouble(Car car)
		{
			car.Speed -= 1;
			Console.WriteLine();
			Console.WriteLine($"{car.Name} fick motorfel och hastigheten sänktes med 1km/h!");
		}
	}
}

	

