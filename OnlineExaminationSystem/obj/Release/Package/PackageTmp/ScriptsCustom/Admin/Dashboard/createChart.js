function createChart(dataSet) {

    var chart = new window.CanvasJS.Chart("chartContainer", {
        animationEnabled: true,
        theme: "light2",
        axisX: {
            valueFormatString: "HH:mm",
            intervalType: "minute",
            crosshair: {
                enabled: true,
                snapToDataPoint: true
            }
        },
        axisY: {
            valueFormatString: "## ",
            title: "Doğru Cevap",
            suffix: "Soru",
            crosshair: {
                enabled: true
            }
        },
        toolTip: {
            shared: true
        },
        legend: {
            cursor: "pointer",
            verticalAlign: "bottom",
            horizontalAlign: "left",
            dockInsidePlotArea: true,
            itemclick: toogleDataSeries
        },
        data: [{
            type: "line",
            showInLegend: true,
            name: "Soru Sayısı",
            markerType: "square",
            xValueFormatString: "DD/MM/YYYY HH:mm",
            yValueFormatString: "# Soru",
            color: "#F08080",
            dataPoints: dataSet
        }]
    });
    chart.render();

    function toogleDataSeries(e) {
        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
            e.dataSeries.visible = false;
        } else {
            e.dataSeries.visible = true;
        }
        chart.render();
    }
}