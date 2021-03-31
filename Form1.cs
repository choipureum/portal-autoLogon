using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AutoLogin
{

    public partial class Form1 : Form
    {
        private const string KEY_FILENAME = "key.txt";

        protected ChromeDriverService _driverService = null;
        protected ChromeOptions _options = null;
        protected ChromeDriver _driver = null;
        protected StreamWriter writer;
        // 정보 txt 파일 생성 폴더
        protected string path = @"C:/Users/autoLogin";
        DirectoryInfo di = null;
        protected List<key> webArr = null;
        String nowhandle;  //현재 창의 핸들찾기
        IList<String> handles;  //모든 창의 핸들 찾기

        public Form1()
        {
            InitializeComponent();

            try
            {
                _driverService = ChromeDriverService.CreateDefaultService();
                _driverService.HideCommandPromptWindow = true;

                _options = new ChromeOptions();
                _options.AddArgument("disable-gpu");
                //클릭이벤트선언
                this.plus1.Click += plus1_Click;
                this.minus1.Click += minus1_Click;
            }
            catch (Exception)
            {
                MessageBox.Show("[Error]");
                processKill();
            }

        }
        /// <summary>
        /// 버튼 1 : 로그인
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //파일읽기
                string[] lines = File.ReadAllLines(path + @"/" + KEY_FILENAME);
                string url = string.Empty;
                string id = string.Empty;
                string pw = string.Empty;

                _driver = new ChromeDriver(_driverService, _options);


                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];

                    string[] lineArr = line.Split(' ');
                    url = lineArr[0];
                    if (i > 0)
                    {
                        ExecuteScript(_driver, "window.open();");
                        Thread.Sleep(1000);
                    }
                    nowhandle = _driver.CurrentWindowHandle;
                    handles = _driver.WindowHandles;

                    TapChange(_driver, handles[i]);

                    _driver.Navigate().GoToUrl(url); // 웹 사이트에 접속합니다.
                    _driver.Manage().Window.Maximize();
                    //_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

                    Thread.Sleep(1000);

                    //모두 존재(자동로그인3)
                    if (lineArr.Length > 2 && (lineArr[1] != "" || lineArr[2] != ""))
                    {
                        id = lineArr[1];
                        pw = lineArr[2];

                        //M portal
                        if (url.Contains("mportal"))
                        {
                            IWebElement element = _driver.FindElement(By.XPath("//*[@id='login_com']"));
                            IList<IWebElement> AllDropDownList = element.FindElements(By.XPath("//option"));
                            int DpListCount = AllDropDownList.Count;
                            for (int j = 0; j < DpListCount; j++)
                            {
                                if (AllDropDownList[j].Text == "iMBC")
                                {
                                    AllDropDownList[j].Click();
                                }
                            }

                            element = _driver.FindElementByCssSelector("#id");

                            element.SendKeys(id);

                            Thread.Sleep(500);

                            element = _driver.FindElementByCssSelector("#passwd");
                            element.SendKeys(pw);

                            element = _driver.FindElementByXPath("//*[@id='wrap']/div[2]/div[2]/p[5]/img");
                            element.Click();
                        }
                        else if (url.Contains("netflix"))
                        {
                            IWebElement element = _driver.FindElementByCssSelector("#id_userLoginId");

                            element.SendKeys(id);

                            Thread.Sleep(500);

                            element = _driver.FindElementByCssSelector("#id_password");
                            element.SendKeys(pw);

                            element = _driver.FindElementByXPath("//*[@id='appMountPoint']/div/div[3]/div/div/div[1]/form/button");
                            element.Click();
                        }
                    }
                }
                return;
            }
            catch (Exception)
            {
                processKill();
            }


        }
        //웹추가
        private void plus1_Click(object sender, EventArgs e)
        {
            string web = textBox1.Text.Trim();
            string key = textBox2.Text.Trim();
            string pw = textBox3.Text.Trim();

            try
            {
                if (web != "")
                {
                    //value 텍스트 파일 생성 / 입력
                    writer = File.AppendText(path + @"/" + KEY_FILENAME);
                    writer.WriteLine(web + " " + key + " " + pw);

                    // listbox 추가
                    listBox1.Items.Add(web + " " + key + " " + pw);
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();

                    writer.Close();
                }
                else
                {
                    MessageBox.Show("[Error] value empty");
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show("[Error]" + e1.StackTrace);
            }

        }
        //txt 저장 데이터 삭제
        private void minus1_Click(object sender, EventArgs e)
        {
            try
            {
                string selected = listBox1.SelectedItem.ToString();
                listBox1.Items.Remove(selected);
                string[] temp = selected.Split(' ');

                UpdateFileSkipRow(path + @"/" + KEY_FILENAME, temp[0].Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("[Error]");
            }

        }
        public void UpdateFileSkipRow(string fileFullPath, string key)
        {
            string[] lines = File.ReadAllLines(fileFullPath);
            int pos = Array.FindIndex(lines, row => row.Contains(key));
            if (pos < 0)
                return; // 일치하는게 없다면 return  
            RemoveAt<string>(ref lines, pos);
            File.WriteAllLines(fileFullPath, lines);
        }
        public void RemoveAt<T>(ref T[] arr, int index)
        {
            for (int a = index; a < arr.Length - 1; a++) arr[a] = arr[a + 1];
            Array.Resize(ref arr, arr.Length - 1);
        }

        //삭제 변동
        private void minus1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                minus1.Enabled = false;
                MessageBox.Show("선택 항목이 없습니다.");
                return;
            }
            minus1.Enabled = true;
        }

        //how to use 깃헙 readme연동
        private void howtouse_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/choipureum/portal-autoLogon");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //문서 불러오기
            di = new DirectoryInfo(path);
            if (di.Exists == false)
            {
                di.Create();
                return;
            }
            //dir 존재
            else
            {
                //파일존재판단
                FileInfo fi = new FileInfo(path + @"/" + KEY_FILENAME);
                //FileInfo.Exists로 파일 존재시"
                if (fi.Exists)
                {
                    //listbox 추가
                    string[] keylines = File.ReadAllLines(path + @"/" + KEY_FILENAME, Encoding.UTF8);

                    for (int i = 0; i < keylines.Length; i++)
                    {
                        string[] key = keylines[i].Split(' ');
                        // listbox 추가
                        listBox1.Items.Add(key[0] + " " + key[1] + " " + key[2]);
                    }
                }
                else
                {
                    writer = File.CreateText(path + @"/" + KEY_FILENAME);
                }
            }

        }
        //동기적 실행 (driver, 자바스크립트)
        private void ExecuteScript(IJavaScriptExecutor driver, string script)
        {
            try
            {
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript(script);
            }
            catch (Exception) { }

        }

        //비동기적 실행 (driver, 자바스크립트)
        private void ExecuteAsyncScript(IJavaScriptExecutor driver, string script)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteAsyncScript(script);
        }
        //tap change
        private void TapChange(ChromeDriver driver, String handle)
        {
            driver.SwitchTo().Window(handle);
        }

        //크롬 종료
        private void button1_Click_1(object sender, EventArgs e)
        {
            processKill();
        }
        private void processKill()
        {
            try
            {
                //오류및 강제종료시 process kill
                Process[] processList = Process.GetProcessesByName("chromedriver.exe");
                if (processList.Length > 0)
                {
                    processList[0].Kill();
                }
                _driver.Quit();
            }
            catch (Exception) { }
        }
        //창종료
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            processKill();
        }
        private void label1_Click(object sender, EventArgs e)
        {
            //없음
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //없음
        }
        private void label2_Click(object sender, EventArgs e)
        {
            //없음
        }


    }
}
