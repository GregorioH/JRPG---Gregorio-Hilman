using UnityEngine;
using UnityEngine.SceneManagement;

public class Encuentros : MonoBehaviour
{
    private bool calcularEncuentro = false;
    private bool calcularSoloUnEncuentro = true;

    [SerializeField]
    private MovimientoJugador movJug;
    [SerializeField]
    private Transform PosicionJugador;
    [SerializeField]
    private Transform PosicionHierba;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (calcularSoloUnEncuentro && calcularEncuentro == true)
        {
            if (Mathf.Approximately(PosicionJugador.position.x, PosicionHierba.position.x) && Mathf.Approximately(PosicionJugador.position.y, PosicionHierba.position.y))
            {
                CalcularEncuentro();
                calcularSoloUnEncuentro = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!movJug.posicionCargada)
        {
            if (collision.tag == "Player")
            {
                calcularEncuentro = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!movJug.posicionCargada)
        {
            if (collision.tag == "Player")
            {
                calcularSoloUnEncuentro = true;
            }
        }
    }
    private void CalcularEncuentro()
    {
        switch(Random.Range(0, 10))
        {
            case 0: 
                Debug.Log("Hay encuentro");
                movJug.GuardarPosicion();
                SceneManager.LoadScene("Batalla");
                break;
            default:
                Debug.Log("No hay encuentro");
                break;
        }
    }
}
