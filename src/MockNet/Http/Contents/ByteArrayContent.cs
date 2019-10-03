using System;
using System.Collections;
using System.Collections.Generic;
using SystemHttpContent = System.Net.Http.HttpContent;
using SystemByteArrayContent = System.Net.Http.ByteArrayContent;

namespace MockNet.Http
{
    public class ByteArrayContent : IHttpContent
    {
        private readonly byte[] content;
        private readonly int offset;
        private readonly int count;

        public ByteArrayContent(byte[] content)
        {
            this.content = content;
        }

        public ByteArrayContent(byte[] content, int offset, int count)
        {
            this.content = content;
            this.offset = offset;
            this.count = count;
        }

        public SystemHttpContent ToHttpContent() => new SystemByteArrayContent(content, offset, count);

        #region Overrides
        public override string ToString()
        {
            return content.ToString();
        }

        public override int GetHashCode()
        {
            return content.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (obj is byte[] b)
            {
                return this == b;
            }

            {
                if (obj is ByteArrayContent content)
                {
                    return this == content.content;
                }
            }

            {
                if (obj is SystemByteArrayContent content)
                {
                    var bytes = content.ReadAsByteArrayAsync().GetAwaiter().GetResult();

                    return this == bytes;
                }
            }

            return false;
        }
        #endregion

        public static implicit operator byte[](ByteArrayContent content) => content.content;
        public static implicit operator ByteArrayContent(byte[] content) => new ByteArrayContent(content);
        public static implicit operator SystemByteArrayContent(ByteArrayContent content) => content.ToHttpContent() as SystemByteArrayContent;

        public static bool operator ==(ByteArrayContent content, byte[] bytes)
        {
            // TODO: implement byte array comparison.
            // return MemoryCompare.Compare(content.content, 0, bytes, 0, 0);
            return false;
        }

        public static bool operator !=(ByteArrayContent content, byte[] bytes) => !(content == bytes);

        public static bool operator ==(byte[] bytes, ByteArrayContent content) => (content == bytes);

        public static bool operator !=(byte[] bytes, ByteArrayContent content)  => !(content == bytes);

        public static bool operator ==(ByteArrayContent b1, ByteArrayContent b2) => (b1 == b2.content);

        public static bool operator !=(ByteArrayContent b1, ByteArrayContent b2)  => !(b1 == b2.content);
    }
}