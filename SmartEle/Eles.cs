using System.Collections;

namespace SmartEle
{
    /// <summary>
    /// 电梯组状态
    /// </summary>
    public enum ElesStauts
    {
        /// <summary>
        /// 停止
        /// </summary>
        EssStop = 0,
        /// <summary>
        /// 运行
        /// </summary>
        EssRun = 1,
        /// <summary>
        /// 暂停
        /// </summary>
        EssPause = 2
    }
    /// <summary>
    /// 电梯组类
    /// </summary>
    public class Eles
    {
        /// <summary>
        /// 楼层数(默认楼层从1开始)
        /// </summary>
        private readonly int _floor;
        public int AllFloor
        {
            get { return _floor; }
        }
        /// <summary>
        /// 每层运行时间,单位s
        /// </summary>
        private readonly int _oneFloorRunTime;
        public int OneFloorRunTime
        {
            get { return _oneFloorRunTime; }
        }

        /// <summary>
        /// 每次停靠时间，单位s
        /// </summary>
        private readonly int _stopover;
        public int StopOverTime
        {
            get { return _stopover; }
        }

        /// <summary>
        /// 电梯
        /// </summary>
        private ArrayList _ele;

        /// <summary>
        /// 电梯组运行状态
        /// </summary>
        private ElesStauts _runstatus;

        /// <summary>
        /// 电梯组构造函数
        /// </summary>
        /// <param name="allfloor"></param>
        /// <param name="onefloorruntime"></param>
        /// <param name="stopover"></param>
        public Eles(int allfloor, int onefloorruntime, int stopover)
        {
            _floor = allfloor < 2 ? 2 : allfloor;
            _oneFloorRunTime = onefloorruntime<1?1:onefloorruntime;
            _stopover = stopover<1?1:stopover;
            _ele = new ArrayList();
            _runstatus = ElesStauts.EssStop;
        }

        /// <summary>
        /// 用户请求电梯资源
        /// </summary>
        /// <param name="user"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Ele Request(User user, int to)
        {
            //判断用户是否已经设置过请求或已经在电梯中
           //TODO
            return null;
        }
    }
}
