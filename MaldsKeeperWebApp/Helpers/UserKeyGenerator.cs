using System.Text;

namespace MaldsKeeperWebApp.Helpers
{
    public class UserKeyGenerator
    {
        private static readonly char[] chars =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

        public static string GetKey(int size = 20)
        {
            var stringBuilder = new StringBuilder(size);
            var random = new Random();

            for (int i = 0; i < size; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }
    }
}
