using Xunit;
using KoAText;
using KoAText.Abilities;
using System.Linq;
using NuGet.Frameworks;


namespace KoAText.Tests

{
    public class DestinyTests
    {
        [Fact]
        public void Sorcery_Starts_With_Expected_Abilities()
        {
            //arrange
            var sorcery = new Sorcery();
            //Act
            var level1Abilities =sorcery.getAbilitiesByLevel(1);
            //Assert
            Assert.Contains(level1Abilities, a => a.name == "Storm Bolt");
            Assert.True(level1Abilities.Count() == 1);
        }
        [Fact]
        public void Sorcery_Returns_FlameMark_At_Level_2()
        {
            //arrange
            var sorcery = new Sorcery();

            //Act
            var level2Abilities = sorcery.getAbilitiesByLevel(2);
            Assert.Contains(level2Abilities, a => a.name == "Flame Mark");
        }
    }
    public class LevelUpTests
    {
     
        [Fact]
        public void Sorcery_Level_Up_Adds_Magic_Bonuses()
        {
            // Arrange
            var sorcery = new Sorcery();
            var player = new Player("TestMage", sorcery);

            int originalMagicAttack = player.Vitals.BaseMagicAttack;
            int originalMagicDefense = player.Vitals.BaseMagicDefense;
            int originalMana = player.Vitals.BaseMana;

            // Act
            player.GainLevelInDestiny(sorcery);

            // Assert (default + Sorcery bonus)
            Assert.Equal(originalMagicAttack + 2 + 10, player.Vitals.BaseMagicAttack);
            Assert.Equal(originalMagicDefense + 2 + 10, player.Vitals.BaseMagicDefense);
            Assert.Equal(originalMana + 10 + 50, player.Vitals.BaseMana);
        }
      
        [Fact]
        public void Player_Gains_New_Destiny_On_First_Splash()
        {
            // Arrange
            var sorcery = new Sorcery();
            var might = new Might();
            var player = new Player("HybridHero", sorcery);

            // Act
            player.GainLevelInDestiny(might); // Splash might for first time

            // Assert
            var mightProgress = player.Destinies.FirstOrDefault(d => d.Destiny.Name == "Might");
            Assert.NotNull(mightProgress);
            Assert.Equal(1, mightProgress.Level);
        }
        [Fact]
        public void Existing_Destiny_Levels_Up()
        {
            // Arrange
            var sorcery = new Sorcery();
            var player = new Player("ArcaneRogue", sorcery);

            // Act
            player.GainLevelInDestiny(sorcery); // Now Sorcery should be level 2
            var progress = player.Destinies.FirstOrDefault(d => d.Destiny.Name == "Sorcery");

            // Assert
            Assert.NotNull(progress);
            Assert.Equal(2, progress.Level);
        }
        [Fact]
        public void Splashing_New_Destiny_Adds_Level1_Abilities()
        {
            var sorcery = new Sorcery();
            var might = new Might();
            var player = new Player("Hero", sorcery);

            player.GainLevelInDestiny(might);

            // Assume Might has no abilities, Sorcery does
            Assert.Contains(player.Abilities, a => a.name == "Storm Bolt"); // started with this
            Assert.True(player.Abilities.Count > 1); // picked up something from Might or another destiny
        }
    }
}