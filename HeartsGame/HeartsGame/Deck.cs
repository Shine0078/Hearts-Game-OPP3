using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsGame
{
    public class Deck
    {
        private List<Card> myDeck;

        // Creating new Deck
        public Deck()
        {
            myDeck = new List<Card>();
            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    myDeck.Add(new Card(r, s));
                }
            }
        }

        public Card[] Cards
        {
            get { return myDeck.ToArray(); }
        }

        // Shuffle the Deck
        public void Shuffle()
        {
            Random rnd = new Random();

            for (int i = myDeck.Count - 1; i > 0; --i)
            {
                int shuf = rnd.Next(i + 1);
                Card temp = myDeck[i];
                myDeck[i] = myDeck[shuf];
                myDeck[shuf] = temp;
            }
        }
    }
}
