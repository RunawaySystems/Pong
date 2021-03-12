using System;
using System.Threading.Tasks;

namespace RunawaySystems.Pong {
    public static class InputManager {
        //private static byte[] inputBuffer;
        // private static Stream inputStream;
        private static Task inputListener;

        /// <summary> </summary>
        public static event Action<float> MovementInput;
        public static event Action EnterPressed;

        static InputManager() {
            //inputBuffer = new byte[2084];
            // inputStream = Console.OpenStandardInput();
            // inputStream.BeginRead(inputBuffer, 0, inputBuffer.Length, ar => {

            // });
        }

        public static void Start() {
            inputListener = Task.Run(ListenForInput);
        }

        static Task ListenForInput() {
            while (true) {
                if (Console.KeyAvailable) {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                    switch (keyInfo.Key) {
                        case ConsoleKey.W:
                        case ConsoleKey.UpArrow:
                            MovementInput?.Invoke(-1f);
                            break;
                        case ConsoleKey.S:
                        case ConsoleKey.DownArrow:
                            MovementInput?.Invoke(-1f);
                            break;
                        case ConsoleKey.Enter:
                            EnterPressed?.Invoke();
                            break;
                    }
                }
            }
        }
    }
}
