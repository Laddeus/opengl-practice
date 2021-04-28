using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenGLPractice.Game;
using OpenGLPractice.GLMath;

namespace OpenGLPractice
{
    public partial class OpenGLForm : Form
    {
        private readonly Queue<Keys> r_KeysPressed = new Queue<Keys>();
        private readonly Dictionary<Keys, Action> r_GameObjectActionKeys;
        private readonly Dictionary<Keys, Action> r_CameraActionKeys;
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
                {Keys.Q, () => updateHorizontalAngle(-2)},
                {Keys.E, () => updateHorizontalAngle(2)},
                {Keys.R, () => cGL.Camera.LookAtVerticalAngle -= 2},
                {Keys.F, () => cGL.Camera.LookAtVerticalAngle += 2},
            };

            object[] allGameObjectTypeNames = GameObjectCreator.GetAllGameObjectTypeNames();
            comboBoxGameObjects.Items.AddRange(allGameObjectTypeNames);

            gameObjectBindingSource.DataSource = cGL.GameObjects;
        }

        private void updateHorizontalAngle(float i_AngleToMoveBy)
        {
            cGL.Camera.LookAtHorizontalAngle += i_AngleToMoveBy;

            //sortObjectsByDistanceFromCameraEye();
        }

        private void sortObjectsByDistanceFromCameraEye()
        {
            Vector3 cameraEyePosition = cGL.Camera.EyePosition;

            cGL.GameObjects.Sort(((i_FirstGameObject, i_SecondGameObject) =>
            {
                Vector3 firstGameObjectPosition = i_FirstGameObject.Transform.Position;
                Vector3 secondGameObjectPosition = i_SecondGameObject.Transform.Position;

                if (firstGameObjectPosition.SquaredDistance(cameraEyePosition) <
                    secondGameObjectPosition.SquaredDistance(cameraEyePosition))
                {
                    return 1;
                }

                return -1;
            }));
        }

        private void GameScene_MouseWheel(object i_Sender, MouseEventArgs i_MouseEventArgs)
        {
            cGL.Camera.LookAtDistance += 0.25f * (i_MouseEventArgs.Delta / -120.0f);
        }

        private void GameScene_KeyDown(object i_Sender, KeyEventArgs i_KeyEventArgs)
        {
            Keys currentlyPressedKey = i_KeyEventArgs.KeyCode;

            if (r_AllowedKeys.Contains(currentlyPressedKey))
            {
                r_KeysPressed.Enqueue(i_KeyEventArgs.KeyCode);
            }

            i_KeyEventArgs.Handled = true;
        }

        private void GameCanvas_Resize(object sender, EventArgs e)
        {
            cGL.OnResize();
        }

        private void GameLoopTimer_Tick(object sender, EventArgs e)
        {
            update();
            cGL.Draw();
        }

        private void update()
        {
            Vector3 gameObjectPosition = cGL.SelectedGameObjectForControl.Transform.Position;

            if (r_KeysPressed.Count != 0)
            {
                Keys lastKeyPressed = r_KeysPressed.Dequeue();
                updateGameObjectActions(lastKeyPressed);
                updateCameraActions(lastKeyPressed);
            }

            foreach (GameObject gameObject in cGL.GameObjects)
            {
                gameObject.Tick(GameLoopTimer.Interval / 1000.0f);
            }

            if (cGL.SelectedGameObjectForControl.Transform.Position != gameObjectPosition)
            {
                gameObjectBindingSource.ResetCurrentItem();
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
                gameObjectBindingSource.ResetCurrentItem();
            }
        }

        private void buttonAddGameObjectToScene_Click(object sender, EventArgs e)
        {
            string gameObjectName = textBoxGameObjectName.Text;
            string gameObjectSelected = comboBoxGameObjects.SelectedItem.ToString();

            GameObject gameObjectCreated = GameObjectCreator.CreateGameObjectDefault(gameObjectSelected, gameObjectName);

            gameObjectBindingSource.Add(gameObjectCreated);
        }

        private void listBoxGameObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cGL != null && gameObjectBindingSource.Current != null)
            {
                cGL.SelectedGameObjectForControl = gameObjectBindingSource.Current as GameObject;
                GameScene.Focus();
            }
        }

        private void GameScene_Click(object sender, EventArgs e)
        {
            GameScene.Focus();
        }

        private void buttonResetPosition_Click(object sender, EventArgs e)
        {
            if (cGL.SelectedGameObjectForControl != null)
            {
                cGL.SelectedGameObjectForControl.Transform.Position = Vector3.Zero;
                gameObjectBindingSource.ResetCurrentItem();
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox != null && textBox.Focused && cGL.SelectedGameObjectForControl != null)
            {
                bool isValidNumber = float.TryParse(textBox.Text, out float result);

                if (isValidNumber && result != 0)
                {
                    switch (textBox.Tag.ToString())
                    {
                        case "Position":
                            cGL.SelectedGameObjectForControl.Transform.Position = getPositionFromTextBoxes();
                            break;
                        case "Scale":
                            cGL.SelectedGameObjectForControl.Transform.Scale = getScaleFromTextBoxes();
                            break;
                        case "Rotation":
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private Vector3 getPositionFromTextBoxes()
        {
            float x = float.Parse(xPositionTextBox.Text);
            float y = float.Parse(yPositionTextBox.Text);
            float z = float.Parse(zPositionTextBox.Text);

            return new Vector3(x, y, z);
        }

        private Vector3 getScaleFromTextBoxes()
        {
            float x = float.Parse(xScaleTextBox.Text);
            float y = float.Parse(yScaleTextBox.Text);
            float z = float.Parse(zScaleTextBox.Text);

            return new Vector3(x, y, z);
        }

        private void buttonResetScale_Click(object sender, EventArgs e)
        {
            if (cGL.SelectedGameObjectForControl != null)
            {
                Vector3 currentScale = cGL.SelectedGameObjectForControl.Transform.Scale;
                cGL.SelectedGameObjectForControl.Transform.ChangeScale(1.0f / currentScale.X, 1.0f / currentScale.Y,
                    1.0f / currentScale.Z);

                gameObjectBindingSource.ResetCurrentItem();
            }
        }
    }
}