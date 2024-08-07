namespace SWAD_Assignment2_GrpC
{
    public class CreditCard
    {
        public int CreditCardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CvvNumber { get; set; }
        public string CardholderName { get; set; }

        public CreditCard(int creditcardnumber, DateTime expirationDate, int cvvNumber, string cardholdername)
        {
            CreditCardNumber = creditcardnumber;
            ExpirationDate = expirationDate;
            CvvNumber = cvvNumber;
            CardholderName = cardholdername;
        }
    }
}
