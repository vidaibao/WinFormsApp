namespace StudentMgtV3
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new frmStudentsList());
            
            /*
             *  Copy the three files, .cs, .designer, resx to the target solution folder.
                In the target project, select Add existing item and add the designer file first.
                Modify the Namespace attribute. The .cs file should come in as well.
                Modify the namespace in the .cs file.
                Add the resx file using Add existing item
             */
        }
    }
}