namespace StudentMgtV2
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

            // prevent open 2 forms
            bool isOpen = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text.Equals("List of Students"))
                {
                    isOpen = true; 
                    f.BringToFront();
                    break;
                }
            }
            if (!isOpen) Application.Run(new frmStudentsList());
        }
    }
}