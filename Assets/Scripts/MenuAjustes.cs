using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuAjustes : MonoBehaviour
{
    [SerializeField] private GameObject botonAjustes;
    [SerializeField] private GameObject botonSalir;
    [SerializeField] private GameObject menuAjustes;

    public void Ajustes()
    {
        Time.timeScale = 0f;
        botonAjustes.SetActive(false);
        menuAjustes.SetActive(true);
    }

    public void Salir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal"); // O Application.Quit();
    }
    
    public Slider slider;
    public float sliderValue;
   

    void Start()
    {
        if (slider == null)
        {
            Debug.LogError("Faltan referencias en el inspector.");
            return;
        }

        sliderValue = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        slider.value = sliderValue;
        AudioListener.volume = sliderValue;
       
    }


    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        PlayerPrefs.SetFloat("volumenAudio", sliderValue);
        AudioListener.volume = sliderValue;
       
    }

   
}