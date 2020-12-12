namespace AOC2020.Day10
{
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private List<string> _input = null;

        private List<int> _adapters = new List<int>();

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "10";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                string answer = string.Empty;

                var result = _adapters.Skip(1).Select((x, index) => x - _adapters[index]).ToList(); // Because of skip, index is one lower than the real current position in list, so passing index get's previous
                int oneDiffsMultipliedWithThreeDiffs = result.Where(x => x == 1).Count() * result.Where(x => x == 3).Count();
                answer = oneDiffsMultipliedWithThreeDiffs.ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} as the multiplied total of sum of adapters with a 1 jolt difference and the sum of those with 3 jolt difference", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                string answer = string.Empty;

                List<List<int>> runs = new ();
                List<int> run = null;

                // Get the sub paths (runs) through adapter list that contain variability
                for (int index = 0; index < _adapters.Count; index++)
                {
                    int current = _adapters[index];

                    if (run != null && current - run[^1] >= 3)
                    {
                        // If we have a run going and current is 3 or more away from its last, terminate the run
                        if (run.Count > 2)
                        {
                            // Only runs longer than 2 contain variability
                            runs.Add(run);
                        }

                        run = null;
                    }

                    if (run == null)
                    {
                        run = new ();
                    }

                    run.Add(current);
                }

                // Build a tree for each sub path from above
                List<Tree> trees = new ();
                for (int i = 0; i < runs.Count; i++)
                {
                    var r = runs[i];
                    Tree tree = new Tree(r);
                    trees.Add(tree);
                }

                // Evaluate the trees for how they affect the combinations. We have to multiply total paths by the number of paths throug each tree.
                long totalPaths = 1;
                foreach (var tree in trees)
                {
                    int paths = tree.CountLeaves();
                    totalPaths *= paths;
                }

                answer = totalPaths.ToString();
                _logger.LogInformation("{Day}/Part2: Found {answer} as the total number of combinations of adapters", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;
            _adapters.Add(0);
            foreach (var item in input)
            {
                _adapters.Add(int.Parse(item));
            }

            _adapters.Add(_adapters.Max() + 3);
            _adapters = _adapters.OrderBy(x => x).ToList();
        }
    }
}
