using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Manager;
using TextRPGing.Model;
using TextRPGing.Utils;

namespace TextRPGing.Scene
{
    public class Town : Define.Interface.IScene
    {
        public bool ActByInput(int input)
        {
            var Routes = GameManager.SceneManager.GetEnableScene();
            if (Routes.Length >= input)
                return false;
            else
            {
                GameManager.SceneManager.ChangeScene(Routes[input]);
                return true;
            }
        }

        public void MainLoop()
        {
            StringBuilder sb = new StringBuilder();
            List<string> sbs = new List<string>();
            if (Character.Player == null)
            {
                // 타이틀 출력
                sb.Clear();
                sb.Append($"내일배움캠프에 당도한 것을 환영하오, 낯선이여.\n");
                sb.Append($"나는 나의 훌륭한 학생들을 굽어살피는 깨우친 튜터, 염예찬이오.\n\n");
                sbs.Add(sb.ToString());
                // 이름 입력

                sb.Clear();
                sb.Append($"이름을 입력해 주세요 : ");
                sbs.Add(sb.ToString());
                // 직업 선택

                sb.Clear();
                sb.Append($"직업을 선택해 주세요.\n");
                foreach (var job in )
                sb.Append($"직업을 선택해 주세요.\n");
                sbs.Add(sb.ToString());
            }
            else
            {
                // 타이틀 출력
                sb.Clear();
                sb.Append($"");
                sbs.Add(sb.ToString());
                // 이벤트 출력
                sb.Clear();
                sb.Append($"");
                sbs.Add(sb.ToString());
                // 행선지 출력
                sb.Clear();
                sb.Append($"");
                sbs.Add(sb.ToString());
            }
            MessageToUI message = new MessageToUI(Define.GameEnum.eSceneType.Town, sbs.ToArray());
            GameManager.UIManager.PutToOutQueue(message);
            GameManager.UIManager.DisplayUpdate();
        }
    }
}
