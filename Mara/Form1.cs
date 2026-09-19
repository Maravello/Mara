using Whisper.net.Ggml;
using Mara.Services;
using System.Media;
using System.Drawing;
using Microsoft.VisualBasic.Devices;
using System.Text.Json;

namespace Mara
{
    public partial class Form1 : Form
    {
        private readonly SpeechService speechService = new SpeechService();
        private  OllamaService ollamaService = new OllamaService();

        private readonly TtsService ttsService = new TtsService();

        private readonly ActionService actionService = new ActionService();

        private Boolean isEcoute = false;
        public Form1()
        {
            InitializeComponent();
            this.Load += form_load;
            // Si une image GIF est définie en tant que BackgroundImage dans le designer,
            // la transférer vers Image pour assurer l'animation.
            if (pictureBox1.BackgroundImage != null)
            {
                pictureBox1.Image = pictureBox1.BackgroundImage;
                pictureBox1.BackgroundImage = null;
                ImageAnimator.Animate(pictureBox1.Image, null);
            }
            DotNetEnv.Env.Load();
        }

        private async void form_load(object sender, EventArgs e)
        {
            await speechService.ChargementDuModele();
            textBox1.ReadOnly = true;

            MessageBox.Show("Modèle chargé avec succès !");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_MouseClick(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (!isEcoute)
            {
                button1.Enabled = true;

                isEcoute = true;
                button1.Text = "Ecoute...";
                speechService.StartRecording();
            }
            else
            {
                button1.Enabled = false;
                isEcoute = false;
                button1.Text = "Transcription...";
                speechService.StopRecording();
                pictureBox1.Image = Image.FromFile("C:\\Users\\yahay\\source\\repos\\Mara\\Mara\\thinking.gif");


                string text = await speechService.Transcription("recording.wav");

                textBox2.Text = text;

                button1.Text = "Parler";

                string reponse= await ollamaService.PoserQuestion(text);


                try
                {
                    using JsonDocument jsonDocument = JsonDocument.Parse(reponse);
                    if (jsonDocument.RootElement.TryGetProperty("action", out _))
                    {
                        await actionService.Executer(reponse);
                    }
                    else
                    {
                        pictureBox1.Image = Image.FromFile("C:\\Users\\yahay\\source\\repos\\Mara\\Mara\\mara talking.gif");
                        await this.ttsService.Parler(reponse);
                        Cursor.Current = Cursors.Default;
                        textBox1.Text = reponse;
                        button1.Enabled = true;



                    }
                }
                catch (JsonException)
                {
                    await this.ttsService.Parler(reponse);
                    Cursor.Current = Cursors.Default;
                    textBox1.Text = reponse;
                    button1.Enabled = true;
                }


                button1.Enabled = true;
                pictureBox1.Image = Image.FromFile("C:\\Users\\yahay\\source\\repos\\Mara\\Mara\\mara.gif");






            }


        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
