using Gestion_Championnat.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Gestion_Championnat.Logique.Tests
{
    [Trait("Generation_Arbitrage", "Unit")]
    public class Strategie_Generation_Arbitrage_Tests
    {
        private ITestOutputHelper output;

        public Strategie_Generation_Arbitrage_Tests(ITestOutputHelper _output)
        {
            output = _output;
        }
        [Fact]
        public void Generer_Arbitrage_NbEquipesImpair_GenerationOk()
        {
            // Arrange
            IList<Equipe> equipes = new List<Equipe>()
            {
                new Equipe("Equipe 1"),
                new Equipe("Equipe 2"),
                new Equipe("Equipe 3"),
                new Equipe("Equipe 4"),
                new Equipe("Equipe 5"),
                new Equipe("Equipe 6"),
                new Equipe("Equipe 7")
            };

            // Act
            Strategie_Generation_Matches st = new Strategie_Generation_Matches(equipes);
            IList<Match> matches = st.Generer_Premiers_Matches();

            matches = st.Generer_Deuxiemes_Matches(matches);


            Strategie_Generation_Arbitrage sa = new Strategie_Generation_Arbitrage(equipes, matches);
            matches = sa.Generer_Arbitrage(matches); // Ça c'est con parce qu'on le passe dans le constructeur

            // Assert
            // Toutes les équipes doivent avoir joué au moins une fois
            Assert.NotEmpty(matches);
        }
    }
}
