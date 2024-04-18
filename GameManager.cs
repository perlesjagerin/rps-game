using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace RpsQuest
{
    public class GameManager
    {
        private Game game = new Game();

        public void RunGame()
        {
            Console.WriteLine("Vitej ve hre KAMEN/NUZKY/PAPIR!");
            AnnonceAllowedCommands<GameCommand>();
            GameCommand inputCommand;

            while (true)
            {
                inputCommand = GetCommand<GameCommand>();

                switch (inputCommand)
                {
                    case GameCommand.NewGame:
                        game.NewGame();
                        break;
                    case GameCommand.Fight:
                        game.Fight();
                        break;
                    case GameCommand.Healer:
                        game.Healer();
                        break;
                    case GameCommand.LvlupRock:
                        game.LvlupRock();
                        break;
                    case GameCommand.LvlupPaper:
                        game.LvlupPaper();
                        break;
                    case GameCommand.LvlupScissors:
                        game.LvlupScissors();
                        break;
                    default:
                        throw new NotImplementedException();
                }

                if (ObjectIsNull(true, game.Hero))
                {
                    continue;
                }

                if (Figure.Level >= 10 || game.Hero!.Hitpoints <= 0)
                {
                    Console.WriteLine($"Prejes si hrat hru znovu? Napis " +
                        $"{YesOrNo.yes} nebo {YesOrNo.No}");
                    YesOrNo yesOrNo = GetCommand<YesOrNo>();

                    if (yesOrNo == YesOrNo.yes)
                    {
                        Console.WriteLine($"Zahaj novou hru prikazem: " +
                            $"{GameCommand.NewGame}");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Dekujeme ti za hru!");
                        return;
                    }
                }
            }
        }

        public static void AnnonceAllowedCommands<T>() where T : struct, Enum
        {
            Console.WriteLine();
            Console.WriteLine("Povolene prikazy jsou pouze:");

            foreach (string command in Enum.GetNames<T>())
            {
                Console.WriteLine(command);
            }

            Console.WriteLine();
        }

        public static T GetCommand<T>() where T : struct, Enum
        {
            string? input;
            T type;
            bool invalidInput = true;

            do
            {
                input = Console.ReadLine();

                if (input == null)
                {
                    throw new NullReferenceException();
                }

                string trim = Regex.Replace(input, @"\s+", "");

                if (Enum.TryParse(trim, ignoreCase: true, out type))
                {
                    invalidInput = false;
                }
                else
                {
                    AnnonceAllowedCommands<T>();
                }
            }
            while (invalidInput);

            return type;
        }

        public static bool ObjectIsNull(bool WriteErrorMessage, params object?[] objects)
        {
            foreach (object? obj in objects)
            {
                if (obj == null)
                {
                    if (WriteErrorMessage)
                    {
                        Console.WriteLine($"Prvne musis zahajit hru prikazem: " +
                        $"{GameCommand.NewGame}\n");
                    }

                    return true;
                }
            }

            return false;
        }
    }
}

