using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsGame
{
    public class Card
    {
        public Suit mySuit { get; }
        public Rank myRank { get; }

        // Constructor for Card Object
        public Card(Rank rank, Suit suit)
        {
            myRank = rank;
            mySuit = suit;
        }

        public Rank Rank { get { return myRank; } }
        public Suit Suit { get { return mySuit; } }

        public override string ToString()
        {
            return myRank.ToString() + " of " + mySuit.ToString();
        }

    }
}
