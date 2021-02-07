using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using Google.Cloud.Speech.V1;

//Install-Package Google.Cloud.Speech.V1 -Version 1.3.1

namespace WinformSpeechAPI
{
    public partial class Form1 : Form
    {
        List<string> teller = new List<string>();
        List<string> fruit = new List<string>(new string[] { "사과", "배" });
        List<string> ve = new List<string>(new string[] { "기차", "자전거" });
        List<string> ca = new List<string>(new string[] { "시계", "자" });

        //List<string> f_common = new List<string>(new string[] { "과일 입니다", "동그랗다", "빨갛다", "먹을 수 있습니다", "둥급니다", "맛있습니다", "과즙이 많습니다", "제배가 가능합니다", "키울 수 있습니다", "비닐하우스에서 제배합니다" });
        List<string> f_common = new List<string>(new string[] { "과일", "동그람", "맛있다", "과즙" ,"먹는것", "과즙", "재배가능"});
        List<string> v_common = new List<string>(new string[] { "탈것", "운송수단", "여행을 위한 것", "여행", "바퀴가 있다", "바퀴", "굴러간다" });
        List<string> c_common = new List<string>(new string[] { "측량도구", "무언갈 재는것", "숫자가 있다", "눈금이 있다", "숫자", "눈금" });
        int ct = 0;

        DispatcherTimer timer = new DispatcherTimer();
        float cot;
        string answer;

        public Form1()
        {
            InitializeComponent();
            initTimer();
            Credetials();   // 구글 Credentials 인증 진행
            //QuickStart();
            textBox1.Text = "사과와 배의 공통점은 무엇인가요?";
            pictureBox1.Load(@"D:\apple.PNG");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Load(@"D:\pear.PNG");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            _ = StreamingMicRecognizeAsync(3);
            timer.Start();
        }

        public void initTimer()
        {
            timer.Interval = TimeSpan.FromSeconds(4);
            timer.Tick += Timer_Tick;

        }

        public void Timer_Tick(object sender, EventArgs e)
        {
            if (ct == 0)
            {
                button1.PerformClick();
            }
            else if (ct == 1)
            {
                button2.PerformClick();
            }
            else if (ct == 2)
            {
                button3.PerformClick();
            }
            else if(ct == 3)
            {
                button4.PerformClick();
            }
            else if(ct == 4)
            {
                button5.PerformClick();
            }
            else
            {
                timer.Stop();
                clear1();
            }

            ct++;
        }

        #region ** 구글 클라우드 인증관련 **
        public static string Credentials_File = "wide-link-259109-47d68c2646dc.json";
        private void Credetials()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", Credentials_File);
        }
        #endregion
        #region ** StreamingRecognize 관련 **
        string[] RecommendedWord = new string[] { "사자", "하마" };

        public void compare1()
        {
            int flag = 0;
            for (int i = 0; i < 7; i++)
            {
                if (answer == f_common[i])
                {
                    textBox1.Text = "맞았습니다";
                    flag = 1;
                }
            }
            if (flag == 0)
            {
                textBox1.Text = "틀렸습니다";
            }

        }

        public void compare2()
        {
            int flag = 0;
            for (int i = 0; i < 7; i++)
            {
                if (answer == v_common[i])
                {
                    textBox1.Text = "맞았습니다";
                    flag = 1;
                }

            }
            if (flag == 0)
            {
                textBox1.Text = "틀렸습니다";
            }

        }

        public void compare3()
        {
            int flag = 0;
            for (int i = 0; i < 6; i++)
            {
                if (answer == c_common[i])
                {
                    textBox1.Text = "맞았습니다";
                    flag = 1;
                }

            }
            if (flag == 0)
            {
                textBox1.Text = "틀렸습니다";
            }

        }

        public void clear1()
        {
            pictureBox1.Load(@"D:\수고.PNG");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Load(@"D:\많으셨습니다.PNG");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            textBox1.Text = ""; 

        }


        public async Task<object> StreamingMicRecognizeAsync(int seconds)
        {
            //TextBox textBox = txt;
            //form1.InitializeComponent();

            SpeechClient speech = SpeechClient.Create();
            SpeechClient.StreamingRecognizeStream streamingCall = speech.StreamingRecognize();

            await streamingCall.WriteAsync(
                new StreamingRecognizeRequest()
                {
                    StreamingConfig = new StreamingRecognitionConfig()
                    {
                        Config = new RecognitionConfig()
                        {
                            Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                            SampleRateHertz = 16000,
                            LanguageCode = "ko-KR",
                            SpeechContexts = { new SpeechContext { Phrases = { RecommendedWord } } } // 힌트 
                        },
                        InterimResults = true,
                    }
                });
            
            Task printResponses = Task.Run(async () =>
            { 
                while (await streamingCall.ResponseStream.MoveNext(default(CancellationToken)))
                {
                    foreach (var result in streamingCall.ResponseStream.Current.Results)
                    {
                        foreach (var alternative in result.Alternatives)
                        {
                            if (this.InvokeRequired)
                            {
                                //this.Invoke(
                                //    new MethodInvoker(
                                //    delegate () { this.TxtTest.AppendText(alternative.Transcript + "\r\n"); }));

                                this.Invoke(
                                    new MethodInvoker(
                                    delegate () { 
                                        this.TxtTest.AppendText(
                                       // alternative.Transcript + " / " +
                                       // alternative.Confidence.ToString() + " / " + 
                                       // alternative.Words + " / " +
                                        "\r\n"); 
                                    })); // 띄어 쓰는건 이부분이니까
                                
                                answer = alternative.Transcript;
                                cot = 1;
                            }
                        }

                    } 

                }
            }); 

          
            object writeLock = new object();
            bool writeMore = true;
            var waveIn = new NAudio.Wave.WaveInEvent();
            waveIn.DeviceNumber = 0;
            waveIn.WaveFormat = new NAudio.Wave.WaveFormat(16000, 1);
            waveIn.DataAvailable +=
                (object sender, NAudio.Wave.WaveInEventArgs args) =>
                {
                    lock (writeLock)
                    {
                        if (!writeMore)
                        {
                            return;
                        }

                        streamingCall.WriteAsync(
                            new StreamingRecognizeRequest()
                            {
                                AudioContent = Google.Protobuf.ByteString.CopyFrom(args.Buffer, 0, args.BytesRecorded)
                            }).Wait();
                    }
                };
            waveIn.StartRecording();
            TxtTest.Text += "Speak now."; // 콘솔창 
            Console.WriteLine("Speak now."); 
            await Task.Delay(TimeSpan.FromSeconds(seconds)); 
            waveIn.StopRecording(); 
            lock (writeLock) 
            {
                writeMore = false;
            } // 이부분이 무슨 일을 하는걸가
            await streamingCall.WriteCompleteAsync();
            await printResponses;
            return 0;
        }
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            compare1();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "자, 그럼 기차와 자전거의 공통점은 무엇일까요?";
            pictureBox1.Load(@"D:\train.PNG");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Load(@"D:\by.PNG");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            _ = StreamingMicRecognizeAsync(5);


        }

        private void button3_Click(object sender, EventArgs e)
        {

            compare2();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "자, 그럼 시계와 자의 공통점은 무엇일까요?";
            pictureBox1.Load(@"D:\시계.PNG");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Load(@"D:\자.PNG");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            _ = StreamingMicRecognizeAsync(5);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            compare3();
        }
    }
}
