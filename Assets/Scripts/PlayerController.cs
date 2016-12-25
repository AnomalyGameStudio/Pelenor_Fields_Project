using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IDamageable
{

    public float shield = 100f;
    public float armor = 100f;
    public float hull = 100f;

    public float shieldRechargeRate = 1f;
    public float RechargeCooldown = 1f;

    private float currentShield = 0;

    private float nextTimeRecharge = 0f;

    void Start()
    {
        currentShield = shield;
        nextTimeRecharge = Time.time + RechargeCooldown;

        ScoreManager.Instance.Shield = (int) currentShield;
    }

    void Update()
    {
        // Recharge the shield
        rechargeShield();
        
    }

	public void TakeDamage(float damage)
    {
        if(currentShield > 0)
        {
            currentShield -= damage;
            nextTimeRecharge = Time.time + RechargeCooldown;
            ScoreManager.Instance.Shield = (int) currentShield;

            // TODO add a clamp to the damage.
        }
        else
        {
            ScoreManager.Instance.LoseLife((int)damage);
        }
        //ScoreManager.Instance.LoseLife ((int) damage);
    }

    // TODO Verificar se vai ser usado publicamente, enquanto isso deixar publico
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
    }
    private void rechargeShield()
    {
        if(Time.time > nextTimeRecharge && currentShield < shield)
        {
            
            // TODO Update UI... Not Idea how this will work
            currentShield += shieldRechargeRate * Time.deltaTime;
            
            // TODO See if this is correct. Might need to see the documentation
            currentShield = Mathf.Clamp(currentShield, 0, shield);
            ScoreManager.Instance.Shield = (int)currentShield;
        }
    }
}
