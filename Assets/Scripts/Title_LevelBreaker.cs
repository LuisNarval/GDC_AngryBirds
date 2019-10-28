using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_LevelBreaker : MonoBehaviour{

    [Header("REFERENCIAS")]
    public GameObject[] CANDADOS;

    // Start is called before the first frame update
    void Start(){
        AbrirNiveles();
    }

    public void AbrirNiveles(){
        if (PlayerPrefs.HasKey("NivelUno"))
            CANDADOS[0].SetActive(false);

        if (PlayerPrefs.HasKey("NivelDos"))
            CANDADOS[1].SetActive(false);

        if (PlayerPrefs.HasKey("NivelTres"))
            CANDADOS[2].SetActive(false);

        if (PlayerPrefs.HasKey("NivelCuatro"))
            CANDADOS[3].SetActive(false);

        if (PlayerPrefs.HasKey("NivelCinco"))
            CANDADOS[4].SetActive(false);

        if (PlayerPrefs.HasKey("NivelSeis"))
            CANDADOS[5].SetActive(false);

        if (PlayerPrefs.HasKey("NivelSiete"))
            CANDADOS[6].SetActive(false);

        if (PlayerPrefs.HasKey("NivelOcho"))
            CANDADOS[7].SetActive(false);

        if (PlayerPrefs.HasKey("NivelNueve"))
            CANDADOS[8].SetActive(false);

        if (PlayerPrefs.HasKey("NivelDiez"))
            CANDADOS[9].SetActive(false);
    }

}