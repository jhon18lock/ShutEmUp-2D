using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 targetPosition;
    public float lerpDuration = 2f;
    public Vector3 targetRotation;
    //public Transform targetRotation;
    [Header("Color Sprite")]
    public Renderer sprRenderer;
    public Color colorOriginal;
    public Color colorACambiar;

    public AnimationCurve curve;

    //rotacion objeto
    Quaternion startRotation;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(LerpPosition(startPosition, targetPosition, lerpDuration));
            StartCoroutine(LerpRotation(startRotation, Quaternion.Euler( targetRotation), lerpDuration));
            StartCoroutine(LerpColor(colorOriginal, colorACambiar, lerpDuration));
        }
    }

    IEnumerator LerpPosition(Vector3 start, Vector3 target, float lerpDuration)
    {

        float tiempoTranscurrido = 0f;
        

        while(tiempoTranscurrido < lerpDuration)
        {
            transform.position = Vector3.Lerp(start, target, curve.Evaluate( tiempoTranscurrido / lerpDuration));
            
            tiempoTranscurrido += Time.deltaTime;

            yield return null;
        }

        transform.position = target;

        SwitchTarget();
    }

    void SwitchTarget()
    {
        targetPosition = startPosition;
        startPosition = transform.position;

    }

    IEnumerator LerpRotation(Quaternion start, Quaternion target, float lerpDuration)
    {

        float tiempoTranscurrido = 0f;


        while (tiempoTranscurrido < lerpDuration)
        {
            transform.rotation = Quaternion.Lerp(start, target, curve.Evaluate(tiempoTranscurrido / lerpDuration));

            tiempoTranscurrido += Time.deltaTime;

            yield return null;
        }

        transform.rotation = target;

        SwitchRotation();
    }

    
    void SwitchRotation()
    { 
        targetRotation = startRotation.eulerAngles;
        startRotation = transform.rotation;

        /*.eulerAngles vendría a convertir el quaternion a ángulos de Euler(los ángulos de siempre, que van de 0 a 360 grados). 
         * Justamente, estos ángulos están representados por un Vector3, osea un ángulo para cada uno de los ejes(x, y y z).
         * Al ser del tipo Vector3 ahora si te los dejaría asignar a targetRotation*/
    }

    IEnumerator LerpColor(Color start, Color target, float lerpDuration)
    {

        float tiempoTranscurrido = 0f;


        while (tiempoTranscurrido < lerpDuration)
        {
            sprRenderer.material.color = Color.Lerp(start, target, curve.Evaluate(tiempoTranscurrido / lerpDuration));

            tiempoTranscurrido += Time.deltaTime;

            yield return null;
        }

        sprRenderer.material.color = target;

        SwitchColor();
    }
    void SwitchColor()
    {
        colorACambiar = colorOriginal;
        colorOriginal = sprRenderer.material.color;
    }
}
