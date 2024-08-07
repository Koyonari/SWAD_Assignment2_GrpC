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
    }
}
