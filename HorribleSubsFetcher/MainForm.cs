using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

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
            runRadioButton.Checked = true;

            filenameTextBox.Text = "output.txt";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fetch();
        }

        private void Fetch()
        {
            if (showTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Please insert the name of a show or a link in the textbox");
                return;
            }

            int showId = 0;
            Task.Run(async () => showId = await GetShowId()).GetAwaiter().GetResult();

            if (showId == -1)
            {
                if (linkRadioButton.Checked)
                    MessageBox.Show("Wrong link!");

                return;
            }

            List<int> episodes = ParseEpisodes();

            if (episodes == null)
                return;

            string[] priorityList = priorityTextBox.Lines;

            List<string> magnets = null;
            Task.Run(async () => magnets = await fetcher.GetMagnetLinks(showId, priorityList, episodes)).GetAwaiter().GetResult();

            if (magnets == null)
                return;

            Export(magnets);
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
                MessageBox.Show("Error parsing desired episodes!");
                return null;
            }
        }

        private async Task<int> GetShowId()
        {
            if (nameRadioButton.Checked)
                return await fetcher.GetShowIdByName(showTextBox.Text);
            else if (linkRadioButton.Checked)
                return await fetcher.GetShowIdByLink(showTextBox.Text);

            return -1;
        }

        private void Export(List<string> magnetList)
        {
            try
            {
                string filename = filenameTextBox.Text;
                StreamWriter writer = null;

                if (exportRadioButton.Checked && File.Exists(filename))
                    File.Delete(filename);

                if (exportRadioButton.Checked)
                    writer = File.CreateText(filename);

                for (int i = 0; i < magnetList.Count; i++)
                {
                    if (exportRadioButton.Checked)
                        writer.WriteLine(magnetList[i]);
                    else if (runRadioButton.Checked)
                        System.Diagnostics.Process.Start(magnetList[i]);
                }

                if (exportRadioButton.Checked)
                    writer.Close();

                MessageBox.Show("Exported successfully");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error exporting!" + e);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            episodeTextBox.Enabled = !checkBox1.Checked;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            filenameTextBox.Visible = exportRadioButton.Checked;
        }

        private void showTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                Fetch();
        }

        private void episodeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                Fetch();
        }
    }
}
