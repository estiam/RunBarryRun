using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public static int nbLives = 3;
    public static int coins = 0;

    public Text lifeText;
    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        if(nbLives == 0)
        {
            coins = 0;
            nbLives = 3;
        }
    }

    void Update()
    {
        lifeText.text = nbLives.ToString();
        coinsText.text = coins.ToString();
    }

    public static void Die()
    {
        nbLives--;
    }

    public static void GetCoin()
    {
        coins++;
    }

 
    
}
