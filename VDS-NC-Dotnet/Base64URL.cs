using System;

namespace VDS_NC_Dotnet
{
    //From https://jonlabelle.com/snippets/view/csharp/base64-url-encode-and-decode
    public static class Base64Url
    {
        public static string Encode(byte[] input)
        {
            var output = Convert.ToBase64String(input);
 
            output = output.Split('=')[0]; // Remove any trailing '='s
            output = output.Replace('+', '-'); // 62nd char of encoding
            output = output.Replace('/', '_'); // 63rd char of encoding
 
            return output;
        }
 
        public static byte[] Decode(string input)
        {
            var base64 = ConvertToBase64(input);
 
            var byteConversion = Convert.FromBase64String(base64); // Standard base64 decoder
 
            return byteConversion;
        }

        public static string ConvertToBase64(string input)
        {
            var output = input;
 
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding
 
            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0:
                    break; // No pad chars in this case
                case 2:
                    output += "==";
                    break; // Two pad chars
                case 3:
                    output += "=";
                    break; // One pad char
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(input), "Illegal base64url string!");
            }

            return output;
        }
    }
}
