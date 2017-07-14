using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MGMLS
{
    class Nave

    {
        //dados constantes da nave
        const int VIDA = 100;
        const int ESCUDO = 50;
        const int RAPIDEZ = 5;

        const int TIROS_POR_SEGUNDO = 5;
        const long TEMPO_TIROS = TimeSpan.TicksPerSecond / TIROS_POR_SEGUNDO;

        const int TEMPO_ARREFECIMENTO_ESCUDO_SEGUNDOS = 10;
        const long TEMPO_ARREFECIMENTO_ESCUDO = TEMPO_ARREFECIMENTO_ESCUDO_SEGUNDOS * TimeSpan.TicksPerSecond;

        const int SCREEN_WIDTH_CENTER = GameConstants.WINDOW_WIDTH / 2;

        const int PHASE1_SECONDS = 5;
        const long PHASE1_TIME = PHASE1_SECONDS * TimeSpan.TicksPerSecond;

        const int PHASE2_SECONDS = 30;
        const long PHASE2_TIME = PHASE2_SECONDS * TimeSpan.TicksPerSecond;

        const int PHASE3_SECONDS = 30;
        const long PHASE3_TIME = PHASE3_SECONDS * TimeSpan.TicksPerSecond;

        const int PHASE4_SECONDS = 30;
        const long PHASE4_TIME = PHASE4_SECONDS * TimeSpan.TicksPerSecond;

        const int PHASE5_SECONDS = 30;
        const long PHASE5_TIME = PHASE5_SECONDS * TimeSpan.TicksPerSecond;

        const int PHASE6_SECONDS = 30;
        const long PHASE6_TIME = PHASE6_SECONDS * TimeSpan.TicksPerSecond;

        const int PHASE7_SECONDS = 30;
        const long PHASE7_TIME = PHASE7_SECONDS * TimeSpan.TicksPerSecond;

        const int PHASE8_SECONDS = 600;
        const long PHASE8_TIME = PHASE8_SECONDS * TimeSpan.TicksPerSecond;


        //variaveis da nave
        int vidaActual, escudoActual, posicaoX, posicaoY;
        long tempoTiros, tempoEscudo, tempoAI;
        readonly int posNaveCentroX, posEscudoCentroX;
        bool escudoActivo, visivel, disparou, disable, direccaoParaCima, scriptedAI;
        Keys teclaEsquerda, teclaDireita, teclaDisparar, teclaActivarEscudo;
        Jogador jogador;
        AIPhase currentAIPhase;
        
        //SoundEffect explosao, disparo, reflectir;
        
        //texturas usadas pela a nave
        Texture2D texturaNave, texturaBala, texturaEscudo;

        //rectangulos de desenho
        Rectangle naveDrawRectangulo, escudoDrawRectangulo;

        //formas geometricas utilizadas para detectar colisoes
        CircleF naveShape, escudoShape;
        Point2 centerNave, centerEscudo;


        public Nave(ContentManager content, SoundEffect[] sound, int posicaoFixaY, Jogador j)
        {
            //carrega as texturas e sons
            LoadContent(content, sound);

            //inicializa os valores da nave
            vidaActual = VIDA;
            escudoActual = 0;
            posNaveCentroX = SCREEN_WIDTH_CENTER - texturaNave.Width / 2;
            posEscudoCentroX = SCREEN_WIDTH_CENTER - TexturaEscudo.Width / 2;
            posicaoX = posNaveCentroX;
            posicaoY = posicaoFixaY;
            escudoActivo = false;
            visivel = true;
            jogador = j;
            disparou = false;
            disable = false;
            tempoEscudo = 0;
            tempoTiros = 0;
            tempoAI = 0;


            //determina as teclas da nave conforme o jogador
            if (Jogador.Jogador1 == jogador) {
                teclaEsquerda = Keys.K;
                teclaDireita = Keys.L;
                teclaDisparar = Keys.N;
                teclaActivarEscudo = Keys.M;
                direccaoParaCima = false;
            }

            else if (Jogador.Jogador2 == jogador)
            {
                teclaEsquerda = Keys.Left;
                teclaDireita = Keys.Right;
                teclaDisparar = Keys.Space;
                teclaActivarEscudo = Keys.C;
                direccaoParaCima = true;
            }

            //cria um novo rectangulo de desenho para a nave e o escudo
            naveDrawRectangulo = new Rectangle(posNaveCentroX, posicaoY, texturaNave.Width, texturaNave.Height);
            escudoDrawRectangulo = new Rectangle(posEscudoCentroX, posicaoY - (texturaEscudo.Height - texturaNave.Height) / 2,
                texturaEscudo.Width, texturaEscudo.Height);

            centerNave = new Point2(posNaveCentroX + TexturaNave.Width / 2, posicaoY + TexturaNave.Height / 2);
            centerEscudo = new Point2(posEscudoCentroX + TexturaEscudo.Width / 2, posicaoX + TexturaEscudo.Height / 2);

            //cria novas figuras geometricas para detectar colisoes
            naveShape = new CircleF(centerNave, texturaNave.Width/2);
            escudoShape = new CircleF(centerEscudo, texturaEscudo.Width/2);

        }

        void Phase1(GameTime gametime) {
            //do nothing
            
        }

        void Phase2(GameTime gametime)
        {
            
        }

        void Phase3(GameTime gametime)
        {
            
        }

        void Phase4(GameTime gametime)
        {
            
        }
        void Phase5(GameTime gametime)
        {
            
        }

        void Phase6(GameTime gametime)
        {
            
        }

        void Phase7(GameTime gametime)
        {
            
        }

        void Phase8(GameTime gametime)
        {
            
        }



        public void Update(GameTime gametime, KeyboardState keyboard)
        {
            if (visivel || disable)
            {
                
                if (keyboard.IsKeyDown(teclaDireita) && posicaoX + naveDrawRectangulo.Width + RAPIDEZ <= GameConstants.WINDOW_WIDTH)
                {
                    posicaoX += RAPIDEZ;
                    naveDrawRectangulo.X += RAPIDEZ;
                    escudoDrawRectangulo.X += RAPIDEZ;
                    naveShape.Center.X += RAPIDEZ;
                    escudoShape.Center.X += RAPIDEZ;
                }
                
                if (keyboard.IsKeyDown(teclaEsquerda) && posicaoX - RAPIDEZ >= 0)

                {
                    posicaoX -= RAPIDEZ;
                    naveDrawRectangulo.X -= RAPIDEZ;
                    escudoDrawRectangulo.X -= RAPIDEZ;
                    naveShape.Center.X -= RAPIDEZ;
                    escudoShape.Center.X -= RAPIDEZ;
                }


                if (keyboard.IsKeyDown(teclaDisparar) && vidaActual > 0 && disparou == false)
                {
                    disparou = true;
                    if(direccaoParaCima)
                    GameMLS.AdicionarBala(new Bala(texturaBala, jogador, posicaoX + naveDrawRectangulo.Width / 2 - texturaBala.Width / 2,
                                                   posicaoY, direccaoParaCima));
                    else
                        GameMLS.AdicionarBala(new Bala(texturaBala, jogador, posicaoX + naveDrawRectangulo.Width / 2 - texturaBala.Width / 2,
                                                       posicaoY + naveDrawRectangulo.Height - texturaBala.Height, direccaoParaCima));
                }


                if (keyboard.IsKeyDown(teclaActivarEscudo) && tempoEscudo <= 0 && escudoActivo == false)
                {
                    escudoActivo = true;
                    escudoActual = ESCUDO;
                    tempoEscudo = TEMPO_ARREFECIMENTO_ESCUDO;
                }


                if (disparou == true)
                    tempoTiros += gametime.ElapsedGameTime.Ticks;


                if (tempoTiros >= TEMPO_TIROS)
                {
                    disparou = false;
                    tempoTiros = 0;
                }


                if (tempoEscudo > 0)
                    tempoEscudo -= gametime.ElapsedGameTime.Ticks;


                
            }//end if (visivel||disable)

        }//end Update()

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texturaNave, naveDrawRectangulo, Color.White);
            if (escudoActivo)
                spriteBatch.Draw(texturaEscudo, escudoDrawRectangulo, Color.White);
        }

        public bool Visivel
        {
            set { visivel = value; }
            get { return visivel; }

        }

        public int Vida
        {
            get { return vidaActual; }
            set { vidaActual = value; }
        }

        public int PercentagemVida
        {
            get { return (vidaActual * 100) / VIDA; }
        }

        public int PercentagemVidaBarra
        {
            get { return ((vidaActual * GameConstants.WINDOW_WIDTH) / VIDA); }
        }

        public int Escudo
        {
            get { return escudoActual; }
            set { escudoActual = value; }
        }

        public bool EscudoActivo
        {
            get { return escudoActivo; }
            set { escudoActivo = value; }
        }

        public int PercentagemEscudo
        {
            get { return (escudoActual * 100) / ESCUDO; }
        }

        public int PercentagemEscudoBarra
        {
            get { return ((escudoActual * GameConstants.WINDOW_WIDTH) / ESCUDO); }
        }

        public long TempoEscudo
        {
            get { return (tempoEscudo / TimeSpan.TicksPerSecond); }
        }

        public Rectangle Forma
        {
            get { return naveDrawRectangulo; }
        }

        public Rectangle EscudoForma
        {
            get { return escudoDrawRectangulo; }
        }

        public Texture2D TexturaNave
        {
            get { return texturaNave; }
        }

        public Texture2D TexturaEscudo
        {
            get { return texturaEscudo; }
        }

        public bool Desactivado
        {
            set { disable = value; }
        }

        public void Reset()
        {
            vidaActual = VIDA;
            escudoActual = 0;
            posicaoX = posNaveCentroX;
            naveDrawRectangulo.X = posNaveCentroX;
            escudoDrawRectangulo.X = posEscudoCentroX;
            naveShape.Center = centerNave;
            escudoShape.Center = centerEscudo;
            escudoActivo = false;
            visivel = true;
            disparou = false;
            tempoEscudo = 0;
            tempoTiros = 0;
            disable = false;
        }

        public bool EscudoCollidesWith(CircleF otherCircle) {
            if (this.escudoShape.Intersects(otherCircle))
                return true;
            else
                return false;
        }

        public bool NaveCollidesWith(CircleF otherCircle) {
            if (this.naveShape.Intersects(otherCircle))
                return true;
            else
                return false;
        }

        private void LoadContent(ContentManager content, SoundEffect[] sound)
        {
            //carrega as texturas
            texturaNave = content.Load<Texture2D>("img/nave");
            texturaEscudo = content.Load<Texture2D>("img/escudo");
            texturaBala = content.Load<Texture2D>("img/bala");

            //carrega os sons

        }

    }
}
