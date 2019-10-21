/*
 * Author: Ramkumar Thiyagarajan
 * Description: Executes the current action of the player
 * Created on 21/10/2019
 * Updated on 21/10/2019
 */

/// <summary>
/// Abstract action class with function to excute current action
/// actions could be buy, auction, trade, go to jail, release from jail and so on...
/// </summary>
public abstract class Action
{
    public abstract void ExecuteAction(Game game, Player currentPlayer, Cell currentCell);
}
