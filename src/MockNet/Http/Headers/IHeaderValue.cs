namespace MockNet.Http
{
    public interface IHeaderValue<T>
    {
        T GetValue();
    }
}