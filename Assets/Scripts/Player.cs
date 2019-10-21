/*
 * Author: Ramkumar Thiyagarajan
 * Description: Contains all the methods for performing player actions in the game
 * Created on 20/10/2019
 * Updated on 21/10/2019
 */

using System.Collections.Generic;
using UnityEngine;


/// <summary> Defines the number of Utilities the player has </summary>
public enum Utilities { NONE, ONE, BOTH }

/// <summary> Defines the number of RailRoads the player has </summary>
public enum RailRoads { NONE, ONE, TWO, THREE, FOUR }

/// <summary>
/// Player class
/// Contains all the methods for performing player actions in the game
/// </summary>
public class Player
{

    public List<string> properties;     // temporary - Name of all the properties the player has brought

    public List<int> diceRolls;         // temporary - results of the dice roll

    public int numTrips;                // temporary - num of trips around the board - using to end game


    /// <summary>
    /// setting up initial values
    /// </summary>
    /// <param name="money"> initial money (1500$) </param>
    /// <param name="playerID"> player ID (0 or 1) </param>
    public Player(int money, int playerID)
    {
        Money = money;
        RailRoads = 0;
        PlayerUtilities = 0;
        PlayerSprite = null;
        properties = new List<string>();
        diceRolls = new List<int>();
        CurrentIndex = 0;
        PlayerID = playerID;
        numTrips = 0;
    }

    /// <summary> Money represents the amount of money the player has </summary>
    public int Money { get; private set; }

    /// <summary> Raidroads represents the number of railroads the player has bought </summary>
    public RailRoads RailRoads { get; private set; }

    /// <summary> Utilities represents the number of untilities the player has bought </summary>
    public Utilities PlayerUtilities { get; private set; }

    /// <summary> PlayerSprite represents the gameobject of the player on the board  </summary>
    public GameObject PlayerSprite { get; private set; }

    /// <summary> CurrentIndex represents the index of the player's position on the board </summary>
    public int CurrentIndex { get; private set; }

    /// <summary> PlayerID represents whether it's first or second player </summary>
    public int PlayerID { get; private set; }


    /// <summary>
    /// Set initial object of the player
    /// </summary>
    /// <param name="obj"> reference to the sprite in the scene </param>
    public void SetPlayerSprite(GameObject obj)
    {
        PlayerSprite = obj;
    }


    /// <summary>
    /// Calculate Utility rent 
    /// </summary>
    /// <param name="diceRoll"> the result on the dice roll </param>
    /// <returns>utility rent due</returns>
    private int CalculateUtilityRent(int diceRoll)
    {
        int utilityRent = 0;

        switch (PlayerUtilities)
        {
            case Utilities.ONE:
                utilityRent = diceRoll * 4;
                break;
            case Utilities.BOTH:
                utilityRent = diceRoll * 10;
                break;
        }
        return utilityRent;
    }

    /// <summary>
    /// Move the player to the new position
    /// </summary>
    /// <param name="newTransform"> reference to the new transform </param>
    public void Move(Transform newTransform)
    {
        PlayerSprite.transform.position = newTransform.position;
    }

    /// <summary>
    /// change the current index of the player
    /// </summary>
    /// <param name="diceRoll"> the result of dice roll </param>
    public void ChangeCurrentIndex(int diceRoll)
    {

        if (diceRoll + CurrentIndex > 39)   // if the player's position had reached the last cell of the board
        {
            CurrentIndex += diceRoll - 39;

            Money += 200;                   // Receive advance money
            numTrips++;
        }
        else
        {
            CurrentIndex += diceRoll;
        }
    }

    /// <summary>
    /// Buy a property that is either a color stripe, railroad or an utility
    /// </summary>
    /// <param name="property"> buyable property </param>
    public void Buy(Cell property)
    {

        if (property.Price <= Money)                // if the price is within player's budget
        {
            Money -= property.Price;                // deduct the money from the player's account

            switch (property.Type)                  // if it's a railroad or utility
            {
                case PropertyType.RAILROAD:
                    RailRoads++;
                    break;
                case PropertyType.UTILITY:
                    PlayerUtilities++;
                    break;
            }

            property.UpdateCell(PropertyStatus.SOLD, (PropertyOwnership)PlayerID);  // set the property to be sold

            properties.Add(property.Name);          // Add the propert's name to the player's properties list
        }
    }

    /// <summary>
    /// Pay a rent to a property that is owned by another player
    /// </summary>
    /// <param name="property">reference to the property on the board</param>
    /// <param name="otherPlayer"> owner of the property </param>
    /// <param name="diceRoll"> the value of dice roll </param>
    /// <returns> return paid </returns>
    public int PayRent(Cell property, Player otherPlayer, int diceRoll)
    {
        int owner = (int)property.Ownership;
        int rent = 0;

        switch (property.Type)                      // rent depends on the type of property
        {
            case PropertyType.COLOR_STRIPE:
                rent = property.Rent;
                break;
            case PropertyType.RAILROAD:
                rent = property.Rent * (int)otherPlayer.RailRoads;
                break;
            case PropertyType.UTILITY:
                rent = otherPlayer.CalculateUtilityRent(diceRoll);
                break;
        }
        Money -= rent;                              // deduct the rent from the current player's account
        otherPlayer.Money += rent;                  // Add rent to the owner's account
        return rent;
    }

    /// <summary>
    /// Pay luxury tax to the bank
    /// </summary>
    /// <param name="taxAmount"> amount to pay </param>
    public void PayLuxuryTax(int taxAmount)
    {
        Money -= taxAmount;
    }

    /// <summary>
    /// Pay income tax to the bank
    /// </summary>
    /// <param name="tenPercent">pay 10% or 200</param>
    /// <returns></returns>
    public int PayIncomeTax(bool tenPercent)
    {
        int tax = 0;

        if (tenPercent)                             // pay 10% to the bank
        {
            float temp = Money * 10 / 100;
            tax = (int)temp;
        }
        else                                        // pay 200$ to the bank
        {
            tax = 200;
        }

        Money -= tax;                               // deduct tax

        return tax;
    }

    /// <summary>
    /// temporary - Save file to see the properties and the dice rolls
    /// </summary>
    /// <param name="fileName"> name of the file </param>
    public void Save(string fileName)
    {
        string s = fileName + "Properties" + ".txt";
        System.IO.File.WriteAllLines(s, properties);

        s = fileName + "DiceRolls" + ".txt";
        System.IO.TextWriter writer = new System.IO.StreamWriter(s);

        foreach(int d in diceRolls)
        {
            writer.WriteLine(d);
        }
        writer.Close();
    }
}
