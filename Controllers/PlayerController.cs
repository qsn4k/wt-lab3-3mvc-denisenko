using wt_lab3_3mvc_denisenko.Models;
using wt_lab3_3mvc_denisenko.Views;

namespace wt_lab3_3mvc_denisenko.Controllers;

public class PlayerController
{
    private readonly List<Player> _players;
    private readonly PlayerView _view;

    public PlayerController()
    {
        _players = new List<Player>
        {
            new Player(1, "Shadow", "CS2", "Belarus Wolves", 1840, 34, 12),
            new Player(2, "Frost", "Dota 2", "Minsk Titans", 1715, 28, 16),
            new Player(3, "Nova", "Valorant", "Cyber Eagles", 1920, 41, 9),
            new Player(4, "Raptor", "CS2", "Belarus Wolves", 1630, 22, 19),
            new Player(5, "Pixel", "League of Legends", "Digital Bears", 1490, 18, 21)
        };

        _view = new PlayerView();
    }

    public void Index()
    {
        _view.ShowPlayers(_players);
    }

    public void Show(int id)
    {
        Player? player = _players.FirstOrDefault(p => p.Id == id);

        if (player == null)
        {
            _view.ShowMessage("Игрок не найден.");
            return;
        }

        _view.ShowPlayer(player);
    }

    public void Add(Player player)
    {
        if (_players.Any(p => p.Id == player.Id))
        {
            _view.ShowError($"Игрок с ID {player.Id} уже существует.");
            return;
        }

        _players.Add(player);
        _view.ShowMessage("Игрок успешно добавлен.");
    }

    public void Delete(int id)
    {
        Player? player = _players.FirstOrDefault(p => p.Id == id);

        if (player == null)
        {
            _view.ShowMessage("Игрок не найден.");
            return;
        }

        _players.Remove(player);
        _view.ShowMessage("Игрок успешно удалён.");
    }

    public void Search(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            _view.ShowError("Строка поиска не может быть пустой.");
            return;
        }

        List<Player> result = _players
            .Where(p =>
                p.Nickname.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                p.Game.Contains(text, StringComparison.OrdinalIgnoreCase) ||
                p.Team.Contains(text, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (result.Count == 0)
        {
            _view.ShowMessage("По заданному запросу игроки не найдены.");
            return;
        }

        _view.ShowPlayers(result, $"РЕЗУЛЬТАТЫ ПОИСКА: {text}");
    }

    public void ShowHighRatingPlayers(int minimumRating = 1700)
    {
        List<Player> result = _players
            .Where(p => p.Rating >= minimumRating)
            .OrderByDescending(p => p.Rating)
            .ToList();

        _view.ShowPlayers(result, $"ИГРОКИ С РЕЙТИНГОМ ОТ {minimumRating}");
    }

    public void RegisterWin(int id)
    {
        Player? player = _players.FirstOrDefault(p => p.Id == id);

        if (player == null)
        {
            _view.ShowMessage("Игрок не найден.");
            return;
        }

        player.AddWin();
        _view.ShowMessage(
            $"Победа зарегистрирована. Новый рейтинг игрока {player.Nickname}: {player.Rating}.");
    }

    public void RegisterLoss(int id)
    {
        Player? player = _players.FirstOrDefault(p => p.Id == id);

        if (player == null)
        {
            _view.ShowMessage("Игрок не найден.");
            return;
        }

        player.AddLoss();
        _view.ShowMessage(
            $"Поражение зарегистрировано. Новый рейтинг игрока {player.Nickname}: {player.Rating}.");
    }

    public void Run()
    {
        while (true)
        {
            _view.ShowMenu();
            Console.Write("Выберите действие: ");

            string? command = Console.ReadLine();

            try
            {
                switch (command)
                {
                    case "1":
                        Index();
                        break;

                    case "2":
                        ShowByUserInput();
                        break;

                    case "3":
                        SearchByUserInput();
                        break;

                    case "4":
                        AddByUserInput();
                        break;

                    case "5":
                        DeleteByUserInput();
                        break;

                    case "6":
                        SearchGameByUserInput();
                        break;

                    case "7":
                        ShowHighRatingByUserInput();
                        break;

                    case "8":
                        RegisterWinByUserInput();
                        break;

                    case "9":
                        RegisterLossByUserInput();
                        break;

                    case "0":
                        return;

                    default:
                        _view.ShowError("Неизвестная команда. Выберите пункт из меню.");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                _view.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Нажмите Enter, чтобы продолжить...");
            Console.ReadLine();
        }
    }

    private void ShowByUserInput()
    {
        Console.Write("Введите ID игрока: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _view.ShowError("Некорректный ID. Введите целое число.");
            return;
        }

        Show(id);
    }

    private void SearchByUserInput()
    {
        Console.Write("Введите никнейм, игру или команду: ");
        string? text = Console.ReadLine();

        Search(text ?? string.Empty);
    }

    private void AddByUserInput()
    {
        Console.Write("Введите ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _view.ShowError("Некорректный ID.");
            return;
        }

        Console.Write("Введите никнейм: ");
        string nickname = Console.ReadLine() ?? string.Empty;

        Console.Write("Введите игру: ");
        string game = Console.ReadLine() ?? string.Empty;

        Console.Write("Введите команду: ");
        string team = Console.ReadLine() ?? string.Empty;

        Console.Write("Введите рейтинг: ");
        if (!int.TryParse(Console.ReadLine(), out int rating))
        {
            _view.ShowError("Некорректный рейтинг.");
            return;
        }

        Player player = new Player(id, nickname, game, team, rating);
        Add(player);
    }

    private void DeleteByUserInput()
    {
        Console.Write("Введите ID игрока для удаления: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _view.ShowError("Некорректный ID.");
            return;
        }

        Delete(id);
    }

    private void SearchGameByUserInput()
    {
        Console.Write("Введите название игры: ");
        string game = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(game))
        {
            _view.ShowError("Название игры не может быть пустым.");
            return;
        }

        List<Player> result = _players
            .Where(p => p.Game.Equals(game, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(p => p.Rating)
            .ToList();

        if (result.Count == 0)
        {
            _view.ShowMessage("Игроков для указанной игры не найдено.");
            return;
        }

        _view.ShowPlayers(result, $"ИГРОКИ В ИГРЕ: {game}");
    }

    private void ShowHighRatingByUserInput()
    {
        Console.Write("Минимальный рейтинг (Enter = 1700): ");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            ShowHighRatingPlayers();
            return;
        }

        if (!int.TryParse(input, out int minimumRating) || minimumRating < 0)
        {
            _view.ShowError("Минимальный рейтинг должен быть неотрицательным целым числом.");
            return;
        }

        ShowHighRatingPlayers(minimumRating);
    }

    private void RegisterWinByUserInput()
    {
        Console.Write("Введите ID игрока: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _view.ShowError("Некорректный ID.");
            return;
        }

        RegisterWin(id);
    }

    private void RegisterLossByUserInput()
    {
        Console.Write("Введите ID игрока: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            _view.ShowError("Некорректный ID.");
            return;
        }

        RegisterLoss(id);
    }
}
