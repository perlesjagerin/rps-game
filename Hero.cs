using System;
namespace RpsQuest
{
    public class Hero : Figure
    {
        public int Experience { get; set; }
        public int NextLevelRequirment { get; set; }
        public int ItemLvlUp { get; set; }
        private int experienceToNextLevel => NextLevelRequirment - Experience;

        public Hero()
        {
            Experience = 0;
            NextLevelRequirment = 1;
            ItemLvlUp = 0;
            RockDamage = 5;
            PaperDamage = 5;
            ScissorsDamage = 5;
            Hitpoints = 100;
        }

        public override AttackType Attack()
        {
            return GameManager.GetCommand<AttackType>();
        }

        public void LvlupItem(AttackType item)
        {
            if (ItemLvlUp == 0)
            {
                Console.WriteLine($"Chybi ti {experienceToNextLevel} " +
                    $"zkusenosti do zvyseni sily - {item}");
                return;
            }

            int powerUp = 5 * Level;

            switch (item)
            {
                case AttackType.Rock:
                    RockDamage += powerUp;
                    break;
                case AttackType.Paper:
                    PaperDamage += powerUp;
                    break;
                case AttackType.Scissors:
                    ScissorsDamage += powerUp;
                    break;
                default:
                    throw new NotImplementedException();
            }

            ItemLvlUp -= 1;
            Console.WriteLine($"Uroven zvednuta! {item} +{powerUp}. " +
                $"Muzes zvednout uroven tvých zbrani jeste {ItemLvlUp}x");
        }

        public override void PrintFigureStatus()
        {
            base.PrintFigureStatus();
            Console.WriteLine($"Mas {Experience} zkusenosti a do dalsi " +
                $"urovne ti zbyva {experienceToNextLevel} zkusenosti");
            Console.WriteLine($"Muzes si {ItemLvlUp}x zvysit uroven svych zbrani");
        }
    }
}

