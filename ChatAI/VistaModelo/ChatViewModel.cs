using ChatAI.Commands;
using ChatAI.Modelo;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;

using System.Text.Json;

using System.Windows.Input;

using System.Speech.Synthesis;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Windows.Media.Animation;

using Vosk;
using NAudio.Wave;

using System.Collections.Specialized;
using System.Security.Policy;
using System.Windows.Controls.Primitives;
using Microsoft.VisualBasic.ApplicationServices;

namespace ChatAI.VistaModelo
{
    public class ChatViewModel : ViewModelBase
    {
        private readonly HttpClient _httpClient = new();
        private string _texto;
        private bool _puedeEnviar;
        private bool Cosa = true;
        private ListBox lista;
        private Button botonenviar;
        private Image botonparar, microfono, nueva, animacion;
        private TextBox pregunta;

        private SpeechSynthesizer synthesizer;

        private ScrollViewer scrol;
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private DispatcherTimer temporizador;
        private ToggleButton altavoz;
        private bool activo;
        private bool _grabando;


        private VoskRecognizer speechRecognizer;
        private WaveInEvent waveIn;


        private string textoReconocido2 = "";
        private bool escuchando = false;

        public ObservableCollection<Mensaje> Mensajes { get; } = new();

        new List<object> messages = new List<object>
        {
            new { content = Settings.Default.Instruccion, role = "system" }
        };

        private bool _isSpeechEnabled;
        public bool IsSpeechEnabled
        {
            get => _isSpeechEnabled;
            set
            {
                if (_isSpeechEnabled != value)
                {
                    _isSpeechEnabled = value;
                    OnPropertyChanged();


                }
            }
        }

        public string Texto
        {
            get => _texto;
            set
            {
                if (_texto != value)
                {

                    _texto = value;
                    OnPropertyChanged();
                    PuedeEnviar = !string.IsNullOrWhiteSpace(_texto);
                    ((RelayCommand)EnviarMensajeCommand).RaiseCanExecuteChanged();

                }
            }
        }

        public bool PuedeEnviar
        {
            get => _puedeEnviar;
            set
            {
                if (_puedeEnviar != value)
                {
                    _puedeEnviar = value;
                    OnPropertyChanged();
                }
            }
        }
        
        public ICommand EnviarMensajeCommand { get; }
        public bool Cosa1 { get => Cosa; set => Cosa = value; }

        public ChatViewModel(ListBox milista, Button botonenviar, Image botonparar, TextBox pregunta, ScrollViewer scrol, Image microfono, Image nueva, Image animacion, ToggleButton altavoz)
        {
            EnviarMensajeCommand = new RelayCommand(async () => await EnviarMensaje(), () => PuedeEnviar);

            synthesizer = new SpeechSynthesizer();

            synthesizer.SetOutputToDefaultAudioDevice();

            synthesizer.Rate = 2;

            lista = milista;

            this.botonenviar = botonenviar;

            Mensajes.CollectionChanged += Mensajes_CollectionChanged;

            this.botonparar = botonparar;

            botonparar.MouseDown += CancelarReproduccion;

            this.pregunta = pregunta;

            this.scrol = scrol;

            pregunta.KeyDown += Pregunta_KeyDown;

            this.microfono = microfono;

            microfono.MouseDown += ToggleGrabacion;

            this.nueva= nueva;

            nueva.MouseDown += Nueva_MouseDown;

            this.animacion = animacion;

            var model = new Model("vosk-model-small-es-0.42");

            speechRecognizer = new VoskRecognizer(model, 16000.0f);

            this.altavoz = altavoz;


        }

        /// <summary>
        /// Alterna el estado de grabación del micrófono.
        /// </summary>
        private async void ToggleGrabacion(object sender, MouseButtonEventArgs e)
        {
            if (_grabando)
            {
             
                waveIn.StopRecording();
                waveIn.Dispose();
                _grabando = false;
                microfono.Height = 25;
                microfono.Source = new BitmapImage(new Uri("img/micro.png", UriKind.Relative));
               
            }
            else
            {
                
                    waveIn = new WaveInEvent { WaveFormat = new WaveFormat(16000, 1) };
                    waveIn.DataAvailable += WaveIn_DataAvailable;
                    waveIn.StartRecording();
                    _grabando = true;
                    microfono.Height = 15;
                    microfono.Source = new BitmapImage(new Uri("img/stop.png", UriKind.Relative));
                

            }
        }

        /// <summary>
        /// Procesa los datos de audio disponibles para el reconocimiento de voz.
        /// </summary>
        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {

            try
            {
                if (speechRecognizer.AcceptWaveform(e.Buffer, e.BytesRecorded))
                {
                    var result = JsonSerializer.Deserialize<Dictionary<string, string>>(speechRecognizer.Result());
                    if (result != null && result.ContainsKey("text"))
                    {
                        Application.Current.Dispatcher.Invoke(() => Texto += result["text"] + " ");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

           
        }

        /// <summary>
        /// Maneja el evento de clic en el botón "Nueva Conversación", mostrando una ventana emergente y reiniciando la conversación si se acepta.
        /// </summary>
        private void Nueva_MouseDown(object sender, MouseButtonEventArgs e)
        {
            PopOut popOutWindow = new PopOut();
            popOutWindow.ShowDialog();

            if (popOutWindow.DialogResult == true && popOutWindow.BotonAceptarPulsado)
            {
                Mensajes.Clear();
                messages.Clear();
                Texto = string.Empty;
                textoReconocido2 = "";
                PuedeEnviar = true;
                ((RelayCommand)EnviarMensajeCommand).RaiseCanExecuteChanged();
                pregunta.IsEnabled = true;
            }
            else if (popOutWindow.DialogResult == false && popOutWindow.BotonCancelarPulsado)
            {
                popOutWindow.Close();
            }
        }

    

        /// <summary>
        /// Maneja el evento de tecla presionada en el cuadro de pregunta, enviando el mensaje si se presiona Enter.
        /// </summary>
        private void Pregunta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnviarMensaje();
                e.Handled = true;
            }
        }
    

        /// <summary>
        /// Envía el mensaje del usuario al servidor, procesa la respuesta y actualiza la interfaz de usuario.
        /// </summary>
        private async Task EnviarMensaje()
        {
           

            await ReproducirSonidoAsync(@"C:\Users\image\Desktop\Practica\ChatAI\ChatAI\Sound\envio.mp3");

            var mensajeUsuario = new Mensaje { Contenido = Texto, EsUsuario = true };
            Mensajes.Add(mensajeUsuario);
            Texto = string.Empty;
            PuedeEnviar = false;
            ((RelayCommand)EnviarMensajeCommand).RaiseCanExecuteChanged();

            messages.Add(new { content = mensajeUsuario.Contenido, role = "user" });

            var requestBody = new
            {
                messages = messages.ToArray(),
                model = Settings.Default.Type,
                max_tokens = 2048
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                var response = await _httpClient.PostAsync(Settings.Default.URLServer, content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<ChatResponse>(responseBody);

                var mensajeBot = new Mensaje { Contenido = resultado.choices[0].message.content, EsUsuario = false };
                Mensajes.Add(mensajeBot);
                messages.Add(new { content = mensajeBot.Contenido, role = "assistant" });

                if (IsSpeechEnabled)
                {
                    IniciarAnimacionMicrofono();
                    altavoz.IsEnabled = false;
                    pregunta.IsEnabled = false;
                    microfono.IsEnabled = false;
                    nueva.IsEnabled = false;
                    synthesizer.SpeakStarted += Synthesizer_SpeakStarted;
                    synthesizer.SpeakCompleted += Synthesizer_SpeakCompleted;
                    synthesizer.SpeakAsync(mensajeBot.Contenido);
                }

            }
            catch (Exception ex)
            {
                Mensajes.Add(new Mensaje { Contenido = "Error en la respuesta: " + ex.Message, EsUsuario = false });
            }
        }

        /// <summary>
        /// Cancela la reproducción de voz actual.
        /// </summary>
        private void CancelarReproduccion(object sender, MouseButtonEventArgs e)
        {
            synthesizer.SpeakAsyncCancelAll();
        }

        /// <summary>
        /// Maneja el evento de finalización de la reproducción de voz, actualizando la interfaz de usuario.
        /// </summary>
        private void Synthesizer_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            DetenerAnimacionMicrofono();
            botonenviar.Visibility = Visibility.Visible;
            botonparar.Visibility = Visibility.Hidden;
            pregunta.IsEnabled = true;
            pregunta.Focus();
            altavoz.IsEnabled = true;
            nueva.IsEnabled = true;
            microfono.IsEnabled = true;

        }

        /// <summary>
        /// Maneja el evento de inicio de la reproducción de voz, actualizando la interfaz de usuario.
        /// </summary>
        private void Synthesizer_SpeakStarted(object sender, SpeakStartedEventArgs e)
        {
            botonenviar.Visibility = Visibility.Hidden;
            botonparar.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Detiene la animación del micrófono y restablece su transformación.
        /// </summary>
        private void DetenerAnimacionMicrofono()
        {
            if (temporizador != null)
            {
                temporizador.Stop();
                temporizador.Tick -= Temporizador_Tick;
                temporizador = null;
            }
            animacion.RenderTransform = null;
        }

        /// <summary>
        /// Inicia la animación del micrófono, haciéndolo visible y aplicando una transformación de escala.
        /// </summary>
        private void IniciarAnimacionMicrofono()
        {
            animacion.Visibility = Visibility.Visible;
            ScaleTransform scaleTransform = new ScaleTransform();
            animacion.RenderTransform = scaleTransform;
            animacion.RenderTransformOrigin = new Point(0.5, 0.5);
            temporizador = new DispatcherTimer();
            temporizador.Interval = TimeSpan.FromMilliseconds(100);
            temporizador.Tick += Temporizador_Tick;
            temporizador.Start();
        }

        /// <summary>
        /// Genera y aplica una animación de escala aleatoria al micrófono en cada intervalo del temporizador.
        /// </summary>
        private void Temporizador_Tick(object sender, EventArgs e)
        {
            double escala = 1 + new Random().NextDouble() * 0.5;
            DoubleAnimation animation = new DoubleAnimation
            {
                To = escala,
                Duration = new Duration(TimeSpan.FromMilliseconds(100))
            };
            ScaleTransform scaleTransform = (ScaleTransform)animacion.RenderTransform;
            animacion.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            animacion.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Maneja el evento de cambio de colección de mensajes, desplazando el ScrollViewer hacia abajo.
        /// </summary>
        private void Mensajes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var listBox = lista;
            listBox.Dispatcher.InvokeAsync(() =>
            {
                var scrollViewer = FindVisualChild<ScrollViewer>(listBox);
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollToVerticalOffset(scrollViewer.ExtentHeight);
                }
            });
            scrol.ScrollToBottom();
        }

        /// <summary>
        /// Encuentra un elemento visual hijo de un tipo específico dentro de un objeto DependencyObject.
        /// </summary>
        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T element)
                {
                    return element;
                }
                else
                {
                    T grandChild = FindVisualChild<T>(child);
                    if (grandChild != null)
                    {
                        return grandChild;
                    }
                }
            }
            return null;
        }






        /// <summary>
        /// Reproduce un archivo de sonido de forma asíncrona.
        /// </summary>
        private async Task ReproducirSonidoAsync(string rutaArchivo)
        {
            try
            {
                mediaPlayer.Open(new Uri(rutaArchivo));
                mediaPlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reproducir el sonido: {ex.Message}");
            }
        }
    }
}