using wt_lab3_3mvc_denisenko.Controllers;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("============================================================");
Console.WriteLine("              ЛАБОРАТОРНАЯ РАБОТА WT-3");
Console.WriteLine("       Реализация MVC-архитектуры в консольном приложении");
Console.WriteLine("============================================================");
Console.WriteLine("Предметная область: Игровой сервис / Турниры");
Console.WriteLine("Основная сущность: Player");
Console.WriteLine("Автор: qsn4k");

PlayerController controller = new PlayerController();
controller.Run();

Console.WriteLine();
Console.WriteLine("Программа завершена.");
