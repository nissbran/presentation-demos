fromCategory("account")
.foreachStream()
    .when({
        $init: function (s, e) {
            return { balance: 0 }
        },
        'Demo.Bank.Domain.Events.Transactions.CardTransactionAddedEvent': function (s, e) {
            if (e.data.Amount) {
                s.balance += s.balance + e.data.Amount;
            }
        },
        'Demo.Bank.Domain.Events.Transactions.BankTransferTransactionAddedEvent': function (s, e) {
            if (e.data.Amount) {
                s.balance = s.balance + e.data.Amount;
            }
        }
    })