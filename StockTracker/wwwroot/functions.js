window.helperFunctions = {
    changeModalState: function (modalId, action) {
        $("#" + modalId).modal(action);
    },
    openInMarketWatch: function (ticker) {
        window.open('https://www.marketwatch.com/investing/stock/' + ticker, '_blank');
    }
}