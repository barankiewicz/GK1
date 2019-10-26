namespace ComputerGraphicsLab1
{
    partial class Form1
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
            this.mainWind = new System.Windows.Forms.PictureBox();
            this.menuPanel = new System.Windows.Forms.TableLayoutPanel();
            this.editBox = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.customLinesButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.equalContraintButton = new System.Windows.Forms.Button();
            this.deleteVertexButton = new System.Windows.Forms.Button();
            this.editPolygonButton = new System.Windows.Forms.Button();
            this.newPolygonButton = new System.Windows.Forms.Button();
            this.parallelConstraintButton = new System.Windows.Forms.Button();
            this.saveBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.importButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pictureContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.removeConstraintButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainWind)).BeginInit();
            this.menuPanel.SuspendLayout();
            this.editBox.SuspendLayout();
            this.panel2.SuspendLayout();
            this.saveBox.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainWind
            // 
            this.mainWind.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainWind.BackColor = System.Drawing.Color.White;
            this.mainWind.Location = new System.Drawing.Point(0, 0);
            this.mainWind.Name = "mainWind";
            this.mainWind.Size = new System.Drawing.Size(584, 562);
            this.mainWind.TabIndex = 1;
            this.mainWind.TabStop = false;
            this.mainWind.Paint += new System.Windows.Forms.PaintEventHandler(this.mainWind_Paint);
            this.mainWind.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mainWind_MouseDoubleClick);
            this.mainWind.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainWind_MouseDown);
            this.mainWind.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mainWind_MouseMove);
            this.mainWind.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainWind_MouseUp);
            // 
            // menuPanel
            // 
            this.menuPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuPanel.ColumnCount = 1;
            this.menuPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.menuPanel.Controls.Add(this.editBox, 0, 0);
            this.menuPanel.Controls.Add(this.saveBox, 0, 2);
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.menuPanel.Location = new System.Drawing.Point(584, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.RowCount = 3;
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.menuPanel.Size = new System.Drawing.Size(200, 562);
            this.menuPanel.TabIndex = 2;
            // 
            // editBox
            // 
            this.editBox.Controls.Add(this.panel2);
            this.editBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editBox.Location = new System.Drawing.Point(3, 3);
            this.editBox.Name = "editBox";
            this.editBox.Size = new System.Drawing.Size(194, 316);
            this.editBox.TabIndex = 0;
            this.editBox.TabStop = false;
            this.editBox.Text = "Menu";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.removeConstraintButton);
            this.panel2.Controls.Add(this.customLinesButton);
            this.panel2.Controls.Add(this.moveButton);
            this.panel2.Controls.Add(this.equalContraintButton);
            this.panel2.Controls.Add(this.deleteVertexButton);
            this.panel2.Controls.Add(this.editPolygonButton);
            this.panel2.Controls.Add(this.newPolygonButton);
            this.panel2.Controls.Add(this.parallelConstraintButton);
            this.panel2.Location = new System.Drawing.Point(-3, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 297);
            this.panel2.TabIndex = 4;
            // 
            // customLinesButton
            // 
            this.customLinesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customLinesButton.Location = new System.Drawing.Point(-2, 263);
            this.customLinesButton.Name = "customLinesButton";
            this.customLinesButton.Size = new System.Drawing.Size(199, 34);
            this.customLinesButton.TabIndex = 7;
            this.customLinesButton.Text = "Draw Custom Lines (Bresenham Algorithm)";
            this.customLinesButton.UseVisualStyleBackColor = true;
            this.customLinesButton.Click += new System.EventHandler(this.customLinesButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.moveButton.Location = new System.Drawing.Point(0, 60);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(199, 34);
            this.moveButton.TabIndex = 6;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // equalContraintButton
            // 
            this.equalContraintButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.equalContraintButton.Location = new System.Drawing.Point(1, 122);
            this.equalContraintButton.Name = "equalContraintButton";
            this.equalContraintButton.Size = new System.Drawing.Size(199, 34);
            this.equalContraintButton.TabIndex = 3;
            this.equalContraintButton.Text = "Edge Contraint: Equal";
            this.equalContraintButton.UseVisualStyleBackColor = true;
            this.equalContraintButton.Click += new System.EventHandler(this.equalContraintButton_Click);
            // 
            // deleteVertexButton
            // 
            this.deleteVertexButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteVertexButton.Location = new System.Drawing.Point(-2, 91);
            this.deleteVertexButton.Name = "deleteVertexButton";
            this.deleteVertexButton.Size = new System.Drawing.Size(202, 34);
            this.deleteVertexButton.TabIndex = 2;
            this.deleteVertexButton.Text = "Delete Vertex";
            this.deleteVertexButton.UseVisualStyleBackColor = true;
            this.deleteVertexButton.Click += new System.EventHandler(this.deleteVertexButton_Click);
            // 
            // editPolygonButton
            // 
            this.editPolygonButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editPolygonButton.Location = new System.Drawing.Point(0, 31);
            this.editPolygonButton.Name = "editPolygonButton";
            this.editPolygonButton.Size = new System.Drawing.Size(199, 34);
            this.editPolygonButton.TabIndex = 1;
            this.editPolygonButton.Text = "Edit";
            this.editPolygonButton.UseVisualStyleBackColor = true;
            this.editPolygonButton.Click += new System.EventHandler(this.editPolygonButton_Click);
            // 
            // newPolygonButton
            // 
            this.newPolygonButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newPolygonButton.Location = new System.Drawing.Point(0, 0);
            this.newPolygonButton.Name = "newPolygonButton";
            this.newPolygonButton.Size = new System.Drawing.Size(199, 34);
            this.newPolygonButton.TabIndex = 0;
            this.newPolygonButton.Text = "New Polygon";
            this.newPolygonButton.UseVisualStyleBackColor = true;
            this.newPolygonButton.Click += new System.EventHandler(this.newPolygonButton_Click);
            // 
            // parallelConstraintButton
            // 
            this.parallelConstraintButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.parallelConstraintButton.Location = new System.Drawing.Point(1, 153);
            this.parallelConstraintButton.Name = "parallelConstraintButton";
            this.parallelConstraintButton.Size = new System.Drawing.Size(199, 34);
            this.parallelConstraintButton.TabIndex = 4;
            this.parallelConstraintButton.Text = "Edge Constraint: Parallel";
            this.parallelConstraintButton.UseVisualStyleBackColor = true;
            this.parallelConstraintButton.Click += new System.EventHandler(this.parallelConstraintButton_Click);
            // 
            // saveBox
            // 
            this.saveBox.Controls.Add(this.tableLayoutPanel3);
            this.saveBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveBox.Location = new System.Drawing.Point(3, 445);
            this.saveBox.Name = "saveBox";
            this.saveBox.Size = new System.Drawing.Size(194, 114);
            this.saveBox.TabIndex = 2;
            this.saveBox.TabStop = false;
            this.saveBox.Text = "Import/Export";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.importButton, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.saveButton, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(188, 95);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // importButton
            // 
            this.importButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importButton.Location = new System.Drawing.Point(3, 50);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(182, 42);
            this.importButton.TabIndex = 2;
            this.importButton.Text = "Import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveButton.Location = new System.Drawing.Point(3, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(182, 41);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Export";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // pictureContainer
            // 
            this.pictureContainer.AutoSize = true;
            this.pictureContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pictureContainer.Location = new System.Drawing.Point(3, 3);
            this.pictureContainer.Name = "pictureContainer";
            this.pictureContainer.Size = new System.Drawing.Size(0, 0);
            this.pictureContainer.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.mainWind);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 562);
            this.panel1.TabIndex = 4;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Graph file | *.graph";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "SavedGraph";
            this.openFileDialog.Filter = "Graph file | *.graph";
            // 
            // removeConstraintButton
            // 
            this.removeConstraintButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.removeConstraintButton.Location = new System.Drawing.Point(1, 184);
            this.removeConstraintButton.Name = "removeConstraintButton";
            this.removeConstraintButton.Size = new System.Drawing.Size(199, 34);
            this.removeConstraintButton.TabIndex = 8;
            this.removeConstraintButton.Text = "Remove Constraint";
            this.removeConstraintButton.UseVisualStyleBackColor = true;
            this.removeConstraintButton.Click += new System.EventHandler(this.removeConstraintButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureContainer);
            this.Controls.Add(this.menuPanel);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Polygon Editor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.mainWind)).EndInit();
            this.menuPanel.ResumeLayout(false);
            this.editBox.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.saveBox.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox mainWind;
        private System.Windows.Forms.TableLayoutPanel menuPanel;
        private System.Windows.Forms.GroupBox editBox;
        private System.Windows.Forms.GroupBox saveBox;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel pictureContainer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button equalContraintButton;
        private System.Windows.Forms.Button deleteVertexButton;
        private System.Windows.Forms.Button editPolygonButton;
        private System.Windows.Forms.Button newPolygonButton;
        private System.Windows.Forms.Button parallelConstraintButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button customLinesButton;
        private System.Windows.Forms.Button removeConstraintButton;
    }
}

