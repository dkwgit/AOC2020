namespace AOC2020.Day21
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AOC2020.Utilities;
    using Microsoft.Extensions.Logging;

    public class Puzzle : IPuzzle
    {
        private readonly ILogger _logger;

        private readonly Dictionary<int, Dictionary<string, bool>> _foodToIngredients = new ();

        private readonly Dictionary<string, Dictionary<int, bool>> _ingredientToFoods = new ();

        private readonly Dictionary<int, Dictionary<string, bool>> _foodToAllergens = new ();

        private readonly Dictionary<string, Dictionary<int, bool>> _allergenToFoods = new ();

        private List<string> _ingredientsNotPresentInAllFoodsForAllAllergens;

        private List<string> _input = null;

        public Puzzle(ILogger<Puzzle> logger)
        {
            _logger = logger;
        }

        public string Day => "21";

        public List<string> Input => _input;

        public string Part1
        {
            get
            {
                _ingredientsNotPresentInAllFoodsForAllAllergens = _ingredientToFoods.Keys.Where(x => IsIngredientNotAssociatedWithAnyAllergen(x)).ToList();
                string answer = _ingredientsNotPresentInAllFoodsForAllAllergens.Sum(i => _ingredientToFoods[i].Keys.Count).ToString();
                _logger.LogInformation("{Day}/Part1: Found {answer} occurrences ingredients which are associated with no allergens", Day, answer);
                return answer;
            }
        }

        public string Part2
        {
            get
            {
                var ingredientsToMapToAllergens = _ingredientToFoods.Keys.Where(i => !_ingredientsNotPresentInAllFoodsForAllAllergens.Any(x => x == i)).Select(i => i).ToList();

                string answer = GetDangerousIngredients(ingredientsToMapToAllergens);
                _logger.LogInformation("{Day}/Part2: Found {answer} as list of dangerous ingredients, sorted by underlying allergen", Day, answer);
                return answer;
            }
        }

        public void ProcessPuzzleInput(List<string> input)
        {
            _input = input;

            int food = 0;
            foreach (var line in _input)
            {
                int endIngredientList = line.IndexOf(" (contains ");
                int beginAllergyList = endIngredientList + " (contains ".Length;
                int endAllergyList = line.IndexOf(')');
                int lengthOfAllergyList = endAllergyList - beginAllergyList;

                string ingredients = line.Substring(0, endIngredientList);
                string allergens = line.Substring(beginAllergyList, lengthOfAllergyList).Replace(" ", string.Empty);

                List<string> ingredientList = new (ingredients.Split(' '));
                List<string> allergenList = new (allergens.Split(','));

                var ingredientDict = new Dictionary<string, bool>();
                _foodToIngredients.Add(food, ingredientDict);

                foreach (var ingredient in ingredientList)
                {
                    if (!_ingredientToFoods.ContainsKey(ingredient))
                    {
                        _ingredientToFoods.Add(ingredient, new Dictionary<int, bool>());
                    }

                    ingredientDict.Add(ingredient, true);
                    _ingredientToFoods[ingredient].Add(food, true);
                }

                var allergenDict = new Dictionary<string, bool>();
                _foodToAllergens.Add(food, allergenDict);

                foreach (var allergen in allergenList)
                {
                    if (!_allergenToFoods.ContainsKey(allergen))
                    {
                        _allergenToFoods.Add(allergen, new Dictionary<int, bool>());
                    }

                    allergenDict.Add(allergen, true);
                    _allergenToFoods[allergen].Add(food, true);
                }

                food++;
            }
        }

        private string GetDangerousIngredients(List<string> ingredients)
        {
            Dictionary<string, List<string>> allergenToIngredients = new ();

            // Location all the ingredients present in all foods that declare an allergen.
            // This can "overmatch" ingredients, so that we can now have one or more candidate ingredients for an allergen
            foreach (var allergen in _allergenToFoods.Keys)
            {
                var allFoodsForAllergen = _allergenToFoods[allergen].Keys.ToList();

                foreach (string ingredient in ingredients)
                {
                    if (allFoodsForAllergen.All(a => _foodToIngredients[a].ContainsKey(ingredient)))
                    {
                        if (!allergenToIngredients.ContainsKey(allergen))
                        {
                            allergenToIngredients.Add(allergen, new List<string>());
                        }

                        allergenToIngredients[allergen].Add(ingredient);
                    }
                }
            }

            // If an allergen has a unique ingredient, then that ingredient truly is related to the allergen
            // Remove that ingredient as a candidate from other allergens.
            // Repeat until we have one ingredient for one allergen, for every allergen
            while (allergenToIngredients.Any(x => allergenToIngredients[x.Key].Count > 1))
            {
                var uniqueIngredients = allergenToIngredients.Where(x => allergenToIngredients[x.Key].Count == 1).SelectMany(x => x.Value).ToList();
                foreach (var allergen in allergenToIngredients.Where(x => allergenToIngredients[x.Key].Count > 1).Select(x => x.Key).ToList())
                {
                    var ingredientList = allergenToIngredients[allergen];
                    foreach (var ingredient in uniqueIngredients)
                    {
                        int index = ingredientList.FindIndex(x => x == ingredient);
                        if (index != -1)
                        {
                            ingredientList.RemoveAt(index);
                        }
                    }
                }
            }

            // Retrieve the ingredients ordered by their related allergen
            List<string> dangerousIngredients = new ();
            foreach (var key in allergenToIngredients.Keys.OrderBy(x => x))
            {
                dangerousIngredients.Add(allergenToIngredients[key][0]);
            }

            return string.Join(",", dangerousIngredients);
        }

        private bool IsIngredientNotAssociatedWithAnyAllergen(string ingredient)
        {
            List<bool> resultsByAllergen = new ();
            foreach (string allergen in _allergenToFoods.Keys)
            {
                bool inAllFoodsForAnAllergen = true;
                foreach (var food in _allergenToFoods[allergen].Keys)
                {
                    if (!_foodToIngredients[food].Keys.Any(i => i == ingredient))
                    {
                        inAllFoodsForAnAllergen = false;
                        break;
                    }
                }

                resultsByAllergen.Add(inAllFoodsForAnAllergen);
            }

            return !resultsByAllergen.Any(r => r);
        }
    }
}