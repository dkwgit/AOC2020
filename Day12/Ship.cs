namespace AOC2020.Day12
{
    using System;
    using System.Collections.Generic;
    using Point = AOC2020.Map.Point;

    internal class Ship
    {
        private readonly Dictionary<char, (int, int)> _letterToOffset = new ();

        private readonly Dictionary<(int, int), char> _offsetToLetter = new ();

        private readonly Dictionary<(int, int), (int, int)> _turns = new ();

        private Point _current;

        private (int, int) _facing;

        public Ship((int, int) facing, Point start)
        {
            _facing = facing;
            Start = _current = start;

            _letterToOffset.Add('N', (0, 1));
            _letterToOffset.Add('S', (0, -1));
            _letterToOffset.Add('E', (1, 0));
            _letterToOffset.Add('W', (-1, 0));

            foreach (var k in _letterToOffset.Keys)
            {
                _offsetToLetter.Add(_letterToOffset[k], k);
            }

            _turns.Add((0, 1), (1, 0));
            _turns.Add((1, 0), (0, -1));
            _turns.Add((0, -1), (-1, 0));
            _turns.Add((-1, 0), (0, 1));
        }

        public Point Start { get; init; }

        public Point Current => _current;

        public (int, int) Facing => _facing;

        public int ManhattanDistance
        {
            get
            {
                return Math.Abs(_current.X - Start.X) + Math.Abs(_current.Y - Start.Y);
            }
        }

        public void ProcessInstruction(string instruction)
        {
            char letter = instruction[0];
            int number = int.Parse(instruction[1..]);

            switch (letter)
            {
                case 'F':
                    Move(_facing, number);
                    break;
                case 'N':
                case 'E':
                case 'S':
                case 'W':
                    Move(_letterToOffset[letter], number);
                    break;
                case 'R':
                    TurnRight(number);
                    break;
                case 'L':
                    TurnLeft(number);
                    break;
            }
        }

        private void Move((int, int) offset, int number)
        {
            (int, int) total = (offset.Item1 * number, offset.Item2 * number);
            _current = _current.PointFromOffset(total);
        }

        private void TurnLeft(int number)
        {
            int newNumber = number switch
            {
                90 => 270,
                180 => 180,
                270 => 90,
            };
            TurnRight(newNumber);
        }

        private void TurnRight(int number)
        {
            int turns = number switch
            {
                90 => 1,
                180 => 2,
                270 => 3
            };

            for (int i = 0; i < turns; i++)
            {
                _facing = _turns[_facing];
            }
        }
    }
}
