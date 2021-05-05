using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using OpenGL;

namespace OpenGLPractice.OpenGLUtilities
{
    internal static class GLErrorCatcher
    {
        public static void TryGLCall(Action i_GLCall, [CallerLineNumber] int i_ExecutionLineNumber = 0,
            [CallerMemberName] string i_MemberName = "", [CallerFilePath] string i_Filename = "")
        {
            clearGLErrors();
            i_GLCall.Invoke();
            checkForGLErrors(i_ExecutionLineNumber, i_MemberName, i_Filename);
        }

        private static void checkForGLErrors(int i_ExecutionLineNumber, string i_MemberName, string i_Filename)
        {
            uint glError = GL.glGetError();

            if (glError != 0)
            {
                Debug.WriteLine($"[OpenGL Error]: File: {i_Filename.Substring(i_Filename.LastIndexOf('\\') + 1)}, Member name: {i_MemberName}, Line: {i_ExecutionLineNumber}, Error code: {glError}");
                Debugger.Break();
            }
        }

        private static void clearGLErrors()
        {
            while (GL.glGetError() != GL.GL_NO_ERROR)
            {
            }
        }
    }
}
