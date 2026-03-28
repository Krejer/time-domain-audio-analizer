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
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Location = new Point(105, 8);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(806, 453);
            formsPlot1.TabIndex = 3;
            // 
            // loadButton
            // 
            loadButton.Location = new Point(12, 23);
            loadButton.Name = "loadButton";
            loadButton.Size = new Size(87, 23);
            loadButton.TabIndex = 0;
            loadButton.Text = "Load";
            loadButton.UseVisualStyleBackColor = true;
            loadButton.Click += loadButton_Click;
            // 
            // signalButton
            // 
            signalButton.Location = new Point(12, 67);
            signalButton.Name = "signalButton";
            signalButton.Size = new Size(87, 23);
            signalButton.TabIndex = 2;
            signalButton.Text = "time course";
            signalButton.UseVisualStyleBackColor = true;
            signalButton.Click += signalButton_Click;
            // 
            // steButton
            // 
            steButton.Location = new Point(12, 111);
            steButton.Name = "steButton";
            steButton.Size = new Size(87, 23);
            steButton.TabIndex = 4;
            steButton.Text = "STE";
            steButton.UseVisualStyleBackColor = true;
            steButton.Click += steButton_Click;
            // 
            // volumeButton
            // 
            volumeButton.Location = new Point(12, 160);
            volumeButton.Name = "volumeButton";
            volumeButton.Size = new Size(87, 23);
            volumeButton.TabIndex = 5;
            volumeButton.Text = "Volume";
            volumeButton.UseVisualStyleBackColor = true;
            volumeButton.Click += volumeButton_Click;
            // 
            // zcrButton
            // 
            zcrButton.Location = new Point(12, 208);
            zcrButton.Name = "zcrButton";
            zcrButton.Size = new Size(87, 23);
            zcrButton.TabIndex = 6;
            zcrButton.Text = "ZCR";
            zcrButton.UseVisualStyleBackColor = true;
            zcrButton.Click += zcrButton_Click;
            // 
            // funFreqButton
            // 
            funFreqButton.Location = new Point(11, 251);
            funFreqButton.Margin = new Padding(2);
            funFreqButton.Name = "funFreqButton";
            funFreqButton.Size = new Size(87, 38);
            funFreqButton.TabIndex = 7;
            funFreqButton.Text = "Fundamental Frequency";
            funFreqButton.UseVisualStyleBackColor = true;
            funFreqButton.Click += domFreq_Click;
            // 
            // autcorRadio
            // 
            autcorRadio.AutoSize = true;
            autcorRadio.Checked = true;
            autcorRadio.Location = new Point(11, 294);
            autcorRadio.Name = "autcorRadio";
            autcorRadio.Size = new Size(108, 19);
            autcorRadio.TabIndex = 8;
            autcorRadio.TabStop = true;
            autcorRadio.Text = "Autocorrelation";
            autcorRadio.UseVisualStyleBackColor = true;
            autcorRadio.CheckedChanged += autcorRadio_CheckedChanged;
            // 
            // amdfRadio
            // 
            amdfRadio.AutoSize = true;
            amdfRadio.Location = new Point(11, 319);
            amdfRadio.Name = "amdfRadio";
            amdfRadio.Size = new Size(58, 19);
            amdfRadio.TabIndex = 9;
            amdfRadio.Text = "AMDF";
            amdfRadio.UseVisualStyleBackColor = true;
            // 
            // chkShowSilence
            // 
            chkShowSilence.AutoSize = true;
            chkShowSilence.Location = new Point(11, 358);
            chkShowSilence.Name = "chkShowSilence";
            chkShowSilence.Size = new Size(63, 19);
            chkShowSilence.TabIndex = 10;
            chkShowSilence.Text = "Silence";
            chkShowSilence.UseVisualStyleBackColor = true;
            chkShowSilence.CheckedChanged += chkShowSilence_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(924, 480);
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
            MinimumSize = new Size(940, 519);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Project 1";
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
    }
}
