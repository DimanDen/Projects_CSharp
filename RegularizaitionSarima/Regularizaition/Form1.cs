using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Regularizaition
{
    public partial class Form1 : Form
    {
        MainParams mainParams = new MainParams(564);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadRealization_Click(object sender, EventArgs e)
        {
            string tempStr = FileWorker.ReadFromFile().Replace('.', ',');
            tempStr.Replace("   ", "");
            String[] tempStrings = tempStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            double sum = 0;
            for (int i = 0; i < tempStrings.Length; i++)
            {
                mainParams.realizationAfterFF[i] = double.Parse(tempStrings[i]);
                sum += mainParams.realizationAfterFF[i];
            }
        }

        private void btmLoadComponents_Click(object sender, EventArgs e)
        {
            string tempStr = FileWorker.ReadFromFile().Replace('.', ',');
            tempStr.Replace("   ", "");
            tempStr.Replace("e+005", "e+5");
            String[] tempStrings = tempStr.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempStrings.Length; i++)
            {
                mainParams.trend[i] = double.Parse(tempStrings[i]);
            }

            string tempStr1 = FileWorker.ReadFromFile().Replace('.', ',');
            tempStr1.Replace("   ", "");
            String[] tempStrings1 = tempStr1.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempStrings1.Length; i++)
            {
                mainParams.mo_T[i] = double.Parse(tempStrings1[i]);
            }

            string tempStr2 = FileWorker.ReadFromFile().Replace('.', ',');
            tempStr2.Replace("   ", "");
            String[] tempStrings2 = tempStr2.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempStrings2.Length; i++)
            {
                mainParams.stohastic[i] = double.Parse(tempStrings2[i]);
            }

            string tempStr3 = FileWorker.ReadFromFile().Replace('.', ',');
            tempStr3.Replace("   ", "");
            String[] tempStrings3 = tempStr3.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempStrings3.Length; i++)
            {
                mainParams.derivativeSth[i] = double.Parse(tempStrings3[i]);
            }
        }

        private void buttonRegularization_Click(object sender, EventArgs e)
        {
            double Qteor = 2.0;
            int t_prognoz = int.Parse(textBoxTp.Text);
            int T_AKF = int.Parse(textBox_T_AKF.Text);
            int t1 = int.Parse(textBoxT1.Text);

            int perAKF = 12;
            int N0 = 281;
            int N = 563;
            int tk = int.Parse(textBoxTk.Text);
            int s1;
            int s2;
            int s3;
            List<double> mounthDer = new List<double>();
            List<double> y_Sth = new List<double>();
            double qminExp = 999999999999999;


            double M_l;
            double D_l;
            double M_E = 0;
            double D_E = 0;
            int localInterval = int.Parse(textBoxTeachInterval.Text);
            double Qexp;
            double Q_SKO;
            double[] coef_a = new double[2];
            double[] akfPoints = new double[4];
            List<double> localError = new List<double>();
            int t_finish = (mainParams.numbOfPointsRealization / 2) - 1;
            int t_start = t_finish - localInterval;
            List<double> localYprognoz = new List<double>();

            while (localInterval <= 281)
            {
                t_finish = 280;
                t_start = t_finish - localInterval ;

                for (int i = 0; i <= 281; i++)
                {
                    M_l = getMO(0, localInterval - 1 + i, mainParams.derivativeSth);
                    D_l = getD(0, localInterval - 1 + i, M_l, mainParams.derivativeSth);

                    s1 = (N0 - 1 + i) % 12;
                    s2 = (t_prognoz) % 12;
                    s3 = (s1 + s2) % 12;

                    for (int j = 0; j <= N0 - 1 + i - s3; j = j + 12)
                    {
                        mounthDer.Add(mainParams.derivativeSth[j+s3]);
                    }

                    akfPoints[0] = getK(0, mounthDer.Count - 1, 0, M_l, mounthDer, i);
                    akfPoints[1] = getK(0, mounthDer.Count - 1, tk, M_l, mounthDer, i);
                    akfPoints[2] = getK(0, mounthDer.Count - 1, t1, M_l, mounthDer, i);
                    akfPoints[3] = getK(0, mounthDer.Count - 1, t1 + tk, M_l, mounthDer, i);

                    coef_a[0] = (akfPoints[1] * akfPoints[0] - akfPoints[2] * akfPoints[3]) /
                        (Math.Pow(akfPoints[0], 2) - (Math.Pow(akfPoints[2], 2)));

                    coef_a[1] = (akfPoints[3] * akfPoints[0] - akfPoints[1] * akfPoints[2]) /
                        (Math.Pow(akfPoints[0], 2) - (Math.Pow(akfPoints[2], 2)));

                    localYprognoz.Add(
                        (M_l +
                        (mainParams.derivativeSth[i + (N0 - 1) + t_prognoz - 2*perAKF] - M_l) * coef_a[0] +
                        (mainParams.derivativeSth[i + (N0 - 1) + t_prognoz - 2 * perAKF] - M_l) * coef_a[1]));

                    mounthDer.Clear();
                }
                //while (t_finish < mainParams.numbOfPointsRealization)
                //{ //Заменить ТАКФ на нужное и поменять формулу
                //    M_l = getMO(t_start, t_finish, mainParams.derivativeSth);
                //    D_l = getD(t_start, t_finish, M_l, mainParams.derivativeSth);

                //    //akfPoints[0] = getK(t_start, t_finish, t_prognoz, M_l, mainParams.realizationAfterFF);
                //    //akfPoints[1] = getK(t_start, t_finish, t1, M_l, mainParams.realizationAfterFF);
                //    //akfPoints[2] = getK(t_start, t_finish, t_prognoz + t1, M_l, mainParams.realizationAfterFF);

                //    coef_a[0] = (getK(t_start, t_finish, t_prognoz, M_l, mainParams.realizationAfterFF) * D_l -
                //        getK(t_start, t_finish, t1, M_l, mainParams.realizationAfterFF) * getK(t_start, t_finish, t_prognoz + t1, M_l, mainParams.realizationAfterFF)) /
                //        (Math.Pow(D_l, 2) - (Math.Pow(getK(t_start, t_finish, t1, M_l, mainParams.realizationAfterFF), 2)));

                //    coef_a[1] = (getK(t_start, t_finish, t_prognoz + t1, M_l, mainParams.realizationAfterFF) * D_l -
                //        getK(t_finish, t_prognoz, t_start, M_l, mainParams.realizationAfterFF) * getK(t_start, t_finish, t1, M_l, mainParams.realizationAfterFF)) /
                //        (Math.Pow(D_l, 2) - (Math.Pow(getK(t_start, t_finish, t1, M_l, mainParams.realizationAfterFF), 2)));


                //    localYprognoz.Add(
                //             (M_l +
                //             (mainParams.realizationAfterFF[t_finish] - M_l) * coef_a[0] +
                //             (mainParams.realizationAfterFF[t_finish - t1] - M_l) * coef_a[1]));

                //    t_start++;
                //    t_finish++; 
                //}

                y_Sth.Add(mainParams.stohastic[280]);
                for (var l = 0; l < localYprognoz.Count - 1; l++)
                {
                    y_Sth.Add(y_Sth[l] + localYprognoz[l]);
                }
                y_Sth.RemoveAt(0);

                for (var k = 0; k < localYprognoz.Count - 1 - t_prognoz; k++)
                {
                    double temp;
                    temp = (y_Sth[k] + mainParams.mo_T[k + N0 - 1 + t_prognoz]) * mainParams.trend[k + N0 - 1 + t_prognoz];
                    localError.Add(
                        ((y_Sth[k] + mainParams.mo_T[k + N0 - 1 + t_prognoz]) * mainParams.trend[k + N0 - 1 + t_prognoz]) - mainParams.realizationAfterFF[k + N0 + t_prognoz]
                        );
                }

                M_E = getMO(0, localError.Count - 1, localError);
                D_E = getD(0, localError.Count - 1, M_E, localError);

                Qexp = Math.Pow(M_E, 2) + D_E;
              //  Q_SKO = Math.Pow(Qteor - Qexp, 2);

                if (Qexp < mainParams.QminSKO)
                {
                    mainParams.QminSKO = Qexp;
                    mainParams.teachInterval = localInterval;
                    mainParams.Yprognoz = new List<double>(localYprognoz);
                }

                //if (Q_SKO < mainParams.QminSKO)
                //{
                //    mainParams.QminSKO = Q_SKO;
                //    mainParams.teachInterval = localInterval;
                //    mainParams.Yprognoz = new List<double>(localYprognoz);
                //    mainParams.Error = localError;
                //}

                mounthDer.Clear();
                y_Sth.Clear();
                localError.Clear();
                localYprognoz.Clear();
                localInterval+=1;
            }
            textBoxQminSKO.Text = mainParams.QminSKO.ToString(); //вывод на текстбокс неправильный, убирает нули
            textBoxOptTeachInt.Text = mainParams.teachInterval.ToString();          
        }

        private double getMO(int startCount, int finishCount, double[] realization)
        {
            double localMO = 0;
            for (int i = startCount; i <= finishCount; i++)
            {
                localMO += realization[i];
            }
            localMO = localMO / (finishCount - startCount + 1);
            return localMO;
        }

        private double getMO(int startCount, int finishCount, List<double> realization)
        {
            double localMO = 0;
            for (int i = startCount; i <= finishCount; i++)
            {
                localMO += realization[i];
            }
            localMO = localMO / (finishCount - startCount + 1);
            return localMO;
        }

        private double getD(int startCount, int finishCount, double MO, double[] realization)
        {
            double localD = 0;
            for (int i = startCount; i <= finishCount; i++)
            {
                localD += Math.Pow(realization[i] - MO, 2);
            }
            localD = localD / (finishCount - startCount);
            return localD;
        }

        private double getD(int startCount, int finishCount, double MO, List<double> realization)
        {
            double localD = 0;
            for (int i = startCount; i <= finishCount; i++)
            {
                localD += Math.Pow(realization[i] - MO, 2);
            }
            localD = localD / (finishCount - startCount);
            return localD;
        }

        private double getK(int startCount, int finishCount, int point, double MO, double[] realization)
        {
            double localK = 0;
            for (int i = 0; i <= (finishCount-startCount) - point; i++)
            {
                localK += (realization[i] * realization[i + point]);
            }
            localK -= ((finishCount - startCount + 1) - point) * Math.Pow(MO, 2);
            localK = localK / ((finishCount - startCount + 1) - point - 1);
            return localK;
        }

        private double getK(int startCount, int finishCount, int point, double MO, List<double> realization, int localI)
        {
            double localK = 0;
            for (int i = 0; i <= (finishCount - startCount) - point; i++)
            {
                localK += (realization[i] * realization[i + point]);
            }
            localK -= (281 + localI - point) * Math.Pow(MO, 2);
            localK = localK / (281 + localI - point);
            return localK;
        }

        private void saveResultYprognoz(List<double> localY)
        {
            string localOutStr = "";
            for (int i = 0; i < localY.Count; i++)
            {
                localOutStr += localY[i] + "\r\n";
            }
            localOutStr = localOutStr.Replace(',', '.');
            FileWorker.WriteToFile(localOutStr);
        }


        private void buttonSaveResults_Click(object sender, EventArgs e)
        {
            string localOutStr = "";
            for (int i = 0; i < mainParams.Yprognoz.Count; i++)
            {
                localOutStr += mainParams.Yprognoz[i] + "\r\n";
            }
            localOutStr = localOutStr.Replace(',', '.');
            FileWorker.WriteToFile(localOutStr);
        }
    }

    public class MainParams
    {
        public int numbOfPointsRealization;
        public double[] realizationAfterFF;
        
        public double[] trend;
        public double[] mo_T;
        public double[] stohastic;
        public double[] derivativeSth;

        public int teachInterval;
        public double M_lambada;
        public double D_lambada;
        public List<double> Yprognoz = new List<double>();
        public List<double> Error = new List<double>();
        public double QminSKO = 99999999999999999;

        public MainParams(int numb)
        {
            numbOfPointsRealization = numb;
            realizationAfterFF = new double[numb];
            trend = new double[numb];
            mo_T = new double[numb];
            stohastic = new double[numb];
            derivativeSth = new double[numb-1];
        }
    }
}
