using Gestion_Championnat.Modeles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Championnat.Logique
{
    public class Strategie_Generation_Arbitrage
    {
        public IList<Equipe> equipes { get; private set;}
        public IList<Match> matches { get; private set; }
        public Strategie_Generation_Arbitrage(IList<Equipe> _equipes, IList<Match> _matches)
        {
            this.equipes = _equipes;
            this.matches = _matches;
        }

        public IList<Match> Generer_Arbitrage(IList<Match> matches)
        {
            for (int i = 0; i < matches.Count; i++)
            {
                Equipe arbitre1 = Renvoyer_Arbitre1(matches, i);
                Equipe arbitre2 = Renvoyer_Arbitre2(matches, i, arbitre1);
                matches[i].arbitre1 = arbitre1;
                matches[i].arbitre2 = arbitre2;
            }
            return matches;
        }

        public Equipe Renvoyer_Arbitre1(IList<Match> matches, int indiceMatch)
        {
            Equipe equipe = null; //Erreur de conception, il faudrait un null object pattern ou autre
            foreach (Equipe eq in this.equipes)
            {
                if (!Equipe_Joue_Dans_Match(eq, matches[indiceMatch]) && !Equipe_Joue_Match_Suivant(eq, matches, indiceMatch))
                {
                    equipe = eq;
                    break;
                }
            }
            return equipe;
        }

        public Equipe Renvoyer_Arbitre2(IList<Match> matches, int indiceMatch, Equipe equipe1)
        {
            Equipe equipe = null; //Erreur de conception, il faudrait un null object pattern ou autre
            foreach (Equipe eq in this.equipes)
            {
                if (!equipe1.Equals(eq) && !Equipe_Joue_Dans_Match(eq, matches[indiceMatch]) && !Equipe_Joue_Match_Suivant(eq, matches, indiceMatch))
                {
                    equipe = eq;
                    break;
                }
            }
            return equipe;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="equipe"></param>
        /// <param name="matches"></param>
        /// <param name="indiceMatch"></param>
        /// <returns></returns>
        /// <remarks>
        /// Une équipe ne peut pas jouer après avoir arbitré.
        /// </remarks>
        public bool Equipe_Joue_Match_Suivant(Equipe equipe, IList<Match> matches, int indiceMatch)
        {
            bool equipeJoueMatchSuivant = false;


            if (indiceMatch < matches.Count - 1)
            {
                Match matchSuivant = matches[indiceMatch + 1];
                equipeJoueMatchSuivant = (matchSuivant.equipe1.Equals(equipe) || matchSuivant.equipe2.Equals(equipe));
            }

            return equipeJoueMatchSuivant;
        }

        public bool Equipe_Joue_Dans_Match(Equipe equipe, Match match)
        {
            return (match.equipe1.Equals(equipe) || match.equipe2.Equals(equipe));
        }
    }
}
