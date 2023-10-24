using Autofac.Core.Lifetime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Luhe.Content.UI;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.BitmapFonts;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework.Audio;
using System.Text;
using System.Linq;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using static System.Net.WebRequestMethods;

namespace Luhe.Content
{
    public class BatalhaTrivia : Jogo
    {
        public int Phase = 0;
        public static List<string> PerguntasDisponíveis;

        public string PerguntaAtual = "";

        public string[] Respostas;

        public DifficultySelector[] Portugues;
        public DifficultySelector[] Matematica;
        public DifficultySelector[] Geografia;
        public DifficultySelector[] Historia;
        public DifficultySelector[] Fisica;
        public DifficultySelector[] Quimica;
        public DifficultySelector[] Biologia;
        public DifficultySelector[] Literatura;
        public DifficultySelector[] Ingles;
        public DifficultySelector[] FiloSocio;

        public Texture2D KeyTexture;

        public int TempoAteAlternativas;
        public string RespostaCerta;
        public int TempoRestanteReal;
        public int TempoRestanteSegundos;
        public int PerguntaARemover;
        public string[] PerguntaTotal;

        public int P1Pontos;
        public int P2Pontos;
        public int P1PontosAdicionar;
        public int P2PontosAdicionar;
        public bool P1Respondeu;
        public bool P2Respondeu;
        public Color P1Color = Color.White;
        public Color P2Color = Color.White;

        public int QuantidadeDePerguntas = 10;
        public BatalhaTrivia(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, SpriteFont font) : base(graphics, spriteBatch, font)
        {
            Tipo = 2;
            BackgroundColor = Color.DarkGreen;
        }

        public override void Initialize()
        {
            base.Initialize();
            Phase = 0;
            P1Pontos = 0;
            P2Pontos = 0;
            Respostas = new string[4] { "", "", "", "" };
            PerguntasDisponíveis = new List<string>();

            KeyTexture = Main.LoadedTextures["KeyTexture"];

            Portugues = new DifficultySelector[4];
            for (int i = 0; i < 4; i++)
            {
                Portugues[i] = new DifficultySelector(Main.LoadedTextures["DS"], (i + 1).ToString(), Font)
                {
                    Color = Color.DarkRed,
                    TrueColor = Color.DarkRed,
                    Position = new Vector2((int)(WidthOriginal * 0.15f) + 85 + 60 * i, (int)((120) / 2f) - 15),
                };
                //Portugues0.OnClick += DifficultyClicked(Portugues0);
                UIElements.Add(Portugues[i]);
            }
            Portugues[0].Questions = new List<string>() { "Qual palavra rima com boneca?*Sapeca*Dourada*Canário*Figurinha" };
            Portugues[1].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Qual das seguintes frases podem ser consideradas como frases nominais?\n\nI.Ó, compadre!\nII.Fecha a janela, compadre!\nIII.Ó, compadre, abra a porta!", 1200, Font)) + "*I, apenas*II, apenas*III, apenas*I e II, apenas" };
            Portugues[2].Questions = new List<string>() { "A abolição da escravidão no Brasil foi um evento ___republicano.*ante*anti*antu*anto" };
            Portugues[3].Questions = new List<string>() { string.Join('\n', Helper.SplitString("\"Quanto à inveja, pregou friamente que era _A_ virtuda principal, origem de prosperidades infinitas; virtude preciosa, que chegava _A_ suprir todas as outras, e ao próprio talento.\"", 1200, Font)) + "\n\nOs termos em destaque são, respesctivamente:*Um artigo e uma preposição*Um pronome e um artigo*Um conjunção e um artigo*Um pronome e uma preposição" };

            Matematica = new DifficultySelector[4];
            for (int i = 0; i < 4; i++)
            {
                Matematica[i] = new DifficultySelector(Main.LoadedTextures["DS"], (i + 1).ToString(), Font)
                {
                    Color = Color.Blue,
                    TrueColor = Color.Blue,
                    Position = new Vector2((int)(WidthOriginal * 0.15f) + 85 + 60 * i, (int)((120) / 2f) - 15 + 60),
                };
                UIElements.Add(Matematica[i]);
            }
            Matematica[0].Questions = new List<string>() { "3 canudinhos = 1 triângulo\n5 canudinhos = 2 triângulos\n7 canudinhos = 3 triângulos\n\nQuantos canudinhos são necessários para formar 5 triângulos?*11*7*9*13" };
            Matematica[1].Questions = new List<string>() { "Na sequência {0, 1, 2, 3, 4, 5, 6, 7}, qual é o menor número par?*0*1*2*4" };
            Matematica[2].Questions = new List<string>() { string.Join('\n', Helper.SplitString("João comprou diversos materiais escolares, a quantidade de lapiseiras adicionada à quantidade de canetas totaliza 12 produtos, enquanto o produto entre a quantidade de canetas e de lapiseiras resulta em 35.", 1200, Font)) + "\n\nQuanto de cada material João comprou?*7 canetas e 5 lapiseiras*6 canetas e 6 lapiseiras*3 canetas e 9 lapiseiras*8 canetas e 4 lapiseiras" };
            Matematica[3].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Qual a probabilidade de quando você lançar dois dados justos de seis lados, ambos caírem com um 6 virado para cima?", 1200, Font)) + "*1 em 36*1 em 12* 1 em 32*1 em 16" };

            Geografia = new DifficultySelector[4];
            for (int i = 0; i < 4; i++)
            {
                Geografia[i] = new DifficultySelector(Main.LoadedTextures["DS"], (i + 1).ToString(), Font)
                {
                    Color = Color.Green,
                    TrueColor = Color.Green,
                    Position = new Vector2((int)(WidthOriginal * 0.15f) + 85 + 60 * i, (int)((120) / 2f) - 15 + 120),
                };
                UIElements.Add(Geografia[i]);
            }
            Geografia[0].Questions = new List<string>() { string.Join('\n', Helper.SplitString("As populações tradicionais do Brasil têm um modo de transmissão de conhecimentos particular. Qual alternativa descreve corretamente essa transmissão?", 1200, Font)) + "*O conhecimento é passado de pais para filhos, na forma oral*O conhecimento é passado por meio de livros, na forma escrita*O conhecimento é passado pela Igreja, na forma escrita*O conhecimento é passado de homens para mulheres, na forma oral" };
            Geografia[1].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Se, em um mapa com escala de 1:50000, a distância entre dois pontos é de três centímetros, qual é a distância real entre esses dois pontos?", 1200, Font)) + "*1,5 quilômetro*20 quilômetros*3 quilômetros*4,5 quilômetros" };
            Geografia[2].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Com a modernização da produção em vários setores econômicos, muitos postos de trabalho são eliminados, pois:", 1200, Font)) + "*A mão de obra é substituída por robôs ou sistemas informatizados*A condição do trabalhador ficou mais precária com o avanço desse processo*A revolução técnico-científica dispensa mão de obra profissional*O trabalho é modernizado, mas apenas nas montadoras de veículos" };
            Geografia[3].Questions = new List<string>() { string.Join('\n', Helper.SplitString("A Conferência de Estocolmo, realizada em 1972, é um marco importante para a questão ambiental. Em diversos países, essa conferência estimulou:", 1200, Font)) + "*A criação de órgãos de defesa do meio ambiente*A fundação de ONGs e a estatização de empresas poluidoras*A transferência de áreas ricas em espécies nativas à ONU*A criação de áreas de preservação permanente com visitas taxadas" };

            Historia = new DifficultySelector[4];
            for (int i = 0; i < 4; i++)
            {
                Historia[i] = new DifficultySelector(Main.LoadedTextures["DS"], (i + 1).ToString(), Font)
                {
                    Color = Color.Gold,
                    TrueColor = Color.Gold,
                    Position = new Vector2((int)(WidthOriginal * 0.15f) + 85 + 60 * i, (int)((120) / 2f) - 15 + 180),
                };
                UIElements.Add(Historia[i]);
            }
            Historia[0].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Espaço público é aquele de uso comum e que pertence a todos. O espaço doméstico é restrito, ou seja, não é de uso ou de acesso de todos. Nas alternativas abaixo, quais são considerados espaços públicos em uma cidade?", 1200, Font)) + "*A rua e a praça*O ônibus e seu quarto*A cozinha de sua moradia e o parque*A rua e a sala de sua moradia" };
            Historia[1].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Qual das alternativas abaixo indica corretamente a teoria de povoamento das Américas que menciona o Estreito de Bering?", 1200, Font)) + "*Povoamento nômade na última era glacial*Travessia de barco através do oceano Atlântico*Surgimento da espécie humana nas Américas*Povoamento pelos vikings e povos germânicos" };
            Historia[2].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Entre as estrátegias políticas adotadas por Stalin no governo da URSS, visando ao fortalecimento do seu governo, aquela que entre seus métodos utilizava de retratos de sua face foi a adoção:", 1200, Font)) + "*Do culto à personalidade como forma de elevar a figura do líder político*De táticas de espionagem contra os oposicionistas do regime stalinista*De uma nova política econômica com base na estatização de empresas privadas*Do comunismo como forma de governo promovendo uma sociedade igualitária" };
            Historia[3].Questions = new List<string>() { string.Join('\n', Helper.SplitString("", 1200, Font)) + "" };

            Fisica = new DifficultySelector[4];
            for (int i = 0; i < 4; i++)
            {
                Fisica[i] = new DifficultySelector(Main.LoadedTextures["DS"], (i + 1).ToString(), Font)
                {
                    Color = Color.DarkCyan,
                    TrueColor = Color.DarkCyan,
                    Position = new Vector2((int)(WidthOriginal * 0.15f) + 85 + 60 * i, (int)((120) / 2f) - 15 + 240),
                };
                UIElements.Add(Fisica[i]);
            }
            Fisica[0].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Qual é o formato do planeta Terra?", 1200, Font)) + "*Quase esférico*Plano*Cuboide*Esférico" };
            Fisica[1].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Em quais camadas do planeta Terra é possível encontrar, de forma predominante, materiais no estado sólido?", 1200, Font)) + "*Crosta e núcleo interno*Crosta e núcleo externo*Manto e núcleo externo*Núcleo externo e núcleo interno" };
            Fisica[2].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Um corpo com massa de 15 kg é submetido a uma força de intensidade 30 N. Qual é a aceleração que ele adquire?", 1200, Font)) + "*2 m/s^2*1 m/s^2*0,5 m/s^2*3 m/s^2" };
            Fisica[3].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Quando um gato se esfrega bastante na calça de uma pessoa, seus pelos podem ficar eletrizados, qual é o motivo por trás disso?", 1200, Font)) + "*Movimentação de elétrons entre a calça e os pelos do gato*Criação de novas partículas eletrizadas nos pelos do gato*Repulsão entre partículas elétrocas da calça e dos pelos do gato*Diminuição do número de prótons nos pelos do gato" };

            Quimica = new DifficultySelector[4];
            for (int i = 0; i < 4; i++)
            {
                Quimica[i] = new DifficultySelector(Main.LoadedTextures["DS"], (i + 1).ToString(), Font)
                {
                    Color = Color.Purple,
                    TrueColor = Color.Purple,
                    Position = new Vector2((int)(WidthOriginal * 0.15f) + 85 + 60 * i, (int)((120) / 2f) - 15 + 300),
                };
                UIElements.Add(Quimica[i]);
            }
            Quimica[0].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Qual é a fórmula molecular da água?", 1200, Font)) + "*H2O*HO*HO2*H2" };
            Quimica[1].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Qual é o nome da passagem do estado líquido para o gasoso", 1200, Font)) + "*Vaporização*Fusão*Condensação*Congelamento" };
            Quimica[2].Questions = new List<string>() { "Abaixo estão citadas quatro propriedades da máteria:\nI. Densidade\nII. Volume\nIII. Temperatura de ebulição\nIV. Massa\n" + string.Join('\n', Helper.SplitString("Quais delas correspondem à extensão de espaço e à quantidade de matéria que existe em um corpo, respectivamente?", 1200, Font)) + "*II e IV*I e II* I e III*II e III" };
            Quimica[3].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Os ácidos biliares são constiuídos por moléculas com porções hidrofílicas e hidrofóbicas. Em razão dessas características, esses ácidos, que nos seres humanos são produzidos pelo:", 1200, Font)) + "*Fígado, atuam na emulsificação de triglicerideos*Fígado, atuam na emulsificação de açúcares*Fígado, atuam na hidrólise de proteínas*Pâncreas, atuam na emulsificação de triglicerídeos" };


            Biologia = new DifficultySelector[4];
            for (int i = 0; i < 4; i++)
            {
                Biologia[i] = new DifficultySelector(Main.LoadedTextures["DS"], (i + 1).ToString(), Font)
                {
                    Color = Color.ForestGreen,
                    TrueColor = Color.ForestGreen,
                    Position = new Vector2((int)(WidthOriginal * 0.15f) + 85 + 60 * i, (int)((120) / 2f) - 15 + 360),
                };
                UIElements.Add(Biologia[i]);
            }
            Biologia[0].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Uma característica da coruja, que pertence ao grupo das aves, é:", 1200, Font)) + "*Reproduzir-se por meio de ovos*Ter dois pares de asas*Alimentar-se apenas de plantas e frutos*Ser capaz de se locomover por meio de nadadeiras" };
            Biologia[1].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Com base na evolução da espécie humana, pode-se afirmar que o:", 1200, Font)) + "*Homo sapiens e o Homo neanderthalensis chegaram a coexistir ao mesmo tempo*Homo erectus se diferenciava dos demais por ter postura ereta e ser bípede*Homo sapiens é descendente direto do Homo habilis*Homo sapiens e o erectus não eram iguais só por habitarem locais diferentes" };
            Biologia[2].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Lamarck defendia principalmente qual desses princípios evolutivos?", 1200, Font)) + "*Lei do uso e desuso*Lei do mais forte*Lei da mutação*Lei da seleção natural" };
            Biologia[3].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Qual das alternativas abaixo contém doenças que podem ser prevenidas por meio da lavagem de alimentos antes de consumí-los?", 1200, Font)) + "*Ascaridíase, cisticercose e amebíase*Amarelão, elefantíase e leishmaniose*Giardíase cólera e febre amarela*Esquistossomose, enterobiose e tripanossomíase" };

            Literatura = new DifficultySelector[4];
            for (int i = 0; i < 4; i++)
            {
                Literatura[i] = new DifficultySelector(Main.LoadedTextures["DS"], (i + 1).ToString(), Font)
                {
                    Color = Color.DarkOrange,
                    TrueColor = Color.DarkOrange,
                    Position = new Vector2((int)(WidthOriginal * 0.15f) + 85 + 60 * i, (int)((120) / 2f) - 15 + 420),
                };
                UIElements.Add(Literatura[i]);
            }
            Literatura[0].Questions = new List<string>() { string.Join('\n', Helper.SplitString("A essência da literatura se encontra em qual aspecto?", 1200, Font)) + "*Palavras*RimasLivros*Histórias" };
            Literatura[1].Questions = new List<string>() { string.Join('\n', Helper.SplitString("A literatura é considerada qual arte?", 1200, Font)) + "*A sexta (6ª)*A primeira (1ª)*A quarta (4ª)*A sétima (7ª)" };
            Literatura[2].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Qual o livro que inaugurou o Barroco no Brasil?", 1200, Font)) + "*Prosopopeia*Os sermões*Inconstância dos bens do mundo*A Cristo N. S. crucificado" };
            Literatura[3].Questions = new List<string>() { string.Join('\n', Helper.SplitString("A segunda fase do Modernismo no Brasil ocorreu entre quais anos?", 1200, Font)) + "*1930 e 1945*1945 e 1960*1915 e 1930*1960 e 1975" };

            Ingles = new DifficultySelector[4];
            for (int i = 0; i < 4; i++)
            {
                Ingles[i] = new DifficultySelector(Main.LoadedTextures["DS"], (i + 1).ToString(), Font)
                {
                    Color = Color.MediumVioletRed,
                    TrueColor = Color.MediumVioletRed,
                    Position = new Vector2((int)(WidthOriginal * 0.15f) + 85 + 60 * i, (int)((120) / 2f) - 15 + 480),
                };
                UIElements.Add(Ingles[i]);
            }
            Ingles[0].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Como se diz \"Meu nome é\" em inglês?", 1200, Font)) + "*My name is*Meu nume es*My nome is*Mai naime é" };
            Ingles[1].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Como que a palavra \"Cozinhar\" fica em inglês?", 1200, Font)) + "*Cook*Cozin*Kitchen*Kitchinhar" };
            Ingles[2].Questions = new List<string>() { string.Join('\n', Helper.SplitString("I __________ the house three times yesterday. Preencha a lacuna com a forma afirmativa de \"to clean\".", 1200, Font)) + "*cleaned*cleant*clend*clenand" };
            Ingles[3].Questions = new List<string>() { string.Join('\n', Helper.SplitString("When Carlos has a headache, he __________ some tea.", 1200, Font)) + "*drinks*is drinking*drank*used to drink" };

            FiloSocio = new DifficultySelector[4];
            for (int i = 0; i < 4; i++)
            {
                FiloSocio[i] = new DifficultySelector(Main.LoadedTextures["DS"], (i + 1).ToString(), Font)
                {
                    Color = Color.Teal,
                    TrueColor = Color.Teal,
                    Position = new Vector2((int)(WidthOriginal * 0.15f) + 85 + 60 * i, (int)((120) / 2f) - 15 + 540),
                };
                UIElements.Add(FiloSocio[i]);
            }
            FiloSocio[0].Questions = new List<string>() { string.Join('\n', Helper.SplitString("Quem escreveu a Alegoria da Caverna?", 1200, Font)) + "*Platão*Sócrates*Aristóteles*Heráclito" };
            FiloSocio[1].Questions = new List<string>() { string.Join('\n', Helper.SplitString("\"O ser é e o não ser não é; este é o caminho da convicção, pois conduz à verdade. (...) Pois pensar e ser é o mesmo.\"\n\nO trecho do Poema de Parmênides revela um princípio fundamental de sua filosofia. Qual é esse princípio?", 1200, Font)) + "*Imutabilidade e permanência*Desprezo da fé*Mobilidade*Centralidade em questões políticas" };
            FiloSocio[2].Questions = new List<string>() { string.Join('\n', Helper.SplitString("René Descartes desenvolveu um método filosófico baseado na dúvida para encontrar uma certeza na qual possa fundamentar o conhecimento seguro. Essa certeza fundamental de Descartes é chamada de cogito e sua formulação principal diz:", 1200, Font)) + "*\"Penso, logo existo\"*\"Deus sive natura\"*\"Conhece-te a ti mesmo\"*\"Só sei que nada sei\"" };
            FiloSocio[3].Questions = new List<string>() { string.Join('\n', Helper.SplitString("No sistema capitalista, as muitas manifestações de crise criam condições que forçam a algum tipo de racionalização. Em geral, essas crises periódicas têm o efeito de expandir a capacidade produtiva e de renovar as condições de acumulação. Podemos conceber cada crise como uma mudança do processo de acumulação para um nível novo e superior.\n\nA condição para a inclusão dos trabalhadores no novo processo produtivo descrito no texto é a:", 1200, Font)) + "*Qualificação profissional*Associação sindical*Participação eleitoral*Regulamentação funcional" };
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Back))
            {
                Main.JogoAtual = new Menu(graphics, spriteBatch, Font);
                Main.JogoAtual.Initialize();
            }
            if (Phase == 0)
            {
                if (state.IsKeyDown(Keys.Enter))
                {
                    Phase = 1;
                    TempoAteAlternativas = 300;
                    UIElements.Clear();
                }
            }
            if (Phase > 0)
            {
                //Ending Sequence
                if (PerguntasDisponíveis.Count == 0 || Phase > QuantidadeDePerguntas)
                {
                    if (state.IsKeyDown(Keys.Enter))
                    {
                        Initialize();
                    }
                }
                else
                {
                    if (TempoAteAlternativas == 300)
                    {
                        PerguntaARemover = Main.Random.Next(0, PerguntasDisponíveis.Count);
                        PerguntaTotal = PerguntasDisponíveis[PerguntaARemover].Split("*");
                        PerguntaAtual = PerguntaTotal[0];
                        RespostaCerta = PerguntaTotal[1];
                    }
                    if (TempoAteAlternativas-- == 1)
                    {
                        List<int> alternativas = new List<int>()
                    {
                    1, 2, 3, 4
                    };
                        for (int i2 = 0; i2 < 4; i2++)
                        {
                            var i3 = Main.Random.Next(0, alternativas.Count);
                            Respostas[i2] = PerguntaTotal[alternativas[i3]];
                            alternativas.RemoveAt(i3);
                        }
                        TempoRestanteReal = 15 * 60;
                        TempoRestanteSegundos = 15 + 1;
                    }
                    if (TempoAteAlternativas <= 0 && TempoRestanteReal > 0)
                    {
                        if (TempoRestanteReal-- % 60 == 0)
                        {
                            TempoRestanteSegundos--;
                        }
                        if (!P1Respondeu)
                        {
                            if (state.IsKeyDown(Keys.Q))
                            {
                                if (Respostas[0] == RespostaCerta)
                                {
                                    P1PontosAdicionar = 700 + TempoRestanteReal * 300 / (15 * 60);
                                }
                                P1Respondeu = true;
                            }
                            if (state.IsKeyDown(Keys.W))
                            {
                                if (Respostas[1] == RespostaCerta)
                                {
                                    P1PontosAdicionar = 700 + TempoRestanteReal * 300 / (15 * 60);
                                }
                                P1Respondeu = true;
                            }
                            if (state.IsKeyDown(Keys.E))
                            {
                                if (Respostas[2] == RespostaCerta)
                                {
                                    P1PontosAdicionar = 700 + TempoRestanteReal * 300 / (15 * 60);
                                }
                                P1Respondeu = true;
                            }
                            if (state.IsKeyDown(Keys.R))
                            {
                                if (Respostas[3] == RespostaCerta)
                                {
                                    P1PontosAdicionar = 700 + TempoRestanteReal * 300 / (15 * 60);
                                }
                                P1Respondeu = true;
                            }
                        }
                        if (!P2Respondeu)
                        {
                            if (state.IsKeyDown(Keys.U))
                            {
                                if (Respostas[0] == RespostaCerta)
                                {
                                    P2PontosAdicionar = 700 + TempoRestanteReal * 300 / (15 * 60);
                                }
                                P2Respondeu = true;
                            }
                            if (state.IsKeyDown(Keys.I))
                            {
                                if (Respostas[1] == RespostaCerta)
                                {
                                    P2PontosAdicionar = 700 + TempoRestanteReal * 300 / (15 * 60);
                                }
                                P2Respondeu = true;
                            }
                            if (state.IsKeyDown(Keys.O))
                            {
                                if (Respostas[2] == RespostaCerta)
                                {
                                    P2PontosAdicionar = 700 + TempoRestanteReal * 300 / (15 * 60);
                                }
                                P2Respondeu = true;
                            }
                            if (state.IsKeyDown(Keys.P))
                            {
                                if (Respostas[3] == RespostaCerta)
                                {
                                    P2PontosAdicionar = 700 + TempoRestanteReal * 300 / (15 * 60);
                                }
                                P2Respondeu = true;
                            }
                        }
                    }
                    if (TempoAteAlternativas <= 0 && TempoRestanteReal <= 0 || P1Respondeu && P2Respondeu)
                    {
                        TempoRestanteReal = 0;
                        TempoRestanteSegundos = 0;
                        for (int i = 0; i < 4; i++)
                        {
                            if (Respostas[i] != RespostaCerta)
                            {
                                Respostas[i] = "";
                            }
                        }
                        if (P1PontosAdicionar > 0)
                        {
                            P1Color = Color.Gold;
                            P1Pontos += P1PontosAdicionar;
                            P1PontosAdicionar = -1;
                        }
                        if (P1PontosAdicionar == 0)
                        {
                            P1Color = Color.DarkRed;
                        }
                        if (P2PontosAdicionar > 0)
                        {
                            P2Color = Color.Gold;
                            P2Pontos += P2PontosAdicionar;
                            P2PontosAdicionar = -1;
                        }
                        if (P2PontosAdicionar == 0)
                        {
                            P2Color = Color.DarkRed;
                        }
                        if (state.IsKeyDown(Keys.Space))
                        {
                            Phase++;
                            TempoAteAlternativas = 300;
                            P1Respondeu = false;
                            P2Respondeu = false;
                            P1PontosAdicionar = 0;
                            P2PontosAdicionar = 0;
                            P1Color = Color.White;
                            P2Color = Color.White;
                            for (int i = 0; i < 4; i++)
                            {
                                Respostas[i] = "";
                            }
                            PerguntasDisponíveis.RemoveAt(PerguntaARemover);
                        }
                    }
                }
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if (Phase == 0)
            {
                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)(WidthOriginal * 0.15f) - 85, (int)((120) / 2f) - 15, 170, 60), Color.DarkRed);
                spriteBatch.DrawString(Font, "Português", new Vector2((int)(WidthOriginal * 0.15f) - Font.MeasureString("Português").X / 2f, (int)((120) / 2f)), Color.White);

                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)(WidthOriginal * 0.15f) - 85, (int)((120) / 2f) - 15 + 60, 170, 60), Color.Blue);
                spriteBatch.DrawString(Font, "Matemática", new Vector2((int)(WidthOriginal * 0.15f) - Font.MeasureString("Matemática").X / 2f, (int)((120) / 2f) + 60), Color.White);

                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)(WidthOriginal * 0.15f) - 85, (int)((120) / 2f) - 15 + 120, 170, 60), Color.Green);
                spriteBatch.DrawString(Font, "Geografia", new Vector2((int)(WidthOriginal * 0.15f) - Font.MeasureString("Geografia").X / 2f, (int)((120) / 2f) + 120), Color.White);

                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)(WidthOriginal * 0.15f) - 85, (int)((120) / 2f) - 15 + 180, 170, 60), Color.Gold);
                spriteBatch.DrawString(Font, "História", new Vector2((int)(WidthOriginal * 0.15f) - Font.MeasureString("História").X / 2f, (int)((120) / 2f) + 180), Color.White);

                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)(WidthOriginal * 0.15f) - 85, (int)((120) / 2f) - 15 + 240, 170, 60), Color.DarkCyan);
                spriteBatch.DrawString(Font, "Física", new Vector2((int)(WidthOriginal * 0.15f) - Font.MeasureString("Física").X / 2f, (int)((120) / 2f) + 240), Color.White);

                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)(WidthOriginal * 0.15f) - 85, (int)((120) / 2f) - 15 + 300, 170, 60), Color.Purple);
                spriteBatch.DrawString(Font, "Química", new Vector2((int)(WidthOriginal * 0.15f) - Font.MeasureString("Química").X / 2f, (int)((120) / 2f) + 300), Color.White);

                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)(WidthOriginal * 0.15f) - 85, (int)((120) / 2f) - 15 + 360, 170, 60), Color.ForestGreen);
                spriteBatch.DrawString(Font, "Biologia", new Vector2((int)(WidthOriginal * 0.15f) - Font.MeasureString("Biologia").X / 2f, (int)((120) / 2f) + 360), Color.White);

                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)(WidthOriginal * 0.15f) - 85, (int)((120) / 2f) - 15 + 420, 170, 60), Color.DarkOrange);
                spriteBatch.DrawString(Font, "Literatura", new Vector2((int)(WidthOriginal * 0.15f) - Font.MeasureString("Literatura").X / 2f, (int)((120) / 2f) + 420), Color.White);

                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)(WidthOriginal * 0.15f) - 85, (int)((120) / 2f) - 15 + 480, 170, 60), Color.MediumVioletRed);
                spriteBatch.DrawString(Font, "Inglês", new Vector2((int)(WidthOriginal * 0.15f) - Font.MeasureString("Inglês").X / 2f, (int)((120) / 2f) + 480), Color.White);

                spriteBatch.Draw(Main.LoadedTextures["MagicRectangle"], new Rectangle((int)(WidthOriginal * 0.15f) - 85, (int)((120) / 2f) - 15 + 540, 170, 60), Color.Teal);
                spriteBatch.DrawString(Font, "Filo/Socio", new Vector2((int)(WidthOriginal * 0.15f) - Font.MeasureString("FiloSocio").X / 2f, (int)((120) / 2f) + 540), Color.White);

                var text = "Clique nos números para habilitar ou\ndesabilitar as dificuldades de cada matéria!\n\n1 = 1º ao 3º Ano\n2 = 4º ao 6º Ano\n3 = 7º ao 9º Ano\n4 = Ensino Médio\n\nAperte Enter para começar o jogo!";
                spriteBatch.DrawString(Font, text, new Vector2((int)(WidthOriginal * 0.7f) - Font.MeasureString(text).X / 2f, (int)(HeightOriginal * 0.5f) - Font.MeasureString(text).Y / 2f), Color.White);

            }
            if (Phase > 0)
            {
                //Ending Sequence
                if (PerguntasDisponíveis.Count == 0 || Phase > QuantidadeDePerguntas)
                {
                    var text = "";
                    var text1 = "";
                    var text2 = "";
                    var text3 = "Aperte Enter para começar um novo jogo!";
                    if (P1Pontos > P2Pontos)
                    {
                        text = "Parabéns! O Jogador 1 é o vencedor! Mais sorte da próxima vez Jogador 2!";
                        text1 = "Jogador 1: " + P1Pontos + " Pontos!";
                        text2 = "Jogador 2: " + P2Pontos + " Pontos :(";
                    }
                    else if (P2Pontos > P1Pontos)
                    {
                        text = "Parabéns! O Jogador 2 é o vencedor! Mais sorte da próxima vez Jogador 1!";
                        text1 = "Jogador 2: " + P2Pontos + " Pontos!";
                        text2 = "Jogador 1: " + P1Pontos + " Pontos :(";
                    }
                    else
                    {
                        text = "Eu não sei como vocês conseguiram isso. Mais sorte da próxima vez, eu acho!";
                        text1 = "Jogador 1 e 2: " + P1Pontos + " Pontos!";
                    }
                    spriteBatch.DrawString(Font, text, new Vector2(WidthOriginal / 2f - Font.MeasureString(text).X / 2f, HeightOriginal * 0.05f - Font.MeasureString(text).Y / 2f), Color.White);
                    spriteBatch.DrawString(Font, text1, new Vector2(WidthOriginal / 2f - Font.MeasureString(text1).X / 2f, HeightOriginal * 0.45f - Font.MeasureString(text1).Y / 2f), Color.Gold);
                    spriteBatch.DrawString(Font, text2, new Vector2(WidthOriginal / 2f - Font.MeasureString(text2).X / 2f, HeightOriginal * 0.55f - Font.MeasureString(text2).Y / 2f), Color.DarkRed);
                    spriteBatch.DrawString(Font, text3, new Vector2(WidthOriginal / 2f - Font.MeasureString(text3).X / 2f, HeightOriginal * 0.95f - Font.MeasureString(text3).Y / 2f), Color.White);
                }
                else
                {
                    spriteBatch.DrawString(Font, PerguntaAtual, new Vector2(WidthOriginal / 2f - Font.MeasureString(PerguntaAtual).X / 2f, HeightOriginal * 0.05f), Color.White);
                    spriteBatch.DrawString(Font, Respostas[0], new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[0]).X / 2f, HeightOriginal * 0.45f - Font.MeasureString(Respostas[0]).Y / 2f), Color.White);
                    spriteBatch.DrawString(Font, Respostas[1], new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[1]).X / 2f, HeightOriginal * 0.55f - Font.MeasureString(Respostas[1]).Y / 2f), Color.White);
                    spriteBatch.DrawString(Font, Respostas[2], new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[2]).X / 2f, HeightOriginal * 0.65f - Font.MeasureString(Respostas[2]).Y / 2f), Color.White);
                    spriteBatch.DrawString(Font, Respostas[3], new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[3]).X / 2f, HeightOriginal * 0.75f - Font.MeasureString(Respostas[3]).Y / 2f), Color.White);

                    if (TempoAteAlternativas <= 0 && TempoRestanteReal > 0)
                    {
                        var text = TempoRestanteSegundos.ToString() + " Segundos";
                        spriteBatch.DrawString(Font, text, new Vector2(WidthOriginal / 2f - Font.MeasureString(text).X / 2f, HeightOriginal * 0.90f), Color.White);

                        if (!P1Respondeu)
                        {
                            spriteBatch.Draw(KeyTexture, new Rectangle((int)(WidthOriginal / 2f - Font.MeasureString(Respostas[0]).X / 2f - KeyTexture.Width / 2f) - 50, (int)(HeightOriginal * 0.45f - KeyTexture.Height / 2f), KeyTexture.Width, KeyTexture.Height), Color.White);
                            spriteBatch.DrawString(Font, "Q", new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[0]).X / 2f - 50 - Font.MeasureString("Q").X / 2f, HeightOriginal * 0.45f - Font.MeasureString("W").Y / 2f), Color.White);

                            spriteBatch.Draw(KeyTexture, new Rectangle((int)(WidthOriginal / 2f - Font.MeasureString(Respostas[1]).X / 2f - KeyTexture.Width / 2f) - 50, (int)(HeightOriginal * 0.55f - KeyTexture.Height / 2f), KeyTexture.Width, KeyTexture.Height), Color.White);
                            spriteBatch.DrawString(Font, "W", new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[1]).X / 2f - 50 - Font.MeasureString("W").X / 2f, HeightOriginal * 0.55f - Font.MeasureString("W").Y / 2f), Color.White);

                            spriteBatch.Draw(KeyTexture, new Rectangle((int)(WidthOriginal / 2f - Font.MeasureString(Respostas[2]).X / 2f - KeyTexture.Width / 2f) - 50, (int)(HeightOriginal * 0.65f - KeyTexture.Height / 2f), KeyTexture.Width, KeyTexture.Height), Color.White);
                            spriteBatch.DrawString(Font, "E", new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[2]).X / 2f - 50 - Font.MeasureString("E").X / 2f, HeightOriginal * 0.65f - Font.MeasureString("W").Y / 2f), Color.White);

                            spriteBatch.Draw(KeyTexture, new Rectangle((int)(WidthOriginal / 2f - Font.MeasureString(Respostas[3]).X / 2f - KeyTexture.Width / 2f) - 50, (int)(HeightOriginal * 0.75f - KeyTexture.Height / 2f), KeyTexture.Width, KeyTexture.Height), Color.White);
                            spriteBatch.DrawString(Font, "R", new Vector2(WidthOriginal / 2f - Font.MeasureString(Respostas[3]).X / 2f - 50 - Font.MeasureString("R").X / 2f, HeightOriginal * 0.75f - Font.MeasureString("W").Y / 2f), Color.White);
                        }
                        if (!P2Respondeu)
                        {
                            spriteBatch.Draw(KeyTexture, new Rectangle((int)(WidthOriginal / 2f + Font.MeasureString(Respostas[0]).X / 2f - KeyTexture.Width / 2f) + 50, (int)(HeightOriginal * 0.45f - KeyTexture.Height / 2f), KeyTexture.Width, KeyTexture.Height), Color.White);
                            spriteBatch.DrawString(Font, "U", new Vector2(WidthOriginal / 2f + Font.MeasureString(Respostas[0]).X / 2f + 50 - Font.MeasureString("U").X / 2f, HeightOriginal * 0.45f - Font.MeasureString("W").Y / 2f), Color.White);

                            spriteBatch.Draw(KeyTexture, new Rectangle((int)(WidthOriginal / 2f + Font.MeasureString(Respostas[1]).X / 2f - KeyTexture.Width / 2f) + 50, (int)(HeightOriginal * 0.55f - KeyTexture.Height / 2f), KeyTexture.Width, KeyTexture.Height), Color.White);
                            spriteBatch.DrawString(Font, "I", new Vector2(WidthOriginal / 2f + Font.MeasureString(Respostas[1]).X / 2f + 50 - Font.MeasureString("I").X / 2f, HeightOriginal * 0.55f - Font.MeasureString("W").Y / 2f), Color.White);

                            spriteBatch.Draw(KeyTexture, new Rectangle((int)(WidthOriginal / 2f + Font.MeasureString(Respostas[2]).X / 2f - KeyTexture.Width / 2f) + 50, (int)(HeightOriginal * 0.65f - KeyTexture.Height / 2f), KeyTexture.Width, KeyTexture.Height), Color.White);
                            spriteBatch.DrawString(Font, "O", new Vector2(WidthOriginal / 2f + Font.MeasureString(Respostas[2]).X / 2f + 50 - Font.MeasureString("O").X / 2f, HeightOriginal * 0.65f - Font.MeasureString("W").Y / 2f), Color.White);

                            spriteBatch.Draw(KeyTexture, new Rectangle((int)(WidthOriginal / 2f + Font.MeasureString(Respostas[3]).X / 2f - KeyTexture.Width / 2f) + 50, (int)(HeightOriginal * 0.75f - KeyTexture.Height / 2f), KeyTexture.Width, KeyTexture.Height), Color.White);
                            spriteBatch.DrawString(Font, "P", new Vector2(WidthOriginal / 2f + Font.MeasureString(Respostas[3]).X / 2f + 50 - Font.MeasureString("P").X / 2f, HeightOriginal * 0.75f - Font.MeasureString("W").Y / 2f), Color.White);
                        }
                    }
                    if (TempoAteAlternativas <= 0 && TempoRestanteReal <= 0 || P1Respondeu && P2Respondeu)
                    {
                        spriteBatch.DrawString(Font, "Aperte Espaço para continuar!", new Vector2(WidthOriginal * 0.5f - Font.MeasureString("Aperte Espaço para continuar!").X / 2f, HeightOriginal * 0.95f - Font.MeasureString("Aperte Espaço para continuar!").Y / 2f), Color.White);
                    }
                    var p1 = "Jogador 1: " + P1Pontos + " Pontos";
                    spriteBatch.DrawString(Font, p1, new Vector2(WidthOriginal * 0.15f - Font.MeasureString(p1).X / 2f, HeightOriginal * 0.95f - Font.MeasureString(p1).Y / 2f), P1Color);

                    var p2 = "Jogador 2: " + P2Pontos + " Pontos";
                    spriteBatch.DrawString(Font, p2, new Vector2(WidthOriginal * 0.85f - Font.MeasureString(p2).X / 2f, HeightOriginal * 0.95f - Font.MeasureString(p2).Y / 2f), P2Color);
                }
            }
        }
    }
    public class DifficultySelector : UIButton
    {
        public string Text = "";
        public List<string> Questions;
        public bool HasBeenAdded;
        public Color TrueColor;
        public SpriteFont Font;
        public DifficultySelector(Texture2D texture, string text, SpriteFont font) : base(texture)
        {
            Texture = texture;
            Text = text;
            Font = font;
        }
        public override void Click()
        {
            if (HasBeenAdded)
            {
                foreach (string Pergunta in Questions)
                {
                    BatalhaTrivia.PerguntasDisponíveis.Remove(Pergunta);
                }
                HasBeenAdded = false;
            }
            else
            {
                foreach (string Pergunta in Questions)
                {
                    BatalhaTrivia.PerguntasDisponíveis.Add(Pergunta);
                }
                HasBeenAdded = true;
            }

        }
        public override void Update(GameTime gameTime)
        {
            if (IsHovering || HasBeenAdded)
            {
                Color = TrueColor;
            }
            else
            {
                Color = TrueColor * 0.7f;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            spriteBatch.DrawString(Font, Text, new Vector2(Position.X + Rectangle.Width / 2f - Font.MeasureString(Text).X / 2f, Position.Y + Rectangle.Height / 2f - Font.MeasureString(Text).Y / 2f), Color.White);
        }
    }
}