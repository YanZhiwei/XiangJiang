using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XiangJiang.Common
{
    public static class FileHelper
    {
        /// <summary>
        ///     计算文件的哈希值
        /// </summary>
        /// <param name="fs">被操作的源数据流</param>
        /// <param name="algo">HashAlgorithmName</param>
        /// <returns>哈希值16进制字符串</returns>
        private static string HashFile(Stream fs, HashAlgorithmName algo)
        {
            HashAlgorithm crypto;
            if (algo == HashAlgorithmName.SHA1)
                crypto = new SHA1CryptoServiceProvider();
            else if (algo == HashAlgorithmName.SHA256)
                crypto = new SHA256CryptoServiceProvider();
            else if (algo == HashAlgorithmName.MD5)
                crypto = new MD5CryptoServiceProvider();
            else if (algo == HashAlgorithmName.SHA512)
                crypto = new SHA512CryptoServiceProvider();
            else
                crypto = new MD5CryptoServiceProvider();

            var retVal = crypto.ComputeHash(fs);

            var builder = new StringBuilder();
            foreach (var t in retVal)
                builder.Append(t.ToString("x2"));
            return builder.ToString();
        }

        public static void SaveFile(this MemoryStream ms, string destFile)
        {
            using (var fs = new FileStream(destFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                var buffer = ms.ToArray();
                fs.Write(buffer, 0, buffer.Length);
                fs.Flush();
            }
        }

        public static string GetFileMd5(this FileStream fs)
        {
            return HashFile(fs, HashAlgorithmName.MD5);
        }

        public static string GetFileSha1(this Stream fs)
        {
            return HashFile(fs, HashAlgorithmName.SHA1);
        }
    }
}