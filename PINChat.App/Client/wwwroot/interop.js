window.blazorHelpers = {
    scrollToBottom: function(element) {
        if (element.scrollHeight != null)
            element.scrollTop = element.scrollHeight;
    }
};