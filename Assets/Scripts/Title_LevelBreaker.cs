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
        if (PlayerPrefs.HasKey("NIVEL_UNO_DISPONIBLE"))
            CANDADOS[0].SetActive(false);

        if (PlayerPrefs.HasKey("NIVEL_DOS_DISPONIBLE"))
            CANDADOS[0].SetActive(false);

        if (PlayerPrefs.HasKey("NIVEL_TRES_DISPONIBLE"))
            CANDADOS[0].SetActive(false);

        if (PlayerPrefs.HasKey("NIVEL_CUATRO_DISPONIBLE"))
            CANDADOS[0].SetActive(false);

        if (PlayerPrefs.HasKey("NIVEL_CINCO_DISPONIBLE"))
            CANDADOS[0].SetActive(false);

        if (PlayerPrefs.HasKey("NIVEL_SEIS_DISPONIBLE"))
            CANDADOS[0].SetActive(false);

        if (PlayerPrefs.HasKey("NIVEL_SIETE_DISPONIBLE"))
            CANDADOS[0].SetActive(false);

        if (PlayerPrefs.HasKey("NIVEL_OCHO_DISPONIBLE"))
            CANDADOS[0].SetActive(false);

        if (PlayerPrefs.HasKey("NIVEL_NUEVE_DISPONIBLE"))
            CANDADOS[0].SetActive(false);

        if (PlayerPrefs.HasKey("NIVEL_DIEZ_DISPONIBLE"))
            CANDADOS[0].SetActive(false);
    }

}