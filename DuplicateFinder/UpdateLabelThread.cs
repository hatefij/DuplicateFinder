using System.Threading;

namespace DuplicateFinder
{
    public class UpdateLabelThread
    {
        private Form1 myForm;

        public bool Active { get; set; } = false;

        public UpdateLabelThread(Form1 form)
        {
            myForm = form;
        }

        public void UpdateStatus()
        {
            while (Active)
            {
                myForm.Invoke(myForm.updateStatusLabelDelegate);
            }

            myForm.Invoke(myForm.resetStatusLabelDelegate);
        }
    }
}
