﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CodingActivity_TicTacToe_ConsoleGame
{
    public class ConsoleView
    {
        #region ENUMS
        
        public enum ViewState
        {
            Active,
            PlayerTimedOut, // TODO Track player time on task
            PlayerUsedMaxAttempts
        }

        #endregion

        //a

        #region FIELDS

        private const int GAMEBOARD_VERTICAL_LOCATION = 4;

        private const int POSITIONPROMPT_VERTICAL_LOCATION = 13;
        private const int POSITIONPROMPT_HORIZONTAL_LOCATION = 3;

        private const int MESSAGEBOX_VERTICAL_LOCATION = 15;

        private const int TOP_LEFT_ROW = 3;
        private const int TOP_LEFT_COLUMN = 6;

        private Gameboard _gameboard;
        private ViewState _currentViewStat;

        #endregion

        #region PROPERTIES
        public ViewState CurrentViewState
        {
            get { return _currentViewStat; }
            set { _currentViewStat = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public ConsoleView(Gameboard gameboard)
        {
            _gameboard = gameboard;

            InitializeView();

        }

        #endregion

        #region METHODS

        /// <summary>
        /// Initialize the console view
        /// </summary>
        public void InitializeView()
        {
            _currentViewStat = ViewState.Active;

            InitializeConsole();
        }

        /// <summary>
        /// configure the console window
        /// </summary>
        public void InitializeConsole()
        {
            ConsoleUtil.WindowWidth = ConsoleConfig.windowWidth;
            ConsoleUtil.WindowHeight = ConsoleConfig.windowHeight;

            Console.BackgroundColor = ConsoleConfig.bodyBackgroundColor;
            Console.ForegroundColor = ConsoleConfig.bodyBackgroundColor;

            ConsoleUtil.WindowTitle = "The Tic-tac-toe Game";
        }

        /// <summary>
        /// display the Continue prompt
        /// </summary>
        public void DisplayContinuePrompt()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to continue.");
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        public void DisplayContinuePromptMenu()
        {
            Console.CursorVisible = false;

            Console.WriteLine();

            ConsoleUtil.DisplayMessage("Press any key to go back to Menu.");
            ConsoleKeyInfo response = Console.ReadKey();

            Console.WriteLine();

            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the Exit prompt on a clean screen
        /// </summary>
        public void DisplayExitPrompt()
        {
            ConsoleUtil.DisplayReset();

            Console.CursorVisible = false;

            Console.WriteLine();
            ConsoleUtil.DisplayMessage("Thank you for playing the game. Press any key to Exit.");

            Console.ReadKey();

            System.Environment.Exit(1);
        }

        /// <summary>
        /// display the session timed out screen
        /// </summary>
        public void DisplayTimedOutScreen()
        {
            ConsoleUtil.HeaderText = "Session Timed Out!";
            ConsoleUtil.DisplayReset();

            DisplayMessageBox("It appears your session has timed out.");

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the maximum attempts reached screen
        /// </summary>
        public void DisplayMaxAttemptsReachedScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Maximum Attempts Reached!";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you are having difficulty entering your");
            sb.Append(" choice. Please refer to the instructions and play again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }



        /// <summary>
        /// Inform the player that their position choice is not available
        /// </summary>
        public void DisplayGamePositionChoiceNotAvailableScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "Position Choice Unavailable";
            ConsoleUtil.DisplayReset();

            sb.Append(" It appears that you have chosen a position that is all ready");
            sb.Append(" taken. Please try again.");

            DisplayMessageBox(sb.ToString());

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            StringBuilder sb = new StringBuilder();

            ConsoleUtil.HeaderText = "The Tic-tac-toe Game";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Written by the Jaguars: Bruce, Ty, Morgan");
            ConsoleUtil.DisplayMessage("Northwestern Michigan College");
            Console.WriteLine();

            sb.Clear();
            sb.AppendFormat("This application is designed to allow two players to play ");
            sb.AppendFormat("a game of tic-tac-toe. The rules are the standard rules for the ");
            sb.AppendFormat("game with each player taking a turn.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            Console.WriteLine();

            sb.Clear();
            sb.AppendFormat("Entering Q during the game will exit program.");
            ConsoleUtil.DisplayMessage(sb.ToString());
            Console.WriteLine();

            DisplayContinuePrompt();
            DisplayMenuUserAction();
        }

        /// <summary>
        /// display the rules of the game
        /// </summary>
        public void DisplayRules()
        {
            ConsoleUtil.HeaderText = "Rules";
            ConsoleUtil.DisplayReset();

            Console.WriteLine("The game is played on a 4X4 grid");
            Console.WriteLine("Players take turns filling in spaces on the board until a player has 3 spaces in a row");
            Console.WriteLine("Players choose from between 1 through 4 to pick which row they want, ");
            Console.WriteLine("and choosing 1 through 4 again chooses which column they want");
            Console.WriteLine("If neither player has gotten 3 spaces in a row when the board is filled, ");
            Console.WriteLine("then the game is a tie or 'cat'. ");
            Console.WriteLine("Getting four corners is also a win.");
            Console.WriteLine("Press N for new game, R for rules, S for stats, and Q for quit.");

            DisplayContinuePromptMenu();
        }

        /// <summary>
        /// display a closing screen when the user quits the application
        /// </summary>
        public void DisplayClosingScreen()
        {
            ConsoleUtil.HeaderText = "The Tic-tac-toe Game";
            ConsoleUtil.DisplayReset();

            ConsoleUtil.DisplayMessage("Thank you for using The Tic-tac-toe Game.");

            DisplayContinuePrompt();
        }

        public void DisplayGameArea()
        {
            ConsoleUtil.HeaderText = "Current Game Board";
            ConsoleUtil.DisplayReset();

            DisplayGameboard();
            DisplayGameStatus();
        }

        public void DisplayCurrentGameStatus(int roundsPlayed, int playerXWins, int playerOWins, int catsGames)
        {
            ConsoleUtil.HeaderText = "Current Game Status";
            ConsoleUtil.DisplayReset();

            double playerXPercentageWins = (double)playerXWins / roundsPlayed;
            double playerOPercentageWins = (double)playerOWins / roundsPlayed;
            double percentageOfCatsGames = (double)catsGames / roundsPlayed;

            ConsoleUtil.DisplayMessage("Rounds Played: " + roundsPlayed);
            ConsoleUtil.DisplayMessage("Rounds for Player X: " + playerXWins + " - " + String.Format("{0:P2}", playerXPercentageWins));
            ConsoleUtil.DisplayMessage("Rounds for Player O: " + playerOWins + " - " + String.Format("{0:P2}", playerOPercentageWins));
            ConsoleUtil.DisplayMessage("Cat's Games: " + catsGames + " - " + String.Format("{0:P2}", percentageOfCatsGames));

            DisplayContinuePrompt();
        }

        public bool DisplayNewRoundPrompt()
        {
            ConsoleUtil.HeaderText = "Continue or Quit";
            ConsoleUtil.DisplayReset();

            return DisplayGetYesNoPrompt("Would you like to go back to menu?");
        }

        public void DisplayGameStatus()
        {
            StringBuilder sb = new StringBuilder();

            switch (_gameboard.CurrentRoundState)
            {
                case Gameboard.GameboardState.NewRound:
                    //
                    // The new game status should not be an necessary option here
                    //
                    break;
                case Gameboard.GameboardState.PlayerXTurn:
                    DisplayMessageBox("It is currently Player X's turn.");
                    break;
                case Gameboard.GameboardState.PlayerOTurn:
                    DisplayMessageBox("It is currently Player O's turn.");
                    break;
                case Gameboard.GameboardState.PlayerXWin:
                    DisplayMessageBox("Player X Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.PlayerOWin:
                    DisplayMessageBox("Player O Wins! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                case Gameboard.GameboardState.CatsGame:
                    DisplayMessageBox("Cat's Game! Press any key to continue.");

                    Console.CursorVisible = false;
                    Console.ReadKey();
                    Console.CursorVisible = true;
                    break;
                default:
                    break;
            }
        }

        public void DisplayMessageBox(string message)
        {
            string leftMargin = new String(' ', ConsoleConfig.displayHorizontalMargin);
            string topBottom = new String('*', ConsoleConfig.windowWidth - 2 * ConsoleConfig.displayHorizontalMargin);

            StringBuilder sb = new StringBuilder();

            Console.SetCursorPosition(0, MESSAGEBOX_VERTICAL_LOCATION);
            Console.WriteLine(leftMargin + topBottom);

            Console.WriteLine(ConsoleUtil.Center("Game Status"));

            ConsoleUtil.DisplayMessage(message);

            Console.WriteLine(Environment.NewLine + leftMargin + topBottom);
        }

        /// <summary>
        /// display the current game board
        /// </summary>
        private void DisplayGameboard()
        {
            //
            // move cursor below header
            //
            Console.SetCursorPosition(0, GAMEBOARD_VERTICAL_LOCATION);

            Console.Write("\t\t\t        |---+---+---+---|\n");

            for (int i = 0; i < 4; i++)
            {
                Console.Write("\t\t\t        | ");

                for (int j = 0; j < 4; j++)
                {
                    if (_gameboard.PositionState[i, j] == Gameboard.PlayerPiece.None)
                    {
                        Console.Write(" " + " | ");
                    }
                    else
                    {
                        Console.Write(_gameboard.PositionState[i, j] + " | ");
                    }

                }

                Console.Write("\n\t\t\t        |---+---+---+---|\n");
            }
        }

        /// <summary>
        /// display prompt for a player's next move
        /// </summary>
        /// <param name="coordinateType"></param>
        private void DisplayPositionPrompt(string coordinateType)
        {
            //
            // Clear line by overwriting with spaces
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write(new String(' ', ConsoleConfig.windowWidth));
            //
            // Write new prompt
            //
            Console.SetCursorPosition(POSITIONPROMPT_HORIZONTAL_LOCATION, POSITIONPROMPT_VERTICAL_LOCATION);
            Console.Write("Enter " + coordinateType + " number: ");
        }

        /// <summary>
        /// Display a Yes or No prompt with a message
        /// </summary>
        /// <param name="promptMessage">prompt message</param>
        /// <returns>bool where true = yes</returns>
        private bool DisplayGetYesNoPrompt(string promptMessage)
        {
            bool yesNoChoice = false;
            bool validResponse = false;
            string userResponse;

            while (!validResponse)
            {
                ConsoleUtil.DisplayReset();

                ConsoleUtil.DisplayPromptMessage(promptMessage + "(yes/no)");
                userResponse = Console.ReadLine();

                if (userResponse.ToUpper() == "YES")
                {
                    validResponse = true;
                    yesNoChoice = true;
                }
                else if (userResponse.ToUpper() == "NO")
                {
                    validResponse = true;
                    yesNoChoice = false;
                }
                else
                {
                    ConsoleUtil.DisplayMessage(
                        "It appears that you have entered an incorrect response." +
                        " Please enter either \"yes\" or \"no\"."
                        );
                    DisplayContinuePrompt();
                }
            }

            return yesNoChoice;
        }

        /// <summary>
        /// Get a player's position choice within the correct range of the array
        /// Note: The ConsoleView is allowed access to the GameboardPosition struct.
        /// </summary>
        /// <returns>GameboardPosition</returns>
        public GameboardPosition GetPlayerPositionChoice()
        {
            //
            // Initialize gameboardPosition with -1 values
            //
            GameboardPosition gameboardPosition = new GameboardPosition(-1, -1);

            //
            // Get row number from player.
            //
            gameboardPosition.Row = PlayerCoordinateChoice("Row");

            //
            // Get column number.
            //
            if (CurrentViewState != ViewState.PlayerUsedMaxAttempts)
            {
                gameboardPosition.Column = PlayerCoordinateChoice("Column");
            }

            return gameboardPosition;

        }

        /// <summary>
        /// Validate the player's coordinate response for integer and range
        /// </summary>
        /// <param name="coordinateType">an integer value within proper range or -1</param>
        /// <returns></returns>
        private int PlayerCoordinateChoice(string coordinateType)
        {
            int tempCoordinate = -1;
            int numOfPlayerAttempts = 1;
            int maxNumOfPlayerAttempts = 4;

            while ((numOfPlayerAttempts <= maxNumOfPlayerAttempts))
            {
                DisplayPositionPrompt(coordinateType);

                string userChoice = Console.ReadLine();
                if (int.TryParse(userChoice, out tempCoordinate))
                {
                    //
                    // Player response within range
                    //
                    if (tempCoordinate >= 1 && tempCoordinate <= _gameboard.MaxNumOfRowsColumns)
                    {
                        return tempCoordinate;
                    }
                    //
                    // Player response out of range
                    //
                    else
                    {
                        DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3,4)");
                    }
                }
                //
                // Always true exit game command
                //
                else if (userChoice.ToUpper() == "Q")
                {
                    DisplayExitPrompt();
                }

                //
                // Player response cannot be parsed as integer
                //
                else
                {
                    DisplayMessageBox(coordinateType + " numbers are limited to (1,2,3,4)");
                }

                //
                // Increment the number of player attempts
                //
                numOfPlayerAttempts++;
            }

            //
            // Player used maximum number of attempts, set view state and return
            //
            CurrentViewState = ViewState.PlayerUsedMaxAttempts;
            return tempCoordinate;
        }
        public bool DisplayExitGame()
        {
            ConsoleUtil.HeaderText = "Continue or Exit";
            ConsoleUtil.DisplayReset();

            bool? response = DisplayGetYesNoPrompt("Are you sure you want to quit the game?");
            if (response == null)
            {
                DisplayExitPrompt();
                return false;
            }
            else
            {
                return response.Value;
               
            }
        }

        //
        // Ask user if they want to play new game, view rules, see stats or quit game as a Menu
        //

        public string DisplayMenuUserAction()
        {
            bool validResponse = false;
            string userResponse;


            StringBuilder sb = new StringBuilder();


            ConsoleUtil.HeaderText = "Menu";
            ConsoleUtil.DisplayReset();

            sb.Clear();
            Console.WriteLine("     Menu Options:");
            Console.WriteLine("");
            Console.WriteLine("   N.  Play new round");
            Console.WriteLine("");
            Console.WriteLine("   R.  View rules");
            Console.WriteLine("");
            Console.WriteLine("   S.  View current game stats");
            Console.WriteLine("");
            Console.WriteLine("   Q.  Quit");
            ConsoleUtil.DisplayMessage(sb.ToString());

            ConsoleUtil.DisplayPromptMessage("(N, R, S or Q) : ");        
            userResponse = Console.ReadLine();


            while (!validResponse)
            {

                if (userResponse.ToUpper() == "N")
                {
                    validResponse = true;
                    
                }
                else if (userResponse.ToUpper() == "R")
                {
                    validResponse = true;
                    DisplayRules();
                    DisplayMenuUserAction();
                }
                else if (userResponse.ToUpper() == "S")
                {
                    validResponse = true;
                    DisplayCurrentGameStatus(0,0,0,0);
                    DisplayMenuUserAction();
                }
                else if (userResponse.ToUpper() == "Q")
                {
                    validResponse = true;
                    DisplayExitPrompt();                    
                }
                else
                {
                    ConsoleUtil.DisplayMessage(
                        "It appears that you have entered an incorrect response." +
                        " Please enter either N, R, S or Q."
                        );
                    DisplayContinuePrompt();
                    DisplayMenuUserAction();
                }
            }

            return userResponse;
        }
        #endregion
    }
}
