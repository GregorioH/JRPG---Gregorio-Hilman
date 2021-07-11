using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    public float posX;
    public float posY;

    public static bool gameOver = false;

    public int vida;
    public int ataque;
    public int defensa;

    public int vidaEnemigo;
    public int ataqueEnemigo;
    public int defensaEnemigo;

    public void Start()
    {
        if (gameOver == true)
        {
            Reiniciar();
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Mundo");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void Reiniciar()
    {
        PlayerPrefs.SetFloat("Posicion X", posX);
        PlayerPrefs.SetFloat("Posicion Y", posY);

        PlayerPrefs.SetInt("Vida", vida);
        PlayerPrefs.SetInt("Ataque", ataque);
        PlayerPrefs.SetInt("Defensa", defensa);
        PlayerPrefs.SetInt("DefensaPreCalculo", PlayerPrefs.GetInt("Defensa"));

        PlayerPrefs.SetInt("VidaE", vidaEnemigo);
        PlayerPrefs.SetInt("AtaqueE", ataqueEnemigo);
        PlayerPrefs.SetInt("DefensaE", defensaEnemigo);

        gameOver = false;
    }
}
