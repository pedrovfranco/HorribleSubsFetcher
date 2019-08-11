using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HorribleSubsFetcher
{
    public partial class MainForm : Form
    {
        Fetcher fetcher;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            fetcher = new Fetcher();

            nameRadioButton.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int showId = 0;
            Task.Run(async () => showId = await GetShowId()).GetAwaiter().GetResult();

            List<int> episodes = ParseEpisodes();
            string[] priorityList = priorityTextBox.Lines;

            List<string> magnets;
            Task.Run(async () => magnets = await fetcher.GetMagnetLinks(showId, priorityList, episodes)).GetAwaiter().GetResult();
            

            int a = 0;
        }

        private List<int> ParseEpisodes()
        {
            try
            {
                List<int> episodes = new List<int>();

                var commaSeperated = episodeTextBox.Text.Split(',');

                for (int i = 0; i < commaSeperated.Length; i++)
                {
                    var dashSeperated = commaSeperated[i].Split('-');

                    if (dashSeperated.Length == 1)
                        episodes.Add(Int32.Parse(dashSeperated[0]));

                    if (dashSeperated.Length == 2)
                    {
                        if (commaSeperated[i].Contains("-"))
                        {
                            int lowerBound = Int32.Parse(dashSeperated[0]);
                            int upperBound = Int32.Parse(dashSeperated[1]);
                            for (int j = lowerBound; j <= upperBound; j++)
                            {
                                episodes.Add(j);
                            }
                        }
                    }
                }

                return episodes;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task<int> GetShowId()
        {
            if (showTextBox.Text != "")
            {
                if (nameRadioButton.Checked)
                    return await fetcher.GetShowIdByName(showTextBox.Text);
                else if (linkRadioButton.Checked)
                    return await fetcher.GetShowIdByLink(showTextBox.Text);
            }

            return -1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            episodeTextBox.Enabled = !checkBox1.Checked;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            filenameTextBox.Visible = exportRadioButton.Checked;
        }
    }
}
