namespace Football
{
    // Класс, представляющий футбольную игру
    public class Game
    {
        public Team HomeTeam { get; } // Домашняя команда
        public Team AwayTeam { get; } // Гостевая команда
        public Stadium Stadium { get; } // Стадион
        public Ball Ball { get; private set; } // Мяч в игре

        // Конструктор игры с домашней и гостевой командами и стадионом
        public Game(Team homeTeam, Team awayTeam, Stadium stadium)
        {
            HomeTeam = homeTeam; // Установка домашней команды
            homeTeam.Game = this; // Привязка игры к домашней команде
            AwayTeam = awayTeam; // Установка гостевой команды
            awayTeam.Game = this; // Привязка игры к гостевой команде
            Stadium = stadium; // Установка стадиона
        }

        // Метод для начала игры
        public void Start()
        {
            Ball = new Ball(Stadium.Width / 2, Stadium.Height / 2, this); // Создание мяча в центре поля
            HomeTeam.StartGame(Stadium.Width / 2, Stadium.Height); // Начало игры для домашней команды
            AwayTeam.StartGame(Stadium.Width / 2, Stadium.Height); // Начало игры для гостевой команды
        }

        // Метод для получения позиции для гостевой команды
        private (double, double) GetPositionForAwayTeam(double x, double y)
        {
            return (Stadium.Width - x, Stadium.Height - y); // Отражение координат для гостевой команды
        }

        // Получение позиции для заданной команды
        public (double, double) GetPositionForTeam(Team team, double x, double y)
        {
            return team == HomeTeam ? (x, y) : GetPositionForAwayTeam(x, y); // Если команда домашняя, возвращаем её координаты, иначе отражённые
        }

        // Получение позиции мяча для заданной команды
        public (double, double) GetBallPositionForTeam(Team team)
        {
            return GetPositionForTeam(team, Ball.X, Ball.Y); // Получение координат мяча в зависимости от команды
        }

        // Установка скорости мяча для заданной команды
        public void SetBallSpeedForTeam(Team team, double vx, double vy)
        {
            if (team == HomeTeam)
            {
                Ball.SetSpeed(vx, vy); // Установка скорости мяча для домашней команды
            }
            else
            {
                Ball.SetSpeed(-vx, -vy); // Отражение скорости для гостевой команды
            }
        }

        // Метод для движения команд и мяча
        public void Move()
        {
            HomeTeam.Move(); // Движение домашней команды
            AwayTeam.Move(); // Движение гостевой команды
            Ball.Move(); // Движение мяча
        }
    }