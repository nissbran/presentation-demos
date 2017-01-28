fromCategory("account")
.when({
    $init: function (s, e) {
        return { count: 0 }
    },
    'Demo.Bank.Domain.Events.Account.AccountCreatedEvent': function (s, e) {
        s.count++;
    }
})