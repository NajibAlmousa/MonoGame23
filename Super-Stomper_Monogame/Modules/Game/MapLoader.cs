using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Xna.Framework.Content;
using Super_Stomper_Monogame.Modules.BaseClasses;
using SharpDX.Direct2D1;

namespace Super_Stomper_Monogame.Modules.Game
{
    internal class MapLoader
    {
        public List<Coins> coins;
        public List<Tile> tiles;
        public List<Hitbox> colliders;
        public List<IEnemy> enemies;
        public MyHero myHero;
        public VictoryFlag victoryFlag;
        public int levelMaxWidth;
      

        public MapLoader(ContentManager content, int level)
        {
            tiles = new List<Tile>();
            colliders = new List<Hitbox>();
            enemies = new List<IEnemy>();
            coins = new List<Coins>();
            myHero = null;
            victoryFlag = null;
            levelMaxWidth = 0;

            string debugDirectory = Environment.CurrentDirectory;
            string levelDirectory = debugDirectory + @"\Content\Levels";
            string jsonFileName = @"Level " + level.ToString() + ".json";

            string jsonFilePath = Path.Combine(levelDirectory, jsonFileName);




            string jsonText = File.ReadAllText(jsonFilePath);
            Console.WriteLine(jsonText);

            Root levelData = JsonConvert.DeserializeObject<Root>(jsonText);
            levelMaxWidth = levelData.width;


            Texture2D tileset = content.Load<Texture2D>(@"Spritesheets\Environment\OverWorld1");


            for (int i = 0; i < levelData.layers.Count; i++)
            {
                Layer layer = levelData.layers[i];

                if (layer.name == "Entities")
                {
                    for (int j = 0; j < layer.entities.Count; j++)
                    {
                        Entity entity = layer.entities[j];
                        switch (entity.name)
                        {
                        
                            case "MyHero":
                                myHero = new MyHero(content, new Vector2(entity.x, entity.y));
                                break;
                            case "Collider":
                                colliders.Add(new Hitbox(new Rectangle(entity.x, entity.y, (int)entity.width, (int)entity.height), Vector2.Zero));
                                break;
                            case "Martian":
                                enemies.Add(new Martian(content, new Vector2(entity.x, entity.y)));
                                break;
                            case "Coin":
                                coins.Add(new Coins(content, new Vector2(entity.x, entity.y)));
                                break;
                            case "flag":
                                victoryFlag = new VictoryFlag(content, new Vector2(entity.x, entity.y));
                                break;
                        }

                    }
                }
                else if (layer.name == "Environment")
                {
                    List<int> data = levelData.layers[i].data;
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (data[j] == -1)
                            continue;
                        tiles.Add(new Tile(tileset, new Rectangle((data[j] * Tile.tileWidth) % tileset.Width, ((data[j] * Tile.tileWidth) / tileset.Width) * Tile.tileHeight, Tile.tileWidth, Tile.tileHeight), new Vector2((j * levelData.layers[i].gridCellWidth) % levelData.width, ((j * levelData.layers[i].gridCellWidth) / levelData.width) * levelData.layers[i].gridCellHeight)));
                    }
                }
            }
        }


        public class Entity
        {
            public string name { get; set; }
            public int id { get; set; }
            public string _eid { get; set; }
            public int x { get; set; }
            public int y { get; set; }
            public int originX { get; set; }
            public int originY { get; set; }
            public int? width { get; set; }
            public int? height { get; set; }
        }
        public class Layer
        {
            public string name { get; set; }
            public string _eid { get; set; }
            public int offsetX { get; set; }
            public int offsetY { get; set; }
            public int gridCellWidth { get; set; }
            public int gridCellHeight { get; set; }
            public int gridCellsX { get; set; }
            public int gridCellsY { get; set; }
            public List<Entity> entities { get; set; }
            public string tileset { get; set; }
            public List<int> data { get; set; }
            public int? exportMode { get; set; }
            public int? arrayMode { get; set; }
        }

        public class Root
        {
            public string ogmoVersion { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public int offsetX { get; set; }
            public int offsetY { get; set; }
            public List<Layer> layers { get; set; }
        }
    }

}