namespace SWAD_Assignment2_GrpC
{
    public class CreditCard
    {
        public long CreditCardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CvvNumber { get; set; }
        public string CardholderName { get; set; }

        public CreditCard(long creditcardnumber, DateTime expirationDate, int cvvNumber, string cardholdername)
        {
            CreditCardNumber = creditcardnumber;
            ExpirationDate = expirationDate;
            CvvNumber = cvvNumber;
            CardholderName = cardholdername;
        }

        // Start of Yong Shyan's methods
        // -----------------------------------------------------------------------------------------------
        // Prompt user for credit card number
        static public long promptCreditCardNumber()
        {
            Console.Write("16-digit Credit Card Number: ");
            long ccn = long.Parse(Console.ReadLine());

            return ccn;
        }

        // Prompt user for credit card expiry date
        static public DateTime promptCreditCardExpiry()
        {
            Console.Write("Credit Card Expiration Date (yy-mm): ");
            DateTime exp = Convert.ToDateTime(Console.ReadLine());

            return exp;
        }

        // Prompt user for credit card cvv
        static public int promptCreditCardCVV()
        {
            Console.Write("Credit Card CVV: ");
            int cvv = int.Parse(Console.ReadLine());

            return cvv;
        }

        // Prompt user for credit card name
        static public string promptCreditCardName()
        {
            Console.Write("Credit Cardholder Name: ");
            string cchn = Console.ReadLine();

            return cchn;
        }

        // Verify credit card details
        public bool verifyCreditCardDetails(CreditCard creditCardPayment)
        {
            // Make CreditCard list for validation
            List<CreditCard> creditCards = new List<CreditCard>();

            // Simulate Process with the bank externally and get OK response
            string validCCPath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\validCreditCard.txt";
            string[] lines = File.ReadAllLines(validCCPath);

            for (int i = 0; i < lines.Length; i += 4)
            {
                // Extract and trim details from each line
                long valid_ccn = long.Parse(lines[i].Split(':')[1].Trim());
                DateTime valid_exp = Convert.ToDateTime(lines[i + 1].Split(':')[1].Trim());
                int valid_cvv = int.Parse(lines[i + 2].Split(':')[1].Trim());
                string valid_cchn = lines[i + 3].Split(':')[1].Trim();

                // Make CreditCard object to add to list of valid credit cards
                CreditCard validCreditCard = new CreditCard(valid_ccn, valid_exp, valid_cvv, valid_cchn);
                creditCards.Add(validCreditCard);
            }

            // Check if the input credit card matches any in the valid list
            bool valid = false;
            foreach (CreditCard validCard in creditCards)
            {
                if (creditCardPayment.CreditCardNumber == validCard.CreditCardNumber &&
                    creditCardPayment.ExpirationDate == validCard.ExpirationDate &&
                    creditCardPayment.CvvNumber == validCard.CvvNumber &&
                    creditCardPayment.CardholderName.Trim().ToLower().Equals(validCard.CardholderName.Trim().ToLower()))
                {
                    valid = true; // Match found
                }
            }
            return valid;
        }
        // -----------------------------------------------------------------------------------------------
        // End of Yong Shyan's methods
    }
}
