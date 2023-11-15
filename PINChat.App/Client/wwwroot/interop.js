window.blazorHelpers = {
    triggerClick: function(elementId) {
        const element = document.getElementById(elementId);
        if (element) {
            element.click();
        }
    }
};