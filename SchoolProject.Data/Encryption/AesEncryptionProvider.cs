using SoftFluent.EntityFrameworkCore.DataEncryption;
using SoftFluent.EntityFrameworkCore.DataEncryption.Providers;

namespace SchoolProject.Data.Encryption
{


    public class AesEncryptionProvider : IEncryptionProvider
    {
        private readonly AesProvider _aesProvider;

        public AesEncryptionProvider(byte[] key, byte[] iv)
        {
            _aesProvider = new AesProvider(key, iv);
        }

        public byte[] Encrypt(byte[] input)
        {
            return _aesProvider.Encrypt(input);
        }

        public byte[] Decrypt(byte[] input)
        {
            return _aesProvider.Decrypt(input);
        }
    }

}
