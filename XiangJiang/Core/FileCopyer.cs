using System.IO;
using System.Threading.Tasks;

namespace XiangJiang.Core
{
    public sealed class FileCopyer
    {
        private readonly string _destFile;
        private readonly string _srcFile;

        public FileCopyer(string srcFile, string destFile, bool overrideDestFile = true)
        {
            _srcFile = srcFile;
            _destFile = destFile;
            Checker.Begin().CheckFileExists(srcFile).IsFilePath(destFile);
            if (overrideDestFile && File.Exists(_destFile))
                File.Delete(_destFile);
        }


        /// <summary>
        /// 以文件流的形式复制大文件
        /// </summary>
        /// <param name="bufferSize">缓冲区大小，默认8MB</param>
        public void Copy(int bufferSize = 1024 * 8 * 1024)
        {
            using (var srcStream = File.Open(_srcFile, FileMode.Open))
            {
                using (var destWrite = new FileStream(_destFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    var buffer = new byte[bufferSize];
                    int len;
                    while ((len = srcStream.Read(buffer, 0, buffer.Length)) != 0)
                        destWrite.Write(buffer, 0, len);
                }
            }
        }

        /// <summary>
        /// 以文件流的形式复制大文件(异步方式)
        /// </summary>
        /// <param name="bufferSize">缓冲区大小，默认8MB</param>
        public async void CopyAsync(int bufferSize = 1024 * 1024 * 8)
        {
            using (var srcStream = File.Open(_srcFile, FileMode.Open))
            {
                using (var fsWrite = new FileStream(_destFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    var buffer = new byte[bufferSize];
                    int len;
                    await Task.Run(() =>
                    {
                        while ((len = srcStream.Read(buffer, 0, buffer.Length)) != 0) 
                            fsWrite.Write(buffer, 0, len);
                    }).ConfigureAwait(true);
                }
            }
        }
    }
}