#region "Librairies"
using System;
#endregion

namespace Gestion_Championnat.Modeles
{
    /// <summary>
    /// Classe représentant une équipe.
    /// Implémente l'interface <see cref="IEquatable{T}"/>
    /// </summary>
    public class Equipe:IEquatable<Equipe>
    {
        /// <summary>
        /// On peut obtenir le nom de l'équipe à n'importe quel moment mais pas le changer en cours de route
        /// </summary>
        public string nom { get; private set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="_nom">Nom de l'équipe</param>
        public Equipe(string _nom) 
        {
            this.nom = _nom;
        }

        /// <summary>
        /// On redéfinit la méthode de contrôle d'égalité sur les équipes
        /// </summary>
        /// <param name="other">Autre équipe à comparer <see cref="Equipe"/></param>
        /// <returns></returns>
        public bool Equals(Equipe other)
        {
            return string.Compare(this.nom, other.nom, StringComparison.InvariantCultureIgnoreCase) == 0;
        }


    }
}
