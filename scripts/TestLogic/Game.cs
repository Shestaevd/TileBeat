using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using TileBeat.scripts.testing;

public partial class Game : Node2D
{

    public int XSize = 10;
    public int YSize = 10;

    public Node2D[,] tiles;

    public string[,] tileNames;
    public Dictionary<string, Sprite2D> sprites;

    private Stater stater = new Stater();

    private void testlogic()
    {
        sprites = new Dictionary<string, Sprite2D>();
        var tilePS = GD.Load<PackedScene>("scenes/tile.tscn");

        var tile1 = (Sprite2D)tilePS.Instantiate();
        sprites.Add("t1", tile1);

        var tile2 = (Sprite2D)tilePS.Instantiate();
        tile2.Rotate(90);
        sprites.Add("t2", tile2);

        var tile3 = (Sprite2D)tilePS.Instantiate();
        tile3.Rotate(-90);
        sprites.Add("t3", tile3);

        tileNames = new string[10, 10] {
            { "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
            { "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
            { "t1","t1","t2","t2","t2","t2","t2","t2","t1","t1" },
            { "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
            { "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
            { "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
            { "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
            { "t1","t1","t3","t3","t3","t3","t3","t3","t1","t1" },
            { "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
            { "t1","t1","t1","t1","t1","t1","t1","t1","t1","t1" },
        };
    }

    public override void _Ready()
    {
        testlogic();

        var viewportsize = GetViewport().GetVisibleRect().Size;
        var maxPixelSize = Math.Min(viewportsize.X, viewportsize.Y);

        float NodeXSize = maxPixelSize / XSize;
        float NodeYSize = maxPixelSize / YSize;

        float XOffset = (viewportsize.X - maxPixelSize) / 2;
        float YOffset = (viewportsize.Y - maxPixelSize) / 2;

        foreach (KeyValuePair<string, Sprite2D> entry in sprites)
        {
            var tSize = entry.Value.Texture.GetSize();

            entry.Value.ApplyScale(
                new Vector2(
                    NodeXSize / tSize.X,
                    NodeYSize / tSize.Y
                )
            );
        }

        tiles = new Node2D[XSize, YSize];

        for (int i = 0; i < XSize; i++)
        {
            for (int j = 0; j < YSize; j++)
            {
                var tile = new Node2D();

                tile.Position = new Vector2(
                    XOffset + NodeXSize * i + NodeXSize / 2,
                    YOffset + NodeYSize * j + NodeYSize / 2
                );

                var sprite = (Sprite2D)sprites[tileNames[i, j]].Duplicate();
                tile.AddChild(sprite);

                AddChild(tile);

                tiles[i, j] = tile;
            }
        }

        var ticker = new Timer();
        ticker.OneShot = false;
        ticker.Autostart = false;
        ticker.Timeout += onTick;

        AddChild(ticker);
        ticker.Start(0.5);

    }

    private void onTick()
    {
        var steps = stater.nextSteps();

        if (steps != null)
        {
            foreach (Step step in steps)
            {
                var t = tiles[step.X, step.Y];

                t.RemoveChild(t.GetChild(0));

                var sprite = (Sprite2D)sprites[step.sprite].Duplicate();
                t.AddChild(sprite);
            }
        }
    }


}
