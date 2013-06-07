using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.Source
{
    public enum RoomType
    {
        Flooded,
        Normal,
        Shop,
        Pit,
        FloodedSkull,
        Violent,
        Special
    }

    /// <summary>
    /// Provides a factory for creating rooms and there specific bounds
    /// </summary>
    static class RoomFactory
    {
        private static Texture2D ViolentRoom;
        private static Texture2D ViolentRoomv1;
        private static Texture2D ViolentRoomv2;
        private static Texture2D ViolentRoomv3;

        private static Texture2D FloodedRoom;
        private static Texture2D FloodedSkullRoom;

        private static Texture2D NormalRoom;
        private static Texture2D Normalv1Room;
        private static Texture2D Normalv2Room;
        private static Texture2D Normalv3Room;
        private static Texture2D Normalv4Room;
        private static Texture2D Normalv5Room;
        private static Texture2D Normalv6Room;
        private static Texture2D Normalv7Room;
        private static Texture2D Normalv8Room;
        private static Texture2D Normalv9Room;

        private static Texture2D ShopRoom;
        private static Texture2D PitRoom;

        private static Texture2D SpecialRoom;

        private static List<Vector2> BaseBounds;

        private static AnimatedTexture[] Walls;

        /// <summary>
        /// Initiates the boundaries 
        /// </summary>
        static RoomFactory()
        {
            #region Base Bounds
            RoomFactory.BaseBounds = new List<Vector2>()
            {
                new Vector2(437, 63),
                new Vector2(555, 70),
                new Vector2(882, 171),
                new Vector2(957, 399),

                new Vector2(882, 648),
                new Vector2(788, 713),

                new Vector2(601, 752),
                new Vector2(427, 751),

                new Vector2(218, 680),
                new Vector2(125, 603),
                new Vector2(190, 496),
                new Vector2(99, 369),

                new Vector2(190, 193),
                new Vector2(271, 35),

                new Vector2(317, 79),
            };
            #endregion
        }

        /// <summary>
        /// Downloades the specific images
        /// </summary>
        /// <param name="manager"></param>
        public static void InitFactory(ContentManager manager)
        {
            RoomFactory.FloodedRoom = manager.Load<Texture2D>("Textures\\Cave\\flooded");
            RoomFactory.FloodedSkullRoom = manager.Load<Texture2D>("Textures\\Cave\\flooded skull");

            RoomFactory.NormalRoom = manager.Load<Texture2D>("Textures\\Cave\\normal");
            RoomFactory.Normalv1Room = manager.Load<Texture2D>("Textures\\Cave\\normalv1");
            RoomFactory.Normalv2Room = manager.Load<Texture2D>("Textures\\Cave\\normalv2");
            RoomFactory.Normalv3Room = manager.Load<Texture2D>("Textures\\Cave\\normalv3");
            RoomFactory.Normalv4Room = manager.Load<Texture2D>("Textures\\Cave\\normalv4");
            RoomFactory.Normalv5Room = manager.Load<Texture2D>("Textures\\Cave\\normalv5");
            RoomFactory.Normalv6Room = manager.Load<Texture2D>("Textures\\Cave\\normalv6");
            RoomFactory.Normalv7Room = manager.Load<Texture2D>("Textures\\Cave\\normalv7");
            RoomFactory.Normalv8Room = manager.Load<Texture2D>("Textures\\Cave\\normalv8");
            RoomFactory.Normalv9Room = manager.Load<Texture2D>("Textures\\Cave\\normalv9");

            RoomFactory.ShopRoom = manager.Load<Texture2D>("Textures\\Cave\\shop");
            RoomFactory.PitRoom = manager.Load<Texture2D>("Textures\\Cave\\pit");

            RoomFactory.ViolentRoom = manager.Load<Texture2D>("Textures\\Cave\\Violent");
            RoomFactory.ViolentRoomv1 = manager.Load<Texture2D>("Textures\\Cave\\Violentv1");
            RoomFactory.ViolentRoomv2 = manager.Load<Texture2D>("Textures\\Cave\\Violentv2");
            RoomFactory.ViolentRoomv3 = manager.Load<Texture2D>("Textures\\Cave\\Violentv3");

            RoomFactory.SpecialRoom = manager.Load<Texture2D>("Textures\\Cave\\Special");

            RoomFactory.LoadWalls(manager);
        }
        /// <summary>
        /// Loads up the walls from the content manager
        /// </summary>
        /// <param name="content">Content Manager</param>
        private static void LoadWalls(ContentManager content)
        {
            RoomFactory.Walls = new AnimatedTexture[6];

            RoomFactory.Walls[0] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\n rock wallFULL"), 1, Helper.WallLayer);
            RoomFactory.Walls[1] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\ne rock wallFULL"), 1, Helper.WallLayer);
            RoomFactory.Walls[2] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\se rock wallFULL"), 1, Helper.WallLayer);
            RoomFactory.Walls[3] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\s rock wallFULL"), 1, Helper.WallLayer);
            RoomFactory.Walls[4] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\sw rock wallFULL"), 1, Helper.WallLayer);
            RoomFactory.Walls[5] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\nw rock wallFULL"), 1, Helper.WallLayer);
        }

        /// <summary>
        /// Creates a random type of room with its specific bounds
        /// </summary>
        /// <returns></returns>
        public static Room CreateRandomRoom(MainGame mainGame, Cave cave, int index)
        {
            double random = mainGame.Random.NextDouble();

            Tuple<Texture2D, List<Vector2>> tuple;
            RoomType type;

            if (random < 0.4)
            {
                tuple = new Tuple<Texture2D, List<Vector2>>(RoomFactory.GetRandomNormalRoom(), RoomFactory.BaseBounds);
                type = RoomType.Normal;
            }
            else if (random < 0.5)
            {
                tuple = GetRoomAndBound(RoomType.Flooded);
                type = RoomType.Flooded;
            }
            else if (random < 0.6)
            {
                tuple = GetRoomAndBound(RoomType.Shop);
                type = RoomType.Shop;
            }
            else if (random < 0.8)
            {
                tuple = GetRoomAndBound(RoomType.FloodedSkull);
                type = RoomType.FloodedSkull;
            }
            else if (random < 0.9)
            {
                tuple = GetRoomAndBound(RoomType.Violent);
                type = RoomType.Violent;
            }
            else
            {
                tuple = GetRoomAndBound(RoomType.Special);
                type = RoomType.Special;
            }

            Room room = new Room(mainGame, cave, index, tuple.Item1, tuple.Item2, type, Walls);
            return room;
        }
        /// <summary>
        /// Create a specific type room
        /// </summary>
        /// <param name="mainGame">parameters for room</param>
        /// <param name="cave">parameters for room</param>
        /// <param name="type">type of room</param>
        /// <param name="index">the rooms index</param>
        /// <returns>the wanted room</returns>
        public static Room Create(MainGame mainGame, Cave cave, RoomType type, int index)
        {
            double random = mainGame.Random.NextDouble();

            Tuple<Texture2D, List<Vector2>> tuple = GetRoomAndBound(type);

            Room room = new Room(mainGame, cave, index, tuple.Item1, tuple.Item2, type, Walls);
            return room;
        }

        /// <summary>
        /// Return the needed pair.
        /// </summary>
        /// <param name="type">Type of room: ENUM</param>
        /// <returns></returns>
        private static Tuple<Texture2D, List<Vector2>> GetRoomAndBound(RoomType type)
        {
            switch (type)
            {
                case RoomType.Flooded:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.FloodedRoom, RoomFactory.BaseBounds);
                case RoomType.Normal:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.GetRandomNormalRoom(), RoomFactory.BaseBounds);
                case RoomType.Shop:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.ShopRoom, RoomFactory.BaseBounds);
                case RoomType.Pit:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.PitRoom, RoomFactory.BaseBounds);
                case RoomType.FloodedSkull:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.FloodedRoom, RoomFactory.BaseBounds);
                case RoomType.Violent:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.GetRandomViolentRoom(), RoomFactory.BaseBounds);
                case RoomType.Special:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.SpecialRoom, RoomFactory.BaseBounds);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets a random variation of a all the violent rooms
        /// </summary>
        /// <returns>A texture of the random violent room</returns>
        private static Texture2D GetRandomViolentRoom()
        {
            Random randGen = new Random(DateTime.Now.Millisecond);
            double rand = randGen.NextDouble();

            if (rand < 0.25)
            {
                return RoomFactory.ViolentRoom;
            }
            else if (rand < 0.5)
            {
                return RoomFactory.ViolentRoomv1;
            }
            else if (rand < 0.75)
            {
                return RoomFactory.ViolentRoomv2;
            }
            else
            {
                return RoomFactory.ViolentRoomv3;
            }
        }
        /// <summary>
        /// Gets a random variation of a all the "Normal" rooms
        /// </summary>
        /// <returns>A texture of the random normal room</returns>
        private static Texture2D GetRandomNormalRoom()
        {
            Random randGen = new Random(DateTime.Now.Millisecond);
            double rand = randGen.NextDouble();

            if (rand < 0.10)
            {
                return RoomFactory.NormalRoom;
            }
            else if (rand < 0.20)
            {
                return RoomFactory.Normalv1Room;
            }
            else if (rand < 0.30)
            {
                return RoomFactory.Normalv2Room;
            }
            else if (rand < 0.40)
            {
                return RoomFactory.Normalv3Room;
            }
            else if (rand < 0.50)
            {
                return RoomFactory.Normalv4Room;
            }
            else if (rand < 0.60)
            {
                return RoomFactory.Normalv5Room;
            }
            else if (rand < 0.70)
            {
                return RoomFactory.Normalv6Room;
            }
            else if (rand < 0.80)
            {
                return RoomFactory.Normalv7Room;
            }
            else if (rand < 0.90)
            {
                return RoomFactory.Normalv8Room;
            }
            else
            {
                return RoomFactory.Normalv9Room;
            }
        }
    }
}
