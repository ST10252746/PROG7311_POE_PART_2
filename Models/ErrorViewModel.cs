namespace ST10252746_PROG7311_POE_PART_2.Models
{
    //This class is used to send error details to the error view

    public class ErrorViewModel
    {
        //Stores the ID of the request that caused the error which helps when checking logs
        public string? RequestId { get; set; }

        //Returns true if the RequestId has a value so it can be shown on the error page
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
