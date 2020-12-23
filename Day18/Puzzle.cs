namespace AOC2020.Day18
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "18";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                var result = _input.Select(x => ParseLinePart1(x)).ToList();
                string answer = result.Sum().ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as the sum of all expressions using no precedence", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                var result = _input.Select(x => ParseLinePart2(x)).ToList();
                string answer = result.Sum().ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} as the sume of all expressions with addition having higher precedence than multiplication", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = new List<string>();
            foreach (var item in input)
            {
                _input.Add(item.Replace(" ", string.Empty));
            }
        }

        private static long ParseLinePart1(string line)
        {
            List<string> stack = new ();

            foreach (var ch in line)
            {
                stack.Add(string.Empty + ch);

                if (char.IsDigit(ch) && stack.Count >= 3)
                {
                    ProcessSecondNumericOperandPart1(stack);
                }

                if (ch == ')')
                {
                    Debug.Assert(stack[^3] == "(", "Expected an opening paren 2 elements away");
                    Debug.Assert(long.TryParse(stack[^2], out _), "Expected previous item to be a number");

                    var number = stack[^2];
                    stack.RemoveRange(stack.Count - 3, 3);
                    stack.Add(number);
                    if (stack.Count >= 3)
                    {
                        ProcessSecondNumericOperandPart1(stack);
                    }
                }
            }

            Debug.Assert(stack.Count == 1, "Expected stack to have only one element after processing");
            Debug.Assert(long.TryParse(stack[0], out _), "Expected sole item to be a number");

            return long.Parse(stack[0]);
        }

        private static void ProcessSecondNumericOperandPart1(List<string> stack)
        {
            if (stack[^2] == "*" || stack[^2] == "+")
            {
                long priorNumber = long.Parse(stack[^3]);
                long currentNumber = long.Parse(stack[^1]);
                long result = stack[^2] switch
                {
                    "+" => priorNumber + currentNumber,
                    "*" => priorNumber * currentNumber,
                    _ => throw new InvalidOperationException($"Unexpected value {stack[^2]} in switch case"),
                };
                stack.RemoveRange(stack.Count - 3, 3);
                stack.Add(result.ToString());
            }
        }

        private static long ParseLinePart2(string line)
        {
            List<string> stack = new ();

            foreach (var ch in line)
            {
                stack.Add(string.Empty + ch);
                if (ch == ')')
                {
                    int openParenIndex = -1;
                    for (int i = stack.Count - 2; i >= 0; i--)
                    {
                        if (stack[i] == "(")
                        {
                            openParenIndex = i;
                            break;
                        }
                    }

                    ProcessExpressionPart2(stack, openParenIndex, stack.Count - 1);
                }
            }

            Debug.Assert(stack.FindIndex(x => x == "(") == -1 && stack.FindIndex(x => x == ")") == -1, "Expecting stack to only have no parens");
            ProcessExpressionPart2(stack, 0, stack.Count - 1);
            Debug.Assert(stack.Count == 1, "Expecting stack to only have one element");
            return long.Parse(stack[0]);
        }

        private static void ProcessExpressionPart2(List<string> stack, int startIndex, int endIndex)
        {
            int originalStart = startIndex;
            int originalEnd = endIndex;

            if (stack[startIndex] == "(")
            {
                startIndex++;
            }

            if (stack[endIndex] == ")")
            {
                endIndex--;
            }

            List<string> expression = stack.GetRange(startIndex, endIndex - startIndex + 1);

            Debug.Assert(expression.Find(x => x == "(" || x == ")") == null, "Expected no parens in expression");
            bool resolvePlus = true;
            while (resolvePlus)
            {
                int plusIndex = expression.FindIndex(x => x == "+");
                if (plusIndex == -1)
                {
                    resolvePlus = false;
                    break;
                }

                Debug.Assert(long.TryParse(expression[plusIndex - 1], out _) && long.TryParse(expression[plusIndex + 1], out _), "Expected + to be surrounded by numbers");
                long before = long.Parse(expression[plusIndex - 1]);
                long after = long.Parse(expression[plusIndex + 1]);
                long result = before + after;
                expression.RemoveRange(plusIndex - 1, 3);
                expression.Insert(plusIndex - 1, result.ToString());
            }

            bool resolveMultiply = true;
            while (resolveMultiply)
            {
                int multiplyIndex = expression.FindIndex(x => x == "*");
                if (multiplyIndex == -1)
                {
                    resolveMultiply = false;
                    break;
                }

                Debug.Assert(long.TryParse(expression[multiplyIndex - 1], out _) && long.TryParse(expression[multiplyIndex + 1], out _), "Expected * to be surrounded by numbers");
                long before = long.Parse(expression[multiplyIndex - 1]);
                long after = long.Parse(expression[multiplyIndex + 1]);
                long result = before * after;
                expression.RemoveRange(multiplyIndex - 1, 3);
                expression.Insert(multiplyIndex - 1, result.ToString());
            }

            Debug.Assert(expression.Count == 1, "Expecting expression to only have one element");
            Debug.Assert(long.TryParse(expression[0], out _), "Expecting remaining element of expression to be numeric");

            stack.RemoveRange(originalStart, originalEnd - originalStart + 1);
            stack.Insert(originalStart, expression[0]);
        }
    }
}
