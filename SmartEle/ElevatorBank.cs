using System.Collections;

namespace SmartEle
{
    /// <summary>
    /// 电梯组状态
    /// </summary>
    public enum ElevatorBankStauts
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
    public class ElevatorBank
    {
        /// <summary>
        /// 楼层数(默认楼层从1开始)
        /// </summary>
        private readonly int _floor;
        /// <summary>
        /// 楼层数(默认楼层从1开始)
        /// </summary>
        public int AllFloor
        {
            get { return _floor; }
        }
        /// <summary>
        /// 每层运行时间,单位s
        /// </summary>
        private readonly int _oneFloorRunTime;
        /// <summary>
        /// 每层运行时间,单位s
        /// </summary>
        public int OneFloorRunTime
        {
            get { return _oneFloorRunTime; }
        }

        /// <summary>
        /// 每次停靠时间，单位s
        /// </summary>
        private readonly int _stopover;
        /// <summary>
        /// 每次停靠时间，单位s
        /// </summary>
        public int StopOverTime
        {
            get { return _stopover; }
        }

        /// <summary>
        /// 电梯
        /// </summary>
        private readonly ArrayList _ele;

        /// <summary>
        /// 电梯组运行状态
        /// </summary>
        private readonly ElevatorBankStauts _runstatus;
        /// <summary>
        /// 电梯组运行状态
        /// </summary>
        public ElevatorBankStauts RunStatus
        {
            get { return _runstatus; }
        }

        /// <summary>
        /// 电梯组构造函数
        /// </summary>
        /// <param name="allfloor"></param>
        /// <param name="onefloorruntime"></param>
        /// <param name="stopover"></param>
        public ElevatorBank(int allfloor, int onefloorruntime, int stopover)
        {
            _floor = allfloor < 2 ? 2 : allfloor;
            _oneFloorRunTime = onefloorruntime<1?1:onefloorruntime;
            _stopover = stopover<1?1:stopover;
            _ele = new ArrayList();
            _runstatus = ElevatorBankStauts.EssStop;
        }

        /// <summary>
        /// 用户请求电梯资源
        /// </summary>
        /// <param name="user"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Ele Request(User user, int to)
        {
            if (user == null||_ele==null||_ele.Count<=0)
                return null;
            //已经在等待某个电梯或在某个电梯时，不重新分配电梯资源
            if (user.Status == UserStatus.UsInEle || user.Status == UserStatus.UsWait)
                return null;
            if (_ele.Count == 1)
                return (Ele) _ele[0];

           //TODO
            return null;
        }

        public int GetNewEleid()
        {
            //TODO
            return 1;
        }

        /// <summary>
        /// 运行电梯组
        /// </summary>
        public void RunElevatorBank()
        {
        }

        /// <summary>
        /// 停止电梯组运行,相当于重置
        /// </summary>
        public void StopElevatorBank()
        {
            
        }
        /// <summary>
        /// 暂停电梯组运行
        /// </summary>
        public void PauseElevatorBank()
        {
            
        }

        /// <summary>
        /// 新增电梯
        /// </summary>
        public void NewEle()
        {
            //TODO
        }
    }
}
