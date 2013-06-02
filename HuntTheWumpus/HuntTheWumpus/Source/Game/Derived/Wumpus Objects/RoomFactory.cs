using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HuntTheWumpus.Source.Game.Derived.Wumpus_Objects
{
    public enum RoomType
    {
        Flooded,
        Normal,
        RockSlide,
        ShopRoom
    }

    /// <summary>
    /// Provides a factory for creating rooms and there specific bounds
    /// </summary>
    static class RoomFactory
    {
        private static Texture2D FloodedRoom;
        private static Texture2D NormalRoom;
        private static Texture2D RockSlideRoom;
        private static Texture2D ShopRoom;

        private static List<Vector2> FloodedRoomBounds;
        private static List<Vector2> NormalRoomBounds;
        private static List<Vector2> RockSlideRoomBounds;
        private static List<Vector2> ShopRoomBounds;

        private static Random Random;

        /// <summary>
        /// Initiates the boundaries 
        /// </summary>
        static RoomFactory()
        {

            #region Normal Room Bounds
            RoomFactory.NormalRoomBounds = new List<Vector2>()
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
            #region Flooded Room Bounds
            RoomFactory.FloodedRoomBounds = new List<Vector2>()
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
            #region Rock Slide Room Bounds
            RoomFactory.RockSlideRoomBounds = new List<Vector2>()
            {
                new Vector2(213f,230f),
                new Vector2(305f,243f),
                new Vector2(373f,273f),
                new Vector2(439f,333f),
                new Vector2(508f,339f),
                new Vector2(548f,384f),
                new Vector2(552f,414f),
                new Vector2(525f,434f),
                new Vector2(496f,434f),
                new Vector2(485f,470f),
                new Vector2(440f,468f),
                new Vector2(399f,496f),
                new Vector2(260f,230f),
                new Vector2(177f,545f),
                new Vector2(51f,445f),
                new Vector2(18f,383f),
                new Vector2(20f,318f),
                new Vector2(209f,163f),
                new Vector2(178f,128f),
                new Vector2(279f,28f),
                new Vector2(350f,490f),
                new Vector2(442f,0f),
                new Vector2(553f,0f),
                new Vector2(725f,95f),
                new Vector2(760f,85f),
                new Vector2(802f,173f),
                new Vector2(873f,160f),
                //
                new Vector2(902f,193f),
                new Vector2(895f,247f),
                new Vector2(750f,386f),
                new Vector2(698f,385f),
                new Vector2(708f,433f),
                new Vector2(802f,452f),
                new Vector2(869f,449f),
                new Vector2(934f,425f),
                new Vector2(1008f,286f),
                new Vector2(1010f,450f),
                new Vector2(851f,598f),
                new Vector2(893f,658f),
                new Vector2(858f,696f),
                new Vector2(816f,662f),
                new Vector2(653f,767f),
                new Vector2(386f,767f),
                new Vector2(248f,643f),
                new Vector2(202f,671f),
                new Vector2(158f,628f),
                new Vector2(199f,597f),
                new Vector2(180f,531f)
            };
            #endregion
            #region Shop Room Bounds
            RoomFactory.ShopRoomBounds = new List<Vector2>()
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
                new Vector2(427, 5),
                //Break from outisde bondaries and start on shop
                new Vector2(-1,-1),
                new Vector2(418,271),
                new Vector2(622,272),
                new Vector2(629,341),
                new Vector2(658,346),
                new Vector2(661,401),
                new Vector2(642,417),
                new Vector2(620,402),
                new Vector2(424,400),
                new Vector2(418,271),
            };
            #endregion

            RoomFactory.Random = new Random();
        }

        /// <summary>
        /// Downloades the specific images
        /// </summary>
        /// <param name="manager"></param>
        public static void InitFactory(ContentManager manager)
        {
            RoomFactory.FloodedRoom = manager.Load<Texture2D>("Textures\\Cave\\flooded");
            RoomFactory.NormalRoom = manager.Load<Texture2D>("Textures\\Cave\\normal");
            RoomFactory.RockSlideRoom = manager.Load<Texture2D>("Textures\\Cave\\rockslide");
            RoomFactory.ShopRoom = manager.Load<Texture2D>("Textures\\Cave\\shop");
        }

        /// <summary>
        /// Creates a random type of room with its specific bounds
        /// </summary>
        /// <returns></returns>
        public static Room Create(MainGame mainGame, Cave cave, int index)
        {
            double random = RoomFactory.Random.NextDouble();

            Tuple<Texture2D, List<Vector2>> tuple;
            RoomType type;

            if (random < 0.4)
            {
                tuple = GetRoomAndBound(RoomType.Normal);
                type = RoomType.Normal;
            }
            else if (random < 0.6)
            {
                tuple = GetRoomAndBound(RoomType.RockSlide);
                type = RoomType.RockSlide;
            }
            else if (random < 0.9)
            {
                tuple = GetRoomAndBound(RoomType.Flooded);
                type = RoomType.Flooded;
            }
            else
            {
                tuple = GetRoomAndBound(RoomType.ShopRoom);
                type = RoomType.ShopRoom;
            }
            Room room = new Room(mainGame, cave, index, tuple.Item1, tuple.Item2, type);
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
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.FloodedRoom, RoomFactory.FloodedRoomBounds);
                case RoomType.Normal:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.NormalRoom, RoomFactory.NormalRoomBounds);
                case RoomType.RockSlide:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.RockSlideRoom, RoomFactory.RockSlideRoomBounds);
                case RoomType.ShopRoom:
                    return new Tuple<Texture2D, List<Vector2>>(RoomFactory.ShopRoom, RoomFactory.ShopRoomBounds);
                default:
                    return null;
            }
        }
    }
}
