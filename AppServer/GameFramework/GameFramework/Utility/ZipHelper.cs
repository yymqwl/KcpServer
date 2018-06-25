using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameFramework
{
    public class ZipHelper
    {

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="source">源目录</param>
        /// <param name="s">ZipOutputStream对象</param>
        public static void Compress(string source, ZipOutputStream s)
        {
            string[] filenames = Directory.GetFileSystemEntries(source);
            foreach (string file in filenames)
            {
                if (Directory.Exists(file))
                {
                    // 递归压缩子文件夹
                    Compress(file, s);
                }
                else
                {
                    using (FileStream fs = File.OpenRead(file))
                    {
                        byte[] buffer = new byte[4 * 1024];
                        // 此处去掉盘符，如D:\123\1.txt 去掉D:
                        ZipEntry entry = new ZipEntry(file.Replace(Path.GetPathRoot(file), ""));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        int sourceBytes;
                        do
                        {
                            sourceBytes = fs.Read(buffer, 0, buffer.Length);
                            s.Write(buffer, 0, sourceBytes);
                        } while (sourceBytes > 0);
                    }
                }
            }
        }

        /// <summary>
        /// 解压缩
        /// </summary>
        /// <param name="sourceFile">压缩包完整路径地址</param>
        /// <param name="targetPath">解压路径是哪里</param>
        /// <returns></returns>
        public static bool Decompress(string sourceFile, string targetPath)
        {
            if (!File.Exists(sourceFile))
            {
                throw new FileNotFoundException(string.Format("未能找到文件 '{0}' ", sourceFile));
            }

            if (Directory.Exists(targetPath))
            {
                Directory.Delete(targetPath);
            }
            Directory.CreateDirectory(targetPath);

            using (var s = new ZipInputStream(File.OpenRead(sourceFile)))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.IsDirectory)
                    {
                        continue;
                    }
                    string directorName = Path.Combine(targetPath, Path.GetDirectoryName(theEntry.Name));
                    string fileName = Path.Combine(directorName, Path.GetFileName(theEntry.Name));
                    if (!Directory.Exists(directorName))
                    {
                        Directory.CreateDirectory(directorName);
                    }
                    if (!String.IsNullOrEmpty(fileName))
                    {
                        using (FileStream streamWriter = File.Create(fileName))
                        {
                            int size = 4096;
                            byte[] data = new byte[size];
                            while (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                                size = s.Read(data, 0, data.Length);
                            }
                        }
                    }
                }
            }
            return true;
        }

    }

}