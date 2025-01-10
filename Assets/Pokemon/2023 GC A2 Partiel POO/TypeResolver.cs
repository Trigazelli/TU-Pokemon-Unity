
using System;
using System.Collections.Generic;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition des types dans le jeu
    /// </summary>
    public enum TYPE { NORMAL, WATER, FIRE, GRASS }

    public class TypeResolver
    {

        /// <summary>
        /// Récupère le facteur multiplicateur pour la résolution des résistances/faiblesses
        /// WATER faible contre GRASS, resiste contre FIRE
        /// FIRE faible contre WATER, resiste contre GRASS
        /// GRASS faible contre FIRE, resiste contre WATER
        /// </summary>
        /// <param name="attacker">Type de l'attaque (le skill)</param>
        /// <param name="receiver">Type de la cible</param>
        /// <returns>
        /// Normal returns 1 if attacker or receiver
        /// 0.8 if resist
        /// 1.0 if same type
        /// 1.2 if vulnerable
        /// </returns>
        public static float GetFactor(TYPE attacker, TYPE receiver)
        {
            if (attacker == TYPE.NORMAL || receiver == TYPE.NORMAL) return 1; 
            Dictionary<TYPE, Dictionary<TYPE, float>> typeChart = new Dictionary<TYPE, Dictionary<TYPE, float>>();
            List<TYPE> type = new List<TYPE> {TYPE.GRASS, TYPE.WATER};
            Dictionary<TYPE, float> fireChart = new Dictionary<TYPE, float>() { { TYPE.NORMAL, 1 }, {TYPE.GRASS, 0.8f }, { TYPE.WATER, 1.2f }, { TYPE.FIRE, 1 } };
            Dictionary<TYPE, float> waterChart = new Dictionary<TYPE, float>() { { TYPE.NORMAL, 1 }, {TYPE.GRASS, 1.2f }, { TYPE.WATER, 1 }, { TYPE.FIRE, 0.8f } };
            Dictionary<TYPE, float> grassChart = new Dictionary<TYPE, float>() { { TYPE.NORMAL, 1 }, {TYPE.GRASS, 1 }, { TYPE.WATER, 0.8f }, { TYPE.FIRE, 1.2f } };
            typeChart.Add(TYPE.FIRE, fireChart);
            typeChart.Add(TYPE.WATER, waterChart);
            typeChart.Add(TYPE.GRASS, grassChart);
            return typeChart[attacker][receiver];
        }

    }
}
