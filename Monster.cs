using System;
namespace RpsQuest
{
    public class Monster : Figure
    {
        private int powerAttack = Level * 10;
        private static Random r = new Random();

        public Monster()
        {

            RockDamage = GenerateRandomAttack(); 
            PaperDamage = GenerateRandomAttack();
            ScissorsDamage = GenerateRandomAttack();
            Hitpoints = GenerateRandomAttack();
        }

        public override AttackType Attack()
        {
            Random r = new Random();
            int index = r.Next(Enum.GetNames<AttackType>().Length);
            return (AttackType)index;
        }

        private int GenerateRandomAttack()
        {
            return r.Next(powerAttack - 8, powerAttack);
        }
    }
}

