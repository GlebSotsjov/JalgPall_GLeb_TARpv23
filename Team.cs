using System;
using System.Collections.Generic;

namespace Football
{
    // Класс, представляющий команду в футбольной игре
    public class Team
    {
        public List<Player> Players { get; } = new List<Player>(); // Список игроков команды
        public string Name { get; private set; } // Имя команды
        public Game Game { get; set; } // Ссылка на игру, в которой участвует команда

        // Конструктор команды с именем
        public Team(string name)
        {
            Name = name;
        }

        // Метод для начала игры и установки случайных позиций игроков
        public void StartGame(int width, int height)
        {
            Random rnd = new Random(); // Генератор случайных чисел
            foreach (var player in Players)
            {
                // Установка случайной позиции для каждого игрока
                player.SetPosition(
                    rnd.NextDouble() * width,
                    rnd.NextDouble() * height
                );
            }
        }

        // Метод для добавления игрока в команду
        public void AddPlayer(Player player)
        {
            // Если у игрока уже есть команда, ничего не делать
            if (player.Team != null) return;
            Players.Add(player); // Добавление игрока в список
            player.Team = this; // Установка команды у игрока
        }

        // Получение позиции мяча для команды
        public (double, double) GetBallPosition()
        {
            return Game.GetBallPositionForTeam(this); // Запрос позиции мяча в игре
        }

        // Установка скорости мяча
        public void SetBallSpeed(double vx, double vy)
        {
            Game.SetBallSpeedForTeam(this, vx, vy); // Передача скорости мяча в игру
        }

        // Получение ближайшего игрока к мячу
        public Player GetClosestPlayerToBall()
        {
            Player closestPlayer = Players[0]; // Начальный ближайший игрок
            double bestDistance = Double.MaxValue; // Начальное максимальное расстояние
            foreach (var player in Players)
            {
                var distance = player.GetDistanceToBall(); // Расстояние до мяча
                // Если расстояние меньше текущего лучшего расстояния, обновляем
                if (distance < bestDistance)
                {
                    closestPlayer = player;
                    bestDistance = distance;
                }
            }

            return closestPlayer; // Возвращаем ближайшего игрока
        }

        // Движение команды
        public void Move()
        {
            // Движение ближайшего игрока к мячу
            GetClosestPlayerToBall().MoveTowardsBall();
            // Движение всех игроков в команде
            Players.ForEach(player => player.Move());
        }
    }
}
