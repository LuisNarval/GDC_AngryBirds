using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour{

    [Header("CONFIGURACION")]
    public float volumenMaximo;
    public float volumenMinimo;

    [Header("REFERENCIAS")]
    public AudioSource BGM;
    public AudioClip[] Clips;

    private static BGMController instanciaBGM;

    //public static BGMController Instance {
      //  get {
        //    return instanciaBGM;
        //}
   // }

    private void Awake(){
        if ( instanciaBGM != null && instanciaBGM != this){
            Destroy(this.gameObject);
        }else{
            instanciaBGM = this;
            DontDestroyOnLoad(instanciaBGM);
        }
    }


    // Start is called before the first frame update
    void Start(){
        BGM.volume = volumenMaximo;
        ElegirPista();
    }

    void ElegirPista(){
        StopAllCoroutines();
        int aleatorio = (int)Random.Range(0.0f, Clips.Length);
        BGM.clip = Clips[aleatorio];
        BGM.Stop();
        BGM.Play();
        StartCoroutine(corrutina_EsperarFinal());
    }

    IEnumerator corrutina_EsperarFinal(){
        while (BGM.isPlaying){
            yield return new WaitForSeconds(2.0f);
        }
        Debug.Log("La canción ha terminado");
        ElegirPista();
    }

    public void bajarVolumen() {
        StartCoroutine(corrutina_bajarVolumen());
    }

    public void subirVolumen() {
        StartCoroutine(corrutina_subirVolumen());
    }


    IEnumerator corrutina_bajarVolumen(){
        float tiempo = volumenMaximo;
        while (tiempo > volumenMinimo){
            tiempo -= Time.deltaTime / 2.0f;
            BGM.volume = tiempo;
            yield return new WaitForEndOfFrame();
        }
        BGM.volume = volumenMinimo;
    }

    IEnumerator corrutina_subirVolumen(){
        float tiempo = volumenMinimo;
        while (tiempo < volumenMaximo){
            tiempo += Time.deltaTime / 2.0f;
            BGM.volume = tiempo;
            yield return new WaitForEndOfFrame();
        }
        BGM.volume = volumenMaximo;
    }

    public void FinalizarBGM(){
        StartCoroutine(corrutina_FinalizarBGM());
    }

    IEnumerator corrutina_FinalizarBGM(){
        float tiempo = volumenMinimo;
        while (tiempo > 0.0f){
            tiempo -= Time.deltaTime *1.3f;
            BGM.volume = tiempo;
            yield return new WaitForEndOfFrame();
        }
        BGM.volume = 0.0f;
        instanciaBGM = null;
        Destroy(this.gameObject);
    }

}