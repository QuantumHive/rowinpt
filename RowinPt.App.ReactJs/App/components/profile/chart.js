import React from "react";
import { Line } from "react-chartjs-2";
import moment from "moment";

const options = {
    legend: {
        display: false,
    },
    scales: {
        xAxes: [{
            gridLines: {
                display: false,
            },
            ticks: {
                autoSkipPadding: 20,
            }
        }],
        yAxes: [{
            gridLines: {
                drawBorder: false,
                drawTicks: true,
            },
            ticks: {
                maxTicksLimit: 4
            }
        }]
    }
};

const datasetsTemplate = {
    fill: false,
    lineTension: 0.3,
    spanGaps: true
};

function generateLabels() {

}

export default ({ measurements, type }) => {
    const start = moment().startOf("month").subtract(5, "months");

    const labels = [];
    const data = [];

    for (let i = 0; i <= 5; i++) {
        labels.push(start.format("MMM"));
        const measurement = measurements.find(m => moment(m.date).startOf("month").isSame(start, "month"));
        data.push(measurement === undefined ? null : measurement[type]);

        start.add(1, "month");
    }

    const dataContainer = {
        labels,
        datasets: [
            {
                ...datasetsTemplate,
                data
            }
        ]
    };

    return (
        <div className="mb-4">
            {
                data.some(d => d !== null) ?
                    <Line data={dataContainer} options={options} />
                    :
                    <div className="alert alert-info">
                        Geen data beschikbaar
                    </div> 
            }

        </div>
    );
}