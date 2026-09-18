using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Mara
{
    public partial class imageVisualisation : Form
    {
        private string imageUrl;
        public imageVisualisation(string image)
        {
            InitializeComponent();
            this.imageUrl = image;
            this.Load += imageVisualisation_Load;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private async void imageVisualisation_Load(object sender, EventArgs e)
        {
            using HttpClient client = new HttpClient();

            byte[] imagebyte = await client.GetByteArrayAsync(imageUrl);

            using MemoryStream flux = new MemoryStream(imagebyte);

            pictureBox1.Image = Image.FromStream(flux);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
