using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IDamageable
{

	public void TakeDamage(float damage)
    {
        ScoreManager.Instance.LoseLife ((int) damage);
    }
}
