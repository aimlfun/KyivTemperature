//#define DEBUG_DUMP_NEURAL_NETWORK_TO_FILE // <- uncomment if you want to see the neural network weightings/biases etc
using System.Text;

namespace Temperature;

/// <summary>
///    _   _                      _   _   _      _                      _
///   | \ | | ___ _   _ _ __ __ _| | | \ | | ___| |___      _____  _ __| | __
///   |  \| |/ _ \ | | | '__/ _` | | |  \| |/ _ \ __\ \ /\ / / _ \| '__| |/ /
///   | |\  |  __/ |_| | | | (_| | | | |\  |  __/ |_ \ '  ' / (_) | |  |   < 
///   |_| \_|\___|\__,_|_|  \__,_|_| |_| \_|\___|\__| \_/\_/ \___/|_|  |_|\_\
///
/// Implementation of a feedforward neural network.
/// </summary>
public class NeuralNetwork
{
    #region CONSTANTS
    /// <summary>
    /// The maximum gradient allowed, to prevent the network from blowing up.
    /// </summary>
    private const double c_maxGradient = 1000f;

    /// <summary>
    /// SELU alpha.
    /// </summary>
    private const float c_SeLUalpha = 1.6732f;

    /// <summary>
    /// SELU lambda.
    /// </summary>
    private const float c_SeLUlambda = 1.0507f;
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public enum InitType { Random, Xavier, Gaussian };

    /// <summary>
    /// 
    /// </summary>
    private readonly InitType _initType;

    /// <summary>
    /// Indicates whether to use the Adam optimizer.
    /// </summary>
    private readonly bool _adamOptimiser;

    /// <summary>
    /// How many layers of neurons (2+). Do not do 1.
    /// </summary>
    internal readonly int[] Layers;

    /// <summary>
    /// The neurons.
    /// [layer][neuron]
    /// </summary>
    internal double[][] Neurons;

    /// <summary>
    /// NN Biases. Either improves or lowers the chance of this neuron fully firing.
    /// [layer][neuron]
    /// </summary>
    internal double[][] Biases;

    /// <summary>
    /// NN weights. Reduces or amplifies the output for the relationship between neurons in each layer
    /// [layer][neuron][neuron]
    /// </summary>
    internal double[][][] Weights;

    #region BACK PROPAGATION
    /// <summary>
    /// Controls the speed of back-propagation (too large: oscillation will occur, too small: takes forever to train).
    /// </summary>
    internal float LearningRate { get; set; } = 0.01f;
    #endregion

    #region "Adam Optimizer" variables and constants
    /// <summary>
    /// Parameters for the Adam optimizer.
    /// </summary>
    private const float c_beta1 = 0.9f;
    private const float c_beta2 = 0.999f;
    private const float c_epsilon = 1e-8f;

    private double[][][] _m;
    private double[][][] _v;
    private double[][] _mBias;
    private double[][] _vBias;

    private int _t = 0;
    #endregion

    /// <summary>
    /// Lock applied to ensure that the same weights and biases are used for all instances of the NeuralNetwork.
    /// </summary>
    private static readonly Lock lockObject = new();

#if DEBUG_DUMP_NEURAL_NETWORK_TO_FILE
    /// <summary>
    /// Used to sequentially write the networks to file.
    /// </summary>
    static int networkIndex = 0;
#endif

    /// <summary>
    /// Constructor.
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Init*() set the fields.
    internal NeuralNetwork(int[] layerDefinition, InitType initType, bool useAdam)
#pragma warning restore CS8618
    {
        _initType = initType;

        lock (lockObject)
        {
            BiasWeightingCache.SameWeightsBiases(layerDefinition, _initType);
        }

        // (1) INPUT (2) HIDDEN (3) OUTPUT.
        if (layerDefinition.Length < 2) throw new ArgumentException(nameof(layerDefinition) + " insufficient layers.");

        // copy layerDefinition to Layers.     
        Layers = new int[layerDefinition.Length];

        for (int layer = 0; layer < layerDefinition.Length; layer++)
        {
            Layers[layer] = layerDefinition[layer];
        }

        _t = 0;
        _adamOptimiser = useAdam;

        InitialiseNeurons();
        InitialiseBiases();
        InitialiseWeights();

        InitialiseAdam();

#if DEBUG_DUMP_NEURAL_NETWORK_TO_FILE
        File.WriteAllText(@$"c:\temp\net-{networkIndex++}.txt", DumpOfWeightingsAndBiases());
#endif
    }

    /// <summary>
    /// SeLU activation function.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static double SeLUActivationFunction(double value)
    {
        return value > 0 ? c_SeLUlambda * value : c_SeLUlambda * c_SeLUalpha * (Math.Exp(value) - 1);
    }

    /// <summary>
    /// Derivative of SeLU activation function.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private static double DerivativeOfSeLUActivationFunction(double input)
    {
        return input > 0 ? c_SeLUlambda : c_SeLUlambda * c_SeLUalpha * Math.Exp(input);
    }

    /// <summary>
    /// Create empty storage array for the neurons in the network.
    /// </summary>
    private void InitialiseNeurons()
    {
        List<double[]> neuronsList = [];

        for (int layer = 0; layer < Layers.Length; layer++)
        {
            neuronsList.Add(new double[Layers[layer]]);
        }

        Neurons = [.. neuronsList];
    }

    /// <summary>
    /// initializes and populates biases.
    /// </summary>
    internal void InitialiseBiases()
    {
        Biases = BiasWeightingCache.GetBiases(_initType, Layers);
    }

    /// <summary>
    /// initializes random array for the weights being held in the network.
    /// </summary>
    internal void InitialiseWeights()
    {
        Weights = BiasWeightingCache.GetWeights(_initType, Layers);
    }

    /// <summary>
    /// Feed forward, inputs >==> outputs.
    /// </summary>
    /// <param name="inputs"></param>
    /// <returns></returns>
    internal double[] FeedForward(double[] inputs)
    {
        // put the INPUT values into layer 0 neurons
        for (int i = 0; i < inputs.Length; i++)
        {
            Neurons[0][i] = inputs[i];
        }

        // we start on layer 1 as we are computing values from prior layers (layer 0 is inputs)

        for (int layer = 1; layer < Layers.Length; layer++)
        {
            for (int neuronIndexForLayer = 0; neuronIndexForLayer < Layers[layer]; neuronIndexForLayer++)
            {
                // sum of outputs from the previous layer
                double value = 0f;

                for (int neuronIndexInPreviousLayer = 0; neuronIndexInPreviousLayer < Layers[layer - 1]; neuronIndexInPreviousLayer++)
                {
                    value += Weights[layer - 1][neuronIndexForLayer][neuronIndexInPreviousLayer] * Neurons[layer - 1][neuronIndexInPreviousLayer];
                }

                Neurons[layer][neuronIndexForLayer] = SeLUActivationFunction(value + Biases[layer - 1][neuronIndexForLayer]);
            }
        }

        return Neurons[^1]; // final* layer contains OUTPUT
    }

    /// <summary>
    /// Initialise Adam optimizer.
    /// </summary>
    internal void InitialiseAdam()
    {
        _m = new double[Weights.Length][][];
        _v = new double[Weights.Length][][];
        _mBias = new double[Biases.Length][];
        _vBias = new double[Biases.Length][];

        for (int i = 0; i < Weights.Length; i++)
        {
            _m[i] = new double[Weights[i].Length][];
            _v[i] = new double[Weights[i].Length][];

            for (int j = 0; j < Weights[i].Length; j++)
            {
                _m[i][j] = new double[Weights[i][j].Length];
                _v[i][j] = new double[Weights[i][j].Length];
            }
        }

        for (int i = 0; i < Biases.Length; i++)
        {
            _mBias[i] = new double[Biases[i].Length];
            _vBias[i] = new double[Biases[i].Length];
        }
    }

    /// <summary>
    /// Back propagate training.
    /// </summary>
    /// <param name="inputs"></param>
    /// <param name="expected"></param>
    public void BackPropagate(double[] inputs, double[] expected)
    {
        if (_adamOptimiser)
        {
            BackPropagateAdam(inputs, expected);
        }
        else
        {
            BackPropagateNoOptimiser(inputs, expected);
        }
    }

    /// <summary>
    /// Back propagate training, without using the Adam optimizer.
    /// </summary>
    /// <param name="inputs"></param>
    /// <param name="expected"></param>
    public void BackPropagateNoOptimiser(double[] inputs, double[] expected)
    {
        double[] output = FeedForward(inputs);

        List<double[]> gammaList = [];

        for (int i = 0; i < Layers.Length; i++)
        {
            gammaList.Add(new double[Layers[i]]);
        }

        double[][] gamma = [.. gammaList];

        int layersMinus2 = Layers.Length - 2;
        int layersMinus1 = Layers.Length - 1;

        for (int i = 0; i < output.Length; i++)
        {
            // for each output, workout the error scaled by the derivative of the activation function.
            // The "error" is fairly obvious: I expected 1, I got 0.8, I need to increase the output by 0.2.
            // The derivative of the activation function is less obvious, it's the rate of change of the activation function. That in itself
            // makes it no easier to understand. What we're doing is scaling the error based on how much 1 unit from upstream would have affected
            // the output. 

            double error = output[i] - expected[i];
            gamma[layersMinus1][i] = ClampGradient(error) * DerivativeOfSeLUActivationFunction(output[i]);
        }

        /*
            First Moment Estimate (m):
            •	m is an exponentially decaying average of past gradients used to smooth out the gradient updates, reducing the 
                variance and making the optimization process more stable.
        
            Second Moment Estimate (v):
            •	v is an exponentially decaying average of the squared gradients, helping to scale the learning rate for each 
                parameter individually, adapting the learning rate based on the magnitude of past gradients.
        */
        for (int i = 0; i < Layers[^1]; i++)
        {
            Biases[layersMinus2][i] -= gamma[layersMinus1][i] * LearningRate;

            for (int j = 0; j < Layers[^2]; j++)
            {
                Weights[layersMinus2][i][j] -= gamma[layersMinus1][i] * Neurons[layersMinus2][j] * LearningRate;
            }
        }

        for (int i = Layers.Length - 2; i > 0; i--)
        {
            int layer = i - 1;
            int layerPlus1 = i + 1;

            for (int j = 0; j < Layers[i]; j++)
            {
                for (int k = 0; k < gamma[layerPlus1].Length; k++)
                {
                    gamma[i][j] += ClampGradient(gamma[layerPlus1][k] * Weights[i][k][j]);
                }

                gamma[i][j] *= ClampGradient(DerivativeOfSeLUActivationFunction(Neurons[i][j]));

                Biases[layer][j] -= gamma[i][j] * LearningRate; // modify biases of network

                for (int k = 0; k < Layers[layer]; k++)
                {
                    Weights[layer][j][k] -= gamma[i][j] * Neurons[layer][k] * LearningRate; // modify weights of network
                }
            }
        }
    }

    /// <summary>
    /// Back propagate training, using the Adam optimizer.
    /// </summary>
    /// <param name="inputs"></param>
    /// <param name="expected"></param>
    public void BackPropagateAdam(double[] inputs, double[] expected)
    {
        double[] output = FeedForward(inputs);

        List<double[]> gammaList = [];

        for (int i = 0; i < Layers.Length; i++)
        {
            gammaList.Add(new double[Layers[i]]);
        }

        double[][] gamma = [.. gammaList];

        int layersMinus2 = Layers.Length - 2;
        int layersMinus1 = Layers.Length - 1;

        for (int i = 0; i < output.Length; i++)
        {
            // for each output, workout the error scaled by the derivative of the activation function.
            // The "error" is fairly obvious: I expected 1, I got 0.8, I need to increase the output by 0.2.
            // The derivative of the activation function is less obvious, it's the rate of change of the activation function. That in itself
            // makes it no easier to understand. What we're doing is scaling the error based on how much 1 unit from upstream would have affected
            // the output. 

            double error = output[i] - expected[i];
            gamma[layersMinus1][i] = ClampGradient(error) * DerivativeOfSeLUActivationFunction(output[i]);
        }

        _t++;

        /*
            First Moment Estimate (m):
            •	m is an exponentially decaying average of past gradients used to smooth out the gradient updates, reducing the 
                variance and making the optimization process more stable.
        
            Second Moment Estimate (v):
            •	v is an exponentially decaying average of the squared gradients, helping to scale the learning rate for each 
                parameter individually, adapting the learning rate based on the magnitude of past gradients.
        */
        for (int i = 0; i < Layers[^1]; i++)
        {
            _mBias[layersMinus2][i] = c_beta1 * _mBias[layersMinus2][i] + (1 - c_beta1) * gamma[layersMinus1][i];
            _vBias[layersMinus2][i] = c_beta2 * _vBias[layersMinus2][i] + (1 - c_beta2) * Math.Pow(gamma[layersMinus1][i], 2);

            // mHatBias is the bias-corrected first moment estimate, ensuring that the gradient direction is accurate.
            // vHatBias is the bias-corrected second moment estimate, ensuring that the learning rate is appropriately scaled, preventing overly large or small updates.
            double mHatBias = _mBias[layersMinus2][i] / (1 - Math.Pow(c_beta1, _t));
            double vHatBias = _vBias[layersMinus2][i] / (1 - Math.Pow(c_beta2, _t));

            /*
                Bias Correction:
                •	When m and v are first initialized, they are biased towards zero, especially during the initial steps of training.
                •	To correct this bias, mHatBias and vHatBias are the bias-corrected versions of m and v, respectively. They adjust the 
                    estimates to account for the initial bias towards zero.

                By using mHatBias and vHatBias, the Adam optimizer combines the benefits of both momentum (smoothing out updates) and adaptive 
                learning rates (scaling updates based on gradient magnitudes), leading to more efficient and stable training.
            */
            Biases[layersMinus2][i] -= LearningRate * mHatBias / (Math.Sqrt(vHatBias) + c_epsilon);

            for (int j = 0; j < Layers[^2]; j++)
            {
                // adjust the weights based on the gradient and the learning rate.
                _m[layersMinus2][i][j] = c_beta1 * _m[layersMinus2][i][j] + (1 - c_beta1) * gamma[layersMinus1][i] * Neurons[layersMinus2][j];
                _v[layersMinus2][i][j] = c_beta2 * _v[layersMinus2][i][j] + (1 - c_beta2) * Math.Pow(gamma[layersMinus1][i] * Neurons[layersMinus2][j], 2);

                double mHat = _m[layersMinus2][i][j] / (1 - Math.Pow(c_beta1, _t));
                double vHat = _v[layersMinus2][i][j] / (1 - Math.Pow(c_beta2, _t));

                Weights[layersMinus2][i][j] -= LearningRate * mHat / (Math.Sqrt(vHat) + c_epsilon);
            }
        }

        for (int i = Layers.Length - 2; i > 0; i--)
        {
            int layer = i - 1;
            int layerPlus1 = i + 1;

            for (int j = 0; j < Layers[i]; j++)
            {
                for (int k = 0; k < gamma[layerPlus1].Length; k++)
                {
                    gamma[i][j] += ClampGradient(gamma[layerPlus1][k] * Weights[i][k][j]);
                }

                gamma[i][j] *= ClampGradient(DerivativeOfSeLUActivationFunction(Neurons[i][j]));

                _mBias[layer][j] = c_beta1 * _mBias[layer][j] + (1 - c_beta1) * gamma[i][j];
                _vBias[layer][j] = c_beta2 * _vBias[layer][j] + (1 - c_beta2) * Math.Pow(gamma[i][j], 2);

                double mHatBias = _mBias[layer][j] / (1 - Math.Pow(c_beta1, _t));
                double vHatBias = _vBias[layer][j] / (1 - Math.Pow(c_beta2, _t));
                Biases[layer][j] -= LearningRate * mHatBias / (Math.Sqrt(vHatBias) + c_epsilon);

                for (int k = 0; k < Layers[layer]; k++)
                {
                    _m[layer][j][k] = c_beta1 * _m[layer][j][k] + (1 - c_beta1) * gamma[i][j] * Neurons[layer][k];
                    _v[layer][j][k] = c_beta2 * _v[layer][j][k] + (1 - c_beta2) * Math.Pow(gamma[i][j] * Neurons[layer][k], 2);

                    double mHat = _m[layer][j][k] / (1 - Math.Pow(c_beta1, _t));
                    double vHat = _v[layer][j][k] / (1 - Math.Pow(c_beta2, _t));

                    Weights[layer][j][k] -= LearningRate * mHat / (Math.Sqrt(vHat) + c_epsilon);
                }
            }
        }
    }

    /// <summary>
    /// To prevent the gradient becoming too large, we clamp it.
    /// SELU doesn't have a positive limit, so it's quite possible to blow up and get Infinity -> NaN.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static double ClampGradient(double value)
    {
        // clip range
        if (value < -c_maxGradient) return -c_maxGradient;
        if (value > c_maxGradient) return c_maxGradient;

        return value;
    }

    /// <summary>
    /// Returns a string representation of the weights and biases.
    /// </summary>
    /// <returns></returns>
    internal string DumpOfWeightingsAndBiases()
    {
        StringBuilder sb = new();
        for (int i = 0; i < Weights.Length; i++)
        {
            sb.Append($"L[{i + 1}] Weights ");

            // weights
            for (int j = 0; j < Weights[i].Length; j++)
            {
                for (int k = 0; k < Weights[i][j].Length; k++)
                {
                    sb.Append($"{Weights[i][j][k]:0.000} ");
                }
            }

            // biases
            sb.Append("Biases ");
            for (int j = 0; j < Biases[i].Length; j++)
            {
                sb.Append($"{Biases[i][j]:0.000} ");
            }
        }

        sb.AppendLine("");
        return sb.ToString();
    }
}