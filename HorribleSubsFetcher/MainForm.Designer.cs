﻿namespace HorribleSubsFetcher
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fetchButton = new System.Windows.Forms.Button();
            this.showTextBox = new System.Windows.Forms.TextBox();
            this.nameRadioButton = new System.Windows.Forms.RadioButton();
            this.linkRadioButton = new System.Windows.Forms.RadioButton();
            this.episodeTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exportRadioButton = new System.Windows.Forms.RadioButton();
            this.runRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.filenameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.priorityTextBox = new System.Windows.Forms.TextBox();
            this.lastCheckBox = new System.Windows.Forms.CheckBox();
            this.allCheckBox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fetchButton
            // 
            this.fetchButton.Location = new System.Drawing.Point(172, 151);
            this.fetchButton.Name = "fetchButton";
            this.fetchButton.Size = new System.Drawing.Size(76, 23);
            this.fetchButton.TabIndex = 0;
            this.fetchButton.Text = "Fetch";
            this.fetchButton.UseVisualStyleBackColor = true;
            this.fetchButton.Click += new System.EventHandler(this.fetchButton_Click);
            // 
            // showTextBox
            // 
            this.showTextBox.Location = new System.Drawing.Point(70, 68);
            this.showTextBox.Name = "showTextBox";
            this.showTextBox.Size = new System.Drawing.Size(280, 20);
            this.showTextBox.TabIndex = 1;
            this.showTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.showTextBox_KeyPress);
            // 
            // nameRadioButton
            // 
            this.nameRadioButton.AutoSize = true;
            this.nameRadioButton.Location = new System.Drawing.Point(9, 3);
            this.nameRadioButton.Name = "nameRadioButton";
            this.nameRadioButton.Size = new System.Drawing.Size(53, 17);
            this.nameRadioButton.TabIndex = 2;
            this.nameRadioButton.TabStop = true;
            this.nameRadioButton.Text = "Name";
            this.nameRadioButton.UseVisualStyleBackColor = true;
            // 
            // linkRadioButton
            // 
            this.linkRadioButton.AutoSize = true;
            this.linkRadioButton.Location = new System.Drawing.Point(68, 3);
            this.linkRadioButton.Name = "linkRadioButton";
            this.linkRadioButton.Size = new System.Drawing.Size(45, 17);
            this.linkRadioButton.TabIndex = 3;
            this.linkRadioButton.TabStop = true;
            this.linkRadioButton.Text = "Link";
            this.linkRadioButton.UseVisualStyleBackColor = true;
            // 
            // episodeTextBox
            // 
            this.episodeTextBox.Location = new System.Drawing.Point(142, 120);
            this.episodeTextBox.Name = "episodeTextBox";
            this.episodeTextBox.Size = new System.Drawing.Size(136, 20);
            this.episodeTextBox.TabIndex = 4;
            this.episodeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.episodeTextBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(189, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Show";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Episodes";
            // 
            // exportRadioButton
            // 
            this.exportRadioButton.AutoSize = true;
            this.exportRadioButton.Location = new System.Drawing.Point(82, 12);
            this.exportRadioButton.Name = "exportRadioButton";
            this.exportRadioButton.Size = new System.Drawing.Size(83, 17);
            this.exportRadioButton.TabIndex = 7;
            this.exportRadioButton.TabStop = true;
            this.exportRadioButton.Text = "Export to file";
            this.exportRadioButton.UseVisualStyleBackColor = true;
            this.exportRadioButton.CheckedChanged += new System.EventHandler(this.exportButton_CheckedChanged);
            // 
            // runRadioButton
            // 
            this.runRadioButton.AutoSize = true;
            this.runRadioButton.Location = new System.Drawing.Point(13, 12);
            this.runRadioButton.Name = "runRadioButton";
            this.runRadioButton.Size = new System.Drawing.Size(45, 17);
            this.runRadioButton.TabIndex = 8;
            this.runRadioButton.TabStop = true;
            this.runRadioButton.Text = "Run";
            this.runRadioButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkRadioButton);
            this.panel1.Controls.Add(this.nameRadioButton);
            this.panel1.Location = new System.Drawing.Point(146, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(114, 25);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.filenameTextBox);
            this.panel2.Controls.Add(this.runRadioButton);
            this.panel2.Controls.Add(this.exportRadioButton);
            this.panel2.Location = new System.Drawing.Point(198, 195);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 69);
            this.panel2.TabIndex = 10;
            // 
            // filenameTextBox
            // 
            this.filenameTextBox.Location = new System.Drawing.Point(82, 41);
            this.filenameTextBox.Name = "filenameTextBox";
            this.filenameTextBox.Size = new System.Drawing.Size(112, 20);
            this.filenameTextBox.TabIndex = 16;
            this.filenameTextBox.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Quality priority list";
            // 
            // priorityTextBox
            // 
            this.priorityTextBox.Location = new System.Drawing.Point(12, 196);
            this.priorityTextBox.Multiline = true;
            this.priorityTextBox.Name = "priorityTextBox";
            this.priorityTextBox.Size = new System.Drawing.Size(104, 70);
            this.priorityTextBox.TabIndex = 14;
            this.priorityTextBox.Text = "1080p\r\n720p\r\n480p";
            // 
            // lastCheckBox
            // 
            this.lastCheckBox.AutoSize = true;
            this.lastCheckBox.Location = new System.Drawing.Point(285, 121);
            this.lastCheckBox.Name = "lastCheckBox";
            this.lastCheckBox.Size = new System.Drawing.Size(86, 17);
            this.lastCheckBox.TabIndex = 15;
            this.lastCheckBox.Text = "Last episode";
            this.lastCheckBox.UseVisualStyleBackColor = true;
            this.lastCheckBox.CheckedChanged += new System.EventHandler(this.lastCheckBox_CheckedChanged);
            // 
            // allCheckBox
            // 
            this.allCheckBox.AutoSize = true;
            this.allCheckBox.Location = new System.Drawing.Point(368, 121);
            this.allCheckBox.Name = "allCheckBox";
            this.allCheckBox.Size = new System.Drawing.Size(37, 17);
            this.allCheckBox.TabIndex = 16;
            this.allCheckBox.Text = "All";
            this.allCheckBox.UseVisualStyleBackColor = true;
            this.allCheckBox.CheckedChanged += new System.EventHandler(this.allCheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 310);
            this.Controls.Add(this.allCheckBox);
            this.Controls.Add(this.lastCheckBox);
            this.Controls.Add(this.priorityTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.episodeTextBox);
            this.Controls.Add(this.showTextBox);
            this.Controls.Add(this.fetchButton);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "HorribleSubsFetcher";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fetchButton;
        private System.Windows.Forms.TextBox showTextBox;
        private System.Windows.Forms.RadioButton nameRadioButton;
        private System.Windows.Forms.RadioButton linkRadioButton;
        private System.Windows.Forms.TextBox episodeTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton exportRadioButton;
        private System.Windows.Forms.RadioButton runRadioButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox priorityTextBox;
        private System.Windows.Forms.TextBox filenameTextBox;
        private System.Windows.Forms.CheckBox lastCheckBox;
        private System.Windows.Forms.CheckBox allCheckBox;
    }
}