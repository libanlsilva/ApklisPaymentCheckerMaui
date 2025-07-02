namespace ApklisPaymentCheckerMaui.Interfaces
{
    public interface IApklisPaymentChecker
    {
        public (bool IsPaid, string UserName) GetPaymentInfo(string packageName);
    }
}
