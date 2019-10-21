/*
 * Author: Ramkumar Thiyagarajan
 * Description: Executes pay rent of the player
 * Created on 21/10/2019
 * Updated on 21/10/2019
 */

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pay rent action
/// </summary>
public class RentAction : Action
{
    public override void ExecuteAction(Game game, Player currentPlayer, Cell currentCell)
    {
        if (currentCell.Ownership != (PropertyOwnership)currentPlayer.PlayerID)     // pay rent
        {
            Player owner = game.players[(int)currentCell.Ownership];          // get the owner of the property
            int rent = currentPlayer.PayRent(currentCell, owner, game.RollResult);
            game.ui.SetGenText("Rent paid: " + rent.ToString(), currentPlayer.PlayerID);
        }
        else
        {
            game.ui.SetGenText("you are the owner!", currentPlayer.PlayerID);
        }
        game.EndTurn();
    }
}
