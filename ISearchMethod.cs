namespace ArraySearch
{
    internal interface ISearchMethod
    {
        int Search(string[] sortedArray, string[] originalArray, string key);
    }
}