

using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    public float stamina;
    public float staminaRecoveryRate = 2f;
    public float staminaDepletionRate = 10f;
    
    [SerializeField] private Image staminaBar;
    void Start()
    {
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        if (stamina < maxStamina)
        {
            stamina += staminaRecoveryRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0f, maxStamina);
        }
        UpdateStaminaUI();
    }
    public void UpdateStaminaUI()
    {
        
        float hFraction = stamina / maxStamina;
        staminaBar.fillAmount = hFraction;
        
        

    }

}
