using RowinPt.Contract.Models;

namespace RowinPt.Contract.Commands.Account
{
    public class ActivateAccountCommand
    {
        public ActivateAccountCommand(Activation activation)
        {
            Activation = activation;
        }

        public Activation Activation { get; }
    }
}
