using System;
using System.Linq;
using Microsoft.Maui.Controls;


namespace HangmanAssignment
{
    public partial class HangmanGamePage : ContentPage
    {
        private string wordToGuess = "CORRECTGUESS";  // The word to guess
        private string displayWord;                   // Current state of the word (e.g., "_O___CTGU___")
        private int attempts = 6;                     // Number of attempts left
        private string[] hangmanImages =
        {
            "hang1.png", "hang2.png", "hang3.png",
            "hang4.png", "hang5.png", "hang6.png", "hang7.png"
        };

        public HangmanGamePage()
        {
            InitializeComponent();
            ResetGame();
        }

        private void ResetGame()
        {
            displayWord = new string('_', wordToGuess.Length); // Initialize blank underscores
            UpdateDisplay();
        }

        private void OnGuessButtonClicked(object sender, EventArgs e)
        {
            var guess = GuessEntry.Text?.ToUpper();
            if (string.IsNullOrWhiteSpace(guess) || guess.Length != 1)
            {
                DisplayAlert("Error", "Please enter a single valid letter.", "OK");
                return;
            }

            if (wordToGuess.Contains(guess))
            {
                UpdateDisplayWord(guess);
            }
            else
            {
                attempts--;
                HangmanImage.Source = hangmanImages[6 - attempts];
            }

            CheckGameStatus();
            GuessEntry.Text = ""; // Clear input
        }

        private void UpdateDisplayWord(string guess)
        {
            char[] displayChars = displayWord.ToCharArray();
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == guess[0])
                {
                    displayChars[i] = guess[0];
                }
            }

            displayWord = new string(displayChars);
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            wordLabel.Text = string.Join(" ", displayWord.ToCharArray());
            AttemptsLabel.Text = $"Attempts Left: {attempts}";
        }

        private void CheckGameStatus()
        {
            if (displayWord == wordToGuess)
            {
                DisplayAlert("Congratulations!", "You guessed the word correctly!", "Play Again");
                ResetGame();
            }
            else if (attempts <= 0)
            {
                DisplayAlert("Game Over", $"You lost! The word was {wordToGuess}.", "Try Again");
                ResetGame();
            }
        }
    }
}
