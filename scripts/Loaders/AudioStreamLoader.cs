
using Godot;
using System;
using System.IO;

namespace TileBeat.scripts.Loaders
{
    internal class AudioStreamLoader
    {
        public static AudioStream LoadAudio(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            switch (Path.GetExtension(path)) 
            {
                case ".mp3":
                    AudioStreamMP3 asm = new AudioStreamMP3();
                    asm.Data = bytes;
                    return asm;
                case ".wav":
                    AudioStreamWav asw = new AudioStreamWav();
                    asw.Data = bytes;
                    return asw;
                default:
                    throw new FormatException("Audio loader support only Wav and Mp3");
            }
        }
    }
}
