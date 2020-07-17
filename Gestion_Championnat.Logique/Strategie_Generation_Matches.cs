using Gestion_Championnat.Modeles;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Gestion_Championnat.Logique
{
    public class Strategie_Generation_Matches
    {
        public IList<Equipe> equipes { get; private set; }

        public Strategie_Generation_Matches(IList<Equipe> _equipes)
        {
            if (_equipes == null)
            {
                throw new ArgumentException("La liste d'équipes ne peut pas être nulle");
            }

            if (_equipes.Count < 4)
            {
                throw new ArgumentException("Il doit y avoir un nombre suffisnat d'équipes.");
            }

            // On vérifie maintnant que la liste d'équipes fournies ne contient pas de doublons
            IList<Equipe> listeDouble = new List<Equipe>();
            foreach (Equipe eq in _equipes)
            {
                listeDouble.Add(new Equipe(eq.nom));
            }

            foreach (Equipe eq in _equipes)
            {
                int nbOccurences = 0;
                foreach (Equipe eq2 in listeDouble)
                {
                    if (eq.Equals(eq2))
                    {
                        nbOccurences++;
                    }
                }
                if (nbOccurences > 1)
                {
                    throw new ArgumentException("La liste d'équipes fournie ne doit pas comporter de doublons.");
                }
            }

            this.equipes = _equipes;
        }

        public IList<Match> Generer_Matches()
        {
            // On génère les premiers matches
            IList<Match> matches = Generer_Premiers_Matches();

            // On génère les deuxièmes matches
            matches = Generer_Deuxiemes_Matches(matches);

            // On génère l'arbitrage

            return matches;
        }

        public IList<Match> Generer_Premiers_Matches()
        {
            int cptMatch = 1;
            IList<Match> premiersMatches = new List<Match>();

            for (int i = 0; i < this.equipes.Count; i += 2)
            {
                if (i == this.equipes.Count - 1)
                {
                    premiersMatches.Add(new Match(cptMatch, this.equipes[i], this.equipes[0]));
                }
                else
                {
                    premiersMatches.Add(new Match(cptMatch, this.equipes[i], this.equipes[i + 1]));
                }
                cptMatch++;
            }

            return premiersMatches;
        }

        public IList<Match> Generer_Deuxiemes_Matches(IList<Match> _matches)
        {
            int cptMatch = _matches[_matches.Count - 1].id + 1;
            int indiceDepart = 0;
            if (this.equipes.Count % 2 != 0)
            {
                indiceDepart = 1;
            }
            int nbMatchTotal = this.equipes.Count;
            int nbMatchEnCours = _matches.Count;
            for (int i = indiceDepart; i < this.equipes.Count; i++)
            {
                if (!Equipe_Deux_A_Joue_Deux_Fois(_matches, this.equipes[i]))
                {
                    int j = i + 2;
                    if (j > this.equipes.Count-1)
                    {
                        j = 0;
                    }
                    Match matchTemp = new Match(cptMatch, this.equipes[i], this.equipes[j]);
                    bool matchEnDouble = Match_En_Double(_matches, matchTemp);
                    bool equipe2AJoueDeuxFois = Equipe_Deux_A_Joue_Deux_Fois(_matches, matchTemp.equipe2);
                    while (matchEnDouble || equipe2AJoueDeuxFois)
                    {
                        if (this.equipes[i].Equals(this.equipes[j]) == false)
                        {
                            matchTemp = new Match(cptMatch, this.equipes[i], this.equipes[j]);
                            matchEnDouble = Match_En_Double(_matches, matchTemp);
                            equipe2AJoueDeuxFois = Equipe_Deux_A_Joue_Deux_Fois(_matches, matchTemp.equipe2);
                        }
                        
                        j++;
                    }

                    if (!matchEnDouble)
                    {
                        _matches.Add(matchTemp);
                        nbMatchEnCours++;
                    }

                    cptMatch++;
                }
                if (nbMatchEnCours >= nbMatchTotal)
                {
                    break;
                }
            }

            return _matches;
        }

        public bool Match_En_Double(IList<Match> matches, Match match)
        {
            bool matchEnDouble = false;
            int indiceMatch = 0;
            while (indiceMatch < matches.Count && !matchEnDouble)
            {
                Match matchTemoin = matches[indiceMatch];
                matchEnDouble = matchTemoin.Equals(match);

                indiceMatch++;
            }

            return matchEnDouble;
        }

        public bool Equipe_Deux_A_Joue_Deux_Fois(IList<Match> matches, Equipe equipe2)
        {
            int nbMatchEquipe = 0;

            foreach (Match m in matches)
            {
                if (m.equipe1.Equals(equipe2) || m.equipe2.Equals(equipe2))
                {
                    nbMatchEquipe++;
                }

            }

            return nbMatchEquipe >= 2;
        }
    }
}
