namespace AOC2020.Day05
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    record BoardingPass
    {
        public const int TotalRows = 128;

        public const int SeatsPerRow = 8;

        public int SeatId { get; }

        public string SeatCode { get; }

        public BoardingPass(string seatCode) => (SeatCode, SeatId) = (seatCode, GetSeatId(seatCode));

        private static int GetSeatId(string seatCode)
        {
            int highIndexOfRows = TotalRows - 1;
            int lowIndexOfRows = 0;
            int highIndexOfSeats = SeatsPerRow - 1;
            int lowIndexOfSeats = 0;

            foreach (var code in seatCode)
            {
                switch (code)
                {
                    case 'F':
                        highIndexOfRows -= (highIndexOfRows - lowIndexOfRows + 1) / 2;
                        break;
                    case 'B':
                        lowIndexOfRows += (highIndexOfRows - lowIndexOfRows + 1) / 2;
                        break;
                    case 'L':
                        highIndexOfSeats -= (highIndexOfSeats - lowIndexOfSeats + 1) / 2;
                        break;
                    case 'R':
                        lowIndexOfSeats += (highIndexOfSeats - lowIndexOfSeats + 1) / 2;
                        break;
                    default:
                        throw new Exception($"Bad code {code} in seatCode {seatCode}");
                }
            }

            return (highIndexOfRows * SeatsPerRow) + highIndexOfSeats;
        }
    }
}
