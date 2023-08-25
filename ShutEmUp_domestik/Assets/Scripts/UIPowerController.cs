using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerController : MonoBehaviour
{
    //Es una buena practica utilizar varios canvas
    //para distintos objetos, ej: textos o animaciones, otro canvas para
    //objetos interactuables... es mas eficiente

    [SerializeField] private Text powerLevel;

    [SerializeField] private Image fillImage;


    //suscribirse a evento de gamecontroller para actualizar puntos
    private void Start()
    {
        GameController.Instance.Player.OnPowerChanged += OnPowerChanged;

        UpdateFillImagePercentage(GameController.Instance.Player.GetCurrentPowerLevel(), GameController.Instance.Player.GetMaxPowerLevel());
    }

    private void OnPowerChanged(int currentPower, int totalPower)
    {
        UpdateFillImagePercentage(currentPower, totalPower);
    }

    public void UpdateFillImagePercentage(int current, int total)
    {
        if(powerLevel != null)
        {
            powerLevel.text = string.Format("{0}/{1}", current, total);
        }

        // valores decimales, entre 0 y 1
        fillImage.fillAmount = (float)current / (float)total;
    }

    //desuscribirse
    private void OnDestroy()
    {
        if(GameController.Instance != null && GameController.Instance.Player != null)
        {
            GameController.Instance.Player.OnPowerChanged -= OnPowerChanged;
        }
    }
}
