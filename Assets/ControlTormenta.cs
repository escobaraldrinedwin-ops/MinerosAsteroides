using UnityEngine;
using System.Collections;

public class ControlTormenta : MonoBehaviour
{
    public GameObject meteoritoPrefab; // Aquí pones tu meteorito rojo
    public float velocidad = 5f;
    public float tiempoEntreOlas = 3f;

    void Start()
    {
        StartCoroutine(GenerarTormenta());
    }

    IEnumerator GenerarTormenta()
    {
        while (true)
        {
            // Espera unos segundos
            yield return new WaitForSeconds(tiempoEntreOlas);

            // Elige una altura aleatoria
            float alturaRandom = Random.Range(-4f, 4f);
            
            // Crea el meteorito fuera de la pantalla (Derecha)
            Vector3 posicion = new Vector3(10, alturaRandom, 0);
            GameObject meteoro = Instantiate(meteoritoPrefab, posicion, Quaternion.identity);

            // Lo empuja hacia la izquierda
            Rigidbody2D rb = meteoro.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.left * velocidad;

            // Lo destruye después de 5 segundos para no llenar la memoria
            Destroy(meteoro, 5f);
        }
    }
}