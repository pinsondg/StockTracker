window.helperFunctions = {
    changeModalState: function (modalId, action) {
        $("#" + modalId).modal(action);
    }
}