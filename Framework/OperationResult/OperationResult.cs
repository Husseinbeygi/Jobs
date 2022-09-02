namespace Framework.OperationResult
{

    public class OperationResult : object
    {
        public OperationResult() : base()
        {
            ErrorMessages =
                new List<string>();

            HiddenMessages =
                new List<string>();

            InformationMessages =
                new List<string>();

            Succeeded = true;
        }

        public bool Succeeded { get; set; }
        public IList<string> ErrorMessages { get; protected set; }
        public IList<string> HiddenMessages { get; protected set; }
        public IList<string> InformationMessages { get; protected set; }

        public void AddErrorMessage(string message)
        {
            message =
                Text.Utility.Fix(message);

            if (message.Length == 0)
            {
                return;
            }

            if (ErrorMessages.Contains(message))
            {
                return;
            }

            ErrorMessages.Add(message);
        }

        public void AddHiddenMessage(string message)
        {
            message =
                Text.Utility.Fix(message);

            if (message.Length == 0)
            {
                return;
            }

            if (HiddenMessages.Contains(message))
            {
                return;
            }

            HiddenMessages.Add(message);
        }

        public void AddInformationMessage(string message)
        {
            message =
                Text.Utility.Fix(message);

            if (message.Length == 0)
            {
                return;
            }

            if (InformationMessages.Contains(message))
            {
                return;
            }

            InformationMessages.Add(message);
        }

        public void ClearAllMessages()
        {
            ClearErrorMessages();
            ClearHiddenMessages();
            ClearInformationMessages();
        }

        public void ClearNonHiddenMessages()
        {
            ClearErrorMessages();
            ClearInformationMessages();
        }

        public void ClearErrorMessages()
        {
            ErrorMessages.Clear();
        }

        public void ClearHiddenMessages()
        {
            ErrorMessages.Clear();
        }

        public void ClearInformationMessages()
        {
            InformationMessages.Clear();
        }
    }
}

