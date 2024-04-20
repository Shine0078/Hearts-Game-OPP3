using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace HeartsGame
{
    public class AIPlayer : Player
    {

        public AIPlayer(string name) : base(name)
        {
        }

        public Card PlayCard(Suit leadingSuit, bool heartsBroken)
        {
            // Play 2 of clubs first if available
            Card twoOfClubs = Hand.FirstOrDefault(c => c.Suit == Suit.Clubs && c.Rank == Rank.Two);
            if (twoOfClubs != null)
            {
                Hand.Remove(twoOfClubs);
                return twoOfClubs;
            }

            // If leading suit is Hearts and hearts isn't broken, try to play other suits
            if (leadingSuit == Suit.Hearts && !heartsBroken)
            {
                var nonHeartCards = Hand.Where(c => c.Suit != Suit.Hearts);
                if (nonHeartCards.Any())
                {
                    var cardToPlay = nonHeartCards.OrderBy(c => (int)c.Rank).Last();
                    Hand.Remove(cardToPlay);
                    return cardToPlay;
                }
            }

            // Otherwise, play the highest card of the leading suit if possible
            var validCards = Hand.Where(c => c.Suit == leadingSuit);
            if (validCards.Any())
            {
                var cardToPlay = validCards.OrderBy(c => (int)c.Rank).Last();
                Hand.Remove(cardToPlay);
                return cardToPlay;
            }

            // If no valid cards of the leading suit, play any card
            var anyCard = Hand.OrderBy(c => (int)c.Rank).Last();
            Hand.Remove(anyCard);
            return anyCard;
        }


        public Card StartPlay()
        {
            Card twoOfClubs = Hand.FirstOrDefault(c => c.Suit == Suit.Clubs && c.Rank == Rank.Two);
            Hand.Remove(twoOfClubs);
            return twoOfClubs;
        }
    }
}

