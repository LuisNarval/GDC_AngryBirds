using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resortera : MonoBehaviour {

    [Header("CONFIGURACION")]
    public float LongitudLiga;
    public float FuerzaDisparo;
    public Vector3 posReposo;

    [Header("REFERENCIAS A ESCENA")]
    [Header("Aves")]
    public Transform[] Pajaros;
    public int PActual = 0;

    [Header("Posiciones")]
    public Transform pos1;
    public Transform pos2;
    public Transform posCentral;

    [Header("Resortes")]
    public LineRenderer Resorte1;
    public LineRenderer Resorte2;
    public Transform transTira;

    [Header("Managers")]
    public LevelManager code_LevelManager;
    public GuiaDeTiro code_GuiaDeTiro;
    public Rastro code_Rastro;
    public SFXPajaros code_SFXPajaros;
    public Rigidbody2D[] utileria;
    public Rigidbody2D[] cerdos;

    [Header("Consultas")]
    public bool TiroEnProceso = false;
    public bool ResorteraActiva=false;
    public bool PunteroDentro = false;
    public bool ZonaProhibida = false;

    private CameraController code_CameraController;

    // Start is called before the first frame update
    void Start() {
        transTira.position = posCentral.position;
        code_CameraController = GameObject.Find("CameraController").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update() {
        if (!TiroEnProceso){
            actualizarAve();
            actualizarTira(Pajaros[PActual].position);
        }

        actualizarResortes();
    }

    private bool Estiron = false;
    public void actualizarTira(Vector3 nPos){
        Vector3 direccion = nPos - posCentral.position;
        
        if (direccion.magnitude == 0)
            direccion = posReposo - posCentral.position;

        if (ResorteraActiva){
            if (!Estiron && direccion.magnitude > 0.3f){
                this.GetComponent<AudioSource>().Play();
                Estiron = true;
            }else if(Estiron && direccion.magnitude < 0.3f)
                Estiron = false;
        } 

        direccion = Vector3.Normalize(direccion);

        transTira.position = Pajaros[PActual].position + (direccion* Pajaros[PActual].GetComponent<Ave>().radio);
        transTira.LookAt(posCentral.position);
    }


    void actualizarResortes(){
        Resorte1.SetPosition(0, pos1.position);
        Resorte1.SetPosition(1, transTira.position);
        Resorte2.SetPosition(0, pos2.position);
        Resorte2.SetPosition(1, transTira.position);
    }


    void actualizarAve() {

        if (!ZonaProhibida && ResorteraActiva){
            
            if (PunteroDentro)
                actualizarPosicionAve(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            else
                calcularRadio();
            
        }else{
            actualizarPosicionAve(posCentral.position);
        }

        if (ResorteraActiva && !ZonaProhibida){
            code_GuiaDeTiro.CalcularAngulo(Pajaros[PActual].position, posCentral.position);
        }
            
    }


    void actualizarPosicionAve(Vector3 posicionC) {
        Vector3 punto = new Vector3(posicionC.x, posicionC.y, 0.0f);
        Pajaros[PActual].position = punto;
    }

    void calcularRadio() {
        Vector3 punto = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 Direccion = new Vector3(punto.x, punto.y, 0.0f) - posCentral.position;
        Direccion = Vector3.Normalize(Direccion);
        actualizarPosicionAve(posCentral.position + (Direccion * LongitudLiga));
    }



    public void punteroAdentro() {
        PunteroDentro = true;
    }

    public void punteroAfuera() {
        PunteroDentro = false;
    }

    public void zonaProhibidaEntrada(){
        ZonaProhibida = true;
        if(ResorteraActiva)
            code_GuiaDeTiro.GetComponent<LineRenderer>().enabled = false;

    }

    public void zonaProhibidaSalida(){
        ZonaProhibida = false;
        if (ResorteraActiva)
            code_GuiaDeTiro.GetComponent<LineRenderer>().enabled = true;
    }



    public void touchDownEnArea() {
        ResorteraActiva = true;
        code_GuiaDeTiro.GetComponent<LineRenderer>().enabled = true;
    }

    public void touchUpEnArea() {

        ResorteraActiva = false;
        code_GuiaDeTiro.GetComponent<LineRenderer>().enabled = false;

        if (!ZonaProhibida){
            TiroEnProceso = true;
            code_CameraController.DisparoRealizado(Pajaros[PActual]);
            Vector3 direccion = posCentral.position - Pajaros[PActual].position;
            direccion = Vector3.Normalize(direccion);
            Pajaros[PActual].gameObject.GetComponent<Rigidbody2D>().AddForce(direccion*FuerzaDisparo* Pajaros[PActual].GetComponent<Rigidbody2D>().mass,ForceMode2D.Impulse);
            Pajaros[PActual].gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

            Pajaros[PActual].gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            Pajaros[PActual].gameObject.GetComponent<Ave>().lanzado = true;

            code_SFXPajaros.GritoPajaro();

            StartCoroutine(corrutina_RestaurarResortera());
        }
    }


    



    IEnumerator corrutina_RestaurarResortera(){
        float tiempo = 0.0f;
        Vector3 posInicial = Pajaros[PActual].position;
        while (tiempo < 1.0f){
            actualizarTira(new Vector3(Mathf.Lerp(posInicial.x, posCentral.position.x, tiempo),
                                           Mathf.Lerp(posInicial.y, posCentral.position.y, tiempo),
                                           Mathf.Lerp(posInicial.z, posCentral.position.z, tiempo)));

            tiempo += Time.deltaTime * FuerzaDisparo;
            yield return new WaitForEndOfFrame();
        }

        transTira.position = posCentral.position;

        code_Rastro.DejarNuevoRastro(Pajaros[PActual].transform);

        StartCoroutine(corrutina_VerificarAve());

        

    }


    IEnumerator corrutina_VerificarAve(){

        Rigidbody2D cuerpo = Pajaros[PActual].GetComponent<Rigidbody2D>();

        while (cuerpo.velocity.magnitude > 0.1f){
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1.0f);

        if (PActual + 1 < Pajaros.Length){
            PActual++;
            TiroEnProceso = false;
            code_CameraController.EnDisparo = false;
        }else{
            StartCoroutine(corrutina_VerificarEscenario());
        }
    }

    IEnumerator corrutina_VerificarEscenario(){
        bool movimiento = true;

        do{
            movimiento = false;
            for (int i = 0; i < cerdos.Length; i++){
                if (cerdos[i].velocity.magnitude > 0.05f){
                    movimiento = true;
                }
            }
            yield return new WaitForSeconds(0.5f);
        } while (movimiento);

        /*do{
            movimiento = false;
            for (int i = 0; i < utileria.Length; i++){
                if (utileria[i].velocity.magnitude > 0.5f){
                    movimiento = true;
                }
            }

            yield return new WaitForSeconds(0.5f);
        } while (movimiento);*/

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < cerdos.Length; i++){
            cerdos[i].simulated = false;
        }

        

        code_LevelManager.GameOver();

        StopAllCoroutines();
    }

    
    
}