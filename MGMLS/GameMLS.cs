using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MGMLS
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GameMLS : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //objectos e texturas do jogo
        Nave nave1, nave2;
        String temporizador1, temporizador2;
        Texture2D texturaBarraVida, texturaBarraEscudo;
        Rectangle drawBarraVida1, drawBarraVida2, drawBarraEscudo1, drawBarraEscudo2, drawGameOver, drawButtonSim, drawButtonNao;
        static List<Bala> balas = new List<Bala>();
        SpriteFont font;
        Vector2 posVida1, posVida2, posEscudoTempo1, posEscudoTempo2, posPontos1, posPontos2;
        //CsvWriter writeDataSet;
        //CsvReader loadDataSet;
        //TextWriter bufferWriter;
        //TextReader bufferReader;

        //variaveis do jogo
        int vidaNave1, vidaNave2, escudoNave1, escudoNave2, pontosNave1, pontosNave2, numeroRondas;
        bool escudoActivoNave1, escudoActivoNave2, playerAI;

        //sons do jogo
        //ainda por fazer no fim

        public GameMLS()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //tamanho da janela
            graphics.PreferredBackBufferHeight = GameConstants.WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = GameConstants.WINDOW_WIDTH;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here



            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //carregar as texturas
            texturaBarraVida = Content.Load<Texture2D>("img/barra_hp");
            texturaBarraEscudo = Content.Load<Texture2D>("img/barra_escudo");

            //determinar se o jogador 1 trata-se de um scripted AI 
            playerAI = true;

            //criar as barras
            drawBarraVida1 = new Rectangle(0, 0, texturaBarraVida.Width, 20);
            drawBarraVida2 = new Rectangle(0, GameConstants.WINDOW_HEIGHT - 20, GameConstants.WINDOW_WIDTH, 20);
            drawBarraEscudo1 = new Rectangle(0, 0, GameConstants.WINDOW_WIDTH, 20);
            drawBarraEscudo2 = new Rectangle(0, GameConstants.WINDOW_HEIGHT - 20, GameConstants.WINDOW_WIDTH, 20);

            //criar as naves
            nave1 = new Nave(this.Content, null, drawBarraVida1.Height + 19, Jogador.Jogador1);
            escudoActivoNave1 = nave1.EscudoActivo;

            nave2 = new Nave(this.Content, null, GameConstants.WINDOW_HEIGHT - drawBarraVida2.Height - 
                            Content.Load<Texture2D>("img/nave").Height - 19, Jogador.Jogador2);
            escudoActivoNave2 = nave2.EscudoActivo;

            //carregar a letra dos temporizadores
            font = Content.Load<SpriteFont>("Arial");
            posVida1 = new Vector2(GameConstants.WINDOW_WIDTH / 2 - 10, 0);
            posVida2 = new Vector2(GameConstants.WINDOW_WIDTH / 2 - 10, GameConstants.WINDOW_HEIGHT - 20);
            posEscudoTempo1 = new Vector2(GameConstants.WINDOW_WIDTH - 20, 20);
            posEscudoTempo2 = new Vector2(10, GameConstants.WINDOW_HEIGHT - 40);
            posPontos1 = new Vector2(10, 20);
            posPontos2 = new Vector2(GameConstants.WINDOW_WIDTH - 20, GameConstants.WINDOW_HEIGHT - 40);
            numeroRondas = 5;

            //inicializar leitor de CSV e abrir CSV (se existir)

            //inicializar escritor de CSV 

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();

            // Allows the game to exit
            if (keyboard.IsKeyDown(Keys.Escape))
            {

                //guardar ou adicionar dados escritos no CSV

                //fechar jogo
                this.Exit();
            }


            // TODO: Add your update logic here

            //actualizar as naves
            nave1.Update(gameTime, keyboard);
            nave2.Update(gameTime, keyboard);

            //actualizar os temporizadores
            temporizador1 = nave1.TempoEscudo.ToString();
            temporizador2 = nave2.TempoEscudo.ToString();

            //actualizar as balas
            foreach (Bala bala in balas)
            {
                bala.Update(gameTime);
                if (bala.Y > GameConstants.WINDOW_HEIGHT || bala.Y < 0)
                    bala.Visivel = false;
            }

            foreach (Bala bala in balas)
            {
                if (nave1.Visivel) { 
                    if (nave1.EscudoActivo)
                    {
                        if (nave1.EscudoForma.Intersects(bala.Forma) && bala.Pertence == Jogador.Jogador2 &&
                            nave1.EscudoCollidesWith(bala.shapeBala))
                            //IntersectPixels(nave1.EscudoForma, nave1.TexturaEscudo, bala.Forma, bala.TexturaBala))
                        {
                            nave1.Escudo -= bala.Dano;
                            bala.Visivel = false;
                            drawBarraEscudo1.Width = nave1.PercentagemEscudoBarra;
                        }
                    }
                    else
                    {
                        if (nave1.Forma.Intersects(bala.Forma) && bala.Pertence == Jogador.Jogador2 &&
                            nave1.NaveCollidesWith(bala.shapeBala))
                        //IntersectPixels(nave1.Forma, nave1.TexturaNave, bala.Forma, bala.TexturaBala))
                        {
                            nave1.Vida -= bala.Dano;
                            bala.Visivel = false;
                            drawBarraVida1.Width = nave1.PercentagemVidaBarra;
                        }
                        
                    }
                }
                if (nave2.Visivel)
                {
                    if (nave2.EscudoActivo)
                    {
                        if (nave2.Forma.Intersects(bala.Forma) && bala.Pertence == Jogador.Jogador1 &&
                            nave2.EscudoCollidesWith(bala.shapeBala))
                        //IntersectPixels(nave2.EscudoForma, nave2.TexturaEscudo, bala.Forma, bala.TexturaBala))
                        {
                                nave2.Escudo -= bala.Dano;
                                bala.Visivel = false;
                                drawBarraEscudo2.Width = nave2.PercentagemEscudoBarra;
                        }
                    }
                    else
                    {
                        if (nave2.EscudoForma.Intersects(bala.Forma) && nave2.EscudoActivo && bala.Pertence == Jogador.Jogador1 &&
                            nave2.NaveCollidesWith(bala.shapeBala))
                            //IntersectPixels(nave2.Forma, nave2.TexturaNave, bala.Forma, bala.TexturaBala))
                        {
                            nave2.Vida -= bala.Dano;
                            bala.Visivel = false;
                            drawBarraVida2.Width = nave2.PercentagemVidaBarra;
                        }
                    }
                }
            }

            //limpar todas as balas "invisiveis"
            balas.RemoveAll(x => x.Visivel == false);


            //fazer as naves "invisiveis" quando morrem
            if (nave1.Vida <= 0)
            {
                nave1.Visivel = false;
            }
            if (nave2.Vida <= 0)
            {
                nave2.Visivel = false;
            }

            if (nave1.EscudoActivo && nave1.Escudo <= 0)
            {
                nave1.EscudoActivo = false;
                drawBarraEscudo1.Width = GameConstants.WINDOW_WIDTH;
            }

            if (nave2.EscudoActivo && nave2.Escudo <= 0)
            {
                nave2.EscudoActivo = false;
                drawBarraEscudo2.Width = GameConstants.WINDOW_WIDTH;
            }

            

            if (nave1.Visivel == false || nave2.Visivel == false)
            {
                nave1.Desactivado = true;
                nave2.Desactivado = true;

                if (nave1.Visivel == false)
                    pontosNave2++;

                else
                    pontosNave1++;

                limparBalas();

               
                //if (pontosNave1 == numeroRondas)
                //{
                //    return;
                //}

                //if (pontosNave2 == numeroRondas)
                //{
                //    return;
                //}

                nave1.Reset();
                nave2.Reset();
                drawBarraVida1.Width = GameConstants.WINDOW_WIDTH;
                drawBarraVida2.Width = GameConstants.WINDOW_WIDTH;

            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Aqua);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //desenhar as naves se estiverem visiveis/vivas
            if (nave1.Visivel == true)
                nave1.Draw(spriteBatch);
            if (nave2.Visivel == true)
                nave2.Draw(spriteBatch);

            //desenhar as balas
            foreach (Bala bala in balas)
                bala.Draw(spriteBatch);


            //desenhar as barras de hp e de escudo(se activo)
            spriteBatch.Draw(texturaBarraVida, drawBarraVida1, Color.White);
            spriteBatch.Draw(texturaBarraVida, drawBarraVida2, Color.White);

            //desenhar os valores de vida da nave

            if (nave1.EscudoActivo)
            {
                spriteBatch.Draw(texturaBarraEscudo, drawBarraEscudo1, Color.White);
                spriteBatch.DrawString(font, nave1.PercentagemEscudo.ToString(), posVida1, Color.Blue);
            }
            else
                spriteBatch.DrawString(font, nave1.PercentagemVida.ToString(), posVida1, Color.Green);

            if (nave2.EscudoActivo)
            {
                spriteBatch.Draw(texturaBarraEscudo, drawBarraEscudo2, Color.White);
                spriteBatch.DrawString(font, nave2.PercentagemEscudo.ToString(), posVida2, Color.Blue);
            }
            else
                spriteBatch.DrawString(font, nave2.PercentagemVida.ToString(), posVida2, Color.Green);

            //desenhar o tempo de arrefecimento do escudo
            spriteBatch.DrawString(font, temporizador1, posEscudoTempo1, Color.White);
            spriteBatch.DrawString(font, temporizador2, posEscudoTempo2, Color.White);

            //desenhar os pontos de cada nave
            spriteBatch.DrawString(font, pontosNave1.ToString(), posPontos1, Color.Black);
            spriteBatch.DrawString(font, pontosNave2.ToString(), posPontos2, Color.Black);

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public static void AdicionarBala(Bala bala)
        {
            balas.Add(bala);
        }

        public static void limparBalas()
        {
            balas.Clear();
        }


        ////algoritmo detector de colisao de imagens
        //static bool IntersectPixels(Rectangle rectangleA, Texture2D texA, Rectangle rectangleB, Texture2D texB)
        //{
        //    // Find the bounds of the rectangle intersection
        //    int x1 = Math.Max(rectangleA.X, rectangleB.X);
        //    int x2 = Math.Min(rectangleA.X + rectangleA.Width, rectangleB.X + rectangleB.Width);
        //    int y1 = Math.Max(rectangleA.Y, rectangleB.Y);
        //    int y2 = Math.Min(rectangleA.Y + rectangleA.Height, rectangleB.Y + rectangleB.Height);

        //    // Get Color data of each Texture
        //    Color[] dataA = new Color[texA.Width * texA.Height];
        //    texA.GetData(dataA);
        //    Color[] dataB = new Color[texB.Width * texB.Height];
        //    texB.GetData(dataB);

        //    // Check every point within the intersection bounds
        //    for (int y = y1; y < y2; y++)
        //    {
        //        for (int x = x1; x < x2; x++)
        //        {
        //            // Get the color of both pixels at this point
        //            Color colorA = dataA[(x - rectangleA.X) +
        //                                 (y - rectangleA.Y) * texA.Width];
        //            Color colorB = dataB[(x - rectangleB.X) +
        //                                 (y - rectangleB.Y) * texB.Width];

        //            // If both pixels are not completely transparent,
        //            if (colorA.A != 0 && colorB.A != 0)
        //            {
        //                // then an intersection has been found
        //                return true;
        //            }
        //        }
        //    }

        //    // No intersection found
        //    return false;
        //}



    }
}
