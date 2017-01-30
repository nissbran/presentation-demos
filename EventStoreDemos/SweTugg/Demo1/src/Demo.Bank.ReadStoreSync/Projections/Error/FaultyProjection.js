fromCategory("account")
.foreachStream()
.when({
    $initShared: function(s,e) {
        return { balance:0}
    },
    $any: function(s,e) 
    {
        if (e.data.Amount)
            s.balance += e.data.Amount;
    }
})