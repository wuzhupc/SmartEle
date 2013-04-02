namespace SmartEle
{
    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudAllFloor = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudRunTime = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.nudStopOver = new System.Windows.Forms.NumericUpDown();
            this.btSetElevatorBank = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.btPause = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nudEleInitFloor = new System.Windows.Forms.NumericUpDown();
            this.btAddEle = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btAddUser = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.nudUserFrom = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudUserTo = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lvEleStatus = new System.Windows.Forms.ListView();
            this.lvUserStatus = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAllFloor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRunTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopOver)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEleInitFloor)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserTo)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btPause);
            this.groupBox1.Controls.Add(this.btStop);
            this.groupBox1.Controls.Add(this.btSetElevatorBank);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.nudStopOver);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudRunTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nudAllFloor);
            this.groupBox1.Location = new System.Drawing.Point(7, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 103);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "电梯组设置";
            // 
            // nudAllFloor
            // 
            this.nudAllFloor.Location = new System.Drawing.Point(65, 18);
            this.nudAllFloor.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudAllFloor.Name = "nudAllFloor";
            this.nudAllFloor.Size = new System.Drawing.Size(40, 21);
            this.nudAllFloor.TabIndex = 0;
            this.nudAllFloor.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "总楼层数";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "每层运行时间(秒)";
            // 
            // nudRunTime
            // 
            this.nudRunTime.Location = new System.Drawing.Point(218, 18);
            this.nudRunTime.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudRunTime.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.nudRunTime.Name = "nudRunTime";
            this.nudRunTime.Size = new System.Drawing.Size(40, 21);
            this.nudRunTime.TabIndex = 2;
            this.nudRunTime.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(264, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "停靠时间(秒)";
            // 
            // nudStopOver
            // 
            this.nudStopOver.Location = new System.Drawing.Point(344, 18);
            this.nudStopOver.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nudStopOver.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudStopOver.Name = "nudStopOver";
            this.nudStopOver.Size = new System.Drawing.Size(40, 21);
            this.nudStopOver.TabIndex = 4;
            this.nudStopOver.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // btSetElevatorBank
            // 
            this.btSetElevatorBank.Location = new System.Drawing.Point(390, 16);
            this.btSetElevatorBank.Name = "btSetElevatorBank";
            this.btSetElevatorBank.Size = new System.Drawing.Size(69, 23);
            this.btSetElevatorBank.TabIndex = 6;
            this.btSetElevatorBank.Text = "设置";
            this.btSetElevatorBank.UseVisualStyleBackColor = true;
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(468, 13);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(64, 38);
            this.btStop.TabIndex = 7;
            this.btStop.Text = "运行";
            this.btStop.UseVisualStyleBackColor = true;
            // 
            // btPause
            // 
            this.btPause.Location = new System.Drawing.Point(468, 55);
            this.btPause.Name = "btPause";
            this.btPause.Size = new System.Drawing.Size(64, 38);
            this.btPause.TabIndex = 8;
            this.btPause.Text = "暂停";
            this.btPause.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btAddEle);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.nudEleInitFloor);
            this.groupBox2.Location = new System.Drawing.Point(8, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 53);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "电梯设置";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "初始楼层";
            // 
            // nudEleInitFloor
            // 
            this.nudEleInitFloor.Location = new System.Drawing.Point(65, 22);
            this.nudEleInitFloor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudEleInitFloor.Name = "nudEleInitFloor";
            this.nudEleInitFloor.Size = new System.Drawing.Size(40, 21);
            this.nudEleInitFloor.TabIndex = 2;
            this.nudEleInitFloor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btAddEle
            // 
            this.btAddEle.Location = new System.Drawing.Point(111, 20);
            this.btAddEle.Name = "btAddEle";
            this.btAddEle.Size = new System.Drawing.Size(64, 23);
            this.btAddEle.TabIndex = 4;
            this.btAddEle.Text = "添加电梯";
            this.btAddEle.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.nudUserTo);
            this.groupBox3.Controls.Add(this.btAddUser);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.nudUserFrom);
            this.groupBox3.Location = new System.Drawing.Point(189, 42);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(276, 53);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "用户设置";
            // 
            // btAddUser
            // 
            this.btAddUser.Location = new System.Drawing.Point(201, 19);
            this.btAddUser.Name = "btAddUser";
            this.btAddUser.Size = new System.Drawing.Size(69, 23);
            this.btAddUser.TabIndex = 4;
            this.btAddUser.Text = "添加用户";
            this.btAddUser.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "初始楼层";
            // 
            // nudUserFrom
            // 
            this.nudUserFrom.Location = new System.Drawing.Point(65, 22);
            this.nudUserFrom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudUserFrom.Name = "nudUserFrom";
            this.nudUserFrom.Size = new System.Drawing.Size(37, 21);
            this.nudUserFrom.TabIndex = 2;
            this.nudUserFrom.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(102, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "目的楼层";
            // 
            // nudUserTo
            // 
            this.nudUserTo.Location = new System.Drawing.Point(158, 21);
            this.nudUserTo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudUserTo.Name = "nudUserTo";
            this.nudUserTo.Size = new System.Drawing.Size(37, 21);
            this.nudUserTo.TabIndex = 5;
            this.nudUserTo.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lvEleStatus);
            this.groupBox4.Location = new System.Drawing.Point(7, 113);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(535, 118);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "电梯状态";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lvUserStatus);
            this.groupBox5.Location = new System.Drawing.Point(7, 237);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(535, 118);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "用户状态";
            // 
            // lvEleStatus
            // 
            this.lvEleStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.lvEleStatus.FullRowSelect = true;
            this.lvEleStatus.Location = new System.Drawing.Point(5, 14);
            this.lvEleStatus.MultiSelect = false;
            this.lvEleStatus.Name = "lvEleStatus";
            this.lvEleStatus.Size = new System.Drawing.Size(524, 98);
            this.lvEleStatus.TabIndex = 0;
            this.lvEleStatus.UseCompatibleStateImageBehavior = false;
            this.lvEleStatus.View = System.Windows.Forms.View.Details;
            // 
            // lvUserStatus
            // 
            this.lvUserStatus.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.lvUserStatus.FullRowSelect = true;
            this.lvUserStatus.Location = new System.Drawing.Point(5, 14);
            this.lvUserStatus.MultiSelect = false;
            this.lvUserStatus.Name = "lvUserStatus";
            this.lvUserStatus.Size = new System.Drawing.Size(524, 98);
            this.lvUserStatus.TabIndex = 1;
            this.lvUserStatus.UseCompatibleStateImageBehavior = false;
            this.lvUserStatus.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "用户名";
            this.columnHeader1.Width = 48;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "当前状态";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "当前楼层";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "起始楼层";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "目的楼层";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "所在电梯";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "还需等待";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "到达时间";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "已经用时";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "电梯名";
            this.columnHeader10.Width = 48;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "当前状态";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "当前楼层";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "电梯中用户";
            this.columnHeader13.Width = 180;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "等待进电梯用户";
            this.columnHeader14.Width = 170;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 364);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "楼层请求面板外置式智能电梯组算法";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAllFloor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRunTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStopOver)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEleInitFloor)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUserTo)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudAllFloor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudRunTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudStopOver;
        private System.Windows.Forms.Button btSetElevatorBank;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Button btPause;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudEleInitFloor;
        private System.Windows.Forms.Button btAddEle;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btAddUser;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudUserFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudUserTo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ListView lvEleStatus;
        private System.Windows.Forms.ListView lvUserStatus;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
    }
}

