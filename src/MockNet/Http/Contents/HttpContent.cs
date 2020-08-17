using SystemHttpContent = System.Net.Http.HttpContent;

namespace Theorem.MockNet.Http
{
    public abstract class HttpContent
    {
        private HttpContentHeaders headers;

        public HttpContentHeaders Headers => headers ??= new HttpContentHeaders();

        protected abstract SystemHttpContent ToSystemHttpContent();

        public SystemHttpContent ToHttpContent()
        {
            var content = ToSystemHttpContent();
            foreach (var contentHeader in Headers)
            {
                content.Headers.Add(contentHeader.Key, contentHeader.Value);
            }

            return content;
        }
    }
}