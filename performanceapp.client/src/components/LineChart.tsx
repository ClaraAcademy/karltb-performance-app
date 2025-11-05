import { useEffect, useRef, useState } from "react";
import "./LineChart.css";
import { usePortfolioBenchmark } from "../contexts/PortfolioBenchmarkContext";

const LineChart = () => {
    const { portfolioBenchmark } = usePortfolioBenchmark();

    const [svg, setSvg] = useState<string | undefined>(undefined);

    useEffect(() => {
        if (portfolioBenchmark == null) {
            return;
        }
        const portfolioId = portfolioBenchmark[0].portfolioId;
        const benchmarkId = portfolioBenchmark[0].benchmarkId;
        fetch(`api/Svg?portfolioId=${portfolioId}&benchmarkId=${benchmarkId}`)
            .then((res) => res.text())
            .then((data) => setSvg("data:image/svg+xml;base64," + btoa(data)))
            .catch((err) => console.error(err));
    }, [portfolioBenchmark]);

    return <img src={svg} alt="Chart" />;
};

export default LineChart;
