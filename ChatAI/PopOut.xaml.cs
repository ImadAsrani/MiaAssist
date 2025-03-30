using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media;

namespace ChatAI
{

    public partial class PopOut : Window
    {
        public PopOut()
        {
            InitializeComponent();
        }

        public bool BotonAceptarPulsado { get; set; } = false;
        public bool BotonCancelarPulsado { get; set; } = false;

        /// <summary>
        /// Maneja el evento MouseLeftButtonDown para el botón Cancelar.
        /// Establece la propiedad BotonCancelarPulsado a true, establece el DialogResult a false y cierra la ventana.
        /// </summary>
        private void btnCancelar_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            BotonCancelarPulsado = true;
            this.DialogResult = false;
            this.Close();
        }

        /// <summary>
        /// Maneja el evento MouseEnter para el botón Cancelar.
        /// Aplica un efecto de sombra y una animación de escala al botón.
        /// </summary>
        private void btnCancelar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnCancelar.Effect = new DropShadowEffect
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
            btnCancelar.RenderTransform = new ScaleTransform();
            btnCancelar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            btnCancelar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Maneja el evento MouseLeave para el botón Cancelar.
        /// Elimina el efecto de sombra y revierte la animación de escala al botón.
        /// </summary>
        private void btnCancelar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnCancelar.Effect = null;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.1,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            btnCancelar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            btnCancelar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Maneja el evento MouseLeftButtonDown para el botón Aceptar.
        /// Establece la propiedad BotonAceptarPulsado a true, establece el DialogResult a true y cierra la ventana.
        /// </summary>
        private void btnAceptar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BotonAceptarPulsado = true;
            this.DialogResult = true;
            this.Close();
        }

        /// <summary>
        /// Maneja el evento MouseEnter para el botón Aceptar.
        /// Aplica un efecto de sombra y una animación de escala al botón.
        /// </summary>
        private void btnAceptar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnAceptar.Effect = new DropShadowEffect
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
            btnAceptar.RenderTransform = new ScaleTransform();
            btnAceptar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            btnAceptar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Maneja el evento MouseLeave para el botón Aceptar.
        /// Elimina el efecto de sombra y revierte la animación de escala al botón.
        /// </summary>
        private void btnAceptar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnAceptar.Effect = null;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = 1.1,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.1)
            };
            btnAceptar.RenderTransform.BeginAnimation(ScaleTransform.ScaleXProperty, animation);
            btnAceptar.RenderTransform.BeginAnimation(ScaleTransform.ScaleYProperty, animation);
        }

        /// <summary>
        /// Maneja el evento MouseDown para el borde de la ventana.
        /// Permite mover la ventana cuando se mantiene presionado el botón izquierdo del ratón.
        /// </summary>
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}