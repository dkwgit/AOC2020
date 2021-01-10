namespace AOC2020.Day19
{
    using System.Collections.Generic;
    using System.Diagnostics;

    internal record TerminalRule : IAbstractRule
    {
        public bool IsVariableLength { get; } = false;

        public int ExpansionIncrement { get; } = 0;

        public IAbstractRule Parent { get; init; }

        public int Id { get; init; }

        public string GeneratingExpression { get; init; }

        public string Value { get; init; }

        public int MatchLength { get; set; }

        public TerminalRule(IAbstractRule parent, int id, string generatingExpression, string value) => (Parent, Id, GeneratingExpression, Value, MatchLength) = (parent, id, generatingExpression, value, 1);

        public bool Valid(string expression)
        {
            return expression == Value;
        }

        public static TerminalRule Create(Puzzle _, IAbstractRule parent, int id, string expression)
        {
            Debug.Assert(expression.Length == 3 && (expression[1] == 'a' || expression[1] == 'b'), $"Unexpected expression for Terminalrule: {expression}");

            return new TerminalRule(parent, id, expression, expression[1].ToString());
        }
    }
}
