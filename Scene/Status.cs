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
    public class Status : Define.Interface.IScene
    {
        public bool ActByInput(int input, ref Define.GameEnum.eSceneType scene)
        {
            var Routes = GameManager.SceneManager.GetEnableScene(scene);
            if (Routes.Length <= input)
                return false;
            else
            {
                GameManager.SceneManager.ChangeScene(ref scene, Routes[input]);
                return true;
            }
        }

        public void MainLoop()
        {
            GameManager.UIManager.ConsoleClear();
            List<string> sbs = new List<string>();
            StringBuilder sb = new StringBuilder();
            // 타이틀 출력
            sb.Clear();
            sb.Append("상태보기\n");
            sb.Append("캐릭터의 정보가 표시됩니다.\n\n");
            sbs.Add(sb.ToString());

            // 캐릭터 정보 출력
            sb.Clear();
            sb.Append($"LV. {Character.Player.Level}\n");
            sb.Append($"{Character.Player.Name}  ({Character.Player.Job})\n");
            sb.Append($"공격력 : {Character.Player.ATK}\n");
            sb.Append($"방어력 : {Character.Player.DEF}\n");
            sb.Append($"체  력 : {Character.Player.HP}/{Character.Player.MaxHP}\n");
            sb.Append($"골  드 : {Character.Player.Inven.Gold}\n\n");
            sbs.Add(sb.ToString());
            
            // 선택지 출력
            sb.Clear();
            var Routes = GameManager.SceneManager.GetEnableScene(Define.GameEnum.eSceneType.Status);
            int i = 0;
            foreach (var route in Routes)
            {
                switch (route)
                {
                    case Define.GameEnum.eSceneType.Town:
                        sb.Append($"{i}. 나가기\n");
                        break;
                    case Define.GameEnum.eSceneType.Battle:
                        sb.Append($"{i}. 전투 시작\n");
                        break;
                    default:
                        sb.Append($"{i}. 오류입니다. 수정해주세요.\n");
                        break;
                }
                ++i;
            }
            sbs.Add(sb.ToString());

            MessageToUI message = new MessageToUI(Define.GameEnum.eSceneType.Status, sbs.ToArray());
            GameManager.UIManager.PutToOutQueue(message);
            GameManager.UIManager.DisplayUpdate();
        }
    }
}
