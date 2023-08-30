using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPGing.Define.Interface;
using TextRPGing.Scene;

namespace TextRPGing.Manager
{
    public class SceneManager
    {
        public static SceneManager instance = null;
        
        public Scene.Status StatusScene { get => mScenes[(int)Define.GameEnum.eSceneType.Status] as Status; }
        public Scene.Battle BattleScene { get => mScenes[(int)Define.GameEnum.eSceneType.Battle] as Battle; }
        public Scene.Recovery RecoveryScene { get => mScenes[(int)Define.GameEnum.eSceneType.Recovery] as Recovery; }
        public Scene.SaveLoad SaveLoadScene { get => mScenes[(int)Define.GameEnum.eSceneType..SaveLoadScene] as SaveLoad; }
        public Scene.Town TownScene { get => mScenes[(int)Define.GameEnum.eSceneType.Town] as Town; }

        private int[,] mRoadMap;
        private Define.GameEnum.eSceneType mCurrentScene;
        private IScene[] mScenes;

        public SceneManager()
        {
            mRoadMap = new int[(int)Define.GameEnum.eSceneType.End+1, (int)Define.GameEnum.eSceneType.End+1];
            MapSetting();
            if (instance == null)
                instance = this;
            mCurrentScene = Define.GameEnum.eSceneType.Town;

            mScenes = new IScene[(int)Define.GameEnum.eSceneType.End+1];
            mScenes[(int)Define.GameEnum.eSceneType.Town] = new Scene.Town();
            mScenes[(int)Define.GameEnum.eSceneType.Status] = new Scene.Status();
            mScenes[(int)Define.GameEnum.eSceneType.Battle] = new Scene.Battle();
            mScenes[(int)Define.GameEnum.eSceneType.Recovery] = new Scene.Recovery();
            mScenes[(int)Define.GameEnum.eSceneType.SaveLoad] = new Scene.SaveLoad();
        }
        public bool ActByInput(int input)
        {
            return mScenes[(int)mCurrentScene].ActByInput(input);
        }
        public void MainLoop()
        {
            mScenes[(int)mCurrentScene].MainLoop();
        }
        public void ChangeScene(Define.GameEnum.eSceneType sceneType)
        {
            if (mRoadMap[(int)mCurrentScene, (int)sceneType] == 1)
            {
                mCurrentScene = sceneType;
            }
            else
                throw new Exception($"{mCurrentScene} -> {sceneType} 허락되지 않은 Scene 이동입니다.");
        }
        public Define.GameEnum.eSceneType[] GetEnableScene()
        {
            List<Define.GameEnum.eSceneType> ret = new List<Define.GameEnum.eSceneType>();
            for (int i = 0; i < mRoadMap.GetLength((int)mCurrentScene);)
            {
                if (mRoadMap[(int)mCurrentScene, i] == 1)
                    ret.Add((Define.GameEnum.eSceneType)i);
            }
            return ret.ToArray();
        }
        private void MapSetting()
        {
            for (int i = 0; i <= (int)Define.GameEnum.eSceneType.SaveLoad; ++i)
            {
                mRoadMap[i, i] = 0;
                switch ((Define.GameEnum.eSceneType)i)
                {
                    case Define.GameEnum.eSceneType.Status:
                        mRoadMap[i, (int)Define.GameEnum.eSceneType.Town] = 1;
                        break;
                    case Define.GameEnum.eSceneType.Battle:
                        mRoadMap[i, (int)Define.GameEnum.eSceneType.Town] = 1;
                        break;
                    case Define.GameEnum.eSceneType.Recovery:
                        mRoadMap[i, (int)Define.GameEnum.eSceneType.Town] = 1;
                        break;
                    case Define.GameEnum.eSceneType.SaveLoad:
                        mRoadMap[i, (int)Define.GameEnum.eSceneType.Town] = 1;
                        break;
                    case Define.GameEnum.eSceneType.Town:
                        mRoadMap[i, (int)Define.GameEnum.eSceneType.Battle] = 1;
                        mRoadMap[i, (int)Define.GameEnum.eSceneType.Recovery] = 1;
                        mRoadMap[i, (int)Define.GameEnum.eSceneType.SaveLoad] = 1;
                        mRoadMap[i, (int)Define.GameEnum.eSceneType.Town] = 1;
                        break;
                    case Define.GameEnum.eSceneType.End:
                        mRoadMap[i, (int)Define.GameEnum.eSceneType.Town] = 1;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
