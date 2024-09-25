using Football;

public class Ball
{
    public double X { get; private set; } // Позиция мяча по оси X
    public double Y { get; private set; } // Позиция мяча по оси Y

    private double _vx, _vy; // Скорости мяча по осям X и Y
    private Game _game; // Ссылка на игру

    // Конструктор мяча с координатами и ссылкой на игру
    public Ball(double x, double y, Game game)
    {
        _game = game; // Привязка игры
        X = x; // Установка начальной позиции по оси X
        Y = y; // Установка начальной позиции по оси Y
    }

    // Установка скорости мяча
    public void SetSpeed(double vx, double vy)
    {
        _vx = vx; // Установка скорости по оси X
        _vy = vy; // Установка скорости по оси Y
    }

    // Метод для движения мяча
    public void Move()
    {
        double newX = X + _vx; // Новая позиция по оси X
        double newY = Y + _vy; // Новая позиция по оси Y
                               // Проверка, находится ли новая позиция в пределах стадиона
        if (_game.Stadium.IsIn(newX, newY))
        {
            X = newX; // Обновление позиции мяча по оси X
            Y = newY; // Обновление позиции мяча по оси Y
        }
        else
        {
            _vx = 0; // Остановка скорости по оси X
            _vy = 0; // Остановка скорости по оси Y
        }
    }
}
