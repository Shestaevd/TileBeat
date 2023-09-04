using Godot;

namespace TileBeat.scripts.Loaders
{
    internal class SpriteLoader
    {
        public static Sprite2D LoadSprite(string path)
        {
            Image image = new Image();
            image.Load(path);
            Sprite2D sprite = new Sprite2D();
            ImageTexture texture = ImageTexture.CreateFromImage(image);
            sprite.Texture = texture;
            return sprite;
        }

        public static void LoadAnimation(AnimatedSprite2D animatedSprite, string animName, float frameDuration, string[] path) 
        {
            if (animatedSprite.SpriteFrames == null) animatedSprite.SpriteFrames = new SpriteFrames();
            animatedSprite.SpriteFrames.AddAnimation(animName);
            for (int i = 0; i < path.Length; i++) animatedSprite.SpriteFrames.AddFrame(animName, LoadSprite(path[i]).Texture, frameDuration);
        }
    }
}
