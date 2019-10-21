/*
 * Author: Ramkumar Thiyagarajan
 * Description: executes player buy action
 * Created on 21/10/2019
 * Updated on 21/10/2019
 */

using UnityEngine;

/// <summary>
/// Action to buy property.
/// </summary>
public class BuyAction : Action
{
    public override void ExecuteAction(Game game, Player currentPlayer, Cell currentCell)
    {
        game.ui.SetGenText("Press 'ENTER' to buy or 'ESC' to decline", currentPlayer.PlayerID);

        if (Input.GetKeyDown(KeyCode.Return))           // buy the prooperty 
        {
            currentPlayer.Buy(currentCell);
            game.ui.SetPropText(currentCell.Name, currentPlayer.PlayerID);
            game.ui.SetGenText("Property Bought: " + currentCell.Name, currentPlayer.PlayerID);
            game.EndTurn();
        }

        if (Input.GetKeyDown(KeyCode.Escape))           // pass the property
        {
            game.ui.SetGenText("Auction feature coming soon", currentPlayer.PlayerID);
            game.EndTurn();
        }
    }
}
