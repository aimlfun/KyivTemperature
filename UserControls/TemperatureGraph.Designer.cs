namespace Temperature
{
    partial class TemperatureGraph
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelLabel = new Panel();
            labelGraphTitle = new Label();
            pictureBoxGraph = new PictureBox();
            panelLabel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGraph).BeginInit();
            SuspendLayout();
            // 
            // panelLabel
            // 
            panelLabel.BackColor = Color.Transparent;
            panelLabel.Controls.Add(labelGraphTitle);
            panelLabel.Dock = DockStyle.Top;
            panelLabel.Location = new Point(0, 0);
            panelLabel.Name = "panelLabel";
            panelLabel.Size = new Size(261, 20);
            panelLabel.TabIndex = 2;
            // 
            // labelGraphTitle
            // 
            labelGraphTitle.Dock = DockStyle.Top;
            labelGraphTitle.Font = new Font("Segoe UI Variable Display Light", 7F);
            labelGraphTitle.Location = new Point(0, 0);
            labelGraphTitle.Name = "labelGraphTitle";
            labelGraphTitle.Size = new Size(261, 18);
            labelGraphTitle.TabIndex = 1;
            labelGraphTitle.Text = "Kyiv 2012 Temperatures";
            labelGraphTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pictureBoxGraph
            // 
            pictureBoxGraph.Dock = DockStyle.Fill;
            pictureBoxGraph.Location = new Point(0, 20);
            pictureBoxGraph.Name = "pictureBoxGraph";
            pictureBoxGraph.Size = new Size(261, 209);
            pictureBoxGraph.TabIndex = 3;
            pictureBoxGraph.TabStop = false;
            // 
            // TemperatureGraph
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pictureBoxGraph);
            Controls.Add(panelLabel);
            Name = "TemperatureGraph";
            Size = new Size(261, 229);
            panelLabel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxGraph).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLabel;
        private Label labelGraphTitle;
        private PictureBox pictureBoxGraph;
    }
}
