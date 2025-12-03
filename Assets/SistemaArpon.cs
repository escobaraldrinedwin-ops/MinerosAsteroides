using UnityEngine;
using TMPro;

public class SistemaArpon : MonoBehaviour
{
    public Camera camaraPrincipal;
    public TextMeshProUGUI textoPuntaje;
    
    // --- AUDIO ---
    public AudioSource fuenteAudio; // Usamos el mismo AudioSource de la nave
    public AudioClip sonidoMinar;   // Sonido de éxito
    public AudioClip sonidoGanar;   // Sonido final (opcional)

    private DistanceJoint2D joint;
    private LineRenderer cuerda;
    private bool enganchado = false;
    private float tiempoEnganchado = 0f;
    private int cristales = 0;
    public bool juegoTerminado = false;

    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        cuerda = GetComponent<LineRenderer>();
        // Si no asignaste el audio manual, lo buscamos automático
        if(fuenteAudio == null) fuenteAudio = GetComponent<AudioSource>();
        
        joint.enabled = false;
        cuerda.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (enganchado) Soltar();
            else IntentarEnganchar();
        }

        if (enganchado)
        {
            cuerda.SetPosition(0, transform.position);
            if(joint.connectedBody == null) { Soltar(); return; }
            cuerda.SetPosition(1, joint.connectedBody.transform.position);

            if (joint.connectedBody.gameObject.CompareTag("Mineral"))
            {
                tiempoEnganchado += Time.deltaTime;
                if (tiempoEnganchado >= 2.0f) MinarAsteroide(joint.connectedBody.gameObject);
            }
        }
    }

    void IntentarEnganchar()
    {
        Vector2 mousePos = camaraPrincipal.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D golpe = Physics2D.Raycast(mousePos, Vector2.zero);

        if (golpe.collider != null && golpe.collider.GetComponent<Rigidbody2D>() != null)
        {
            joint.connectedBody = golpe.collider.GetComponent<Rigidbody2D>();
            joint.distance = Vector2.Distance(transform.position, golpe.point);
            joint.enabled = true;
            cuerda.enabled = true;
            enganchado = true;
            tiempoEnganchado = 0f;
        }
    }

    void Soltar()
    {
        joint.enabled = false;
        cuerda.enabled = false;
        enganchado = false;
        joint.connectedBody = null;
        tiempoEnganchado = 0f;
    }

    void MinarAsteroide(GameObject asteroide)
    {
        // --- SONIDO AQUÍ ---
        fuenteAudio.PlayOneShot(sonidoMinar);
        // ------------------
        
        Destroy(asteroide);
        Soltar();
        cristales++;
        textoPuntaje.text = "Cristales: " + cristales + "/3";
    }

    private void OnCollisionEnter2D(Collision2D colision)
    {
        if (colision.gameObject.CompareTag("Base"))
        {
            if (cristales >= 3 && juegoTerminado == false)
            {
                juegoTerminado = true;
                textoPuntaje.text = "¡MISIÓN CUMPLIDA!";
                textoPuntaje.color = Color.green;
                
                // Sonido final si quieres
                if(sonidoGanar != null) fuenteAudio.PlayOneShot(sonidoGanar);

                GameObject generador = GameObject.Find("GeneradorTormenta");
                if (generador != null) Destroy(generador);
                
                Debug.Log("¡GANASTE!");
            }
        }
    }
}