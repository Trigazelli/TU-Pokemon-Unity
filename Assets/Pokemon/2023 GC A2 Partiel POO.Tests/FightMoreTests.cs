using _2023_GC_A2_Partiel_POO.Level_2;
using NUnit.Framework;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_2
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer des features et les TU sur le reste du projet

        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
        // - un heal ne régénère pas plus que les HP Max
        // - si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
        // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
        // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type
        // - L'utilisation d'objets : Potion, SuperPotion, Vitess+, Attack+ etc.
        // - Gérer les PP (limite du nombre d'utilisation) d'une attaque,
        // si on selectionne une attack qui a 0 PP on inflige 0

        // Choisis ce que tu veux ajouter comme feature et fait en au max.
        // Les nouveaux TU doivent être dans ce fichier.
        // En Modifiant des features il est possible que certaines valeurs
        // des TU précédentes ne matchent plus, tu as le droit de réadapter les valeurs
        // de ces anciens TU pour ta nouvelle situation.
        [Test]
        public void CharacterReceiveSuperEffectiveFireBall()
        {
            var bulbizarre = new Character(100, 50, 30, 20, TYPE.GRASS);
            var fireball = new FireBall();
            var oldHealth = bulbizarre.CurrentHealth;

            bulbizarre.ReceiveAttack(fireball); // hp : 100 => 70
            Assert.That(bulbizarre.CurrentHealth,
                Is.EqualTo(oldHealth - (fireball.Power * TypeResolver.GetFactor(fireball.Type, bulbizarre.BaseType) - bulbizarre.Defense))); // 100 - (60-30)
            Assert.That(bulbizarre.CurrentStatus, Is.EqualTo(null));
            Assert.That(bulbizarre.IsAlive, Is.EqualTo(true));
            Assert.That(bulbizarre.CurrentHealth, Is.EqualTo(70));

            bulbizarre.ReceiveAttack(fireball); // hp : 70 => 40
            Assert.That(bulbizarre.CurrentHealth, Is.EqualTo(40));
            Assert.That(bulbizarre.IsAlive, Is.EqualTo(true));

            bulbizarre.ReceiveAttack(fireball); // hp : 40 => 10
            Assert.That(bulbizarre.CurrentHealth, Is.EqualTo(10));
            Assert.That(bulbizarre.IsAlive, Is.EqualTo(true));

            bulbizarre.ReceiveAttack(fireball); // hp : 10 => 0
            Assert.That(bulbizarre.CurrentHealth, Is.EqualTo(0));
            Assert.That(bulbizarre.IsAlive, Is.EqualTo(false));
            // RIP bulbizarre
        }

        [Test]
        public void CharacterReceiveResistedFireBall()
        {
            var carapuce = new Character(100, 50, 30, 20, TYPE.WATER);
            var fireball = new FireBall();
            var oldHealth = carapuce.CurrentHealth;

            carapuce.ReceiveAttack(fireball); // hp : 100 => 90
            Assert.That(carapuce.CurrentHealth,
                Is.EqualTo(oldHealth - (fireball.Power * TypeResolver.GetFactor(fireball.Type, carapuce.BaseType) - carapuce.Defense))); // 100 - (60-30)
            Assert.That(carapuce.CurrentStatus, Is.EqualTo(null));
            Assert.That(carapuce.IsAlive, Is.EqualTo(true));
            Assert.That(carapuce.CurrentHealth, Is.EqualTo(90));

            carapuce.ReceiveAttack(fireball); // hp : 90 => 80
            Assert.That(carapuce.CurrentHealth, Is.EqualTo(80));
            Assert.That(carapuce.IsAlive, Is.EqualTo(true));
            // https://tenor.com/view/pokemon-squirtle-cute-cool-gif-17959904
        }
    }
}
