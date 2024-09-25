using System;

namespace Football
{
    // Класс, представляющий игрока в футбольной игре
    public class Player
    {
        public string Name { get; } // Имя игрока
        public double X { get; private set;
        public double Y { get; private set; } 
        private double _vx, _vy; //На сколько пунктов передвигается
        public Team? Team { get; set; } = null; // В игре 2 команды, и игрок прикреплен к 1

        private const double MaxSpeed = 5; // Максимальная скорость игрока
        private const double MaxKickSpeed = 25; // Максимальная скорость удара по мячу
        private const double BallKickDistance = 10; // Расстояние, с которого игрок может ударить по мячу

        private Random _random = new Random(); // Генератор случайных значений

        // Конструктор игрока с именем
        public Player(string name)
        {
            Name = name;
        }

        // Конструктор игрока с именем, позицией и командой
        public Player(string name, double x, double y, Team team)
        {
            Name = name;
            X = x;
            Y = y;
            Team = team;
        }

        // Установка новой позиции игрока
        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        // Позиции игрока в поле, зависит от команды
        public (double, double) GetAbsolutePosition()
        {
            return Team!.Game.GetPositionForTeam(Team, X, Y);
        }

        // Получение расстояния до мяча
        public double GetDistanceToBall()
        {
            var ballPosition = Team!.GetBallPosition(); // Бегут к мечу, завсит от команды
            var dx = ballPosition.Item1 - X; // Разность координат X
            var dy = ballPosition.Item2 - Y; // Разность координат Y
            return Math.Sqrt(dx * dx + dy * dy); // Диагональ
        }

        // Движение игрока к мячу
        public void MoveTowardsBall()
        {
            var ballPosition = Team!.GetBallPosition(); // Позиция мяча
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed; // скорости
            _vx = dx / ratio; 
            _vy = dy / ratio; 
        }

        // Движение игрока
        public void Move()
        {
           
            if (Team.GetClosestPlayerToBall() != this)
            {
                _vx = 0; // Остановка по оси X
                _vy = 0; // Остановка по оси Y
            }

            // Если игрок достаточно близко к мячу, выполняем удар
            if (GetDistanceToBall() < BallKickDistance)
            {
                Team.SetBallSpeed(
                    MaxKickSpeed * _random.NextDouble(), // Случайная скорость мяча по оси X
                    MaxKickSpeed * (_random.NextDouble() - 0.5) // Случайная скорость мяча по оси Y
                );
            }

            var newX = X + _vx; // Новая позиция по оси X
            var newY = Y + _vy; // Новая позиция по оси Y
            var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY);

            // Проверка, находится ли новая позиция в пределах стадиона
            if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2))
            {
                X = newX; // Обновление позиции по оси X
                Y = newY; // Обновление позиции по оси Y
            }
            else
            {
                _vx = _vy = 0; // Остановка, если вышел за пределы
            }
        }
    }
}
