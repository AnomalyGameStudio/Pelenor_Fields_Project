using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IDamageable
{

    public float maxShield = 100f;
    public float maxArmor = 100f;
    public float maxHull = 100f;

    public float shieldRechargeRate = 1f;
    public float RechargeCooldown = 1f;

    private float currentShield = 0f;
    private float currentArmor = 0f;
    private float currentHull = 0f;
    private float nextTimeRecharge = 0f;

    void Start()
    {
        // Initialize the player life
        currentShield = maxShield;
        currentArmor = maxArmor;
        currentHull = maxHull;

        // Set up recharge cooldown
        nextTimeRecharge = Time.time + RechargeCooldown;

        // Updates the UI
        UpdateScore();
    }

    void Update()
    {
        // Recharge the shield
        rechargeShield();
    }

    private void UpdateScore()
    {
        ScoreManager.Instance.Shield = currentShield;
        ScoreManager.Instance.Armor = currentArmor;
        ScoreManager.Instance.Hull = currentHull;
    }

	public void TakeDamage(float damage)
    {
        // If the ship still has the shields Up (Shield > 0), the damage should be to the shield
        if(currentShield > 0)
        {
            TakeShieldDamage(damage);
        }
        // If the ship still has armor, the damage will be dealt to the Armor
        else if (currentArmor > 0)
        {
            TakeArmorDamage(damage);
        }
        // If the ship doesn't have neither armor nor shield, deal damage to the Hull
        else
        {
            TakeHullDamage(damage);
        }

        // Updates the UI
        UpdateScore();
    }

    // Code responsible to handle the Damage applied to the Shield
    private void TakeShieldDamage(float damage)
    {
        // Damages the Shield
        currentShield -= damage;
        // Set up the recharge cooldown
        nextTimeRecharge = Time.time + RechargeCooldown;
        // Clamp the current Shield between 0 and the Maximum shield
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);
    }

    // Code responsible to handle the Damage applied to the Armor
    private void TakeArmorDamage(float damage)
    {
        // Damages the Armor
        currentArmor -= damage;
        // Clamp the Current armor between 0 and the Maximum Armor
        currentArmor = Mathf.Clamp(currentArmor, 0, maxArmor);
    }

    // Code responsible to handle the Damage applied to the Hull
    private void TakeHullDamage(float damage)
    {
        // Damages the Hull
        currentHull -= damage;
        // Clamp the Current Hull between 0 and the Maximum Hull
        currentHull = Mathf.Clamp(currentHull, 0, maxHull);

        if(currentHull <= 0)
        {
            ScoreManager.Instance.GameOver();
        }
    }

    // TODO Verificar se vai ser usado publicamente, enquanto isso deixar private
    private void rechargeShield()
    {
        // If the shield is less than max and the Recharge is not on cooldown, heals the shield
        if(Time.time > nextTimeRecharge && currentShield < maxShield)
        {
            // Sends a negative value to the shield Damage Handler. This will act as heal;
            TakeShieldDamage(-shieldRechargeRate);
            
            // Updates the UI
            UpdateScore();
        }
    }
}
