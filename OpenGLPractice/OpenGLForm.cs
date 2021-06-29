using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;
using OpenGLPractice.OpenGLUtilities;

namespace OpenGLPractice
{
    public partial class OpenGLForm : Form
    {
        private const string k_CubemapTexturePath = @"Resources/Textures/Cubemaps";
        private const float k_MouseMovementSensitivity = 1.25f;
        private readonly Queue<Keys> r_KeysPressed = new Queue<Keys>();
        private readonly string r_ProjectDirectoryLocation;
        private readonly Dictionary<Keys, Action> r_GameObjectActionKeys;
        private readonly Dictionary<Keys, Action> r_CameraActionKeys;
        private readonly Dictionary<Keys, Action> r_FreeMovementActionKeys;
        private readonly HashSet<Keys> r_AllowedKeys = new HashSet<Keys>()
        {
            Keys.W,
            Keys.S,
            Keys.A,
            Keys.D,
            Keys.Z,
            Keys.C,
            Keys.U,
            Keys.J,
            Keys.K,
            Keys.H,
            Keys.Q,
            Keys.E,
            Keys.R,
            Keys.F
        };

        private readonly cOGL cGL;
        private bool m_IsMousePressed = false;
        private Vector2 m_LastGameSceneClickPosition;
        private Vector3 m_LastPositionOfSelectedGameObject;
        private bool m_GameStarted = false;

        public OpenGLForm()
        {
            InitializeComponent();
            cGL = new cOGL(GameScene);

            ActiveControl = GameScene;
            GameScene.KeyDown += GameScene_KeyDown;
            GameScene.MouseWheel += GameScene_MouseWheel;

            r_GameObjectActionKeys = new Dictionary<Keys, Action>()
            {
                { Keys.W, () => cGL.SelectedGameObjectForControl.Transform.Translate(0.25f * cGL.SelectedGameObjectForControl.Transform.ForwardVector) },
                { Keys.S, () => cGL.SelectedGameObjectForControl.Transform.Translate(0.25f * cGL.SelectedGameObjectForControl.Transform.BackwardVector) },
                { Keys.A, () => cGL.SelectedGameObjectForControl.Transform.Translate(0.25f * cGL.SelectedGameObjectForControl.Transform.LeftVector) },
                { Keys.D, () => cGL.SelectedGameObjectForControl.Transform.Translate(0.25f * cGL.SelectedGameObjectForControl.Transform.RightVector) },
                { Keys.Z, () => cGL.SelectedGameObjectForControl.Transform.Rotate(2, cGL.SelectedGameObjectForControl.Transform.UpVector) },
                { Keys.C, () => cGL.SelectedGameObjectForControl.Transform.Rotate(-2, cGL.SelectedGameObjectForControl.Transform.UpVector) },
                { Keys.U, () => cGL.SelectedGameObjectForControl.Transform.Rotate(2, cGL.SelectedGameObjectForControl.Transform.RightVector) },
                { Keys.J, () => cGL.SelectedGameObjectForControl.Transform.Rotate(-2, cGL.SelectedGameObjectForControl.Transform.RightVector) },
                { Keys.K, () => cGL.SelectedGameObjectForControl.Transform.Rotate(2, cGL.SelectedGameObjectForControl.Transform.ForwardVector) },
                { Keys.H, () => cGL.SelectedGameObjectForControl.Transform.Rotate(-2, cGL.SelectedGameObjectForControl.Transform.ForwardVector) },
            };

            r_CameraActionKeys = new Dictionary<Keys, Action>()
            {
                { Keys.Q, () => cGL.Camera.YawAngle -= 2 },
                { Keys.E, () => cGL.Camera.YawAngle += 2 },
                { Keys.R, () => cGL.Camera.PitchAngle -= 2 },
                { Keys.F, () => cGL.Camera.PitchAngle += 2 }
            };

            r_FreeMovementActionKeys = new Dictionary<Keys, Action>()
            {
                { Keys.W, () => cGL.Camera.EyePosition += 0.25f * cGL.Camera.Direction },
                { Keys.S, () => cGL.Camera.EyePosition += 0.25f * -cGL.Camera.Direction },
                { Keys.A, () => cGL.Camera.EyePosition += 0.25f * cGL.Camera.UpVector.CrossProduct(cGL.Camera.Direction).Normalized },
                { Keys.D, () => cGL.Camera.EyePosition += 0.25f * cGL.Camera.Direction.CrossProduct(cGL.Camera.UpVector).Normalized }
            };

            r_ProjectDirectoryLocation = getProjectDirectory();

            cOGLBindingSource.DataSource = cGL;

            m_LastGameSceneClickPosition = new Vector2(GameScene.Width / 2.0f, GameScene.Height / 2.0f);
        }

        // EVENTS
        private void OpenGLForm_Load(object sender, EventArgs e)
        {
            loadGameObjectsCombobox();
            loadCubemapCombobox();
        }

        private void GameScene_MouseWheel(object i_Sender, MouseEventArgs i_MouseEventArgs)
        {
            changeSelectedGameObjectZoomLevel(i_MouseEventArgs);
        }

        private void GameScene_KeyDown(object i_Sender, KeyEventArgs i_KeyEventArgs)
        {
            addPressedKeyToQueue(i_KeyEventArgs);
        }

        private void GameCanvas_Resize(object sender, EventArgs e)
        {
            if(cGL != null)
            {
                cGL.OnResize();
            }
        }

        private void GameLoopTimer_Tick(object sender, EventArgs e)
        {
            update(GameLoopTimer.Interval / 1000.0f);
            cGL.Draw();
        }

        private void buttonAddGameObjectToScene_Click(object sender, EventArgs e)
        {
            addSelectedGameObjectToScene();
        }

        private void listBoxGameObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeSelectedGameObject();
        }

        private void GameScene_Click(object sender, EventArgs e)
        {
            GameScene.Focus();
        }

        private void buttonResetPosition_Click(object sender, EventArgs e)
        {
            resetSelectedGameObjectPosition();
        }

        private void buttonResetScale_Click(object sender, EventArgs e)
        {
            resetSelectedGameObjectScale();
        }

        private void GameScene_MouseDown(object sender, MouseEventArgs e)
        {
            m_IsMousePressed = true;
        }

        private void GameScene_MouseUp(object sender, MouseEventArgs e)
        {
            m_IsMousePressed = false;
        }

        private void GameScene_MouseMove(object sender, MouseEventArgs e)
        {
            updateMouseMovement(e);
        }

        private void buttonApplyCubemap_Click(object sender, EventArgs e)
        {
            applySelectedCubemapToWorldCube();
        }

        private void toolStripMenuItemRemoveGameObject_Click(object sender, EventArgs e)
        {
            removeSelectedGameObject();
        }

        private void buttonStartReset_Click(object sender, EventArgs e)
        {
            startResetGame();
        }

        // PRIVATE METHODS
        private void loadGameObjectsCombobox()
        {
            object[] allGameObjectTypeNames = GameObjectCreator.GetAllGameObjectTypeNames();
            comboBoxGameObjects.Items.AddRange(allGameObjectTypeNames);
        }

        private void loadCubemapCombobox()
        {
            string cubemapTexturesDirectory = $"{r_ProjectDirectoryLocation}/{k_CubemapTexturePath}";
            DirectoryInfo cubemapDirectoryInfo = new DirectoryInfo(cubemapTexturesDirectory);

            foreach (DirectoryInfo textureDirectory in cubemapDirectoryInfo.GetDirectories())
            {
                FileInfo firstFileInfo = textureDirectory.GetFiles()[0];
                comboBoxCubemapSelection.Items.Add(new CubemapTexture(textureDirectory.Name, textureDirectory.FullName, firstFileInfo.Extension));
            }
        }

        private void sortObjectsByDistanceFromCameraEye()
        {
            //Vector3 cameraEyePosition = cGL.Camera.EyePosition;

            //cGL.GameObjects.Sort((i_FirstGameObject, i_SecondGameObject) =>
            //{
            //    Vector3 firstGameObjectPosition = i_FirstGameObject.Transform.Position;
            //    Vector3 secondGameObjectPosition = i_SecondGameObject.Transform.Position;

            //    if (firstGameObjectPosition.SquaredDistance(cameraEyePosition) <
            //        secondGameObjectPosition.SquaredDistance(cameraEyePosition))
            //    {
            //        return 1;
            //    }

            //    return -1;
            //});
        }

        private void changeSelectedGameObjectZoomLevel(MouseEventArgs i_MouseEventArgs)
        {
            cGL.Camera.Radius += 0.25f * (i_MouseEventArgs.Delta / -120.0f);
        }

        private void addPressedKeyToQueue(KeyEventArgs i_KeyEventArgs)
        {
            Keys currentlyPressedKey = i_KeyEventArgs.KeyCode;

            if (r_AllowedKeys.Contains(currentlyPressedKey))
            {
                r_KeysPressed.Enqueue(i_KeyEventArgs.KeyCode);
            }

            i_KeyEventArgs.Handled = true;
        }

        private void update(float i_DeltaTime)
        {
            updateKeyPress();

            updateCameraView();

            keepCubemapCenteredAroundView();

            if (radioButtonLightControl.Checked)
            {
                updateLightPosition();
            }

            cGL.GameEnvironment.Update(i_DeltaTime);

            foreach (GameObject gameObject in cGL.GameObjects)
            {
                gameObject.Tick(i_DeltaTime);
            }
        }

        private void updateLightPosition()
        {
            cGL.Light.Position = cGL.Camera.EyePosition;
            this.Invoke(new Action(() => cOGLBindingSource.ResetCurrentItem()));
        }

        private void keepCubemapCenteredAroundView()
        {
            //if (radioButtonObjectSelected.Checked)
            //{
            //    cGL.WorldCube.Transform.Position = cGL.Camera.LookAtPosition;
            //}
            //else
            {
                cGL.WorldCube.Transform.Position = cGL.Camera.EyePosition;
            }
        }

        private void updateCameraView()
        {
            if (radioButtonObjectSelected.Checked && cGL.SelectedGameObjectForControl != null)
            {
                cGL.Camera.LookAtPosition = cGL.SelectedGameObjectForControl.Transform.Position;
                cGL.Camera.UpdateCameraPositionAroundLockedObject();
            }
            else
            {
                cGL.Camera.UpdateCameraFreeMovementDirection();
            }
        }

        private void updateKeyPress()
        {
            if (r_KeysPressed.Count > 0)
            {
                Keys lastKeyPressed = r_KeysPressed.Dequeue();

                if (radioButtonObjectSelected.Checked)
                {
                    updateGameObjectActions(lastKeyPressed);
                }
                else
                {
                    updateFreeMovementActions(lastKeyPressed);
                }

                updateCameraActions(lastKeyPressed);
            }

            if (m_LastPositionOfSelectedGameObject != cGL.SelectedGameObjectForControl?.Transform.Position)
            {
                this.Invoke(new Action(() => gameObjectBindingSource.ResetCurrentItem()));
            }

            m_LastPositionOfSelectedGameObject = cGL.SelectedGameObjectForControl.Transform.Position;
        }

        private void updateFreeMovementActions(Keys i_LastKeyPressed)
        {
            Action freeMovementAction;
            bool KeyHasAction = r_FreeMovementActionKeys.TryGetValue(i_LastKeyPressed, out freeMovementAction);

            if (KeyHasAction && cGL.Camera != null)
            {
                freeMovementAction.Invoke();
            }
        }

        private void updateCameraActions(Keys i_LastKeyPressed)
        {
            Action cameraAction;
            bool KeyHasAction = r_CameraActionKeys.TryGetValue(i_LastKeyPressed, out cameraAction);

            if (KeyHasAction && cGL.Camera != null)
            {
                cameraAction.Invoke();
            }
        }

        private void updateGameObjectActions(Keys i_LastKeyPressed)
        {
            Action gameObjectAction;
            bool KeyHasAction = r_GameObjectActionKeys.TryGetValue(i_LastKeyPressed, out gameObjectAction);

            if (KeyHasAction && cGL.SelectedGameObjectForControl != null)
            {
                gameObjectAction.Invoke();
                this.Invoke(new Action(() => gameObjectBindingSource.ResetCurrentItem()));
            }
        }

        private void addSelectedGameObjectToScene()
        {
            string gameObjectName = textBoxGameObjectName.Text;
            string gameObjectSelected = comboBoxGameObjects.SelectedItem?.ToString();

            try
            {
                GameObject gameObjectCreated = GameObjectCreator.CreateGameObjectDefault(gameObjectSelected, gameObjectName);
                gameObjectBindingSource.Add(gameObjectCreated);
            }
            catch
            {
                MessageBox.Show($"Cannot create default GameObject {gameObjectName}");
            }
        }

        private void changeSelectedGameObject()
        {
            if (cGL != null && gameObjectBindingSource.Current != null)
            {
                cGL.SelectedGameObjectForControl = gameObjectBindingSource.Current as GameObject;
                m_LastPositionOfSelectedGameObject = cGL.SelectedGameObjectForControl.Transform.Position;
                GameScene.Focus();
            }
        }

        private void resetSelectedGameObjectPosition()
        {
            if (cGL.SelectedGameObjectForControl != null)
            {
                cGL.SelectedGameObjectForControl.Transform.Position = Vector3.Zero;
                this.Invoke(new Action(() => gameObjectBindingSource.ResetCurrentItem()));
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            applyTransformationOnSelectedGameObject(textBox);
        }

        private void applyTransformationOnSelectedGameObject(TextBox i_TextBox)
        {
            if (i_TextBox != null && i_TextBox.Focused && cGL.SelectedGameObjectForControl != null)
            {
                bool isValidNumber = float.TryParse(i_TextBox.Text, out float result);

                if (isValidNumber && result != 0)
                {
                    Panel parentPanelOfVectorTextBoxes = i_TextBox.Parent as Panel;
                    Vector3 newVectorFromTextBoxes = getVectorFromPanel(parentPanelOfVectorTextBoxes);

                    switch (i_TextBox.Tag.ToString())
                    {
                        case "GameObject Position":
                            cGL.SelectedGameObjectForControl.Transform.Position = newVectorFromTextBoxes;
                            break;
                        case "GameObject Scale":
                            cGL.SelectedGameObjectForControl.Transform.Scale = newVectorFromTextBoxes;
                            break;
                        case "GameObject Rotation":
                            break;
                        case "Light Position":
                            cGL.Light.Position = newVectorFromTextBoxes;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private Vector3 getVectorFromPanel(Panel i_PanelWithVectorValues)
        {
            TextBox[] vectorTextBoxes = i_PanelWithVectorValues.Controls.OfType<TextBox>().ToArray();
            Array.Sort(vectorTextBoxes, (i_TextBox1, i_TextBox2) => i_TextBox1.TabIndex.CompareTo(i_TextBox2.TabIndex));
            float x = float.Parse(vectorTextBoxes[0].Text);
            float y = float.Parse(vectorTextBoxes[1].Text);
            float z = float.Parse(vectorTextBoxes[2].Text);

            return new Vector3(x, y, z);
        }

        ////private Vector3 getPositionFromTextBoxes()
        ////{
        ////    float x = float.Parse(xPositionTextBox.Text);
        ////    float y = float.Parse(yPositionTextBox.Text);
        ////    float z = float.Parse(zPositionTextBox.Text);
        //
        ////    return new Vector3(x, y, z);
        ////}
        //
        ////private Vector3 getScaleFromTextBoxes()
        ////{
        ////    float x = float.Parse(xScaleTextBox.Text);
        ////    float y = float.Parse(yScaleTextBox.Text);
        ////    float z = float.Parse(zScaleTextBox.Text);
        //
        ////    return new Vector3(x, y, z);
        ////}

        private void resetSelectedGameObjectScale()
        {
            if (cGL.SelectedGameObjectForControl != null)
            {
                cGL.SelectedGameObjectForControl.Transform.Scale = new Vector3(1.0f);
                this.Invoke(new Action(() => gameObjectBindingSource.ResetCurrentItem()));
            }
        }

        private void updateMouseMovement(MouseEventArgs e)
        {
            if (m_IsMousePressed)
            {
                Vector2 mouseOffsetFromLastClicked =
                    new Vector2(e.X - m_LastGameSceneClickPosition.X, m_LastGameSceneClickPosition.Y - e.Y).Normalized *
                    k_MouseMovementSensitivity;

                m_LastGameSceneClickPosition.X = e.X;
                m_LastGameSceneClickPosition.Y = e.Y;

                //if(cGL.Camera.PitchAngle <= 89.0f && cGL.Camera.PitchAngle >= -89.0f)
                {
                    cGL.Camera.PitchAngle += mouseOffsetFromLastClicked.Y;
                    preventHighPitchCameraMovement();
                }
                

                cGL.Camera.YawAngle += mouseOffsetFromLastClicked.X;

            }
        }

        private void preventHighPitchCameraMovement()
        {
            if (cGL.Camera.PitchAngle > 89.0f)
            {
                cGL.Camera.PitchAngle = 89.0f;
            }

            if (cGL.Camera.PitchAngle < -89.0f)
            {
                cGL.Camera.PitchAngle = -89.0f;
            }
        }

        private string getProjectDirectory()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.FullName;

            return projectDirectory;
        }

        private void applySelectedCubemapToWorldCube()
        {
            CubemapTexture selectedCubemapTexture = comboBoxCubemapSelection.SelectedItem as CubemapTexture;

            if (selectedCubemapTexture != null)
            {
                if (!selectedCubemapTexture.IsLoaded)
                {
                    selectedCubemapTexture.LoadTextures();
                }

                cGL.WorldCube.UseTexture(selectedCubemapTexture);
            }
        }

        private void removeSelectedGameObject()
        {
            GameObject selectedGameObject = listBoxGameObjects.SelectedItem as GameObject;

            if (selectedGameObject != null)
            {
                cGL.GameObjects.Remove(selectedGameObject);
                cOGLBindingSource.ResetBindings(false);
            }
        }

        private void startResetGame()
        {
            buttonStartReset.Text = "Reset";

            if (!m_GameStarted)
            {
                cGL.GameEnvironment.StartGame();
            }
            else
            {
                cGL.GameEnvironment.ResetGame();
            }

            m_GameStarted = !m_GameStarted;
        }
    }
}