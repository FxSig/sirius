using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpiralLab.Sirius.FCEU
{
    /// <summary>
    /// 사용자 레벨
    /// </summary>
    public enum UserLevel
    {
        /// <summary>
        /// 오퍼레이터
        /// </summary>
        Operator=0,
        /// <summary>
        /// 메인터넌스
        /// </summary>
        Maint,
        /// <summary>
        /// 엔지니어
        /// </summary>
        Engineer,
        /// <summary>
        /// 개발자
        /// </summary>
        Developer,
    }

    /// <summary>
    /// 로그인 델리게이트
    /// </summary>
    /// <param name="level"></param>
    public delegate void LoggedIn(UserLevel level);

    /// <summary>
    /// 로그아웃 델리게이트
    /// </summary>
    public delegate void LoggedOut();

    /// <summary>
    /// 사용자 전역 객체
    /// </summary>
    public class User
    {
        /// <summary>
        /// 로그인 이벤트
        /// </summary>
        public static event LoggedIn OnLoggedIn;

        /// <summary>
        /// 로그아웃 이벤트
        /// </summary>
        public static event LoggedOut OnLoggedOut;

        /// <summary>
        /// 사용자 이름
        /// </summary>
        public static string Name { get; private set; }

        /// <summary>
        /// 로그인 여부
        /// </summary>
        public static bool IsLoggedIn { get; private set; }

        /// <summary>
        /// 사용자 레벨
        /// </summary>
        public static UserLevel Level 
        {
            get;
            private set;
        }

        private static string sharedKey = @"spirallab.sirius";
        private static string uacFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "uac.ini");

        /// <summary>
        /// 이전 상태 복원
        /// </summary>
        public static void Reload()
        {
            User.Level = UserLevel.Operator;
            User.Name = @"NoName";

            var oldName = new StringBuilder(255);
            var oldLevel = new StringBuilder(255);
            var isUdpBroadCast = new StringBuilder(255);
            var udpBroadCastPort = new StringBuilder(255);
            NativeMethods.GetPrivateProfileString("Current", "Name", string.Empty, oldName, 255, uacFileName);
            NativeMethods.GetPrivateProfileString("Current", "Level", string.Empty, oldLevel, 255, uacFileName);

            if (!string.IsNullOrEmpty(oldName.ToString()))
            {
                User.Name = oldName.ToString();
                User.IsLoggedIn = true;
            }
            if (!string.IsNullOrEmpty(oldLevel.ToString()))
            {
                User.Level = (UserLevel)int.Parse(oldLevel.ToString());
                User.IsLoggedIn = true;
            }
            if (System.Diagnostics.Debugger.IsAttached)
            {
                User.Name = "DEBUG";
                User.Level = UserLevel.Developer;
                User.IsLoggedIn = true;
            }
        }

        /// <summary>
        /// 로그인 시도
        /// </summary>
        /// <param name="name">사용자 이름</param>
        /// <param name="level">사용자 레벨</param>
        /// <param name="password">암호</param>
        /// <returns></returns>
        public static bool LogIn(string name, UserLevel level, string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;
            var configuredPassword = new StringBuilder(255);          
            NativeMethods.GetPrivateProfileString(level.ToString(), "Password", string.Empty, configuredPassword, 255, uacFileName);
            if (string.IsNullOrEmpty(configuredPassword.ToString()))
            {
                string encryptedInputedPassword = StringCipher.Encrypt(password, sharedKey);
                NativeMethods.WritePrivateProfileString(level.ToString(), "Password", encryptedInputedPassword, uacFileName);
                var mb = new MessageBoxOk();
                mb.ShowDialog("Password", $"Password key is not exist! so reset to current input password", 30);
            }
            else
            {
                string decryptedPassword = StringCipher.Decrypt(configuredPassword.ToString(), sharedKey); 
                if (!password.Equals(decryptedPassword))
                {
                    Logger.Log(Logger.Type.Error, $"fail to log in as {name} by {level.ToString()}");
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(name))
                User.Name = name;
            User.Level = level;
            if (User.IsLoggedIn)
                User.OnLoggedOut?.Invoke();
            User.OnLoggedIn?.Invoke(level);
            NativeMethods.WritePrivateProfileString("Old", "Name", User.Name, uacFileName);
            NativeMethods.WritePrivateProfileString("Old", "Level", $"{(int)User.Level}", uacFileName);
            Logger.Log(Logger.Type.Info, $"success to logged in as {name} by {level.ToString()}");
            return true;
        }

        /// <summary>
        /// 강제 로그인
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private static bool LogIn(string name, UserLevel level)
        {
            if (!string.IsNullOrEmpty(name))
                User.Name = name;
            User.Level = level;
            User.OnLoggedIn?.Invoke(level);
            NativeMethods.WritePrivateProfileString("Old", "Name", User.Name, uacFileName);
            NativeMethods.WritePrivateProfileString("Old", "Level", $"{(int)User.Level}", uacFileName);
            User.IsLoggedIn = true;
            Logger.Log(Logger.Type.Info, $"success to logged in as {name} by {level.ToString()}");
            return true;
        }

        /// <summary>
        /// 로그 아웃
        /// </summary>
        /// <returns></returns>
        public static bool LogOut()
        {
            User.Name = @"NoName";
            User.Level = UserLevel.Operator;
            User.OnLoggedOut?.Invoke();
            NativeMethods.WritePrivateProfileString("Old", "Name", User.Name, uacFileName);
            NativeMethods.WritePrivateProfileString("Old", "Level", $"{(int)User.Level}", uacFileName);
            User.IsLoggedIn = false;
            Logger.Log( Logger.Type.Info, $"success to logged out");
            return true;
        }
    }

    #region https://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp
    internal static class StringCipher
    {
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
    #endregion
}
