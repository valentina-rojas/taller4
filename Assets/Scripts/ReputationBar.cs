using UnityEngine;
using UnityEngine.UI;

public class ReputationBar : MonoBehaviour
{
    public static ReputationBar instance;

    public Slider sliderReputacion;
    public Gradient colorReputacion; // desde malo (rojo) a bueno (verde)
    public Image fillImage; // la imagen dentro del fill area del slider

    private float reputacion = 0.5f; // empieza neutro

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        ActualizarUI();
    }

    public void AplicarDecision(string tipo)
    {
        switch (tipo)
        {
            case "buena":
                reputacion += 0.1f;
                break;
            case "mala":
                reputacion -= 0.2f;
                break;
                case "neutra":
                reputacion -= 0.1f;
                break;
            // "neutra" no cambia
        }

        reputacion = Mathf.Clamp01(reputacion); // entre 0 y 1
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        sliderReputacion.value = reputacion;
        fillImage.color = colorReputacion.Evaluate(reputacion);
    }

    public string ObtenerEstadoReputacion()
    {
        if (reputacion >= 0.75f) return "buena";
        if (reputacion <= 0.25f) return "mala";
        return "neutra";
    }
}
