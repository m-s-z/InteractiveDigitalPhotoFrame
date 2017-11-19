using System.Collections.Generic;
using System.Collections.ObjectModel;
using AAA.Utils;

namespace AAA.ViewModels
{
    /// <summary>
    /// MainViewModel class.
    /// Provides properites and methods to handle application logic.
    /// </summary>
    public class MainViewModel : IDPFLibrary.ViewModelBase
    {
        #region fields

        /// <summary>
        /// Path to some files
        /// </summary>
        private const string EXAMPLE_OF_CONST = "path/to/some/files";

        /// <summary>
        /// Backing field for TestNumberOne property.
        /// </summary>
        private string _testNumberOne;

        /// <summary>
        /// Backing field for TestNumberTwo property.
        /// </summary>
        private bool _testNumberTwo;

        private ObservableCollection<ListItem> _testList;

        #endregion

        #region properties

        /// <summary>
        /// Property indicating some testing value.
        /// </summary>
        public string TestNumberOne
        {
            get => _testNumberOne;

            set => SetProperty(ref _testNumberOne, value);
        }

        /// <summary>
        /// Flag indicating whether is something or not.
        /// </summary>
        public bool TestNumberTwo
        {
            get => _testNumberTwo;

            set => SetProperty(ref _testNumberTwo, value);
        }

        public ObservableCollection<ListItem> TestList
        {
            get => _testList;

            set => SetProperty(ref _testList, value);
        }   

    #endregion

    #region methods

        public MainViewModel()
        {
            TestNumberOne = "Hello!!!";
            TestList = new ObservableCollection<ListItem>();
            TestList.Add(new ListItem("First"));
            TestList.Add(new ListItem("First"));
            TestList.Add(new ListItem("First"));
        }

        /// <summary>
        /// TestMethod method.
        /// Updates value of TestNumberOne property if secondParameter is true.
        /// </summary>
        /// <param name="firstParameter">Value to assing to TestNumberOne property.</param>
        /// <param name="secondParameter">Flag indicating whether to change value of TestNumberOne property or not.</param>
        /// <returns>Returns flag indicating whether the value of TestNumberOne property was changed or not.</returns>
        private bool TestMethod(string firstParameter, bool secondParameter)
        {
            if (secondParameter)
            {
                TestNumberOne = firstParameter;
                return true;
            }

            return false;
        }

        #endregion
    }
}