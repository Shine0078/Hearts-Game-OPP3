using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeartsGame
{
    internal class Game
    {
        private Deck deck;
        private List<Player> players;
        private int currentPlayerIndex;
        

        public List<Player> Players { get { return players; } }
        public Game()
        {
            deck = new Deck();
            players = new List<Player>
            {
                new Player("Player 1"),
                new AIPlayer("AI Opponent 1"),
                new AIPlayer("AI Opponent 2"),
                new AIPlayer("AI Opponent 3")
            };
            currentPlayerIndex = 0;

        }

        public void StartNewGame()
        {
            deck.Shuffle();
            DealCards();
            //PlayRound();
        }

        private void DealCards()
        {
            foreach (Card card in deck.Cards)
            {
                players[currentPlayerIndex].AddCard(card);
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }
        }

    private int CountPoints(List<Card> cards)
        {
            int points = 0;
            foreach (Card card in cards)
            {
                if (card.Suit == Suit.Hearts)
                {
                    points++;
                }
                else if (card.Suit == Suit.Spades && card.Rank == Rank.Queen)
                {
                    points += 13;
                }
            }
            return points;
        }

    }
}
