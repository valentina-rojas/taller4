using UnityEngine;

public class CobwebCleaning : MonoBehaviour
{
    public float cantidadClicsParaDesaparecer = 5f;
    private float clicsActuales = 0f;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        CobwebManager.instance.RegistrarTelaraña(this);
    }

    private void OnMouseDown()
    {
        clicsActuales++;
        float alpha = Mathf.Lerp(1f, 0f, clicsActuales / cantidadClicsParaDesaparecer);
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        if (clicsActuales >= cantidadClicsParaDesaparecer)
        {
            CobwebManager.instance.EliminarTelaraña(this);
            Destroy(gameObject);
        }
    }
}
