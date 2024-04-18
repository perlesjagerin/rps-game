using System;
using System.Threading;

namespace RpsQuest
{
    public abstract class Figure
    {
        public int RockDamage { get; set; }
        public int PaperDamage { get; set; }
        public int ScissorsDamage { get; set; }
        public int Hitpoints { get; set; }
        public static int Level { get; set; }

        public int AttackPower(AttackType attackType)
        {
            switch (attackType)
            {
                case AttackType.Rock:
                    return RockDamage;
                case AttackType.Paper:
                    return PaperDamage;
                default:
                    return ScissorsDamage;
            }
        }

        public virtual void PrintFigureStatus()
        {
            Console.WriteLine($"{this.GetType().Name}:");
            Console.WriteLine($"{Hitpoints} zivotu");
            Console.WriteLine($"{RockDamage} utok kamenem");
            Console.WriteLine($"{PaperDamage} utok papirem");
            Console.WriteLine($"{ScissorsDamage} utok nuzkami");
        }

        public abstract AttackType Attack();
    }
}

