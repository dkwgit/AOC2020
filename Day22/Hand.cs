namespace AOC2020.Day22
{
    using System.Diagnostics;
    using System.Numerics;

    internal class Hand
    {
        public Hand(BigInteger cards, int count)
        {
            Cards = cards;
            CardCount = count;
        }

        public static BigInteger TopCardMask { get; } = new BigInteger(63); // a bitmask of 6 ones.

        public BigInteger Cards { get; set; }

        public int CardCount { get; set; }

        public static Hand DealHand(int[] cards)
        {
            BigInteger value = BigInteger.Zero;
            int count = 0;
            for (int i = cards.Length - 1; i >= 0; i--)
            {
                value <<= 6;
                value += cards[i];
                count++;
            }

            return new Hand(value, count);
        }

        public static int[] GetCards(BigInteger current, int count)
        {
            BigInteger value = current + BigInteger.Zero;
            int[] cards = new int[count];
            for (int i = 0; i < count; i++)
            {
                cards[i] = GetBottomValue(value);
                value >>= 6;
            }

            return cards;
        }

        public int PlayCard()
        {
            int card = GetBottomValue(Cards);
            Cards >>= 6;
            CardCount--;

            return card;
        }

        public long Score()
        {
            BigInteger copy = Cards + BigInteger.Zero;
            long score = 0;
            for (int i = CardCount; i >= 1; i--)
            {
                score += GetBottomValue(copy) * i;
                copy >>= 6;
            }

            return score;
        }

        public void AddAtBack(int card)
        {
            BigInteger toAdd = new BigInteger(card);
            toAdd <<= CardCount * 6;
            Cards += toAdd;
            CardCount++;
        }

        private static int GetBottomValue(BigInteger value)
        {
            byte[] bytes = (value & TopCardMask).ToByteArray();
            Debug.Assert(bytes.Length == 1, "Expecting only one byte");
            int card = (int)bytes[0];
            return card;
        }
    }
}
