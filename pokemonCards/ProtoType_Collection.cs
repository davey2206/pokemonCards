using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using PokemonTcgSdk;
using System.Net;
namespace pokemonCards
{
    public partial class ProtoType_Collection : Form
    {
        public ProtoType_Collection()
        {
            InitializeComponent();
            var sets = Sets.All();

            foreach (var set in sets)
            {
                comboBox1.Items.Add(set.Name.ToString());
            }
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            List<PictureBox> pictures = this.panel1.Controls.OfType<PictureBox>().ToList();
            foreach (PictureBox pic in pictures)
            {
                this.panel1.Controls.Remove(pic);
            }
            int x = 0;
            int y = 0;
            var sets = Sets.All();
            Dictionary<string, string> query = new Dictionary<string, string>();
            foreach (var set in sets)
            {
                if (set.Name == comboBox1.SelectedItem.ToString())
                {
                    query = new Dictionary<string, string>()
                    {
                        { "setCode",  set.Code.ToString() }
                    };
                }
            }
            var cards = Card.Get(query);
            if (cards.Cards != null)
            {
                foreach (var card in cards.Cards)
                {
                    PictureBox pictureBox1 = new PictureBox();
                    pictureBox1.Location = new System.Drawing.Point(x, y);
                    pictureBox1.Name = "pictureBox1";
                    pictureBox1.Size = new System.Drawing.Size(125, 175);
                    pictureBox1.Load(card.ImageUrl);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.MouseDown += ((o, a) => pictureBox1.Size = new System.Drawing.Size(325, 450));
                    pictureBox1.MouseUp += ((o, a) => pictureBox1.Size = new System.Drawing.Size(125, 175));
                    panel1.Controls.Add(pictureBox1);
                    x = x + 150;
                    if (x > (panel1.Size.Width - 325))
                    {
                        x = 0;
                        y = y + 200;
                    }
                }
            }
        }
    }
}