using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Manager
{
    public class GameManager
    {
        private SceneManager _sceneManager = new SceneManager();
        private UIManager _uiManager = new UIManager();

        public void GameStart()
        {
            
            while (true)
            {
                string input = Console.ReadLine();
                int inputNum = ValidInputNum(input);

                _sceneManager.ActByInput(inputNum);
            }
        }

        private int ValidInputNum(string input)
        {
            int inputNum = 0;
            if (int.TryParse(input, out inputNum) == false)
            {
                // TODO
                // UIManager 객체 사용해서 '잘못된 입력입니다' 출력
            }

            return inputNum;
        }
    }
}
