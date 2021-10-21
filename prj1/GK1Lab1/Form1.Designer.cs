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
            this.newConstraintParallel = new System.Windows.Forms.Button();
            this.newConstraintTangent = new System.Windows.Forms.Button();
            this.newConstraintEqualEdges = new System.Windows.Forms.Button();
            this.newConstraintLength = new System.Windows.Forms.Button();
            this.newConstraintRadius = new System.Windows.Forms.Button();
            this.antialiasing = new System.Windows.Forms.CheckBox();
            this.deleteFigureButton = new System.Windows.Forms.Button();
            this.newCircleButton = new System.Windows.Forms.Button();
            this.customLinesButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.deleteVertexButton = new System.Windows.Forms.Button();
            this.editPolygonButton = new System.Windows.Forms.Button();
            this.newPolygonButton = new System.Windows.Forms.Button();
            this.saveBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.importButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.pictureContainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.mainWind.Size = new System.Drawing.Size(601, 616);
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
            this.menuPanel.Location = new System.Drawing.Point(601, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.RowCount = 3;
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.menuPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.menuPanel.Size = new System.Drawing.Size(200, 616);
            this.menuPanel.TabIndex = 2;
            // 
            // editBox
            // 
            this.editBox.Controls.Add(this.panel2);
            this.editBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editBox.Location = new System.Drawing.Point(3, 3);
            this.editBox.Name = "editBox";
            this.editBox.Size = new System.Drawing.Size(194, 482);
            this.editBox.TabIndex = 0;
            this.editBox.TabStop = false;
            this.editBox.Text = "Menu";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.newConstraintParallel);
            this.panel2.Controls.Add(this.newConstraintTangent);
            this.panel2.Controls.Add(this.newConstraintEqualEdges);
            this.panel2.Controls.Add(this.newConstraintLength);
            this.panel2.Controls.Add(this.newConstraintRadius);
            this.panel2.Controls.Add(this.antialiasing);
            this.panel2.Controls.Add(this.deleteFigureButton);
            this.panel2.Controls.Add(this.newCircleButton);
            this.panel2.Controls.Add(this.customLinesButton);
            this.panel2.Controls.Add(this.moveButton);
            this.panel2.Controls.Add(this.deleteVertexButton);
            this.panel2.Controls.Add(this.editPolygonButton);
            this.panel2.Controls.Add(this.newPolygonButton);
            this.panel2.Location = new System.Drawing.Point(-3, 19);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 463);
            this.panel2.TabIndex = 4;
            // 
            // newConstraintParallel
            // 
            this.newConstraintParallel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newConstraintParallel.Location = new System.Drawing.Point(1, 196);
            this.newConstraintParallel.Name = "newConstraintParallel";
            this.newConstraintParallel.Size = new System.Drawing.Size(199, 34);
            this.newConstraintParallel.TabIndex = 20;
            this.newConstraintParallel.Text = "New Constraint - Parallel Edges";
            this.newConstraintParallel.UseVisualStyleBackColor = true;
            // 
            // newConstraintTangent
            // 
            this.newConstraintTangent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newConstraintTangent.Location = new System.Drawing.Point(0, 164);
            this.newConstraintTangent.Name = "newConstraintTangent";
            this.newConstraintTangent.Size = new System.Drawing.Size(199, 34);
            this.newConstraintTangent.TabIndex = 19;
            this.newConstraintTangent.Text = "New Constraint - Tangent edge";
            this.newConstraintTangent.UseVisualStyleBackColor = true;
            // 
            // newConstraintEqualEdges
            // 
            this.newConstraintEqualEdges.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newConstraintEqualEdges.Location = new System.Drawing.Point(0, 133);
            this.newConstraintEqualEdges.Name = "newConstraintEqualEdges";
            this.newConstraintEqualEdges.Size = new System.Drawing.Size(199, 34);
            this.newConstraintEqualEdges.TabIndex = 18;
            this.newConstraintEqualEdges.Text = "New Constraint - Equal Edges";
            this.newConstraintEqualEdges.UseVisualStyleBackColor = true;
            // 
            // newConstraintLength
            // 
            this.newConstraintLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newConstraintLength.Location = new System.Drawing.Point(0, 102);
            this.newConstraintLength.Name = "newConstraintLength";
            this.newConstraintLength.Size = new System.Drawing.Size(199, 34);
            this.newConstraintLength.TabIndex = 17;
            this.newConstraintLength.Text = "New Constraint - Set Length";
            this.newConstraintLength.UseVisualStyleBackColor = true;
            // 
            // newConstraintRadius
            // 
            this.newConstraintRadius.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newConstraintRadius.Location = new System.Drawing.Point(1, 71);
            this.newConstraintRadius.Name = "newConstraintRadius";
            this.newConstraintRadius.Size = new System.Drawing.Size(199, 34);
            this.newConstraintRadius.TabIndex = 16;
            this.newConstraintRadius.Text = "New Constraint - Set Radius";
            this.newConstraintRadius.UseVisualStyleBackColor = true;
            this.newConstraintRadius.Click += new System.EventHandler(this.newConstraintRadius_Click);
            // 
            // antialiasing
            // 
            this.antialiasing.AutoSize = true;
            this.antialiasing.Location = new System.Drawing.Point(3, 392);
            this.antialiasing.Name = "antialiasing";
            this.antialiasing.Size = new System.Drawing.Size(79, 17);
            this.antialiasing.TabIndex = 15;
            this.antialiasing.Text = "Antialiasing";
            this.antialiasing.UseVisualStyleBackColor = true;
            this.antialiasing.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // deleteFigureButton
            // 
            this.deleteFigureButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteFigureButton.Location = new System.Drawing.Point(0, 342);
            this.deleteFigureButton.Name = "deleteFigureButton";
            this.deleteFigureButton.Size = new System.Drawing.Size(202, 34);
            this.deleteFigureButton.TabIndex = 10;
            this.deleteFigureButton.Text = "Delete Figure";
            this.deleteFigureButton.UseVisualStyleBackColor = true;
            this.deleteFigureButton.Click += new System.EventHandler(this.deleteFigureButton_Click);
            // 
            // newCircleButton
            // 
            this.newCircleButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newCircleButton.Location = new System.Drawing.Point(1, 31);
            this.newCircleButton.Name = "newCircleButton";
            this.newCircleButton.Size = new System.Drawing.Size(199, 34);
            this.newCircleButton.TabIndex = 9;
            this.newCircleButton.Text = "New Circle";
            this.newCircleButton.UseVisualStyleBackColor = true;
            this.newCircleButton.Click += new System.EventHandler(this.newCircleButton_Click);
            // 
            // customLinesButton
            // 
            this.customLinesButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customLinesButton.Location = new System.Drawing.Point(3, 410);
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
            this.moveButton.Location = new System.Drawing.Point(0, 272);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(199, 34);
            this.moveButton.TabIndex = 6;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.moveButton_Click);
            // 
            // deleteVertexButton
            // 
            this.deleteVertexButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteVertexButton.Location = new System.Drawing.Point(0, 311);
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
            this.editPolygonButton.Location = new System.Drawing.Point(0, 241);
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
            // saveBox
            // 
            this.saveBox.Controls.Add(this.tableLayoutPanel3);
            this.saveBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveBox.Location = new System.Drawing.Point(3, 499);
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
            this.panel1.Size = new System.Drawing.Size(601, 616);
            this.panel1.TabIndex = 4;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Canvas file | *.canvas";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "SavedCanvas";
            this.openFileDialog.Filter = "Canvas file | *.canvas";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 616);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureContainer);
            this.Controls.Add(this.menuPanel);
            this.MinimumSize = new System.Drawing.Size(817, 655);
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
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Button deleteVertexButton;
        private System.Windows.Forms.Button editPolygonButton;
        private System.Windows.Forms.Button newPolygonButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button customLinesButton;
        private System.Windows.Forms.Button newCircleButton;
        private System.Windows.Forms.Button deleteFigureButton;
        private System.Windows.Forms.CheckBox antialiasing;
        private System.Windows.Forms.Button newConstraintParallel;
        private System.Windows.Forms.Button newConstraintTangent;
        private System.Windows.Forms.Button newConstraintEqualEdges;
        private System.Windows.Forms.Button newConstraintLength;
        private System.Windows.Forms.Button newConstraintRadius;
    }
}

