using System.ComponentModel;

namespace MockNet.Http
{
    public interface IHeaderValue<T>
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        T GetValue();
    }
}