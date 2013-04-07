using System;
using System.Collections;
using System.Windows.Forms;

namespace SmartEle
{
    public partial class FrmMain : Form
    {
        /// <summary>
        /// 用于定时更新信息
        /// </summary>
        private readonly System.Timers.Timer _timer;

        delegate void UpdateInfoDelegate();

        /// <summary>
        /// 用于生成随机数 
        /// </summary>
        private readonly Random _r;

        public FrmMain()
        {
            InitializeComponent();
            InitViewStatus();
            _timer = new System.Timers.Timer(1000) {AutoReset = true};
            _timer.Elapsed += TimerElapsed;
            _r=new Random(DateTime.Now.Millisecond);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ApplicationInfo info = ApplicationInfo.GetInfo();

            ElevatorBank elevatorBank = info.GetEles();
            if (elevatorBank == null || elevatorBank.RunStatus == ElevatorBankStauts.EssStop
                ||elevatorBank.RunStatus == ElevatorBankStauts.EssPause)
            {
                return;
            }
            UpdateInfoDelegate uiDelegate = new UpdateInfoDelegate(UpdateInfo);
            this.Invoke(uiDelegate);
        }

        /// <summary>
        /// 初始化设置界面按钮等状态
        /// </summary>
        private void InitViewStatus()
        {
            btAddEle.Enabled = false;
            btAddUser.Enabled = false;
            btPause.Enabled = false;
            btStop.Enabled = false;
            btSetElevatorBank.Enabled = true;
        }

        /// <summary>
        /// 更新用户状态列表
        /// </summary>
        /// <param name="users"></param>
        private void UpdateUserInfo(ArrayList users)
        {
            lvUserStatus.Items.Clear();
            if (users == null || users.Count <= 0)
            {
                return;
            }
            for (int i = users.Count-1; i >= 0; i--)
            {
                User user = (User)users[i];

                ListViewItem lvi = new ListViewItem(user.Name);
                lvi.SubItems.Add(user.NowStatusDesc);
                lvi.SubItems.Add(user.NowFloor.ToString());
                lvi.SubItems.Add(user.FromFloor.ToString());
                lvi.SubItems.Add(user.ToFloor==0?"":user.ToFloor.ToString());
                lvi.SubItems.Add(user.InOrWaitEle == null ? "" : user.InOrWaitEle.Name);
                lvi.SubItems.Add(user.Status == UserStatus.UsWait ? user.GetNeedWaitTime().ToString() : "");
                lvi.SubItems.Add(user.Status == UserStatus.UsNone ? "" : user.GetNeedAllTime().ToString());
                lvi.SubItems.Add(user.UseTime.ToString());
                lvUserStatus.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 更新电梯状态列表
        /// </summary>
        /// <param name="eles"></param>
        private void UpdateEleInfo(ArrayList eles)
        {
            lvEleStatus.Items.Clear();
            if (eles == null || eles.Count <= 0)
            {
                return;
            }
            for (int i = 0; i < eles.Count; i++)
            {
                Ele ele = (Ele) eles[i];
                ListViewItem lvi=new ListViewItem(ele.Name);

                lvi.SubItems.Add(ele.NowStatusDesc);
                lvi.SubItems.Add(ele.NowFloor.ToString());
                lvi.SubItems.Add(ele.InEleUsers);
                lvi.SubItems.Add(ele.WaitEleUsers);
                lvEleStatus.Items.Add(lvi);
            }
        }

        /// <summary>
        /// 用于被调用统一更新电梯状态和用户状态列表 
        /// </summary>
        private void UpdateInfo()
        {
            ClearInfo();
            ApplicationInfo info = ApplicationInfo.GetInfo();

            ElevatorBank elevatorBank = info.GetEles();
            if (elevatorBank == null)
                return;
            UpdateEleInfo(elevatorBank.Eles);
            UpdateUserInfo(info.Users);
        }

        private void ClearInfo()
        {
            lvEleStatus.Items.Clear();
            lvUserStatus.Items.Clear();
        }

        private void BtSetElevatorBankClick(object sender, System.EventArgs e)
        {
            //判断是否已经设置过
            ApplicationInfo info = ApplicationInfo.GetInfo();
            ElevatorBank elevatorBank = info.GetEles();
            if (elevatorBank != null)
            {
                elevatorBank.StopElevatorBank();
            }
            if (_timer != null && _timer.Enabled)
                _timer.Enabled = false;
            ClearInfo();
            //重新设置
            info.SetElevatorBank(
                new ElevatorBank((int) nudAllFloor.Value,(int) nudRunTime.Value,(int) nudStopOver.Value));
            nudEleInitFloor.Maximum = nudAllFloor.Value;
            nudUserFrom.Maximum = nudAllFloor.Value;
            nudUserTo.Maximum = nudAllFloor.Value;
            btAddEle.Enabled = true;
            btStop.Enabled = true; 
        }

        private void BtAddEleClick(object sender, System.EventArgs e)
        {
            ApplicationInfo info = ApplicationInfo.GetInfo();
            ElevatorBank elevatorBank = info.GetEles();

            if (elevatorBank == null)
            {
                btAddEle.Enabled = false;
                return;
            }
            elevatorBank.NewEle((int)nudEleInitFloor.Value);
            if (!btAddUser.Enabled&&elevatorBank.RunStatus==ElevatorBankStauts.EssRun)
                btAddUser.Enabled = true;
            UpdateEleInfo(elevatorBank.Eles);
            nudEleInitFloor.Value = _r.Next((int)nudEleInitFloor.Minimum, (int)nudEleInitFloor.Maximum);
        }

        private void BtAddUserClick(object sender, System.EventArgs e)
        {
            ApplicationInfo info = ApplicationInfo.GetInfo();
            ElevatorBank elevatorBank = info.GetEles();

            if (elevatorBank == null)
            {
                btAddUser.Enabled = false;
                return;
            }

            User user = info.NewUser((int)nudUserFrom.Value);
            user.UserRequestToFloor((int) nudUserTo.Value);
            if (elevatorBank.RunStatus != ElevatorBankStauts.EssRun)
            {
                UpdateUserInfo(info.Users);
                UpdateEleInfo(elevatorBank.Eles);
            }

            nudUserTo.Value = _r.Next((int)nudUserTo.Minimum, (int)nudUserTo.Maximum);
            nudUserFrom.Value = _r.Next((int)nudUserFrom.Minimum, (int)nudUserFrom.Maximum);
        }

        private void BtStopClick(object sender, System.EventArgs e)
        {
            ApplicationInfo info = ApplicationInfo.GetInfo();
            ElevatorBank elevatorBank = info.GetEles();

            if (elevatorBank == null)
            {
                btSetElevatorBank.Enabled = true;
                btStop.Enabled = false;
                return;
            }
            //运行
            if (elevatorBank.RunStatus == ElevatorBankStauts.EssStop)
            {
                btStop.Text = "停止";
                btPause.Text = "暂停";
                btPause.Enabled = true;
                elevatorBank.RunElevatorBank();
                _timer.Enabled = true;
                btSetElevatorBank.Enabled = false;
                if (elevatorBank.Eles != null && elevatorBank.Eles.Count > 0)
                    btAddUser.Enabled = true;
            }
            else
            {
                btStop.Text = "运行";
                btPause.Text = "暂停";
                btPause.Enabled = false;
                info.Users.Clear();
                elevatorBank.StopElevatorBank();
                _timer.Enabled = false;
                btAddUser.Enabled = false;
                btSetElevatorBank.Enabled = true;
                UpdateInfo();
            }
            
        }

        private void BtPauseClick(object sender, System.EventArgs e)
        {
            ApplicationInfo info = ApplicationInfo.GetInfo();
            ElevatorBank elevatorBank = info.GetEles();

            if (elevatorBank == null)
            {
                btSetElevatorBank.Enabled = true;
                btStop.Enabled = false;
                btPause.Enabled = false;
                return;
            }
            //运行
            if (elevatorBank.RunStatus == ElevatorBankStauts.EssPause)
            {
                btPause.Text = "暂停";
                elevatorBank.RunElevatorBank();
                _timer.Enabled = true;
            }
            else
            {
                btPause.Text = "继续运行";
                elevatorBank.PauseElevatorBank();
                _timer.Enabled = false;
            }
        }

        /// <summary>
        /// 关闭时释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMainFormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationInfo info = ApplicationInfo.GetInfo();
            ElevatorBank elevatorBank = info.GetEles();

            if (elevatorBank != null)
            {
                elevatorBank.StopElevatorBank();
                elevatorBank.Dispose();
            }
            if (_timer != null)
            {
                if(_timer.Enabled)
                    _timer.Stop();
                _timer.Dispose();
            }
        }
    }
}
