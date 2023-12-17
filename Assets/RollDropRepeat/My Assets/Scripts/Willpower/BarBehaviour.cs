using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image _bar;
    
    public void SetBar(float amount)
    {
        _bar.fillAmount = amount;
    }
}
