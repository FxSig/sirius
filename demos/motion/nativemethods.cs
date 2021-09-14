using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace SpiralLab.Sirius
{
    /// <summary>
    /// P/Invoke 용 네이티브 코드 집합
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    public static class NativeMethods
    {
        /// <summary>
        /// Ini 파일에서 읽기
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        internal static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public static T ReadIni<T>(string fileName, string section, string key)
        {
            const int size = 255;
            var sb = new StringBuilder(size);
            GetPrivateProfileString(section, key, string.Empty, sb, size, fileName);
            if (string.IsNullOrEmpty(sb.ToString()))
                return default(T);
            return (T) Convert.ChangeType(sb.ToString(), typeof(T));
        }

        /// <summary>
        /// Ini 파일에 쓰기
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        public static extern long WritePrivateProfileString(string Section, string key, string val, string filePath);

        internal static void WriteIni<T>(string fileName, string section, string key, T value)
        {
            WritePrivateProfileString(section, key, value.ToString(), fileName);
        }
    }
}
