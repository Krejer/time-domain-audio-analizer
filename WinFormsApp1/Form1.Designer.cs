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
            button1 = new Button();
            Print = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            domFreq = new Button();
            Silence = new Button();
            autcorRadio = new RadioButton();
            amdfRadio = new RadioButton();
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
            // button1
            // 
            button1.Location = new Point(12, 23);
            button1.Name = "button1";
            button1.Size = new Size(87, 23);
            button1.TabIndex = 0;
            button1.Text = "Load";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Print
            // 
            Print.Location = new Point(12, 67);
            Print.Name = "Print";
            Print.Size = new Size(87, 23);
            Print.TabIndex = 2;
            Print.Text = "time course";
            Print.UseVisualStyleBackColor = true;
            Print.Click += Print_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 111);
            button2.Name = "button2";
            button2.Size = new Size(87, 23);
            button2.TabIndex = 4;
            button2.Text = "STE";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(12, 160);
            button3.Name = "button3";
            button3.Size = new Size(87, 23);
            button3.TabIndex = 5;
            button3.Text = "Volume";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(12, 208);
            button4.Name = "button4";
            button4.Size = new Size(87, 23);
            button4.TabIndex = 6;
            button4.Text = "ZCR";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // domFreq
            // 
            domFreq.Location = new Point(12, 301);
            domFreq.Margin = new Padding(2);
            domFreq.Name = "domFreq";
            domFreq.Size = new Size(87, 38);
            domFreq.TabIndex = 7;
            domFreq.Text = "Fundamental Frequency";
            domFreq.UseVisualStyleBackColor = true;
            domFreq.Click += domFreq_Click;
            // 
            // Silence
            // 
            Silence.ImageAlign = ContentAlignment.TopRight;
            Silence.Location = new Point(12, 252);
            Silence.Margin = new Padding(2);
            Silence.Name = "Silence";
            Silence.Size = new Size(87, 28);
            Silence.TabIndex = 7;
            Silence.Text = "Silence";
            Silence.UseVisualStyleBackColor = true;
            Silence.Click += Silence_Click;
            // 
            // autcorRadio
            // 
            autcorRadio.AutoSize = true;
            autcorRadio.Checked = true;
            autcorRadio.Location = new Point(12, 344);
            autcorRadio.Name = "autcorRadio";
            autcorRadio.Size = new Size(108, 19);
            autcorRadio.TabIndex = 8;
            autcorRadio.TabStop = true;
            autcorRadio.Text = "Autocorrelation";
            autcorRadio.UseVisualStyleBackColor = true;
            // 
            // amdfRadio
            // 
            amdfRadio.AutoSize = true;
            amdfRadio.Location = new Point(12, 369);
            amdfRadio.Name = "amdfRadio";
            amdfRadio.Size = new Size(58, 19);
            amdfRadio.TabIndex = 9;
            amdfRadio.Text = "AMDF";
            amdfRadio.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(924, 480);
            Controls.Add(amdfRadio);
            Controls.Add(autcorRadio);
            Controls.Add(domFreq);
            Controls.Add(Silence);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(formsPlot1);
            Controls.Add(Print);
            Controls.Add(button1);
            MinimumSize = new Size(940, 519);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Project 1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button Print;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button Silence;
        private Button domFreq;
        private RadioButton autcorRadio;
        private RadioButton amdfRadio;
    }
}
