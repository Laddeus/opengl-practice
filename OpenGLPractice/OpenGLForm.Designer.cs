using OpenGLPractice.Game;
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
            this.radioButtonLightControl = new System.Windows.Forms.RadioButton();
            this.radioButtonObjectSelected = new System.Windows.Forms.RadioButton();
            this.radioButtonFreeMovement = new System.Windows.Forms.RadioButton();
            this.localCoordinatesActiveCheckBox = new System.Windows.Forms.CheckBox();
            this.gameObjectBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cOGLBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelLightPosition = new System.Windows.Forms.Panel();
            this.xLightPositionTextBox = new System.Windows.Forms.TextBox();
            this.zLightPositionTextBox = new System.Windows.Forms.TextBox();
            this.labelZLightPosition = new System.Windows.Forms.Label();
            this.yLightPositionTextBox = new System.Windows.Forms.TextBox();
            this.labelYLightPosition = new System.Windows.Forms.Label();
            this.labelXLightPosition = new System.Windows.Forms.Label();
            this.labelLightPosition = new System.Windows.Forms.Label();
            this.panelCubemapSelection = new System.Windows.Forms.Panel();
            this.buttonApplyCubemap = new System.Windows.Forms.Button();
            this.comboBoxCubemapSelection = new System.Windows.Forms.ComboBox();
            this.labelSelectSkybox = new System.Windows.Forms.Label();
            this.panelGameObjectSceneAdd = new System.Windows.Forms.Panel();
            this.labelAddGameObject = new System.Windows.Forms.Label();
            this.comboBoxGameObjects = new System.Windows.Forms.ComboBox();
            this.buttonAddGameObjectToScene = new System.Windows.Forms.Button();
            this.labelGameObjectType = new System.Windows.Forms.Label();
            this.textBoxGameObjectName = new System.Windows.Forms.TextBox();
            this.labelGameObjectName = new System.Windows.Forms.Label();
            this.panelScaleInfoView = new System.Windows.Forms.Panel();
            this.zScaleTextBox = new System.Windows.Forms.TextBox();
            this.buttonResetScale = new System.Windows.Forms.Button();
            this.yScaleTextBox = new System.Windows.Forms.TextBox();
            this.xScaleTextBox = new System.Windows.Forms.TextBox();
            this.labelScale = new System.Windows.Forms.Label();
            this.panelPositionInfoView = new System.Windows.Forms.Panel();
            this.zPositionTextBox = new System.Windows.Forms.TextBox();
            this.buttonResetPosition = new System.Windows.Forms.Button();
            this.yPositionTextBox = new System.Windows.Forms.TextBox();
            this.xPositionTextBox = new System.Windows.Forms.TextBox();
            this.labelObjectPosition = new System.Windows.Forms.Label();
            this.GameScene = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelGameObjectsList = new System.Windows.Forms.Panel();
            this.listBoxGameObjects = new System.Windows.Forms.ListBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panelGameObjectDetailsView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameObjectBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cOGLBindingSource)).BeginInit();
            this.panelLightPosition.SuspendLayout();
            this.panelCubemapSelection.SuspendLayout();
            this.panelGameObjectSceneAdd.SuspendLayout();
            this.panelScaleInfoView.SuspendLayout();
            this.panelPositionInfoView.SuspendLayout();
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
            this.xLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(7, 27);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(17, 13);
            this.xLabel.TabIndex = 2;
            this.xLabel.Text = "X:";
            // 
            // yLabel
            // 
            this.yLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(7, 53);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(17, 13);
            this.yLabel.TabIndex = 4;
            this.yLabel.Text = "Y:";
            // 
            // zLabel
            // 
            this.zLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zLabel.AutoSize = true;
            this.zLabel.Location = new System.Drawing.Point(7, 79);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(17, 13);
            this.zLabel.TabIndex = 6;
            this.zLabel.Text = "Z:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Z:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 27);
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
            this.panelGameObjectDetailsView.Controls.Add(this.radioButtonLightControl);
            this.panelGameObjectDetailsView.Controls.Add(this.radioButtonObjectSelected);
            this.panelGameObjectDetailsView.Controls.Add(this.radioButtonFreeMovement);
            this.panelGameObjectDetailsView.Controls.Add(this.localCoordinatesActiveCheckBox);
            this.panelGameObjectDetailsView.Controls.Add(this.panelLightPosition);
            this.panelGameObjectDetailsView.Controls.Add(this.panelCubemapSelection);
            this.panelGameObjectDetailsView.Controls.Add(this.panelGameObjectSceneAdd);
            this.panelGameObjectDetailsView.Controls.Add(this.panelScaleInfoView);
            this.panelGameObjectDetailsView.Controls.Add(this.panelPositionInfoView);
            this.panelGameObjectDetailsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGameObjectDetailsView.Location = new System.Drawing.Point(0, 0);
            this.panelGameObjectDetailsView.Name = "panelGameObjectDetailsView";
            this.panelGameObjectDetailsView.Size = new System.Drawing.Size(192, 622);
            this.panelGameObjectDetailsView.TabIndex = 24;
            // 
            // radioButtonLightControl
            // 
            this.radioButtonLightControl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonLightControl.AutoSize = true;
            this.radioButtonLightControl.Location = new System.Drawing.Point(24, 360);
            this.radioButtonLightControl.Name = "radioButtonLightControl";
            this.radioButtonLightControl.Size = new System.Drawing.Size(84, 17);
            this.radioButtonLightControl.TabIndex = 25;
            this.radioButtonLightControl.Text = "Control Light";
            this.radioButtonLightControl.UseVisualStyleBackColor = true;
            // 
            // radioButtonObjectSelected
            // 
            this.radioButtonObjectSelected.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonObjectSelected.AutoSize = true;
            this.radioButtonObjectSelected.Location = new System.Drawing.Point(24, 337);
            this.radioButtonObjectSelected.Name = "radioButtonObjectSelected";
            this.radioButtonObjectSelected.Size = new System.Drawing.Size(103, 17);
            this.radioButtonObjectSelected.TabIndex = 24;
            this.radioButtonObjectSelected.Text = "Control Selected";
            this.radioButtonObjectSelected.UseVisualStyleBackColor = true;
            // 
            // radioButtonFreeMovement
            // 
            this.radioButtonFreeMovement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonFreeMovement.AutoSize = true;
            this.radioButtonFreeMovement.Checked = true;
            this.radioButtonFreeMovement.Location = new System.Drawing.Point(24, 314);
            this.radioButtonFreeMovement.Name = "radioButtonFreeMovement";
            this.radioButtonFreeMovement.Size = new System.Drawing.Size(99, 17);
            this.radioButtonFreeMovement.TabIndex = 23;
            this.radioButtonFreeMovement.TabStop = true;
            this.radioButtonFreeMovement.Text = "Free Movement";
            this.radioButtonFreeMovement.UseVisualStyleBackColor = true;
            // 
            // localCoordinatesActiveCheckBox
            // 
            this.localCoordinatesActiveCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.localCoordinatesActiveCheckBox.AutoSize = true;
            this.localCoordinatesActiveCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("CheckState", this.gameObjectBindingSource, "LocalCoordinatesActive", true));
            this.localCoordinatesActiveCheckBox.Location = new System.Drawing.Point(24, 291);
            this.localCoordinatesActiveCheckBox.Name = "localCoordinatesActiveCheckBox";
            this.localCoordinatesActiveCheckBox.Size = new System.Drawing.Size(125, 17);
            this.localCoordinatesActiveCheckBox.TabIndex = 15;
            this.localCoordinatesActiveCheckBox.Text = "Display Local Coords";
            this.localCoordinatesActiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // gameObjectBindingSource
            // 
            this.gameObjectBindingSource.AllowNew = true;
            this.gameObjectBindingSource.DataMember = "GameObjects";
            this.gameObjectBindingSource.DataSource = this.cOGLBindingSource;
            // 
            // cOGLBindingSource
            // 
            this.cOGLBindingSource.DataSource = typeof(OpenGLPractice.cOGL);
            // 
            // panelLightPosition
            // 
            this.panelLightPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLightPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLightPosition.Controls.Add(this.xLightPositionTextBox);
            this.panelLightPosition.Controls.Add(this.zLightPositionTextBox);
            this.panelLightPosition.Controls.Add(this.labelZLightPosition);
            this.panelLightPosition.Controls.Add(this.yLightPositionTextBox);
            this.panelLightPosition.Controls.Add(this.labelYLightPosition);
            this.panelLightPosition.Controls.Add(this.labelXLightPosition);
            this.panelLightPosition.Controls.Add(this.labelLightPosition);
            this.panelLightPosition.Location = new System.Drawing.Point(17, 604);
            this.panelLightPosition.Name = "panelLightPosition";
            this.panelLightPosition.Size = new System.Drawing.Size(154, 113);
            this.panelLightPosition.TabIndex = 19;
            // 
            // xLightPositionTextBox
            // 
            this.xLightPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xLightPositionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cOGLBindingSource, "Light.Position.X", true));
            this.xLightPositionTextBox.Location = new System.Drawing.Point(49, 25);
            this.xLightPositionTextBox.Name = "xLightPositionTextBox";
            this.xLightPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.xLightPositionTextBox.TabIndex = 9;
            // 
            // zLightPositionTextBox
            // 
            this.zLightPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zLightPositionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cOGLBindingSource, "Light.Position.Z", true));
            this.zLightPositionTextBox.Location = new System.Drawing.Point(49, 73);
            this.zLightPositionTextBox.Name = "zLightPositionTextBox";
            this.zLightPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.zLightPositionTextBox.TabIndex = 7;
            this.zLightPositionTextBox.Tag = "Light Position";
            this.zLightPositionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // labelZLightPosition
            // 
            this.labelZLightPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelZLightPosition.AutoSize = true;
            this.labelZLightPosition.Location = new System.Drawing.Point(12, 76);
            this.labelZLightPosition.Name = "labelZLightPosition";
            this.labelZLightPosition.Size = new System.Drawing.Size(17, 13);
            this.labelZLightPosition.TabIndex = 6;
            this.labelZLightPosition.Text = "Z:";
            // 
            // yLightPositionTextBox
            // 
            this.yLightPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yLightPositionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.cOGLBindingSource, "Light.Position.Y", true));
            this.yLightPositionTextBox.Location = new System.Drawing.Point(49, 49);
            this.yLightPositionTextBox.Name = "yLightPositionTextBox";
            this.yLightPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.yLightPositionTextBox.TabIndex = 5;
            this.yLightPositionTextBox.Tag = "Light Position";
            this.yLightPositionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // labelYLightPosition
            // 
            this.labelYLightPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelYLightPosition.AutoSize = true;
            this.labelYLightPosition.Location = new System.Drawing.Point(12, 52);
            this.labelYLightPosition.Name = "labelYLightPosition";
            this.labelYLightPosition.Size = new System.Drawing.Size(17, 13);
            this.labelYLightPosition.TabIndex = 4;
            this.labelYLightPosition.Text = "Y:";
            // 
            // labelXLightPosition
            // 
            this.labelXLightPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelXLightPosition.AutoSize = true;
            this.labelXLightPosition.Location = new System.Drawing.Point(12, 28);
            this.labelXLightPosition.Name = "labelXLightPosition";
            this.labelXLightPosition.Size = new System.Drawing.Size(17, 13);
            this.labelXLightPosition.TabIndex = 2;
            this.labelXLightPosition.Text = "X:";
            // 
            // labelLightPosition
            // 
            this.labelLightPosition.AutoSize = true;
            this.labelLightPosition.Location = new System.Drawing.Point(-1, -1);
            this.labelLightPosition.Name = "labelLightPosition";
            this.labelLightPosition.Size = new System.Drawing.Size(70, 13);
            this.labelLightPosition.TabIndex = 8;
            this.labelLightPosition.Text = "Light Position";
            // 
            // panelCubemapSelection
            // 
            this.panelCubemapSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelCubemapSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCubemapSelection.Controls.Add(this.buttonApplyCubemap);
            this.panelCubemapSelection.Controls.Add(this.comboBoxCubemapSelection);
            this.panelCubemapSelection.Controls.Add(this.labelSelectSkybox);
            this.panelCubemapSelection.Location = new System.Drawing.Point(18, 499);
            this.panelCubemapSelection.Name = "panelCubemapSelection";
            this.panelCubemapSelection.Size = new System.Drawing.Size(153, 91);
            this.panelCubemapSelection.TabIndex = 21;
            // 
            // buttonApplyCubemap
            // 
            this.buttonApplyCubemap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApplyCubemap.AutoSize = true;
            this.buttonApplyCubemap.Location = new System.Drawing.Point(49, 59);
            this.buttonApplyCubemap.Name = "buttonApplyCubemap";
            this.buttonApplyCubemap.Size = new System.Drawing.Size(91, 23);
            this.buttonApplyCubemap.TabIndex = 12;
            this.buttonApplyCubemap.Text = "Apply Cubemap";
            this.buttonApplyCubemap.UseVisualStyleBackColor = true;
            this.buttonApplyCubemap.Click += new System.EventHandler(this.buttonApplyCubemap_Click);
            // 
            // comboBoxCubemapSelection
            // 
            this.comboBoxCubemapSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCubemapSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCubemapSelection.FormattingEnabled = true;
            this.comboBoxCubemapSelection.Location = new System.Drawing.Point(49, 32);
            this.comboBoxCubemapSelection.MaxDropDownItems = 16;
            this.comboBoxCubemapSelection.Name = "comboBoxCubemapSelection";
            this.comboBoxCubemapSelection.Size = new System.Drawing.Size(89, 21);
            this.comboBoxCubemapSelection.TabIndex = 11;
            // 
            // labelSelectSkybox
            // 
            this.labelSelectSkybox.AutoSize = true;
            this.labelSelectSkybox.Location = new System.Drawing.Point(-1, 0);
            this.labelSelectSkybox.Name = "labelSelectSkybox";
            this.labelSelectSkybox.Size = new System.Drawing.Size(88, 13);
            this.labelSelectSkybox.TabIndex = 0;
            this.labelSelectSkybox.Text = "Select Cubemap:";
            // 
            // panelGameObjectSceneAdd
            // 
            this.panelGameObjectSceneAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelGameObjectSceneAdd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGameObjectSceneAdd.Controls.Add(this.labelAddGameObject);
            this.panelGameObjectSceneAdd.Controls.Add(this.comboBoxGameObjects);
            this.panelGameObjectSceneAdd.Controls.Add(this.buttonAddGameObjectToScene);
            this.panelGameObjectSceneAdd.Controls.Add(this.labelGameObjectType);
            this.panelGameObjectSceneAdd.Controls.Add(this.textBoxGameObjectName);
            this.panelGameObjectSceneAdd.Controls.Add(this.labelGameObjectName);
            this.panelGameObjectSceneAdd.Location = new System.Drawing.Point(18, 380);
            this.panelGameObjectSceneAdd.Name = "panelGameObjectSceneAdd";
            this.panelGameObjectSceneAdd.Size = new System.Drawing.Size(154, 113);
            this.panelGameObjectSceneAdd.TabIndex = 19;
            // 
            // labelAddGameObject
            // 
            this.labelAddGameObject.AutoSize = true;
            this.labelAddGameObject.Location = new System.Drawing.Point(1, 0);
            this.labelAddGameObject.Name = "labelAddGameObject";
            this.labelAddGameObject.Size = new System.Drawing.Size(91, 13);
            this.labelAddGameObject.TabIndex = 9;
            this.labelAddGameObject.Text = "Add Game Object";
            // 
            // comboBoxGameObjects
            // 
            this.comboBoxGameObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxGameObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGameObjects.FormattingEnabled = true;
            this.comboBoxGameObjects.Location = new System.Drawing.Point(53, 55);
            this.comboBoxGameObjects.Name = "comboBoxGameObjects";
            this.comboBoxGameObjects.Size = new System.Drawing.Size(89, 21);
            this.comboBoxGameObjects.TabIndex = 10;
            // 
            // buttonAddGameObjectToScene
            // 
            this.buttonAddGameObjectToScene.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddGameObjectToScene.AutoSize = true;
            this.buttonAddGameObjectToScene.Location = new System.Drawing.Point(57, 82);
            this.buttonAddGameObjectToScene.Name = "buttonAddGameObjectToScene";
            this.buttonAddGameObjectToScene.Size = new System.Drawing.Size(86, 23);
            this.buttonAddGameObjectToScene.TabIndex = 11;
            this.buttonAddGameObjectToScene.Text = "Add To Scene";
            this.buttonAddGameObjectToScene.UseVisualStyleBackColor = true;
            this.buttonAddGameObjectToScene.Click += new System.EventHandler(this.buttonAddGameObjectToScene_Click);
            // 
            // labelGameObjectType
            // 
            this.labelGameObjectType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGameObjectType.AutoSize = true;
            this.labelGameObjectType.Location = new System.Drawing.Point(1, 58);
            this.labelGameObjectType.Name = "labelGameObjectType";
            this.labelGameObjectType.Size = new System.Drawing.Size(31, 13);
            this.labelGameObjectType.TabIndex = 14;
            this.labelGameObjectType.Text = "Type";
            // 
            // textBoxGameObjectName
            // 
            this.textBoxGameObjectName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxGameObjectName.Location = new System.Drawing.Point(53, 29);
            this.textBoxGameObjectName.Name = "textBoxGameObjectName";
            this.textBoxGameObjectName.Size = new System.Drawing.Size(89, 20);
            this.textBoxGameObjectName.TabIndex = 12;
            // 
            // labelGameObjectName
            // 
            this.labelGameObjectName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGameObjectName.AutoSize = true;
            this.labelGameObjectName.Location = new System.Drawing.Point(1, 32);
            this.labelGameObjectName.Name = "labelGameObjectName";
            this.labelGameObjectName.Size = new System.Drawing.Size(38, 13);
            this.labelGameObjectName.TabIndex = 13;
            this.labelGameObjectName.Text = "Name:";
            // 
            // panelScaleInfoView
            // 
            this.panelScaleInfoView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelScaleInfoView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelScaleInfoView.Controls.Add(this.zScaleTextBox);
            this.panelScaleInfoView.Controls.Add(this.buttonResetScale);
            this.panelScaleInfoView.Controls.Add(this.label1);
            this.panelScaleInfoView.Controls.Add(this.yScaleTextBox);
            this.panelScaleInfoView.Controls.Add(this.label2);
            this.panelScaleInfoView.Controls.Add(this.xScaleTextBox);
            this.panelScaleInfoView.Controls.Add(this.label3);
            this.panelScaleInfoView.Controls.Add(this.labelScale);
            this.panelScaleInfoView.Location = new System.Drawing.Point(24, 150);
            this.panelScaleInfoView.Name = "panelScaleInfoView";
            this.panelScaleInfoView.Size = new System.Drawing.Size(153, 139);
            this.panelScaleInfoView.TabIndex = 18;
            // 
            // zScaleTextBox
            // 
            this.zScaleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zScaleTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Scale.Z", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.zScaleTextBox.Location = new System.Drawing.Point(46, 76);
            this.zScaleTextBox.Name = "zScaleTextBox";
            this.zScaleTextBox.Size = new System.Drawing.Size(100, 20);
            this.zScaleTextBox.TabIndex = 7;
            this.zScaleTextBox.Tag = "GameObject Scale";
            this.zScaleTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // buttonResetScale
            // 
            this.buttonResetScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetScale.AutoSize = true;
            this.buttonResetScale.Location = new System.Drawing.Point(61, 102);
            this.buttonResetScale.Name = "buttonResetScale";
            this.buttonResetScale.Size = new System.Drawing.Size(85, 23);
            this.buttonResetScale.TabIndex = 16;
            this.buttonResetScale.Text = "Reset Scale";
            this.buttonResetScale.UseVisualStyleBackColor = true;
            this.buttonResetScale.Click += new System.EventHandler(this.buttonResetScale_Click);
            // 
            // yScaleTextBox
            // 
            this.yScaleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yScaleTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Scale.Y", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.yScaleTextBox.Location = new System.Drawing.Point(46, 50);
            this.yScaleTextBox.Name = "yScaleTextBox";
            this.yScaleTextBox.Size = new System.Drawing.Size(100, 20);
            this.yScaleTextBox.TabIndex = 5;
            this.yScaleTextBox.Tag = "GameObject Scale";
            this.yScaleTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // xScaleTextBox
            // 
            this.xScaleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xScaleTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Scale.X", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.xScaleTextBox.Location = new System.Drawing.Point(46, 24);
            this.xScaleTextBox.Name = "xScaleTextBox";
            this.xScaleTextBox.Size = new System.Drawing.Size(100, 20);
            this.xScaleTextBox.TabIndex = 3;
            this.xScaleTextBox.Tag = "GameObject Scale";
            this.xScaleTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // labelScale
            // 
            this.labelScale.AutoSize = true;
            this.labelScale.Location = new System.Drawing.Point(3, 0);
            this.labelScale.Name = "labelScale";
            this.labelScale.Size = new System.Drawing.Size(113, 13);
            this.labelScale.TabIndex = 8;
            this.labelScale.Text = "Selected Object Scale";
            // 
            // panelPositionInfoView
            // 
            this.panelPositionInfoView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPositionInfoView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPositionInfoView.Controls.Add(this.zPositionTextBox);
            this.panelPositionInfoView.Controls.Add(this.buttonResetPosition);
            this.panelPositionInfoView.Controls.Add(this.zLabel);
            this.panelPositionInfoView.Controls.Add(this.yPositionTextBox);
            this.panelPositionInfoView.Controls.Add(this.yLabel);
            this.panelPositionInfoView.Controls.Add(this.xPositionTextBox);
            this.panelPositionInfoView.Controls.Add(this.xLabel);
            this.panelPositionInfoView.Controls.Add(this.labelObjectPosition);
            this.panelPositionInfoView.Location = new System.Drawing.Point(23, 12);
            this.panelPositionInfoView.Name = "panelPositionInfoView";
            this.panelPositionInfoView.Size = new System.Drawing.Size(154, 132);
            this.panelPositionInfoView.TabIndex = 17;
            // 
            // zPositionTextBox
            // 
            this.zPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zPositionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Position.Z", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.zPositionTextBox.Location = new System.Drawing.Point(48, 76);
            this.zPositionTextBox.Name = "zPositionTextBox";
            this.zPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.zPositionTextBox.TabIndex = 7;
            this.zPositionTextBox.Tag = "GameObject Position";
            this.zPositionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // buttonResetPosition
            // 
            this.buttonResetPosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResetPosition.AutoSize = true;
            this.buttonResetPosition.Location = new System.Drawing.Point(63, 102);
            this.buttonResetPosition.Name = "buttonResetPosition";
            this.buttonResetPosition.Size = new System.Drawing.Size(85, 23);
            this.buttonResetPosition.TabIndex = 16;
            this.buttonResetPosition.Text = "Reset Position";
            this.buttonResetPosition.UseVisualStyleBackColor = true;
            this.buttonResetPosition.Click += new System.EventHandler(this.buttonResetPosition_Click);
            // 
            // yPositionTextBox
            // 
            this.yPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yPositionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Position.Y", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.yPositionTextBox.Location = new System.Drawing.Point(48, 50);
            this.yPositionTextBox.Name = "yPositionTextBox";
            this.yPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.yPositionTextBox.TabIndex = 5;
            this.yPositionTextBox.Tag = "GameObject Position";
            this.yPositionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // xPositionTextBox
            // 
            this.xPositionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xPositionTextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.gameObjectBindingSource, "Transform.Position.X", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.xPositionTextBox.Location = new System.Drawing.Point(48, 24);
            this.xPositionTextBox.Name = "xPositionTextBox";
            this.xPositionTextBox.Size = new System.Drawing.Size(100, 20);
            this.xPositionTextBox.TabIndex = 3;
            this.xPositionTextBox.Tag = "GameObject Position";
            this.xPositionTextBox.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // labelObjectPosition
            // 
            this.labelObjectPosition.AutoSize = true;
            this.labelObjectPosition.Location = new System.Drawing.Point(2, 0);
            this.labelObjectPosition.Name = "labelObjectPosition";
            this.labelObjectPosition.Size = new System.Drawing.Size(123, 13);
            this.labelObjectPosition.TabIndex = 8;
            this.labelObjectPosition.Text = "Selected Object Position";
            // 
            // GameScene
            // 
            this.GameScene.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.GameScene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameScene.Location = new System.Drawing.Point(0, 0);
            this.GameScene.Name = "GameScene";
            this.GameScene.Size = new System.Drawing.Size(505, 622);
            this.GameScene.TabIndex = 6;
            this.GameScene.Tag = "GameObject Scale";
            this.GameScene.Click += new System.EventHandler(this.GameScene_Click);
            this.GameScene.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GameScene_MouseDown);
            this.GameScene.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GameScene_MouseMove);
            this.GameScene.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GameScene_MouseUp);
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
            this.splitContainer1.Panel1.AccessibleName = "";
            this.splitContainer1.Panel1.Controls.Add(this.panelGameObjectsList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(784, 622);
            this.splitContainer1.SplitterDistance = 79;
            this.splitContainer1.TabIndex = 0;
            // 
            // panelGameObjectsList
            // 
            this.panelGameObjectsList.Controls.Add(this.listBoxGameObjects);
            this.panelGameObjectsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGameObjectsList.Location = new System.Drawing.Point(0, 0);
            this.panelGameObjectsList.Name = "panelGameObjectsList";
            this.panelGameObjectsList.Size = new System.Drawing.Size(79, 622);
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
            this.listBoxGameObjects.Size = new System.Drawing.Size(79, 622);
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
            this.splitContainer2.Size = new System.Drawing.Size(701, 622);
            this.splitContainer2.SplitterDistance = 505;
            this.splitContainer2.TabIndex = 0;
            // 
            // OpenGLForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 622);
            this.Controls.Add(this.splitContainer1);
            this.Name = "OpenGLForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.OpenGLForm_Load);
            this.panelGameObjectDetailsView.ResumeLayout(false);
            this.panelGameObjectDetailsView.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gameObjectBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cOGLBindingSource)).EndInit();
            this.panelLightPosition.ResumeLayout(false);
            this.panelLightPosition.PerformLayout();
            this.panelCubemapSelection.ResumeLayout(false);
            this.panelCubemapSelection.PerformLayout();
            this.panelGameObjectSceneAdd.ResumeLayout(false);
            this.panelGameObjectSceneAdd.PerformLayout();
            this.panelScaleInfoView.ResumeLayout(false);
            this.panelScaleInfoView.PerformLayout();
            this.panelPositionInfoView.ResumeLayout(false);
            this.panelPositionInfoView.PerformLayout();
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
        private System.Windows.Forms.Panel panelScaleInfoView;
        private System.Windows.Forms.TextBox zScaleTextBox;
        private System.Windows.Forms.Button buttonResetScale;
        private System.Windows.Forms.TextBox yScaleTextBox;
        private System.Windows.Forms.TextBox xScaleTextBox;
        private System.Windows.Forms.Label labelScale;
        private System.Windows.Forms.Panel panelPositionInfoView;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Label zLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panelGameObjectSceneAdd;
        private System.Windows.Forms.BindingSource cOGLBindingSource;
        private System.Windows.Forms.Panel panelCubemapSelection;
        private System.Windows.Forms.Label labelSelectSkybox;
        private System.Windows.Forms.Button buttonApplyCubemap;
        private System.Windows.Forms.ComboBox comboBoxCubemapSelection;
        private System.Windows.Forms.Panel panelLightPosition;
        private System.Windows.Forms.TextBox zLightPositionTextBox;
        private System.Windows.Forms.Label labelZLightPosition;
        private System.Windows.Forms.TextBox yLightPositionTextBox;
        private System.Windows.Forms.Label labelYLightPosition;
        private System.Windows.Forms.Label labelXLightPosition;
        private System.Windows.Forms.Label labelLightPosition;
        private System.Windows.Forms.RadioButton radioButtonLightControl;
        private System.Windows.Forms.RadioButton radioButtonObjectSelected;
        private System.Windows.Forms.RadioButton radioButtonFreeMovement;
        private System.Windows.Forms.TextBox xLightPositionTextBox;
    }
}