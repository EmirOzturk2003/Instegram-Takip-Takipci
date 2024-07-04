using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace takipTakipci
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
           IWebDriver driver=new ChromeDriver();
       
        private void Form1_Load(object sender, EventArgs e)
        {
           
           
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int takipsayi,takipcisayi;
            driver.Navigate().GoToUrl("https://www.instagram.com/?hl=tr");
            Thread.Sleep(2500);
            IWebElement userName = driver.FindElement(By.Name("username"));
            IWebElement password = driver.FindElement(By.Name("password"));
            userName.SendKeys(textBox1.Text);
            password.SendKeys(textBox2.Text);
            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[3]")).Click();//göndere basar
            Thread.Sleep(4000);
            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[1]/div/div/div/div/div[2]/div[8]/div/div/a/div")).Click();
            Thread.Sleep(2000);
            
            takipsayi = Convert.ToInt16(driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/section/main/div/header/section/ul/li[2]/a/span/span/span")).Text);//takip sayýsýný alýyorum
           
            Thread.Sleep(1000);
            takipcisayi = Convert.ToInt16(driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/section/main/div/header/section/ul/li[3]/a/span/span/span")).Text);//takipci sayýsýný alýyorum
            string[] takip = new string[takipsayi];
            string[] takipci = new string[takipcisayi];
            Thread.Sleep(2500);
            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/section/main/div/header/section/ul/li[2]/a")).Click();//takip listeler

           

            Thread.Sleep(2500);

            //takip scroll start
            string jsCommand = "" +
                "sayfa = document.querySelector('._aano');" +
                "sayfa.scrollTo(0,sayfa.scrollHeight);" +
                "var sayfaSonu= sayfa.scrollHeight;" +
                "return sayfaSonu;";
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

            var sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));

            while (true)
            {
                var son = sayfaSonu;
                Thread.Sleep(1250);

                 sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
                if (son == sayfaSonu)
                    break;
            }
            //takip scroll end


            // takipci start
            int sayac = 0;

            IReadOnlyCollection<IWebElement> follwers = driver.FindElements(By.CssSelector(".x9f619.xjbqb8w.x1rg5ohu.x168nmei.x13lgxp2.x5pf9jr.xo71vjh.x1n2onr6.x1plvlek.xryxfnj.x1c4vz4f.x2lah0s.x1q0g3np.xqjyukv.x6s0dn4.x1oa3qoh.x1nhvcw1"));
            foreach(IWebElement follower in follwers)
            {
                takip[sayac] = follower.Text;
                sayac++;
            }
            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div/div[2]/div/div/div/div/div[2]/div/div/div[1]/div/div[3]/div/button")).Click();
            Thread.Sleep(250);

            //end

            driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[1]/div/div/div/div[1]/div[1]/div[2]/section/main/div/header/section/ul/li[3]/a/span")).Click();//takipci listeler
            Thread.Sleep(2500);


            //takipci scroll start
             jsCommand = "" +
                "sayfa = document.querySelector('._aano');" +
                "sayfa.scrollTo(0,sayfa.scrollHeight);" +
                "var sayfaSonu= sayfa.scrollHeight;" +
                "return sayfaSonu;";
             js = (IJavaScriptExecutor)driver;

             sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));

            while (true)
            {
                var son = sayfaSonu;
                Thread.Sleep(1250);

                sayfaSonu = Convert.ToInt32(js.ExecuteScript(jsCommand));
                if (son == sayfaSonu)
                    break;
            }
            //takipci scroll end


            // takipci start
             sayac = 0;

            IReadOnlyCollection<IWebElement> follwerss = driver.FindElements(By.CssSelector(".x9f619.xjbqb8w.x1rg5ohu.x168nmei.x13lgxp2.x5pf9jr.xo71vjh.x1n2onr6.x1plvlek.xryxfnj.x1c4vz4f.x2lah0s.x1q0g3np.xqjyukv.x6s0dn4.x1oa3qoh.x1nhvcw1"));
            foreach (IWebElement follower in follwerss)
            {
                takipci[sayac] = follower.Text;
                sayac++;
            }

            //end
            driver.Quit();


            //karsýlastýrma
            if (checkBox1.Checked==true)
            {
                bool takipediyormu = false;
                for (int i = 0; i < takipcisayi; i++)
                {

                    for (int j = 0; j < takipsayi; j++)
                    {
                        if (takipci[i] == takip[j])
                        {
                            takipediyormu = true;
                            break;
                        }
                    }
                    if (takipediyormu == false)
                    {
                        richTextBox1.Text += "\n ==>" + takipci[i];
                    }
                    takipediyormu = false;

                }
            }
            else
            {
                bool takipediyormu = false;
                for (int i = 0; i < takipcisayi; i++)
                {

                    for (int j = 0; j < takipsayi; j++)
                    {
                        if (takipci[i] == takip[j])
                        {

                            takipediyormu = true;
                            break;
                        }
                    }
                    if (takipediyormu == false && takipci[i].IndexOf("Doðrulanmýþ", 0, takipci[i].Length) == -1)
                    {
                        richTextBox1.Text += "\n ==>" + takipci[i];
                    }
                    takipediyormu = false;

                }
            }
           
        }
    }
}