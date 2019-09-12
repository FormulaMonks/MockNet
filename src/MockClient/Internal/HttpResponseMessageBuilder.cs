using SystemHttpStatusCode = System.Net.HttpStatusCode;
using SystemHttpResponseMessage = System.Net.Http.HttpResponseMessage;
using SystemHttpContent = System.Net.Http.HttpContent;

namespace MockClient
{
    internal class HttpResponseMessageBuilder
    {
        private SystemHttpStatusCode code = SystemHttpStatusCode.OK;
        private HttpResponseHeaders headers;
        private IContent content;

        public HttpResponseMessageBuilder WithStatusCode(int code)
        {
            this.code = (SystemHttpStatusCode)code;

            return this;
        }

        public HttpResponseMessageBuilder WithHeaders(HttpResponseHeaders headers)
        {
            this.headers = headers;

            return this;
        }

        public HttpResponseMessageBuilder WithContent(IContent content)
        {
            this.content = content;

            return this;
        }

        public SystemHttpResponseMessage Build()
        {
            var message = new SystemHttpResponseMessage(code);

            if (content is IContent)
            {
                message.Content = content.ToHttpContent();
            }

            if (headers is HttpResponseHeaders)
            {
                foreach (var header in headers)
                {
                    if (!message.Headers.TryAddWithoutValidation(header.Key, header.Value) && message.Content is SystemHttpContent)
                    {
                        message.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value);
                    }
                }
            }

            return message;
        }
    }
}