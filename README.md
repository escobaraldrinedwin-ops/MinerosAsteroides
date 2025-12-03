# Minero de Asteroides 🚀💎

**Examen Final Individual de Desarrollo de Videojuegos**
**Estudiante:** Aldrin Edwin Escobar Bendezú
**Motor:** Unity 6 (2D)

## 📖 Descripción
"Minero de Asteroides" es un juego de supervivencia y recolección basado en físicas espaciales (estilo *Lunar Lander*). El jugador controla a la nave **Nova**, cuya misión es extraer cristales energéticos de un campo de asteroides y regresar a la base antes de ser destruido por la **Tormenta Micrométrica**.

## 🎮 Instrucciones de Juego

### Objetivo
1.  Recolectar **3 Cristales** de los asteroides flotantes.
2.  Aterrizar a salvo en la **Base** (plataforma inferior) una vez recolectados.
3.  Evitar la lluvia de meteoritos (bolas rojas).

### Controles
| Acción | Tecla / Input |
| :--- | :--- |
| **Propulsión (Volar)** | Barra Espaciadora |
| **Rotar Nave** | Teclas A y D (o Flechas) |
| **Lanzar/Soltar Arpón** | Clic Izquierdo del Mouse |

### Mecánica de Minado
Para extraer un cristal, debes **enganchar** un asteroide con el arpón y **mantenerte conectado** durante 2 segundos sin soltarlo.

---

## ⚙️ Detalles Técnicos (Arquitectura)

El proyecto fue desarrollado utilizando el motor de física 2D de Unity. Los sistemas principales son:

* **Físicas de Vuelo:** Se utiliza `Rigidbody2D` con gravedad reducida (0.2) y aplicación de fuerzas relativas (`AddForce`) para simular la inercia en gravedad cero.
* **Sistema de Arpón (`SistemaArpon.cs`):** Implementado con `DistanceJoint2D` y `LineRenderer`. Permite conectar dinámicamente el Rigidbody de la nave con los asteroides. Incluye lógica para detectar el tiempo de conexión y "minar" el objeto.
* **Generador de Tormenta (`ControlTormenta.cs`):** Un sistema de *Object Pooling* simplificado que instancia meteoritos (`Prefabs`) fuera de cámara y los impulsa horizontalmente a través del nivel usando `Velocity`.
* **Gestión de Estado:** El juego detecta colisiones críticas (Meteoritos) para reiniciar la escena (`SceneManager`) y colisiones de victoria (Base) para detener la tormenta y mostrar el mensaje de éxito.

---

## 📥 Cómo Descargar y Jugar

1.  Ve a la sección de **[Releases](../../releases)** de este repositorio (a la derecha).
2.  Descarga el archivo `.zip` de la última versión (`v1.0`).
3.  Descomprime el archivo en tu computadora.
4.  Ejecuta el archivo `MineroAsteroides.exe`.
