using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGing.Manager
{
    public class SceneManager
    {
        public static SceneManager instance = null;
        private int[,] mRoadMap;
        private Define.GameEnum.eSceneType mCurrentScene;

        public SceneManager()
        {
            mRoadMap = new int[(int)Define.GameEnum.eSceneType.End+1, (int)Define.GameEnum.eSceneType.End+1];
            MapSetting();
            if (instance == null)
                instance = this;
            mCurrentScene = Define.GameEnum.eSceneType.Town;
        }
        public bool ActByInput(int input)
        {
            return false;
        }
        public void MainLoop()
        {

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
