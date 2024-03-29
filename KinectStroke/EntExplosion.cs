﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MeepEngine
{
    public class EntExplosion
        : Entity
    {
        public EntExplosion(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            Enabled = true;
            Visible = true;
            
            // Set sprite parameters
            sprite = Assets.nosprite;
            imageAngle = 0f;
            imageScale = 1f;
            layer = 0.18f;
        }

        public override void Update(GameTime gameTime)
        {
            alpha -= 0.05f;
            angle = (float)(2 * Math.PI * Main.rand.NextDouble());

            if (alpha <= 0)
                Kill();

            base.Update(gameTime);
        }

        float angle;
        float scale = 1;
        float alpha = 1;

        public override void Create()
        {
            angle = (float)(2 * Math.PI * Main.rand.NextDouble());
            for (int i = 0; i < 5; i++)
                Main.InstanceCreate(new EntShrapnel(game, spriteBatch), pos);
        }

        public override void Destroy()
        {

        }

        public override void Draw(GameTime gameTime)
        {
            Main.DrawSprite(Assets.sprFlame, pos, angle, scale, layer, alpha);
            base.Draw(gameTime);
        }
    }
}