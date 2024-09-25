public class Stadium
{
    public Stadium(int width, int height)
    {
        Width = width; // Установка ширины стадиона
        Height = height; // Установка высоты стадиона
    }

    public int Width { get; } // Ширина стадиона
    public int Height { get; } // Высота стадиона

    // Метод для проверки, находится ли точка в пределах стадиона
    public bool IsIn(double x, double y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height; // Проверка границ стадиона
    }
}
}