using AlperAslanApps.Core;
using RowinPt.Domain;
using System;
using System.IO;
using System.Text;

namespace RowinPt.Business.Services
{
    public class UserTokenProvider : ITokenProvider<UserModel>
    {
        public byte[] Generate(UserModel source, string purpose)
        {
            var ms = new MemoryStream();

            var userId = source.Id.ToString();
            var stamp = source.SecurityStamp.ToString();

            using (var writer = ms.CreateWriter())
            {
                writer.Write(DateTimeOffset.UtcNow);
                writer.Write(userId);
                writer.Write(purpose);
                writer.Write(stamp);
            }

            return ms.ToArray();
        }

        public bool Validate(byte[] data, UserModel source, string purpose)
        {
            var ms = new MemoryStream(data);

            using (var reader = ms.CreateReader())
            {
                var creationTime = reader.ReadDateTimeOffset();
                var expirationTime = creationTime + TimeSpan.FromDays(7);
                if (expirationTime < DateTimeOffset.UtcNow)
                {
                    return false;
                }

                var userId = reader.ReadString();
                var actualUserId = source.Id.ToString();
                if (userId != actualUserId)
                {
                    return false;
                }
                var purp = reader.ReadString();
                if (!string.Equals(purp, purpose))
                {
                    return false;
                }
                var stamp = reader.ReadString();
                if (reader.PeekChar() != -1)
                {
                    return false;
                }

                return stamp == source.SecurityStamp.ToString();
            }
        }
    }

    internal static class StreamExtensions
    {
        internal static readonly Encoding DefaultEncoding = new UTF8Encoding(false, true);

        public static BinaryReader CreateReader(this Stream stream)
        {
            return new BinaryReader(stream, DefaultEncoding, true);
        }

        public static BinaryWriter CreateWriter(this Stream stream)
        {
            return new BinaryWriter(stream, DefaultEncoding, true);
        }

        public static DateTimeOffset ReadDateTimeOffset(this BinaryReader reader)
        {
            return new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);
        }

        public static void Write(this BinaryWriter writer, DateTimeOffset value)
        {
            writer.Write(value.UtcTicks);
        }
    }
}
