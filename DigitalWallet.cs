namespace SWAD_Assignment2_GrpC
{
    public class DigitalWallet
    {
        public string WalletType { get; set; }
        public string WalletOwnerName { get; set; }
        public string WalletUsername { get; set; }
        public string WalletPassword { get; set; }

        public DigitalWallet(string walletType, string walletOwnerName, string walletUsername, string walletPassword)
        {
            WalletType = walletType;
            WalletOwnerName = walletOwnerName;
            WalletUsername = walletUsername;
            WalletPassword = walletPassword;
        }

        // Start of Yong Shyan's methods
        // -----------------------------------------------------------------------------------------------
        // Prompt user for wallet type
        static public string promptWalletType()
        {
            Console.Write("Wallet Type: ");
            string walletType = Console.ReadLine();

            return walletType;
        }

        // Prompt user for wallet name
        static public string promptWalletName()
        {
            Console.Write("Wallet Owner Name: ");
            string walletName = Console.ReadLine();

            return walletName;
        }

        // Prompt user for wallet username
        static public string promptWalletUsername(string walletType)
        {
            string capitalizedWalletType = char.ToUpper(walletType[0]) + walletType.Substring(1);
            Console.WriteLine("\nLog in with " + capitalizedWalletType);
            Console.Write("Wallet Username: ");
            string walletUsername = Console.ReadLine();

            return walletUsername;
        }

        // Prompt user for wallet password
        static public string promptWalletPassword()
        {
            Console.Write("Wallet Password: ");
            string walletPassword = Console.ReadLine();

            return walletPassword;
        }

        // Verify digital payment details
        public bool verifyDigitalPaymentDetails(DigitalWallet digitalWalletPayment)
        {
            // Make DigitalWallet list for validation
            List<DigitalWallet> digitalWallets = new List<DigitalWallet>();

            //Simulate process with the digital wallet externally and get OK response
            string validCCPath = @"C:\Users\user\Documents\Files\Ngee Ann\Y2 Semester 1\Software Analysis & Design 4CU\Assignment 2\SWAD_Assignment2_GrpC\validDigitalWallet.txt";
            string[] lines = File.ReadAllLines(validCCPath);

            for (int i = 0; i < lines.Length; i += 4)
            {
                // Extract and trim details from each line
                string validWalletType = lines[i].Split(':')[1].Trim();
                string validWalletOwnerName = lines[i + 1].Split(':')[1].Trim();
                string validWalletUsername = lines[i + 2].Split(':')[1].Trim();
                string validWalletPassword = lines[i + 3].Split(':')[1].Trim();

                // Make CreditCard object to add to list of valid credit cards
                DigitalWallet validDigitalWallet = new DigitalWallet(validWalletType, validWalletOwnerName, validWalletUsername, validWalletPassword);
                digitalWallets.Add(validDigitalWallet);
            }

            // Check if the input credit card matches any in the valid list
            bool valid = false;
            foreach (DigitalWallet digitalWallet in digitalWallets)
            {
                if (digitalWalletPayment.WalletType.Trim().ToLower() == digitalWallet.WalletType.Trim().ToLower() &&
                    digitalWalletPayment.WalletOwnerName.Trim().ToLower() == digitalWallet.WalletOwnerName.Trim().ToLower() &&
                    digitalWalletPayment.WalletUsername.Equals(digitalWallet.WalletUsername) &&
                    digitalWalletPayment.WalletPassword.Equals(digitalWallet.WalletPassword))
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
