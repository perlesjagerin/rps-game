using System;
namespace RpsQuest
{
    public class Game
    {
        private Monster? monster;
        public Hero? Hero { get; set; }

        public void NewGame()
        {
            Hero = new Hero();
            Figure.Level = 1;

            Console.WriteLine("\nNova hra zapocala!\n");
            Hero.PrintFigureStatus();
            Console.WriteLine();
        }

        public void Fight()
        {
            if (GameManager.ObjectIsNull(false, Hero))
            {
                return;
            }

            monster = new Monster();
            Console.WriteLine();
            monster.PrintFigureStatus();
            Console.WriteLine("\nBoj zapocal!\n");

            while (Hero!.Hitpoints > 0 && monster.Hitpoints > 0)
            {
                AttackType heroAttack = Hero.Attack();
                AttackType monsterAttack = monster.Attack();
                FightResult fightResult = EvaluateAttack(heroAttack, monsterAttack);
                EvaluateHitpoints(fightResult, heroAttack, monsterAttack);
            }

            AfterFightEvalutation();
        }

        public void Healer()
        {
            if (GameManager.ObjectIsNull(false, Hero))
            {
                return;
            }

            int maxHitpoints = Figure.Level * 100;

            if (Hero!.Hitpoints == maxHitpoints)
            {
                Console.WriteLine($"Mas jiz maximalni pocet zivotu: " +
                    $"{Hero.Hitpoints}");
                return;
            }

            int healedHitpoints = maxHitpoints - Hero.Hitpoints;
            Hero.Hitpoints = maxHitpoints;
            Console.WriteLine($"Byl jsi vylecen! Doplnil jsi " +
                $"{healedHitpoints} zivoty a mas tedy {Hero.Hitpoints} zivotu");
        }

        public void LvlupRock()
        {
            if (GameManager.ObjectIsNull(false, Hero))
            {
                return;
            }

            Hero!.LvlupItem(AttackType.Rock);
        }

        public void LvlupPaper()
        {
            if (GameManager.ObjectIsNull(false, Hero))
            {
                return;
            }

            Hero!.LvlupItem(AttackType.Paper);
        }

        public void LvlupScissors()
        {
            if(GameManager.ObjectIsNull(false, Hero))
            {
                return;
            }

            Hero!.LvlupItem(AttackType.Scissors);
        }

        private FightResult EvaluateAttack(AttackType heroAttack,
            AttackType monsterAttack)
        {
            if (heroAttack == AttackType.Rock &&
                monsterAttack == AttackType.Scissors ||
                heroAttack == AttackType.Paper &&
                monsterAttack == AttackType.Rock ||
                heroAttack == AttackType.Scissors &&
                monsterAttack == AttackType.Paper)
            {
                return FightResult.Win;
            }
            else if (heroAttack == monsterAttack)
            {
                return FightResult.Draw;
            }
            else
            {
                return FightResult.Defeat;
            }
        }

        private void EvaluateHitpoints(FightResult fightResult,
            AttackType heroAttack, AttackType monsterAttack)
        {
            if (GameManager.ObjectIsNull(false, Hero, monster))
            {
                return;
            }

            int heroPowerAttack = Hero!.AttackPower(heroAttack);
            int monsterPowerAttack = monster!.AttackPower(monsterAttack);

            if (fightResult == FightResult.Win)
            {
                monster.Hitpoints -= heroPowerAttack;
                Console.WriteLine($"{monsterAttack} - Zranil jsi nestvuru");
                Console.WriteLine($"Nestvure zbyva {monster.Hitpoints} zivotu");
            }
            else if (fightResult == FightResult.Defeat)
            {
                Hero.Hitpoints -= monsterPowerAttack;
                Console.WriteLine($"{monsterAttack} - Byl jsi zranen");
                Console.WriteLine($"Zbyva ti {Hero.Hitpoints} zivotu");
            }
            else
            {
                Console.WriteLine($"{monsterAttack} - Remiza");
            }
        }

        private void EvaluateLevel()
        {
            if (GameManager.ObjectIsNull(false, Hero))
            {
                return;
            }

            while (Hero!.Experience > Hero.NextLevelRequirment)
            {
                Figure.Level += 1;
                Hero.ItemLvlUp += 1;
                Hero.Experience -= Hero.NextLevelRequirment;
                Hero.NextLevelRequirment += 1;
                Console.WriteLine($"Gratuluji! Dosahl jsi " +
                    $"{Figure.Level}. urovne!");
            }

            Console.WriteLine($"Celkem mas uroven {Figure.Level} " +
                $"a {Hero.Experience} zkusenosti");
        }

        private void AfterFightEvalutation()
        {
            if (GameManager.ObjectIsNull(false, Hero, monster))
            {
                return;
            }

            if (Hero!.Hitpoints > 0)
            {
                int experience = (monster!.RockDamage +
                    monster.PaperDamage + monster.ScissorsDamage) / 3;
                Hero.Experience += experience;
                Console.WriteLine($"Nestvura porazena! Ziskavas " +
                    $"{experience} zkusenosti");
                EvaluateLevel();
                Console.WriteLine();

                if (Figure.Level >= 10)
                {
                    Console.WriteLine("Dosahl jsi 10. urovne! Vyhral jsi!");
                }
            }
            else
            {
                Console.WriteLine("Byl jsi porazen! Konec hry");
            }
        }
    }
}

