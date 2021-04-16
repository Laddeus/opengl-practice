using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenGLPractice.GameObjects;
using OpenGLPractice.Utilities;

namespace OpenGLPractice
{
    public partial class OpenGLForm : Form
    {
        private readonly Queue<Keys> r_KeysPressed = new Queue<Keys>();
        private readonly Dictionary<Keys, Action> r_KeyActionDictionary;
        private readonly cOGL cGL;

        public OpenGLForm()
        {
            InitializeComponent();
            cGL = new cOGL(GameScene);

            ActiveControl = GameScene;
            GameScene.KeyDown += GameScene_KeyDown;
            GameScene.MouseWheel += GameScene_MouseWheel;
            r_KeyActionDictionary = new Dictionary<Keys, Action>()
            {
                { Keys.W, () => cGL.SelectedGameObjectForControl.Transform.Translate(0.25f * cGL.SelectedGameObjectForControl.Transform.ForwardVector) },
                { Keys.S, () => cGL.SelectedGameObjectForControl.Transform.Translate(0.25f * cGL.SelectedGameObjectForControl.Transform.BackwardVector) },
                { Keys.A, () => cGL.SelectedGameObjectForControl.Transform.Translate(0.25f * cGL.SelectedGameObjectForControl.Transform.LeftVector) },
                { Keys.D, () => cGL.SelectedGameObjectForControl.Transform.Translate(0.25f * cGL.SelectedGameObjectForControl.Transform.RightVector) },
                { Keys.Q, () => cGL.Camera.LookAtHorizontalAngle -= 2 },
                { Keys.E, () => cGL.Camera.LookAtHorizontalAngle += 2 },
                { Keys.R, () => cGL.Camera.LookAtVerticalAngle -= 2 },
                { Keys.F, () => cGL.Camera.LookAtVerticalAngle += 2 },
                { Keys.Z, () => cGL.SelectedGameObjectForControl.Transform.Rotate(2, cGL.SelectedGameObjectForControl.Transform.UpVector) },
                { Keys.C, () => cGL.SelectedGameObjectForControl.Transform.Rotate(-2, cGL.SelectedGameObjectForControl.Transform.UpVector) },
                { Keys.U, () => cGL.SelectedGameObjectForControl.Transform.Rotate(2, cGL.SelectedGameObjectForControl.Transform.RightVector) },
                { Keys.J, () => cGL.SelectedGameObjectForControl.Transform.Rotate(-2, cGL.SelectedGameObjectForControl.Transform.RightVector) },
                { Keys.K, () => cGL.SelectedGameObjectForControl.Transform.Rotate(2, cGL.SelectedGameObjectForControl.Transform.ForwardVector) },
                { Keys.H, () => cGL.SelectedGameObjectForControl.Transform.Rotate(-2, cGL.SelectedGameObjectForControl.Transform.ForwardVector) },
            };

            object[] allGameObjectTypeNames = GameObjectCreator.GetAllGameObjectTypeNames();
            comboBoxGameObjects.Items.AddRange(allGameObjectTypeNames);

            gameObjectBindingSource.DataSource = cGL.GameObjects;
        }

        private void GameScene_MouseWheel(object i_Sender, MouseEventArgs i_MouseEventArgs)
        {
            cGL.Camera.LookAtDistance += 0.25f * (i_MouseEventArgs.Delta / -120.0f);
        }

        private void GameScene_KeyDown(object i_Sender, KeyEventArgs i_KeyEventArgs)
        {
            r_KeysPressed.Enqueue(i_KeyEventArgs.KeyCode);

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
            if (r_KeysPressed.Count != 0)
            {
                Keys lastKeyPressed = r_KeysPressed.Dequeue();

                Action actionToPerform;
                bool KeyHasAction = r_KeyActionDictionary.TryGetValue(lastKeyPressed, out actionToPerform);

                if (KeyHasAction && cGL.SelectedGameObjectForControl != null)
                {
                    actionToPerform.Invoke();
                    gameObjectBindingSource.ResetCurrentItem();
                }
            }

            foreach (GameObject gameObject in cGL.GameObjects)
            {
                gameObject.Tick(GameLoopTimer.Interval / 1000.0f);
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