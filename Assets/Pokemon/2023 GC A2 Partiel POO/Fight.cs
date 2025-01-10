using UnityEngine;
using System;
using System.Diagnostics;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Fight
    {
        public Fight(Character character1, Character character2)
        {
            if (character1 == null || character2 == null) throw new ArgumentNullException("One of the characters is not implemented.");
            Character1 = character1;
            Character2 = character2;
            IsFightFinished = false;
        }

        public Character Character1 { get; }
        public Character Character2 { get; }
        /// <summary>
        /// Est-ce que la condition de victoire/défaite a été rencontré ?
        /// </summary>
        public bool IsFightFinished { get; private set; }

        /// <summary>
        /// Jouer l'enchainement des attaques. Attention à bien gérer l'ordre des attaques par apport à la speed des personnages
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque selectionné par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque selectionné par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {
            float damageDoneToCharacter1 = skillFromCharacter2.Power * TypeResolver.GetFactor(skillFromCharacter2.Type, Character1.BaseType);
            float damageDoneToCharacter2 = skillFromCharacter1.Power * TypeResolver.GetFactor(skillFromCharacter1.Type, Character2.BaseType);
            if (Character1.Speed > Character2.Speed)
            {
                //UnityEngine.Debug.Log("character 1 attacking first");
                Character2.ReceiveAttack(skillFromCharacter1);
                CheckEndOfFight();
                if (IsFightFinished) return;
                Character1.ReceiveAttack(skillFromCharacter2);
            } else if (Character1.Speed < Character2.Speed)
            {
                //UnityEngine.Debug.Log("character 2 attacking first");
                Character1.ReceiveAttack(skillFromCharacter2);
                CheckEndOfFight();
                if (IsFightFinished) return;
                Character2.ReceiveAttack(skillFromCharacter1);
            }
            
        }

        private void CheckEndOfFight()
        {
            IsFightFinished = !(Character1.IsAlive && Character2.IsAlive);
        }

    }
}
