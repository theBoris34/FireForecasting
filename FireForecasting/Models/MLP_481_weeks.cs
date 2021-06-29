using System;
using System.Collections.Generic;
using System.Text;

namespace FireForecasting.Models
{
    class MLP_481_weeks
    {
        double[,] Spreadsheet35_13_MLP_4_8_1_input_hidden_weights = new double [8, 4]
        {
            {-1.05783429680007e+000, 8.13799306324800e-001, -2.02523695064535e-001, -4.02567842000066e-002 },
            {-2.65376501915090e-001, 8.11072681323367e-001, -8.05468297385855e-002, 3.54867069651682e-001 },
            {-2.06126459979332e+000, -1.20743953692101e+000, 4.36699977696720e-001, 6.50886506060193e-001 },
            {-1.40718895154064e+000, -1.66516167536356e+000, 4.11804817460383e+000, -3.51159623200115e+000 },
            {3.23405720073195e+000, 3.53883734166772e+000, -3.94887793960177e+000, 1.26319407623311e+000 },
            {-6.94313377999530e-001, 1.48285818119369e+000, 2.37789586809132e+000, -4.81298977135300e-001 },
            {2.49831337090176e+000, -5.18313480145128e-001, 4.58786533626754e+000, -4.26587173815548e+000 },
            {5.98769740612207e-002, -1.31233669602043e+000, 3.60075769656696e+000, -4.30680525343899e-001 } 
        };

        double[] Spreadsheet35_13_MLP_4_8_1_hidden_bias = new double[8] { 4.53719084705230e-001, 5.90747439645833e-001, -1.06043294951180e-001, 1.46931919048807e+000, -1.86848571342829e+000, 3.64340485106457e-001, -1.57578225619267e-002, -3.02203426643688e-001 };

        double[,] Spreadsheet35_13_MLP_4_8_1_hidden_output_wts = new double[1,8]    
        {   
            {8.64859821952411e-001, 3.32023785062415e-001, 1.81960935773748e+000, -3.28352582388436e+000, -1.37937951215287e+000, 1.12256250955177e+000, 2.30871233692687e+000, -1.17213610515434e+000 }
        };
        
        double[] Spreadsheet35_13_MLP_4_8_1_output_bias = new double[1] { -1.86127933717727e-002 };

        double[] Spreadsheet35_13_MLP_4_8_1_max_input = new double[4] { 4.20000000000000e+001, 4.20000000000000e+001, 4.20000000000000e+001, 4.20000000000000e+001 };

        double[] Spreadsheet35_13_MLP_4_8_1_min_input = new double[4] { 3.00000000000000e+000, 3.00000000000000e+000, 3.00000000000000e+000, 3.00000000000000e+000 };

        double[] Spreadsheet35_13_MLP_4_8_1_max_target = new double[1] { 4.20000000000000e+001 };

        double[] Spreadsheet35_13_MLP_4_8_1_min_target = new double[1] { 3.00000000000000e+000 };

        double[] Spreadsheet35_13_MLP_4_8_1_input = new double[4];
        double[] Spreadsheet35_13_MLP_4_8_1_hidden = new double[8];
        double[] Spreadsheet35_13_MLP_4_8_1_output = new double[1];

        double[] Spreadsheet35_13_MLP_4_8_1_MeanInputs = new double[1] { 1.63525179856115e+001 };

        public void Spreadsheet35_13_MLP_4_8_1_ScaleInputs(double [] input, double minimum, double maximum, int nCategoricalInputs, int nContInputs, int steps)
        {
            double delta;
            long i, j, n;
            for (j = 0, n = 0; j < steps; j++, n += nCategoricalInputs)
            {
                for (i = 0; i < nContInputs; i++)
                {
                    delta = (maximum - minimum) / (Spreadsheet35_13_MLP_4_8_1_max_input[i] - Spreadsheet35_13_MLP_4_8_1_min_input[i]);
                    input[n] = minimum - delta * Spreadsheet35_13_MLP_4_8_1_min_input[i] + delta * input[n];
                    n++;
                }
            }
        }

        void Spreadsheet35_13_MLP_4_8_1_UnscaleTargets(double[] output, double minimum, double maximum, int size)
        {
            double delta;
            long i;
            for (i = 0; i < size; i++)
            {
                delta = (maximum - minimum) / (Spreadsheet35_13_MLP_4_8_1_max_target[i] - Spreadsheet35_13_MLP_4_8_1_min_target[i]);
                output[i] = (output[i] - minimum + delta * Spreadsheet35_13_MLP_4_8_1_min_target[i]) / delta;
            }
        }

        double Spreadsheet35_13_MLP_4_8_1_logistic(double x)
        {
            if (x > 100.0) x = 1.0;
            else if (x < -100.0) x = 0.0;
            else x = 1.0 / (1.0 + Math.Exp(-x));
            return x;
        }

        void Spreadsheet35_13_MLP_4_8_1_ComputeFeedForwardSignals(double[,] MAT_INOUT, double[] V_IN, double[] V_OUT, double[] V_BIAS, int size1, int size2, int layer)
        {
            int row, col;
            for (row = 0; row < size2; row++)
            {
                V_OUT[row] = 0.0;

                for (col = 0; col < size1; col++) 
                    V_OUT[row] += ((MAT_INOUT[row,col] + (row * size1) + col) * V_IN[col]);

                V_OUT[row] += V_BIAS[row];
                if (layer == 0) V_OUT[row] = Spreadsheet35_13_MLP_4_8_1_logistic(V_OUT[row]);
            }
        }

        void Spreadsheet35_13_MLP_4_8_1_RunNeuralNet_TS_Reg()
        {
            Spreadsheet35_13_MLP_4_8_1_ComputeFeedForwardSignals(Spreadsheet35_13_MLP_4_8_1_input_hidden_weights, Spreadsheet35_13_MLP_4_8_1_input, Spreadsheet35_13_MLP_4_8_1_hidden, Spreadsheet35_13_MLP_4_8_1_hidden_bias, 4, 8, 0);
            Spreadsheet35_13_MLP_4_8_1_ComputeFeedForwardSignals(Spreadsheet35_13_MLP_4_8_1_hidden_output_wts, Spreadsheet35_13_MLP_4_8_1_hidden, Spreadsheet35_13_MLP_4_8_1_output, Spreadsheet35_13_MLP_4_8_1_output_bias, 8, 1, 1);
        }

        public int Main_1()
        {
            int i = 0;
            int keyin = 1;
            int stepcntr;
            int inputindex;
            int cont_inps_idx;
            int nsteps;
            while (true)
            {
                stepcntr = 1;
                inputindex = 0;
                for (nsteps = 0; nsteps < 4; nsteps++)
                {
                    inputindex -= 1;
                    //Substitution of missing continuous variables
                    
            //        scanf("%lg", &Spreadsheet35_13_MLP_4_8_1_input[inputindex++]);
                    for (cont_inps_idx = 0; cont_inps_idx < 1; cont_inps_idx++)
                    {

                        //scanf("%lg", &Spreadsheet35_13_MLP_4_8_1_input[inputindex++]);
                        if (Spreadsheet35_13_MLP_4_8_1_input[inputindex] == -9999)
                            Spreadsheet35_13_MLP_4_8_1_input[inputindex] = Spreadsheet35_13_MLP_4_8_1_MeanInputs[cont_inps_idx];
                        inputindex++;
                    }

                }
                Spreadsheet35_13_MLP_4_8_1_ScaleInputs(Spreadsheet35_13_MLP_4_8_1_input, 0, 1, 0, 1, 4);
                Spreadsheet35_13_MLP_4_8_1_RunNeuralNet_TS_Reg();
                Spreadsheet35_13_MLP_4_8_1_UnscaleTargets(Spreadsheet35_13_MLP_4_8_1_output, 0, 1, 1);
                keyin += 1;
                if (keyin == 48) break;
            }

            //while (1)
            //{
            //    stepcntr = 1;
            //    inputindex = 0;
            //    printf("\n%s\n", "Enter values for Continuous inputs (To skip a continuous input please enter -9999)");
            //    for (nsteps = 0; nsteps < 4; nsteps++)
            //    {
            //        printf("\n%s%d\n", "Enter Input values for Step ", stepcntr++);
            //        printf("%s", "Cont. Input-0(NewVar): ");
            //        scanf("%lg", &Spreadsheet35_13_MLP_4_8_1_input[inputindex++]);
            //        inputindex -= 1;
            //        //Substitution of missing continuous variables
            //        for (cont_inps_idx = 0; cont_inps_idx < 1; cont_inps_idx++)
            //        {
            //            if (Spreadsheet35_13_MLP_4_8_1_input[inputindex] == -9999)
            //                Spreadsheet35_13_MLP_4_8_1_input[inputindex] = Spreadsheet35_13_MLP_4_8_1_MeanInputs[cont_inps_idx];
            //            inputindex++;
            //        }
            //    }
            //    Spreadsheet35_13_MLP_4_8_1_ScaleInputs(Spreadsheet35_13_MLP_4_8_1_input, 0, 1, 0, 1, 4);
            //    Spreadsheet35_13_MLP_4_8_1_RunNeuralNet_TS_Reg();
            //    Spreadsheet35_13_MLP_4_8_1_UnscaleTargets(Spreadsheet35_13_MLP_4_8_1_output, 0, 1, 1);
            //    printf("\n%s%.14e", "Predicted Output of NewVar = ", Spreadsheet35_13_MLP_4_8_1_output[0]);
            //    printf("\n\n%s\n", "Press any key to make another prediction or enter 0 to quit the program.");
            //    keyin = getch();
            //    if (keyin == 48) break;
            //}
            return 0;
        }

    }


}
