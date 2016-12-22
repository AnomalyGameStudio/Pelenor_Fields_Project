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
