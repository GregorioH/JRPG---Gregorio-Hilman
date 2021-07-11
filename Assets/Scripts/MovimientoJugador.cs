using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField]
    private float velocidad = 5;
    [SerializeField]
    public Transform puntoMovimiento;
    [SerializeField]
    private LayerMask capaObstaculos;

    public Text VidaTxt;
    public Text AtaqueTxt;
    public Text DefensaTxt;

    public bool posicionCargada = false;

    private int vida;
    private int ataque;
    private int defensa;

    private int vidaEnemigo;
    private int ataqueEnemigo;
    private int defensaEnemigo;

    private int defensaPreCalculo;

    void Start()
    {
        puntoMovimiento.parent = null;

        CargarPosicion();
    }

    void Update()
    {
        float cantidadMovimiento = velocidad * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, puntoMovimiento.position, cantidadMovimiento);

        if (Vector3.Distance(transform.position, puntoMovimiento.position) <= 0.05f)
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                Movimiento(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0));
                posicionCargada = false;
            }
            else if (Input.GetButtonDown("Vertical"))
            {
                Movimiento(new Vector3(0, Input.GetAxisRaw("Vertical"), 0));
                posicionCargada = false;
            }
        }

        if (Input.GetButton("Menu"))
        {
            GuardarPosicion();
            SceneManager.LoadScene("Menu");
        }
    }

    private void Movimiento(Vector3 direccion)
    {
        Vector3 posicionNueva = puntoMovimiento.position + direccion;
        if (!Physics2D.OverlapCircle(posicionNueva, 0.2f, capaObstaculos))
        {
            puntoMovimiento.position = posicionNueva;
        }
    }

    public void GuardarPosicion()
    {
        PlayerPrefs.SetFloat("Posicion X", transform.position.x);
        PlayerPrefs.SetFloat("Posicion Y", transform.position.y);

        PlayerPrefs.SetInt("Vida", vida);
        PlayerPrefs.SetInt("Ataque", ataque);
        PlayerPrefs.SetInt("Defensa", defensa);
        PlayerPrefs.SetInt("DefensaPreCalculo", defensaPreCalculo);

        PlayerPrefs.SetInt("VidaE", vidaEnemigo);
        PlayerPrefs.SetInt("AtaqueE", ataqueEnemigo);
        PlayerPrefs.SetInt("DefensaE", defensaEnemigo);
    }

    public void CargarPosicion()
    {
        transform.position = new Vector3(PlayerPrefs.GetFloat("Posicion X"), PlayerPrefs.GetFloat("Posicion Y"));
        puntoMovimiento.position = transform.position;
        posicionCargada = true;

        vida = PlayerPrefs.GetInt("Vida");
        ataque = PlayerPrefs.GetInt("Ataque");
        defensa = PlayerPrefs.GetInt("Defensa");
        defensaPreCalculo = PlayerPrefs.GetInt("DefensaPreCalculo");

        vidaEnemigo = PlayerPrefs.GetInt("VidaE");
        ataqueEnemigo = PlayerPrefs.GetInt("AtaqueE");
        defensaEnemigo = PlayerPrefs.GetInt("DefensaE");

        VidaTxt.text = "Vida: " + vida.ToString();
        AtaqueTxt.text = "Ataque: " + ataque.ToString();
        DefensaTxt.text = "Defensa: " + defensa.ToString();
    }
}
