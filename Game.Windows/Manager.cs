using System;
using System.Windows.Forms;
using Game.Windows.Events;
using Game.Windows.Interface;

namespace Game.Windows
{
    public class Manager : ApplicationContext
    {
        private int _formsCount;

        public Manager()
        {
            var modeSelector = new Launcher();
            modeSelector.GameStart += OnGameStart;
            CreateForm(modeSelector);
        }

        private void OnGameStart(object sender, GameStartArgs e)
        {
            var form = new GameWindow(e.Mode);
            CreateForm(form);

            ((Form) sender).Close();
        }

        private void CreateForm(Form form)
        {
            ++_formsCount;

            form.Closed += OnFormClosed;
            form.Show();
        }
        
        private void OnFormClosed(object sender, EventArgs e)
        {
            --_formsCount;

            if (_formsCount <= 0)
                Application.Exit();
        }
    }
}
