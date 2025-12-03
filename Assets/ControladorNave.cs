using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar el nivel

public class ControladorNave : MonoBehaviour
{
    public float fuerzaImpulso = 20f;
    public float velocidadGiro = 200f;
    
    // Variables de Audio (Opcionales, no darán error si están vacías)
    public AudioSource audioMotor;
    public AudioClip sonidoMotor;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. IMPULSO (Barra Espaciadora)
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * fuerzaImpulso);

            // Sonido del motor (solo si asignaste el audio)
            if (audioMotor != null && !audioMotor.isPlaying)
            {
                audioMotor.PlayOneShot(sonidoMotor);
            }
        }

        // 2. ROTACIÓN (A y D)
        float giro = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, -giro * velocidadGiro * Time.deltaTime);
    }

    // --- DETECTAR MUERTE (GAME OVER) ---
    void OnCollisionEnter2D(Collision2D colision)
    {
        // Verificamos si chocamos con algo que tenga "Meteorito" o "Circle" en el nombre.
        // Esto funciona aunque no hayas creado Tags.
        string nombre = colision.gameObject.name;

        if (nombre.Contains("Meteorito") || nombre.Contains("Circle")) 
        {
            Debug.Log("¡GAME OVER!");
            // Reinicia el nivel actual
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}