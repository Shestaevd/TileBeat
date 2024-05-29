using Godot;
using System;
using TileBeat.scripts.Loaders;
using TileBeat.scripts.Managers.Beat;

namespace TileBeat.scripts.GameObjects
{
    public static class TrackBeatMappinig
    {
        public static GodotTrack PixelMurder = PixelMurderLoad();
        private static GodotTrack PixelMurderLoad()
        {   try
            {
                AudioStream audio = AudioStreamLoader.LoadFromFSAudio("E:\\GodotProjects\\TileBeat\\assets\\test\\PIXEL_MURDER.mp3");
                return new GodotTrack(
                    audio, 
                    120, 
                    BeatQueueAutoMaker.GenerateBeatsByBpm(
                        (float)audio.GetLength(), 
                        120, 
                        (uint)audio.GetLength() * 2
                        )
                    );
            } 
            catch (Exception e)
            {
                GD.PrintErr("Unable to load song: PixelMurder. " + e.Message);
                return null; 
            }
            
        }

    }
}
