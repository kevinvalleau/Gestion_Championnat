using Gestion_Championnat.Modeles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Gestion_Championnat.Logique.Tests
{
    [Trait("Generation_Matches", "Unit")]
    public class Strategie_Generation_Matches_Tests
    {
        ITestOutputHelper output;
        public Strategie_Generation_Matches_Tests(ITestOutputHelper _output)
        {
            this.output = _output;
        }

        #region Méthodes utilitaires
        public IDictionary<string, int> Renvoyer_Nb_Matches_Par_Equipe(IList<Match> matches, IList<Equipe> equipes)
        {
            IDictionary<string, int> equipe_match = new Dictionary<string, int>();
            foreach (Equipe eq in equipes)
            {
                int nbMatches = matches.Where(x => x.equipe1.Equals(eq) || x.equipe2.Equals(eq)).Count();
                equipe_match.Add(new KeyValuePair<string, int>(eq.nom, nbMatches));
            }

            return equipe_match;
        }

        public int Renvoyer_Nb_Matches_En_Double(IList<Match> matches)
        {
            int nbMatchesEnDouble = 0;
            IDictionary<string, int> equipe_match_double = new Dictionary<string, int>();
            foreach (Match m in matches)
            {
                int nbMatches = matches.Where(x => (x.equipe1.Equals(m.equipe1) || x.equipe2.Equals(m.equipe1)) && (x.equipe1.Equals(m.equipe2) || x.equipe2.Equals(m.equipe2))).Count();
                if (nbMatches > 1)
                {
                    nbMatchesEnDouble++;
                }
            }
            return nbMatchesEnDouble;
        }


        #endregion

        [Fact]
        public void Generer_Premiers_Matches_NbEquipesImpair_GenerationOk()
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

            IDictionary<string, int> equipe_match = Renvoyer_Nb_Matches_Par_Equipe(matches, equipes);

            int matchesEnDouble = Renvoyer_Nb_Matches_En_Double(matches);

            output.WriteLine($@"Matches en double {matchesEnDouble}");

            int nbEquipeAvecDeuxMatches = equipe_match.Where(x => x.Value > 1).Count();

            // Assert
            // Toutes les équipes doivent avoir joué au moins une fois
            Assert.DoesNotContain(equipe_match, x => x.Value < 1);
            Assert.Equal(1, nbEquipeAvecDeuxMatches);
            Assert.Equal(0, matchesEnDouble);
        }

        [Fact]
        public void Generer_Premiers_Matches_NbEquipesPair_GenerationOk()
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
                new Equipe("Equipe 7"),
                new Equipe("Equipe 8"),
            };

            // Act
            Strategie_Generation_Matches st = new Strategie_Generation_Matches(equipes);
            IList<Match> matches = st.Generer_Premiers_Matches();

            IDictionary<string, int> equipe_match = Renvoyer_Nb_Matches_Par_Equipe(matches, equipes);

            int matchesEnDouble = Renvoyer_Nb_Matches_En_Double(matches);


            int nbEquipeAvecDeuxMatches = equipe_match.Where(x => x.Value > 1).Count();

            // Assert
            // Toutes les équipes doivent avoir joué au moins une fois
            Assert.DoesNotContain(equipe_match, x => x.Value < 1);
            Assert.Equal(0, nbEquipeAvecDeuxMatches);
            Assert.Equal(0, matchesEnDouble);
        }

        [Fact]
        public void Generer_Deuxiemes_Matches_NbEquipesImpair_GenerationOk()
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

            IDictionary<string, int> equipe_match = Renvoyer_Nb_Matches_Par_Equipe(matches, equipes);

            int matchesEnDouble = Renvoyer_Nb_Matches_En_Double(matches);

            output.WriteLine($@"Matches en double {matchesEnDouble}");

            int nbEquipeAvecTroisMatches = equipe_match.Where(x => x.Value > 2).Count();

            // Assert
            // Toutes les équipes doivent avoir joué au moins une fois
            Assert.DoesNotContain(equipe_match, x => x.Value < 2);
            Assert.Equal(0, nbEquipeAvecTroisMatches);
            Assert.Equal(0, matchesEnDouble);
        }

        [Fact]
        public void Generer_Deuxiemes_Matches_NbEquipesPair_GenerationOk() 
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
                new Equipe("Equipe 7"),
                new Equipe("Equipe 8"),
            };

            // Act
            Strategie_Generation_Matches st = new Strategie_Generation_Matches(equipes);
            IList<Match> matches = st.Generer_Premiers_Matches();

            matches = st.Generer_Deuxiemes_Matches(matches);

            IDictionary<string, int> equipe_match = Renvoyer_Nb_Matches_Par_Equipe(matches, equipes);

            int matchesEnDouble = Renvoyer_Nb_Matches_En_Double(matches);

            int nbEquipeAvecTroisMatches = equipe_match.Where(x => x.Value > 2).Count();

            // Assert
            // Toutes les équipes doivent avoir joué au moins une fois
            Assert.DoesNotContain(equipe_match, x => x.Value < 2);
            Assert.Equal(0, nbEquipeAvecTroisMatches);
            Assert.Equal(0, matchesEnDouble);
        }
    }
}
