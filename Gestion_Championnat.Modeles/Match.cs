using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Championnat.Modeles
{
    public class Match:IEquatable<Match>
    {
        public int id { get; set; }
        public Equipe equipe1 { get; set; }
        public Equipe equipe2 { get; set; }
        public Equipe arbitre1 { get; set; }
        public Equipe arbitre2 { get; set; }

        public Match(int _id, Equipe _equipe1, Equipe _equipe2)
        {
            // Ce sont les clauses de garde qui assurent que le programme plante si on essaie de créer un match
            // qui ne respecte pas les règles ci-dessous.
            if (_equipe1.Equals(_equipe2))
            {
                throw new ArgumentException("L'équipe 1 et l'équipe 2 doivent être différentes.");
            }


            this.id = _id;
            this.equipe1 = _equipe1;
            this.equipe2 = _equipe2;
    

        }

        public Match(int _id, Equipe _equipe1, Equipe _equipe2, Equipe _arbitre1, Equipe _arbitre2)
        {
            // Ce sont les clauses de garde qui assurent que le programme plante si on essaie de créer un match
            // qui ne respecte pas les règles ci-dessous.
            if (_equipe1.Equals(_equipe2))
            {
                throw new ArgumentException("L'équipe 1 et l'équipe 2 doivent être différentes.");
            }

            if (_arbitre1.Equals(_arbitre2))
            {
                throw new ArgumentException("L'équipe arbitre 1 doit être différente de l'équipe arbitre 2");
            }

            if (_equipe1.Equals(_arbitre1) || _equipe1.Equals(_arbitre2) || _equipe2.Equals(_arbitre1) || _equipe2.Equals(_arbitre2))
            {
                throw new ArgumentException("Une équipe ne peut pas jouer et arbitrer en même temps.");
            }

            this.id = _id;
            this.equipe1 = _equipe1;
            this.equipe2 = _equipe2;
            this.arbitre1 = _arbitre1;
            this.arbitre2 = _arbitre2;

        }

        /// <summary>
        /// Surcharge de la méthode de vérification d'égalité
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Match other)
        {
            return this.equipe1.Equals(other.equipe1) && this.equipe2.Equals(other.equipe2) && this.arbitre1.Equals(other.arbitre1) && this.arbitre2.Equals(other.arbitre2);
        }
    }
}
