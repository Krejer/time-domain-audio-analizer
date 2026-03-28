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
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Location = new Point(180, 15);
            formsPlot1.Margin = new Padding(5, 6, 5, 6);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1382, 906);
            formsPlot1.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(21, 46);
            button1.Margin = new Padding(5, 6, 5, 6);
            button1.Name = "button1";
            button1.Size = new Size(149, 46);
            button1.TabIndex = 0;
            button1.Text = "Load";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Print
            // 
            Print.Location = new Point(21, 134);
            Print.Margin = new Padding(5, 6, 5, 6);
            Print.Name = "Print";
            Print.Size = new Size(149, 46);
            Print.TabIndex = 2;
            Print.Text = "time course";
            Print.UseVisualStyleBackColor = true;
            Print.Click += Print_Click;
            // 
            // button2
            // 
            button2.Location = new Point(21, 222);
            button2.Margin = new Padding(5, 6, 5, 6);
            button2.Name = "button2";
            button2.Size = new Size(149, 46);
            button2.TabIndex = 4;
            button2.Text = "STE";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(21, 320);
            button3.Margin = new Padding(5, 6, 5, 6);
            button3.Name = "button3";
            button3.Size = new Size(149, 46);
            button3.TabIndex = 5;
            button3.Text = "Volume";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(21, 416);
            button4.Margin = new Padding(5, 6, 5, 6);
            button4.Name = "button4";
            button4.Size = new Size(149, 46);
            button4.TabIndex = 6;
            button4.Text = "ZCR";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // domFreq
            // 
            domFreq.Location = new Point(21, 500);
            domFreq.Name = "domFreq";
            domFreq.Size = new Size(149, 77);
            domFreq.TabIndex = 7;
            domFreq.Text = "Dominant frequencies";
            domFreq.UseVisualStyleBackColor = true;
            domFreq.Click += domFreq_Click;
            // 
            // Silence
            // 
            Silence.ImageAlign = ContentAlignment.TopRight;
            Silence.Location = new Point(12, 236);
            Silence.Name = "Silence";
            Silence.Size = new Size(87, 23);
            Silence.TabIndex = 7;
            Silence.Text = "Silence";
            Silence.UseVisualStyleBackColor = true;
            Silence.Click += Silence_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1576, 936);
            Controls.Add(domFreq);
            ClientSize = new Size(1584, 961);
            Controls.Add(Silence);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(formsPlot1);
            Controls.Add(Print);
            Controls.Add(button1);
            Margin = new Padding(5, 6, 5, 6);
            MinimumSize = new Size(1600, 1000);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Project 1";
            ResumeLayout(false);
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
    }
}
