using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HappinessBarScript : MonoBehaviour
{
    public Image uiBarImage;
    
    public void UpdateHappiness(float maxHappiness, float currentHappiness)
    {
        uiBarImage.fillAmount = currentHappiness/maxHappiness;
    }
}
