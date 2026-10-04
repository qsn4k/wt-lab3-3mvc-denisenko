using wt_lab3_3mvc_denisenko.Models;

namespace wt_lab3_3mvc_denisenko.Views;

public class PlayerView
{
    public void ShowPlayers(IEnumerable<Player> players, string title = "СПИСОК ИГРОКОВ")
    {
        List<Player> playerList = players.ToList();

        Console.WriteLine();
        Console.WriteLine($"=== {title} ===");

        if (playerList.Count == 0)
        {
            Console.WriteLine("Список пуст.");
            return;
        }

        Console.WriteLine(new string('-', 95));
        Console.WriteLine(
            $"{"ID",-4} {"Никнейм",-18} {"Игра",-16} {"Команда",-18} {"Рейтинг",-8} {"Победы",-8} {"Поражения",-10}");
        Console.WriteLine(new string('-', 95));

        foreach (Player player in playerList)
        {
            Console.WriteLine(
                $"{player.Id,-4} " +
                $"{player.Nickname,-18} " +
                $"{player.Game,-16} " +
                $"{player.Team,-18} " +
                $"{player.Rating,-8} " +
                $"{player.Wins,-8} " +
                $"{player.Losses,-10}");
        }

        Console.WriteLine(new string('-', 95));
    }

    public void ShowPlayer(Player player)
    {
        Console.WriteLine();
        Console.WriteLine("=== ИНФОРМАЦИЯ ОБ ИГРОКЕ ===");
        Console.WriteLine($"ID: {player.Id}");
        Console.WriteLine($"Никнейм: {player.Nickname}");
        Console.WriteLine($"Игра: {player.Game}");
        Console.WriteLine($"Команда: {player.Team}");
        Console.WriteLine($"Рейтинг: {player.Rating}");
        Console.WriteLine($"Победы: {player.Wins}");
        Console.WriteLine($"Поражения: {player.Losses}");
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowError(string message)
    {
        Console.WriteLine($"Ошибка: {message}");
    }

    public void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("         ИГРОВОЙ СЕРВИС / ТУРНИРЫ");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Показать всех игроков");
        Console.WriteLine("2. Найти игрока по ID");
        Console.WriteLine("3. Найти игрока по никнейму");
        Console.WriteLine("4. Добавить игрока");
        Console.WriteLine("5. Удалить игрока");
        Console.WriteLine("6. Найти игроков по игре");
        Console.WriteLine("7. Показать игроков с высоким рейтингом");
        Console.WriteLine("8. Зарегистрировать победу игрока");
        Console.WriteLine("9. Зарегистрировать поражение игрока");
        Console.WriteLine("0. Выход");
        Console.WriteLine("========================================");
    }
}
