namespace AOC2020.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public record RepresentationComparer
    {
        public List<string> Input { get; init; }

        public RepresentationComparer(List<string> input) => Input = input;

        public bool Different(List<string> other)
        {
            if (other.Count != Input.Count)
            {
                return true;
            }

            for (int i = 0; i < Input.Count; i++)
            {
                if (Input[i].CompareTo(other[i]) != 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
