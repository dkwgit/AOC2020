namespace AOC2020.Day19
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AOC2020.Utilities;

    record MultiRule : IAbstractRule
    {
        public bool IsVariableLength
        {
            get
            {
                return Value.Where(x => x.GetType() == typeof(SelfRule)).ToList().Count > 0;
            }
        }

        public int ExpansionIncrement
        {
            get
            {
                if (!IsVariableLength)
                {
                    return 0;
                }

                return Value.Sum(x => x.MatchLength);
            }
        }

        public IAbstractRule Parent { get; init; }

        public int Id { get; init; }

        public string GeneratingExpression { get; init; }

        public List<IAbstractRule> Value { get; init; }

        public int MatchLength { get; set; }

        public MultiRule(IAbstractRule parent, int id, string generatingExpression, List<IAbstractRule> value) => (Parent, Id, GeneratingExpression, Value) = (parent, id, generatingExpression, value);

        public bool Valid(string expression)
        {
            List<IAbstractRule> rulesToUse = new ();
            rulesToUse.AddRange(Value);
            int baseLength = rulesToUse.Sum(x => x.MatchLength);

            while (IsVariableLength && baseLength < expression.Length)
            {
                int index = rulesToUse.FindIndex(x => x.GetType() == typeof(SelfRule));
                rulesToUse.RemoveAt(index);
                rulesToUse.InsertRange(index, Value);
                baseLength = rulesToUse.Sum(x => x.MatchLength);
            }

            List<(IAbstractRule rule, List<int> expansions)> rulesWithExpansions = new ();
            foreach (var item in rulesToUse)
            {
                if (item.GetType() == typeof(SelfRule))
                {
                    continue;
                }

                rulesWithExpansions.Add((item, new List<int>() { 0 }));
            }

            foreach (var (rule, expansions) in rulesWithExpansions)
            {
                if (rule.IsVariableLength && baseLength < expression.Length)
                {
                    GenerateExpansions(expansions, rule.ExpansionIncrement, baseLength, expression.Length);
                }
            }

            List<List<int>> alphabet = new ();
            foreach (var (_, expansions) in rulesWithExpansions)
            {
                alphabet.Add(expansions);
            }

            var comboGenerator = new ComboGeneratorWithPositionalAlphabet<int>(alphabet, rulesWithExpansions.Count, true);
            var allCombos = comboGenerator.Iterator().Distinct().Where(x => x.Sum() + baseLength == expression.Length).ToList();

            List<List<int>> combos = new ();
            foreach (var combo in allCombos)
            {
                bool include = true;
                Debug.Assert(combo.Count == rulesWithExpansions.Count, "Expect the combinations to have the same number of elements as the total rules involved");
                for (int ruleIndex = 0; ruleIndex < rulesWithExpansions.Count; ruleIndex++)
                {
                    if (!rulesWithExpansions[ruleIndex].expansions.Any(x => x == combo[ruleIndex]))
                    {
                        include = false;
                        break;
                    }
                }

                if (include)
                {
                    combos.Add(combo);
                }
            }

            if (combos.Count == 0)
            {
                return false;
            }

            List<List<bool>> comboResults = new ();
            foreach (var combo in combos)
            {
                List<bool> comboResult = new ();
                int startingStringIndex = 0;
                for (int i = 0; i < combo.Count; i++)
                {
                    (IAbstractRule rule, _) = rulesWithExpansions[i];

                    int expressionLength = rule.MatchLength + combo[i];
                    comboResult.Add(rule.Valid(expression.Substring(startingStringIndex, expressionLength)));
                    startingStringIndex += expressionLength;
                }

                comboResults.Add(comboResult);
            }

            if (comboResults.Any(x => x.All(y => y)))
            {
                return true;
            }

            return false;
        }

        public static MultiRule Create(Puzzle p, IAbstractRule parent, int id, string expression)
        {
            var ruleIdList = expression.Split(' ').Select(x => int.Parse(x)).ToList();

            List<IAbstractRule> rules = new ();
            foreach (var ruleId in ruleIdList)
            {
                if (ruleId == id)
                {
                    rules.Add(new SelfRule(parent, id, expression));
                }
                else
                {
                    rules.Add(p.LoadRule(parent, ruleId));
                }
            }

            MultiRule rule = new MultiRule(parent, id, expression, rules)
            {
                MatchLength = rules.Sum(x => x.MatchLength),
            };

            return rule;
        }

        private static void GenerateExpansions(List<int> expansions, int expansionIncrement, int baseLength, int expressionLength)
        {
            int incrementBase = 0;
            while (baseLength + incrementBase + expansionIncrement <= expressionLength)
            {
                expansions.Add(incrementBase + expansionIncrement);
                incrementBase += expansionIncrement;
            }
        }
    }
}
