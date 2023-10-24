
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealf : MonoBehaviour,ITakingDamage
{
    private float healh;
    private float LerpTimer;
    [Header("Healf Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("Damege Overlay")]
    public Image overlay;
    public float duration;
    public float FadeSpead;

    private float duractionTimer;
    public float Healh { get => healh; }
    void Start()
    {
        healh = maxHealth;
        overlay.color =
                   new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        healh =Mathf.Clamp(healh, 0, maxHealth);
        UpdatehealthUI();

        if (overlay.color.a > 0)
        {
            duractionTimer += Time.deltaTime;
            if(duractionTimer>duration)
            {
                float tempAlpha=overlay.color.a;
                tempAlpha-=Time.deltaTime*FadeSpead;
                overlay.color= 
                    new Color(overlay.color.r,overlay.color.g,overlay.color.b,tempAlpha);

            }
        }
        
    }
    public void UpdatehealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = healh / maxHealth;
        if(fillB>hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            LerpTimer += Time.deltaTime;
            float percentComplete = LerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color =Color.green;
            backHealthBar.fillAmount =hFraction;
            LerpTimer += Time.deltaTime;
            float percentComplete = LerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);

        }
    }
    public void TakeDamage(float damage)
    {
        healh -= damage;
        LerpTimer = 0f;
        duractionTimer= 0f;
        if(healh< 20f) 
        {
            overlay.color =new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0.7f);

        }
    }
    public void RestoreHealth(float healfAmount)
    {
        healh += healfAmount;
        LerpTimer = 0f;
    }
    
}
