using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleSignOn.Core.Services
{
    public interface ISimplePasswordHash
    {
        string Hash(string password);
    }

    public class SimplePasswordHash : ISimplePasswordHash
    {
        public string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "";
            }

#if DEBUG
            return password;
#else
            var bytes = new UTF8Encoding().GetBytes(password);
            byte[] hashBytes;
            using (var algorithm = new System.Security.Cryptography.SHA512Managed())
            {
                hashBytes = algorithm.ComputeHash(bytes);
            }
            return Convert.ToBase64String(hashBytes);
#endif


        }

    }
}
