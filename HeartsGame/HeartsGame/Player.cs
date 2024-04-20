using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeartsGame
{
    public class Player
    {
        public string Name { get; }
        public List<Card> Hand { get; }
        public int Points { get; set; }

        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
            Points = 0;
        }

        public void AddCard(Card card)
        {
            Hand.Add(card);
        }

        public void RemoveCard(Card card)
        {
            int cardIndex = Hand.IndexOf(card);
            Hand.Remove(card);
        }

        public void ClearHand()
        {
            Hand.Clear();
        }
    }
}

