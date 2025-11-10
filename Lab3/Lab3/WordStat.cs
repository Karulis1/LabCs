public class WordStat
{
    public int Count { get; set; }
    public SortedSet<int> Lines { get; set; }

    public WordStat()
    {
        Count = 0;
        Lines = new SortedSet<int>();
    }
}