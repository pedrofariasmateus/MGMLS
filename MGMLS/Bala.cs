using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGMLS
{
    public class Bala
    {
        //valores constantes das caracteristicas da bala
        const int DANO = 10;
        const int VELOCIDADE = 5;

        //variáveis da posição da bala no ecrã e se visivel
        int posX, posY;
        bool visivel, paraCima;

        //enumerador de quem pertence a bala
        Jogador balaPertence;

        //textura da bala
        Texture2D texturaBala;
        Rectangle drawBala;
        public CircleF shapeBala;

        public Bala(Texture2D textura, Jogador jogador, int posiX, int posiY, bool direccaoCima)
        {
            texturaBala = textura;
            balaPertence = jogador;
            posX = posiX;
            posY = posiY;
            paraCima = direccaoCima;
            drawBala = new Rectangle(posiX, posiY, texturaBala.Width, texturaBala.Height);
            shapeBala = new CircleF(new Point2(posiX + (texturaBala.Width / 2), posiY + (texturaBala.Height / 2)), texturaBala.Width / 2);
            visivel = true;
        }

        public void Update(GameTime gameTime)
        {
            if (paraCima == true)
            {
                posY -= VELOCIDADE;
                drawBala.Y -= VELOCIDADE;
                shapeBala.Center.Y -= VELOCIDADE;
            }
            if (paraCima == false)
            {
                posY += VELOCIDADE;
                drawBala.Y += VELOCIDADE;
                shapeBala.Center.Y += VELOCIDADE;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texturaBala, drawBala, Color.White);
        }

        public int X
        {
            get { return posX; }
        }

        public int Y
        {
            get { return posY; }
        }

        public bool Visivel
        {
            get { return visivel; }
            set { visivel = value; }
        }

        public int Dano
        {
            get { return DANO; }
        }

        public Jogador Pertence
        {
            get { return balaPertence; }
        }

        public Rectangle Forma
        {
            get { return drawBala; }
        }

        public Texture2D TexturaBala
        {
            get { return texturaBala; }
        }

        public bool CollidesWith(CircleF otherCircle) {
            if (this.shapeBala.Intersects(otherCircle))
                return true;
            else
                return false;
        }
    }
}
