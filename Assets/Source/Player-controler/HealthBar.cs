using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarforegroundImage;
    public void UpdateHealthBar(HealthController healthController)
    {
        _healthBarforegroundImage.fillAmount = healthController.RemaningHealthPercentage;
    }
        
    
}
