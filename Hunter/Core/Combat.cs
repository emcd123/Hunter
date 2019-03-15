using Hunter.Systems;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Core
{
    public class Combat
    {
        public Combat()
        {

        }
        public void Attack(Actor attacker, Actor defender)
        {
            StringBuilder attackMessage = new StringBuilder();
            StringBuilder defenseMessage = new StringBuilder();

            int ATK = ResolveAttack(attacker, defender, attackMessage);

            int DEF = ResolveDefense(defender, ATK, attackMessage, defenseMessage);

            Game.MessageLog.Add(attackMessage.ToString());
            if (!string.IsNullOrWhiteSpace(defenseMessage.ToString()))
            {
                Game.MessageLog.Add(defenseMessage.ToString());
            }

            int ACC = 0;
            if (ACC > 95)
                ATK *= 2;
            int DMG = ATK - DEF;

            ResolveDamage(defender, DMG);
        }

        // The attacker rolls based on his stats to see if he gets any hits
        private static int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMessage)
        {
            int ATK_number = 0;

            attackMessage.AppendFormat("{0} attacks {1} for ", attacker.Name, defender.Name);

            // Roll a standard damage rating for your first. We take the pure damage.
            // WHen weapons are implemented your fists will be rebalanced.
            // TODO: Will be switching to having items and will rebalance then
            DiceExpression attackDice = new DiceExpression().Dice(2, 6);
            DiceResult attackResult = attackDice.Roll();

            // Look at the face value of each single die that was rolled
            foreach (TermResult termResult in attackResult.Results)
            {
                attackMessage.Append(termResult.Value + ", ");
                ATK_number++;
            }
            return ATK_number;
        }

        // The defender rolls based on his stats to see if he blocks any of the hits from the attacker
        private static int ResolveDefense(Actor defender, int ATK_number, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int DEF_number = 0;

            if (ATK_number > 0)
            {
                attackMessage.AppendFormat("Inflicting {0} damage.", ATK_number);
                defenseMessage.AppendFormat("  {0} defends with a value of ", defender.Name);

                // Roll a number of 100-sided dice equal to the Defense value of the defendering actor
                DiceExpression defenseDice = new DiceExpression().Dice(1, 6);
                DiceResult defenseRoll = defenseDice.Roll();

                // Look at the face value of each single die that was rolled
                foreach (TermResult termResult in defenseRoll.Results)
                {
                    defenseMessage.Append(termResult.Value + ", ");
                    DEF_number++;
                }
                defenseMessage.AppendFormat("sustaining {0} damage.", DEF_number);
            }
            else
            {
                attackMessage.Append("and misses completely.");
            }

            return DEF_number;
        }

        // Apply any damage that wasn't blocked to the defender
        private static void ResolveDamage(Actor defender, int damage, bool crit)
        {
            if (damage > 0)
            {                
                defender.Health = defender.Health - damage;

                if(!crit)
                    Game.MessageLog.Add($"  {defender.Name} was hit for {damage} damage");
                else
                    Game.MessageLog.Add($"  {defender.Name} was hit for {damage} CRITICAL damage");

                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
            else
            {
                Game.MessageLog.Add($"  {defender.Name} blocked all damage");
            }
        }

        // Remove the defender from the map and add some messages upon death.
        private static void ResolveDeath(Actor defender)
        {
            if (defender is Player)
            {
                Globals.IsPlayerDead = true;
                Game.MessageLog.Add("YOU DIED");
            }
            else if (defender is Monster)
            {
                Game.DungeonMap.RemoveMonster((Monster)defender);

                Game.MessageLog.Add($"  {defender.Name} died and dropped {defender.Gold} gold");
                if (defender.IsBoss == true)
                {
                    Globals.IsBossDead = true;
                    Game.MessageLog.Add("You have killed the Boss");
                }
            }
        }
    }
}
