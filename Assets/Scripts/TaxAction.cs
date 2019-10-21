/*
 * Author: Ramkumar Thiyagarajan
 * Description: Executes the tax action of the player
 * Created on 21/10/2019
 * Updated on 21/10/2019
 */

using UnityEngine;

/// <summary>
/// Action to pay an income or luxury tax
/// </summary>
public class TaxAction : Action
{
    public override void ExecuteAction(Game game, Player currentPlayer, Cell currentCell)
    {
        bool tenPercent = false;
        if (currentCell.Name == "Income Tax")       // pay income tax either 10% or 200$
        {
            game.ui.SetGenText("Press 'T' to pay 10% or 'M' to pay 200$", currentPlayer.PlayerID);

            if (Input.GetKeyDown(KeyCode.T))        // pay 10%
            {
                tenPercent = true;
                int tax = currentPlayer.PayIncomeTax(tenPercent);
                game.ui.SetGenText("Tax Paid: " + tax.ToString(), currentPlayer.PlayerID);
                game.EndTurn();
            }

            else if (Input.GetKeyDown(KeyCode.M))   // pay  200$
            {
                int tax = currentPlayer.PayIncomeTax(tenPercent);
                game.ui.SetGenText("Tax Paid: " + tax.ToString(), currentPlayer.PlayerID);
                game.EndTurn();
            }
        }
        else if (currentCell.Name == "Luxury Tax")  // pay luxury tax 75$
        {
            currentPlayer.PayLuxuryTax(currentCell.Rent);
            game.ui.SetGenText("Tax Paid: " + currentCell.Rent.ToString(), currentPlayer.PlayerID);
            game.EndTurn();
        }
        else                                        // chance or community cells
        {
            game.ui.SetGenText("This feature is coming soon!", currentPlayer.PlayerID);
            game.EndTurn();
        }
    }
}
