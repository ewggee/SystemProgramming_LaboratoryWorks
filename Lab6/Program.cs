using System;
using System.Threading;

namespace Lab6
{
    class Animal
    {
        public string Name { get; }
        public ThreadPriority Priority { get; set; }
        public int Meters { get; private set; }

        public Animal(string name, ThreadPriority priority)
        {
            Name = name;
            Priority = priority;
            Meters = 0;
        }

        public void Run()
        {
            while (Meters < 100)
            {
                Meters++;
                Console.WriteLine($"{Name} пробежал {Meters} метров.");
                Thread.Sleep(100);
            }
            Console.WriteLine($"{Name} финишировал!");
        }
    }

    class RabbitAndTurtle
    {
        static void Main(string[] args)
        {
            // Создаем объекты животных
            Animal rabbit = new Animal("Кролик", ThreadPriority.Highest);
            Animal turtle = new Animal("Черепаха", ThreadPriority.Lowest);

            // Создаем потоки
            Thread rabbitThread = new Thread(() => rabbit.Run());
            Thread turtleThread = new Thread(() => turtle.Run());

            // Запускаем потоки
            rabbitThread.Start();
            turtleThread.Start();

            // Ждем окончания работы потоков
            rabbitThread.Join();
            turtleThread.Join();

            // ... (остальной код) 
        }

        static void ChangePriorities(Animal rabbit, Animal turtle)
        {
            // ... (остальной код)
            rabbit.Priority = ThreadPriority.Highest;
            turtle.Priority = ThreadPriority.Lowest;
            // ... (остальной код)
        }
    }


}
