using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CredistController : MonoBehaviour{

    [Header("REFERENCIAS")]
    public Animator anim_Creditos;
    AudioSource BGM;

    void Start(){
        BGM = this.GetComponent<AudioSource>();
        BGM.volume = 0.0f;
        
        StartCoroutine(corrutina_Creditos());
    }

    IEnumerator corrutina_Creditos(){

        yield return new WaitForSeconds(1.0f);

        anim_Creditos.Play("Muestra");
        BGM.Play();

        float tiempo = 0;
        while (tiempo < 1.0f){
            tiempo += Time.deltaTime / 6;
            BGM.volume = tiempo;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(59.0f);
        
        tiempo = 1;
        while (tiempo > 0){
            tiempo -= Time.deltaTime / 6;
            BGM.volume = tiempo;
            yield return new WaitForEndOfFrame();
        }
        BGM.volume = 0.0f;
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Titulo");
    }


    IEnumerator corrutina_Reloj(){

        float segundos = 0.0f;

        while (segundos < 60){
            segundos += Time.deltaTime;
            Debug.Log("R: " + segundos);
            yield return new WaitForEndOfFrame();
        }

    }

}