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
        BloodRoom
    }

    /// <summary>
    /// Provides a factory for creating rooms and there specific bounds
    /// </summary>
    static class RoomFactory
    {
        private static Texture2D BloodSkullRoom;

        private static Texture2D FloodedRoom;
        private static Texture2D FloodedSkullRoom;

        private static Texture2D NormalRoom;
        private static Texture2D Normalv1Room;
        private static Texture2D Normalv2Room;
        private static Texture2D Normalv3Room;

        private static Texture2D ShopRoom;
        private static Texture2D PitRoom;

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
                new Vector2(432, 0),
                new Vector2(542, 0),
                new Vector2(583, 62),
                new Vector2(646, 104),
                new Vector2(721, 169),
                new Vector2(789, 182),
                new Vector2(834, 136),
                new Vector2(936, 133),
                new Vector2(940, 144),
                new Vector2(990, 211),
                new Vector2(900, 250),
                new Vector2(853, 296),
                new Vector2(924, 336),
                new Vector2(948, 410),
                new Vector2(915, 496),
                new Vector2(826, 533),
                new Vector2(904, 595),
                new Vector2(930, 678),
                new Vector2(873, 736),
                new Vector2(772, 703),
                new Vector2(760, 645),
                new Vector2(578, 764),
                new Vector2(456, 768),
                new Vector2(310, 639),
                new Vector2(223, 710),
                new Vector2(113, 689),
                new Vector2(123, 592),
                new Vector2(223, 519),
                new Vector2(100, 363),
                new Vector2(298, 199),
                new Vector2(157, 121),
                new Vector2(272, 22),
                new Vector2(365, 101),
                new Vector2(427, 5)
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

            RoomFactory.ShopRoom = manager.Load<Texture2D>("Textures\\Cave\\shop");
            RoomFactory.PitRoom = manager.Load<Texture2D>("Textures\\Cave\\pit");
            RoomFactory.BloodSkullRoom = manager.Load<Texture2D>("Textures\\Cave\\Blood Room");

            RoomFactory.LoadWalls(manager);
        }

        private static void LoadWalls(ContentManager content)
        {
            RoomFactory.Walls = new AnimatedTexture[6];

            RoomFactory.Walls[0] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\north"));
            RoomFactory.Walls[1] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\northeast"));
            RoomFactory.Walls[2] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\southeast"));
            RoomFactory.Walls[3] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\south"));
            RoomFactory.Walls[4] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\southwest"));
            RoomFactory.Walls[5] = new AnimatedTexture(content.Load<Texture2D>("Textures\\Walls\\northwest"));
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

            if (random < 0.3)
            {
                tuple = new Tuple<Texture2D, List<Vector2>>(RoomFactory.GetRandomNormalRoom(), RoomFactory.BaseBounds);
                type = RoomType.Normal;
            }
            else if (random < 0.4)
            {
                tuple = GetRoomAndBound(RoomType.Flooded);
                type = RoomType.Flooded;
            }
            else if (random < 0.8)
            {
                tuple = GetRoomAndBound(RoomType.Shop);
                type = RoomType.Shop;
            }
            else if (random < 0.9)
            {
                tuple = GetRoomAndBound(RoomType.FloodedSkull);
                type = RoomType.FloodedSkull;
            }
            else
            {
                tuple = GetRoomAndBound(RoomType.BloodRoom);
                type = RoomType.BloodRoom;
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
                case RoomType.BloodRoom:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.BloodSkullRoom, RoomFactory.BaseBounds);
                default:
                    return null;
            }
        }

        private static Texture2D GetRandomNormalRoom()
        {
            Random randGen = new Random(DateTime.Now.Millisecond);
            double rand = randGen.NextDouble();
            if (rand < 0.25)
            {
                return RoomFactory.NormalRoom;
            }
            else if (rand < 0.5)
            {
                return RoomFactory.Normalv1Room;
            }
            else if (rand < 0.75)
            {
                return RoomFactory.Normalv2Room;
            }
            else
            {
                return RoomFactory.Normalv3Room;
            }
        }
    }
}
