using System;
using System.Collections.Generic;

namespace Fish
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandAddFish = "1";
            const string CommandPullOutFish = "2";
            const string CommandExit = "0";

            Aquarium aquarium = new Aquarium();

            bool isWork = true;

            while (isWork)
            {
                aquarium.ShowFishes();

                Console.WriteLine("\nВыберите команду: ");
                Console.WriteLine($"{CommandAddFish} - Добавить рыбку.");
                Console.WriteLine($"{CommandPullOutFish} - Достать рыбку.");
                Console.WriteLine($"{CommandExit} - Выйти.");

                string command = Console.ReadLine();

                switch (command)
                {
                    case CommandAddFish:
                        aquarium.AddFish();
                        break;

                    case CommandPullOutFish:
                        aquarium.PullOutFish();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда.");
                        break;
                }

                aquarium.IncreaseFishesLife();
                aquarium.RemoveDeadFish();

                Console.WriteLine();
                Console.Clear();
            }
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes;
        private int _maxCountFish = 10;

        public Aquarium()
        {
            _fishes = new List<Fish>();
        }

        public void AddFish()
        {
            if (_fishes.Count < _maxCountFish)
                _fishes.Add(new Fish());
            else
                Console.WriteLine("В аквариуме больше нет места для рыбок.");
        }

        public void PullOutFish()
        {
            if (TryGetFish(out Fish fish))
            {
                _fishes.Remove(fish);
            }
        }

        public void ShowFishes()
        {
            Console.WriteLine($"В аквариуме максимально может быть {_maxCountFish} рыбок.");

            foreach (Fish fish in _fishes)
            {
                Console.WriteLine(fish.ToString());
            }
        }

        public void IncreaseFishesLife()
        {
            foreach (Fish fish in _fishes)
            {
                fish.BecomeOlder();
            }
        }

        public void RemoveDeadFish()
        {
            for (int i = _fishes.Count - 1; i >= 0; i--)
            {
                if (_fishes[i].Age >= _fishes[i].MaxAge)
                    _fishes.RemoveAt(i);
            }
        }

        private bool TryGetFish(out Fish fish)
        {
            int userInputId = UserUtils.ReadInt("Введите номер рыбки, которую хотите удалить.");

            foreach (var tempFish in _fishes)
            {
                if (tempFish.Id == userInputId - 1)
                {
                    fish = tempFish;
                    return true;
                }
            }

            Console.WriteLine("Такой рыбки нет.");
            Console.ReadKey();

            fish = null;

            return false;
        }
    }

    class Fish
    {
        private static int s_idCounter;

        public Fish()
        {
            Id = s_idCounter++;
            MaxAge = 12;
        }

        public int Id { get; private set; }
        public int Age { get; private set; }
        public int MaxAge { get; private set; }

        public void BecomeOlder()
        {
            Age++;
        }

        public override string ToString()
        {
            return $"{Id + 1}) этой рыбке {Age} лет.";
        }
    }

    static class UserUtils
    {
        public static int ReadInt(string prompt)
        {
            int value;

            Console.Write(prompt + ": ");

            while (int.TryParse(Console.ReadLine(), out value) == false)
            {
                Console.WriteLine("Неверный ввод, введите число еще раз");
                Console.Write(prompt + ": ");
            }

            return value;
        }
    }
}
