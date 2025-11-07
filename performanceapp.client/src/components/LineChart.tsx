import { useEffect, useRef, useState } from "react";
import "./LineChart.css";
import { usePortfolioBenchmark } from "../contexts/PortfolioBenchmarkContext";

const LineChart = () => {
    const { portfolioBenchmark } = usePortfolioBenchmark();

    const [svg, setSvg] = useState<string | undefined>(undefined);

    const WIDTH = 800;
    const HEIGHT = 500;

    useEffect(() => {
        if (portfolioBenchmark == null) {
            return;
        }
        const portfolioId = portfolioBenchmark[0].portfolioId;
        fetch(
            `api/svg?portfolioId=${portfolioId}&width=${WIDTH}&height=${HEIGHT}`,
        )
            .then((res) => res.text())
            .then((data) => setSvg("data:image/svg+xml;base64," + btoa(data)))
            .catch((err) => console.error(err));
    }, [portfolioBenchmark]);

    return svg == null ? (
        <div className="svg" style={{ width: WIDTH, height: HEIGHT }} />
    ) : (
        <img className="svg" src={svg} alt="Chart" />
    );
};

export default LineChart;
