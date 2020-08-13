function displayGraph() {
    startSpinner();
    $.ajax({
        type: "POST",
        url: getActionUrl("AdminHome", "GetGraphData"),
        data: {
            id: $('#select-exam-type').val()
        },
        success: function (result) {
            var dataSet = [
                { x: new Date(2018, 03, 29, 16, 00, 00), y: 1.3 },
                { x: new Date(2018, 03, 29, 16, 10, 00), y: 1.5 },
                { x: new Date(2018, 03, 29, 16, 20, 00), y: 1.9 },
                { x: new Date(2018, 03, 29, 16, 30, 00), y: 2.0 },
                { x: new Date(2018, 03, 29, 16, 40, 00), y: 2.2 },
                { x: new Date(2018, 03, 29, 16, 50, 00), y: 1.9 },
                { x: new Date(2018, 03, 29, 17, 00, 00), y: 1.7 },
                { x: new Date(2018, 03, 29, 17, 10, 00), y: 2.1 },
                { x: new Date(2018, 03, 29, 17, 20, 00), y: 1.9 },
                { x: new Date(2018, 03, 29, 17, 30, 00), y: 1.8 },
                { x: new Date(2018, 03, 29, 17, 40, 00), y: 1.9 },
                { x: new Date(2018, 03, 29, 17, 50, 00), y: 2.2 },
                { x: new Date(2018, 03, 29, 18, 00, 00), y: 2.0 },
                { x: new Date(2018, 03, 29, 18, 10, 00), y: 1.8 },
                { x: new Date(2018, 03, 29, 18, 20, 00), y: 2.1 },
                { x: new Date(2018, 03, 29, 18, 30, 00), y: 2.0 },
                { x: new Date(2018, 03, 29, 18, 40, 00), y: 2.2 },
                { x: new Date(2018, 03, 29, 18, 50, 00), y: 2.4 }
            ];
            createChart(dataSet);
            stopSpinner();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            errorMessage("Hata Oluştu");
            stopSpinner();
        }
    });
}
