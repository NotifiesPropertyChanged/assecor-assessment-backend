namespace DotNetApiChallenge.Contracts
{
    public interface IDataAdapter<T>
    {
        void SetDataSource(string source);
        List<T> GetData();
    }
}
