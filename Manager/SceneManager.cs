using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define;
using TextRPGing.Define.Interface;
using TextRPGing.Scene;

namespace TextRPGing.Manager
{
    public class SceneManager
    {       
        public Status StatusScene { get => mScenes[(int)GameEnum.eSceneType.Status] as Status; }
        public Battle BattleScene { get => mScenes[(int)GameEnum.eSceneType.Battle] as Battle; }
        public Recovery RecoveryScene { get => mScenes[(int)GameEnum.eSceneType.Recovery] as Recovery; }
        public SaveLoad SaveLoadScene { get => mScenes[(int)GameEnum.eSceneType.SaveLoad] as SaveLoad; }
        public Town TownScene { get => mScenes[(int)GameEnum.eSceneType.Town] as Town; }

        private int[,] mRoadMap;
        public Define.GameEnum.eSceneType CurrentScene { get; set; }
        private Define.GameEnum.eSceneType? SceneToChange { get; set; }
        private IScene[] mScenes;

        public SceneManager()
        {
            mRoadMap = new int[(int)GameEnum.eSceneType.End+1, (int)GameEnum.eSceneType.End+1];
            MapSetting();
            CurrentScene = GameEnum.eSceneType.Town;

            mScenes = new IScene[(int)GameEnum.eSceneType.End+1];
            mScenes[(int)GameEnum.eSceneType.Town] = new Town();
            mScenes[(int)GameEnum.eSceneType.Status] = new Status();
            mScenes[(int)GameEnum.eSceneType.Battle] = new Battle();
            mScenes[(int)GameEnum.eSceneType.Recovery] = new Recovery();
            mScenes[(int)GameEnum.eSceneType.SaveLoad] = new SaveLoad();
        }
        public bool ActByInput(int input)
        {
            bool ret = mScenes[(int)CurrentScene].ActByInput(input);
            if (SceneToChange.HasValue)
                CurrentScene = SceneToChange.Value;
            return ret;
        }
        public void MainLoop()
        {
            mScenes[(int)CurrentScene].MainLoop();
        }
        public void ChangeScene(Define.GameEnum.eSceneType sceneType)
        {
            if (mRoadMap[(int)CurrentScene, (int)sceneType] == 1)
            {
                CurrentScene = sceneType;
                SceneToChange = sceneType;
            }
            else
                throw new Exception($"{CurrentScene} -> {sceneType} 허락되지 않은 Scene 이동입니다.");
        }
        public Define.GameEnum.eSceneType[] GetEnableScene()
        {
            List<Define.GameEnum.eSceneType> ret = new List<Define.GameEnum.eSceneType>();
            for (int i = 0; i < mRoadMap.GetLength((int)CurrentScene);++i)
            {
                if (mRoadMap[(int)CurrentScene, i] == 1 && (int)CurrentScene != i)
                    ret.Add((Define.GameEnum.eSceneType)i);
            }
            return ret.ToArray();
        }
        private void MapSetting()
        {
            for (int i = 0; i <= (int)GameEnum.eSceneType.SaveLoad; ++i)
            {
                mRoadMap[i, i] = 0;
                switch ((Define.GameEnum.eSceneType)i)
                {
                    case GameEnum.eSceneType.Status:
                        mRoadMap[i, (int)GameEnum.eSceneType.Town] = 1;
                        break;
                    case GameEnum.eSceneType.Battle:
                        mRoadMap[i, (int)GameEnum.eSceneType.Town] = 1;
                        break;
                    case GameEnum.eSceneType.Recovery:
                        mRoadMap[i, (int)GameEnum.eSceneType.Town] = 1;
                        break;
                    case GameEnum.eSceneType.SaveLoad:
                        mRoadMap[i, (int)GameEnum.eSceneType.Town] = 1;
                        break;
                    case GameEnum.eSceneType.Town:
                        mRoadMap[i, (int)GameEnum.eSceneType.Battle] = 1;
                        mRoadMap[i, (int)GameEnum.eSceneType.Recovery] = 1;
                        mRoadMap[i, (int)GameEnum.eSceneType.SaveLoad] = 1;
                        mRoadMap[i, (int)GameEnum.eSceneType.Status] = 1;
                        break;
                    case GameEnum.eSceneType.End:
                        mRoadMap[i, (int)GameEnum.eSceneType.Town] = 1;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
