﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeartsGame
{
    public partial class frmHeartsGame : Form
    {
        public static List<PictureBox> PictureBoxes { get; set;  } = new List<PictureBox>();
        public static List<PictureBox> PictureBoxesAI { get; set; } = new List<PictureBox>();
        private Game game;
        private bool heartsBroken;
        private Card twoClubs;
        private Card leadingCard;
        private int roundsPlayed;
        private int currentPlayerIndex;
        private string imagePath;
        private int playCount;
        public Card selectedCard { get; set; }
        public Player startingPLayer { get; set; }

        public frmHeartsGame()
        {
            InitializeComponent();
        }

        private void UpdatePlayerHandUI(Player player, Card selectedCard)
        {
            PictureBoxes = new List<PictureBox>
            {
                pictureBoxCard1, pictureBoxCard2, pictureBoxCard3, pictureBoxCard4, pictureBoxCard5,
                pictureBoxCard6, pictureBoxCard7, pictureBoxCard8, pictureBoxCard9, pictureBoxCard10,
                pictureBoxCard11, pictureBoxCard12, pictureBoxCard13

            };
            PictureBoxesAI = new List<PictureBox>
            {
                pictureBox15, pictureBox16, pictureBox17
            };
            string projectPath = Directory.GetCurrentDirectory();
            for (int i = 0; i < player.Hand.Count; i++)
            {
                if (i < player.Hand.Count)
                {
                    // Get the file path for the card image
                    imagePath = Path.Combine(projectPath, "CardImg", $"{player.Hand[i].Rank}{player.Hand[i].Suit}.gif");

                    // Check if the image file exists
                    if (File.Exists(imagePath))
                    {
                        // Load the image and assign it to the PictureBox
                        PictureBoxes[i].Image = Image.FromFile(imagePath);
                    }
                    else
                    {
                        // Handle missing image file
                        MessageBox.Show($"Image file {imagePath} not found!");
                    }

                    // Make PictureBox visible
                    PictureBoxes[i].Visible = true;
                }
                else
                {
                    // Hide PictureBox if no card to display
                    PictureBoxes[i].Visible = false;
                }
            }

            // Display the selected card on the selected card PictureBox
            string selectedCardImgPath = Path.Combine(projectPath, "CardImg", $"{selectedCard.Rank}{selectedCard.Suit}.gif");
            if (File.Exists(selectedCardImgPath))
            {
                pictureBox14.Image = Image.FromFile(selectedCardImgPath);
                pictureBox14.Visible = true;
                pictureBox14.Location = new Point(266, 128);
            }
            else
            {
                MessageBox.Show($"Image file {selectedCardImgPath} not found!");
            }
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void changeBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Load the selected image and set it as the background image
                        string selectedImage = openFileDialog.FileName;
                        this.BackgroundImage = Image.FromFile(selectedImage);

                        // Resize the background image to fit the form
                        ResizeBackgroundImage();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading image: " + ex.Message);
                    }
                }
            }
        }

        private void ResizeBackgroundImage()
        {
            if (this.BackgroundImage != null)
            {
                // Calculate the ratio of form width to image width
                float ratioWidth = (float)this.Width / this.BackgroundImage.Width;

                // Calculate the ratio of form height to image height
                float ratioHeight = (float)this.Height / this.BackgroundImage.Height;

                // Use the smaller ratio to ensure that the entire image fits within the form
                float ratio = Math.Min(ratioWidth, ratioHeight);

                // Calculate the new size of the image
                int newWidth = (int)(this.BackgroundImage.Width * ratio);
                int newHeight = (int)(this.BackgroundImage.Height * ratio);

                // Resize the background image
                this.BackgroundImage = new Bitmap(this.BackgroundImage, new Size(newWidth, newHeight));
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Call the ResizeBackgroundImage method whenever the form is resized
            ResizeBackgroundImage();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void rulesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The dealer is chosen by all players when drawing a card. The highest card will become the dealer (if there is a tie they will draw again)." +
                " Each player is dealt 13 cards and there will be 13 rounds in total with each player playing 1 card in each round.\n\nFor the first round, " +
                "the player with the 2 of Hearts is the player who starts the game. The next subsequent player plays the next card with the same suit as the " +
                "first player of the round. If they can't, they can play any suit including the hearts. If this happens, the Hearts is now 'broken', meaning that a heart " +
                "can be played as a starting hand in the later rounds.\n\nAfter the first round, the starting player can play any card that is not a heart, unless the heart is " +
                "'broken'. The winner of each round is decided by having played the highest card with the same suit as the round's starting card. The player who won the round " +
                "takes all 4 cards played that round, and the player with the lowest number of points wins.\n\nEach heart is counted as 1 point and the Queen of Spades is counted as 5 " +
                "points. The game ends if a person either reaches 50 or 100 points depending on the settings.", "Rules of Hearts");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtboxPlayer1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxPlayer2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxPlayer3_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtboxPlayer4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panelCard1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSelect1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect2_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect3_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect4_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect5_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect6_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect7_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect8_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect9_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect10_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect11_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect12_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnSelect13_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int cardIndex = int.Parse(button.Tag.ToString());
            selectedCard = game.Players[0].Hand[cardIndex];

            // Call the UpdatePlayerHandUI method with the selected card
            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void startToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Check if both the textbox and checked boxes are empty
            if (string.IsNullOrWhiteSpace(txtboxPlayer1.Text) && !chBox50Points.Checked && !chBox100Points.Checked)
            {
                MessageBox.Show("Please enter the player name and select at least one option for points.", "Error");
                return;
            }

            // Check if the textbox is empty and if both check boxes are checked
            if (string.IsNullOrWhiteSpace(txtboxPlayer1.Text) && chBox50Points.Checked && chBox100Points.Checked)
            {
                MessageBox.Show("Please enter the player name and only select one option for points.", "Error");
                return;
            }

            // Check if the textbox is empty
            if (string.IsNullOrWhiteSpace(txtboxPlayer1.Text))
            {
                MessageBox.Show("Please enter the player name in the textbox.", "Error");
                return;
            }

            // Check if at any of the points check boxes are checked
            if (!chBox50Points.Checked && !chBox100Points.Checked)
            {
                MessageBox.Show("Please select at least one option for points.", "Error");
                return;
            }

            // Check if both check boxes are checked
            if (chBox50Points.Checked && chBox100Points.Checked)
            {
                MessageBox.Show("Please only select one option for points.", "Error");
                return;
            }


            game = new Game();

            game.StartNewGame();

            Card selectedCard = game.Players[0].Hand.FirstOrDefault();

            UpdatePlayerHandUI(game.Players[0], selectedCard);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Check if both the textbox and checked boxes are empty
            if (string.IsNullOrWhiteSpace(txtboxPlayer1.Text) && !chBox50Points.Checked && !chBox100Points.Checked)
            {
                MessageBox.Show("Please enter the player name and select at least one option for points.", "Error");
                return;
            }

            // Check if the textbox is empty and if both check boxes are checked
            if (string.IsNullOrWhiteSpace(txtboxPlayer1.Text) && chBox50Points.Checked && chBox100Points.Checked)
            {
                MessageBox.Show("Please enter the player name and only select one option for points.", "Error");
                return;
            }

            // Check if the textbox is empty
            if (string.IsNullOrWhiteSpace(txtboxPlayer1.Text))
            {
                MessageBox.Show("Please enter the player name in the textbox.", "Error");
                return;
            }

            // Check if at any of the points check boxes are checked
            if (!chBox50Points.Checked && !chBox100Points.Checked)
            {
                MessageBox.Show("Please select at least one option for points.", "Error");
                return;
            }

            // Check if both check boxes are checked
            if (chBox50Points.Checked && chBox100Points.Checked)
            {
                MessageBox.Show("Please only select one option for points.", "Error");
                return;
            }

            game = new Game();

            game.StartNewGame();

            Card selectedCard = game.Players[0].Hand.FirstOrDefault();

            UpdatePlayerHandUI(game.Players[0], selectedCard);

            // Find the player with the 2 of clubs
            Player startingPlayer = game.Players.FirstOrDefault(p => p.Hand.Any(c => c.Rank == Rank.Two && c.Suit == Suit.Clubs));
            // Play the first card
            twoClubs = startingPlayer.Hand.First(c => c.Rank == Rank.Two && c.Suit == Suit.Clubs);
            MessageBox.Show($"{startingPlayer.Name} starts the round with {twoClubs}");

            // Set the starting player index
            currentPlayerIndex = game.Players.IndexOf(startingPlayer);
            Player currentPlayer = game.Players[currentPlayerIndex];
            string projectPath = Directory.GetCurrentDirectory();
            AIPlayer aiPlayer = game.Players[currentPlayerIndex] as AIPlayer;
            if (currentPlayerIndex != 0)
            {
                Card chosenCard = aiPlayer.StartPlay();
                imagePath = Path.Combine(projectPath, "CardImg", $"{chosenCard.Rank}{chosenCard.Suit}.gif");

                leadingCard = chosenCard;
                MessageBox.Show($"AI {currentPlayerIndex} Played {chosenCard.Rank} of {chosenCard.Suit}");

                PictureBoxesAI[currentPlayerIndex - 1].Image = Image.FromFile(imagePath);
                currentPlayerIndex++;
                playCount++;
                if (currentPlayerIndex >= 4)
                {
                    currentPlayerIndex = 0;
                }
            }

            while (true)
            {
                if (currentPlayerIndex >= 4)
                {
                    currentPlayerIndex = 0;
                    break;
                }
                else if (currentPlayerIndex != 0)
                {
                    Card chosenCard = aiPlayer.PlayCard(leadingCard.Suit, heartsBroken);
                    imagePath = Path.Combine(projectPath, "CardImg", $"{chosenCard.Rank}{chosenCard.Suit}.gif");
                    PictureBoxesAI[currentPlayerIndex - 1].Image = Image.FromFile(imagePath);
                    leadingCard = chosenCard;
                    MessageBox.Show($"AI {currentPlayerIndex} Played {chosenCard.Rank} of {chosenCard.Suit}");
                    currentPlayerIndex++;
                    playCount++;
                    if (playCount > 4)
                    {
                        playCount = 0;
                        leadingCard = null;
                    }

                }
                else
                {
                    break;
                }
            }
        }
        private void btnPlayCard_Click(object sender, EventArgs e)
        {
            heartsBroken = false;
            roundsPlayed = 0;
            Player currentPlayer = game.Players[currentPlayerIndex];
            string projectPath = Directory.GetCurrentDirectory();
            // Go through each player and let them play a card
            for (int i = 1; i < 13; i++)
            {
                if (currentPlayer is AIPlayer aiPlayer)
                {
                    Card chosenCard = aiPlayer.PlayCard(leadingCard.Suit, heartsBroken);
                    MessageBox.Show($"AI {currentPlayerIndex} Played {chosenCard.Rank} of {chosenCard.Suit}");
                    imagePath = Path.Combine(projectPath, "CardImg", $"{chosenCard.Rank}{chosenCard.Suit}.gif");
                    PictureBoxesAI[currentPlayerIndex - 1].Image = Image.FromFile(imagePath);
                    currentPlayerIndex++;
                    playCount++;
                    if (playCount > 4)
                    {
                        playCount = 0;
                        //leadingCard = null;
                    }
                    if (currentPlayerIndex >= 4)
                    {
                        currentPlayerIndex = 0;
                    }
                }
                else if (currentPlayerIndex == 0)
                {
                    Card cardToPlay = selectedCard;
                    MessageBox.Show("You played " + selectedCard.Rank + " of " + selectedCard.Suit);
                    UpdatePlayerHandUI(game.Players[0], cardToPlay);
                    if (leadingCard == null)
                    {
                        leadingCard = selectedCard;
                    }
                    if (cardToPlay.Suit != leadingCard.Suit && !heartsBroken)
                    {
                        Console.WriteLine("Hearts broken!");
                        heartsBroken = true;
                    }
                    currentPlayerIndex++;
                    currentPlayer = game.Players[currentPlayerIndex];
                    playCount++;
                    if (playCount > 4)
                    {
                        playCount = 0;
                        //leadingCard = null;
                    }
                    if (currentPlayerIndex >= 4)
                    {
                        currentPlayerIndex = 0;
                    }
                    game.Players[0].RemoveCard(cardToPlay);
                }
            }

            // Determine the winner of the round
            Card highestCard = game.Players.SelectMany(p => p.Hand).Where(c => c.Suit == leadingCard.Suit).OrderByDescending(c => c.Rank).First();
            Player roundWinner = game.Players.First(p => p.Hand.Contains(highestCard));
            Console.WriteLine($"Round winner: {roundWinner.Name}");

            // Add points to the round winner
            foreach (Player player in game.Players)
            {
                if (player == roundWinner)
                    continue;

                foreach (Card card in player.Hand)
                {
                    if (card.Suit == Suit.Hearts)
                        roundWinner.Points++;
                    else if (card.Suit == Suit.Spades && card.Rank == Rank.Queen)
                        roundWinner.Points += 5;
                }
            }

            // Clear hands
            //foreach (Player player in game.Players)
                //player.ClearHand();

            // Increment rounds played
            roundsPlayed++;

            // Check for game end conditions
            if (roundWinner.Points >= 50 || roundsPlayed >= 100)
            {
                MessageBox.Show($"{roundWinner} won!");
                return;
            }
        }

        private void chBox50Points_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void videoDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the Video Demo Form
            VideoDemoForm videoDemoForm = new VideoDemoForm();
            videoDemoForm.Show();
        }

    }
}