/*
 * Author: Ramkumar Thiyagarajan
 * Description: Manages the entire game
 * Created on 20/10/2019
 * Updated on 21/10/2019
 */

using UnityEngine;


/// <summary>
/// Game or Main class
/// Manages the entire game
/// </summary>
public class Game : MonoBehaviour
{
    const int NUMPLAYERS = 2;               // num of players in game
    const int NUMDICE = 2;                  // num of dice in game

    private bool playerOneTurn;             // swap player's turn
    private int[] dices;                    // 2 dice in game

    public UIManager ui;                    // manages UI text fields - added this to show some details

    public Player[] players;                // 2 players in game

    private Board board;

    private Cell currentCell = null;        // current cell the player is on

    private Player currentPlayer = null;    // current player in the game

    private Action action;                  // current action

    private BuyAction buyAction;            // action to buy or pass a property
    private TaxAction taxAction;            // action to pay an income or luxury tax
    private RentAction rentAction;          // action to pay rent

    /// <summary>
    /// Set up the players with money, label and a sprite
    /// </summary>
    void SetUpPlayers()
    {

        players = new Player[NUMPLAYERS];       // create reference to the players.

        GameObject[] sprites = GameObject.FindGameObjectsWithTag("Player"); // grab the sprites from the scene

        for (int i = 0; i < NUMPLAYERS; ++i)    // assign initial money, set playerID and add sprites to the player
        {
            players[i] = new Player(1500, i);
            players[i].SetPlayerSprite(sprites[i]);
        }

        currentPlayer = (playerOneTurn) ? players[0] : players[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        board = GameObject.FindGameObjectWithTag("GameBoard").GetComponent<Board>();

        dices = new int[NUMDICE];   // create reference to the dice

        playerOneTurn = true;       // can pick during the game as well, for now player one always starts first.

        SetUpPlayers();

        buyAction = new BuyAction(); // reference to actions
        taxAction = new TaxAction();
        rentAction = new RentAction();

        ui.playerOneMoneyText.text = "Money: " + players[0].Money;
        ui.playerTwoMoneyText.text = "Money: " + players[1].Money;

        RollResult = 12;            
        currentCell = null;
        action = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (RollResult != 0)
        {
            StartTurn();                                        // Start turn


            if (action != null)
            {
                action.ExecuteAction(this, currentPlayer, currentCell); // Execute current action
            }
        }
        else
        {
            EndGame();
        }

        if (dices[0] == 6 && dices[1] == 6 && 
            players[0].numTrips > 2 && players[1].numTrips > 2)                 // End Game
        {
                RollResult = 0;
        }
    }

    /// <summary>
    /// rolls the dice
    /// </summary>
    private void DiceRoll()
    {
        if (dices != null && dices.Length == 2)
        {
            dices[0] = Random.Range(1, 7);
            dices[1] = Random.Range(1, 7);
            RollResult = dices[0] + dices[1];
        }
    }

    /// <summary>
    /// Represents the result of the dice roll
    /// </summary>
    public int RollResult{ get; private set; }


    /// <summary>
    /// start the round
    /// roll the dice, pick an action (buy, pay rent & pay tax)
    /// </summary>
    void StartTurn()
    {
        if (currentCell != null)
        {
            if (currentCell.Status == PropertyStatus.UNSOLD)
            {
                action = buyAction;
            }
            else if (currentCell.Status == PropertyStatus.SOLD)
            {
                action = rentAction;
            }
            else
            {
                action = taxAction;
            }
        }
        else
        {
            ui.SetGenText("Press 'SPACE' to roll!", currentPlayer.PlayerID);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DiceRoll();
                ui.SetDiceText("Rolled: " + RollResult.ToString(), currentPlayer.PlayerID);
                currentPlayer.diceRolls.Add(RollResult);

                currentPlayer.ChangeCurrentIndex(RollResult);                       // change the index of player on the board

                currentCell = board.cells[currentPlayer.CurrentIndex];              // get the cell player is about to land on

                currentPlayer.Move(currentCell.CellTransform);                      // Move the player to the cell
            }
        }
    }


    /// <summary>
    /// Ends the turn
    /// swap the players, reset the action and current cell
    /// </summary>
    public void EndTurn()
    {
        ui.playerOneMoneyText.text = "Money: " + players[0].Money;
        ui.playerTwoMoneyText.text = "Money: " + players[1].Money;

        // swap the players
        playerOneTurn = !playerOneTurn;
        currentPlayer = (playerOneTurn) ? players[0] : players[1];

        currentCell = null;
        action = null;
    }

    /// <summary>
    /// Ends the game
    /// </summary>
    public void EndGame()
    {
        ui.SetGenText("GAME OVER!", 0);
        ui.SetGenText("GAME OVER!", 1);

        Time.timeScale = 0;                     // pause the game


        players[0].Save("player1");         // save player 1
        players[1].Save("player2");         // save player 2


        if (Input.GetKeyDown(KeyCode.Escape))   // Exit the window
            Application.Quit();
    }
}
