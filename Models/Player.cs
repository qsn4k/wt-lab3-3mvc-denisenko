namespace wt_lab3_3mvc_denisenko.Models;

public class Player
{
    private int _rating;

    public int Id { get; set; }

    public string Nickname { get; set; }

    public string Game { get; set; }

    public string Team { get; set; }

    public int Rating
    {
        get => _rating;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException("Рейтинг не может быть отрицательным.");
            }

            _rating = value;
        }
    }

    public int Wins { get; private set; }

    public int Losses { get; private set; }

    public Player(
        int id,
        string nickname,
        string game,
        string team,
        int rating,
        int wins = 0,
        int losses = 0)
    {
        if (id <= 0)
        {
            throw new ArgumentException("ID должен быть положительным.");
        }

        if (string.IsNullOrWhiteSpace(nickname))
        {
            throw new ArgumentException("Никнейм не может быть пустым.");
        }

        if (string.IsNullOrWhiteSpace(game))
        {
            throw new ArgumentException("Название игры не может быть пустым.");
        }

        if (string.IsNullOrWhiteSpace(team))
        {
            throw new ArgumentException("Название команды не может быть пустым.");
        }

        if (wins < 0 || losses < 0)
        {
            throw new ArgumentException("Количество побед и поражений не может быть отрицательным.");
        }

        Id = id;
        Nickname = nickname.Trim();
        Game = game.Trim();
        Team = team.Trim();
        Rating = rating;
        Wins = wins;
        Losses = losses;
    }

    public void AddWin()
    {
        Wins++;
        UpdateRating(25);
    }

    public void AddLoss()
    {
        Losses++;

        if (Rating >= 10)
        {
            UpdateRating(-10);
        }
    }

    public void UpdateRating(int value)
    {
        int newRating = Rating + value;
        Rating = Math.Max(0, newRating);
    }
}
