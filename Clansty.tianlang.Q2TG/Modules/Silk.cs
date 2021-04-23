using System.Diagnostics;
using System.IO;

namespace Clansty.tianlang
{
    public static class Silk
    {
        private const string decoder = "/home/clansty/silk-v3-decoder/silk/decoder";
        
        public static string decode(string path)
        {
            Process.Start(decoder, $"{path} {path}.pcm").WaitForExit();
            Process.Start("ffmpeg", 
                $"-f s16le -ar 24000 -ac 1 -i {path}.pcm -c:a libopus {path}.ogg").WaitForExit();
            File.Delete(path);
            File.Delete("{path}.pcm");
            return path + ".ogg";
        }
    }
}