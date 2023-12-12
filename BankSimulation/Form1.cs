using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankSimulation
{
    public partial class Form1 : Form
    {
        private List<CheckBox> seatCheckBoxes;
        private SemaphoreSlim semaphore; // 信号量，用于控制顾客的访问
        private Queue<int> queue; // 顾客队列，存储顾客的号码
        public Form1()
        {
            InitializeComponent();
            semaphore = new SemaphoreSlim(1); // 初始化信号量，初始计数为1
            queue = new Queue<int>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 创建服务窗口和座位
            CreateServiceWindow();
            CreateSeats();
        }

        private void CreateServiceWindow()
        {
            Label lblServiceWindow = new Label();
            lblServiceWindow.Text = "服务窗口";
            lblServiceWindow.AutoSize = true;
            lblServiceWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblServiceWindow.Location = new System.Drawing.Point(20, 20);
            Controls.Add(lblTicket);
            Controls.Add(lblServiceWindow);
        }
        private List<bool> seatStatus;

        private void CreateSeats()
        {
            seatStatus = new List<bool>();
            seatCheckBoxes = new List<CheckBox>();

            for (int i = 0; i < 10; i++)
            {
                CheckBox chkSeat = new CheckBox();
                chkSeat.Text = "座位 " + (i + 1);
                chkSeat.AutoSize = true;
                chkSeat.Location = new System.Drawing.Point(20, 60 + i * 30);

                seatCheckBoxes.Add(chkSeat); // 将 CheckBox 引用添加到列表中
                Controls.Add(chkSeat);

                seatStatus.Add(true); // 初始化座位状态为 true，表示空闲
            }
        }

        private async void btnCustomer_Click(object sender, EventArgs e)
        {
            await Task.Run(() => CustomerProcess());
        }

        private async void CustomerProcess()
        {
            await semaphore.WaitAsync(); // 顾客尝试获取信号量

            if (queue.Count < 10) // 检查是否有空座位
            {
                int ticketNumber = queue.Count + 1;
                queue.Enqueue(ticketNumber); // 领取号码

                // 占用座位
                int seatIndex = ticketNumber - 1; // 座位索引从0开始
                if (seatIndex >= 0 && seatIndex < seatStatus.Count)
                {
                    seatStatus[seatIndex] = false; // 将座位状态设置为非空闲
                    UpdateSeatStatusDisplay(); // 更新座位状态显示
                }

                UpdateTicketDisplay(ticketNumber); // 更新界面显示
                semaphore.Release(); // 释放信号量

                await ServeCustomer(ticketNumber); // 等待顾客被叫号并服务
            }
            else
            {
                semaphore.Release(); // 释放信号量
                MessageBox.Show("座位已满，请稍后再来！");
            }
        }

        private async Task ServeCustomer(int ticketNumber)
        {
            await semaphore.WaitAsync(); // 营业员尝试获取信号量

            // 模拟营业员服务顾客
            await Task.Delay(1000); // 假设服务时间为2秒

            UpdateServingDisplay(ticketNumber); // 更新界面显示
            queue.Dequeue(); // 顾客被服务完毕，从队列中移除

            // 释放座位
            int seatIndex = ticketNumber - 1; // 座位索引从0开始
            if (seatIndex >= 0 && seatIndex < seatStatus.Count)
            {
                seatStatus[seatIndex] = true; // 将座位状态设置为空闲
                UpdateSeatStatusDisplay(); // 更新座位状态显示
            }

            semaphore.Release(); // 释放信号量
        }

        private void UpdateTicketDisplay(int ticketNumber)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateTicketDisplay), ticketNumber);
            }
            else
            {
                lblTicket.Text = "当前号码：" + ticketNumber;
            }

        }
        private void UpdateSeatStatusDisplay()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateSeatStatusDisplay));
            }
            else
            {
                for (int i = 0; i < seatStatus.Count; i++)
                {
                    CheckBox chkSeat = seatCheckBoxes[i];
                    chkSeat.Checked = !seatStatus[i]; // 非空闲状态设置为选中
                }
            }
        }
        private void UpdateServingDisplay(int ticketNumber)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateServingDisplay), ticketNumber);
            }
            else
            {
                lblServing.Text = "正在服务：" + ticketNumber;

                // 更新座位状态
                int seatIndex = ticketNumber - 1; // 座位索引从0开始
                if (seatIndex >= 0 && seatIndex < seatStatus.Count)
                {
                    seatStatus[seatIndex] = false; // 将座位状态设置为非空闲
                    UpdateSeatStatusDisplay(); // 更新座位状态显示
                }
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }


    }
}