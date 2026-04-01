namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            loadButton = new Button();
            signalButton = new Button();
            steButton = new Button();
            volumeButton = new Button();
            zcrButton = new Button();
            funFreqButton = new Button();
            autcorRadio = new RadioButton();
            amdfRadio = new RadioButton();
            chkShowSilence = new CheckBox();
            chkVoiced = new CheckBox();
            chkUnvoiced = new CheckBox();
            ClipLevelSaver = new Button();
            chkSpeech = new CheckBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Location = new Point(245, 45);
            formsPlot1.Margin = new Padding(5, 6, 5, 6);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1317, 931);
            formsPlot1.TabIndex = 3;
            // 
            // loadButton
            // 
            loadButton.Location = new Point(21, 46);
            loadButton.Margin = new Padding(5, 6, 5, 6);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(149, 46);
            loadButton.TabIndex = 0;
            loadButton.Text = "Load";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += loadButton_Click;
            // 
            // signalButton
            // 
            signalButton.Location = new Point(21, 134);
            signalButton.Margin = new Padding(5, 6, 5, 6);
            signalButton.Name = "signalButton";
            signalButton.Size = new Size(149, 46);
            signalButton.TabIndex = 2;
            signalButton.Text = "time course";
            signalButton.UseVisualStyleBackColor = true;
            signalButton.Click += signalButton_Click;
            // 
            // steButton
            // 
            steButton.Location = new Point(21, 222);
            steButton.Margin = new Padding(5, 6, 5, 6);
            steButton.Name = "steButton";
            steButton.Size = new Size(149, 46);
            steButton.TabIndex = 4;
            steButton.Text = "STE";
            steButton.UseVisualStyleBackColor = true;
            steButton.Click += steButton_Click;
            // 
            // volumeButton
            // 
            volumeButton.Location = new Point(21, 320);
            volumeButton.Margin = new Padding(5, 6, 5, 6);
            volumeButton.Name = "volumeButton";
            volumeButton.Size = new Size(149, 46);
            volumeButton.TabIndex = 5;
            volumeButton.Text = "Volume";
            volumeButton.UseVisualStyleBackColor = true;
            volumeButton.Click += volumeButton_Click;
            // 
            // zcrButton
            // 
            zcrButton.Location = new Point(21, 416);
            zcrButton.Margin = new Padding(5, 6, 5, 6);
            zcrButton.Name = "zcrButton";
            zcrButton.Size = new Size(149, 46);
            zcrButton.TabIndex = 6;
            zcrButton.Text = "ZCR";
            zcrButton.UseVisualStyleBackColor = true;
            zcrButton.Click += zcrButton_Click;
            // 
            // funFreqButton
            // 
            funFreqButton.Location = new Point(21, 502);
            funFreqButton.Margin = new Padding(3, 4, 3, 4);
            funFreqButton.Name = "funFreqButton";
            funFreqButton.Size = new Size(149, 76);
            funFreqButton.TabIndex = 7;
            funFreqButton.Text = "Fundamental Frequency";
            funFreqButton.UseVisualStyleBackColor = true;
            funFreqButton.Click += domFreq_Click;
            // 
            // autcorRadio
            // 
            autcorRadio.AutoSize = true;
            autcorRadio.Checked = true;
            autcorRadio.Location = new Point(19, 588);
            autcorRadio.Margin = new Padding(5, 6, 5, 6);
            autcorRadio.Name = "autcorRadio";
            autcorRadio.Size = new Size(182, 34);
            autcorRadio.TabIndex = 8;
            autcorRadio.TabStop = true;
            autcorRadio.Text = "Autocorrelation";
            autcorRadio.UseVisualStyleBackColor = true;
            autcorRadio.CheckedChanged += autcorRadio_CheckedChanged;
            // 
            // amdfRadio
            // 
            amdfRadio.AutoSize = true;
            amdfRadio.Location = new Point(19, 638);
            amdfRadio.Margin = new Padding(5, 6, 5, 6);
            amdfRadio.Name = "amdfRadio";
            amdfRadio.Size = new Size(96, 34);
            amdfRadio.TabIndex = 9;
            amdfRadio.Text = "AMDF";
            amdfRadio.UseVisualStyleBackColor = true;
            // 
            // chkShowSilence
            // 
            chkShowSilence.AutoSize = true;
            chkShowSilence.Location = new Point(19, 716);
            chkShowSilence.Margin = new Padding(5, 6, 5, 6);
            chkShowSilence.Name = "chkShowSilence";
            chkShowSilence.Size = new Size(157, 34);
            chkShowSilence.TabIndex = 10;
            chkShowSilence.Text = "Silence (Red)";
            chkShowSilence.UseVisualStyleBackColor = true;
            chkShowSilence.CheckedChanged += chkShowSilence_CheckedChanged;
            // 
            // chkVoiced
            // 
            chkVoiced.AutoSize = true;
            chkVoiced.Location = new Point(19, 766);
            chkVoiced.Margin = new Padding(5, 6, 5, 6);
            chkVoiced.Name = "chkVoiced";
            chkVoiced.Size = new Size(174, 34);
            chkVoiced.TabIndex = 11;
            chkVoiced.Text = "Voiced (Green)";
            chkVoiced.UseVisualStyleBackColor = true;
            chkVoiced.CheckedChanged += chkVoiced_CheckedChanged;
            // 
            // chkUnvoiced
            // 
            chkUnvoiced.AutoSize = true;
            chkUnvoiced.Location = new Point(19, 816);
            chkUnvoiced.Margin = new Padding(5, 6, 5, 6);
            chkUnvoiced.Name = "chkUnvoiced";
            chkUnvoiced.Size = new Size(183, 34);
            chkUnvoiced.TabIndex = 12;
            chkUnvoiced.Text = "Unvoiced (Blue)";
            chkUnvoiced.UseVisualStyleBackColor = true;
            chkUnvoiced.CheckedChanged += chkUnvoiced_CheckedChanged;
            // 
            // ClipLevelSaver
            // 
            ClipLevelSaver.ImageAlign = ContentAlignment.BottomLeft;
            ClipLevelSaver.Location = new Point(19, 916);
            ClipLevelSaver.Margin = new Padding(5, 6, 5, 6);
            ClipLevelSaver.Name = "ClipLevelSaver";
            ClipLevelSaver.Size = new Size(149, 46);
            ClipLevelSaver.TabIndex = 13;
            ClipLevelSaver.Text = "SaveParams";
            ClipLevelSaver.UseVisualStyleBackColor = true;
            ClipLevelSaver.Click += ClipLevelSaver_Click;
            // 
            // chkSpeech
            // 
            chkSpeech.AutoSize = true;
            chkSpeech.Location = new Point(19, 866);
            chkSpeech.Margin = new Padding(5, 6, 5, 6);
            chkSpeech.Name = "chkSpeech";
            chkSpeech.Size = new Size(245, 34);
            chkSpeech.TabIndex = 14;
            chkSpeech.Text = "Speech/music (purple)";
            chkSpeech.UseVisualStyleBackColor = true;
            chkSpeech.CheckedChanged += chkSpeech_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(286, 9);
            label1.Name = "label1";
            label1.Size = new Size(0, 30);
            label1.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1584, 1000);
            Controls.Add(label1);
            Controls.Add(chkSpeech);
            Controls.Add(ClipLevelSaver);
            Controls.Add(chkUnvoiced);
            Controls.Add(chkVoiced);
            Controls.Add(chkShowSilence);
            Controls.Add(amdfRadio);
            Controls.Add(autcorRadio);
            Controls.Add(funFreqButton);
            Controls.Add(zcrButton);
            Controls.Add(volumeButton);
            Controls.Add(steButton);
            Controls.Add(formsPlot1);
            Controls.Add(signalButton);
            Controls.Add(loadButton);
            Margin = new Padding(5, 6, 5, 6);
            MinimumSize = new Size(1594, 974);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Project 1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button loadButton;
        private Button signalButton;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private Button steButton;
        private Button volumeButton;
        private Button zcrButton;
        private Button funFreqButton;
        private RadioButton autcorRadio;
        private RadioButton amdfRadio;
        private CheckBox chkShowSilence;
        private CheckBox chkVoiced;
        private CheckBox chkUnvoiced;
        private Button ClipLevelSaver;
        private CheckBox chkSpeech;
        private Label label1;
    }
}
