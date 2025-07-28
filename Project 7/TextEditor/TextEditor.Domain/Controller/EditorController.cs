using System;
using TextEditor.Domain.Editor;
using TextEditor.Domain.Input;
using TextEditor.Domain.CursorLogic;
using TextEditor.Domain.Display;

namespace TextEditor.Domain.Controller
{
    public class EditorController
    {
        FileManager fileManager;
        TextManager textManager;
        InputHandler inputHandler;
        Displayer displayer;
        Viewport viewport;
        Cursor cursor;

        public EditorController(
            FileManager fileManager,
            TextManager textManager,
            InputHandler inputHandler,
            Displayer displayer,
            Viewport viewport,
            Cursor cursor)
        {
            this.fileManager = fileManager;
            this.textManager = textManager;
            this.inputHandler = inputHandler;
            this.displayer = displayer;
            this.viewport = viewport;
            this.cursor = cursor;
        }

        public void Run()
        {   
            //scrie testele
            List<string> renderedText = viewport.RenderText();

            inputHandler.ArrowKeyPressed += cursor.OnArrowKeyPressed;
            inputHandler.EscapeKeyPressed += cursor.OnEscapeKeyPressed;

            cursor.CursorMoved += displayer.OnCursorMoved;
            cursor.CursorExitedViewport += viewport.OnCursorExitedView;

            viewport.ViewportMoved += displayer.OnViewportMoved;

            displayer.DisplayText(renderedText);
            cursor.SetDefaultPosition();
            inputHandler.Listen();
        }

    }
}
