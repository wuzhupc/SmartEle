using System.Collections;
using System.Timers;

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

        public ArrayList Eles
        {
            get { return _ele; }
        }

        /// <summary>
        /// 电梯组运行状态
        /// </summary>
        private  ElevatorBankStauts _runstatus;
        /// <summary>
        /// 电梯组运行状态
        /// </summary>
        public ElevatorBankStauts RunStatus
        {
            get { return _runstatus; }
        }

        private readonly Timer _timer;

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

            _timer = new Timer(1000);
            _timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
        }


        public void Dispose()
        {
            if (_timer != null)
            {
                if(_timer.Enabled)
                    _timer.Stop();
                _timer.Dispose();
            }
        }

        /// <summary>
        /// 定时处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            if (_runstatus == ElevatorBankStauts.EssPause || _runstatus == ElevatorBankStauts.EssStop)
                return;
            if (_ele == null || _ele.Count <= 0)
                return;
            for (int i = 0; i < _ele.Count; i++)
            {
                Ele ele = (Ele) _ele[i];
                ele.OnRun();
            }
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
            int index = 0;
            int min = int.MaxValue;
            for (int i = 0; i < _ele.Count; i++)
            {
                Ele ele = (Ele) _ele[i];
                int time = ele.ComRunToFloorTime(user.FromFloor, to);
                if (time >= min) continue;
                index = i;
                min = time;
            }
            return (Ele) _ele[index];
        }

        public int GetNewEleid()
        {
            if (_ele == null || _ele.Count <= 0)
                return 1;
            return _ele.Count+1;
        }

        /// <summary>
        /// 运行电梯组
        /// </summary>
        public void RunElevatorBank()
        {
            if (_runstatus == ElevatorBankStauts.EssRun)
                return;
            //启动定时器
            _runstatus = ElevatorBankStauts.EssRun;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        /// <summary>
        /// 停止电梯组运行,相当于重置
        /// </summary>
        public void StopElevatorBank()
        { 
            //停止定时器
            _timer.Enabled = false;
            if(_ele!=null&&_ele.Count>0)
                _ele.Clear();
            _runstatus = ElevatorBankStauts.EssStop;
           
        }
        /// <summary>
        /// 暂停电梯组运行
        /// </summary>
        public void PauseElevatorBank()
        {
            //停止定时器
            _timer.Enabled = false;
            _runstatus = ElevatorBankStauts.EssPause;
        }

        /// <summary>
        /// 新增电梯
        /// </summary>
        public void NewEle(int initfloor)
        {
            Ele ele = new Ele(this,GetNewEleid(),initfloor);
            _ele.Add(ele);
        }
    }
}
