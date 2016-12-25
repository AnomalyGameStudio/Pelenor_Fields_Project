using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{
    private float hull = 0;
    private float armor = 0;
    private float shield = 0;

    private int money = 100;
    
    public Text moneyText;

    public Text hullText;
    public Text armorText;
    public Text shieldText;

    protected ScoreManager() {}

    public int Money
    {
        get
        {
            return money;
        }
    }

    public float Shield
    {
        get
        {
            return shield;
        }

        set
        {
            if (shield != value)
            {
                shield = value;
                shieldText.text = "Shield: " + shield;
            }
        }
    }

    public float Armor
    {
        get
        {
            return armor;
        }

        set
        {
            if (armor != value)
            {
                armor = value;
                armorText.text = "Armor: " + armor;
            }
            
        }
    }

    public float Hull
    {
        get
        {
            return hull;
        }

        set
        {
            if( hull != value)
            {
                hull = value;
                hullText.text = "Hull: " + hull;
            }
            
        }
    }

    // DEPRECATED
    public void LoseLife(int l=1)
    {
        hull -= l;

        if(hull <= 0)
        {
            GameOver();
        }
    }
    
    public void GameOver()
    {
        Debug.Log("Game Over");

        // TODO: Send the player to a game-over screen instead!
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        // FIXME: This Doesn't actually need to update the text every frame.
        moneyText.text = "Money: " + money;
    }

    // Use for all money transactions
    public bool addMoney(int money)
    {
        bool transactionStatus = false;

        // Tries to add the money. if it goes negative, denies the transaction
        if ((this.money + money) < 0)
        {
            Debug.Log("Not enough money");
            return transactionStatus;
        }

        this.money += money;
        transactionStatus = true;
        return transactionStatus;
    }
}
