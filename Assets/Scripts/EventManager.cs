using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    public delegate void diceRollEventRaiser(Player currentPlayer, Cell currentCell);
    public static event diceRollEventRaiser OnDiceRollEvent;


    public static void RaiseOnButtonPressed()
    {
        if (OnDiceRollEvent != null)
        {
            //OnDiceRollEvent();
        }
    }

}
