using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Media;
using System.IO;

namespace Boxing_Coach
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer timer = new DispatcherTimer();
        public List<Exercise> exerciseList = new List<Exercise>();
        public List<int> punchesList = new List<int>();
        public List<SoundPlayer> comboList = new List<SoundPlayer>();
        public Queue<Exercise> routine = new Queue<Exercise>();
        public int tiempoRutina = 0;
        public int tiempoCombi = 0;
        public string textoRutina = "";
        public string textoCombi = "";
        public int rutinaActiva = 0;
        public int comboActivo = 0;
        public int tiempoEjercicio = 0;
        public int ejercicioActivo = 0;
        public int noEjercicio = 0;
        public int betweemCombo = 0;

        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();

            //Comboboxes
            for (int i = 1; i < 10; i++) {
                Min_Value.Items.Add(i.ToString());
                Max_Value.Items.Add(i.ToString());
            }
            Min_Value.SelectedItem = "1";
            Max_Value.SelectedItem = "9";
        }

        private void Add_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (rutinaActiva == 0) {
                try
                {
                    string name = Nombre.Text;
                    int sec = int.Parse(Segundos.Text);
                    if ((name == "") || (sec <= 0))
                    {
                        MessageBox.Show("Informacion erronea", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        Exercise exercise = new Exercise();
                        exerciseList.Add(exercise);
                        int count = exerciseList.Count;
                        exerciseList[count - 1].Numero = count;
                        exerciseList[count - 1].Nombre = name;
                        exerciseList[count - 1].Segundos = sec;
                        Exercises.Items.Add(exerciseList[count - 1]);
                        Nombre.Text = "";
                        Segundos.Text = "";
                        //Settear texto de los ejercicios
                        if (count == 0)
                        {
                            Sig_Ejercicio.Text = "";
                            Ejercicio.Text = "";
                        }
                        else if (count == 1)
                        {
                            Sig_Ejercicio.Text = "";
                            Ejercicio.Text = exerciseList[0].Nombre;
                        }
                        else
                        {
                            Sig_Ejercicio.Text = exerciseList[1].Nombre;
                            Ejercicio.Text = exerciseList[0].Nombre;
                        }
                        //Settear tiempo del timer
                        tiempoRutina = 0;
                        for (int i = 0; i < count; i++)
                        {
                            tiempoRutina += exerciseList[i].Segundos;
                        }
                        if (tiempoRutina < 60)
                        {
                            string segundos = tiempoRutina.ToString();
                            textoRutina = segundos;
                        }
                        else if ((tiempoRutina >= 60) && (tiempoRutina < 3600))
                        {
                            string segundos = "";
                            string minutos = "";
                            int modulo = tiempoRutina % 60;
                            int tiempo = (tiempoRutina - modulo) / 60;
                            if (modulo < 10)
                            {
                                segundos += "0";
                            }
                            segundos += modulo.ToString();
                            if (tiempo < 10)
                            {
                                minutos += "0";
                            }
                            minutos += tiempo.ToString();

                            textoRutina = "";
                            textoRutina += minutos;
                            textoRutina += ":";
                            textoRutina += segundos;

                        }
                        else if ((tiempoRutina >= 3600) && (tiempoRutina < 86400))
                        {
                            string segundos = "";
                            string minutos = "";
                            string horas = "";
                            int moduloMin = tiempoRutina % 3600; //minutos con segundos que sobran aparte de las horas
                            int tiempoMin = (tiempoRutina) / 3600; //horas
                            int moduloSec = moduloMin % 60;             //sobrante de segundos
                            int tiempoSec = (moduloMin - moduloSec) / 60; //miuntos

                            if (moduloSec < 10)
                            {
                                segundos += "0";
                            }
                            segundos += moduloSec.ToString();
                            if (tiempoSec < 10)
                            {
                                minutos += "0";
                            }
                            minutos += tiempoSec.ToString();
                            if (tiempoMin < 10)
                            {
                                horas += "0";
                            }
                            horas += tiempoMin.ToString();

                            textoRutina = "";
                            textoRutina += horas;
                            textoRutina += ":";
                            textoRutina += minutos;
                            textoRutina += ":";
                            textoRutina += segundos;
                        }
                        else
                        {
                            MessageBox.Show("Entrenar mas de un día seguido es demasiado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            exerciseList.Clear();
                            Exercises.Items.Clear();
                            Sig_Ejercicio.Text = "";
                            Ejercicio.Text = "";
                            tiempoRutina = 0;
                            textoRutina = "";
                            Countdown.Text = textoRutina;
                        }

                        Countdown.Text = textoRutina;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
                
        }

        private void Rm_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (rutinaActiva == 0) {
                try
                {
                    Exercise selected = Exercises.SelectedItem as Exercise;
                    if (selected != null)
                    {
                        int id = selected.Numero;
                        exerciseList.RemoveAt(id - 1);
                        Exercises.Items.Clear();
                        int count = exerciseList.Count;
                        for (int i = 0; i < count; i++)
                        {
                            exerciseList[i].Numero = i + 1;
                            Exercises.Items.Add(exerciseList[i]);
                        }
                        //Settear texto de los ejercicios
                        if (count == 0)
                        {
                            Sig_Ejercicio.Text = "";
                            Ejercicio.Text = "";
                        }
                        else if (count == 1)
                        {
                            Sig_Ejercicio.Text = "";
                            Ejercicio.Text = exerciseList[0].Nombre;
                        }
                        else
                        {
                            Sig_Ejercicio.Text = exerciseList[1].Nombre;
                            Ejercicio.Text = exerciseList[0].Nombre;
                        }
                        //Settear tiempo del timer
                        tiempoRutina = 0;
                        for (int i = 0; i < count; i++)
                        {
                            tiempoRutina += exerciseList[i].Segundos;
                        }
                        if (tiempoRutina < 60)
                        {
                            string segundos = tiempoRutina.ToString();
                            textoRutina = segundos;
                        }
                        else if ((tiempoRutina >= 60) && (tiempoRutina < 3600))
                        {
                            string segundos = "";
                            string minutos = "";
                            int modulo = tiempoRutina % 60;
                            int tiempo = (tiempoRutina - modulo) / 60;
                            if (modulo < 10)
                            {
                                segundos += "0";
                            }
                            segundos += modulo.ToString();
                            if (tiempo < 10)
                            {
                                minutos += "0";
                            }
                            minutos += tiempo.ToString();

                            textoRutina = "";
                            textoRutina += minutos;
                            textoRutina += ":";
                            textoRutina += segundos;

                        }
                        else if ((tiempoRutina >= 3600) && (tiempoRutina < 86400))
                        {
                            string segundos = "";
                            string minutos = "";
                            string horas = "";
                            int moduloMin = tiempoRutina % 3600; //minutos con segundos que sobran aparte de las horas
                            int tiempoMin = (tiempoRutina) / 3600; //horas
                            int moduloSec = moduloMin % 60;             //sobrante de segundos
                            int tiempoSec = (moduloMin - moduloSec) / 60; //miuntos

                            if (moduloSec < 10)
                            {
                                segundos += "0";
                            }
                            segundos += moduloSec.ToString();
                            if (tiempoSec < 10)
                            {
                                minutos += "0";
                            }
                            minutos += tiempoSec.ToString();
                            if (tiempoMin < 10)
                            {
                                horas += "0";
                            }
                            horas += tiempoMin.ToString();

                            textoRutina = "";
                            textoRutina += horas;
                            textoRutina += ":";
                            textoRutina += minutos;
                            textoRutina += ":";
                            textoRutina += segundos;
                        }
                        else
                        {
                            MessageBox.Show("Entrenar mas de un día seguido es demasiado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                            exerciseList.Clear();
                            Exercises.Items.Clear();
                            Sig_Ejercicio.Text = "";
                            Ejercicio.Text = "";
                            tiempoRutina = 0;
                            textoRutina = "";
                            Countdown.Text = textoRutina;
                        }

                        Countdown.Text = textoRutina;
                    }
                    else
                    {
                        MessageBox.Show("No hay ningun item seleccionado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
                
            
        }

        private void Clear_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (rutinaActiva == 0) {
                exerciseList.Clear();
                Exercises.Items.Clear();
                Sig_Ejercicio.Text = "";
                Ejercicio.Text = "";
                tiempoRutina = 0;
                textoRutina = "";
                Countdown.Text = textoRutina;
                noEjercicio = 0;
            }
            
        }

        private void Timer_Tick(object sender, EventArgs e) {
            //Routine Timer
            if (rutinaActiva == 1) {
                if (ejercicioActivo == 0) {
                    routine.Dequeue();
                    if (routine.Count > 0) {
                        if (routine.Peek().Numero < exerciseList.Count)
                        {
                            Sig_Ejercicio.Text = exerciseList[noEjercicio + 2].Nombre;
                            Ejercicio.Text = routine.Peek().Nombre;
                            tiempoEjercicio = routine.Peek().Segundos;
                            ejercicioActivo = 1;
                            noEjercicio++;
                            try
                            {
                                SoundPlayer player1 = new SoundPlayer("Audio\\Transition-sound.wav");
                                player1.Play();
                            }
                            catch (Exception ex)
                            {
                                string error = "";
                                error += ex;
                                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            Sig_Ejercicio.Text = "";
                            Ejercicio.Text = routine.Peek().Nombre;
                            tiempoEjercicio = routine.Peek().Segundos;
                            ejercicioActivo = 1;
                            try
                            {
                                SoundPlayer player1 = new SoundPlayer("Audio\\Transition-sound.wav");
                                player1.Play();
                            }
                            catch (Exception ex)
                            {
                                string error = "";
                                error += ex;
                                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    
                }
                
                //Settear tiempo del timer
                if (tiempoRutina < 60)
                {
                    string segundos = tiempoRutina.ToString();
                    textoRutina = segundos;
                }
                else if ((tiempoRutina >= 60) && (tiempoRutina < 3600))
                {
                    string segundos = "";
                    string minutos = "";
                    int modulo = tiempoRutina % 60;
                    int tiempo = (tiempoRutina - modulo) / 60;
                    if (modulo < 10)
                    {
                        segundos += "0";
                    }
                    segundos += modulo.ToString();
                    if (tiempo < 10)
                    {
                        minutos += "0";
                    }
                    minutos += tiempo.ToString();

                    textoRutina = "";
                    textoRutina += minutos;
                    textoRutina += ":";
                    textoRutina += segundos;

                }
                else if ((tiempoRutina >= 3600) && (tiempoRutina < 86400))
                {
                    string segundos = "";
                    string minutos = "";
                    string horas = "";
                    int moduloMin = tiempoRutina % 3600; //minutos con segundos que sobran aparte de las horas
                    int tiempoMin = (tiempoRutina) / 3600; //horas
                    int moduloSec = moduloMin % 60;             //sobrante de segundos
                    int tiempoSec = (moduloMin - moduloSec) / 60; //miuntos

                    if (moduloSec < 10)
                    {
                        segundos += "0";
                    }
                    segundos += moduloSec.ToString();
                    if (tiempoSec < 10)
                    {
                        minutos += "0";
                    }
                    minutos += tiempoSec.ToString();
                    if (tiempoMin < 10)
                    {
                        horas += "0";
                    }
                    horas += tiempoMin.ToString();

                    textoRutina = "";
                    textoRutina += horas;
                    textoRutina += ":";
                    textoRutina += minutos;
                    textoRutina += ":";
                    textoRutina += segundos;
                }
                tiempoEjercicio--;
                tiempoRutina--;

                if (tiempoEjercicio <= 0)
                {
                    ejercicioActivo = 0;
                    
                }

                if (tiempoRutina < 0) {
                    rutinaActiva = 0;
                    try
                    {
                        SoundPlayer player2 = new SoundPlayer("Audio\\Final-beep.wav");
                        player2.Play();
                    }
                    catch (Exception ex)
                    {
                        string error = "";
                        error += ex;
                        MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                Countdown.Text = textoRutina;
                
                
            }

            //ComboTimer -----------------------------------------------------------------------------------

            if (comboActivo == 1) {
                if (tiempoCombi == 0)
                {
                    comboActivo = 0;
                    try
                    {
                        SoundPlayer player2 = new SoundPlayer("Audio\\Final-beep.wav");
                        player2.PlaySync();
                    }
                    catch (Exception ex)
                    {
                        string error = "";
                        error += ex;
                        MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }


                tiempoCombi--;
                if (tiempoCombi < 60)
                {
                    string segundos = tiempoCombi.ToString();
                    textoCombi = segundos;
                }
                else if ((tiempoCombi >= 60) && (tiempoCombi < 3600))
                {
                    string segundos = "";
                    string minutos = "";
                    int modulo = tiempoCombi % 60;
                    int tiempo = (tiempoCombi - modulo) / 60;
                    if (modulo < 10)
                    {
                        segundos += "0";
                    }
                    segundos += modulo.ToString();
                    if (tiempo < 10)
                    {
                        minutos += "0";
                    }
                    minutos += tiempo.ToString();

                    textoCombi = "";
                    textoCombi += minutos;
                    textoCombi += ":";
                    textoCombi += segundos;

                }
                else if ((tiempoCombi >= 3600) && (tiempoCombi < 86400))
                {
                    string segundos = "";
                    string minutos = "";
                    string horas = "";
                    int moduloMin = tiempoCombi % 3600; //minutos con segundos que sobran aparte de las horas
                    int tiempoMin = (tiempoCombi) / 3600; //horas
                    int moduloSec = moduloMin % 60;             //sobrante de segundos
                    int tiempoSec = (moduloMin - moduloSec) / 60; //miuntos

                    if (moduloSec < 10)
                    {
                        segundos += "0";
                    }
                    segundos += moduloSec.ToString();
                    if (tiempoSec < 10)
                    {
                        minutos += "0";
                    }
                    minutos += tiempoSec.ToString();
                    if (tiempoMin < 10)
                    {
                        horas += "0";
                    }
                    horas += tiempoMin.ToString();

                    textoCombi = "";
                    textoCombi += horas;
                    textoCombi += ":";
                    textoCombi += minutos;
                    textoCombi += ":";
                    textoCombi += segundos;
                }
                else
                {
                    MessageBox.Show("Entrenar mas de un día seguido es demasiado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    tiempoCombi = 0;
                    textoCombi = "";
                    Combi_Countdown.Text = textoCombi;
                }

                if (tiempoCombi < 0) {
                    textoCombi = "0";
                }

                Combi_Countdown.Text = textoCombi;

                //Dictating the combos
                if (betweemCombo <= 0)
                {
                    string value1 = Min_Value.SelectedItem.ToString();
                    string value2 = Max_Value.SelectedItem.ToString();
                    int minVal = int.Parse(value1);
                    int maxVal = int.Parse(value2);
                    Random range = new Random();
                    int numberCombo = range.Next(minVal, maxVal);

                    int max = punchesList.Count;
                    

                    Task.Run(() =>
                    {
                        for (int i = 0; i<numberCombo; i++) {
                            Random randomNum = new Random();
                            int random = randomNum.Next(0, max);
                            comboList[random].PlaySync();
                        }
                    });

                    Random number = new Random();
                    betweemCombo = number.Next(4, 6);
                }
                else {
                    betweemCombo--;
                }
                


               
            }
        }

        private void Comenzar_Click(object sender, RoutedEventArgs e)
        {
            if (rutinaActiva == 0) {
                int count = exerciseList.Count;

                if (count <= 0)
                {
                    MessageBox.Show("No hay ejercicios", "Error", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else
                {
                    routine.Clear();

                    for (int i = 0; i < count; i++)
                    {
                        routine.Enqueue(exerciseList[i]);
                    }
                    rutinaActiva = 1;
                    ejercicioActivo = 1;
                    tiempoEjercicio = routine.Peek().Segundos;
                }
                try
                {
                    SoundPlayer player1 = new SoundPlayer("Audio\\Transition-sound.wav");
                    player1.PlaySync();
                    player1.PlaySync();
                    player1.PlaySync();
                    SoundPlayer player2 = new SoundPlayer("Audio\\Final-beep.wav");
                    player2.PlaySync();
                }
                catch (Exception ex)
                {
                    string error = "";
                    error += ex;
                    MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }   
            
           

        }

        private void Pausar_Click(object sender, RoutedEventArgs e)
        {
            rutinaActiva = 0;
        }

        private void Reiniciar_Click(object sender, RoutedEventArgs e)
        {
            if (rutinaActiva == 0) {
                //reiniciar el queue
                routine.Clear();
                int count = exerciseList.Count;
                for (int i = 0; i < count; i++)
                {
                    routine.Enqueue(exerciseList[i]);
                }

                noEjercicio = 0;

                //Settear texto de los ejercicios
                if (count == 0)
                {
                    Sig_Ejercicio.Text = "";
                    Ejercicio.Text = "";
                }
                else if (count == 1)
                {
                    Sig_Ejercicio.Text = "";
                    Ejercicio.Text = exerciseList[0].Nombre;
                }
                else
                {
                    Sig_Ejercicio.Text = exerciseList[1].Nombre;
                    Ejercicio.Text = exerciseList[0].Nombre;
                }
                //Settear tiempo del timer
                tiempoRutina = 0;
                for (int i = 0; i < count; i++)
                {
                    tiempoRutina += exerciseList[i].Segundos;
                }
                if (tiempoRutina < 60)
                {
                    string segundos = tiempoRutina.ToString();
                    textoRutina = segundos;
                }
                else if ((tiempoRutina >= 60) && (tiempoRutina < 3600))
                {
                    string segundos = "";
                    string minutos = "";
                    int modulo = tiempoRutina % 60;
                    int tiempo = (tiempoRutina - modulo) / 60;
                    if (modulo < 10)
                    {
                        segundos += "0";
                    }
                    segundos += modulo.ToString();
                    if (tiempo < 10)
                    {
                        minutos += "0";
                    }
                    minutos += tiempo.ToString();

                    textoRutina = "";
                    textoRutina += minutos;
                    textoRutina += ":";
                    textoRutina += segundos;

                }
                else if ((tiempoRutina >= 3600) && (tiempoRutina < 86400))
                {
                    string segundos = "";
                    string minutos = "";
                    string horas = "";
                    int moduloMin = tiempoRutina % 3600; //minutos con segundos que sobran aparte de las horas
                    int tiempoMin = (tiempoRutina) / 3600; //horas
                    int moduloSec = moduloMin % 60;             //sobrante de segundos
                    int tiempoSec = (moduloMin - moduloSec) / 60; //miuntos

                    if (moduloSec < 10)
                    {
                        segundos += "0";
                    }
                    segundos += moduloSec.ToString();
                    if (tiempoSec < 10)
                    {
                        minutos += "0";
                    }
                    minutos += tiempoSec.ToString();
                    if (tiempoMin < 10)
                    {
                        horas += "0";
                    }
                    horas += tiempoMin.ToString();

                    textoRutina = "";
                    textoRutina += horas;
                    textoRutina += ":";
                    textoRutina += minutos;
                    textoRutina += ":";
                    textoRutina += segundos;
                }
                else
                {
                    MessageBox.Show("Entrenar mas de un día seguido es demasiado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    exerciseList.Clear();
                    Exercises.Items.Clear();
                    Sig_Ejercicio.Text = "";
                    Ejercicio.Text = "";
                    tiempoRutina = 0;
                    textoRutina = "";
                    Countdown.Text = textoRutina;
                }

                Countdown.Text = textoRutina;

            }

        }

        private void Combi_Comenzar_Click(object sender, RoutedEventArgs e)
        {
            //Settear tiempo del timer de combos
            try {
                punchesList.Clear();
                comboList.Clear();
                string secs = segundosCombi.Text;
                int checkedItems = 0;
                
                if (secs == "")
                {
                    MessageBox.Show("Verifica la informacion e intenta de vuelta", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else {
                    string value1 = Min_Value.SelectedItem.ToString();
                    string value2 = Max_Value.SelectedItem.ToString();
                    int minVal = int.Parse(value1);
                    int maxVal = int.Parse(value2);
                    if (minVal > maxVal)
                    {
                        MessageBox.Show("El valor minimo no puede ser mayor al maximo", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else {
                        if (Jab.IsChecked == true) {
                            punchesList.Add(1);
                            SoundPlayer one = new SoundPlayer("Audio\\1.wav");
                            comboList.Add(one);
                            checkedItems++;
                        }

                        if (Cross.IsChecked == true)
                        {
                            punchesList.Add(2);
                            SoundPlayer two = new SoundPlayer("Audio\\2.wav");
                            comboList.Add(two);
                            checkedItems++;
                        }

                        if (R_Hook.IsChecked == true)
                        {
                            punchesList.Add(3);
                            SoundPlayer three = new SoundPlayer("Audio\\3.wav");
                            comboList.Add(three);
                            checkedItems++;
                        }

                        if (L_Hook.IsChecked == true)
                        {
                            punchesList.Add(4);
                            SoundPlayer four = new SoundPlayer("Audio\\4.wav");
                            comboList.Add(four);
                            checkedItems++;
                        }

                        if (R_Upper.IsChecked == true)
                        {
                            punchesList.Add(5);
                            SoundPlayer five = new SoundPlayer("Audio\\5.wav");
                            comboList.Add(five);
                            checkedItems++;
                        }

                        if (S_Upper.IsChecked == true)
                        {
                            punchesList.Add(6);
                            SoundPlayer six = new SoundPlayer("Audio\\6.wav");
                            comboList.Add(six);
                            checkedItems++;
                        }

                        if (Liver_Hook1.IsChecked == true)
                        {
                            punchesList.Add(7);
                            SoundPlayer seven = new SoundPlayer("Audio\\7.wav");
                            comboList.Add(seven);
                            checkedItems++;
                        }

                        if (Liver_Hook2.IsChecked == true)
                        {
                            punchesList.Add(8);
                            SoundPlayer eight = new SoundPlayer("Audio\\8.wav");
                            comboList.Add(eight);
                            checkedItems++;
                        }

                        if (Over.IsChecked == true)
                        {
                            punchesList.Add(9);
                            SoundPlayer nine = new SoundPlayer("Audio\\9.wav");
                            comboList.Add(nine);
                            checkedItems++;
                        }

                        if (checkedItems <= 1)
                        {
                            MessageBox.Show("No puedes tener combinaciones con un golpe o sin golpes", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else {
                            tiempoCombi = int.Parse(secs);

                            if (tiempoCombi < 60)
                            {
                                string segundos = tiempoCombi.ToString();
                                textoCombi = segundos;
                            }
                            else if ((tiempoCombi >= 60) && (tiempoCombi < 3600))
                            {
                                string segundos = "";
                                string minutos = "";
                                int modulo = tiempoCombi % 60;
                                int tiempo = (tiempoCombi - modulo) / 60;
                                if (modulo < 10)
                                {
                                    segundos += "0";
                                }
                                segundos += modulo.ToString();
                                if (tiempo < 10)
                                {
                                    minutos += "0";
                                }
                                minutos += tiempo.ToString();

                                textoCombi = "";
                                textoCombi += minutos;
                                textoCombi += ":";
                                textoCombi += segundos;

                            }
                            else if ((tiempoCombi >= 3600) && (tiempoCombi < 86400))
                            {
                                string segundos = "";
                                string minutos = "";
                                string horas = "";
                                int moduloMin = tiempoCombi % 3600; //minutos con segundos que sobran aparte de las horas
                                int tiempoMin = (tiempoCombi) / 3600; //horas
                                int moduloSec = moduloMin % 60;             //sobrante de segundos
                                int tiempoSec = (moduloMin - moduloSec) / 60; //miuntos

                                if (moduloSec < 10)
                                {
                                    segundos += "0";
                                }
                                segundos += moduloSec.ToString();
                                if (tiempoSec < 10)
                                {
                                    minutos += "0";
                                }
                                minutos += tiempoSec.ToString();
                                if (tiempoMin < 10)
                                {
                                    horas += "0";
                                }
                                horas += tiempoMin.ToString();

                                textoCombi = "";
                                textoCombi += horas;
                                textoCombi += ":";
                                textoCombi += minutos;
                                textoCombi += ":";
                                textoCombi += segundos;
                            }
                            else
                            {
                                MessageBox.Show("Entrenar mas de un día seguido es demasiado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                                tiempoCombi = 0;
                                textoCombi = "";
                                Combi_Countdown.Text = textoCombi;
                            }

                            Combi_Countdown.Text = textoCombi;
                            comboActivo = 1;
                            try
                            {
                                SoundPlayer player1 = new SoundPlayer("Audio\\Transition-sound.wav");
                                player1.PlaySync();
                                player1.PlaySync();
                                player1.PlaySync();
                                SoundPlayer player2 = new SoundPlayer("Audio\\Final-beep.wav");
                                player2.PlaySync();
                            }
                            catch (Exception ex)
                            {
                                string error = "";
                                error += ex;
                                MessageBox.Show(error, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        

                    }
                }
                    
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void Combi_Pusa_Click(object sender, RoutedEventArgs e)
        {
            comboActivo = 0;
        }


        private void Reanudar_Click(object sender, RoutedEventArgs e)
        {
            string checks = Combi_Countdown.Text;
            if (checks == "")
            {
                MessageBox.Show("El entrenamiento no ha comenzado aun", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else {
                comboActivo = 1;
            }
        }
    }

    public class Exercise {
        public int Numero { get; set; }
        public string Nombre { get; set; }
        public int Segundos { get; set; }

    }
}
