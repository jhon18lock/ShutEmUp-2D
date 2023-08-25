using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreController : MonoBehaviour
{
    //Es una buena practica utilizar varios canvas
    //para distintos objetos, ej: textos o animaciones, otro canvas para
    //objetos interactuables... es mas eficiente
    //si es interactuable , usar canvas y Graphic Raycast

    [SerializeField] private Text scoreValueText;


    //suscribirse a evento de gamecontroller para actualizar puntos
    private void Start()
    {
        GameController.Instance.OnScoreChanged += OnScoreChanged;

        UpdateScoreValueText(GameController.Instance.PlayerScore);
    }

    private void OnScoreChanged(int updatedScore)
    {
        UpdateScoreValueText(updatedScore);
    }
    public void UpdateScoreValueText(int newScore)
    {
        scoreValueText.text = newScore.ToString();
    }

    //desuscribirse
    private void OnDestroy()
    {
        if(GameController.Instance != null)
        {
            GameController.Instance.OnScoreChanged -= OnScoreChanged;
        }
    }
}
