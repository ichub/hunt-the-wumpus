using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HuntTheWumpus.Source
{
    interface ICollideable : IGameObject
    {
        List<BoundingBox> BoundingBoxes { get; set; }
        Team ObjectTeam { get; set; }
        void CollideWith(ICollideable gameObject);
    }
}
