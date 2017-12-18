using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DPF.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewCellExtension : ViewCell
    {
        #region properties

        /// <summary>
        /// Bindable property for AdditionalCommand.
        /// </summary>
        public static readonly BindableProperty AdditionalCommandProperty =
            BindableProperty.Create("AdditionalCommand", typeof(ICommand), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for AdditionalCommandParameter.
        /// </summary>
        public static readonly BindableProperty AdditionalCommandParameterProperty =
            BindableProperty.Create("AdditionalCommandParameter", typeof(object), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for AdditionalImageSource.
        /// </summary>
        public static readonly BindableProperty AdditionalImageSourceProperty =
            BindableProperty.Create("AdditionalImageSource", typeof(string), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for MainCommand.
        /// </summary>
        public static readonly BindableProperty MainCommandProperty =
            BindableProperty.Create("MainCommand", typeof(ICommand), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for MainCommandParameter.
        /// </summary>
        public static readonly BindableProperty MainCommandParameterProperty =
            BindableProperty.Create("MainCommandParameter", typeof(object), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for MainImageSource.
        /// </summary>
        public static readonly BindableProperty MainImageSourceProperty =
            BindableProperty.Create("MainImageSource", typeof(string), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for MainText.
        /// </summary>
        public static readonly BindableProperty MainTextProperty =
            BindableProperty.Create("MainText", typeof(string), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for SubimageSource.
        /// </summary>
        public static readonly BindableProperty SubimageSourceProperty =
            BindableProperty.Create("SubimageSource", typeof(string), typeof(ViewCellExtension));

        /// <summary>
        /// Bindable property for Subtext.
        /// </summary>
        public static readonly BindableProperty SubtextProperty =
            BindableProperty.Create("Subtext", typeof(string), typeof(ViewCellExtension));

        /// <summary>
        /// Additional command to execute on right side of item tap.
        /// </summary>
        public ICommand AdditionalCommand
        {
            get => (ICommand)GetValue(AdditionalCommandProperty);
            set => SetValue(AdditionalCommandProperty, value);
        }

        /// <summary>
        /// Main command to execute on item tap.
        /// </summary>
        public ICommand MainCommand
        {
            get => (ICommand)GetValue(MainCommandProperty);
            set => SetValue(MainCommandProperty, value);
        }

        /// <summary>
        /// Object specified by a binding source used by AdditionalCommand.
        /// </summary>
        public object AdditionalCommandParameter
        {
            get => GetValue(AdditionalCommandParameterProperty);
            set => SetValue(AdditionalCommandParameterProperty, value);
        }

        /// <summary>
        /// Object specified by a binding source used by MainCommand.
        /// </summary>
        public object MainCommandParameter
        {
            get => GetValue(MainCommandParameterProperty);
            set => SetValue(MainCommandParameterProperty, value);
        }

        /// <summary>
        /// Source to additional image displayed on the cell.
        /// </summary>
        public string AdditionalImageSource
        {
            get => (string)GetValue(AdditionalImageSourceProperty);
            set => SetValue(AdditionalImageSourceProperty, value);
        }

        /// <summary>
        /// Source to main image displayed on the cell.
        /// </summary>
        public string MainImageSource
        {
            get => (string)GetValue(MainImageSourceProperty);
            set => SetValue(MainImageSourceProperty, value);
        }

        /// <summary>
        /// Main text displayed on the cell.
        /// </summary>
        public string MainText
        {
            get => (string)GetValue(MainTextProperty);
            set => SetValue(MainTextProperty, value);
        }

        /// <summary>
        /// Source to subimage displayed on the cell.
        /// </summary>
        public string SubimageSource
        {
            get => (string)GetValue(SubimageSourceProperty);
            set => SetValue(SubimageSourceProperty, value);
        }

        /// <summary>
        /// Subtext dispalyed on the cell.
        /// </summary>
        public string Subtext
        {
            get => (string)GetValue(SubtextProperty);
            set => SetValue(SubtextProperty, value);
        }

        #endregion

        #region methods

        /// <summary>
        /// ViewCellExtension class constructor.
        /// </summary>
        public ViewCellExtension()
        {
            InitializeComponent();
            Subtext = "";
        }

        #endregion
    }
}