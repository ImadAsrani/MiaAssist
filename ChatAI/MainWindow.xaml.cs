namespace ChatAI
{
    using ChatAI.VistaModelo;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Effects;

    public partial class MainWindow : Window
    {
        internal bool activado = false;
        MediaPlayer mediaPlayer = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ChatViewModel(milista, botonenviar, botonparar, pregunta, scrol, microfono, nueva, animacion, altavoz);
            pregunta.Focus();
        }

        /// <summary>
        /// Permite mover la ventana al mantener pulsado encima.
        /// </summary>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        /// <summary>
        /// Aplica un efecto de sombra y animación de escala al botón "cerrar" cuando el ratón entra.
        /// </summary>
        private void cerrar_MouseEnter(object sender, MouseEventArgs e)
        {
            cerrar.Effect = new DropShadowEffect
            {
                Color = Color.FromRgb(63, 210, 0),
                Direction = -10,
                BlurRadius = 10,
                ShadowDepth = 0,
                Opacity = 0.9
            };

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            cerrar.RenderTransform = new ScaleTransform();
            cerrar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            cerrar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Elimina el efecto de sombra y revierte la animación de escala al botón "cerrar" cuando el ratón sale.
        /// </summary>
        private void cerrar_MouseLeave(object sender, MouseEventArgs e)
        {
            cerrar.Effect = null;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.1,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            cerrar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            cerrar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Aplica un efecto de sombra y animación de escala al botón "maximizar" cuando el ratón entra.
        /// </summary>
        private void maximizar_MouseEnter(object sender, MouseEventArgs e)
        {
            maximizar.Effect = new DropShadowEffect
            {
                Color = Color.FromRgb(63, 210, 0),
                Direction = -10,
                BlurRadius = 10,
                ShadowDepth = 0,
                Opacity = 0.9
            };

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            maximizar.RenderTransform = new ScaleTransform();
            maximizar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            maximizar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Elimina el efecto de sombra y revierte la animación de escala al botón "maximizar" cuando el ratón sale.
        /// </summary>
        private void maximizar_MouseLeave(object sender, MouseEventArgs e)
        {
            maximizar.Effect = null;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.1,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            maximizar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            maximizar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Aplica un efecto de sombra y animación de escala al botón "minimizar" cuando el ratón entra.
        /// </summary>
        private void minimizar_MouseEnter(object sender, MouseEventArgs e)
        {
            minimizar.Effect = new DropShadowEffect
            {
                Color = Color.FromRgb(63, 210, 0),
                Direction = -10,
                BlurRadius = 10,
                ShadowDepth = 0,
                Opacity = 0.9
            };

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            minimizar.RenderTransform = new ScaleTransform();
            minimizar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            minimizar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Elimina el efecto de sombra y revierte la animación de escala al botón "minimizar" cuando el ratón sale.
        /// </summary>
        private void minimizar_MouseLeave(object sender, MouseEventArgs e)
        {
            minimizar.Effect = null;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.1,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            minimizar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            minimizar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Aplica un efecto de sombra y animación de escala al botón "botonparar" cuando el ratón entra.
        /// </summary>
        private void botonparar_MouseEnter(object sender, MouseEventArgs e)
        {
            botonparar.Effect = new DropShadowEffect
            {
                Color = Colors.Red,
                Direction = -10,
                BlurRadius = 10,
                ShadowDepth = 0,
                Opacity = 0.9
            };

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            botonparar.RenderTransform = new ScaleTransform();
            botonparar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            botonparar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Elimina el efecto de sombra y revierte la animación de escala al botón "botonparar" cuando el ratón sale.
        /// </summary>
        private void botonparar_MouseLeave(object sender, MouseEventArgs e)
        {
            botonparar.Effect = null;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.1,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            botonparar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            botonparar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Cierra la ventana cuando se hace clic en el botón "cerrar".
        /// </summary>
        private void cerrar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maximiza o restaura la ventana cuando se hace clic en el botón "maximizar".
        /// </summary>
        private void maximizar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }

        /// <summary>
        /// Minimiza la ventana con una animación de escala vertical.
        /// </summary>
        private async void minimizar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // Cambiar el estado primero

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.3)
            };

            ScaleTransform scaleTransform = new ScaleTransform();
            this.RenderTransform = scaleTransform;
            this.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);

            await Task.Delay(300); // Ajustar el retraso si es necesario

            this.RenderTransform = null; // Restablecer después de la animación
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.RenderTransform = null;
            }
            if (this.WindowState == WindowState.Maximized)
            {
                this.RenderTransform = null;
            }
        }


        /// <summary>
        /// Muestra u oculta el placeholder en el TextBox.
        /// </summary>
        private void pregunta_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(pregunta.Text))
            {
                lblplaceholder.Visibility = Visibility.Visible;
            }
            else
            {
                lblplaceholder.Visibility = Visibility.Collapsed;

            }

        }
        
        /// <summary>
        /// Aplica un efecto de sombra y animación de escala al botón "microfono" cuando el ratón entra.
        /// Cambia el color del efecto dependiendo del estado de 'activado'.
        /// </summary>
        private void microfono_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!activado)
            {
                microfono.Effect = new DropShadowEffect
                {
                    Color = Color.FromRgb(63, 210, 0),
                    Direction = -10,
                    BlurRadius = 10,
                    ShadowDepth = 0,
                    Opacity = 0.9
                };
            }
            else
            {
                microfono.Effect = new DropShadowEffect
                {
                    Color = Colors.Red,
                    Direction = -10,
                    BlurRadius = 10,
                    ShadowDepth = 0,
                    Opacity = 0.9
                };
            }

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            microfono.RenderTransform = new ScaleTransform();
            microfono.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            microfono.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Elimina el efecto de sombra y revierte la animación de escala al botón "microfono" cuando el ratón sale.
        /// </summary>
        private void microfono_MouseLeave(object sender, MouseEventArgs e)
        {
            microfono.Effect = null;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.1,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            microfono.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            microfono.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Aplica un efecto de sombra y animación de escala al botón "altavoz" cuando el ratón entra.
        /// </summary>
        private void altavoz_MouseEnter(object sender, MouseEventArgs e)
        {
            altavoz.Effect = new DropShadowEffect
            {
                Color = Color.FromRgb(63, 210, 0),
                Direction = -10,
                BlurRadius = 10,
                ShadowDepth = 0,
                Opacity = 0.9
            };

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            altavoz.RenderTransform = new ScaleTransform();
            altavoz.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            altavoz.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Elimina el efecto de sombra y revierte la animación de escala al botón "altavoz" cuando el ratón sale.
        /// </summary>
        private void altavoz_MouseLeave(object sender, MouseEventArgs e)
        {
            altavoz.Effect = null;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.1,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            altavoz.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            altavoz.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Aplica una animación de "latido" al borde del TextBox "pregunta" y al placeholder cuando recibe el foco.
        /// </summary>
        private async void pregunta_GotFocus(object sender, RoutedEventArgs e)
        {
            ScaleTransform scaleTransform = new ScaleTransform();
            bordeescribir.RenderTransform = scaleTransform;
            bordeescribir.RenderTransformOrigin = new Point(0.5, 0.5);

            pregunta.RenderTransform = scaleTransform;
            pregunta.RenderTransformOrigin = new Point(0.5, 0.5);

            lblplaceholder.RenderTransform = scaleTransform;
            lblplaceholder.RenderTransformOrigin = new Point(0.5, 0.5);

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.02,
                Duration = TimeSpan.FromSeconds(0.15)
            };
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);

            await Task.Delay(200);

            animation = new DoubleAnimation
            {
                From = 1.02,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.15)
            };
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);

            await Task.Delay(200);
        }

        /// <summary>
        /// Cambia el estado de 'activado', actualiza el contenido y color del placeholder, y realiza la acción correspondiente.
        /// </summary>
        private async void microfono_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!activado)
            {
                activado = true;
                lblplaceholder.Content = "Escuchando...";
                lblplaceholder.Foreground = Brushes.Red;
            }
            else
            {
                activado = false;
                lblplaceholder.Content = "Preguntame algo...";
                lblplaceholder.Foreground = Brushes.Gray;
            }
        }
        /// <summary>
        /// Aplica un efecto de sombra y animación de escala al botón "nueva" cuando el ratón entra.
        /// </summary>
        private void nueva_MouseEnter(object sender, MouseEventArgs e)
        {
            nueva.Effect = new DropShadowEffect
            {
                Color = Color.FromRgb(63, 210, 0),
                Direction = -10,
                BlurRadius = 10,
                ShadowDepth = 0,
                Opacity = 0.9
            };

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.0,
                To = 1.1,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            nueva.RenderTransform = new ScaleTransform();
            nueva.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            nueva.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Elimina el efecto de sombra y revierte la animación de escala al botón "nueva" cuando el ratón sale.
        /// </summary>
        private void nueva_MouseLeave(object sender, MouseEventArgs e)
        {
            nueva.Effect = null;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.1,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            nueva.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            nueva.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Cambia la visibilidad del efecto visual 'efecto' y llama a las funciones de animación correspondientes
        /// dependiendo del estado del botón 'altavoz'.
        /// </summary>
        private async void altavoz_Click(object sender, RoutedEventArgs e)
        {
            if (altavoz.IsChecked == true)
            {
                efecto.Visibility = Visibility.Visible;
                await ReproducirSonidoAsync(@"C:/Users/image/Desktop/Practica/ChatAI/ChatAI/Sound/on.mp3");
                AnimarAparicion();
            }
            else
            {
                await ReproducirSonidoAsync(@"C:/Users/image/Desktop/Practica/ChatAI/ChatAI/Sound/off.mp3");
                AnimarDesaparicion();
                efecto.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Anima la desaparición del control 'animacion' mediante una animación de escala.
        /// Oculta el control al finalizar la animación.
        /// </summary>
        private async void AnimarDesaparicion()
        {


            ScaleTransform scaleTransform = new ScaleTransform();
            animacion.RenderTransform = scaleTransform;
            animacion.RenderTransformOrigin = new Point(0.5, 0.5);

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(0.3))
            };

            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);

            animation.Completed += (s, e) =>
            {
                animacion.Visibility = Visibility.Hidden;
            };
        }

        /// <summary>
        /// Anima la aparición del control 'animacion' mediante una animación de escala.
        /// Asegura que el control sea visible antes de iniciar la animación.
        /// </summary>
        private async void AnimarAparicion()
        {
            await Task.Delay(400);

            animacion.Visibility = Visibility.Visible;

            ScaleTransform scaleTransform = new ScaleTransform();
            animacion.RenderTransform = scaleTransform;
            animacion.RenderTransformOrigin = new Point(0.5, 0.5);

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0.7))
            };

            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
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