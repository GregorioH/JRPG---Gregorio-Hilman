using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Combate : MonoBehaviour
{
    public CanvasGroup Acciones;

    public CambioEscena CambioEsc;

    public Text VidaTxt;
    public Text AtaqueTxt;
    public Text DefensaTxt;

    public Text VidaEnemigoTxt;
    public Text AtaqueEnemigoTxt;
    public Text DefensaEnemigoTxt;
    public Text Narrador;

    private int vida;
    private int ataque;
    private int defensa;
    private int defensaPreCalculo;

    private int vidaEnemigo;
    private int ataqueEnemigo;
    private int defensaEnemigo;
    void Start()
    {
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

        VidaEnemigoTxt.text = "Vida: " + vidaEnemigo.ToString();
        AtaqueEnemigoTxt.text = "Ataque: " + ataqueEnemigo.ToString();
        DefensaEnemigoTxt.text = "Defensa: " + defensaEnemigo.ToString();

        defensaPreCalculo = defensa;

        Debug.Log("Tu Turno");
        Narrador.text = "Tu Turno";
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {
            StartCoroutine(GameOver());
        }

        if (vidaEnemigo <= 0)
        {
            StartCoroutine(Victoria());
        }
    }

    public void Atacar()
    {
        Acciones.interactable = false;

        Debug.Log("Has atacado, hiciste " + (ataque - defensaEnemigo) + " de daño");
        Narrador.text = "Has atacado, hiciste " + (ataque - defensaEnemigo) + " de daño";

        vidaEnemigo -= ataque - defensaEnemigo;
        VidaEnemigoTxt.text = "Vida: " + vidaEnemigo.ToString();

        StartCoroutine(Turnos());
    }

    public void Defender()
    {
        Acciones.interactable = false;

        Debug.Log("Te defendiste, defensa + 50%");
        Narrador.text = "Te defendiste, defensa + 50%";

        defensa += Mathf.RoundToInt(defensa * 0.50f);
        DefensaTxt.text = "Defensa: " + defensa.ToString();

        StartCoroutine(Turnos());
    }

    public void Huir()
    {
        StartCoroutine(Huida());
    }

    private void TurnoEnemigo()
    {
        switch(Random.Range(0, 5))
        {
            // Observar
            case 0:
                Debug.Log("El enemigo te observa...");
                Narrador.text = "El enemigo te observa...";
                break;
            // Duplicar Ataque
            case 1:
                Debug.Log("El enemigo ha duplicado su ataque");
                Narrador.text = "El enemigo ha duplicado su ataque";
                ataqueEnemigo += ataqueEnemigo;
                AtaqueEnemigoTxt.text = "Ataque: " + ataqueEnemigo.ToString();
                break;
            //Atacar
            default:
                if (defensa >= ataqueEnemigo)
                {
                    Debug.Log("No recibiste daño");
                    Narrador.text = "No recibiste daño";
                }
                else
                {
                    Debug.Log("Recibiste " + (ataqueEnemigo - defensa) + " de daño");
                    Narrador.text = "Recibiste " + (ataqueEnemigo - defensa) + " de daño";
                    vida -= ataqueEnemigo - defensa;
                    VidaTxt.text = "Vida: " + vida.ToString();
                }
                break;
        }
    }

    IEnumerator Turnos()
    {
        yield return new WaitForSeconds(2);

        Debug.Log("Turno Enemigo");
        Narrador.text = "Turno Enemigo";

        yield return new WaitForSeconds(2);

        TurnoEnemigo();

        yield return new WaitForSeconds(2);

        defensa = defensaPreCalculo;
        DefensaTxt.text = "Defensa: " + defensa.ToString();

        Debug.Log("Tu turno");
        Narrador.text = "Tu Turno";

        Acciones.interactable = true;
    }

    IEnumerator GameOver()
    {
        Acciones.interactable = false;

        Narrador.text = "Game Over";
        Debug.Log("Game Over");

        CambioEscena.gameOver = true;

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Menu");
    }

    IEnumerator Huida()
    {
        Debug.Log("Huiste");
        Narrador.text = "Huiste";

        PlayerPrefs.SetInt("Vida", vida);
        PlayerPrefs.SetInt("Ataque", ataque);
        PlayerPrefs.SetInt("Defensa", defensa);
        PlayerPrefs.SetInt("DefensaPreCalculo", defensaPreCalculo);

        PlayerPrefs.SetInt("VidaE", vidaEnemigo);
        PlayerPrefs.SetInt("AtaqueE", ataqueEnemigo);
        PlayerPrefs.SetInt("DefensaE", defensaEnemigo);

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Mundo");
    }

    IEnumerator Victoria()
    {
        Acciones.interactable = false;

        Narrador.text = "Has ganado";
        Debug.Log("Has ganado");

        CambioEscena.gameOver = true;

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Menu");
    }
}
