using System.ComponentModel;

namespace Theorem.MockNet.Http
{
    public interface IHeaderValue<T>
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        T GetValue();
    }
}