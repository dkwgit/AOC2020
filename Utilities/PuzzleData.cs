namespace AOC2020.Utilities
{
    using System.Collections.Generic;

    public record PuzzleData
    {
        public string Day { get; }

        public string Type { get; }

        public List<string> Input { get; }

        public string AnswerPart1 { get; }

        public string AnswerPart2 { get; }

        public string AdditionalNote { get; }

        public bool Enabled { get; }

        public PuzzleData(string day, string type, List<string> input, string answerPart1, string answerPart2, string additionalNote = "", bool enabled = true) =>
            (Day, Type, Input, AnswerPart1, AnswerPart2, AdditionalNote, Enabled) = (day, type, input, answerPart1, answerPart2, additionalNote, enabled);
    }
}
