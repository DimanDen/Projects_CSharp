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
        MainParams mainParams = new MainParams(10000);

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

        private void buttonRegularization_Click(object sender, EventArgs e)
        {
            double Qteor = double.Parse(textBoxQteor.Text);
            int t_prognoz = int.Parse(textBoxTp.Text);
            int T_AKF = int.Parse(textBox_T_AKF.Text);
            int t1 = int.Parse(textBoxT1.Text);
            double M_l;
            double D_l;
            double M_E = 0;
            double D_E = 0;
            int localInterval = int.Parse(textBoxTeachInterval.Text);
            double Qexp;
            double Q_SKO;
            double[] coef_a = new double[2];
            double[] akfPoints = new double[3];
            List<double> localError = new List<double>();
            int t_finish = (mainParams.numbOfPointsRealization / 2) - 1;
            int t_start = t_finish - localInterval;
            List<double> localYprognoz = new List<double>();

            while (localInterval <= mainParams.numbOfPointsRealization / 2)
            {
                t_finish = (mainParams.numbOfPointsRealization / 2) - 1;
                t_start = t_finish - localInterval + 1 ;

                M_l = getMO(t_start, t_finish, mainParams.realizationAfterFF);
                D_l = getD(t_start, t_finish, M_l, mainParams.realizationAfterFF);
                while (t_finish < mainParams.numbOfPointsRealization)
                { //Заменить ТАКФ на нужное и поменять формулу


                    //akfPoints[0] = getK(t_start, t_finish, t_prognoz, M_l, mainParams.realizationAfterFF);
                    //akfPoints[1] = getK(t_start, t_finish, t1, M_l, mainParams.realizationAfterFF);
                    //akfPoints[2] = getK(t_start, t_finish, t_prognoz + t1, M_l, mainParams.realizationAfterFF);

                    coef_a[0] = (getK(t_start, t_finish, t_prognoz, M_l, mainParams.realizationAfterFF) * D_l -
                        getK(t_start, t_finish, t1, M_l, mainParams.realizationAfterFF) * getK(t_start, t_finish, t_prognoz + t1, M_l, mainParams.realizationAfterFF)) /
                        (Math.Pow(D_l, 2) - (Math.Pow(getK(t_start, t_finish, t1, M_l, mainParams.realizationAfterFF), 2)));

                    coef_a[1] = (getK(t_start, t_finish, t_prognoz + t1, M_l, mainParams.realizationAfterFF) * D_l -
                        getK(t_finish, t_prognoz, t_start, M_l, mainParams.realizationAfterFF) * getK(t_start, t_finish, t1, M_l, mainParams.realizationAfterFF)) /
                        (Math.Pow(D_l, 2) - (Math.Pow(getK(t_start, t_finish, t1, M_l, mainParams.realizationAfterFF), 2)));


                    localYprognoz.Add(
                             (M_l +
                             (mainParams.realizationAfterFF[t_finish] - M_l) * coef_a[0] +
                             (mainParams.realizationAfterFF[t_finish - t1] - M_l) * coef_a[1]));

                    t_start++;
                    t_finish++; 
                }


                for (int i = 0; i < mainParams.numbOfPointsRealization / 2 - t_prognoz - 1; i++)
                {
                    localError.Add(
                        localYprognoz[i] - mainParams.realizationAfterFF[mainParams.numbOfPointsRealization / 2 + i + t_prognoz]
                        );
                }

                M_E = getMO(0, localError.Count - 1, localError);
                D_E = getD(0, localError.Count - 1, M_E, localError);

                Qexp = Math.Pow(M_E, 2) + D_E;
                Q_SKO = Math.Pow(Qteor - Qexp, 2);

                if (localInterval == 5000)
                {
                    Console.WriteLine();
                }

                if (Q_SKO < mainParams.QminSKO)
                {
                    mainParams.QminSKO = Q_SKO;
                    mainParams.teachInterval = localInterval;
                    mainParams.Yprognoz = new List<double>(localYprognoz);
                    mainParams.Error = localError;
                }

                localError.Clear();
                localYprognoz.Clear();
                localInterval+=5;
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
        public int teachInterval;
        public double M_lambada;
        public double D_lambada;
        public List<double> Yprognoz = new List<double>();
        public List<double> Error = new List<double>();
        public double QminSKO = 99;

        public MainParams(int numb)
        {
            numbOfPointsRealization = numb;
            realizationAfterFF = new double[numb];
        }
    }
}
