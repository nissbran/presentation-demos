fromCategory("account")
.when({
    $init: function (s,e) {
        return { count: 0 }
    },
    'Demo.Bank.Domain.Events.Transactions.CardTransactionAddedEvent': function(s,e) {
        
        if (e.data.Amount > 50000)
        {
            s.count++;

            linkTo("$large-card-transactions", e);
        }
    }
})