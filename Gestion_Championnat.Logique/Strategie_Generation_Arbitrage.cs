#region "Librairies"
using Gestion_Championnat.Modeles;
using System.Collections.Generic;
#endregion


namespace Gestion_Championnat.Logique
{
    /// <summary>
    /// Stratégie de génération de l'arbitrage dans les matches.
    /// Utilisation du patron de conception Strategy qui permet de transformer et d'utiliser
    /// un algorithme sous forme d'objet.
    /// </summary>
    public class Strategie_Generation_Arbitrage
    {
        #region "Attributs"
        public IList<Equipe> equipes { get; private set;}
        public IList<Match> matches { get; private set; }
        #endregion

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="_equipes">Liste des équipes <see cref="IList{Equipe}"/></param>
        /// <param name="_matches">Liste des matches <see cref="IList{Match}"/></param>
        public Strategie_Generation_Arbitrage(IList<Equipe> _equipes, IList<Match> _matches)
        {
            this.equipes = _equipes;
            this.matches = _matches;
        }

        /// <summary>
        /// Méthode permettant la génération de l'arbitrage dans la liste des matches passée en paramètre
        /// </summary>
        /// <param name="matches">Liste des matches pour lesquels il faut générer l'arbitrage. <see cref="IList{Match}"/></param>
        /// <returns><see cref="IList{Match}"/></returns>
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

        /// <summary>
        /// Méthode permettant de renvoyer l'équipe arbitre 1 pour un match
        /// </summary>
        /// <param name="matches">Liste des matches <see cref="IList{Match}"/></param>
        /// <param name="indiceMatch">Indice du match dans la liste passée en paramètre.</param>
        /// <returns>Équipe arbitre 1 <see cref="Equipe"/></returns>
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

        /// <summary>
        /// méthode permettant de renvoyer l'équipe arbitre 2 pour un match
        /// </summary>
        /// <param name="matches">Liste des matches <see cref="IList{Match}"/></param>
        /// <param name="indiceMatch">Indice du match dans la liste passée en paramètre</param>
        /// <param name="equipe1">Équipe arbitre 1 du match dont l'indice est passé en paramètre. <see cref="Equipe"/></param>
        /// <returns>Équipe arbitre 2 <see cref="Equipe"/></returns>
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
        /// Méthode qui permet de vérifier si une équipe joue au match suivant
        /// </summary>
        /// <param name="equipe">Équipe à vérifier <see cref="Equipe"/></param>
        /// <param name="matches">Liste des matches <see cref="IList{Match}"/></param>
        /// <param name="indiceMatch">Indice du match courant</param>
        /// <returns>true si l'équipe joue au match suivant, false sinon</returns>
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

        /// <summary>
        /// Méthode permettant de vérifier si l'équipe passée en paramètre joue dans le match passé en paramètre
        /// </summary>
        /// <param name="equipe">Équipe à vérifier <see cref="Equipe"/></param>
        /// <param name="match">Match à vérifier <see cref="Match"/></param>
        /// <returns></returns>
        public bool Equipe_Joue_Dans_Match(Equipe equipe, Match match)
        {
            return (match.equipe1.Equals(equipe) || match.equipe2.Equals(equipe));
        }
    }
}
