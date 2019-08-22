using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text;

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

        private void fetchButton_Click(object sender, EventArgs e)
        {
            Fetch();
        }

        private async Task<int> GetShowId()
        {
            if (nameRadioButton.Checked)
                return await fetcher.GetShowIdByName(showTextBox.Text);
            else if (linkRadioButton.Checked)
                return await fetcher.GetShowIdByLink(showTextBox.Text);

            return -1;
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
                if (nameRadioButton.Checked)
                    MessageBox.Show("Failed to find show!");
                else if (linkRadioButton.Checked)
                    MessageBox.Show("Wrong link!");

                return;
            }

            string[] priorityList = priorityTextBox.Lines;

            List<string> magnets = null;
            List<int> failedEpisodes = new List<int>();

            if (lastCheckBox.Checked) // Last episode only
            {
                Task.Run(async () => magnets = fetcher.GetMagnetLinks(priorityList, await fetcher.GetLastEpisode(showId), failedEpisodes)).GetAwaiter().GetResult();
            }
            else if (allCheckBox.Checked) // All episodes
            {
                Task.Run(async () => magnets = fetcher.GetMagnetLinks(priorityList, await fetcher.GetAllEpisodes(showId), failedEpisodes)).GetAwaiter().GetResult();
            }
            else // Episodes list from episodeTextBox
            {
                List<int> episodes = ParseEpisodes();

                if (episodes == null)
                    return;

                Task.Run(async () => magnets = await fetcher.GetMagnetLinks(showId, priorityList, episodes, failedEpisodes)).GetAwaiter().GetResult();
            }

            if (magnets == null)
                return;

            Export(magnets, failedEpisodes);
        }


        private void Export(List<string> magnetList, List<int> failedEpisodes = null)
        {
            try
            {
                string filename = filenameTextBox.Text;
                StreamWriter writer = null;

                if (exportRadioButton.Checked)
                {
                    if (File.Exists(filename))
                        File.Delete(filename);

                    writer = File.CreateText(filename);
                }

                for (int i = 0; i < magnetList.Count; i++)
                {
                    if (exportRadioButton.Checked)
                        writer.WriteLine(magnetList[i]);
                    else if (runRadioButton.Checked)
                        System.Diagnostics.Process.Start(magnetList[i]);
                }

                StringBuilder sb = new StringBuilder();

                if (magnetList == null || magnetList.Count == 0)
                    sb.Append("No episodes you selected were fetched");
                else if (failedEpisodes == null || failedEpisodes.Count == 0)
                    sb.Append("Exported successfully");
                else
                {
                    sb.Append("Errors fetching links:");
                    sb.Append(Environment.NewLine);

                    for (int i = 0; i < failedEpisodes.Count; i++)
                    {
                        sb.Append("Failed to fetch episode ");
                        sb.Append(failedEpisodes[i]);
                        sb.Append(Environment.NewLine);
                    }
                }

                if (exportRadioButton.Checked)
                    writer.Close();

                MessageBox.Show(sb.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("Error exporting!" + e);
            }

        }

        private void exportButton_CheckedChanged(object sender, EventArgs e)
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

        private void lastCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (lastCheckBox.Checked && allCheckBox.Checked)
                allCheckBox.Checked = false;

            checkEpisodeTextBox();
        }

        private void allCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (lastCheckBox.Checked && allCheckBox.Checked)
                lastCheckBox.Checked = false;

            checkEpisodeTextBox();
        }

        private void checkEpisodeTextBox()
        {
            episodeTextBox.Enabled = !(lastCheckBox.Checked || allCheckBox.Checked);
        }
    }
}
