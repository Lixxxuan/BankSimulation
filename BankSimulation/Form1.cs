using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankSimulation
{
    public partial class Form1 : Form
    {
        private SemaphoreSlim semaphore; // 信号量，用于控制顾客的访问
        private Queue<int> queue; // 顾客队列，存储顾客的号码

        reasonml
        复制
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

        private void CreateSeats()
        {
            for (int i = 0; i < 10; i++)
            {
                Label lblSeat = new Label();
                lblSeat.Text = "座位 " + (i + 1);
                lblSeat.AutoSize = true;
                lblSeat.Location = new System.Drawing.Point(20, 60 + i * 30);

                Controls.Add(lblSeat);
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

        private void UpdateServingDisplay(int ticketNumber)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(UpdateServingDisplay), ticketNumber);
            }
            else
            {
                lblServing.Text = "正在服务：" + ticketNumber;
            }
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }


    }
}