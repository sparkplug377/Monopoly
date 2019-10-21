/*
 * Author: Ramkumar Thiyagarajan
 * Description: Manages all the text fields in the scene
 * Created on 20/10/2019
 * Updated on 21/10/2019
 */

using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// UIManager class
/// temporary implementation
/// Updates all the text fields in the scene
/// </summary>
public class UIManager : MonoBehaviour
{
    public Text playerOneMoneyText;
    public Text playerTwoMoneyText;

    [SerializeField] private Text playerOneDiceText;
    [SerializeField] private Text playerOnePropText;
    [SerializeField] private Text playerOneGenText;

    [SerializeField] private Text playerTwoDiceText;
    [SerializeField] private Text playerTwoPropText;
    [SerializeField] private Text playerTwoGenText;


    public void SetDiceText(string message, int playerID)
    {
        if (playerID == 0)
        {
            playerOneDiceText.text = message;
        }
        else
        {
            playerTwoDiceText.text = message;
        }
    }

    public void SetGenText(string message, int playerID)
    {
        if (playerID == 0)
        {
            playerOneGenText.text = message;
        }
        else
        {
            playerTwoGenText.text = message;
        }
    }

    public void SetPropText(string message, int playerID)
    {
        if (playerID == 0)
        {
            playerOnePropText.text += "\n" + message;
        }
        else
        {
            playerTwoPropText.text += "\n" + message;
        }
    }
}
