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
            button1 = new Button();
            Print = new Button();
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
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
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Location = new Point(124, 23);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(1300, 870);
            formsPlot1.TabIndex = 3;
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1584, 961);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(formsPlot1);
            Controls.Add(Print);
            Controls.Add(button1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Button Print;
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
