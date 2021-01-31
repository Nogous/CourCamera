using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchivementList
{
    Suivre_le_joueur,
    D_étranges_balises,
    Un_rail_le_long_de_la_corniche,
    La_victoire_au_sommet_de_la_corniche,
    L_entrée_de_la_caverne,
    La_salle_arrière_de_la_caverne,
    La_montée_de_la_tour,
    Vue_panoramique_du_haut_de_la_tour,
    Attention_au_trou,
    Top_Down_au_fond_du_trou,
    La_rampe_vers_les_étoiles,
    Selfie_devant_les_étoiles,
    Les_quatre_rampes,
    J_ai_trouvé_comment_sauter_dans_le_vide,
}

public class Achivement : MonoBehaviour
{
    public static Achivement instance;

    private void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }
    }

    private bool[] unlock = new bool[14];

    public void UnlockAchivement(AchivementList achivement)
    {
        if (!unlock[achivement.GetHashCode()])
        {
            SpawnAchivement(achivement);
            unlock[achivement.GetHashCode()] = true;
        }
    }

    public void SpawnAchivement(AchivementList achivement)
    {
        string achivementName = "";

        switch (achivement)
        {
            case AchivementList.Suivre_le_joueur:

                achivementName = "Suivre_le_joueur";
                break;
            case AchivementList.D_étranges_balises:
                achivementName = "D'étranges balises";
                break;
            case AchivementList.Un_rail_le_long_de_la_corniche:
                achivementName = "Un rail le long de la corniche";
                break;
            case AchivementList.La_victoire_au_sommet_de_la_corniche:
                achivementName = "La victoire au sommet de la corniche";
                break;
            case AchivementList.L_entrée_de_la_caverne:
                achivementName = "L'entrée de la caverne";
                break;
            case AchivementList.La_salle_arrière_de_la_caverne:
                achivementName = "La salle arrière de la caverne";
                break;
            case AchivementList.La_montée_de_la_tour:
                achivementName = "La montée de la tour";
                break;
            case AchivementList.Vue_panoramique_du_haut_de_la_tour:
                achivementName = "Vue panoramique du haut de la tour";
                break;
            case AchivementList.Attention_au_trou:
                achivementName = "Attention au trou";
                break;
            case AchivementList.Top_Down_au_fond_du_trou:
                achivementName = "Top-Down au fond du trou";
                break;
            case AchivementList.La_rampe_vers_les_étoiles:
                achivementName = "La rampe vers les étoiles";
                break;
            case AchivementList.Selfie_devant_les_étoiles:
                achivementName = "Selfie devant les étoiles";
                break;
            case AchivementList.Les_quatre_rampes:
                achivementName = "Les quatre rampes";
                break;
            case AchivementList.J_ai_trouvé_comment_sauter_dans_le_vide:
                achivementName = "J'ai trouvé comment sauter dans le vide !";
                break;
            default:
                achivementName = "null";
                break;
        }

        // anim a lancer
        Debug.Log(achivementName + " unlock.");
    }
}
