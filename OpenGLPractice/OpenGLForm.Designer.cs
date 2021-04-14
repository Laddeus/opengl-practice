using OpenGLPractice.GameObjects;

namespace OpenGLPractice
{
    public partial class OpenGLForm
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
            this.components = new System.ComponentModel.Container();
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.zLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GameLoopTimer = new System.Windows.Forms.Timer(this.components);
            this.panelGameObjectDetailsView = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.zScaleTextBox = new System.Windows.Forms.TextBox();
            this.gameObjectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonResetScale = new System.Windows.Forms.Button();
            this.yScaleTextBox = new System.Windows.Forms.TextBox();
            this.xScaleTextBox = new System.Windows.Forms.TextBox();
            this.labelScale = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.zPositionTextBox = new System.Windows.Forms.TextBox();
            this.buttonResetPosition = new System.Windows.Forms.Button();
            this.yPositionTextBox = new System.Windows.Forms.TextBox();
            this.xPositionTextBox = new System.Windows.Forms.TextBox();
            this.labelObjectPosition = new System.Windows.Forms.Label();
            this.localCoordinatesActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.labelGameObjectType = new System.Windows.Forms.Label();
            this.labelGameObjectName = new System.Windows.Forms.Label();
            this.textBoxGameObjectName = new System.Windows.Forms.TextBox();
            this.buttonAddGameObjectToScene = new System.Windows.Forms.Button();
            this.comboBoxGameObjects = new System.Windows.Forms.ComboBox();
            this.labelAddGameObject = new System.Windows.Forms.Label();
            this.GameScene = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelGameObjectsList = new System.Windows.Forms.Panel();
            this.listBoxGameObjects = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelGameObjectDetailsView.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameObjectBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelGameObjectsList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(20, 27);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(17, 13);
            this.xLabel.TabIndex = 2;
            this.xLabel.Text = "X:";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(20, 53);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(17, 13);
            this.yLabel.TabIndex = 4;
            this.yLabel.Text = "Y:";
            // 
            // zLabel
            // 
            this.zLabel.AutoSize = true;
            this.zLabel.Location = new System.Drawing.Point(20, 79);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(17, 13);
            this.zLabel.TabIndex = 6;
            this.zLabel.Text = "Z:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Z:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "X:";
            // 
            // GameLoopTimer
            // 
            this.GameLoopTimer.Enabled = true;
            this.GameLoopTimer.Interval = 16;
            this.GameLoopTimer.Tick += new System.EventHandler(this.GameLoopTimer_Tick);
            // 
            // panelGameObjectDetailsView
            // 
            this.panelGameObjectDetailsView.AutoScroll = true;
            this.panelGameObjectDetailsView.Controls.Add(this.panel2);
            this.panelGameObjectDetailsView.Controls.Add(this.panel1);
            this.panelGameObjectDetailsView.Controls.Add(this.localCoordinatesActiveCheckBox);
            this.panelGameObjectDetailsView.Controls.Add(this.labelGameObjectType);
            this.panelGameObjectDetailsView.Controls.Add(this.labelGameObjectName);
            this.panelGameObjectDetailsView.Controls.Add(this.textBoxGameObjectName);
            this.panelGameObjectDetailsView.Controls.Add(this.buttonAddGameObjectToScene);
            this.panelGameObjectDetailsView.Controls.Add(this.comboBoxGameObjects);
            this.panelGameObjectDetailsView.Controls.Add(this.labelAddGameObject);
            this.panelGameObjectDetailsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGameObjectDetailsView.Location = new System.Drawing.Point(0, 0);
            this.panelGameObjectDetailsView.Name = "panelGameObjectDetailsView";
            this.panelGameObjectDetailsView.Size = new System.Drawing.Size(196, 506);
            this.panelGameObjectDetailsView.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.zScaleTextBox);
            this.panel2.Controls.Add(this.buttonResetScale);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.yScaleTextBox);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.xScaleTextBox);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.labelScale);
            this.panel2.Location = new System.Drawing.Point(12, 170);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(181, 139);
            this.panel2.TabIndex = 18;
            // 
            // zScaleTextBox
            // 
            this.zScaleTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Scale.Z", true));
            this.zScaleTextBox.Location = new System.Drawing.Point(61, 76);
            this.zScaleTextBox.Name = "zScaleTextBox";
            this.zScaleTextBox.Size = new System.Drawing.Size(100, 20);
            this.zScaleTextBox.TabIndex = 7;
            this.zScaleTextBox.Tag = "Scale";
            this.zScaleTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // gameObjectBindingSource
            // 
            this.gameObjectBindingSource.DataSource = typeof(OpenGLPractice.GameObjects.GameObject);
            // 
            // buttonResetScale
            // 
            this.buttonResetScale.AutoSize = true;
            this.buttonResetScale.Location = new System.Drawing.Point(76, 102);
            this.buttonResetScale.Name = "buttonResetScale";
            this.buttonResetScale.Size = new System.Drawing.Size(85, 23);
            this.buttonResetScale.TabIndex = 16;
            this.buttonResetScale.Text = "Reset Scale";
            this.buttonResetScale.UseVisualStyleBackColor = true;
            this.buttonResetScale.Click += new System.EventHandler(this.buttonResetScale_Click);
            // 
            // yScaleTextBox
            // 
            this.yScaleTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Scale.Y", true));
            this.yScaleTextBox.Location = new System.Drawing.Point(61, 50);
            this.yScaleTextBox.Name = "yScaleTextBox";
            this.yScaleTextBox.Size = new System.Drawing.Size(100, 20);
            this.yScaleTextBox.TabIndex = 5;
            this.yScaleTextBox.Tag = "Scale";
            this.yScaleTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // xScaleTextBox
            // 
            this.xScaleTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Scale.X", true));
            this.xScaleTextBox.Location = new System.Drawing.Point(61, 24);
            this.xScaleTextBox.Name = "xScaleTextBox";
            this.xScaleTextBox.Size = new System.Drawing.Size(100, 20);
            this.xScaleTextBox.TabIndex = 3;
            this.xScaleTextBox.Tag = "Scale";
            this.xScaleTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // labelScale
            // 
            this.labelScale.AutoSize = true;
            this.labelScale.Location = new System.Drawing.Point(20, 8);
            this.labelScale.Name = "labelScale";
            this.labelScale.Size = new System.Drawing.Size(37, 13);
            this.labelScale.TabIndex = 8;
            this.labelScale.Text = "Scale:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.zPositionTextBox);
            this.panel1.Controls.Add(this.buttonResetPosition);
            this.panel1.Controls.Add(this.zLabel);
            this.panel1.Controls.Add(this.yPositionTextBox);
            this.panel1.Controls.Add(this.yLabel);
            this.panel1.Controls.Add(this.xPositionTextBox);
            this.panel1.Controls.Add(this.xLabel);
            this.panel1.Controls.Add(this.labelObjectPosition);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 139);
            this.panel1.TabIndex = 17;
            // 
            // zPositionTextBox
            // 
            this.zPositionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Position.Z", true));
            this.zPositionTextBox.Location = new System.Drawing.Point(61, 76);
            this.zPositionTextBox.Name = "zPositionTextBox";
            this.zPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.zPositionTextBox.TabIndex = 7;
            this.zPositionTextBox.Tag = "Position";
            this.zPositionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // buttonResetPosition
            // 
            this.buttonResetPosition.AutoSize = true;
            this.buttonResetPosition.Location = new System.Drawing.Point(76, 102);
            this.buttonResetPosition.Name = "buttonResetPosition";
            this.buttonResetPosition.Size = new System.Drawing.Size(85, 23);
            this.buttonResetPosition.TabIndex = 16;
            this.buttonResetPosition.Text = "Reset Position";
            this.buttonResetPosition.UseVisualStyleBackColor = true;
            this.buttonResetPosition.Click += new System.EventHandler(this.buttonResetPosition_Click);
            // 
            // yPositionTextBox
            // 
            this.yPositionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Position.Y", true));
            this.yPositionTextBox.Location = new System.Drawing.Point(61, 50);
            this.yPositionTextBox.Name = "yPositionTextBox";
            this.yPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.yPositionTextBox.TabIndex = 5;
            this.yPositionTextBox.Tag = "Position";
            this.yPositionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // xPositionTextBox
            // 
            this.xPositionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Position.X", true));
            this.xPositionTextBox.Location = new System.Drawing.Point(61, 24);
            this.xPositionTextBox.Name = "xPositionTextBox";
            this.xPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.xPositionTextBox.TabIndex = 3;
            this.xPositionTextBox.Tag = "Position";
            this.xPositionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // labelObjectPosition
            // 
            this.labelObjectPosition.AutoSize = true;
            this.labelObjectPosition.Location = new System.Drawing.Point(20, 8);
            this.labelObjectPosition.Name = "labelObjectPosition";
            this.labelObjectPosition.Size = new System.Drawing.Size(47, 13);
            this.labelObjectPosition.TabIndex = 8;
            this.labelObjectPosition.Text = "Position:";
            // 
            // localCoordinatesActiveCheckBox
            // 
            this.localCoordinatesActiveCheckBox.AutoSize = true;
            this.localCoordinatesActiveCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.gameObjectBindingSource, "LocalCoordinatesActive", true));
            this.localCoordinatesActiveCheckBox.Location = new System.Drawing.Point(15, 341);
            this.localCoordinatesActiveCheckBox.Name = "localCoordinatesActiveCheckBox";
            this.localCoordinatesActiveCheckBox.Size = new System.Drawing.Size(111, 17);
            this.localCoordinatesActiveCheckBox.TabIndex = 15;
            this.localCoordinatesActiveCheckBox.Text = "Local Coordinates";
            this.localCoordinatesActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // labelGameObjectType
            // 
            this.labelGameObjectType.AutoSize = true;
            this.labelGameObjectType.Location = new System.Drawing.Point(32, 428);
            this.labelGameObjectType.Name = "labelGameObjectType";
            this.labelGameObjectType.Size = new System.Drawing.Size(31, 13);
            this.labelGameObjectType.TabIndex = 14;
            this.labelGameObjectType.Text = "Type";
            // 
            // labelGameObjectName
            // 
            this.labelGameObjectName.AutoSize = true;
            this.labelGameObjectName.Location = new System.Drawing.Point(32, 402);
            this.labelGameObjectName.Name = "labelGameObjectName";
            this.labelGameObjectName.Size = new System.Drawing.Size(38, 13);
            this.labelGameObjectName.TabIndex = 13;
            this.labelGameObjectName.Text = "Name:";
            // 
            // textBoxGameObjectName
            // 
            this.textBoxGameObjectName.Location = new System.Drawing.Point(84, 399);
            this.textBoxGameObjectName.Name = "textBoxGameObjectName";
            this.textBoxGameObjectName.Size = new System.Drawing.Size(89, 20);
            this.textBoxGameObjectName.TabIndex = 12;
            // 
            // buttonAddGameObjectToScene
            // 
            this.buttonAddGameObjectToScene.AutoSize = true;
            this.buttonAddGameObjectToScene.Location = new System.Drawing.Point(88, 464);
            this.buttonAddGameObjectToScene.Name = "buttonAddGameObjectToScene";
            this.buttonAddGameObjectToScene.Size = new System.Drawing.Size(86, 23);
            this.buttonAddGameObjectToScene.TabIndex = 11;
            this.buttonAddGameObjectToScene.Text = "Add To Scene";
            this.buttonAddGameObjectToScene.UseVisualStyleBackColor = true;
            this.buttonAddGameObjectToScene.Click += new System.EventHandler(this.buttonAddGameObjectToScene_Click);
            // 
            // comboBoxGameObjects
            // 
            this.comboBoxGameObjects.FormattingEnabled = true;
            this.comboBoxGameObjects.Location = new System.Drawing.Point(84, 425);
            this.comboBoxGameObjects.Name = "comboBoxGameObjects";
            this.comboBoxGameObjects.Size = new System.Drawing.Size(89, 21);
            this.comboBoxGameObjects.TabIndex = 10;
            // 
            // labelAddGameObject
            // 
            this.labelAddGameObject.AutoSize = true;
            this.labelAddGameObject.Location = new System.Drawing.Point(32, 372);
            this.labelAddGameObject.Name = "labelAddGameObject";
            this.labelAddGameObject.Size = new System.Drawing.Size(94, 13);
            this.labelAddGameObject.TabIndex = 9;
            this.labelAddGameObject.Text = "Add Game Object:";
            // 
            // GameScene
            // 
            this.GameScene.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.GameScene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameScene.Location = new System.Drawing.Point(0, 0);
            this.GameScene.Name = "GameScene";
            this.GameScene.Size = new System.Drawing.Size(476, 506);
            this.GameScene.TabIndex = 6;
            this.GameScene.Click += new System.EventHandler(this.GameScene_Click);
            this.GameScene.Resize += new System.EventHandler(this.GameCanvas_Resize);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AccessibleName = string.Empty;
            this.splitContainer1.Panel1.Controls.Add(this.panelGameObjectsList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(860, 506);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 0;
            // 
            // panelGameObjectsList
            // 
            this.panelGameObjectsList.Controls.Add(this.listBoxGameObjects);
            this.panelGameObjectsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGameObjectsList.Location = new System.Drawing.Point(0, 0);
            this.panelGameObjectsList.Name = "panelGameObjectsList";
            this.panelGameObjectsList.Size = new System.Drawing.Size(180, 506);
            this.panelGameObjectsList.TabIndex = 0;
            // 
            // listBoxGameObjects
            // 
            this.listBoxGameObjects.DataSource = this.gameObjectBindingSource;
            this.listBoxGameObjects.DisplayMember = "Name";
            this.listBoxGameObjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxGameObjects.FormattingEnabled = true;
            this.listBoxGameObjects.Location = new System.Drawing.Point(0, 0);
            this.listBoxGameObjects.Name = "listBoxGameObjects";
            this.listBoxGameObjects.Size = new System.Drawing.Size(180, 506);
            this.listBoxGameObjects.TabIndex = 0;
            this.listBoxGameObjects.SelectedIndexChanged += new System.EventHandler(this.listBoxGameObjects_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.GameScene);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panelGameObjectDetailsView);
            this.splitContainer2.Size = new System.Drawing.Size(676, 506);
            this.splitContainer2.SplitterDistance = 476;
            this.splitContainer2.TabIndex = 0;
            // 
            // OpenGLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 506);
            this.Controls.Add(this.splitContainer1);
            this.Name = "OpenGLForm";
            this.Text = "Form1";
            this.panelGameObjectDetailsView.ResumeLayout(false);
            this.panelGameObjectDetailsView.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameObjectBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelGameObjectsList.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer GameLoopTimer;
        private System.Windows.Forms.Panel panelGameObjectDetailsView;
        private System.Windows.Forms.Panel GameScene;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelGameObjectsList;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox listBoxGameObjects;
        private System.Windows.Forms.BindingSource gameObjectBindingSource;
        private System.Windows.Forms.TextBox xPositionTextBox;
        private System.Windows.Forms.TextBox yPositionTextBox;
        private System.Windows.Forms.TextBox zPositionTextBox;
        private System.Windows.Forms.Button buttonAddGameObjectToScene;
        private System.Windows.Forms.ComboBox comboBoxGameObjects;
        private System.Windows.Forms.Label labelAddGameObject;
        private System.Windows.Forms.Label labelObjectPosition;
        private System.Windows.Forms.Label labelGameObjectType;
        private System.Windows.Forms.Label labelGameObjectName;
        private System.Windows.Forms.TextBox textBoxGameObjectName;
        private System.Windows.Forms.CheckBox localCoordinatesActiveCheckBox;
        private System.Windows.Forms.Button buttonResetPosition;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox zScaleTextBox;
        private System.Windows.Forms.Button buttonResetScale;
        private System.Windows.Forms.TextBox yScaleTextBox;
        private System.Windows.Forms.TextBox xScaleTextBox;
        private System.Windows.Forms.Label labelScale;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Label zLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}