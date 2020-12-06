namespace AOC2020.Day06
{
    using System.Collections.Generic;
    using System.Linq;

    record GroupInfo
    {
        public Dictionary<char, int> AllAnswersWithCount { get; init; }

        public int MemberCount { get; init; }

        public int QuestionsWithAnAnswer
        {
            get
            {
                return AllAnswersWithCount.Keys.Count;
            }
        }

        public int QuestionsAnsweredByAll
        {
            get
            {
                return AllAnswersWithCount.Values.Where(x => x == MemberCount).Count();
            }
        }

        public GroupInfo(Dictionary<char, int> allAnswersWithCount, int memberCount) => (AllAnswersWithCount, MemberCount) = (allAnswersWithCount, memberCount);
    }
}
