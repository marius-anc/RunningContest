using System;

namespace RunningContest
{
    public struct Contestant
    {
        public string Name;
        public string Country;
        public double Time;

        public Contestant(string name, string country, double time)
        {
            this.Name = name;
            this.Country = country;
            this.Time = time;
        }
    }

    public struct ContestRanking
    {
        public Contestant[] Contestants;
    }

    public struct Contest
    {
        public ContestRanking[] Series;
        public ContestRanking GeneralRanking;
    }

    public class Program
    {
        public static void GenerateGeneralRanking(ref Contest contest)
        {
            contest.GeneralRanking.Contestants = new Contestant[0];
            foreach (ContestRanking serie in contest.Series)
            {
                int generalRankingLength = contest.GeneralRanking.Contestants.Length;
                Contestant[] temp = new Contestant[generalRankingLength + serie.Contestants.Length];
                contest.GeneralRanking.Contestants.CopyTo(temp, 0);
                serie.Contestants.CopyTo(temp, generalRankingLength);
                contest.GeneralRanking.Contestants = temp;
            }

            MergeSort(contest.GeneralRanking.Contestants, 0, contest.GeneralRanking.Contestants.Length - 1);
        }

        static void Main()
        {
            Contest contest = ReadContestSeries();
            GenerateGeneralRanking(ref contest);
            Print(contest.GeneralRanking);
            Console.Read();
        }

        private static void Print(ContestRanking contestRanking)
        {
            for (int i = 0; i < contestRanking.Contestants.Length; i++)
            {
                Contestant contestant = contestRanking.Contestants[i];
                const string line = "{0} - {1} - {2:F3}";
                Console.WriteLine(string.Format(line, contestant.Name, contestant.Country, contestant.Time));
            }
        }

        private static void MergeSort(Contestant[] allContestants, int left, int right)
            {
            if (left >= right)
            {
                return;
            }

            int middle = (left + right) / 2;
            MergeSort(allContestants, left, middle);
            MergeSort(allContestants, middle + 1, right);

            Merge(allContestants, left, middle, right);
        }

        private static void Merge(Contestant[] allContestants, int left, int middle, int right)
        {
            Contestant[] leftArray = new Contestant[middle - left + 1];
            Contestant[] rightArray = new Contestant[right - middle];

            Array.Copy(allContestants, left, leftArray, 0, middle - left + 1);
            Array.Copy(allContestants, middle + 1, rightArray, 0, right - middle);

            int i = 0;
            int j = 0;
            for (int k = left; k < right + 1; k++)
            {
                if (i == leftArray.Length)
                {
                    allContestants[k] = rightArray[j];
                    j++;
                    continue;
                }
                else if (j == rightArray.Length || leftArray[i].Time <= rightArray[j].Time)
                {
                    allContestants[k] = leftArray[i];
                    i++;
                    continue;
                }

                allContestants[k] = rightArray[j];
                j++;
            }
        }

        static Contest ReadContestSeries()
        {
            Contest contest = new Contest();

            int seriesNumber = Convert.ToInt32(Console.ReadLine());
            int contestantsPerSeries = Convert.ToInt32(Console.ReadLine());

            contest.Series = new ContestRanking[seriesNumber];

            for (int i = 0; i < seriesNumber; i++)
            {
                contest.Series[i].Contestants = new Contestant[contestantsPerSeries];
                for (int j = 0; j < contestantsPerSeries; j++)
                {
                    string contestantLine = "";

                    while (contestantLine == "")
                    {
                        contestantLine = Console.ReadLine();
                    }

                    contest.Series[i].Contestants[j] = CreateContestant(contestantLine.Split('-'));
                }
            }

            return contest;
        }

        private static Contestant CreateContestant(string[] contestantData)
        {
            const int nameIndex = 0;
            const int countryIndex = 1;
            const int timeIndex = 2;

            return new Contestant(
                contestantData[nameIndex].Trim(),
                contestantData[countryIndex].Trim(),
                Convert.ToDouble(contestantData[timeIndex]));
        }
    }
}
