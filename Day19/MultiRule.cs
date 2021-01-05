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
                return Value.Where(x => x.GetType() == typeof(SelfRule)).Count() > 0;
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

            // if we can expand this rule and need to, to handle an expression, do so
            while (IsVariableLength && baseLength < expression.Length)
            {
                int index = rulesToUse.FindIndex(x => x.GetType() == typeof(SelfRule));
                rulesToUse.RemoveAt(index);
                rulesToUse.InsertRange(index, Value);
                baseLength = rulesToUse.Sum(x => x.MatchLength);
            }

            List<(IAbstractRule rule, List<int> expansions)> rulesWithExpansions = GetRulesWithExpansions(rulesToUse, baseLength, expression);

            // combos contains all the possible rule lengths that together add up to the expression length, including the effects of expansions
            List<int[]> combos = GetCombos(rulesWithExpansions, baseLength, expression);

            // if there are no combos, no combination will handle the length of the expression
            if (combos.Count == 0)
            {
                return false;
            }

            List<List<bool>> comboResults = new ();

            // Now step through each combo, split the expression into substrings for each rule per the combo and store whether the combo was valid
            foreach (var combo in combos)
            {
                List<bool> comboResult = new ();

                int startingStringIndex = 0;
                for (int i = 0; i < combo.Length; i++)
                {
                    (IAbstractRule rule, _) = rulesWithExpansions[i];

                    int expressionLength = rule.MatchLength + combo[i];
                    comboResult.Add(rule.Valid(expression.Substring(startingStringIndex, expressionLength)));
                    startingStringIndex += expressionLength;
                }

                comboResults.Add(comboResult);
            }

            // fine any combos for which each rule returned valid == true.  If any, the MuliRule is valid for that expression.
            if (comboResults.Any(x => x.All(y => y)))
            {
                return true;
            }

            return false;
        }

        public static MultiRule Create(Puzzle p, IAbstractRule parent, int id, string expression)
        {
            var ruleIdList = expression.Split(' ').Select(x => int.Parse(x));

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

        // For each rule, get a list of expansion sizes.  Rules that do not expand will have one expansion of 0
        private static List<(IAbstractRule rule, List<int> expansions)> GetRulesWithExpansions(List<IAbstractRule> rulesToUse, int baseLength, string expression)
        {
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

            return rulesWithExpansions;
        }

        // For rules that expand, find out how many times it can expand and handle the expression. These expansions (growth in lenngth) are added to the expanion list for that rule
        private static void GenerateExpansions(List<int> expansions, int expansionIncrement, int baseLength, int expressionLength)
        {
            int incrementBase = 0;
            while (baseLength + incrementBase + expansionIncrement <= expressionLength)
            {
                expansions.Add(incrementBase + expansionIncrement);
                incrementBase += expansionIncrement;
            }
        }

        // Get combos between various rules, by combining the possible lengths of the rules (including expansiosn) to sum up to the length of the expression
        private static List<int[]> GetCombos(List<(IAbstractRule rule, List<int> expansions)> rulesWithExpansions, int baseLength, string expression)
        {
            int[][] alphabet = new int[rulesWithExpansions.Count][];
            int alphabetSlot = 0;
            foreach (var (_, expansions) in rulesWithExpansions)
            {
                alphabet[alphabetSlot] = new int[expansions.Count];
                for (int i = 0; i < expansions.Count; i++)
                {
                    alphabet[alphabetSlot][i] = expansions[i];
                }

                alphabetSlot++;
            }

            var comboGenerator = new ComboGeneratorWithPositionalAlphabet<int>(alphabet, rulesWithExpansions.Count);
            var combos = comboGenerator.Iterator().Distinct().Where(x => x.Sum() + baseLength == expression.Length).ToList();

            return combos;
        }
    }
}
