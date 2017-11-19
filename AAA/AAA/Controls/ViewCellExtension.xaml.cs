using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AAA.Controls
{
    /// <summary>
    /// ViewCellExtension class.
    /// Extends View Cell  with Command property.
    /// </summary>
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
        /// Bindable property for MainText.
        /// </summary>
        public static readonly BindableProperty MainTextProperty =
            BindableProperty.Create("MainText", typeof(string), typeof(ViewCellExtension));

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
        /// Object specified by a binding source used by AdditionalCommand.
        /// </summary>
        public object AdditionalCommandParameter
        {
            get => GetValue(AdditionalCommandParameterProperty);
            set => SetValue(AdditionalCommandParameterProperty, value);
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
        /// Object specified by a binding source used by MainCommand.
        /// </summary>
        public object MainCommandParameter
        {
            get => GetValue(MainCommandParameterProperty);
            set => SetValue(MainCommandParameterProperty, value);
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
        /// Subtext dispalyed on the cell.
        /// </summary>
        public string Subtext
        {
            get => (string)GetValue(SubtextProperty);
            set => SetValue(SubtextProperty, value);
        }

        #endregion

        #region methods

        public ViewCellExtension()
        {
            InitializeComponent();
            Subtext = "";
        }

        /// <summary>
        /// Executes the ViewCellExtension MainCommand property.
        /// </summary>
        protected override void OnTapped()
        {
            MainCommand?.Execute(MainCommandParameter);
        }

        #endregion
    }
}